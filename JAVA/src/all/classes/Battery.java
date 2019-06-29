package all.classes;

import java.util.ArrayList;

public class Battery 
{

	public int numberOfColumn;
    public int numberOfElevator;
    public int numberOfFloor;
    public ArrayList<Column> columnList;
    public ArrayList<Integer> startFloorList;
    public ArrayList<Integer> endFloorList;



	public Battery(int numberOfColumn, int numberOfElevator, int numberOfFloor, ArrayList<Integer> startFloorList, ArrayList<Integer> endFloorList)
	{
		this.numberOfColumn = numberOfColumn;
        this.numberOfElevator = numberOfElevator;
        this.numberOfFloor = numberOfFloor;
        this.columnList = new ArrayList<Column>();
		this.startFloorList = startFloorList;
		this.endFloorList = endFloorList;
			
		   for (int i = 0; i < numberOfColumn; i++)
		    {
				Column column = new Column(i, numberOfFloor, numberOfElevator, startFloorList, endFloorList);
                this.columnList.add(column);
            }

	}
}