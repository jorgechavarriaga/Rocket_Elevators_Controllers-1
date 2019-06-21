

class Column():

	def __init__(self, numberOfFloor, numberOfElevator):
		self.numberOfFloor = numberOfFloor
		self.numberOfElevator = numberOfElevator
		self.externalButtonList = []
		self.elevatorList = []
		for i in range(numberOfElevator):
			self.elevatorList.append(Elevator(i + 1, 1, numberOfFloor))	
		self.initExternalButtonList()

	def initExternalButtonList(self):
		for i in range(1, self.numberOfFloor): #-------- no UP with the higgest floor
			self.externalButtonList.append(ExternalButton(i,"UP",False))
		for i in range(2, self.numberOfFloor + 1): #---- no DOWN with the first floor
			self.externalButtonList.append(ExternalButton(i,"DOWN",False))		

	def findElevatorById(self, id):
		for elevator in self.elevatorList:
			if elevator.id == id:
				print("RESULT elevator #", elevator.id , " ( Current floor", elevator.currentFloor, ")")  
				return elevator

	def findBestElevator(self, requestedFloor, direction): 
		
		
		# DEFAULT Variables
		aIdleStatusExist = False
		bestGAP = self.numberOfFloor 
		elevatorIdWithBestGap = -1
		

		# 1 - Take the first IDLE elevator / or / compare directions
		for elevator in self.elevatorList:
			if elevator.status== "IDLE":
				aIdleStatusExist = True
			elif elevator.direction == "UP" and  direction == "UP" and   requestedFloor  >  elevator.currentFloor : 
				elevator.inSamedirection = True
			elif elevator.direction == "DOWN" and  direction == "DOWN" and   requestedFloor  <  elevator.currentFloor :
				elevator.inSamedirection = True
			else:
				elevator.inSamedirection = False
 

		# 2 - THE NEAREST ELEVATOR with the IDLE status 
		if aIdleStatusExist==True:
			for elevator in self.elevatorList:
				
				if elevator.status == "IDLE":
					Gap = elevator.currentFloor - requestedFloor
					Gap = abs(Gap) # Exemple : (-4) become (4)
					if Gap < bestGAP:
						#print("IDLE and Best gap is : " , Gap)  
						elevatorIdWithBestGap = elevator.id
						bestGAP = Gap
			
			#print("Return : THE NEAREST ELEVATOR with the IDLE status is : [", elevator.id ,"]")  
			return self.findElevatorById(elevatorIdWithBestGap)

		# 3 - THE NEAREST ELEVATOR that's is coming toward me 
		if aIdleStatusExist==False:
			for elevator in self.elevatorList:

				if elevator.inSamedirection == True:
					Gap = elevator.currentFloor - requestedFloor
					Gap = abs(Gap) # Exemple : (-4) become (4)
					if Gap < bestGAP:
						#print("INSAMEDIRECTION and Best gap is : " , Gap)  
						elevatorIdWithBestGap = elevator.id
						bestGAP = Gap
			#print("Return : THE NEAREST ELEVATOR that's is coming toward me is : [", elevator.id ,"]")  
			return self.findElevatorById(elevatorIdWithBestGap)	

	def requestElevator(self, requestedFloor, direction): 
		
		print(">>> requestElevator : " , requestedFloor , direction)  
		elevator = self.findBestElevator(requestedFloor, direction)

		elevator.addToQueue(requestedFloor)
		elevator.moveElevator()
	
	def requestFloor(self, elevator, requestedFloor):
		
		print(">>> requestFloor : " , requestedFloor , "  WITH  elevator #" , elevator.id , " ( Current floor", elevator.currentFloor, ")")  
		
		elevator.addToQueue(requestedFloor)
		elevator.closeDoor()
		elevator.moveElevator()
	
			
