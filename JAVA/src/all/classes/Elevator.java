package all.classes;

import java.util.ArrayList;
import java.util.Collections;// to sort reverse or in order

public class Elevator
{
		public int id;
		public String direction;
		public int numberOfFloor;
		public int currentFloor;
		public String status;
		public ArrayList<Integer> queue;
		public ArrayList<InternalButton> internalButtonList; 
		public String door = "CLOSED";
		public boolean inSamedirection = false;
		public int startFloor;
        public int endFloor;


	public Elevator(int id, int currentFloor, int numberOfFloor, int startFloor, int endFloor)
	{
		this.id = id + 1; ////////////////////
		this.direction = ""; /////////////////  "UP" "DOWN"
		this.numberOfFloor = numberOfFloor; //
		this.currentFloor = currentFloor; ////  Elevator's Position
		this.status = "IDLE"; ////////////////  "IDLE" "MOVING"
		this.queue = new ArrayList<Integer>();
		this.internalButtonList = new ArrayList<InternalButton>();
		this.door = "CLOSED"; ////////////////  "CLOSED" or "OPENED"
		this.inSamedirection = false; ////////  true or false
		this.startFloor = startFloor;
        this.endFloor = endFloor;

	
		InternalButton buttonFirstFloor = new InternalButton(1, false);
        this.internalButtonList.add(buttonFirstFloor);	
		
		for (int i = startFloor; i <= endFloor; i++)
		{
				InternalButton button = new InternalButton(i, false);
                this.internalButtonList.add(button);			
        }

	}
	public void addToQueue(int requestedFloor) 
	{

		System.out.println(">>> addToQueue  [ " + requestedFloor + " ] to [ " + this.queue + " ]");
	
			this.queue.add(requestedFloor);
				System.out.println(">>> addToQueue REFRESH   [ " + this.queue + " ]");
		
			System.out.println("direction :   [ " + this.direction + " ]");
			if (this.direction == "UP") 
			{			
				Collections.sort(this.queue);// SORT FOR ACENDING
				System.out.println(">>> addToQueue SORT UP   [ " + this.queue + " ]");
			}
			if (this.direction == "DOWN") 
			{
				Collections.reverse(this.queue); // SORT FOR DESCENDING
				System.out.println(">>> addToQueue SORT DOWN [ " + this.queue + " ]");
			}
	}

	public void moveElevator() 
	{

	System.out.println(">>> moveElevator");

		while (this.queue.size() > 0)
		{

			if (this.door == "OPENED") 
			{
				this.closeDoor();
			}

			int firstElement = this.queue.get(0);

			if (firstElement == this.currentFloor) 
			{
				this.queue.remove(0); // Delete the first element
				this.openDoor();
				System.out.println("------ door ------>  " + firstElement + "  <------------------");
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
		if (this.queue.size() > 0) {} else {
			System.out.println("Status  IDLE !!!");
			this.status = "IDLE";
		}
	}
	
	public void moveUp() 
	{
		this.currentFloor = (this.currentFloor + 1);//INCREMENT
		System.out.println("Current floor + 1 =  " + this.currentFloor + " [ " +  this.queue + " ]");
	}

	public void moveDown() 
	{
		this.currentFloor = (this.currentFloor - 1); //DECREMENT
		System.out.println("Current floor - 1 = " + this.currentFloor  + " [ " +  this.queue + " ]");
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
