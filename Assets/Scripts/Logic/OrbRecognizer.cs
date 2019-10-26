using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRecognizer : MonoBehaviour
{
    public PlayerController playerController = new PlayerController();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.GetComponent<Collectible>())
        {
            Debug.Log("Collectible found! " + other.gameObject.name);
            if (other.gameObject.GetComponent<LightOrb>() != null)
            {
                playerController.EatOrb(other.gameObject);
            }
        }
    }
}
