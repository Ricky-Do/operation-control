using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour{

  private static int points = 0;
  private static int health = 50;

  // Start is called before the first frame update
  void Start(){

  }

  // Update is called once per frame
  void Update(){
      
  }

  public static void AddPoints(int addPoints){
  points += addPoints;
	}

	public static int GetPoints(){
		return points;
	}

  public static void SetPoints(int setPoints){
    points = setPoints;
  }

  public static int GetHealth(){
    return health;
	}
    
	public static void UpdateHealth(int update){
		health += update;
	}

  public static void SetHealth(int setHealth){
    health = setHealth;
  }
}
