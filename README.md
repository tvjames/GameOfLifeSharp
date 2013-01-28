# Game Of Life Sharp

Implementation of Conway's Game of Life in C# ported from the Ruby 
code written in the last session of the Brisbane Code Retreat (CR) on 
Global Day of Code Retreat 2012. (With Anton)

This port builds on the CR version by allowing the user to navigate around the 
game world.

## How to play

The console application must be provided with the inital game state, this is 
read from a command line argument and loaded from a file that contains pairs of
coordinates for the inital state of the game. 

### Example 

    > GameOfLifeSharp -s 100 blinker.gols 
    
    Parameters
        -s , --speed    Specifies the time in milliseconds between game transitions

### Controls

    q, Q    Quits the game of life
    h, H    Returns the viewport to the original view 
    Up      Move the viewport up
    Down    Move the viewport down
    Left    Move the viewport left
    Right   Move the viewport right

### Game Files

The game state is stored in text files that contain a list of coordinate pairs in 
natural numbers. The format is relaxed to whitespace. The current game state is 
written out when the game exits. 

Example

    0,0 1,0 -1,0
    1,1      1,2

Additional whitespace will be ignored, allowing the pairs to be arranged as you like. 
