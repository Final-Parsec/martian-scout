using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsAndEnums : MonoBehaviour
{
    public const string Projectile = "projectile";
    public const string Player = "Player";
    public const string PlayerMovement = "PlayerMovement";
    public const string ScoreDisplay = "ScoreDisplay";
    public const string BoostBar = "BoostBar";

    public enum PlayerWeapon
    {
        StarterShooter=0
    }

    public enum ProjectileType
    {
        BasicBullet=0
    };

    public enum RockType
    {
        RegularRock=0,
        Fragment=1
    }

    //Level Game States
    public enum GameState {
        Start = 0, //no enemies for a moment you get your bearings
        Tier1 = 1, //these increasing tiers will represent more rox
        Tier2 = 2,
        Tier3 = 3,
        Win = 4,
        Lose = 5
    }
}
