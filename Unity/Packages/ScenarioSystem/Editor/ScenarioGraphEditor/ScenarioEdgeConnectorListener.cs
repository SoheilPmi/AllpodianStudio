using UnityEditor.Experimental.GraphView;

namespace ScenarioSystem.Editor
{
    public class ScenarioEdgeConnectorListener : IEdgeConnectorListener
    {
        readonly ScenarioGraphView _graphView;
        public ScenarioEdgeConnectorListener(ScenarioGraphView gv) =>
            _graphView = gv;

        public void OnDropOutsidePort(Edge edge, Vector2 pos) { }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            if (edge.input != null && edge.output != null)
                _graphView.AddElement(edge);
        }
    }
}
