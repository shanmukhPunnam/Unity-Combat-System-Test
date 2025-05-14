using UnityEngine;

namespace Subvrsive.Combat.Characters
{
    public class CharacterStats : MonoBehaviour
    {
        public CharacterStatsData characterStatsData = new();

        public void AddKill()
        {
            characterStatsData.KillCount++;
            Debug.Log($"{gameObject.name} has {characterStatsData.KillCount} kills.");
        }
    }
}

