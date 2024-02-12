
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

        //Onderstaande property creeert een one-to-many relatie tussen Move en MoveRating
        virtual public ICollection<MoveRating> Ratings { get; set; }


        public Move(string name, string description, int sweatRate)
        {
            //this.Id = id;
            this.Name = name;
            this.Description = description;
            this.SweatRate = sweatRate;
        }
    }
}
