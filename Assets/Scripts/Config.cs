using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  All Information that is consistent between levels should be here and here only
 */

public static class Config
{
    public static class Tags
    {
        public static readonly string Wall= "Wall";
        public static readonly string DeadPit = "DeadPit";
        public static readonly string Enemy = "Enemy";
        public static readonly string Player = "Player";
        public static readonly string Jumpable = "JumpPlatform";
        public static readonly string Checkpoint = "Checkpoint";
    }

    public static class ObjectNames
    {
        public static readonly string mainCamera = "MainCamera";
        public static readonly string respawnPoint = "RespawnPoint";
        public static readonly string playerParent = "PlayerParentObject";

    }

    public static class Input
    {
        public static readonly KeyCode left = KeyCode.A;
        public static readonly KeyCode right = KeyCode.D;
        public static readonly KeyCode forward = KeyCode.W;
        public static readonly KeyCode backward = KeyCode.S;

        public static readonly KeyCode jump = KeyCode.Space;
        public static readonly KeyCode sprint = KeyCode.LeftShift;

        public static readonly KeyCode flyToggle = KeyCode.Q;
        public static readonly KeyCode ascend = KeyCode.LeftShift;
        public static readonly KeyCode descend = KeyCode.Space;
    }

    public static class Levels
    {
        private static readonly Level[] levels = { new Level("Level_01"), 
                                                   new Level("Level_02"),
                                                   new Level("Level_03"),
                                                   new Level("ObjectLibrary"),
                                                   new Level("EnemyTesting"),
                                                   new Level("TestingScene"),};

        public static Level GetLevel(int index)
        {
            return levels[index % levels.Length];
        }

        public static Level GetLevel(string levelName)
        {
            foreach (var level in levels)
            {
                if (level.name == levelName)
                    return level;
            }

            throw new System.Exception("Scene name does not exist in configuration");
        }

    }

    public static class MaterialPaths
    {
        public static readonly string ghostPowerup = "Materials/GhostPowerup";
        public static readonly string player = "Materials/Player";
    }
}

public struct Level
{
    static int levelsCreated = 0;
    public Level(string _name)
    {
        name = _name;
        order = levelsCreated++;
    }

    public readonly string name;
    public readonly int order;
}