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

### Extensions and Changes
- A Simulation class has been added to the system to manage the setup of the simulation.
  - Simulation variables and parameters are passed into the system through a struct that contains all variables in a neat package
- Simulation Runs and Control variables have been refined in the Methodology section of the literature review
literature review or UML diagram
