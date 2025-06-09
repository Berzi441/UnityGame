using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleHUD : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lvText;
    public Slider HPSlider;
    public Slider sliderBuffer;

    private void Update()
    {
        sliderBuffer.value = Mathf.MoveTowards(sliderBuffer.value, HPSlider.value, 0.02f);
        //if (sliderBuffer.value > HPSlider.value)
        //	sliderBuffer.value -= 0.02f;
        //else if (sliderBuffer.value < HPSlider.value)
        //	sliderBuffer.value += 0.02f;
    }
    public void SetHUD(Pokemon pm)
    {
        nameText.text = pm.pokemonName;
        lvText.text = pm.pokemonLevel.ToString();
        HPSlider.maxValue = pm.maxHP;
        HPSlider.value = pm.currentHP;
        sliderBuffer.maxValue = pm.maxHP;
        sliderBuffer.value = pm.currentHP;
    }

    public void SetHP(int hp)
    {
        HPSlider.value = hp;
    }

}
