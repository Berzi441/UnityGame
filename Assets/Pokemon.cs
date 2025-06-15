using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Propertyset
{
    Lightning = 0,  // 电
    Wood = 1,   // 木
    Ice = 2,  // 冰
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
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        // 相克规则: Lightning(0) > Wood(1) > Ice(2) > Water(3) > Fire(4) > Lightning(0)
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

    public void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void PlayIdleAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
