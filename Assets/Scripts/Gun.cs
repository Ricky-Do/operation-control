using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private Transform bulletSpawnPoint;
   private float nextFireTime = 0f;
   [SerializeField] private float fireRate;
   [SerializeField] private static int magazineSize = 30;
   [SerializeField] private static int currentAmmo;
   [SerializeField] private float reloadTime = 2f;
   private static bool isReloading = false;
   private static string reloadStatus;
   
   private void Start(){
    currentAmmo = magazineSize;
   }

    // Update is called once per frame
    private void Update(){
        if(isReloading){
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            //Holds Update function and waits until Reload function is done
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetKey(KeyCode.Mouse0) && currentAmmo > 0){
            if(Time.time > nextFireTime){
                Shoot();
                reloadStatus = "Press R to Reload";
            }
        }
    }

    //Shoot the gun and decrement the amount of ammo
    private void Shoot(){
        currentAmmo--;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // Set the next time the player can fire
        nextFireTime = Time.time + 1.0f / fireRate;
    }

    //Reload gun after certain amount of seconds
    IEnumerator Reload(){
        isReloading = true;
        reloadStatus = "Reloading...";
        Debug.Log("Reloading...");

        // Simulate the reload time
        yield return new WaitForSeconds(reloadTime);

        // Refill the magazine
        currentAmmo = magazineSize;

        reloadStatus = "Reloaded";
        Debug.Log("Reloaded!");
        isReloading = false;
    }

    public static int GetCurrentAmmo(){
        return currentAmmo;
    }

    public static int GetMagazineSize(){
        return magazineSize;
    }
    
    public static string GetReloadStatus(){
        return reloadStatus;
    }
}
