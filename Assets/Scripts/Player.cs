using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum States { DEFAULT, FREEZE, READY, INTERACT }
    private States state = States.DEFAULT;

    private CharacterController controller;
    private float speed = 2.0f;
    private float turnSpeed = 120.0f;

    private int health = 100;

    private CameraSystem camSystem;

    private CombatSystem combatSystem;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camSystem = GetComponent<CameraSystem>();
        combatSystem = GetComponent<CombatSystem>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    private void Update()
    {
        if (state == States.FREEZE)
            return;

        HandleMovement();
        HandleCombat();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        Vector3 moveDir;

        if (state == States.DEFAULT || state == States.READY)
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        if (state != States.DEFAULT)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 5f;
        else
            speed = 2f;


        moveDir = transform.forward * Input.GetAxis("Vertical") * speed;
        // moves the character in horizontal direction

        controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
    }

    private void HandleCombat()
    {
        if (state != States.DEFAULT && state != States.READY)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            camSystem.ToggleView();
            combatSystem.ToggleLaser();
            Cursor.lockState = CursorLockMode.Locked;

        }

        if (Input.GetMouseButton(1))
        {
            state = States.READY;

            combatSystem.TakeAim();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            camSystem.ToggleView();
            combatSystem.ToggleLaser();
            combatSystem.ResetGun();
            state = States.DEFAULT;
            Cursor.lockState = CursorLockMode.Confined;
            
        }
    }

    private void HandleInteraction()
    {
        if (state != States.DEFAULT)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable interactable = getInteractable();
            if (interactable)
                interactable.Interact();
        }
    }

    public Interactable getInteractable()
    {
        int layerMask = 1 << 9; //Interactables are on layer 9

        Collider[] hitColliders = Physics.OverlapSphere(transform.forward * 0.5f + transform.position, .75f, layerMask);

        if (hitColliders.Length == 0)
            return null;
        else if (hitColliders.Length == 1)
        {
            return hitColliders[0].gameObject.GetComponent<Interactable>();
        }
        else
        {
            // TODO: Add in get closest check
            return null;
        }
    }

    public void Damage(int damage)
    {
        Debug.Log("Player has taken " + damage + " damage");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetState(int newState)
    {
        if(state == States.READY && (States) newState == States.FREEZE)
        {
            camSystem.ToggleView();
            combatSystem.ToggleLaser();
            combatSystem.ResetGun();
            Cursor.lockState = CursorLockMode.Confined;
        }
        state = (States)newState;
    }

    /*void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.forward * 0.5f + transform.position, .75f);
    }*/

}
