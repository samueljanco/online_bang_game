using System;
namespace BangOnline
{
    public struct Role
    {
        public string Name { get; set; }
        public string Mission { get; set; }
       

        public Role(string name, string mission)
        {
            Name = name;
            Mission = mission;
        }

    }
}
