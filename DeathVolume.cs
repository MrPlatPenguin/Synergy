using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolume : MonoBehaviour
{
    [SerializeField] LayerMask layers;
    BoxCollider warning, death;
    [SerializeField] float warningDistance = 2f;

    int deathValue;
    Hearts hearts;

    GameManager GM;


    private void Start()
    {
        death = GetComponent<BoxCollider>();
        warning = gameObject.AddComponent<BoxCollider>();
        warning.isTrigger = true;
        hearts = FindObjectOfType<Hearts>();
        GM = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        warning.size = death.size + Vector3.one * warningDistance;
        warning.center = death.center;

        if(deathValue >= 2)
        {
            GM.Death();
            //GameManager.ReloadGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (layers == (layers | (1 << other.gameObject.layer)))
        {
            deathValue += 1;

            if (other.CompareTag("CharacterOne") && deathValue == 1)
                hearts.whiteNearDeath = true;
            if (other.CompareTag("CharacterTwo") && deathValue == 1)
                hearts.blackNearDeath = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (layers == (layers | (1 << other.gameObject.layer)))
        {
            deathValue -= 1;

            if (other.CompareTag("CharacterOne") && deathValue == 0)
                hearts.whiteNearDeath = false;
            if (other.CompareTag("CharacterTwo") && deathValue == 0)
                hearts.blackNearDeath = false;
        }
    }
}
