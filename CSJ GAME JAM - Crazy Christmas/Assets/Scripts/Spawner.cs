using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   
    public GameObject[] gift;
    public PlayerMove player;
    public float _minX = -2.6f, _maxX = 2.6f;
    float spawnDelay = 2.5f;
    int _instGiftIndex;
    float _gravityScale;
    float _linearDrag;
    int _count;
    
    


    // Start is called before the first frame update
    void Start()
    {
        _gravityScale = 0.1f;
        _linearDrag = 1f;
        player.speed = 2f;
        _count = 1;
        StartCoroutine(IncreaseSpeed());
        StartCoroutine(StartSpawn());
    }

    // Update is called once per frame
    void Update()
    {

        if(player.speed >= 6)
        {
            player.speed = 6f;
        }

        if (spawnDelay <= 0.2)
        {
            spawnDelay = 0.2f;
        }
        if(_gravityScale >= 1)
        {
            _gravityScale = 1f;
        }
        if(_linearDrag <= 0)
        {
            _linearDrag = 0;
        }
        
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        _count++;
        _instGiftIndex = Random.Range(0, 4);
        GameObject instacedGift = Instantiate(gift[_instGiftIndex]);
        
       
        float xPos = Random.Range(_minX, _maxX);

        instacedGift.transform.position = new Vector2(xPos, 7f);
        

        instacedGift.GetComponent<Rigidbody2D>().gravityScale = _gravityScale;
        instacedGift.GetComponent<Rigidbody2D>().drag = _linearDrag;
        //if (x <= 0.2)
        //{
        //    x = 0.2f;
        //}
        if (_count > 20)
        {
            _gravityScale += 0.05f;
            _linearDrag -= 0.05f;
            player.speed += 0.1f;
            _count = 0;
            Debug.Log(_gravityScale);
            
        }
        StartCoroutine(StartSpawn());
        
    }

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(5f);
        spawnDelay -= 0.05f;
        
        
        StartCoroutine(IncreaseSpeed());
    }
}
