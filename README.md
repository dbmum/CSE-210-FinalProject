# CSE-210-FinalProject
Worms Armageddon Remastered

This game is a turn-based shooter between two teams of players. THis physics based game is a fun challenge and survival of the fittest type game where the best team will win!

High priority features:
    Players
    Arrow (above active player)
    Terrain
    Guns
    Bullets

Low priority features:
    Randomly generated terrain
    destructable terrain
    Menu to choose amount of players on each team and player type
    AI players
    Different shot types/weapons

Classes and responsibilities:

    Cast<Actor>:
        player - defines the player on the screen (needs _activePlayer _playerType 
            past the usual variables)
        gun - Rotating image that determines direction of spawned bullets
        bullet - the actor that gets fired and interaccts with the other players and
            terrain
        terrain - allows the player to walk and bullets to disappear when hitting the
            (A list of points)
        arrow - Stay above the active player to display whose turn it is

    Script<Action>:
        ControlActorsAction - handle inputs to move the players 
        MoveGunAction - moves the orientation of the gun with inputs
        ShootAction - fire a bullet based off of the orientation of the gun
        GravityAction - Have gravity affect certain actors, such as bullets 
            (modify velocity)
        HandleCollisionsAction - handle the collisions of the various actors
        HandleChangeofTurn - change turn once the player has shot and the bullet has
            arrived at its destination.
        DrawActorsAction - draw the actors on the screen
        MoveActorsAction - Move the actors their velocity

    Services (Raylib):
        InputService - Use Raylib to get inputs from the user
        OutputService - Use Raylib to display things on the screen
        PhysicsService - Use Raylib to check for collisions of actors
        AudioService - Use Raylib to output audio.