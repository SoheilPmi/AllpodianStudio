using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using System.Collections.Generic;
using ES3;

public class SaveManager : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Filename (inside persistentDataPath) to save/load data")]
    public string fileName = "gameSave.es3";

    // Call this to snapshot your scene
    public void SaveGame(int slot)
    {
        var uids = FindObjectsOfType<UniqueID>();
        var idList = new List<int>();

        foreach (var uid in uids)
        {
            int id = uid.ID;
            idList.Add(id);
            string baseKey = $"{slot}_{id}";
            Transform t = uid.transform;

            // Save transform data
            ES3.Save($"{baseKey}_pos", t.position, fileName);
            ES3.Save($"{baseKey}_rot", t.rotation.eulerAngles, fileName);

            // Save Addressables key if present
            var addrRef = uid.GetComponent<AddressableReference>();
            if (addrRef != null && addrRef.prefabRef.RuntimeKeyIsValid())
            {
                string runtimeKey = addrRef.prefabRef.RuntimeKey.ToString();
                ES3.Save($"{baseKey}_addrKey", runtimeKey, fileName);
            }
        }

        // Save the list of IDs so we know what to load
        ES3.Save($"{slot}_idList", idList, fileName);
        Debug.Log($"Game saved to {fileName} (slot {slot})");
    }

    // Call this to restore your scene
    public void LoadGame(int slot)
    {
        StartCoroutine(LoadCoroutine(slot));
    }

    private IEnumerator LoadCoroutine(int slot)
    {
        // Clean up existing dynamic objects
        foreach (var existing in FindObjectsOfType<UniqueID>())
        {
            if (existing.GetComponent<AddressableReference>() != null)
                Destroy(existing.gameObject);
        }

        string idListKey = $"{slot}_idList";
        if (!ES3.KeyExists(idListKey, fileName))
        {
            Debug.LogWarning($"No save found in {fileName} for slot {slot}");
            yield break;
        }

        var idList = ES3.Load<List<int>>(idListKey, fileName);
        foreach (int id in idList)
        {
            string baseKey = $"{slot}_{id}";

            // Load transform
            Vector3 pos = ES3.Load<Vector3>($"{baseKey}_pos", fileName);
            Vector3 rotEuler = ES3.Load<Vector3>($"{baseKey}_rot", fileName);

            GameObject go;
            string addrKey = $"{baseKey}_addrKey";

            if (ES3.KeyExists(addrKey, fileName))
            {
                // Asynchronously load the prefab and instantiate
                string runtimeKey = ES3.Load<string>(addrKey, fileName);
                var handle = Addressables.LoadAssetAsync<GameObject>(runtimeKey);
                yield return handle;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    go = Instantiate(handle.Result);
                else
                {
                    Debug.LogError($"Failed to load Addressable: {runtimeKey}");
                    go = new GameObject($"Object_{id}");
                }
            }
            else
            {
                // Fallback: empty GameObject
                go = new GameObject($"Object_{id}");
            }

            // Apply transform
            go.transform.position = pos;
            go.transform.rotation = Quaternion.Euler(rotEuler);

            // Reassign UniqueID
            var uidComp = go.AddComponent<UniqueID>();
            uidComp.ID = id;

            yield return null; // Let Unity breathe between spawns
        }

        Debug.Log($"Game loaded from {fileName} (slot {slot})");
    }
}






