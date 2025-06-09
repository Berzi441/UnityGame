using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Propertyset
{
    Metal = 0,  // ��
    Wood = 1,   // ľ
    Earth = 2,  // ��
    Water = 3,  // ˮ
    Fire = 4    // ��
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

        // ����������˹�ϵ
        // ��˹���: Metal(0) > Wood(1) > Earth(2) > Water(3) > Fire(4) > Metal(0)
        bool isResisted = (attackerPropValue == (defenderPropValue + 1) % 5);
        bool isEffective = (defenderPropValue == (attackerPropValue + 1) % 5);

        int dmg = 0;
        // ����������˹�ϵ (ʹ��ȡģ�����γ�ѭ�����)
        if (isEffective)
        {
            dmg = 2 * atk - def; // �������Կ��Ʒ������ԣ�˫���˺�
        }
        else if (isResisted)
        {
            dmg = Mathf.Max(1, atk / 2 - def); // �������Ա����ƣ������˺�(����1��)
        }
        else
        {
            dmg = atk - def; // ���������
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
