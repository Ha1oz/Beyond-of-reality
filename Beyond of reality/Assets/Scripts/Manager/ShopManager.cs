using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static int SBCounter,SBSaver;
    //public PlayerConfiguration config;
    public void BuyO2(){
        //Debug.Log("Balance: "+PlayerCharacteristics.balance+"   02 Cylenders: "+PlayerCharacteristics.O2Cylenders);
        if(PlayerCharacteristics.balance>=15f){
            PlayerCharacteristics.balance-=15;
            PlayerCharacteristics.O2Cylenders++;

            CounterForStatistics(15);
            
            GameManager.Instance.UpdateScore(PlayerCharacteristics.balance);
            GameManager.Instance.UpdateCylenders(PlayerCharacteristics.O2Cylenders);    
        }
    }
    public void BuyPower(){
        if(PlayerCharacteristics.balance>=50f){
            PlayerCharacteristics.balance-=50;
            PlayerCharacteristics.superJump = 1;

            CounterForStatistics(50);

            GameManager.Instance.UpdateScore(PlayerCharacteristics.balance);
            GameManager.Instance.UpdateSuperJumpBar(PlayerCharacteristics.superJump,false);
        }
    }
    public void BuyJumpForce(){
        if(PlayerCharacteristics.balance>=100f){
            PlayerCharacteristics.balance-=100f;
            PlayerMovement.jumpForce +=0.5f;

            CounterForStatistics(100);

            GameManager.Instance.UpdateScore(PlayerCharacteristics.balance);
            GameManager.Instance.UpdateJumpForce(PlayerMovement.jumpForce+0.5f);
        }
    }
    public void BuySpeed(){
        if(PlayerCharacteristics.balance>=150f){
            PlayerCharacteristics.balance-=150f;
            PlayerMovement.moveSpeed +=0.5f;

            CounterForStatistics(150);

            GameManager.Instance.UpdateScore(PlayerCharacteristics.balance);
            GameManager.Instance.UpdateSpeed(PlayerMovement.moveSpeed+0.5f);
        }
    }

    private void CounterForStatistics(int forStat){
        SBCounter+=forStat;     
        SBSaver = PlayerPrefs.GetInt("StatisticsOfSpentBalance");
        SBSaver+=forStat;

        PlayerPrefs.SetInt("StatisticsOfSpentBalance", SBSaver);
    }

}
