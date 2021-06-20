using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public MovementJoyStick movementJoyStick;
    public ButtonHandler buttonHandler;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    private float joystickDirection_x;
    private float joystickDirection_y;
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
#if false //for Mobile
        speed = 5f;
#else //for pc
        speed = 15f;
#endif

    }

    // Update is called once per frame
    void Update()
    {
#if false //for Mobile
        //if(movementJoyStick.joystickVec.y != 0)
        //{
        //    if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        //    {
        //        myRigidbody.velocity = new Vector2(movementJoyStick.joystickVec.x * speed, movementJoyStick.joystickVec.y * speed);
        //
        //        animator.SetFloat("moveX", myRigidbody.velocity.x);
        //        animator.SetFloat("moveY", myRigidbody.velocity.y);
        //        animator.SetBool("moving", true);
        //    }
        //}
        //else
        //{
        //    myRigidbody.velocity = Vector2.zero;
        //    animator.SetBool("moving", false);
        //}

        if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            if(movementJoyStick.joystickVec.y != 0)
            {
                myRigidbody.velocity = new Vector2(movementJoyStick.joystickVec.x * speed, movementJoyStick.joystickVec.y * speed);
                if (myRigidbody.velocity.x >= 0.5f)
                {
                    joystickDirection_x = 1;
                }
                else if (myRigidbody.velocity.x <= -0.5f)
                {
                    joystickDirection_x = -1;
                }
                else
                {
                    joystickDirection_x = 0;
                }
                if (myRigidbody.velocity.y >= 0.5f)
                {
                    joystickDirection_y = 1;
                }
                else if (myRigidbody.velocity.y <= -0.5f)
                {
                    joystickDirection_y = -1;
                }
                else
                {
                    joystickDirection_y = 0;
                }
                animator.SetFloat("moveX", joystickDirection_x);
                animator.SetFloat("moveY", joystickDirection_y);
                animator.SetBool("moving", true);
            }
            else
            {
                myRigidbody.velocity = Vector2.zero;
                animator.SetBool("moving", false);
            }
        }

        if (buttonHandler.attackbutton && currentState != PlayerState.attack 
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        // else if (currentState == PlayerState.walk)
        // {
        //     UpdateAnimationAndMove();
        // }
#else //for pc
        //Is the player in the interaction
        if(currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
#endif
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}