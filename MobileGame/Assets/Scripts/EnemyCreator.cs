using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyCreator : ScriptableObject
{
    public int Health;
    public int Damage;
    public string Description;
    public int ExpDrop;
    public int FragmentsDrop;
}
