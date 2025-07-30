using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ScenarioSystem.Runtime
{
    public interface IScenarioAction
    {
        IEnumerator Run(Blackboard bb);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ActionNameAttribute : Attribute
    {
        public string Name;
        public ActionNameAttribute(string name) => Name = name;
    }

    public static class ActionFactory
    {
        static readonly System.Collections.Generic.Dictionary<string, Type> types;

        static ActionFactory()
        {
            types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IScenarioAction).IsAssignableFrom(t)
                         && !t.IsInterface && !t.IsAbstract)
                .ToDictionary(
                    t => t.GetCustomAttribute<ActionNameAttribute>()?.Name ?? t.Name,
                    t => t
                );
        }

        public static IScenarioAction Create(
            string typeName,
            System.Collections.Generic.Dictionary<string, string> parameters)
        {
            if (!types.TryGetValue(typeName, out var t))
            {
                Debug.LogWarning($"Action '{typeName}' not found.");
                return new EmptyAction();
            }
            var action = (IScenarioAction)Activator.CreateInstance(t);
            foreach (var kv in parameters)
            {
                var field = t.GetField(kv.Key,
                    BindingFlags.Public | BindingFlags.Instance);
                if (field != null)
                    field.SetValue(action, Convert.ChangeType(kv.Value, field.FieldType));
            }
            return action;
        }
    }

    public class EmptyAction : IScenarioAction
    {
        public IEnumerator Run(Blackboard bb) { yield break; }
    }

    [ActionName("Destroy")]
    public class DestroyAction : IScenarioAction
    {
        public string tag = "Tree";
        public int count = 1;
        public IEnumerator Run(Blackboard bb)
        {
            var objs = GameObject.FindGameObjectsWithTag(tag);
            for (int i = 0; i < Mathf.Min(count, objs.Length); i++)
                GameObject.Destroy(objs[i]);
            yield return null;
        }
    }

    [ActionName("Collect")]
    public class CollectPointsAction : IScenarioAction
    {
        public string variableName = "score";
        public float target = 0;
        public IEnumerator Run(Blackboard bb)
        {
            yield return new WaitUntil(() =>
                bb.Get<float>(variableName) >= target);
        }
    }
}
