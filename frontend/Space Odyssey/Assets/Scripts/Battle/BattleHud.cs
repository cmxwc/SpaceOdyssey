using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public void SetEnemy()
    {
        nameText.text = "Enemy 1";
        levelText.text = "Lvl " + DataManager.level;
        hpBar.SetHp((float)100 / 100);
    }

    public void UpdateHP(int newHP, int maxHP)
    {
        hpBar.SetHp((float)newHP / maxHP);
    }

}
