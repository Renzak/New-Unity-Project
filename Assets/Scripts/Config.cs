using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    // Tags
    public static readonly string DEAD_TAG = "DeadPit";
    public static readonly string ENEMY_TAG = "Enemy";
    public static readonly string PLAYER_TAG = "Player";
    public static readonly string JUMPABLE_TAG = "JumpPlatform";
    public static readonly string CHECKPOINT_TAG = "Checkpoint";
    public static readonly string RESPAWN_OBJECT_NAME = "RespawnPoint";
    public static readonly string PLAYER_PARENT_OBJECT_NAME = "PlayerParentObject";

    // Basic movement
    public static KeyCode leftMovementKeyCode = KeyCode.A;
    public static KeyCode rightMovementKeyCode = KeyCode.D;
    public static KeyCode forwardMovementKeyCode = KeyCode.W;
    public static KeyCode backwardMovementKeyCode = KeyCode.S;

    public static KeyCode jumpKeyCode = KeyCode.Space;
    public static KeyCode sprintKeyCode = KeyCode.LeftShift;

    // Cheat related
    public static KeyCode flyToggleKeyCode = KeyCode.Q;
    public static KeyCode ascendFlyKeyCode = KeyCode.LeftShift;
    public static KeyCode descendFlyKeyCode = KeyCode.Space;
}