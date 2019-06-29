using System;
using System.Collections.Generic;

namespace RocketElevatorCSharpCommercialClassic
{

	public class Elevator
	{

			public int id;
			public string direction;
			public int numberOfFloor;
			public int currentFloor;
			public string status;
			public List<int> queue;
			public List<InternalButton> internalButtonList; 
			public string door = "CLOSED";
			public bool inSamedirection = false;
			public int startFloor;
            public int endFloor;


		public Elevator(int id, int currentFloor, int numberOfFloor, int startFloor, int endFloor)
		{
			this.id = id + 1; ////////////////////
			this.direction = ""; /////////////////  "UP" "DOWN"
			this.numberOfFloor = numberOfFloor; //
			this.currentFloor = currentFloor; ////  Elevator's Position
			this.status = "IDLE"; ////////////////  "IDLE" "MOVING"
			this.queue = new List<int>();
			this.internalButtonList = new List<InternalButton>();
			this.door = "CLOSED"; ////////////////  "CLOSED" or "OPENED"
			this.inSamedirection = false; ////////  true or false
			this.startFloor = startFloor;
            this.endFloor = endFloor;

		
			InternalButton buttonFirstFloor = new InternalButton(1, false);
            this.internalButtonList.Add(buttonFirstFloor);	
			
   			for (int i = startFloor; i <= endFloor; i++)
			{
					InternalButton button = new InternalButton(i, false);
                    this.internalButtonList.Add(button);			
            }

		}
		public void addToQueue(int requestedFloor) 
		{

		Console.WriteLine(">>> addToQueue  [ " + requestedFloor + " ] to [ " + (String.Join(",", this.queue)) + " ]");

		this.queue.Add(requestedFloor);
			Console.WriteLine(">>> addToQueue REFRESH   [ " + (String.Join(",", this.queue)) + " ]");

		Console.WriteLine("direction :   [ " + this.direction + " ]"); /// temporaire line
		if (this.direction == "UP") 
		{
			this.queue.Sort((a, b) => a - b); // SORT FOR ACENDING
			Console.WriteLine(">>> addToQueue SORT UP   [ " + (String.Join(",", this.queue)) + " ]");
		}
		if (this.direction == "DOWN") 
		{
			this.queue.Sort((a, b) => b - a); // SORT FOR DESCENDING
			Console.WriteLine(">>> addToQueue SORT DOWN [ " + (String.Join(",", this.queue)) + " ]");
		}
		}

		public void moveElevator() 
		{

		Console.WriteLine(">>> moveElevator");

			while (this.queue.Count > 0)
			{

				if (this.door == "OPENED") 
				{
					this.closeDoor();
				}

				int firstElement = this.queue[0];

				if (firstElement == this.currentFloor) 
				{
					this.queue.RemoveAt(0); // Delete the first element
					this.openDoor();
					Console.WriteLine("------ door ------>  " + firstElement + "  <------------------");
				}
				if (firstElement > this.currentFloor) 
				{
					this.status = "MOVING";
					this.direction = "UP";
					this.moveUp();
				}
				if (firstElement < this.currentFloor) 
				{
					this.status = "MOVING";
					this.direction = "DOWN";
					this.moveDown();
				}
			}
			if (this.queue.Count > 0) {} else {
				Console.WriteLine("Status  IDLE !!!");
				this.status = "IDLE";
			}
		}
		
		public void moveUp() 
		{
			this.currentFloor = (this.currentFloor + 1);//INCREMENT
			Console.WriteLine("Current floor + 1 =  " + this.currentFloor + " [ " + (String.Join(",", this.queue)) + " ]");
		}

		public void moveDown() 
		{
			this.currentFloor = (this.currentFloor - 1); //DECREMENT
			Console.WriteLine("Current floor - 1 = " + this.currentFloor  + " [ " + (String.Join(",", this.queue)) + " ]");
		}

		public void openDoor() 
		{
				this.door = "OPENED";
		}

		public void closeDoor() 
		{
				this.door="CLOSED";
		}
		
	}
}