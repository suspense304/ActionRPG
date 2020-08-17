#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    [Header("State Machine")]
    [SerializeField] public PlayerState currentState;

    [Header("Movement")]
    [SerializeField] float speed;

    [Header("Attack Variables")]
    [SerializeField] public float attackBuffer;
    [SerializeField] float attackDelay;
    [SerializeField] float attackMovement;
    [SerializeField] float comboWindow;
    [SerializeField] int specialCost;

    [Header("Booleans")]
    [SerializeField] bool isAttacking;

    [Header("References")]
    [SerializeField] public Inventory inventory;
    [SerializeField] public PlayerStats player;
    [SerializeField] public SignalSender playerHealthSignal;
    [SerializeField] public SpriteRenderer newItemSprite;
    [SerializeField] public VectorValue startingPosition;
    [SerializeField] public Vector2 testPosition;

    Animator anim;
    Rigidbody2D rb;
    Vector3 change;
    float attackTimer;
    float comboTimer;
    int attackCount = 0;
    

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);

        if (startingPosition.initialPosition == Vector2.zero)
        {
            transform.position = testPosition;
        }
        else
        {
            transform.position = startingPosition.initialPosition;
        }
        attackTimer = attackBuffer;
    }

    void Update()
    {

        if (currentState == PlayerState.interact) return;

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");


        if (Input.GetButtonDown("spin") && currentState != PlayerState.attack)
        {
            StartCoroutine(ExecuteSpinAttack());
        }

        if (Input.GetButtonDown("attack"))
        {
            isAttacking = true;
            attackTimer = attackBuffer;
        }

        if(attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer <= 0)
        {
            isAttacking = false;
            attackTimer = 0;
        }

        if(comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }

        if (comboTimer <= 0)
        {
            attackCount = 0;
        }

        if (isAttacking && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(ExecuteAttack());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationsMove();
        }
    }

    public void RaiseItem()
    {
        anim = GetComponent<Animator>();

        if (inventory.currentItem == null) return;

        if(currentState != PlayerState.interact)
        {
            anim.SetBool("isItem", true);
            PlayerSoundManager.instance.PauseThemeMusic();
            PlayerSoundManager.instance.PlayItemFound();
            currentState = PlayerState.interact;
            newItemSprite.sprite = inventory.currentItem.itemSprite;
        }
        else
        {
            anim.SetBool("isItem", false);
            currentState = PlayerState.idle;
            newItemSprite.sprite = null;
            inventory.currentItem = null;
            PlayerSoundManager.instance.PlayThemeMusic();
        }
        
    }

    IEnumerator ExecuteAttack()
    {
        if (attackCount == 0)
        {
            anim.SetTrigger("Attack1");
            currentState = PlayerState.attack;
            PlayerSoundManager.instance.PlayAttackSound();
            yield return new WaitForSeconds(attackDelay);

            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
            attackCount++;
            comboTimer = comboWindow;
        }
        else if (comboWindow > 0 && attackCount == 1)
        {
            anim.SetTrigger("Attack2");
            currentState = PlayerState.attack;
            PlayerSoundManager.instance.PlayAttackSound();
            yield return new WaitForSeconds(attackDelay);

            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
            attackCount = 0;
        }
        
    }

    IEnumerator ExecuteSpinAttack()
    {
        if(player.CurrentMana >= specialCost)
        {
            player.UseAbilityPower(specialCost);
            anim.SetTrigger("SpinAttack");
            currentState = PlayerState.attack;
            PlayerSoundManager.instance.PlaySpinAttackSound();

            yield return new WaitForSeconds(attackDelay);
            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
        } else
        {
            Debug.Log("NO MANA");
        }

        
    }

    void UpdateAnimationsMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        change = change.normalized;
        rb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
    public void Knockback(float knockbackDuration, int damage)
    {
        StartCoroutine(KnockbackWait(knockbackDuration));
        player.TakeDamage(damage);
    }
    IEnumerator KnockbackWait(float knockbackDuration)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
