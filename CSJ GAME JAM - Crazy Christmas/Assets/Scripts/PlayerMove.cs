using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    Animator _anim;
    SpriteRenderer _spriteRender;

    public GameObject giftParticles;

    AudioSource _triguerSound;

    public GameController gameController;

    [HideInInspector]
    public int points ;
    // Start is called before the first frame update
    void Start()
    {
        _triguerSound = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");


        Vector3 move = new Vector3(horizontal, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;


        if(horizontal > 0 || horizontal < 0)
        {
            _anim.SetBool("isRunning", true);
            if (horizontal < 0)
            {
                _spriteRender.flipX = true;
            }
            else
            {
                _spriteRender.flipX = false;
            }
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue" && gameController.isBlue)
        {
            _triguerSound.Play();
            Instantiate(giftParticles, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.1f);
            points++;
        }
        else if(collision.tag == "Green" && gameController.isWhite)
        {
            _triguerSound.Play();
            Instantiate(giftParticles, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.1f);
            points++;
        }
        else if (collision.tag == "Pink" && gameController.isPink)
        {
            _triguerSound.Play();
            Instantiate(giftParticles, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.1f);
            points++;
        }
        else if (collision.tag == "Yellow" && gameController.isYellow)
        {
            _triguerSound.Play();
            Instantiate(giftParticles, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.1f);
            points++;
        }
        else
        {
            gameController.isEnded = true;
            gameController.DisplayGameOver();
        }
    }
}
