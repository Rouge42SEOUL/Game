using System.Collections.Generic;
using Newtonsoft.Json;

namespace Managers.DataManager
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DataContainer
    {
        [JsonProperty]
        public int Map;
        [JsonProperty] 
        public Dictionary<int, EventType> Events;
        [JsonProperty]
        public int PlayerCurrentNode;
        [JsonProperty]
        public int Gold;

        public void SaveInfo(Node[] nodes, Node currentNode)
        {
            Events = new Dictionary<int, EventType>();
            for (int i = 0; i < nodes.Length; i++)
            {
                Events.Add(i, nodes[i].eventType);
                if (nodes[i] == currentNode)
                {
                    PlayerCurrentNode = i;
                }
            }
        }
    }
}