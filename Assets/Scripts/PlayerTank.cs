using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    Vector3 touchPoint;
    Vector3 moveDir;
    Vector3 check = new Vector3(0, 0, 0);
    Camera main;

    private void Start()
    {
        main = Camera.main;
    }
    protected override void Move()
    {
        float moveAxis = Input.GetAxis("Vertical");
        float rotAxis = Input.GetAxis("Horizontal");

        rb.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime * moveAxis));
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.up * rotAxis * rotSpeed * Time.deltaTime));

        createMoveEffect(moveAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LookAt(target));
        }

        if (Input.GetMouseButton(0))
        {
            SetTouchPoint();
        }

        GetToTouchPoint();

    }

    protected override IEnumerator LookAt(Transform target)
    {
        while (Vector3.Angle(turret.forward, (target.position - transform.position)) > 5f)
        {
            turret.Rotate(turret.up, 4f);
            yield return null;
        }
        Fire();
    }

    private void SetTouchPoint()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction*100,Color.red);
        if (Physics.Raycast(ray,out RaycastHit info))
        {
            touchPoint = new Vector3(info.point.x, transform.position.y, info.point.z);
            moveDir = touchPoint - transform.position;
        }
    }
    private void GetToTouchPoint()
    {
        if (moveDir == check)
        {
            return;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, touchPoint, Time.deltaTime * moveSpeed);
            Quaternion lookRotation = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }
        
    }

}
