
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BornToMove
{
    public class Move
	{
        private int id;
        private string name;
        private string description;
        private int sweatRate;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int SweatRate
        {
            get { return sweatRate; }
            set { sweatRate = value; }
        }
    }
}
