using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    GameState state;

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {

        }
        else if (state == GameState.Battle)
        {

        }
    }
}
