using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterManager CM;
    CharacterController controller;
    Vector3 velocity;

    Vector3 direction;

    bool _isGrounded = true;

    Vector3 _warpPos;
    [HideInInspector] public bool warp;

    Animator anim;

    [SerializeField] bool _isControlled;

    [SerializeField] GameObject spotLight;

    private void Start()
    {
        CM = FindObjectOfType<CharacterManager>();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        isControlled = isControlled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change") && !gameObject.CompareTag("JoinedCharacter"))
            isControlled = !isControlled;

        float x = 0;
        float y = 0;
        if (isControlled)
        {
            x = Input.GetAxisRaw("Vertical");
            y = -Input.GetAxisRaw("Horizontal");
        }

        direction = new Vector3(x, 0, y).normalized;

        Collider[] cols = Physics.OverlapSphere(transform.position, controller.radius * transform.localScale.x);
        bool groundFound = false;
        foreach (Collider col in cols)
        {
            if (CM.groundLayers == (CM.groundLayers | (1 << col.gameObject.layer)))
            {
                groundFound = true;
                break;
            }
        }
        isGrounded = groundFound;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isControlled && Input.GetButtonDown("Jump") && isGrounded)
        {
            Launch(Vector3.up, CM.jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    

    private void FixedUpdate()
    {
        //transform.position += (direction * CM.moveSpeed * Time.deltaTime) + (velocity * Time.deltaTime);
        transform.forward = direction.magnitude > 0 ? direction : transform.forward;

        if (direction.magnitude > 0)
            anim.SetBool("isWalking",true);
        else
            anim.SetBool("isWalking", false);

        controller.Move(direction * CM.moveSpeed * Time.deltaTime);

        velocity.y -= CM.gravity * Time.deltaTime;



        controller.Move(velocity * Time.deltaTime);

        if (warp)
        {
            transform.position = warpPos;
        }
    }

    public void Launch(Vector3 direction, float force)
    {
        velocity += direction.normalized * force;
    }

    bool isGrounded
    {
        get { return _isGrounded; }
        set
        {
            if (!_isGrounded && value)
                anim.SetTrigger("Land");
            anim.SetBool("isGrounded", value);
            _isGrounded = value;
        }
    }

    public Vector3 warpPos
    {
        get
        {
            warp = false;
            return _warpPos;
        }
        set
        {
            warp = true;
            _warpPos = value;
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, controller.radius * transform.localScale.x);
    }

    public bool isControlled
    {
        get { return _isControlled; }
        set 
        {
            if (spotLight != null) spotLight.SetActive(value);
            anim.SetBool("IsInactive", !value);
            _isControlled = value;
        }
    }
}
