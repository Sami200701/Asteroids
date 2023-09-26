using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float rotationSpeed = 120f;
    public float movementSpeed = 100f;
    public GameObject bulletPrefab, bulletSpawner;
    

    private Rigidbody _rigid;

    public static int SCORE = 0;

    public float xBoardLimit, yBoardLimit;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float thrust = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        Vector3 movementDirection = transform.right;
        
        transform.Rotate(Vector3.forward, -rotation);
        
        _rigid.AddForce(thrust * movementDirection);

        var newPos = transform.position;
        if (newPos.x > xBoardLimit)
            newPos.x = -xBoardLimit + 1;
        else if (newPos.x < -xBoardLimit)
            newPos.x = xBoardLimit - 1;
        else if (newPos.y > yBoardLimit)
            newPos.y = -yBoardLimit + 1;
        else if (newPos.y < -yBoardLimit)
            newPos.y = yBoardLimit - 1;
        transform.position = newPos;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
          //GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, Quaternion.identity);
          GameObject bullet = BulletPool.instance.GetPooledBullet();
          if (bullet != null)
          {
              bullet.transform.position = bulletSpawner.transform.position;
              bullet.SetActive(true);
              bullet.GetComponent<Bullet>().targetVector = transform.right;
          }
          
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Fragment"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa");
        }
        
    }
    
}
