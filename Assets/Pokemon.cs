using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Propertyset
{
    Metal = 0,  // 金
    Wood = 1,   // 木
    Earth = 2,  // 土
    Water = 3,  // 水
    Fire = 4    // 火
}
public class Pokemon : MonoBehaviour
{
    public string pokemonName;
    public int pokemonLevel;
    public int atk;
    public int def;
    public int spd;
    public int maxHP;
    public int currentHP;
    public Propertyset property;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int  TakeDamage(int atk, int def,Propertyset atk_property,Propertyset def_property)
    {
        int attackerPropValue = (int)atk_property;
        int defenderPropValue = (int)def_property;

        // 计算属性相克关系
        // 相克规则: Metal(0) > Wood(1) > Earth(2) > Water(3) > Fire(4) > Metal(0)
        bool isResisted = (attackerPropValue == (defenderPropValue + 1) % 5);
        bool isEffective = (defenderPropValue == (attackerPropValue + 1) % 5);

        int dmg = 0;
        // 计算属性相克关系 (使用取模运算形成循环相克)
        if (isEffective)
        {
            dmg = 2 * atk - def; // 攻击属性克制防御属性，双倍伤害
        }
        else if (isResisted)
        {
            dmg = Mathf.Max(1, atk / 2 - def); // 攻击属性被克制，减半伤害(至少1点)
        }
        else
        {
            dmg = atk - def; // 无属性相克
        }

        if (dmg < 0)
            dmg = 0;
        return dmg;
    }

    public void Heal(int HealAmount)
    {
        currentHP += HealAmount;
        if (currentHP >= maxHP)
            currentHP = maxHP;
    }

    public bool isDefeated(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
