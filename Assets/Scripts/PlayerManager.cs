using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool isStoping = false;
    public bool isRum = false;
    public bool isFly = false;
    public Animator ANBot;
    public PathFollower PF;
    public float speed = 15f;

    [Header("Ускорители")] public AudioSource SorceObstacle;
    public AudioSource SpeedUp;
    
    [Header("Монетки")] 
    public Text moneyText;
    public int money;
    public GameObject hitEffectorMoney;
    public AudioSource SorceMoney;
    
    private void Start()
    {
        speed = 15f;
       // PF = FindObjectOfType<PathFollower>();
       PF.speed = speed;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            SceneManager.LoadScene(0);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0); 
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GreenTLBorder"))
        {
            isStoping = true;
        }

        if (other.gameObject.CompareTag("CarBot"))
        {
            isFly = true;
            ANBot.SetBool("isFly",isFly); 
            //FlyBot();
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0); 
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            money++;
            Destroy(other.gameObject); 
            Instantiate(hitEffectorMoney, transform.position, Quaternion.identity);
            SorceMoney.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RedTLBorder"))
        {
            if (isRum == false)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (other.gameObject.CompareTag("CarBot"))
        {
            StartCoroutine(FlyBot());
        }

        if (other.gameObject.CompareTag("SpeedUP"))
        {
            speed += 40f;
            Debug.Log("Скорость получена");
            SorceObstacle.Play();
            SpeedUp.Play();
        }

        if (other.gameObject.CompareTag("SpeedLow"))
        {
            speed = 15f;
            Debug.Log("Скорость снижена");
            SorceObstacle.Play();
        }

        if (other.gameObject.CompareTag("SpeedUPx2"))
        {
            speed = 170f;
            SorceObstacle.Play();
            SpeedUp.Play();
        }

        if (other.gameObject.CompareTag("SpeedLowx2"))
        {
            speed = 30f;
            SorceObstacle.Play();
            SpeedUp.Play();
        }
    }

    IEnumerator FlyBot()
    {
        yield return new WaitForSeconds(2f);
        isFly = true;
        StartCoroutine(FlyBot());
    }
}
