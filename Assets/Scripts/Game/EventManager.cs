using System;
using UnityEngine;

public class EventManager
{
    public static event Action OnGameStart;
    public static void Event_OnGameStart()
    {
        OnGameStart?.Invoke();
    }


    public static event Action OnGameOver;
    public static void Event_OnGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static event Action<CharacterManager, CharacterManager> OnCharacterDie;
    public static void Event_OnCharacterDie(CharacterManager target, CharacterManager attacker)
    {
        OnCharacterDie?.Invoke(target, attacker);
    }

}
