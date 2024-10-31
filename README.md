# TrafficSystem
### Goals
The main goal of this system is to gain the ability to repeatedly measure the traffic created by closing a specific number of lanes for a specific length of road. Existing reasearch has struggled to analyze traffic in construction zones due to the sheer number of factors that effect real-world traffic. By creating a simplified, repeatable system without the environmental factors of a complex system, general conclusions on the effect of construction on traffic systems will be able to be confidently drawn.

### Overall Plan for the Simulation
There are several variables to modify when running the simulation to gather data: number of vehicles on the road, how quickly to populate the highway with vehicles, where lane closures occur, and where vehicles will try to get off the highway.

#### Control with 1 Exit
A control will be established by randomly populating the a 5-lane highway of length 300 with 100 vehicles, all vehicles will be assigned the target exit of 200. This will be run 3 times and the data averaged will serve as the control data for vehicles merging to a single exit, this will be the simplest simulation run.
#### Simulation Run 1
The same setup as the control is repeated with lane closures starting at index 25 and ending at 75. This test will be run with various depths(up to closing all but one lane) of lane closures, meaning the right lane, then the right two lanes, and so on.

#### Control with 4 Exits
For our next control 100 vehicles will be simulated on a 5-lane highway. Exits are at 50, 75, 100, and 125. These exits will be distributed sequentially as cars are spawned, meaning the first car’s exit will be 50, the second 75, and so on, repeating: the fifth car’s exit will be 50. These parameters will be run 3 times and the data will be averaged and will serve as the control group with no lane closures.
and any extensions or changes to the original documentation (such as the literature review or UML diagram).
#### Simulation Run 2
The second control setup will be repeated with lane closures from 35 to 40, 60 to 65, and 105 to 115. This will be our worst case scenario test. Data will be recorded at various depths of lane closures(up to closing all but one lane).

#### Simulation Parameters:
![image](https://github.com/user-attachments/assets/16b8bfd5-eff9-4b14-b242-1c4a8bdc05d2)

Highway Width
- Number of lanes on the highway.

Highway Length
- Length of the highway.

Number of Cars
- How many vehicles to spawn.

Lane Closure Start and End 
- Lanes are closed between the starting index and this index.

Lane Closure Width
- How many lanes are closed, starting from the rightmost lane.

Exit Location
- The target destination of vehicles, add multiple exits separated by comma example: “12, 18”.

Incoming Traffic
- 1 Random: a vehicle is spawned in a random lane each timestep.
- 2 Sequential: vehicles are spawned by iterating through the available lanes(ie. 1,2,3, 1,2,3).
- 3 Simultaneous: Vehicles are spawned in all lanes simultaneously until the configured vehicle count is reached (Heavy Traffic).

File Path
- Specify where to output the results in the form of a .txt file. To not output results leave this blank.


#### How to Run Simulation
- Download the latest release in the 'releases' tab.
- Unpack the .zip
- Run the "TrafficSystem.exe"
- The application is now running.
- Set simulation parameters as desired(see list above). Once satisfied confirm by pressing "Save"
- Then press "Run"
- A new window will appear with your running simulation.
  ![image](https://github.com/user-attachments/assets/37ff180b-9926-4517-a29d-fd2e8b8055f1)

#### Features to be added
- Dynamically change timestep(normal speed, fast forward?)
-  Pause feature
