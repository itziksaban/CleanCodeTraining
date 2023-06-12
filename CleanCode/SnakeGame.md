# Snake Game

Let us see how to design a basic Snake Game.

Game rules:
- A snake can move in a given direction and when it eats the food, the length of snake increases. 
- Food will be generated at a given interval.
- When the snake crosses itself, the game will be over. 
- When the snake crashes the board borders, the game will be over.
 



Please consdider where to place the following logic considerations:
- A snake is a list of dots on the board - this list of dots should be re-calculated on every move.
- In every move, decide if the game is over or not (if snake crashes itself or the boarders).
- Generate food
- On every move, the snake and all the other objects should be re-drawn on the screen.
- Where will be the logic that decides in which direction the user chooses to move to - right/left/up/down
