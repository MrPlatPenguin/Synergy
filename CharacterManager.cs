using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform characterOne, characterTwo, joinedCharacter;

    public float moveSpeed, jumpForce, joinDistance, gravity;
    [HideInInspector] public float distance;
    [HideInInspector] public bool joined, joinReady;

    public LayerMask groundLayers;

    Vector3 midPoint;


    public float lightValue, darkValue;

    private void Start()
    {
        GetMidPoint();
        GetDistance();
        joinReady = distance > joinDistance;
    }

    // Update is called once per frame
    void Update()
    {
        GetDistance();
    }

    public Vector3 GetMidPoint()
    {
        if (joined)
        {
            midPoint = joinedCharacter.position;
        }
        else
        {
            float xMiddle = (characterOne.position.x - characterTwo.position.x) / 2;
            float yMiddle = (characterOne.position.y - characterTwo.position.y) / 2;
            float zMiddle = (characterOne.position.z - characterTwo.position.z) / 2;
            midPoint = characterTwo.position + new Vector3(xMiddle, yMiddle, zMiddle);
        }
        return midPoint;
    }

    public void Join(Vector3 spawn)
    {
        joined = true;
        joinReady = false;
        joinedCharacter.position = spawn;
        joinedCharacter.gameObject.SetActive(true);
        characterOne.gameObject.SetActive(false);
        characterTwo.gameObject.SetActive(false);
        print("Joined");
    }

    public void Unjoin(Vector3 spawn, float width)
    {
        joined = false;
        characterOne.position = spawn + (Vector3.forward * width);
        characterTwo.position = spawn + (Vector3.back * width);
        joinedCharacter.gameObject.SetActive(false);
        characterOne.gameObject.SetActive(true);
        characterTwo.gameObject.SetActive(true);
        print("Unjoined");

    }

    void GetDistance()
    {
        if (characterOne != null && characterTwo != null)
        {
            distance = Vector3.Distance(characterOne.position, characterTwo.position);
        }
    }
}
