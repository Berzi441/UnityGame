using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    Start, PlayerTurn, EnemyTurn, Win, Lost
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
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SetupBattle()
    {
        GameObject enemy = Instantiate(enemyPrefab, emenyBattleStation);
        enemyPokemon = enemy.GetComponent<Pokemon>();

        GameObject player = Instantiate(PlayerPrefab, playerBatteleStation);
        playerPokemon = player.GetComponent<Pokemon>();

        //dialogText.text = "野生的" + enemyPokemon.pokemonName + "跳出来了";
        yield return StartCoroutine(TypeDialog("野生的" + enemyPokemon.pokemonName + "跳出来了"));

        playerHUD.SetHUD(playerPokemon);
        enemyHUD.SetHUD(enemyPokemon);

        yield return new WaitForSeconds(1f);
        if (enemyPokemon.spd > playerPokemon.spd)
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
        //dialogText.text = playerPokemon.pokemonName + "做什么";
         StartCoroutine(TypeDialog(playerPokemon.pokemonName + "做什么"));
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn)
            return;
        StartCoroutine(PlayerAttack());
    }

    private IEnumerator PlayerAttack()
    {
        //dialogText.text = playerPokemon.pokemonName + "使用了攻击！";
        yield return StartCoroutine(TypeDialog(playerPokemon.pokemonName + "使用了攻击！"));
        yield return new WaitForSeconds(1f);
        int dmg = enemyPokemon.TakeDamage(playerPokemon.atk, enemyPokemon.def, playerPokemon.property, enemyPokemon.property);
        //dialogText.text = enemyPokemon.pokemonName + "受到了" + dmg + "点伤害";
        yield return StartCoroutine(TypeDialog(enemyPokemon.pokemonName + "受到了" + dmg + "点伤害"));
        bool isDefeated = enemyPokemon.isDefeated(dmg);
        enemyHUD.SetHP(enemyPokemon.currentHP);
        yield return new WaitForSeconds(1f);
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

    private IEnumerator EndBattle()
    {
        if (state == BattleState.Win)
        {
            //dialogText.text = playerPokemon.pokemonName + "赢得了战斗！";
            yield return StartCoroutine(TypeDialog(playerPokemon.pokemonName + "赢得了战斗！"));
        }
        else
        {
            //dialogText.text = playerPokemon.pokemonName + "被击败了！";
            yield return StartCoroutine(TypeDialog(playerPokemon.pokemonName + "被击败了！"));
        }
    }

    private IEnumerator EnemyTurn()
    {
        //dialogText.text = enemyPokemon.pokemonName + "使用了攻击！";
        yield return StartCoroutine(TypeDialog(enemyPokemon.pokemonName + "使用了攻击！"));
        yield return new WaitForSeconds(1f);
        int dmg = playerPokemon.TakeDamage(enemyPokemon.atk, playerPokemon.def, enemyPokemon.property, playerPokemon.property);
        //dialogText.text = playerPokemon.pokemonName + "受到了" + dmg + "点伤害";
        yield return StartCoroutine(TypeDialog(playerPokemon.pokemonName + "受到了" + dmg + "点伤害"));
        bool isDefeated = playerPokemon.isDefeated(dmg);
        playerHUD.SetHP(playerPokemon.currentHP);
        yield return new WaitForSeconds(1f);
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
        //dialogText.text = playerPokemon.pokemonName + "使用了治疗！";
        yield return StartCoroutine(TypeDialog(playerPokemon.pokemonName + "使用了治疗！"));
        yield return new WaitForSeconds(1f);
        //dialogText.text = playerPokemon.pokemonName + "的体力恢复了！";
        yield return StartCoroutine(TypeDialog(playerPokemon.pokemonName + "的体力恢复了！"));
        yield return new WaitForSeconds(1f);
        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }
    private IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";

        foreach (var item in dialog) { 
            dialogText.text += item;
            yield return new WaitForSeconds(1f/30);
        }
    }
}
