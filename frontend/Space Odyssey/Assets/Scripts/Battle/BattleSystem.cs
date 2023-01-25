using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    // [SerializeField] BattleUnit enemyUnit;

    private void Start()
    {
        SetupBattle();
    }

    public void SetupBattle()
    {
        playerHud.SetData();
        enemyHud.SetEnemy();
    }
}
