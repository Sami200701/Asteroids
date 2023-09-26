using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    public Vector3 targetVector;

    public GameObject fragmentPrefab;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            
            Fragments(collision.transform.position);
        }
        else if (collision.gameObject.CompareTag("Fragment"))
        {
            IncreaseScore();
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score : " + Player.SCORE;
    }

    private void Fragments(Vector2 spawnPosition)
    {
        GameObject fragment1 = Instantiate(fragmentPrefab, spawnPosition, transform.rotation * Quaternion.Euler(0f,0f,50f));
        fragment1.GetComponent<Rigidbody>().velocity = -50 * transform.localScale.y * fragment1.transform.right * Time.deltaTime;
        
        spawnPosition.x += 2f;
        
        GameObject fragment2 = Instantiate(fragmentPrefab, spawnPosition, transform.rotation * Quaternion.Euler(0f,0f,-50f));
        fragment2.GetComponent<Rigidbody>().velocity = 50 * transform.localScale.y * fragment1.transform.right * Time.deltaTime;
        
        Destroy(fragment1, 4f);
        Destroy(fragment2, 4f);

    }
}
