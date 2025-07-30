using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using ScenarioSystem.Runtime;
using UnityEngine;

namespace ScenarioSystem.Editor
{
    public class ScenarioNodeView : Node
    {
        public string nodeGuid;
        public ScenarioNodeBase nodeData;

        public ScenarioNodeView(string title)
        {
            title = title;
            AddPort(Direction.Input, "In");
            AddPort(Direction.Output, "Out");
            RefreshExpandedState();
        }

        void AddPort(Direction dir, string name)
        {
            var port = InstantiatePort(
                Orientation.Horizontal,
                dir,
                Port.Capacity.Single,
                typeof(bool));
            port.portName = name;
            if (dir == Direction.Input)
                inputContainer.Add(port);
            else
                outputContainer.Add(port);
        }

        public void BindNode(ScenarioNodeBase runtimeNode)
        {
            nodeData = runtimeNode;
            title = runtimeNode.nodeName;
        }
    }
}
