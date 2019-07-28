using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rigidbody2D;
    public bool vertical;
    public float changeTime = 3.0f;

    float timer;
    int direction = 1;

    Animator animator;

    bool broken = true;

    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        timer = changeTime;

        animator = GetComponent<Animator>();
    }

    public void Fix()
    {
        Debug.Log("欧! 天哪 ,我被修理了!");
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
        broken = false;
        rigidbody2D.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {

        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2D.MovePosition(position);            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
