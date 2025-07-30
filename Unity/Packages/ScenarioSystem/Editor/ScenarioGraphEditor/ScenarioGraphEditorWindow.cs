using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using ScenarioSystem.Runtime;

namespace ScenarioSystem.Editor
{
    public class ScenarioGraphEditorWindow : EditorWindow
    {
        ScenarioGraphView graphView;

        [MenuItem("Window/Scenario Graph Editor")]
        public static void Open() =>
            GetWindow<ScenarioGraphEditorWindow>("Scenario Graph Editor");

        void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
            GenerateMiniMap();
        }

        void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        void ConstructGraphView()
        {
            graphView = new ScenarioGraphView
            {
                name = "Scenario Graph"
            };
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        void GenerateToolbar()
        {
            var toolbar = new Toolbar();
            var saveButton = new ToolbarButton(() =>
            {
                graphView.RequestDataOperation(true);
            })
            { text = "Save Asset" };
            toolbar.Add(saveButton);
            rootVisualElement.Add(toolbar);
        }

        void GenerateMiniMap()
        {
            var miniMap = new MiniMap { anchored = true };
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            graphView.Add(miniMap);
        }
    }
}
