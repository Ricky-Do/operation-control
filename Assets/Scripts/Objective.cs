using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    private static bool playerOnObjective;
    private static int enemiesOnObjective = 0;
    private float timeOnObjective;
    private float scoreCooldown = 3.0f;
    [SerializeField] private Material controllingFlag;
    [SerializeField] private Material neutralFlag;
    [SerializeField] private Material enemyFlag;
    [SerializeField] private Material contestedFlag;
    private GameObject flag;
  

    private void Start(){
        flag = GameObject.Find("Flag");
    }

    private void Update(){
        SecuringObjective();
        ChangeFlagColor();
        GameResult();
    }

    //Detect when the player and enemies enter the objective radius
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            //Debug.Log("Player entered objective");
            playerOnObjective = true;
        }
        if(other.CompareTag("Enemy")){
            //Debug.Log("Enemy entered objective");
            Enemy enemy = other.transform.gameObject.GetComponent<Enemy>();
            enemy.SetOnObjective(true);
            enemiesOnObjective += 1;
        }
    }

    //Detect when the player and enemies leave the objective radius
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            //Debug.Log("Player left objective");
            playerOnObjective = false;
            timeOnObjective = 0f;
        }
        if(other.CompareTag("Enemy")){
            Enemy enemy = other.transform.gameObject.GetComponent<Enemy>();
            enemy.SetOnObjective(false);
            //Debug.Log("Enemy left objective");
            enemiesOnObjective -= 1;
            timeOnObjective = 0f;
        }
    }

    //Checks the if player and/or enemies are on the objective and assign objective status
    private void SecuringObjective(){
        if(playerOnObjective == true && enemiesOnObjective == 0){
            timeOnObjective += Time.deltaTime;
            if(timeOnObjective >= scoreCooldown){
                PlayerInfo.AddPoints(5);
                timeOnObjective = 0.0f;
            }
            UserInterface.SetObjectiveStatus("Player securing");
        }
        else if(playerOnObjective == false && enemiesOnObjective >= 1){
            timeOnObjective += Time.deltaTime;
            if(timeOnObjective >= scoreCooldown){
                Enemy.AddPoints(5);
                timeOnObjective = 0.0f;
                Debug.LogFormat("Enemy Points: {0}", Enemy.GetPoints());
            }
            UserInterface.SetObjectiveStatus("Enemy securing");
        }
        else if(playerOnObjective == true && enemiesOnObjective >= 1){
            timeOnObjective = 0.0f;
            Debug.Log("Objective contested");
            UserInterface.SetObjectiveStatus("Contested");
        }
        else{
            enemiesOnObjective = 0;
            playerOnObjective = false;
            UserInterface.SetObjectiveStatus("Neutral");
        }
    }

    //Change color of flag depending on if the player is standing on the objective or not
    private void ChangeFlagColor(){
        GameObject flag = GameObject.Find("Flag");

        if(playerOnObjective == true && enemiesOnObjective <= 0){
            GetFlagRenderer().material = controllingFlag;
        }
        else if(playerOnObjective == false && enemiesOnObjective <= 0){
            GetFlagRenderer().material = neutralFlag;
        }
        else if(playerOnObjective == false && enemiesOnObjective > 0){
            GetFlagRenderer().material = enemyFlag;
        }
        else if(playerOnObjective == true && enemiesOnObjective > 0 ){
            GetFlagRenderer().material = contestedFlag;
        }
         
    }

    //Get the flag renderer to change the material
    private Renderer GetFlagRenderer(){
        if (flag != null){
            // Access the Renderer component
            Renderer flagRenderer = flag.GetComponent<Renderer>();
            return flagRenderer;
        }
        else{
            Debug.LogError("Game object not found.");
            return null;
        }
    }

    private void GameResult(){
        if(PlayerInfo.GetPoints() >= 100){
            SceneManager.LoadScene("Victory");
        }
        else if(Enemy.GetPoints() >= 100){
            SceneManager.LoadScene("Defeat");
        }
    }

    public static void UpdateEnemiesOnObjective(int updateAmount){
        enemiesOnObjective += updateAmount;
    }
}
