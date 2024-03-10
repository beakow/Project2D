using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Player;
    public float force;
    private float timer;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("TurtlePlayer");
        rb = GetComponent<Rigidbody2D>();
        if(Player.transform.localScale.x  > 0)
        {
            rb.velocity = transform.right * force;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            rb.velocity = -transform.right * force;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

}
