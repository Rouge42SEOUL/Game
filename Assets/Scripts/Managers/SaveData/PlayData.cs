using Newtonsoft.Json;

namespace Managers.SaveData
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PlayData
    {
        [JsonProperty]
        public int Gold;
        [JsonProperty]
        public int CurrentNodeIdx;
        [JsonProperty]
        public bool IsEventRunning;
    }
}