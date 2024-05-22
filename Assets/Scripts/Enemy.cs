using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float shootingRange;
    [SerializeField] private float nextFireTime;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float detectionRange;
    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthBarSlider;
    private Transform objective;
    private static int points;
    private int currentHealth;
    private int maxHealth = 20;
    private bool onObjective;
    private Vector3 healthBarOffset = new Vector3(0, 2.5f, 0);
    private GameObject healthBar;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        // objective = GameObject.Find("Objective").transform;
        objective = GameObject.Find("=================MAP=====================").transform.Find("Objective").transform;

        // Instantiate the health bar prefab
        healthBar = Instantiate(healthBarPrefab, transform.position + healthBarOffset, transform.rotation);
        healthBar.transform.SetParent(transform);

        // Access the Slider component on the health bar prefab
        healthBarSlider = healthBar.GetComponentInChildren<Slider>();

        navMeshAgent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;
        healthBar.transform.LookAt(player);

        if(distanceToPlayer <= detectionRange){
            // Move towards the player.
            navMeshAgent.SetDestination(player.position);
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 2f * Time.deltaTime);       
        }
        else{
            navMeshAgent.SetDestination(objective.position);
        }



        // Check if the enemy can fire and is within range
        if (Time.time > nextFireTime && distanceToPlayer < shootingRange)
        {
            // Set the next time the enemy can fire
            nextFireTime = Time.time + 1.0f / fireRate;

            // Instantiate a projectile at the enemy's position and make it move towards the player
            /*GameObject bullet = */Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Vector3 direction = player.position - bulletSpawnPoint.position;
            GetComponent<Rigidbody>().velocity = direction * Time.deltaTime;
        }

        // Update the health bar value
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
            if(currentHealth <= 0){
                if(onObjective == true){
                    Objective.UpdateEnemiesOnObjective(-1);
                    onObjective = false;
                }
                Destroy(gameObject); 
            }
        }
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
    public int GetHealth(){
        return currentHealth;
	}
    
	public void UpdateHealth(int update){
		currentHealth += update;
	}

    public void SetHealth(int setHealth){
        currentHealth = setHealth;
    }

    public void SetOnObjective(bool setOnObjective){
        onObjective = setOnObjective;
    }
}
