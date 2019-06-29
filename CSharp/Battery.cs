using System;
using System.Collections.Generic;

namespace RocketElevatorCSharpCommercialClassic
{

	public class Battery 
	{

		public int numberOfColumn;
        public int numberOfElevator;
        public int numberOfFloor;
		public List<Column> columnList;
		public List<int> startFloorList;
		public List<int> endFloorList;

		
		public Battery(int numberOfColumn, int numberOfElevator, int numberOfFloor,List<int> startFloorList, List<int> endFloorList)
		{
			this.numberOfColumn = numberOfColumn;
            this.numberOfElevator = numberOfElevator;
            this.numberOfFloor = numberOfFloor;
			this.columnList = new List<Column>();
			this.startFloorList = startFloorList;
			this.endFloorList = endFloorList;
   			
			   for (int i = 0; i < numberOfColumn; i++)
			    {
					Column column = new Column(i, numberOfFloor, numberOfElevator, startFloorList, endFloorList);
                    this.columnList.Add(column);			
                }

		}
	}
}


