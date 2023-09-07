using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 targetVelocity;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Ground ground;
    private SpriteRenderer sprite;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    private bool lookingRight = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        Debug.Log(direction.x);
        Debug.Log(lookingRight);

        if (direction.x > 0 && lookingRight == false || direction.x < 0 && lookingRight == true) 
        {
            Flip();
        }

        targetVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0);
    }

    private void Flip()
    {
        lookingRight = !lookingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1f, 1f);
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = rb.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, maxSpeedChange);

        rb.velocity = velocity;
    }

    public bool GetLookingRight()
    {
        return lookingRight;
    }
}
