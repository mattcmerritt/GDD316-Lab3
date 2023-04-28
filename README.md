# GDD316-Lab3
WebGL Build: https://mywebspace.quinnipiac.edu/mcmerritt/GDD_316/lab3/index.html

GitHub Repository: https://github.com/mattcmerritt/GDD316-Lab3

# Controls
WASD to move the camera
Buttons on screen to change resource target

# Purpose
The goal is to keep your workers safe, while still gathering as many resources as possible. The only control that the player has over the agents is that the player can set the resource target, which will cause the workers to leave home and gather resources.

# Agents
There are three types of agents in the scene:

## Workers (light blue)
- By default, they will stand at their home location where they spawned.
- If the resource target is not met, they will find the nearest tree and begin collecting wood from it.
- Once at a tree, they will pick up logs of wood which can be seen on their model
- Once they have 5 logs, they will return to home and drop off the logs, then go back to the default behavior
- If attacked, they will freeze up for a bit, drop what they are carrying, and then return home

## Guards (dark blue)
- By default, they patrol between 2 set points near the houses on the map
- If they see a worker during the day, they follow that worker
- If it becomes night, they will return to patrolling
- If an enemy comes close, they will go up and attack the enemy
- If they take 5 hits, they become unconscious, and must recover all their health by standing still
- If they defeat an enemy, they return to patrolling

## Enemies (red)
- By default, they randomly roam all over the map
- If they see a worker and are not fighting a guard, they will chase the worker and try to attack them
- If chasing a worker that gets back home, they will go back to roaming
- If they scare a worker, they will go back to roaming
- If they see a guard, they will go up and attack the guard
- If they take 3 hits, they will disappear
- If they defeat a guard, they return to roaming