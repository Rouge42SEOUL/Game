using System;

namespace Skill
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