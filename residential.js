class Column {

	constructor(numberOfFloor, numberOfElevator) {
		this.numberOfFloor = numberOfFloor;
		this.numberOfElevator = numberOfElevator;
		this.externalButtonList = [];
		this.elevatorList = [];
		for (let i = 0; i < this.numberOfElevator; i++) {
			this.elevatorList.push(new Elevator(i + 1, 1, numberOfFloor));
		}

		this.initExternalButtonList();
	}


	initExternalButtonList() {
		for (let i = 1; i < this.numberOfFloor; i++) {
			this.externalButtonList.push(new ExternalButton(i, "UP", false));
		}
		for (let i = 2; i < this.numberOfFloor + 1; i++) {
			this.externalButtonList.push(new ExternalButton(i, "DOWN", false));
		}
	}

	findElevatorById(id) {
		for (let i = 0; i < this.elevatorList.length; i++) {
			if (this.elevatorList[i].id == id) {
				console.log("RESULT elevator #", this.elevatorList[i].id , " ( Current floor", this.elevatorList[i].currentFloor, ")");
				return this.elevatorList[i];
			}
		}
	}

	findBestElevator(requestedFloor, direction) {

		// DEFAULT Variables
		let aIdleStatusExist = false;
		let bestGAP = this.numberOfFloor;
		let elevatorIdWithBestGap = -1;


		// 1 - Take the first IDLE elevator / or / compare directions
		for (let i = 0; i < this.elevatorList.length; i++) {
			if (this.elevatorList[i].status == "IDLE") {
				aIdleStatusExist = true;
			} else if ((this.elevatorList[i].direction == "UP") && (direction == "UP") && (requestedFloor > this.elevatorList[i].currentFloor)) {
				this.elevatorList[i].inSamedirection = true
			} else if ((this.elevatorList[i].direction == "DOWN") && (direction == "DOWN") && (requestedFloor < this.elevatorList[i].currentFloor)) {
				this.elevatorList[i].inSamedirection = true
			} else {
				this.elevatorList[i].inSamedirection = false
			}
		}


		// 2 - THE NEAREST ELEVATOR with the IDLE status 
		if (aIdleStatusExist == true) {
			for (let i = 0; i < this.elevatorList.length; i++) {

				if (this.elevatorList[i].status == "IDLE") {
					let Gap = this.elevatorList[i].currentFloor - requestedFloor;
					Gap = Math.abs(Gap); // Exemple  (-4) become (4)
					if (Gap < bestGAP) {
						elevatorIdWithBestGap = this.elevatorList[i].id;
						bestGAP = Gap;
					}
				}
			}
			return this.findElevatorById(elevatorIdWithBestGap)
		}

		// 3 - THE NEAREST ELEVATOR that's is coming toward me 
		if (aIdleStatusExist == false) {
			for (let i = 0; i < this.elevatorList.length; i++) {

				if (this.elevatorList[i].inSamedirection == true) {
					let Gap = this.elevatorList[i].currentFloor - requestedFloor;
					Gap = Math.abs(Gap); // Exemple  { (-4) become (4)
					if (Gap < bestGAP) {
						elevatorIdWithBestGap = this.elevatorList[i].id;
						bestGAP = Gap;
					}
				}
			}
			return this.findElevatorById(elevatorIdWithBestGap)
		}
	}

	requestElevator(requestedFloor, direction) {

		console.log(">>> requestElevator  ", requestedFloor, direction);
		let elevator = this.findBestElevator(requestedFloor, direction);

		elevator.addToQueue(requestedFloor);
		elevator.moveElevator();
	}

	requestFloor(elevator, requestedFloor) {

		console.log(">>> requestFloor  ", requestedFloor, "  WITH  elevator #", elevator.id , " ( Current floor", elevator.currentFloor, ")");

		elevator.addToQueue(requestedFloor);
		elevator.closeDoor();
		elevator.moveElevator();
	}

}


class Elevator {

