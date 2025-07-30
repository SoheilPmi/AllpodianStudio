🎮 Scenario System for Unity

``
This is Good System, But I don't Like it...
it's very Complex.
``

A powerful and flexible graph-based scenario system for Unity, designed to help developers and designers create dynamic missions, quests, and gameplay flows with ease.  
Built with extensibility, runtime execution, and persistent saving using Easy Save 3.

---

✨ Features

- 🧠 Visual DSL Editor – Create scenarios using Unity's GraphView-based editor
- ⚙️ Runtime Execution Engine – Execute graphs with conditions, loops, parallel flows, and actions
- 🧩 Modular Architecture – Easily extend with custom nodes and actions
- 💾 Persistent Progress – Save and load player progress using Easy Save 3
- 🔄 Dynamic Variables – Interact with external scripts via a shared blackboard
- 🧪 Debug-Friendly – Includes logging and editor tools for testing scenarios

---

📦 Installation

Manual Installation

1. Clone or download this repository
2. Copy the folder com.yourcompany.scenariosystem into your Unity project's Packages/ directory
3. Open Unity and go to Window → Package Manager
4. Click ➕ and choose Add package from disk...
5. Select the file:
   `
   Packages/com.AllpodianStudio.scenariosystem/package.json
   `

---

🚀 Getting Started

🧱 Create a Scenario Graph

1. Go to Assets → Create → Scenario Graph
2. Open the graph in the editor via Window → Scenario Graph Editor
3. Add nodes like:
   - Start
   - Action
   - Condition
   - Loop
   - Sequence
   - Parallel
4. Connect nodes to define flow
5. Save the graph asset

🎯 Setup in Scene

1. Create an empty GameObject
2. Add the following components:
   - ScenarioRunner
   - Blackboard
3. Assign your graph asset to the runner
4. Press Play to run the scenario

---

🧩 Extending the System

➕ Add Custom Actions

Create a new class implementing IScenarioAction:

```csharp
[ActionName("MyAction")]
public class MyAction : IScenarioAction
{
    public string message;

    public IEnumerator Run(Blackboard bb)
    {
        Debug.Log(message);
        yield return null;
    }
}
```

🧠 Add New Node Types

1. Inherit from ScenarioNodeBase
2. Implement your custom logic in Execute()
3. Register the node in the editor to make it available in the node menu

---

📚 DSL Example (Text-Based)

```plaintext
when score >= 50 -> action Destroy tag=Tree count=2;

loop(3) {
    action CollectPoints var=score target=10;
}

parallel {
    action Destroy tag=Enemy count=1;
    action CollectPoints var=score target=20;
}
```

---

💾 Saving & Loading Progress

- Integrated with Easy Save 3
- Automatically saves mission completion and variable states
- Loads progress on game start
- Works seamlessly with the Blackboard system

---

🧠 Use Cases

- 🎯 Mission-based games
- 📖 Narrative-driven experiences
- 🛠️ Designer-friendly gameplay logic
- 🧪 Prototyping complex game flows

---

📸 Screenshots

x

---

📂 Project Structure

```
com.AllpodianStudio.scenariosystem/
├── Editor/
│   └── ScenarioGraphEditor.cs
├── Runtime/
│   ├── ScenarioRunner.cs
│   ├── Blackboard.cs
│   └── Nodes/
├── GraphView/
│   └── NodeTypes/
├── Samples/
│   └── ExampleGraphs/
```

---

📄 License

This project is licensed under the MIT License.  
Feel free to use, modify, and distribute with attribution.

---

🙌 Contributing

Pull requests are welcome!  
If you have ideas for new node types, actions, or improvements, feel free to open an issue or submit a PR.

---

📬 Contact

For questions or support, reach out via GitHub Issues

---

🏁 Ready to Build Smarter Missions?

Unleash your creativity and build dynamic, designer-friendly gameplay with the Scenario System.  
Happy developing! 🚀






