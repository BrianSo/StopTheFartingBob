# 1 Getting Started

**To get started working with the project**
 1. `git clone https://github.com/BrianSo/StopTheFartingBob.git`
 2. Start working by opening the project using Unity3D 5 or above

**To compile the project**
 1. Open the project using Unity3D 5 or above
 2. On the Unity menu, click build and select appropriate platform to build.

# 2 Project description
## 1 Presentation Slide
Checkout the presentation slides [Here](https://docs.google.com/presentation/d/1b8dvDCDKALaRcWPFodAd9jGj9n7q5GgIh6-MmXZusYs/edit#slide=id.g17f2a2781c_0_0)

## 2 Title and Description

**Title:**
Stop the Farting Bob

**Type:**
Multiplayer 2D Top-down Action Hide and Seek Game

**Story:**
The Gardener does not let Bob play in the garden, so Bob hates the Gardener. Bob takes revenge by farting in his garden to pollute all his plants. The gardener of course wants to prevent this, so he chases down Bob...

**Game Objective:** 
Bob: Keep farting to raise pollution index to 100. 
Gardener:	Defeat Bob to stop his fart from polluting the garden

**Details description:**
The game is played by two players, one playing as Bob and the other playing as the Gardener. Bob will keep farting while the Gardener tries to stop him. The fart is both an advantage and disadvantage for Bob. When the Gardener smells the fart for too long, he will be stunned and unable to move for a few seconds. However the fart can also be a clue for the gardener to trace the location of Bob.

Items will appear on the map periodically. These items can help Bob to escape and the Gardener to catch Bob. Bob can increase the pollution rate by doing certain tasks on map. To win the game, it requires the luck, strategy and real-time reaction of the players.


## 3 Novelty
Farting is the main theme of the game which can provide a lot of funny moments. It gives another way for Bob to win, as Bob can aggressively do a lot of tasks to increase the rate of pollution. Farting as a double-edged sword will make the game more interesting.

In addition, the map of the garden is randomly generated so it will be different for each game. The variety of items such as radar, traps, banana peels will provide diverse experience.

Field of view of the character will be implemented which adds more suspense to the game. With reference to the game Subterrain, the players has to be aware of what they can see. It will also be easier for players to deceive each other because of the limited vision [1].

## 4 Challenges
**Graphics:**
It will be challenging to implement the varying field of view that is dependant on a multiple factors. The first factor is the walls and obstacles that are blocking the line of sight so the player cannot see through it. To calculate the visible area, ray tracing method might be required [2].

Other factors will be elements like bushes and items that modifies their vision. For bushes, the players cannot see into the bushes but the players inside can see the outside. One of the items will be a smoke bomb that can create a smoke field. Players cannot look into the smoke field and when the players are inside the smoke field, they will have reduced vision. Therefore, we have to adjust the field of view dynamically based on the different factors.

There are some possible solutions:
 1. Write a shader to darken the non-visible area.
 2. Add a layer object onto the map and dynamically change the texture.
 3. Make the game in 3D and uses spotlight to lighten the visible area.

**Networking:**
The game supports a 1 v 1 multiplayer mode through the Internet. One player will play as Bob and the other will player as Gardener. One of the player’s machine will be used as the server to host the game. The synchronization of all objects, whether visible or not, will be difficult.

**Map and item generation:**
The map will be procedurally generated so it will be different every time it is played. Elements generated includes the walls, obstacles and bushes of the map. The items that can be picked up will be periodically generated on the map for the players to pick up. The map has to be ensured that there is no isolated area. Also, balancing the map generation would also be a difficulty.

