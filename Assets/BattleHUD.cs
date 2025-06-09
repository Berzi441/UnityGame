using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI lvText;
	public Slider hpSlider;

	public void SetHUD(Pokemon pm)
	{
		nameText.text = pm.pokemonName;
		lvText.text = "Lv" + pm.pokemonLevel;
		hpSlider.maxValue = pm.maxHP;
		hpSlider.value = pm.currentHP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
