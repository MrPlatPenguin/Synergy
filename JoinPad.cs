using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinPad : MonoBehaviour
{
    CharacterManager CM;
    CameraController CC;
    [SerializeField] bool seperate;
    CapsuleCollider col;

    bool darkIn, lightIn;

    [SerializeField] float offset;
    static float oldOffest;

    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        CM = FindObjectOfType<CharacterManager>();
        col = GetComponent<CapsuleCollider>();
        CC = FindObjectOfType<CameraController>();
        oldOffest = CC.camOffest;
    }

    void Update()
    {
        if (particles != null) particles.SetActive(CM.joined == seperate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!seperate)
        {
            if (other.CompareTag("CharacterOne"))
                lightIn = true;
            else if (other.CompareTag("CharacterTwo"))
                darkIn = true;
            if (lightIn && darkIn)
            {
                CC.camOffest = offset;
                CM.Join(transform.position);
                darkIn = false;
                lightIn = false;
            }
        }
        else
        {
            if (other.CompareTag("JoinedCharacter"))
            {
                CC.camOffest = oldOffest;
                CM.Unjoin(transform.position, col.radius / 2);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CharacterOne"))
            lightIn = false;
        else if (other.CompareTag("CharacterTwo"))
            darkIn = false;
    }
}
