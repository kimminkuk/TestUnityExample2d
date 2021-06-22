using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState 
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;

    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private bool DeathCheck;
    private float deathEffectDelay = 1f;
    private void Awake()
    {
        health = maxHealth.intialValue;
    }

    private void TakeDamge(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
            DeathCheck = true;
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        if (!DeathCheck) // StartCoroutine Error... maybe Attack Twice, gameobejct destroy but attack again
        {
            StartCoroutine(KnockCo(myRigidbody, knockTime));
            TakeDamge(damage);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
