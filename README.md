# ArmyClash Concept, Requirements, and Architecture

## 1. Overview

This project is a prototype of an Army Clash game.
Games in this genre are automated or semi-automated battle simulators in which the player:
- creates or configures the army composition before the battle;
- launches the simulation;
- observes the battle without directly controlling units;
- receives the battle result (win/loss) and returns to preparing the next scenario.
The main goal of the project is to implement a basic battle simulation and demonstrate an approach to designing a maintainable and expandable game system.
---
## 2. Technical Constraints and Platform Requirements

- Game Engine: **Unity**
- Target Platform: **Mobile**
---
## 3. Functional Requirements

- The system must provide random generation of army compositions before the start of the simulation
- The system must allow the user to launch a simulation of a battle between two armies
- The system must automatically control all units during the battle without user intervention
- The system must support interactions between units, including enemy detection, movement to targets, and damage infliction at attack range
- The system must remove units from the battlefield upon their death
- The system must determine when one army is completely defeated and end the simulation
- The system must display the battle results to the user after its completion, and then return to the main menu
---
## 4. Game Concept
### 4.1 Game Loop

A typical game loop is as follows:
1. Main Menu
2. Formation or Army Randomization
3. Starting a Battle Simulation
4. Automatic Battle
5. Ending a Battle
6. Returning to the Main Menu
### 4.2 Armies and Units

An army is a collection of units. Each army acts independently and pursues the goal of destroying the enemy. Units are the main active entities of the simulation.
### 4.3 Unit Stats

Each unit must have 4 stats:
- Health Points (HP)
- Attack Power (ATK) - damage to health points inflicted per attack on an enemy unit
- Movement Speed ​​(SPD) - the number of units moved per second
- Attack Speed ​​(ATKSPD) - modifier for the delay between attacks

Base Unit Stats:
- 100 HP
- 10 ATK
- 10 SPD
- 1 ATKSPD
### 4.4 Units classification

Each unit is formed based on a combination of the following classes (classes of the same type cannot be combined):
- **Shape**:
	- Sphere
	- Cube
- **Color**:
	- Blue
	- Red
	- Green
- **Size**:
	- Small
	- Big

Each class has its own bonuses/penalties to its stats, which are listed in the table below:

| Class Type | Class  | **Effect** |     |     |        |
| :--------: | :----: | :--------: | :-: | :-: | ------ |
|            |        |     HP     | ATK | SPD | ATKSPD |
|   Shape    |  Cube  |    +100    | +10 |     |        |
|            | Sphere |    +50     | +20 |     |        |
|    Size    |  Big   |    +50     |     |     |        |
|            | Small  |    -50     |     |     |        |
|   Color    |  Blue  |    -15     |     | +10 | +4     |
|            | Green  |    -50     | +20 | -5  |        |
|            |  Red   |    +200    | +40 | -9  |        |
### 4.5 Combat Simulation Rules

- Units automatically select a target among living enemies
- Preference is given to the closest enemy
- The unit moves to the target before reaching attack range
- All units have only melee attacks
- The attack deals damage to the enemy unit's health periodically
- Upon reaching zero health, the unit is considered destroyed and removed from the simulation
---
## 5. Non-Functional Requirements
### 5.1 Maintainability

- Code must be divided into logical modules with clear areas of responsibility
- Dependencies between logical modules must be minimal
- Combat simulation logic must be separated from visual representation and rendering logic
- Simulation logic and unit/class characteristics must be easily configurable by game designers
- Changes to unit appearance must not require changes to the gameplay logic
- System behavior must be described through explicit states and a lifecycle
### 5.2 Extensibility

The system must allow, without significant changes to the game logic:
- adding new unit classes and models;
- adding new unit characteristics and behavior strategies;
- adding new simulation modes and rules;
- adding a mode for manually generating army composition before battle;
- changing army generation rules.
### 5.3 Reliability and Performance

- Simulation performance must be sufficient for stable operation on low-end mobile devices, or performance issues that prevent this must be documented
- Runtime errors must be fixed or documented
- The project must run without compilation errors
---
## 6. High-Level Architecture Overview

---
## 7. Architectural Decisions
### 7.1 Architectural Style

### 7.2 Principles and Patterns

### 7.3 Technologies Used

- Unity Engine;
- Built-in Unity tools for visualization and animation.
### 7.4 Third party packages

- Zenject - is a lightweight highly performant dependency injection framework built specifically to target Unity 3D.
- UniCore - is an open-source framework authored by the developer of this project.
It provides a set of reusable architectural and utility components
that help speed up development and reduce boilerplate code.

---
## 8. Known Limitations and Trade-offs
