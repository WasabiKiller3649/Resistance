using UnityEngine;
using System;

public class PhisicsController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    public Action<string> OnReflection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GravityChange(float scale)
    {
        _rigidbody2D.gravityScale = scale;
    }
    public void Stop()
    {
        _rigidbody2D.linearVelocity = new Vector2(0, 0);
    }
    public void MoveSurface(Vector3 speed)
    {
        _rigidbody2D.linearVelocity = speed;
    }
    public void MoveHorizontal(float speed)
    {
        _rigidbody2D.linearVelocity = new Vector2(speed, 0);
    }
    public void MoveVertical(float speed)
    {
        _rigidbody2D.linearVelocity = new Vector2(0, speed);
    }
    public void AccelerateHorizontal(float speed)
    {
        _rigidbody2D.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
    }
    public void AccelerateVertical(float speed)
    {
        _rigidbody2D.AddForce(new Vector2(0, speed), ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnReflection?.Invoke(collision.tag);
    }
}
