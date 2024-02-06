
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BornToMove.DAL
{
    public class Move
	{
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int SweatRate { get; set; }


        public Move(int id, string name, string description, int sweatRate)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.SweatRate = sweatRate;
        }
    }
}
