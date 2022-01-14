using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Augment" , menuName = "Augment")]
public class Augment : ScriptableObject
{
    public int id;
    public new string name;
    public string desciption;

    public Sprite artwork;

    public float attackModifier;
    public float defenseModifier;
    public float attackRangeModifier;
    public float healthModifier;
    public float energyModifier;
    public float speedModifier;


}
