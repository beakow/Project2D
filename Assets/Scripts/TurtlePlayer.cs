using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float Move;
    public float jump;
    public bool isOnGround;
    public bool isFacingRight;
    public bool hasRifle;
    
    public GameObject bullet;
    public Transform bulletPos;
    public Animator anim;
    public GameManager gm;

    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move = Input.GetAxis("TurtleHorizontal");
        rb.velocity = new Vector2(speed * Move, rb.velocity.y);
        if(Input.GetButtonDown("TurtleJump") && isOnGround)
        {
            Debug.Log(Input.GetButtonDown("TurtleJump") == true);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        if(Input.GetButtonDown("TurtleShoot") && hasRifle)
        {
            Shoot();
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
        if(other.gameObject.CompareTag("Rifle"))
        {
            Destroy(other.gameObject);
            hasRifle = true;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
