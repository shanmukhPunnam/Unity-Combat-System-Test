using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewWeaponInventory", menuName = "Inventory/WeaponInventory")]
public class WeaponInventory : ScriptableObject
{
    public List<WeaponData> weapons; 
}
