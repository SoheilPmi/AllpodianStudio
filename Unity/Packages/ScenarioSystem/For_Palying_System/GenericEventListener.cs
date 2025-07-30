using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class GenericEventListener : MonoBehaviour
{
[Tooltip("Component or script that has an Event")]
public Component targetComponent;

[Tooltip("Generic Event Name")]
public string eventName;

[Tooltip("Callback if Event is triggered")]
public UnityEvent onEventTriggered;

private EventInfo eventInfo;
private Delegate handler;

void Start()
{
if (targetComponent == null || string.IsNullOrEmpty(eventName))
return;

eventInfo = targetComponent.GetType().GetEvent(eventName,
BindingFlags.Public | BindingFlags.Instance);
if (eventInfo == null)
{
Debug.LogWarning($"Event '{eventName}' not found on {targetComponent.GetType().Name}");
return;
} 

// Assume Delegate with signature (object, EventArgs) 
var method = GetType().GetMethod(nameof(HandleEvent), 
BindingFlags.NonPublic | BindingFlags.Instance); 
handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, method); 
eventInfo.AddEventHandler(targetComponent, handler); 
} 

void OnDestroy() 
{ 
if (eventInfo != null && handler != null) 
eventInfo.RemoveEventHandler(targetComponent, handler); 
} 

// The method that connects to the Event 
void HandleEvent(object sender, EventArgs e) 
{ 
onEventTriggered?.Invoke(); 
}
}
