using UnityEngine;

[CreateAssetMenu(fileName = "Event List", menuName = "Scriptable Object/Event List")]
public class EventList : ScriptableObject
{
    public EventStruct[] eventStructs;
}
