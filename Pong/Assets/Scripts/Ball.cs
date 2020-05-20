using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed;

    float radius;
    Vector2 direction;

    // audio
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip sfxClip;
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.one.normalized;
        radius = transform.localScale.x / 2;

        // audio
        sfxSource.clip = sfxClip;
        musicSource.PlayOneShot(musicClip1);
        // Start Coroutine
        StartCoroutine(PlayLoop());
    }

    IEnumerator PlayLoop()
    {
        yield return new WaitForSecondsRealtime(2.087f);
        musicSource.clip = musicClip2;
        musicSource.loop = true;
        musicSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        // game over
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            Debug.Log("Right Player Wins!");
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Left Player Wins!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            bool isRight = collision.GetComponent<Paddle>().isRight;
            sfxSource.Play();

            // when ball hits right paddle
            if (isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            // when ball hits left paddle
            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }
}
