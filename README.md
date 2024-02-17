# Stealth_Project

## Introduction
This is a 3D decryption game made with unity by myself. The way the game is played is that the player controls the character to avoid the robot (enemy) and a series of laser alarm triggers, find the controllers to turn off the triggers and the exit-door key, then escape through the red gate by taking the elevator.

The game design and models are not my own creation, the components and terrain are from Unity's official free package, and what I did was to utilize all the resources I could to assemble them into a complete project, which also included animating the moving units, tweaking all the models and terrain, setting up all the triggers, and writing the necessary scripts. 

## Animator Setting
***Sets animations for removable units.***

* [Animator Setting Screenshot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/AnimatorSetting.png)

## Character Settings

### Player Setting
***Add all necessary scripts for player character including audio, collider, rigidbody and adjust the animation Settings. In PlayerHealth script, the player's health value is set (hidden) and when the player dies, the death audio is triggered and the game restarts. The player's control is mainly in the PlayerMovement script, and the purpose of PlayerBag.cs as a singleton script and PickUpKeyCard.cs are to check if the player has picked up the elevator door key card.***

* [Screenshot of Player](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/PlayerSetting.png)

* [Player Movement Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PlayerMovement.cs)

* [Player Health Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PlayerHealth.cs)

* [Player Bag Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PlayerBag.cs)

* [Key Card Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/PickUpKeyCard.cs)

*Jump to: [Page Top](#Stealth_Project),[Settings](#animator-setting),[Summary and Experience](#summary-and-experience)*

### Robot(Enemy) Setting
***Add all necessary scripts for Robot character including audio, collider, rigidbody and adjust the animation Settings. The navigation system is used to set up automatic patrol for the robot within a certain range, if alarm system is triggered (including the robot's vision and hearing, as well as a series of hidden cameras and laser rays in the whole map), the robot will immediately chase and try to shoot player. After being thrown away by the player using the terrain, the robot will start walking patrol again.***

* [Screenshot of Robot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/EnemySetting.png)

* [AI Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/EnemyAI.cs)

* [Robot Animation Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/EnemyAnimation.cs)

* [Robot Attack Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/EnemyShoot.cs)

* [Robot Alarm Range Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/EnemySightingAndHearing.cs)

* [Shooting Effect Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/ShootingEffect.cs)

### Alarm System
***Use position changes to determine if an alarm system is triggered, if so play alarm audios to alert the player to a dangerous situation.***

* [Alarm Triggers Screenshot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/AlarmTrigger.png)

* [Alarm Light Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/AlarmLight.cs)

* [Alarm System Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/AlarmSystem.cs)

* [Alarm Trigger Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/AlarmTrigger.cs)

*Jump to: [Page Top](#Stealth_Project)*,[Settings](#animator-setting),[Summary and Experience](#summary-and-experience)*

### Other Settings
***Cameras and lasers are linked to the alarm system, and they display different states depending on whether the alarm is triggered or not. Whether the Exit Gate can be opened based on the player's current location(also must face to the Gate) and whether he has a key card, open and close of all doors are actually moving the coordinates of their models. CameraFollow.cs is written for following the player's view as they move.***

* [Exit Gate Screenshot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/ExitSetting.png)

* [Laser Switches Screenshot](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Images/SwitchSetting.png)

* [Exit Gate Controller Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/BigDoorController.cs)

* [Lift Controller Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/LiftController.cs)

* [Lift Door Controller Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/LittleDoorController.cs)

* [Accessible Door Controller Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/LittleDoorController.cs)

* [Tracking Camera Controller Script](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/CameraFollow.cs)

* [Lasers Controller Scripts](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/LaserController.cs)

* [Camera Follow Scripts](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/CameraFollow.cs)

### Game Constants
***All the constants used in the scripts are concentrated in this class***

* [Game Consts](https://github.com/Jiaha0-Zhang/Stealth-Project/blob/master/Scripts/GameConsts.cs)

## Summary and Experience 
This was a project I completed independently before joining The Tech Academy, and it took me nearly 2 weeks to complete because there was no help from anyone at that time. Fortunately, the complex terrain and thousands of widgets were already set as prefabs, which saved me a lot of time. Although working alone cultivated my ability to think independently and to seek for solutions, I still look forward to working with others because peers can also be teachers of the each other in a way.

In this project, I encountered a lot of difficulties and challenges. For example, the tracking camera was fixed directly on top of the player at a certain distance when the camera followed the player at first, which made me really dizzy. Later, after inquiring some resources, Vector3.LERP was used to make the whole view more natural and solve this problem instantly. 

*Jump to: [Page Top](#Stealth_Project),[Settings](#animator-setting),[Summary and Experience](#summary-and-experience)*