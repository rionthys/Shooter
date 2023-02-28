using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    EnemyAI[] enemys;
    void Start()
    {
    }

    void Update()
    {
        enemys = GetComponentsInChildren<EnemyAI>();
        bool dead = true;

        foreach(EnemyAI enemy in enemys){
            if(!enemy.isDead) dead = false;
        }
        if(dead){
            SceneManager.LoadScene("WinLose");
            int loadedNumber = PlayerPrefs.GetInt("Win");
            PlayerPrefs.SetInt("Win", loadedNumber + 1);
            PlayerPrefs.Save();
        }
    }


    public void ReStart(){
        SceneManager.LoadScene("Game");
    }

    public void Exit(){
         Application.Quit();
    }
}
