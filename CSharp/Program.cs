using System;
using System.Collections.Generic;

namespace RocketElevatorCSharpCommercialClassic
{
	
	public class Program  
	{
		static void Main()
		{
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//////////////////////////////////////   HERE TO SIMULATE  //////////////////////////////////////////////////
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////
			int numberOfColumn = 4;
			int numberOfElevator= 5; // 4 x 5 =  20 elevators
			int numberOfFloor = 85; 
			List<int> startFloorList = new List<int>{2,23,44,65};
			List<int> endFloorList  = new List<int>{22,43,64,85};
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

			Console.WriteLine(" SENARIO 1");

			battery.columnList[1].elevatorList[0].currentFloor = 1;
			battery.columnList[1].elevatorList[0].direction  =  "UP"; 	  		   
			battery.columnList[1].elevatorList[0].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[1].elevatorList[0].queue.Add(24);
			
			battery.columnList[1].elevatorList[1].currentFloor = 23;
			battery.columnList[1].elevatorList[1].direction  =  "UP"; 	  		   
			battery.columnList[1].elevatorList[1].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[1].elevatorList[1].queue.Add(28);
			
			battery.columnList[1].elevatorList[2].currentFloor = 33;
			battery.columnList[1].elevatorList[2].direction  =  "DOWN"; 	  		   
			battery.columnList[1].elevatorList[2].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[1].elevatorList[2].queue.Add(1);

			battery.columnList[1].elevatorList[3].currentFloor = 40;
			battery.columnList[1].elevatorList[3].direction  =  "DOWN"; 	  		   
			battery.columnList[1].elevatorList[3].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[1].elevatorList[3].queue.Add(24);
			
			battery.columnList[1].elevatorList[4].currentFloor = 42;
			battery.columnList[1].elevatorList[4].direction  =  "DOWN"; 	  		   
			battery.columnList[1].elevatorList[4].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[1].elevatorList[4].queue.Add(1);	

			Elevator elevator = battery.columnList[1].requestElevator(1, "UP");
			Console.WriteLine("USED ELEVATOR : " + elevator.id);
			battery.columnList[1].requestFloor(elevator, 36);		


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

			Console.WriteLine(" SENARIO 2");

			battery.columnList[2].elevatorList[0].currentFloor = 58;
			battery.columnList[2].elevatorList[0].direction  =  "DOWN"; 	  		   
			battery.columnList[2].elevatorList[0].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[2].elevatorList[0].queue.Add(1);
			
			battery.columnList[2].elevatorList[1].currentFloor = 50;
			battery.columnList[2].elevatorList[1].direction  =  "UP"; 	  		   
			battery.columnList[2].elevatorList[1].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[2].elevatorList[1].queue.Add(63);
			
			battery.columnList[2].elevatorList[2].currentFloor = 46;
			battery.columnList[2].elevatorList[2].direction  =  "UP"; 	  		   
			battery.columnList[2].elevatorList[2].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[2].elevatorList[2].queue.Add(60);

			battery.columnList[2].elevatorList[3].currentFloor = 1;
			battery.columnList[2].elevatorList[3].direction  =  "UP"; 	  		   
			battery.columnList[2].elevatorList[3].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[2].elevatorList[3].queue.Add(54);
			
			battery.columnList[2].elevatorList[4].currentFloor = 64;
			battery.columnList[2].elevatorList[4].direction  =  "DOWN"; 	  		   
			battery.columnList[2].elevatorList[4].status =  "MOVING"; // MOVING or IDLE
			battery.columnList[2].elevatorList[4].queue.Add(1);			

			Elevator elevatorX = battery.columnList[2].requestElevator(54, "DOWN");
			Console.WriteLine("USED ELEVATOR : " + elevatorX.id);
			battery.columnList[2].requestFloor(elevator, 1);

		}
	}
}	







			/////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//////////////////////////////////////   TEST CLASSES   /////////////////////////////////////////////////////
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//Console.WriteLine("startFloorList : " + startFloorList[2]);
			//Console.WriteLine("endFloorList : " + endFloorList[2]);			
			//Console.WriteLine("battery.columnList[i].columnId : " + battery.columnList[1].columnId);
			//Console.WriteLine("battery.columnList[0].startFloor : " + battery.columnList[0].startFloor);
			//Console.WriteLine("battery.columnList[1].startFloor : " + battery.columnList[1].startFloor);
			//Console.WriteLine("battery.columnList[2].startFloor : " + battery.columnList[2].startFloor);
			//Console.WriteLine("battery.columnList[3].startFloor : " + battery.columnList[3].startFloor);
			//Console.WriteLine("battery.columnList[1].id " + battery.columnList[1].id);
			//Console.WriteLine("battery.columnList[2].elevatorList[5].id " + battery.columnList[1].elevatorList[4].id);
			
			// Console.WriteLine("( --- INTERNAL BUTTON --- )");
			// for (int col = 0 ; col < battery.columnList.Count; col++)
			// {
			// 	for (int i = 0; i < battery.columnList[col].elevatorList[0].internalButtonList.Count; i++)
			// 	{
			// 	Console.WriteLine("battery.columnList[ " + col + " ].elevatorList[4].internalButtonList[ " + i + " ].floor : " + battery.columnList[col].elevatorList[4].internalButtonList[i].floor);
			// 	}
			// }

			
			// Console.WriteLine("( --- EXTERNAL BUTTON --- )");
			// for (int col = 0 ; col < battery.columnList.Count; col++)
			// {
			// 	for (int i = 0; i < battery.columnList[col].externalButtonList.Count; i++)
			// 	{
			// 	Console.WriteLine( "Column.id : " + battery.columnList[col].id + "   Button : " + battery.columnList[col].externalButtonList[i].floor + "  direction   " + battery.columnList[col].externalButtonList[i].direction);
			// 	}	
			// }


				

















