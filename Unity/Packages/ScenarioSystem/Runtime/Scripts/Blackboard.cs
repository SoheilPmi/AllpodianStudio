using UnityEngine;

namespace ScenarioSystem.Runtime
{
    public class Blackboard : MonoBehaviour
    {
        private readonly System.Collections.Generic.Dictionary<string, object> data
            = new System.Collections.Generic.Dictionary<string, object>();

        public void Set<T>(string key, T value) => data[key] = value;

        public T Get<T>(string key)
        {
            if (data.TryGetValue(key, out var v) && v is T t)
                return t;
            return default;
        }
    }
}
