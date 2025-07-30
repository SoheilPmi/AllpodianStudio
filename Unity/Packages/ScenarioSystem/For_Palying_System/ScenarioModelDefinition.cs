using System;
using System.Collections.Generic;

[Serializable]
public class ScenarioModelDefinition
{
public string Id; // Unique identifier
public string Description; // Short description
public string Template; // DSL template with placeholder
public List<string> ParameterKeys; // Parameter keys in template

// Generate final DSL by replacing parameters
public string GenerateDSL(Dictionary<string, object> parameters)
{
string dsl = Template;
foreach (var key in ParameterKeys)
{
if (parameters != null && parameters.TryGetValue(key, out var value))
{
dsl = dsl.Replace("{" + key + "}", value.ToString());
}
}
return dsl;
}
}
