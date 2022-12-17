using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationManager : MonoBehaviour
{
    // refrence for GameObjects and materials to access through the inspector
    [Header("Player Refrence")]
    public GameObject player;

    [Header("Destenation Game Object Refrence")]
    public GameObject destination;

    [Header("Left Hand Materials after and before Teleportation Toggle ")]
    public Material MaterialAfter;
    public Material MaterialBefore;

    [Header("Left Hand Refrence")]
    public GameObject Rhand;

    // refrence for the teleportation destination Visual indicator
    private GameObject currentDestination;

    // Boolean Value to indicate whether the player is in the teleportation mode or not
    [Header("isAiming Boolean")]
    private bool isAiming = false;

    // The layermask the ray will look for
    [Header("Layermask")]
    public LayerMask layermask;

   
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = Instantiate(destination, transform.position, Quaternion.identity);
    }

    // This function will toggle ON/OFF teleportation 
    public void SwitchAiming()
    {
        isAiming = !isAiming;

        if (!isAiming)
        {
            // while not in teleportation mode do not show the teleportation point visual
            currentDestination.SetActive(false);

            // turn Right hand color back to its normal color when exiting teleportation mode
            Rhand.GetComponent<SkinnedMeshRenderer>().material = MaterialBefore;
        }
    }

    // This function will cast a ray to the point where the player should teleport
    private void CheckForDestination()
    {
        Ray ray = new Ray(transform.position, transform.rotation * Vector3.up);

        RaycastHit hit;

        bool b =  Physics.Raycast(ray, out hit, 5 , layermask);

        if (b)
        {
            currentDestination.transform.position = hit.point;
            currentDestination.SetActive(true);
           
        }
    }

    // This function will teleport the player
    public void Teleport()
    {
        if (isAiming && currentDestination.activeSelf)
        {
            player.transform.position = currentDestination.transform.position - new Vector3(0,currentDestination.transform.position.y * 0.48f,0);
            currentDestination.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            // Change Right hand color when switching to teleportation mode
            Rhand.GetComponent<SkinnedMeshRenderer>().material = MaterialAfter;
            CheckForDestination();
        }

        // this line is for debugging pourpses and can be removed safely
        Debug.DrawRay(transform.position, transform.rotation * Vector3.down, Color.green);
    }
}
