using UnityEngine;

/// <summary>
/// Assigns a unique integer ID to each GameObject for tracking during save/load.
/// Automatically generates an ID if none is set.
/// </summary>
[DisallowMultipleComponent]
public class UniqueID : MonoBehaviour
{
    [Tooltip("Unique identifier for this GameObject.")]
    public int ID;

    private static int _globalCounter = 1;

    void Awake()
    {
        // If no ID is assigned, generate a new one
        if (ID == 0)
        {
            ID = _globalCounter;
            _globalCounter++;
        }
    }
}
