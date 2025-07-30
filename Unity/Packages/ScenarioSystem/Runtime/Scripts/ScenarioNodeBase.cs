using UnityEngine;
using System.Collections;

namespace ScenarioSystem.Runtime
{
    public abstract class ScenarioNodeBase : ScriptableObject
    {
        public string nodeName;
        public Vector2 position;

        public abstract IEnumerator Execute(Blackboard bb);
    }
}
