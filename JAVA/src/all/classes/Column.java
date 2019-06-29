package all.classes;

import java.util.ArrayList;
import java.util.List;

public class Column 
{
	
    public int id;
    public int numberOfFloor;
    public int numberOfElevator; 
    public ArrayList<ExternalButton> externalButtonList;  
    public ArrayList<Elevator> elevatorList;
    public int startFloor;
    public int endFloor;
    
    
    public Column(int id, int numberOfFloor, int numberOfElevator, ArrayList<Integer> startFloorList, ArrayList<Integer> endFloorList)
    {
        this.id = id + 1;
        this.numberOfFloor = numberOfFloor;
        this.numberOfElevator = numberOfElevator;
        this.externalButtonList = new ArrayList<ExternalButton>();
        this.elevatorList = new ArrayList<Elevator>();
        this.startFloor = startFloorList.get(id);
        this.endFloor = endFloorList.get(id);


        for (int i = 0; i < this.numberOfElevator; i++) {
          Elevator elevator = new Elevator(i, 1, numberOfFloor, startFloor, endFloor);
           this.elevatorList.add(elevator);
        }
        
        this.initExternalButtonList();
     
    }
    public void initExternalButtonList()
    {   
        ExternalButton buttonFirstFloor = new ExternalButton(1, "UP", false);
        this.externalButtonList.add(buttonFirstFloor);	
		
			for (int i = startFloor; i <= endFloor; i++)
		{
            if (i != endFloor)
            {
				ExternalButton buttonUp = new ExternalButton(i, "UP", false);
                this.externalButtonList.add(buttonUp);
            }
            if (i != 1)
            {
                ExternalButton buttonDown = new ExternalButton(i, "DOWN", false);
                this.externalButtonList.add(buttonDown);
            }			
        }     
    }

    
    public Elevator findElevatorById(int id) 
    {
                   
        for (int i = 0; i < this.elevatorList.size(); i++) {
            if (this.elevatorList.get(i).id == id) {
                System.out.println(">>> findElevatorById RETURN columnId " + this.id + " elevatorId " + this.elevatorList.get(i).id  + " ( Current floor " + this.elevatorList.get(i).currentFloor + " )");
                return this.elevatorList.get(i);
            }
        }
        System.out.println(">>> findElevatorById ========= RETURN NULL");
        return null;
    } 


	public Elevator findBestElevator(int requestedFloor, String direction)
    {

        // DEFAULT Variables
        boolean aIdleStatusExist = false;
        int bestGAP = numberOfFloor;
        int elevatorIdWithBestGap = -1;

        
        // 1 - compare directions or verify Idle status
        for (int i = 0; i < this.elevatorList.size(); i++) {
            if (this.elevatorList.get(i).status == "IDLE") {
                aIdleStatusExist = true;
            } else if ((this.elevatorList.get(i).direction == "UP") && (direction == "UP") && (requestedFloor >= this.elevatorList.get(i).currentFloor)) {
                this.elevatorList.get(i).inSamedirection = true;
            } else if ((this.elevatorList.get(i).direction == "DOWN") && (direction == "DOWN") && (requestedFloor <= this.elevatorList.get(i).currentFloor)) {
                this.elevatorList.get(i).inSamedirection = true;
            } else {
                this.elevatorList.get(i).inSamedirection = false;
            }
        }


        
        // 2 - THE NEAREST ELEVATOR that's is coming toward me 
        if (aIdleStatusExist == false) 
        {
            for (int i = 0; i < this.elevatorList.size(); i++) 
            {
                if (this.elevatorList.get(i).inSamedirection == true) {
                    int Gap = this.elevatorList.get(i).currentFloor - requestedFloor;
                    Gap = Math.abs(Gap); // Example  { (-4) become (4)
                    if (Gap < bestGAP) {
                        elevatorIdWithBestGap = this.elevatorList.get(i).id;
                        System.out.println(">>> elevator id with best gap = " + this.elevatorList.get(i).id );             
                        bestGAP = Gap;
                    }
                }
            }
            System.out.println(">>> findBestElevator RETURN INSAMEDIRECTION ( bestGAP : " + bestGAP + " )");
            return this.findElevatorById(elevatorIdWithBestGap);
        }

        
        // 3 - THE NEAREST ELEVATOR with the IDLE status 
        if (aIdleStatusExist == true) 
        {
            for (int i = 0; i < this.elevatorList.size(); i++) {

                if (this.elevatorList.get(i).status == "IDLE")
                 
                {
                    int Gap = this.elevatorList.get(i).currentFloor - requestedFloor;
                    Gap = Math.abs(Gap); // Example  (-4) become (4)
                    if (Gap < bestGAP) {
                        elevatorIdWithBestGap = this.elevatorList.get(i).id;
                        bestGAP = Gap;
                    }
                }
            }
            System.out.println(">>> findBestElevator RETURN IDLE ( bestGAP : " + bestGAP + " )");
            return this.findElevatorById(elevatorIdWithBestGap);
        }

        System.out.println(">>> findBestElevator ======  NULL ");
        return null; // Default
    }
    
    public Elevator requestElevator(int requestedFloor, String direction)
    {
        System.out.println(">>> requestElevator  " + requestedFloor + " " + direction);
        
        Elevator elevator = this.findBestElevator(requestedFloor, direction);

        elevator.addToQueue(requestedFloor);
        elevator.moveElevator();

        return elevator;
    }

    public void requestFloor(Elevator elevator , int requestedFloor) {

	    System.out.println(">>> requestFloor  " + requestedFloor + "  WITH  elevator # " + elevator.id + " ( Current floor " + elevator.currentFloor + " )");
	
	    elevator.addToQueue(requestedFloor);
	    elevator.closeDoor();
	    elevator.moveElevator();

    }

}


























