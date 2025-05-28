using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState {
    Start,PlayerTurn,EnemyTurn,Win,Lost
}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state= BattleState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