class Elevator():

	def __init__(self, id, currentFloor, numberOfFloor):

		self.id = id
		self.direction  =  ""  	  		   #// "UP" "DOWN"
		self.numberOfFloor = numberOfFloor
		self.currentFloor = currentFloor   #// Elevator's Position
		self.status =  "IDLE"                  #// "IDLE" "MOVING"
		self.queue  =  []        	       #// GOES UP [1,2,3,4...] or DOWN [6,5,4,3,...]
		self.internalButtonList  =  []     #// [1,2,3,4...] 
		self.door  = "CLOSED" 	 		   #// "CLOSED" or "OPENED"
		self.inSamedirection = False       #// True or False

		for i in range(1, self.numberOfFloor + 1): #---- no DOWN with the first floor
			self.internalButtonList.append(InternalButton(i,False))	

	def addToQueue(self, requestedFloor):

		print(">>> addToQueue : [", requestedFloor ,"] to", self.queue)  
        		
		self.queue.append(requestedFloor)
		print(">>> addToQueue REFRESH : " , self.queue)  
    
		if self.direction == "UP": 
			self.queue.sort(reverse=False)
			print(">>> addToQueue SORT UP : " , self.queue)  
		
		if self.direction == "DOWN":
			self.queue.sort(reverse=True)
			print(">>> addToQueue SORT DOWN :", self.queue)  

	def moveElevator(self):

		print(">>> moveElevator")  

		while self.queue:
			
			if self.door == "OPENED":
				self.closeDoor()
			
			firstElement = self.queue[0] 
			
			if firstElement == self.currentFloor:
				del self.queue[0]
				self.openDoor()
				print("------ door ------> ", firstElement, " <------------------")

			if firstElement > self.currentFloor:		
				self.status = "MOVING"
				self.direction = "UP"
				self.moveUp()
			
			if firstElement < self.currentFloor:
				self.status = "MOVING"
				self.direction = "DOWN"
				self.moveDown()

		if self.queue:
			pass
		else:
			print("Status : IDLE !!!")
			self.status="IDLE"
			  
	def moveUp(self):
			
		self.currentFloor = self.currentFloor + 1 #INCREMENT
		print("Current floor + 1 = ", self.currentFloor ," ", self.queue)  
	
	def moveDown(self):
				
		self.currentFloor = self.currentFloor - 1 #DECREMENT
		print("Current floor - 1 = ", self.currentFloor ," ", self.queue)  	
	
	def openDoor(self):
		self.door = "OPENED"

	def closeDoor(self):
		self.door="CLOSED"


class ExternalButton():

	def __init__(self, requestFloor, direction, activated):

		self.requestFloor = requestFloor
		self.direction  =  direction  	  # "UP" / "DOWN"
		self.activated =  False 		  # "ON" / "OFF"


class InternalButton():

	def __init__(self, floor, buttonActivated):

		self.floor = floor
		self.buttonActivated = False



print("--------------------------------------- TEST LIVRABLES -------------------------------------------------------------")

def Method1_requestElevator():
	column1 = Column(10, 2)

	column1.elevatorList[0].currentFloor = 2
	column1.elevatorList[0].direction  =  "UP" 	  		   
	column1.elevatorList[0].status =  "MOVING" #IDLE or MOVING
	column1.elevatorList[0].queue = [4,6]

	column1.elevatorList[1].currentFloor = 6
	column1.elevatorList[1].direction  =  "UP"  	  		   
	column1.elevatorList[1].status =  "MOVING" #IDLE or MOVING
	column1.elevatorList[1].queue = [8,9]

	column1.requestElevator(7, "UP")
#Method1_requestElevator()


def Method2_requestFloor():
	column1 = Column(10, 2)

	column1.elevatorList[0].currentFloor = 2
	column1.elevatorList[0].direction  =  "UP" 	  		   
	column1.elevatorList[0].status =  "MOVING" 
	column1.elevatorList[0].queue = [3,4]

	elevator = column1.elevatorList[0]

	column1.requestFloor(elevator, 9)
#Method2_requestFloor()


