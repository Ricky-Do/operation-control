using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    // Add any other customizable properties for your bullets

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Lifetime management
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Damage the player when bullet hits Player
        if (collision.gameObject.CompareTag("Player")){
            PlayerInfo.UpdateHealth(-2);
            if(PlayerInfo.GetHealth() <= 0){
                SceneManager.LoadScene("Death");
            }
            // Destroy the bullet on collision
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy")){
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.UpdateHealth(-2);
            Destroy(gameObject);
        }
    }
}
