using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }

    void Movement(){
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirrection = Input.GetAxisRaw("Horizontal");

        // 角色移动
        rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
        anim.SetFloat("Running", Mathf.Abs(faceDirrection)); // 绝对值
        if(faceDirrection != 0){
            transform.localScale = new Vector3(faceDirrection, 1, 1);
        }

        // 角色跳跃
        if(Input.GetButtonDown("Jump")){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("Jumping", true);
        }
    }

    void SwitchAnim ()
    {
        anim.SetBool("Idling", false);
        if(anim.GetBool("Jumping")){
            // 开始下落
            if(rb.velocity.y < 0){
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        // 和地面接触
        else if(coll.IsTouchingLayers(ground)){
            anim.SetBool("Falling", false);
            anim.SetBool("Idling", true);
        }
    }
}
