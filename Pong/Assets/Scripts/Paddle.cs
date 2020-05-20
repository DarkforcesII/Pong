using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float height;

    string input;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        speed = 5;
    }

    public void Init(bool isRightPaddle)
    {
        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            isRight = isRightPaddle;

            // right side
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;

            input = "PaddleRight";
        }
        else
        {
            // left side
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;

            input = "PaddleLeft";
        }
        // update paddle position
        transform.position = pos;

        transform.name = input;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // restrict paddle movement
        if (transform.position.y < GameManager.bottomLeft.y + height/ 2 && move < 0)
        {
            move = 0;
        }
        if (transform.position.y > GameManager.topRight.y - height/ 2 && move > 0)
        {
            move = 0;
        }

        transform.Translate(move * Vector2.up);
    }
}
