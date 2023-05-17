using System;
using Actor.Stats;
using Interface;

namespace Actor.Skill
{
    [Serializable]
    public class Skill
    {
        public int id;
        public string name;
        public string description;
        public Skill()
        {
            id = -1;
            name = "";
            description = "";
        }
    }
}