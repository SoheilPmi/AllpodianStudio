# 📦 Full Scene Auto Saver (Unity Package)

**FullSceneAutoSaver** is a Unity package that provides a complete save/load system using **Easy Save 3 (ES3)** and **Addressables**.  
It captures a full snapshot of your scene — including all GameObjects, transforms, and prefab references — and restores it exactly as it was.

---

## 🚀 Features

- ✅ Automatically saves all active GameObjects in the scene
- ✅ Stores transform data (position, rotation)
- ✅ Reconstructs prefab instances using Addressables
- ✅ Supports runtime-instantiated objects
- ✅ No manual tagging or registration required
- ✅ Customizable save file name via Inspector

---

## 📦 Installation

1. Clone or download this repository:
   ```
   https://github.com/SoheilPmi/AllpodianStudio
   ```

2. Copy the folder:
   ```
   Unity/Packages/FullSceneAutoSaver
   ```

3. Add it to your Unity project under:
   ```
   Assets/Packages/FullSceneAutoSaver
   ```

4. Make sure you have:
   - ✅ [Easy Save 3](https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-load-asset-768) installed
   - ✅ Addressables package enabled (`Window > Package Manager > Addressables`)

---

## 🧩 Components

### `UniqueID.cs`
Assigns a unique integer ID to each GameObject for tracking during save/load.

### `AddressableReference.cs`
Holds a reference to the prefab's Addressable asset. Required for prefab reconstruction.

### `SaveManager.cs`
Main controller for saving and loading the scene. Includes public `fileName` field to customize the save file.

---

## 🛠 How to Use

### 1. Setup Prefabs

- Mark your dynamic prefabs as **Addressable** in the Unity Editor.
- Add the `AddressableReference` component to each prefab.
- Assign the correct `AssetReference` in the Inspector.

### 2. Setup Scene

- Create an empty GameObject in your scene.
- Attach the `SaveManager` script.
- Set the `fileName` field (e.g. `mySave.es3`).

### 3. Saving

Call this from a button, event, or script:

```csharp
SaveManager saveManager = FindObjectOfType<SaveManager>();
saveManager.SaveGame(0); // Slot 0
```

### 4. Loading

Call this to restore the scene:

```csharp
SaveManager saveManager = FindObjectOfType<SaveManager>();
saveManager.LoadGame(0); // Slot 0
```

---

## 📂 File Structure

```
FullSceneAutoSaver/
├── package.json
├── Runtime/
│   ├── UniqueID.cs
│   ├── AddressableReference.cs
│   └── SaveManager.cs
```

---

## 🧠 How It Works

- During save:
  - Finds all GameObjects with `UniqueID`
  - Saves their position, rotation, and prefab reference
- During load:
  - Clears existing dynamic objects
  - Reinstantiates prefabs using Addressables
  - Restores transform and ID

---

## ⚠️ Limitations

- Only transform and prefab identity are saved (no custom component data yet)
- Events, coroutines, and non-serializable fields are not restored
- Prefabs must be marked as Addressable and assigned properly

---

## 📄 License

MIT License — free to use, modify, and distribute.

---

## 🙌 Credits

Developed by [Soheil Pmi AllpodianStudio + Ai](https://github.com/SoheilPmi)  
Powered by Unity, Easy Save 3, and Addressables
