using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config",menuName = "PlayerConfig")]

public class PlayerConfiguration : ScriptableObject
{
    [Header("Float")]
    public float balance, superJumpBar, range, bonusOfSuperJump, jumpFrc, speed, playerO2Health,rotateSpeedHor;
    [Header("Int")]
    public int O2Cylenders, CrystalsOfArgon;
}
