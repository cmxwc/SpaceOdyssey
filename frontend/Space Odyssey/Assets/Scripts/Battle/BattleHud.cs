using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public HpBar hpBar;


    public void SetData()
    {
        nameText.text = DataManager.username;
        levelText.text = "Lvl " + DataManager.level;
        hpBar.SetHp((float)DataManager.health / DataManager.maxHp);
    }
    public void SetEnemy(int enemyLevel)
    {
        nameText.text = "Enemy";
        levelText.text = "Lvl " + enemyLevel;
        hpBar.SetHp((float)100 / 100);
    }

    public IEnumerator UpdateHP(int newHP, int maxHP)
    {
        if (newHP < 0)
        {
            newHP = 0;
        }
        yield return hpBar.SetHPSmooth((float)newHP / maxHP);
    }

}
