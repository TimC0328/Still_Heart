using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum States { DEFAULT, FREEZE, READY, INTERACT, CUTSCENE, INVENTORY, DAMAGE, FPS}
    private States state = States.DEFAULT;

    private CharacterController controller;
    private float speed = 2.0f;
    private float turnSpeed = 120.0f;

    private int health = 100;

    private CameraSystem camSystem;

    private CombatSystem combatSystem;

    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camSystem = GetComponent<CameraSystem>();
        combatSystem = GetComponent<CombatSystem>();

        //Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.OnPlayerSpawned(this);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (state == States.FREEZE)
            return;

        HandleMovement();
        HandleCombat();
        HandleInteraction();
        HandleInventory();
        HandleMoveAnimation();
        HandleFPS();
    }

    private void HandleMovement()
    {
        Vector3 moveDir;

        if (state != States.DEFAULT)
            return;
        
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

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

        if (combatSystem.GetCurrentWeapon().name == "None")
            return;

        if (Input.GetMouseButtonDown(1))
        {
            camSystem.ToggleView();
            Cursor.lockState = CursorLockMode.Locked;

            combatSystem.ToggleWeapon();

            animator.SetBool("isAiming", true);
        }

        if (Input.GetMouseButton(1))
        {
            state = States.READY;

            combatSystem.TakeAim();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            camSystem.ToggleView();
            combatSystem.ToggleWeapon();
            combatSystem.ResetGun();
            state = States.DEFAULT;
            Cursor.lockState = CursorLockMode.Confined;

            animator.SetBool("isAiming", false);
            
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

    private void HandleInventory()
    {
        if (state == States.CUTSCENE || state == States.INTERACT || state == States.DAMAGE)
            return;

        if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            camSystem.InventoryCameraToggle();
            if (Inventory.Instance.ToggleUI())
                SetStateLocal(States.INVENTORY);
            else 
                SetStateLocal(States.DEFAULT);
        }
    }

    private void HandleMoveAnimation()
    {
        if (state == States.DEFAULT)
        { 
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void HandleFPS()
    {
        if (state != States.DEFAULT && state != States.FPS)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl))
            FPSMode();

        if (state == States.FPS)
            combatSystem.UpdateFPS();
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
        if((States) state == States.READY && (States) newState == States.FREEZE)
        {
            camSystem.ToggleView();
            combatSystem.ToggleWeapon();
            combatSystem.ResetGun();
            Cursor.lockState = CursorLockMode.Confined;
        }
        state = (States)newState;

        ReturnToDefaultAnim();
    }
    
    private void SetStateLocal(States newState)
    {
        if (state == States.READY && (States)newState == States.FREEZE)
        {
            camSystem.ToggleView();
            combatSystem.ToggleWeapon();
            combatSystem.ResetGun();
            Cursor.lockState = CursorLockMode.Confined;
        }
        state = newState;

        ReturnToDefaultAnim();
    }

    private void FPSMode()
    {
        if (state == States.DEFAULT)
            state = States.FPS;
        else if (state == States.FPS)
        {
            state = States.DEFAULT;
            combatSystem.ResetGun();
        }
        else
            return;
        
       camSystem.ToggleViewFPS();

    }

    /*void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.forward * 0.5f + transform.position, .75f);
    }*/

    private void ReturnToDefaultAnim()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
    }

}
