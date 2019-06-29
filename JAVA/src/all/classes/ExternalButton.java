package all.classes;

public class ExternalButton {

		public int floor;
	    public String direction;
	    public boolean activated;


		public ExternalButton(int floor, String direction, boolean activated)
		{
			this.floor = floor;
			this.direction = direction; // "UP" / "DOWN"
			this.activated = false; //---- true / false		
		}

}
