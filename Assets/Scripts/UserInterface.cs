using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private TextMeshProUGUI objectiveStatus;
    [SerializeField] private TextMeshProUGUI ammoStatus;
    [SerializeField] private TextMeshProUGUI reloadStatus;
    [SerializeField] private TextMeshProUGUI enemyScore;
    private static string objectiveStatusText;

    private void Update()
    {
        // Update the UI Text components with player variables.
        scoreText.text = "Score: " + PlayerInfo.GetPoints();
        playerHealth.text = "Health: " + PlayerInfo.GetHealth();
        objectiveStatus.text = "Objective: " + objectiveStatusText;
        ammoStatus.text = "Ammo: " + Gun.GetCurrentAmmo() + " / " + Gun.GetMagazineSize();
        reloadStatus.text = Gun.GetReloadStatus();
        enemyScore.text = "Enemy Score: " + Enemy.GetPoints();
        SetObjectiveColor(objectiveStatusText);
    }
    
    public static void SetObjectiveStatus(string status){
        objectiveStatusText = status;
    }

    public void SetObjectiveColor(string status){
        if(status == "Neutral"){
            objectiveStatus.color = Color.white;
        }
        else if(status == "Player securing"){
            objectiveStatus.color = Color.green;
        }
        else if(status == "Enemy securing"){
            objectiveStatus.color = Color.red;
        }
        else if(status == "Contested"){
            objectiveStatus.color = Color.yellow;
        }
    }
}
