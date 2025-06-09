using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState {
    Start,PlayerTurn,EnemyTurn,Win,Lost
}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject PlayerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBatteleStation;
    public Transform emenyBattleStation;
    public TextMeshProUGUI dialogText;

    private Pokemon playerPokemon;
    private Pokemon enemyPokemon;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        state= BattleState.Start;
        StartCoroutine( SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SetupBattle()
    {
        GameObject enemy = Instantiate(enemyPrefab, emenyBattleStation);
        enemyPokemon = enemy.GetComponent<Pokemon>();

        GameObject player = Instantiate(PlayerPrefab,playerBatteleStation);
        playerPokemon = player.GetComponent<Pokemon>();

        dialogText.text = "Ұ����" +  enemyPokemon.pokemonName + "��������";

        playerHUD.SetHUD(playerPokemon);
        enemyHUD.SetHUD(enemyPokemon);

        yield return new WaitForSeconds(3f);
        if(enemyPokemon.spd > playerPokemon.spd)
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    private void PlayerTurn()
    {
        dialogText.text = playerPokemon.pokemonName + "��ʲô";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn)
            return;
        StartCoroutine(PlayerAttack());
    }

    private IEnumerator PlayerAttack()
    {
        dialogText.text = playerPokemon.pokemonName + "ʹ���˹�����";
        yield return new WaitForSeconds(3f);
        int dmg = enemyPokemon.TakeDamage(playerPokemon.atk, enemyPokemon.def,playerPokemon.property,enemyPokemon.property);
        dialogText.text = enemyPokemon.pokemonName + "�ܵ���" + dmg + "���˺�";
        bool isDefeated = enemyPokemon.isDefeated(dmg);
        enemyHUD.SetHP(enemyPokemon.currentHP);
        yield return new WaitForSeconds(3f);
        if (isDefeated)
        {
            state = BattleState.Win;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.Win)
        {
            dialogText.text = playerPokemon.pokemonName + "Ӯ����ս����";
        }
        else
        {
            dialogText.text = playerPokemon.pokemonName + "�������ˣ�";
        }
    }

    private IEnumerator EnemyTurn()
    {
        dialogText.text = enemyPokemon.pokemonName + "ʹ���˹�����";
        yield return new WaitForSeconds(2f);
        int dmg = playerPokemon.TakeDamage(enemyPokemon.atk, playerPokemon.def, enemyPokemon.property, playerPokemon.property);
        dialogText.text = playerPokemon.pokemonName + "�ܵ���" + dmg + "���˺�";
        bool isDefeated = playerPokemon.isDefeated(dmg);
        playerHUD.SetHP(playerPokemon.currentHP);
        yield return new WaitForSeconds(2f);
        if (isDefeated)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    public void OnHealButton()
    {
        if (state != BattleState.PlayerTurn)
            return;
        StartCoroutine(PlayerHeal());
    }

    private IEnumerator PlayerHeal()
    {
        playerPokemon.Heal(playerPokemon.pokemonLevel);
        playerHUD.SetHP(playerPokemon.currentHP);
        dialogText.text = playerPokemon.pokemonName + "ʹ�������ƣ�";
        yield return new WaitForSeconds(1f);
        dialogText.text = playerPokemon.pokemonName + "�������ָ��ˣ�";
        yield return new WaitForSeconds(2f);
        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }
}
