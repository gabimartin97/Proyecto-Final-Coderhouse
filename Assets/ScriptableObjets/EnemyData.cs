using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="EnemyData",menuName ="EnemyData")]
public class EnemyData : ScriptableObject
{
     [Header("Stats")]

    [SerializeField]
    [Range(100f, 500f)]
    public float life = 100f;

    [SerializeField]
    [Range(1f, 10f)]
    public float damageCooldown = 5f;

    [SerializeField]
    [Range(25f, 100f)]
    public float damage = 25f;

}
