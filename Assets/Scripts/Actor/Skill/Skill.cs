using System;

namespace Actor.Skill
{
    [Serializable]
    public class Skill
    {
        public int id;
        public string name;
        public int atk;

        public Skill()
        {
            id = -1;
            name = "";
            atk = 0;
        }
    }
}