using System;
using System.Collections.Generic;

namespace RocketElevatorCSharpCommercialClassic
{

	public class ExternalButton
	{
		public int floor;
        public string direction;
        public bool activated;

		public ExternalButton(int floor, string direction, bool activated)
		{
			this.floor = floor;
			this.direction = direction; // "UP" / "DOWN"
			this.activated = false; //---- true / false		
		}
	}

	public class InternalButton
	{
		public int floor;
        public bool activated;

		public InternalButton(int floor, bool activated)
		{
			this.floor = floor;
			this.activated = false; //---- true / false			
		}
	}
}



