using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using ScenarioSystem.Runtime;
using UnityEditor;

namespace ScenarioSystem.Editor
{
    public class ScenarioGraphView : GraphView
    {
        public readonly Vector2 defaultNodeSize = new Vector2(150, 200);

        public ScenarioGraphView()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("ScenarioGraph"));

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            AddElement(GenerateEntryPointNode());
        }

        Port GeneratePort(
            Node node,
            Direction portDirection,
            Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(
                Orientation.Horizontal,
                portDirection,
                capacity,
                typeof(bool));
        }

        Node GenerateEntryPointNode()
        {
            var node = new Node
            {
                title = "Start",
                guid = Guid.NewGuid().ToString(),
                entryPoint = true
            };
            node.SetPosition(new Rect(100, 100, 100, 150));
            var output = GeneratePort(node, Direction.Output);
            output.portName = "Next";
            node.outputContainer.Add(output);
            node.RefreshExpandedState();
            return node;
        }

        public void RequestDataOperation(bool save)
        {
            // Save to ScriptableObject or load from it
            // Implementation depends on your AssetImporter
            Debug.Log("Save Asset requested");
        }
    }
}
