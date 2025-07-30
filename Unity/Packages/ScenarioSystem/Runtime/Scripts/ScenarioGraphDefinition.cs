using UnityEngine;
using System.Collections.Generic;

namespace ScenarioSystem.Runtime
{
    [CreateAssetMenu(menuName = "Scenario System/Graph Definition")]
    public class ScenarioGraphDefinition : ScriptableObject
    {
        public List<ScenarioNodeBase> nodes = new List<ScenarioNodeBase>();
        public List<ScenarioConnection> connections = new List<ScenarioConnection>();
    }
}
