using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float jumpHeightMul = 0.5f;
    public float platformCheckTime = 0.1f;

    [Header("Wall State")]
    public float wallCheckRadius = 0.3f;

    [Header("Ledge climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCooldownTime = 0.5f;
    public float dashSpeed = 15f;
    public float dashTime = 0.2f;
    public float dashDistanceBetweenImages = 0.1f;


    [Header("Check Variables")]
    public float groundCheckRaius = 0.3f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;
}
