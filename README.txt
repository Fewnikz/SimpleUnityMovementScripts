About the project:

This repository contains two simple scripts for 2D character movement

Horizontal Movement: Handles basic movement right and left. Also handles walking and running

Jumping: Handles jump logic for the player. Also includes stuff like coyote jump, variable jump height and ground detection

These scripts are meant to be easy to integrate into Unity 2D projects.


Planned Improvements:

Refactor both scripts into one single script.

Use classes to separate the horizontal movement and jumping logic for better structure and reusability.

Clean up the code to improve the readability

Make the code easier to use


How to use the scripts?:


Step 1. Setting Up the Player GameObject

Create a new game object with a Rigibody2D

Attach both the moveController and the jumpController to the game object


Step 2. Configuring the jumpController

Add an empty child game object under the players, then name it feet, and move it to the bottom of your player object. This will act as the raycast origin for the player

Now assign the feet transform to the feet field in the jumpController inspector


Configure the following in the Inspector:

Raycast Length: Controls how far down the raycast checks for ground.

Ground Mask: Assign the right layer that is used by your ground/platforms
	Tip: Create a new layer named Ground" or something similar. Now assign it
		to your ground/platform objects, then select it in the ground mask field


Now for the jump power: Which controls how high player jumps and the feel of it:

First off the gravity Settings: normalGravityScale and gravityFall controls how jumping and falling feels

Jump start Time and the Jump Power Fraction: Tune these two for the variable jump heigh 

Coyote Time allows more forgiving jump timing


(Optional) Assign a MMFeedbacks asset to the jumpFeedback field for visual effects. But if you don't have the asset or don't want to use it. 
Then just delete the MMFeedback stuff from the code


On to step 3. Using the MoveController

No need for any special setup. You just need to add the script


Configure:

Walk Speed: Is the movement then you are not running

Running Speed: Is the movement then u hold the left shift button


Last step 4. Input

Movement: A/D or Left/Right Arrow keys

Jump: Spacebar (supports variable jump height)

Run: Just hold the left shift
