using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ES3;

[DisallowMultipleComponent]
public class SaveManager : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Filename (inside persistentDataPath) to save/load data")]
    public string fileName = "gameSave.es3";

    // Call this to snapshot your scene
    public void SaveGame(int slot)
    {
        var uids   = FindObjectsOfType<UniqueID>();
        var idList = new List<int>();

        foreach (var uid in uids)
        {
            int id       = uid.ID;
            idList.Add(id);
            string key   = $"{slot}_{id}";
            Transform t  = uid.transform;

            // 1) Transform data
            ES3.Save($"{key}_pos",   t.position,               fileName);
            ES3.Save($"{key}_rot",   t.rotation.eulerAngles,   fileName);
            ES3.Save($"{key}_scale", t.localScale,             fileName);
            ES3.Save($"{key}_active",t.gameObject.activeSelf, fileName);

            // 2) Prefab via Addressables
            var addrRef = uid.GetComponent<AddressableReference>();
            if (addrRef != null && addrRef.prefabRef.RuntimeKeyIsValid())
            {
                string runtimeKey = addrRef.prefabRef.RuntimeKey.ToString();
                ES3.Save($"{key}_addrKey", runtimeKey, fileName);
            }

            // 3) All other components and their fields
            var compList = new List<string>();
            foreach (var comp in uid.GetComponents<Component>())
            {
                var type = comp.GetType();
                if (type == typeof(Transform) 
                    || type == typeof(UniqueID) 
                    || type == typeof(AddressableReference))
                    continue;

                compList.Add(type.AssemblyQualifiedName);
                SaveComponentFields(comp, key);
            }
            ES3.Save($"{key}_comps", compList, fileName);
        }

        // Master list of IDs
        ES3.Save($"{slot}_idList", idList, fileName);
        Debug.Log($"✅ Game saved to '{fileName}' (slot {slot})");
    }

    // Call this to restore your scene
    public void LoadGame(int slot)
    {
        StartCoroutine(LoadCoroutine(slot));
    }

    private IEnumerator LoadCoroutine(int slot)
    {
        // 1) Destroy existing dynamic objects
        foreach (var existing in FindObjectsOfType<UniqueID>())
        {
            if (existing.GetComponent<AddressableReference>() != null)
                Destroy(existing.gameObject);
        }

        string idListKey = $"{slot}_idList";
        if (!ES3.KeyExists(idListKey, fileName))
        {
            Debug.LogWarning($"⛔ No save found in '{fileName}' for slot {slot}");
            yield break;
        }

        var idList = ES3.Load<List<int>>(idListKey, fileName);

        // 2) Recreate each object
        var created = new Dictionary<int, GameObject>();
        foreach (int id in idList)
        {
            string key   = $"{slot}_{id}";
            bool   active= ES3.Load<bool>($"{key}_active", fileName);
            Vector3 pos  = ES3.Load<Vector3>($"{key}_pos", fileName);
            Vector3 rotE = ES3.Load<Vector3>($"{key}_rot", fileName);
            Vector3 scale= ES3.Load<Vector3>($"{key}_scale", fileName);

            // 2a) Instantiate via Addressables or empty GameObject
            GameObject go;
            string addrKey = $"{key}_addrKey";
            if (ES3.KeyExists(addrKey, fileName))
            {
                string runtimeKey = ES3.Load<string>(addrKey, fileName);
                var handle = Addressables.LoadAssetAsync<GameObject>(runtimeKey);
                yield return handle;
                go = handle.Status == AsyncOperationStatus.Succeeded
                     ? Instantiate(handle.Result)
                     : new GameObject($"Object_{id}");
            }
            else
            {
                go = new GameObject($"Object_{id}");
            }

            // 2b) Restore transform & active
            var tr = go.transform;
            tr.position    = pos;
            tr.rotation    = Quaternion.Euler(rotE);
            tr.localScale  = scale;
            go.SetActive(active);

            // 2c) Assign UniqueID
            var uidComp = go.AddComponent<UniqueID>();
            uidComp.ID  = id;

            created[id] = go;

            // 2d) Re-add components and restore their fields
            var compList = ES3.Load<List<string>>($"{key}_comps", fileName);
            foreach (var qName in compList)
            {
                var type = Type.GetType(qName);
                if (type == null) continue;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ES3;

[DisallowMultipleComponent]
public class SaveManager : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Filename (inside persistentDataPath) to save/load data")]
    public string fileName = "gameSave.es3";

    // Call this to snapshot your scene
    public void SaveGame(int slot)
    {
        var uids   = FindObjectsOfType<UniqueID>();
        var idList = new List<int>();

        foreach (var uid in uids)
        {
            int id       = uid.ID;
            idList.Add(id);
            string key   = $"{slot}_{id}";
            Transform t  = uid.transform;

            // 1) Transform data
            ES3.Save($"{key}_pos",   t.position,               fileName);
            ES3.Save($"{key}_rot",   t.rotation.eulerAngles,   fileName);
            ES3.Save($"{key}_scale", t.localScale,             fileName);
            ES3.Save($"{key}_active",t.gameObject.activeSelf, fileName);

            // 2) Prefab via Addressables
            var addrRef = uid.GetComponent<AddressableReference>();
            if (addrRef != null && addrRef.prefabRef.RuntimeKeyIsValid())
            {
                string runtimeKey = addrRef.prefabRef.RuntimeKey.ToString();
                ES3.Save($"{key}_addrKey", runtimeKey, fileName);
            }

            // 3) All other components and their fields
            var compList = new List<string>();
            foreach (var comp in uid.GetComponents<Component>())
            {
                var type = comp.GetType();
                if (type == typeof(Transform) 
                    || type == typeof(UniqueID) 
                    || type == typeof(AddressableReference))
                    continue;

                compList.Add(type.AssemblyQualifiedName);
                SaveComponentFields(comp, key);
            }
            ES3.Save($"{key}_comps", compList, fileName);
        }

        // Master list of IDs
        ES3.Save($"{slot}_idList", idList, fileName);
        Debug.Log($"✅ Game saved to '{fileName}' (slot {slot})");
    }

    // Call this to restore your scene
    public void LoadGame(int slot)
    {
        StartCoroutine(LoadCoroutine(slot));
    }

    private IEnumerator LoadCoroutine(int slot)
    {
        // 1) Destroy existing dynamic objects
        foreach (var existing in FindObjectsOfType<UniqueID>())
        {
            if (existing.GetComponent<AddressableReference>() != null)
                Destroy(existing.gameObject);
        }

        string idListKey = $"{slot}_idList";
        if (!ES3.KeyExists(idListKey, fileName))
        {
            Debug.LogWarning($"⛔ No save found in '{fileName}' for slot {slot}");
            yield break;
        }

        var idList = ES3.Load<List<int>>(idListKey, fileName);

        // 2) Recreate each object
        var created = new Dictionary<int, GameObject>();
        foreach (int id in idList)
        {
            string key   = $"{slot}_{id}";
            bool   active= ES3.Load<bool>($"{key}_active", fileName);
            Vector3 pos  = ES3.Load<Vector3>($"{key}_pos", fileName);
            Vector3 rotE = ES3.Load<Vector3>($"{key}_rot", fileName);
            Vector3 scale= ES3.Load<Vector3>($"{key}_scale", fileName);

            // 2a) Instantiate via Addressables or empty GameObject
            GameObject go;
            string addrKey = $"{key}_addrKey";
            if (ES3.KeyExists(addrKey, fileName))
            {
                string runtimeKey = ES3.Load<string>(addrKey, fileName);
                var handle = Addressables.LoadAssetAsync<GameObject>(runtimeKey);
                yield return handle;
                go = handle.Status == AsyncOperationStatus.Succeeded
                     ? Instantiate(handle.Result)
                     : new GameObject($"Object_{id}");
            }
            else
            {
                go = new GameObject($"Object_{id}");
            }

            // 2b) Restore transform & active
            var tr = go.transform;
            tr.position    = pos;
            tr.rotation    = Quaternion.Euler(rotE);
            tr.localScale  = scale;
            go.SetActive(active);

            // 2c) Assign UniqueID
            var uidComp = go.AddComponent<UniqueID>();
            uidComp.ID  = id;

            created[id] = go;

            // 2d) Re-add components and restore their fields
            var compList = ES3.Load<List<string>>($"{key}_comps", fileName);
            foreach (var qName in compList)
            {
                var type = Type.GetType(qName);
                if (type == null) continue;
                var comp = go.AddComponent(type) as Component;
                LoadComponentFields(comp, key);
            }

            yield return null; // spread instantiation over frames
        }

        // 3) (Optional) Reparent if needed—extend here if you also saved parent IDs

        Debug.Log($"✅ Game loaded from '{fileName}' (slot {slot})");
    }

    // Reflection save: every public or [SerializeField] field
    private void SaveComponentFields(Component comp, string baseKey)
    {
        var type   = comp.GetType();
        var fields = type.GetFields(
                         BindingFlags.Instance
                       | BindingFlags.Public
                       | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            // Skip static or non-serialized
            if (field.IsStatic) continue;
            if (!field.IsPublic && field.GetCustomAttribute<SerializeField>() == null) 
                continue;

            object value = field.GetValue(comp);
            string key   = $"{baseKey}_{type.FullName}_{field.Name}";
            ES3.Save(key, value, fileName);
        }
    }

    // Reflection load: mirror SaveComponentFields
    private void LoadComponentFields(Component comp, string baseKey)
    {
        var type   = comp.GetType();
        var fields = type.GetFields(
                         BindingFlags.Instance
                       | BindingFlags.Public
                       | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            if (!field.IsPublic && field.GetCustomAttribute<SerializeField>() == null) 
                continue;

            string key  = $"{baseKey}_{type.FullName}_{field.Name}";
            if (!ES3.KeyExists(key, fileName)) 
                continue;

            object value = ES3.Load<object>(key, fileName);
            field.SetValue(comp, value);
        }
    }
}






