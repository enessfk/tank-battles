using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    float fieldOfView;
    float maxCheckDistance;
    public Transform target;
    public Animator fsm;
    public override void InitializeSense()
    {
        fieldOfView = 60;
        maxCheckDistance = 20;
    }

    public override void UpdateSense()
    {
        Vector3 direction = ((target.transform.position - transform.position)).normalized; // yönü değişmeyen birim vektör oldu büyüklüğünü kullanamdık 'normalized'
        Debug.DrawRay(transform.position, direction, Color.white);
        float angle = Vector3.Angle(direction, transform.forward);
        Debug.DrawRay(transform.position, transform.forward*maxCheckDistance, Color.blue);

        Debug.DrawRay(transform.position, direction * maxCheckDistance, Color.red);
        if (angle <= fieldOfView / 2)
        {
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxCheckDistance))
            {
                Debug.DrawRay(transform.position, direction * maxCheckDistance, Color.green);
                string name = hitInfo.transform.name;

                if (name.Equals("playerTank"))
                {
                    fsm.SetBool("Visibility", true);
                }
                else
                {
                    fsm.SetBool("Visibility", false);
                
                }

            }
            
        }
        else
        {
            fsm.SetBool("Visibility", false);
        }
    }
}