	constructor(id, currentFloor, numberOfFloor) {

		this.id = id; ////////////////////////
		this.direction = ""; /////////////////  "UP" "DOWN"
		this.numberOfFloor = numberOfFloor; //
		this.currentFloor = currentFloor; ////  Elevator's Position
		this.status = "IDLE"; ////////////////  "IDLE" "MOVING"
		this.queue = []; /////////////////////  GOES UP [1,2,3,4...] or DOWN [6,5,4,3,...]
		this.internalButtonList = []; ////////  [1,2,3,4...] 
		this.door = "CLOSED"; ////////////////  "CLOSED" or "OPENED"
		this.inSamedirection = false; ////////  true or false

		for (let i = 1; i < this.numberOfFloor + 1; i++) {
			this.internalButtonList.push(new InternalButton(i, false));
		}

	}

	addToQueue(requestedFloor) {

		console.log(">>> addToQueue  [", requestedFloor, "] to [", this.queue.toString(), "]")

		this.queue.push(requestedFloor)
		console.log(">>> addToQueue REFRESH   [", this.queue.toString(), "]")

		if (this.direction == "UP") {
			this.queue.sort((a, b) => a - b) // SORT FOR ACENDING
			console.log(">>> addToQueue SORT UP   [", this.queue.toString(), "]");
		}
		if (this.direction == "DOWN") {
			this.queue.sort((a, b) => b - a) // SORT FOR DESCENDING
			console.log(">>> addToQueue SORT DOWN [", this.queue.toString(), "]")
		}
	}


	moveElevator() {

		console.log(">>> moveElevator");

		while (this.queue.length > 0) {

			if (this.door == "OPENED") {
				this.closeDoor();
			}

			let firstElement = this.queue[0];

			if (firstElement == this.currentFloor) {
				this.queue.shift(); // Delete the first element
				this.openDoor();
				console.log("------ door ------> ", firstElement, " <------------------");
			}
			if (firstElement > this.currentFloor) {
				this.status = "MOVING";
				this.direction = "UP";
				this.moveUp();
			}
			if (firstElement < this.currentFloor) {
				this.status = "MOVING";
				this.direction = "DOWN";
				this.moveDown()
			}
		}
		if (this.queue.length > 0) {} else {
			console.log("Status  IDLE !!!");
			this.status = "IDLE";
		}
	}

	moveUp() {

		this.currentFloor = (this.currentFloor + 1) ;//INCREMENT
		console.log("Current floor + 1 = ", this.currentFloor ," [", this.queue.toString(), "]");
	}

	moveDown() {

		this.currentFloor = (this.currentFloor - 1) //DECREMENT
		console.log("Current floor - 1 = ", this.currentFloor ," [", this.queue.toString(), "]");
	}

	openDoor() {
			this.door = "OPENED"
	}
	
	closeDoor() {
			this.door="CLOSED"
	}
	
	

}



class ExternalButton {

	constructor(requestFloor, direction, activated) {
		this.requestFloor = requestFloor;
		this.direction = direction; // "UP" / "DOWN"
		this.activated = false; //---- "ON" / "OFF"

	}

}
class InternalButton {

	constructor(floor, buttonActivated) {
		this.floor = floor;
		this.buttonActivated = false;
	}

}



console.log("--------------------------------------- TEST LIVRABLES -------------------------------------------------------------")


function Method1_requestElevator() {
	
	column1 = new Column(10, 2);

	column1.elevatorList[0].currentFloor = 2
	column1.elevatorList[0].direction = "UP"
	column1.elevatorList[0].status = "MOVING"
	column1.elevatorList[0].queue = [4, 6, 7]

	column1.elevatorList[1].currentFloor = 6
	column1.elevatorList[1].direction = "DOWN"
	column1.elevatorList[1].status = "MOVING"
	column1.elevatorList[1].queue = [4, 3]

	column1.requestElevator(1, "DOWN");
}

Method1_requestElevator()


function  Method2_requestFloor(){
	column1 = new Column(10, 2)

	column1.elevatorList[0].currentFloor = 2
	column1.elevatorList[0].direction  =  "UP" 	  		   
	column1.elevatorList[0].status =  "MOVING" 
	column1.elevatorList[0].queue = [3,4]

	elevator = column1.elevatorList[0]

	column1.requestFloor(elevator, 9)
}

//Method2_requestFloor()

