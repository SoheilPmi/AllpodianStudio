using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioSystem.Runtime
{
    public class ScenarioRunner : MonoBehaviour
    {
        public ScenarioGraphDefinition graphDefinition;
        public Blackboard blackboard;

        public event Action<ScenarioNodeBase> OnNodeStart;
        public event Action OnScenarioComplete;

        Dictionary<ScenarioNodeBase, List<ScenarioNodeBase>> runtimeGraph;

        void Start() => StartCoroutine(Run());

        IEnumerator Run()
        {
            BuildGraph();
            var start = graphDefinition.nodes
                .FirstOrDefault(n => n.nodeName == "Start");
            if (start == null) yield break;
            yield return ExecuteNode(start);
            OnScenarioComplete?.Invoke();
        }

        void BuildGraph()
        {
            runtimeGraph = graphDefinition.nodes
                .ToDictionary(n => n, n => new List<ScenarioNodeBase>());
            foreach (var conn in graphDefinition.connections)
                runtimeGraph[conn.fromNode].Add(conn.toNode);
        }

        IEnumerator ExecuteNode(ScenarioNodeBase node)
        {
            OnNodeStart?.Invoke(node);
            var e = node.Execute(blackboard);
            while (e.MoveNext()) yield return e.Current;
            foreach (var child in runtimeGraph[node])
                yield return ExecuteNode(child);
        }
    }
}
