using UnityEngine;
using System.Collections;

public class PlayerMelee : MonoBehaviour {

    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCoolDown = 0.3f;

    //public SpriteRenderer spriteRenderer;
    public Collider2D attackTrigger;
    private Animator anim;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        
        attackTrigger.enabled = false; //Start off with collider disabled
        //spriteRenderer.enabled = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !attacking) //GetMouseButtonDown(0, 1, 2) Left, right, middle
        {
            attacking = true;
            attackTimer = attackCoolDown;
            attackTrigger.enabled = true;
            //spriteRenderer.enabled = true;
        }

        if (attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;

            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
                //spriteRenderer.enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
    }
}
