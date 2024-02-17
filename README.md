# Stealth_Project

## Introduction
This is a 3D decryption game made with unity by myself. The way the game is played is that the player controls the character to avoid the robot (enemy) and a series of laser alarm triggers, find the controllers to turn off the triggers and the exit-door key, then escape through the red gate by taking the elevator.

The game design and models are not my own creation, the components and terrain are from Unity's official free package, and what I did was to utilize all the resources I could to assemble them into a complete project, which also included animating the moving units, tweaking all the models and terrain, setting up all the triggers, and writing the necessary scripts. 

## Animator Setting
***Sets animations for removable units.***

* [Animator Setting Screenshot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/AnimatorSetting.png)

## Character Settings

### Player Setting
***Add all necessary scripts for the player character including audio, collider, rigidbody and adjust the animation Settings. In PlayerHealth script, the player's health value is set (hidden) and when the player dies, the death audio is triggered and the game restarts. The player's control is mainly in the PlayerMovement script, and PlayerBag's only function as a singleton script is to check if the player has picked up the elevator door key.***

* [Screenshot of Player](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/PlayerSetting.png)

* [Player Movement Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PlayerMovement.cs)

* [Player Health](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PlayerHealth.cs)

* [Player Bag](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PlayerBag.cs)

### Robot(Enemy) Setting

* [Screenshot of Robot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/EnemySetting.png)

* [AI](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/EnemyAI.cs)



