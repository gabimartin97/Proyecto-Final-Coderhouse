using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{[Header("Data")]
    [SerializeField]
    [Range(100, 300)]
    public int life = 100;

    [Header("Movement")]
    [SerializeField]
    [Range(1f, 50f)]
    public float moveSpeed = 5f;

    [Header("Power")]
    [SerializeField]
    [Range(1, 50)]
    public int damage = 10;
    
    [SerializeField]
    [Range(1f, 50f)]
    public float attackDelay = 5f;
    }
