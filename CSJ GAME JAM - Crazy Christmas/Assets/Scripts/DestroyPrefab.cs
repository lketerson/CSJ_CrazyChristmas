using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{
    private GameObject[] particleVector;
    float timer;

    private void Start()
    {
       
       StartCoroutine(destroyParticles());
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            particleVector = GameObject.FindGameObjectsWithTag("destroyFX");
            timer = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }


    IEnumerator destroyParticles()
    {
        yield return new WaitForSeconds(30);

        foreach (var item in particleVector)
        {          
            Destroy(item.gameObject);
            Debug.Log(item.name);
        }

        StartCoroutine(destroyParticles());  
            
    }
}
