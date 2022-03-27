using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 10;
    private GameObject spinePrefab;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        spinePrefab = Resources.Load("spine") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //rb.AddForce(transform.forward * speed);
        transform.Translate(this.transform.worldToLocalMatrix.MultiplyVector(transform.up).normalized * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(Instantiate(spinePrefab, this.transform.position, this.transform.rotation), 10);
            Destroy(Instantiate(spinePrefab, this.transform.position, this.transform.rotation * Quaternion.Euler(0, 0, 180)), 10); ;
            Destroy(this.gameObject);
        }
    }
}
