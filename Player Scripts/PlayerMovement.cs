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
    public ButtonHandler_B buttonHandler_B;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;
    public Signal reduceMagic;

    [Header("IFrame stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int number0fFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    [Header("Projectile Stuff")]
    public GameObject projectile;
    public Item bow;

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
#if UNITY_ANDROID //for Mobile
        speed = 3.5f;
#else //for pc
        speed = 15f;
#endif

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID //for Mobile
        if (currentState == PlayerState.interact)
        {
            return;
        }
        if (buttonHandler.attackbutton && currentState != PlayerState.attack 
            && currentState != PlayerState.stagger)
        {
            //Attack Action with Not Move Motion
            myRigidbody.velocity = Vector2.zero;
            StartCoroutine(AttackCo());
        }
        else if (buttonHandler_B.attackbutton_B && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            //Attack Action with Not Move Motion
            if (playerInventory.CheckForItem(bow)) 
            {
                myRigidbody.velocity = Vector2.zero;
                StartCoroutine(SecondAttackCo());
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
                if (movementJoyStick.joystickVec.y != 0)
                {
                    myRigidbody.velocity = new Vector2(movementJoyStick.joystickVec.x * speed, movementJoyStick.joystickVec.y * speed);
                    if (myRigidbody.velocity.x >= 0.4f)
                    {
                        joystickDirection_x = 1;
                    }
                    else if (myRigidbody.velocity.x <= -0.4f)
                    {
                        joystickDirection_x = -1;
                    }
                    else
                    {
                        joystickDirection_x = 0;
                    }
                    if (myRigidbody.velocity.y >= 0.95f)
                    {
                        joystickDirection_y = 1;
                    }
                    else if (myRigidbody.velocity.y <= -0.95f)
                    {
                        joystickDirection_y = -1;
                    }
                    else
                    {
                        joystickDirection_y = 0;
                    }
                }
                else
                {
                    myRigidbody.velocity = Vector2.zero;
                }
                UpdateAnimationAndMove_Mobile();
        }
#else //for pc
        //Is the player in the interaction
        if (currentState == PlayerState.interact)
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
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(bow))
            {
                StartCoroutine(SecondAttackCo());
            }
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

    private IEnumerator AttackCo_Mobile()
    {
        if (currentState != PlayerState.interact)
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
    }

    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            reduceMagic.Raise();
        }
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                Debug.Log("RaiseItem 1");
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                Debug.Log("RaiseItem 2");
                if (currentState == PlayerState.walk)
                {
                    Debug.Log("PlayerState.walk)\n");
                }
                else if (currentState == PlayerState.attack)
                {
                    Debug.Log("PlayerState.attack\n");
                }
                else if (currentState == PlayerState.interact)
                {
                    Debug.Log("PlayerState.interact\n");
                }
                else if (currentState == PlayerState.stagger)
                {
                    Debug.Log("PlayerState.stagger\n");
                }
                else
                {
                    Debug.Log("PlayerState.idle\n");
                }
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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void UpdateAnimationAndMove_Mobile()
    {
        if (myRigidbody.velocity != Vector2.zero)
        {
            joystickDirection_x = Mathf.Round(joystickDirection_x);
            joystickDirection_y = Mathf.Round(joystickDirection_y);
            animator.SetFloat("moveX", joystickDirection_x);
            animator.SetFloat("moveY", joystickDirection_y);
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
        playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < number0fFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
