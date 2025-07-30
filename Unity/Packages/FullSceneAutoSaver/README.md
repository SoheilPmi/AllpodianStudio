# 🧠 FullSceneAutoSaver for Unity (with Easy Save 3)

This Unity system provides **complete automatic save/load functionality** for your game using [Easy Save 3](https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-load-asset-768).  
It captures a **snapshot of the entire scene** — including all GameObjects, components, serialized fields, and hierarchy — and restores it exactly as it was.

---

## 🚀 Features

- ✅ Automatically saves **all active GameObjects** in the scene
- ✅ Stores **Transform, name, tag, layer, active state**
- ✅ Serializes **all components and their fields** using reflection
- ✅ Reconstructs **parent-child hierarchy**
- ✅ Supports **runtime-instantiated objects**
- ✅ Works with **manual or automatic saving**
- ✅ No need to manually tag or register objects

---

## 📦 Requirements

- Unity 2022.3.21 or newer  
- Easy Save 3 (ES3) installed in your project  
- Optional: Prefabs stored in `Resources/` if you want to restore original assets

---

## 🛠 How It Works

### Saving

1. Traverses all root GameObjects in the active scene
2. Recursively visits all children
3. Adds a `UniqueID` component if missing
4. Saves:
   - Transform (position, rotation, scale)
   - GameObject info (name, tag, layer, active state)
   - Parent ID
   - All components and their serializable fields
5. Stores all object IDs in a master list

### Loading

1. Clears the current scene (except the SaveManager itself)
2. Recreates each GameObject from saved data
3. Adds back all components and sets their fields
4. Reconstructs parent-child relationships

---

## 📂 File Structure

```text
Assets/
├── Scripts/
│   ├── UniqueID.cs
│   └── FullSceneAutoSaver.cs
```

---

## 🧪 Usage

### Setup

1. Create an empty GameObject in your first scene
2. Attach `FullSceneAutoSaver.cs` to it
3. (Optional) Set `autoSaveInterval` in seconds for automatic saving
4. Press `F5` to save, `F9` to load

### Optional: Prefab Restoration

If you want to restore objects from original prefabs:
- Add a `PrefabReference` component to your prefab
- Set the `resourcePath` to match its location in `Resources/`

Example:
```csharp
public class PrefabReference : MonoBehaviour
{
    public string resourcePath = "Prefabs/Enemy";
}
```

---

## 🧠 Technical Notes

- Uses `System.Reflection` to serialize all `[SerializeField]` and public fields
- Skips static fields and non-serializable data
- Does **not** serialize delegates, events, or coroutine states
- Can be extended to support multiple save slots, encryption, cloud sync, etc.

---

## 💡 Limitations

- Only serializable fields are saved (Unity serialization rules apply)
- Runtime-only data like coroutines or event subscriptions must be manually restored
- Large scenes may take time to save/load — consider using coroutines for async behavior

---

## 📸 Concept

Think of this system as a **data screenshot** of your game scene.  
It captures everything visible and internal at that moment — so you can return to it exactly as it was.

---

## 📄 License

MIT License — free to use, modify, and distribute.

---

## 🙌 Credits

Built with ❤️ using Unity and Easy Save 3  
Developed by [Soheilpmi AllpodianStudio + Ai]
