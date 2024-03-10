using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject player1;
    public GameObject player2;
    public Transform currentPoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = -transform.right * speed;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            if(currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
            else
            {
                currentPoint = pointA.transform;
            }
            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        if(Vector2.Distance(transform.position, player1.transform.position) < 5.0f || Vector2.Distance(transform.position, player2.transform.position) < 5.0f)
        {
            speed = 4;
        }
        else
        {
            speed = 2;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
