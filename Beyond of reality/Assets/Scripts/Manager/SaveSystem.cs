using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveSystem
{
    public float[] positions;

    public void SavePosition(PlayerInLobb player){
        positions = new float[3];
        positions[0] = player.transform.position.x;
        positions[1] = 15f;
        positions[2] = player.transform.position.z;

        PlayerPrefs.SetFloat("PosX",positions[0]);
        PlayerPrefs.SetFloat("PosY",positions[1]);
        PlayerPrefs.SetFloat("PosZ",positions[2]);

        PlayerPrefs.Save();
    }

    public Vector3 LoadPosition(){
        return new Vector3(PlayerPrefs.GetFloat("PosX"),PlayerPrefs.GetFloat("PosY"),PlayerPrefs.GetFloat("PosZ"));
    }

}
