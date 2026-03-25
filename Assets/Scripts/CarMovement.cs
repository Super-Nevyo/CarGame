using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Transform[] poweredTires;
    [SerializeField] private Transform cannonTarget;

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpTime;
    [SerializeField] private float aimSpeed;
    [SerializeField] private float cannonTargetMin;
    [SerializeField] private float cannonTargetMax;
    private Rigidbody rb;

    private float _acceleration;
    private float _turning;
    private float _aiming;
    private Vector3 _tempAimPosition;

    public event Action ShootEvent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // pull the required values for moving based on what buttons were pressed
    public void OnMove(InputValue value)
    {
        _acceleration = value.Get<Vector2>().y;
        _turning = value.Get<Vector2>().x;
    }
    // same as on move but for tilting the cannon
    public void OnAimCannon(InputValue value)
    {
        _aiming = value.Get<float>();
    }
    // the jump function doesnt work the same as it may for a person jumping so a corutine is started to push the wheels away from the body of the car to push it up
    public void OnJump() 
    {
        StartCoroutine(Jump(jumpTime));
    }
    // invoke the shoot event to tell the cannon to shoot
    public void OnAttack(InputValue value)
    {
        ShootEvent?.Invoke();
    }
    
    void FixedUpdate()
    {
        // turn the game object the tires are attached to so the tires will turn
        foreach (var t in poweredTires)
        {
            t.Rotate(0, _acceleration * speed, 0);
        }
        // the car rotates by rotating the body of the car directly because i didnt have enough time to work out rotational suspension for the body when the tires have moved
        gameObject.transform.Rotate(0, _turning * turnSpeed, 0);
        // move the aim target based on where aiming happens like we learned in class
        AimUpdate();
    }

    private IEnumerator Jump(float WaitTime)
    {
        Debug.Log("jump corutine started");
        // tire goes down some amount pushing the body of the car up by the suspension script
        foreach(var tire in poweredTires)
        {
            tire.position = tire.position - jumpHeight * gameObject.transform.up;
        }
        yield return new WaitForSeconds(WaitTime);
        // puts the tires back where they are supposed to be
        foreach (var tire in poweredTires)
        {
            tire.position = tire.position + jumpHeight * gameObject.transform.up;
        }
    }
    // we made this in class
    private void AimUpdate()
    {
        _tempAimPosition = cannonTarget.localPosition;
        _tempAimPosition.y += _aiming * aimSpeed;
        _tempAimPosition.y = Mathf.Clamp(_tempAimPosition.y, cannonTargetMin, cannonTargetMax);
        cannonTarget.localPosition = _tempAimPosition;
    }
}
