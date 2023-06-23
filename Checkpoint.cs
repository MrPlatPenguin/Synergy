using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform lightLocation, darkLocation;
    [SerializeField] bool lightTrigger, darkTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("CharacterOne") && lightTrigger) 
            || other.gameObject.CompareTag("CharacterOne") && darkTrigger)
        {
            GameManager.SaveCheckpoint(lightLocation, darkLocation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(lightLocation.position, 1f);
        Gizmos.DrawWireSphere(darkLocation.position, 1f);
    }
}
