using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStat : MonoBehaviour
{
    [SerializeField] PlayerStatManager playerStat;
    [SerializeField] CharacterAttack playerAttack;
    private List<Augment> augments;

    private List<float> stats;
    // Start is called before the first frame update
    void Start()
    {
        loadInventory();
    }

    private void loadInventory()
    {
        //load prev inventory
    }
    
    private void saveInventory()
    {
        //save current inventory
    }

    public void addAugment(Augment augment)
    {
        augments.Add(augment);
    }

    public void removeAugment(Augment augment)
    {
        augments.Remove(augment);
    }

    private void setStat()
    {
        //check attack stat
        foreach(Augment augment in augments)
        {
            stats[0] = stats[0] + augment.attackModifier;
            stats[1] = stats[1] + augment.speedModifier;
            stats[2] = stats[2] + augment.energyModifier;
        }
    }

    public float getAttackModifier()
    {
        return stats[0];
    }

    public float getSpeedModifier()
    {
        return stats[1];
    }

    public float getEnergyModifier()
    {
        return stats[2];
    }


}
