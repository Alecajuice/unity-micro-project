using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum PlayerState {
        STANDING,
        JUMPING
    };
    public float movementSpeed;
    public float movementAccel;
    public float jumpAccel;
    public GameObject swordSwingPrefab;

    Rigidbody2D _rigidbody;
    PlayerState state;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.JUMPING;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        spawnHitbox();
    }

    private void move()
    {
        // float horizontal = Input.GetAxis("Horizontal") * movementAccel;
        float horizontal = 0f;
        float vertical = state == PlayerState.STANDING ? (Input.GetKeyDown(KeyCode.W) ? jumpAccel : 0f) : 0f;

        float xVel = _rigidbody.velocity.x;
        float horizontalAccel = Mathf.Max(-movementSpeed - xVel, Mathf.Min(movementSpeed - xVel, horizontal));

        Vector2 movementForce = new Vector2(x:horizontalAccel * _rigidbody.mass, y:vertical);
        // Debug.Log(xVel + ", " + movementForce.ToString());
        _rigidbody.AddForce(movementForce);
    }

    private void spawnHitbox()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 spawnPosition = _rigidbody.position;
            Instantiate(swordSwingPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            state = PlayerState.STANDING;
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            state = PlayerState.JUMPING;
        }
    }
}
