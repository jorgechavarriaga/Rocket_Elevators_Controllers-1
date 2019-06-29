using System;
using System.Collections.Generic;

namespace RocketElevatorCSharpCommercialClassic
{

    public class Column  
    {
            
        public int id;
        public int numberOfFloor;
        public int numberOfElevator; 
        public List<ExternalButton> externalButtonList; 
        public List<Elevator> elevatorList;
        public int startFloor;
        public int endFloor;
        
        
        public Column(int id, int numberOfFloor, int numberOfElevator, List<int> startFloorList, List<int> endFloorList)
        {
            this.id = id + 1;
            this.numberOfFloor = numberOfFloor;
            this.numberOfElevator = numberOfElevator;
            this.externalButtonList = new List<ExternalButton>();
            this.elevatorList = new List<Elevator>();
            this.startFloor = startFloorList[id];
            this.endFloor = endFloorList[id];
 

            for (int i = 0; i < this.numberOfElevator; i++) {
                Elevator elevator = new Elevator(i, 1, numberOfFloor, startFloor, endFloor);
                this.elevatorList.Add(elevator);
            }
            
            this.initExternalButtonList();
         
        }

        public void initExternalButtonList()
        {   
            ExternalButton buttonFirstFloor = new ExternalButton(1, "UP", false);
            this.externalButtonList.Add(buttonFirstFloor);	
			
   			for (int i = startFloor; i <= endFloor; i++)
			{
                if (i != endFloor)
                {
					ExternalButton buttonUp = new ExternalButton(i, "UP", false);
                    this.externalButtonList.Add(buttonUp);
                }
                if (i != 1)
                {
                    ExternalButton buttonDown = new ExternalButton(i, "DOWN", false);
                    this.externalButtonList.Add(buttonDown);
                }			
            }     
        }

        
        public Elevator findElevatorById(int id) 
        {
                       
            for (int i = 0; i < this.elevatorList.Count; i++) {
                if (this.elevatorList[i].id == id) {
                    Console.WriteLine(">>> findElevatorById RETURN columnId " + this.id + " elevatorId " + this.elevatorList[i].id  + " ( Current floor " + this.elevatorList[i].currentFloor + " )");
                    return this.elevatorList[i];
                }
            }
            Console.WriteLine(">>> findElevatorById ========= RETURN NULL");
            return null;
        }             
    
    	public Elevator findBestElevator(int requestedFloor, string direction)
        {

            // DEFAULT Variables
            bool aIdleStatusExist = false;
            int bestGAP = numberOfFloor;
            int elevatorIdWithBestGap = -1;

            
            // 1 - compare directions or verify Idle status
            for (int i = 0; i < this.elevatorList.Count; i++) {
                if (this.elevatorList[i].status == "IDLE") {
                    aIdleStatusExist = true;
                } else if ((this.elevatorList[i].direction == "UP") && (direction == "UP") && (requestedFloor >= this.elevatorList[i].currentFloor)) {
                    this.elevatorList[i].inSamedirection = true;
                } else if ((this.elevatorList[i].direction == "DOWN") && (direction == "DOWN") && (requestedFloor <= this.elevatorList[i].currentFloor)) {
                    this.elevatorList[i].inSamedirection = true;
                } else {
                    this.elevatorList[i].inSamedirection = false;
                }
            }


            
            // 2 - THE NEAREST ELEVATOR that's is coming toward me 
            if (aIdleStatusExist == false) 
            {
                for (int i = 0; i < this.elevatorList.Count; i++) 
                {
                    if (this.elevatorList[i].inSamedirection == true) {
                        int Gap = this.elevatorList[i].currentFloor - requestedFloor;
                        Gap = Math.Abs(Gap); // Exemple  { (-4) become (4)
                        if (Gap < bestGAP) {
                            elevatorIdWithBestGap = this.elevatorList[i].id;
                            Console.WriteLine(">>> elevator id with best gap = " + this.elevatorList[i].id );             
                            bestGAP = Gap;
                        }
                    }
                }
                Console.WriteLine(">>> findBestElevator RETURN INSAMEDIRECTION ( bestGAP : " + bestGAP + " )");
                return this.findElevatorById(elevatorIdWithBestGap);
            }

            
            // 3 - THE NEAREST ELEVATOR with the IDLE status 
            if (aIdleStatusExist == true) 
            {
                for (int i = 0; i < this.elevatorList.Count; i++) {

                    if (this.elevatorList[i].status == "IDLE")
                     
                    {
                        int Gap = this.elevatorList[i].currentFloor - requestedFloor;
                        Gap = Math.Abs(Gap); // Exemple  (-4) become (4)
                        if (Gap < bestGAP) {
                            elevatorIdWithBestGap = this.elevatorList[i].id;
                            bestGAP = Gap;
                        }
                    }
                }
                Console.WriteLine(">>> findBestElevator RETURN IDLE ( bestGAP : " + bestGAP + " )");
                return this.findElevatorById(elevatorIdWithBestGap);
            }

            Console.WriteLine(">>> findBestElevator ======  NULL ");
            return null; // Default
	    }
        
        public Elevator requestElevator(int requestedFloor, string direction)
        {
            Console.WriteLine(">>> requestElevator  " + requestedFloor + " " + direction);
            
            Elevator elevator = this.findBestElevator(requestedFloor, direction);

            elevator.addToQueue(requestedFloor);
            elevator.moveElevator();

            return elevator;
        }

        public void requestFloor(Elevator elevator , int requestedFloor) {

        Console.WriteLine(">>> requestFloor  " + requestedFloor + "  WITH  elevator # " + elevator.id + " ( Current floor " + elevator.currentFloor + " )");

        elevator.addToQueue(requestedFloor);
        elevator.closeDoor();
        elevator.moveElevator();

	    }

	}
}























