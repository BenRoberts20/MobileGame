using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]
public class Ships : ScriptableObject
{
    public string Name;
    public string Description;
    public int Cost;
    public int Health;
    public int Damage;
    public float Speed;
    public Sprite sprite;
}
