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

    public void OnMove(InputValue value)
    {
        _acceleration = value.Get<Vector2>().y;
        _turning = value.Get<Vector2>().x;
    }

    public void OnAimCannon(InputValue value)
    {
        _aiming = value.Get<float>();
    }

    public void OnJump() 
    {
        StartCoroutine(Jump(jumpTime));
    }

    public void OnAttack(InputValue value)
    {
        ShootEvent?.Invoke();
    }

    void FixedUpdate()
    {

        foreach (var t in poweredTires)
        {
            t.Rotate(0, _acceleration * speed, 0);
        }
        gameObject.transform.Rotate(0, _turning * turnSpeed, 0);
        AimUpdate();
    }

    private IEnumerator Jump(float WaitTime)
    {
        Debug.Log("jump corutine started");
        foreach(var tire in poweredTires)
        {
            tire.position = tire.position - jumpHeight * gameObject.transform.up;
        }
        yield return new WaitForSeconds(WaitTime);
        foreach (var tire in poweredTires)
        {
            tire.position = tire.position + jumpHeight * gameObject.transform.up;
        }
    }
    private void AimUpdate()
    {
        _tempAimPosition = cannonTarget.localPosition;
        _tempAimPosition.y += _aiming * aimSpeed;
        _tempAimPosition.y = Mathf.Clamp(_tempAimPosition.y, cannonTargetMin, cannonTargetMax);
        cannonTarget.localPosition = _tempAimPosition;
    }
}
