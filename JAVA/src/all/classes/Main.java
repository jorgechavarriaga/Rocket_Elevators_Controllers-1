package all.classes;

import java.util.ArrayList;


public class Main {

	public static void main(String[] args) 
	{
		controler();
	}			
		public static void controler() 
		{
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////   Controler Simulation  //////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		int numberOfColumn = 4;
		int numberOfElevator = 5; // 4 x 5 =  20 elevators
		int numberOfFloor = 85;
		ArrayList<Integer> startFloorList = new ArrayList<Integer>();
		startFloorList.add(2);
		startFloorList.add(23);
		startFloorList.add(44);
		startFloorList.add(65);
		ArrayList<Integer> endFloorList = new ArrayList<Integer>();
		endFloorList.add(22);
		endFloorList.add(43);
		endFloorList.add(64);
		endFloorList.add(85);

		Battery battery = new Battery(numberOfColumn, numberOfElevator, numberOfFloor, startFloorList, endFloorList);
		

		
		///////////////////////////////////////////////
		///            SENARIO #1                   ///
		///////////////////////////////////////////////
		
		//With column ID 2 (floors from 23 to 43), 
		//elevatorID 1 at [ 1st ]  floor going to [ 24th ],---> UP
		//elevatorID 2 at [ 23st ] floor going to [ 28th ],---> UP
		//elevatorID 3 at [ 33rd ] floor going to [ 1st  ],---> DOWN
		//elevatorID 4 at [ 40th ] floor going to [ 24th ],---> DOWN
		//elevatorID 5 at [ 42nd ] floor going to [ 1st  ],---> DOWN		
		//SOMEONE is at [1st] floor and requests [ UP ] the [36th] floor 
		//RESULT SHOULD BE : elevator #1
		
		System.out.println(" SENARIO 1");
		
		battery.columnList.get(1).elevatorList.get(0).currentFloor = 1;
		battery.columnList.get(1).elevatorList.get(0).direction  =  "UP"; 	  		   
		battery.columnList.get(1).elevatorList.get(0).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(1).elevatorList.get(0).queue.add(24);
		
		battery.columnList.get(1).elevatorList.get(1).currentFloor = 23;
		battery.columnList.get(1).elevatorList.get(1).direction  =  "UP"; 	  		   
		battery.columnList.get(1).elevatorList.get(1).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(1).elevatorList.get(1).queue.add(28);
		
		battery.columnList.get(1).elevatorList.get(2).currentFloor = 33;
		battery.columnList.get(1).elevatorList.get(2).direction  =  "DOWN"; 	  		   
		battery.columnList.get(1).elevatorList.get(2).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(1).elevatorList.get(2).queue.add(1);
		
		battery.columnList.get(1).elevatorList.get(3).currentFloor = 40;
		battery.columnList.get(1).elevatorList.get(3).direction  =  "DOWN"; 	  		   
		battery.columnList.get(1).elevatorList.get(3).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(1).elevatorList.get(3).queue.add(24);
		
		battery.columnList.get(1).elevatorList.get(4).currentFloor = 42;
		battery.columnList.get(1).elevatorList.get(4).direction  =  "DOWN"; 	  		   
		battery.columnList.get(1).elevatorList.get(4).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(1).elevatorList.get(4).queue.add(1);	
		
		Elevator elevator = battery.columnList.get(1).requestElevator(1, "UP");
		System.out.println("USED ELEVATOR : " + elevator.id);
//		battery.columnList.get(1).requestFloor(elevator, 36);		
		
		
		///////////////////////////////////////////////
		///            SENARIO #2                   ///
		///////////////////////////////////////////////
		
		
		//With column ID 3 (floors from 44 to 64),
		//elevatorID 1 at [ 58th ] floor going to [ 1st  ],---> DOWN
		//elevatorID 2 at [ 50th ] floor going to [ 63rd ],---> UP
		//elevatorID 3 at [ 46th ] floor going to [ 60th ],---> UP
		//elevatorID 4 at [ 1st  ] floor going to [ 54th ],---> UP
		//elevatorID 5 at [ 64th ] floor going to [ 1st  ],---> DOWN
		//SOMEONE is at [ 54th ] floor and requests[ DOWN ] [ 1st ] floor, 
		//RESULT SHOULD BE : elevator #1			
		
		System.out.println(" SENARIO 2");
		
		battery.columnList.get(2).elevatorList.get(0).currentFloor = 58;
		battery.columnList.get(2).elevatorList.get(0).direction  =  "DOWN"; 	  		   
		battery.columnList.get(2).elevatorList.get(0).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(2).elevatorList.get(0).queue.add(1);
		
		battery.columnList.get(2).elevatorList.get(1).currentFloor = 50;
		battery.columnList.get(2).elevatorList.get(1).direction  =  "UP"; 	  		   
		battery.columnList.get(2).elevatorList.get(1).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(2).elevatorList.get(1).queue.add(63);
		
		battery.columnList.get(2).elevatorList.get(2).currentFloor = 46;
		battery.columnList.get(2).elevatorList.get(2).direction  =  "UP"; 	  		   
		battery.columnList.get(2).elevatorList.get(2).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(2).elevatorList.get(2).queue.add(60);
		
		battery.columnList.get(2).elevatorList.get(3).currentFloor = 1;
		battery.columnList.get(2).elevatorList.get(3).direction  =  "UP"; 	  		   
		battery.columnList.get(2).elevatorList.get(3).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(2).elevatorList.get(3).queue.add(54);
		
		battery.columnList.get(2).elevatorList.get(4).currentFloor = 64;
		battery.columnList.get(2).elevatorList.get(4).direction  =  "DOWN"; 	  		   
		battery.columnList.get(2).elevatorList.get(4).status =  "MOVING"; // MOVING or IDLE
		battery.columnList.get(2).elevatorList.get(4).queue.add(1);			
		
		Elevator elevatorX = battery.columnList.get(2).requestElevator(54, "DOWN");
		System.out.println("USED ELEVATOR : " + elevatorX.id);
//		battery.columnList.get(2).requestFloor(elevator, 1);


		}
}
