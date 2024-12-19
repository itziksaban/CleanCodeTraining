# Snake Game

Let us see how to design a basic Snake Game.

Game rules:
- The user controls its snake on the board - right/left/up/down
- On the board there are many other moving snakes that are controlled by the computer.
- When any snake crosses another snake, the other snake becomes food. 
- When any snake eats food, the length of the snake increases accordingly. 
- When the user's snake becomes food, the game is over.
- When the user's snake crosses itself, the game is over. 
- When the user's snake crashes the board borders, the game is over.
 

Logics to consider:
- Understand the direction the user wishes to move to
- Understand if a snake hits another snake, food, boarder, or none.
- Calculate the shape of and location of each snake after each round
- Repaint the entire board after each round
