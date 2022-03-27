using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject alertPrefab;
    private float xBound = 8;
    private float yBound = 4;
    private int currentWave;
    void Start()
    {
        //for (int i = 0; i < 100; i++)
        currentWave = 1;
        InvokeRepeating("SpawnWave", 0, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnWave()
    {
        for (int i = 0; i < currentWave; i++)
        {
            Vector2 pos = GenerateRandomPosition();
            StartCoroutine(SpawnBullet(pos));
        }
        currentWave++;
    }

    IEnumerator SpawnBullet(Vector2 pos)
    {
        Destroy(Instantiate(alertPrefab, pos, Quaternion.identity), 5);
        yield return new WaitForSeconds(5);
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bullet.transform.Rotate(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }

    Vector2 GenerateRandomPosition()
    {
        float x, y;
        RaycastHit2D hit;
        do
        {
            x = Random.Range(-xBound, xBound);
            y = Random.Range(-yBound, yBound);
            hit = Physics2D.Raycast(new Vector2(x, y), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Player")) break;
            }
            else break;
        } while (true);
        return new Vector2(x, y);
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(Random.Range(0, 180), Random.value, Random.value);
    }
}
