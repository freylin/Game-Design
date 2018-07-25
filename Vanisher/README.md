## Introduction
Vanisher is a stealth game. The player controls a thief. To win, the thief must go to the destination without being caught by the guards. The thief can simply hide behind obstacles and crouch towards the destination. The thief have some special skills to help him escape. For example, the thief can distract the guards and make the guards lose clear vision.

## How to Play
Press 'w/s/a/d' to move the thief.

Move the mouse to change orientation of camera.

Press 'c' to crouch.

Press '1' to throw a stone to distract the guards.

Press '2' to fire smoke bomb to make the guards lose vision for a while.

Press '3' to sprint.

## How to Observe Technology Requirements
### Character Animation
The thief is animated using a fairly complex animation blend tree. Transitions between different animations are smooth. Please play with character movement using w/s/a/d/c.

### AI
A powerful AI system (finite state machine) is installed in every guard in the game. The spherical indicator above each guard indicates the currect state (patrol/suspect/chase/search) that the guard is in. 

### Physics Simulation and Interaction
The stone throwing skill of the thief is an example of using physics simulation. A few more examples include openable chests and doors in the game.

### UI
Game start and restart menus are implemented. Game-over and you-win images are shown on appropriate losing/winning conditions. A sprite may show up if the thief approaches an interactable object. For example, an exclamation mark will show up when the thief is close to a chest.
