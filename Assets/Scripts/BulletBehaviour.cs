using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed = 500;
    private bool isDestroyOnTime;

    private ObstaclesDB dB;
    private GameObject spinePrefab;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(Random.Range(-1f, 1f), 1) * speed);
        dB = Resources.Load("ObstaclesDB") as ObstaclesDB;
        spinePrefab = Resources.Load("spine") as GameObject;
        //transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        //rb.velocity = new Vector2(speed, speed);
        Destroy(this.gameObject, 5);
        isDestroyOnTime = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isDestroyOnTime = false;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (isDestroyOnTime)
            Instantiate(dB.GetRandomObstacle, this.transform.position, this.transform.rotation);
        else
        {
            Destroy(Instantiate(spinePrefab, this.transform.position, this.transform.rotation), 10);
            Destroy(Instantiate(spinePrefab, this.transform.position, this.transform.rotation * Quaternion.Euler(0, 0, 180)), 10); ;
        }
    }
}
