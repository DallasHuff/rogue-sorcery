using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Controller _controller = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
    [SerializeField] private TrailRenderer tr;

    private Vector2 direction, targetVelocity, velocity;
    private Rigidbody2D _body;
    private Ground _ground;

    private float inputFloat, maxSpeedChange, acceleration;
    private bool onGround;
    private bool lookingRight = true;

    //Dashing
    private bool canDash = true;
    private bool isDashing;
    [SerializeField]
    private float dashingPower = 24f;
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashingCooldown = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }


        inputFloat = _controller.input.RetrieveMoveInput();
        direction.x = inputFloat;

        if (inputFloat > 0 && lookingRight == false || inputFloat < 0 && lookingRight == true) 
        {
            Flip();
        }

        targetVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - _ground.friction, 0);

    }

    private void Flip()
    {
        lookingRight = !lookingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1f, 1f);
    }

    private void FixedUpdate()
    {
        onGround = _ground.onGround;
        velocity = _body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, maxSpeedChange);

        _body.velocity = velocity;

        if (isDashing)
        {
            return;
        }
    }

    public bool GetLookingRight()
    {
        return lookingRight;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = _body.gravityScale;
        _body.gravityScale = 0f;
        _body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        _body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
