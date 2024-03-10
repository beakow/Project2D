using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float Move;
    public float jump;
    public bool isOnGround;
    public bool isFacingRight;
    public Animator anim;
    public GameManager gm;

    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move = Input.GetAxis("LionHorizontal");
        rb.velocity = new Vector2(speed * Move, rb.velocity.y);
        if(Input.GetButtonDown("LionJump") && isOnGround)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if((Move >= 0.1f || Move <= -0.1f) && isOnGround)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if((!isFacingRight && Move > 0f) || (isFacingRight && Move < 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            anim.SetBool("isJumping", false);
        }        
        if(other.gameObject.CompareTag("JumpPlatform"))
        {
            anim.SetBool("isJumping", true);
            isOnGround = true;
            jump *= 1.5f;
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            isOnGround = true;
            anim.SetBool("isJumping", false);
            gm.ectsCount--;
            anim.SetBool("isHurt", true);
        }
        if(other.gameObject.CompareTag("Doctor"))
        {
            Debug.Log("DOCROR");
            gm.ReloadScene();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
            anim.SetBool("isJumping", true);
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            isOnGround = false;
            anim.SetBool("isHurt", false);
            anim.SetBool("isJumping", true);
        }
        if(other.gameObject.CompareTag("JumpPlatform"))
        {
            anim.SetBool("isJumping", true);
            isOnGround = false;
            jump /= 1.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Book"))
        {
            Destroy(other.gameObject);
            gm.ectsCount+=10;
        }
    }
}

