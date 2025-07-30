using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour
{
[Header("Model Selection")]
public List<string> selectedModelIds; // List of selected IDs
public bool useRandom = false; // Enable/disable random mode
public int randomCount = 3; // Number of random selections
public List<string> constraintTags; // Filter by tag or category

// Full DSL output
public string GeneratedDSL { get; private set; }

// Generate DSL and send to ScenarioRunner
public void GenerateAndRun()
{
var pool = ScenarioModelLibrary.Models;

// Filter by tag (if needed)
if (constraintTags != null && constraintTags.Count > 0)
{
pool = pool
.Where(m => constraintTags.Any(tag => m.Description.Contains(tag)))
.ToList();
}

// Final models
List<ScenarioModelDefinition> chosen;
if (useRandom)
{
chosen = pool.OrderBy(_ => Guid.NewGuid())
.Take(Mathf.Min(randomCount, pool.Count))
.ToList();
}
else
{
chosen = pool.Where(m => selectedModelIds.Contains(m.Id)).ToList();
}

// Build the DSL
var dslLines = new List<string>();
foreach (var model in chosen)
{
// Set parameters (you can add UI/Inspector for editing)
var parameters = new Dictionary<string, object>();
foreach (var key in model.ParameterKeys)
{
// For example, we set the default value to "0" or key
parameters[key] = PlayerPrefs.GetString(model.Id + "_" + key, key);
}

dslLines.Add("// Model: " + model.Id + " — " + model.Description);
dslLines.Add(model.GenerateDSL(parameters));
dslLines.Add(string.Empty);
}

GeneratedDSL = string.Join("\n", dslLines);
Debug.Log("Generated Scenario DSL:\n" + GeneratedDSL);

// Send to ScenarioRunner (assuming component exists)
var runner = GetComponent<ScenarioRunner>();
if (runner != null) 
{ 
runner.LoadFromDSL(GeneratedDSL); 
runner.StartScenario(); 
} 
}
}
