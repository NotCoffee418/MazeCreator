# Maze Creator #
This tool allows you to build a mazes, generating an SQL file of game objects.
You can probably find other uses for it as well.

### Requirements ###
* TrinityCore or cMangos
* Windows 7 or better
* [.NET Framework 4.5](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

### How to build a maze ###
1. [Download the latest Maze Creator](https://github.com/RStijn/MazeCreator/blob/master/MazeCreator/bin/Release/MazeCreator.exe?raw=true)
2. Open and enter information about the location and size of your maze.
3. Mark all walls as red & all open spaces as green.
4. Add a wall on the outside with Tools > Fill Borders
4. Make sure you have an entrance if this is needed.
5. When your maze is done go to File > Export to SQL to save your maze as an SQL file.
6. Import this SQL file into your World database.
7. Restart your emulator and see if you can beat your maze. :)

#### Warning ####
Large mazes may cause server-side lag if several players are near the maze since many gameobjects have to be loaded by each player.
