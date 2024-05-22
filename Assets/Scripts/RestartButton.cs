using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour{

    public void Restart(){
        ResetPlayerStats();
        SceneManager.LoadScene("Main");
    }

    public void ResetPlayerStats(){
        PlayerInfo.SetHealth(10);
        PlayerInfo.SetPoints(0);
        Enemy.SetPoints(0);
    }
}
