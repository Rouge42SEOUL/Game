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
        public Dictionary<int, EventType> Events = new();

        public void SaveInfo()
        {
            Events.Clear();
            for (int i = 0; i < StageManager.Instance.Nodes.Length; i++)
            {
                Events.Add(i, StageManager.Instance.Nodes[i].eventType);
                if (StageManager.Instance.Nodes[i] == MapDataManager.Instance.CurrentNode)
                {
                    DataManager.DataManager.Instance.CurrentNode = i;
                }
            }
        }
    }
}