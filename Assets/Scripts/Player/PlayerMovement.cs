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
    [SerializeField] float speed;
    [SerializeField] float attackDelay;

    bool isAttacking;
    public Inventory inventory;
    public PlayerState currentState;
    public PlayerStats player;
    public SignalSender playerHealthSignal;
    public SpriteRenderer newItemSprite;
    public VectorValue startingPosition;

    Animator anim;
    Rigidbody2D rb;
    Vector3 change;

    public float attackBuffer;
    float attackTimer;
    

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
        transform.position = startingPosition.initialPosition;
        attackTimer = attackBuffer;
    }

    void Update()
    {

        if (currentState == PlayerState.interact) return;

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

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
        anim.SetBool("isAttacking", true);
        currentState = PlayerState.attack;
        PlayerSoundManager.instance.PlayAttackSound();
        yield return null;
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackDelay);   
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
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
