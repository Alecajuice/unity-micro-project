using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxStartVelocity;
    public float baseForce;
    public float forceMultiplier;
    public float knockbackDistance;
    float _timeElapsed = 0f;
    Rigidbody2D _rigidbody;
    ConstantForce2D _constantForce;
    public void Knockback(Transform hitbox)
    {
        // _rigidbody.AddForce(new Vector2(knockbackForce, 0));
        float knockbackSpeed = Mathf.Sqrt(-2 * _constantForce.force.x * knockbackDistance);
        _rigidbody.velocity = new Vector2(knockbackSpeed, _rigidbody.velocity.y
            + (this.gameObject.transform.position.y - hitbox.position.y));
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _constantForce = GetComponent<ConstantForce2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.gravityScale = 0;
        float startVelocity = Random.Range(-maxStartVelocity, maxStartVelocity);
        _rigidbody.velocity = new Vector2(0, -startVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;
        _constantForce.force = new Vector2(baseForce + _timeElapsed * forceMultiplier, 0);
        // Debug.Log(_constantForce.force.ToString());
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            GameOverUI.instance.GameOver();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Level Bounds")) {
            GameOverUI.instance.GameOver();
        }
    }
}
