using UnityEngine;
using System.Collections.Generic;


/* Example to Use:

modelConfigs:
  - modelId: "SpawnWave"
    parameters:
      - key: "interval", value: "5"
      - key: "scoreThreshold", value: "100"
      - key: "enemyTag", value: "Enemy"
      - key: "count", value: "3"

  - modelId: "TimedSpawn"
    parameters:
      - key: "delay", value: "2"
      - key: "enemyTag", value: "Goblin"
      - key: "count", value: "4"

  - modelId: "BossPhase"
    parameters:
      - key: "bossId", value: "Boss1"
      - key: "threshold", value: "50"
      - key: "newPattern", value: "Aggressive"
*/


[System.Serializable]
public class ScenarioParameter
{
    public string key;     // e.g. "interval", "enemyTag"
    public string value;   // e.g. "5", "Goblin"
}

[System.Serializable]
public class ScenarioModelConfig
{
    public string modelId;                      // e.g. "SpawnWave"
    public List<ScenarioParameter> parameters;  // List of key-value pairs
}

public class ScenarioDefaults : MonoBehaviour
{
    [Header("Scenario Model Configurations")]
    public List<ScenarioModelConfig> modelConfigs = new List<ScenarioModelConfig>();

    void Awake()
    {
        foreach (var model in modelConfigs)
        {
            foreach (var param in model.parameters)
            {
                string fullKey = $"{model.modelId}_{param.key}";
                PlayerPrefs.SetString(fullKey, param.value);
                Debug.Log($"Set PlayerPref: {fullKey} = {param.value}");
            }
        }
    }
}
