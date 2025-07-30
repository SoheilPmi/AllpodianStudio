using UnityEngine;
using UnityEngine.AddressableAssets;

[DisallowMultipleComponent]
public class AddressableReference : MonoBehaviour
{
    [Tooltip("Assign the Addressable AssetReference for this prefab")]
    public AssetReference prefabRef;

    void Reset()
    {
        // Optional: auto-assign if this is a prefab with the same name
        if (prefabRef == null && name != null)
        {
            // You could look up by name here if you have a naming convention
        }
    }
}
