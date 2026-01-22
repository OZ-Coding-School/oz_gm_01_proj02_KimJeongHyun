using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatusUIController : BaseUIController
{
    public Sprite[] hpCard;
    public Image hpImage;
    public Animator hpAni;
    public Image[] meterCard;
    public Animator[] meterAni;
    public Sprite baseCard;

    public PlayerController player;



    protected override void Update()
    {
        float energy = player.PlayerStatus.CurrentEnergy;

        for (int i = 0; i < meterCard.Length; i++)
        {
            float fillVal = Mathf.Clamp01(energy - i);
            meterCard[i].fillAmount = fillVal;

            if (fillVal >= 1f)
            {
                if (!meterAni[i].enabled)
                {
                    meterAni[i].enabled = true;
                    meterAni[i].Play("card", 0, 0);
                }
            }
            else
            {
                meterAni[i].enabled = false;
                meterCard[i].sprite = baseCard;
            }
        }





        if (player.PlayerStatus.CurrentHp == 1)
        {
            if (!hpAni.enabled)
            {
                hpAni.enabled = true;
                hpAni.Play("HP", 0, 0f);
            }
        }
        else
        {
            hpAni.enabled = false;

            if (player.PlayerStatus.CurrentHp == 0)
            {
                hpImage.sprite = hpCard[0];
            }
            else
            {
                hpImage.sprite = hpCard[player.PlayerStatus.CurrentHp];
            }
        }
    }
}
