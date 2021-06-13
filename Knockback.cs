using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    [SerializeField]
    private float thrust;

    public float knockTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("knockback onTriggerEnter2D\n");
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("knockback enemy\n");
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
                //StartCoroutine(KnockCoroutine(enemy));


                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);

        enemy.velocity = new Vector2();
        enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
    }
}
