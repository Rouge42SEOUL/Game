using System.Collections.Generic;
using Newtonsoft.Json;

namespace Managers.SaveData
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapData
    {
        [JsonProperty]
        public int MapIndex;
        [JsonProperty] 
        public Dictionary<int, EventType> Events;

        public void SaveInfo(Node[] nodes, Node currentNode)
        {
            Events = new Dictionary<int, EventType>();
            for (int i = 0; i < nodes.Length; i++)
            {
                Events.Add(i, nodes[i].eventType);
                if (nodes[i] == currentNode)
                {
                    DataManager.DataManager.Instance.CurrentNode = i;
                }
            }
        }
    }
}