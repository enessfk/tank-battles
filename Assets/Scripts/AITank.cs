using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AITank : Tank
{
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }
    public Animator fsm { get { return GetComponent<Animator>(); } }
    public Transform[] wayPoints;
    Vector3[] wayPointsPositions;
    int index;

    private void Start()
    {
        wayPointsPositions= new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPointsPositions[i] = wayPoints[i].position;
        }
    }
    protected override void Move()
    {
        float distance = Vector3.Distance(transform.position,target.position);
        fsm.SetFloat("Distance", distance);

        float distanceFromCurrentWaypoints = Vector3.Distance(transform.position, wayPointsPositions[index]);
        fsm.SetFloat("DistanceFromCurrentWaypoint", distanceFromCurrentWaypoints);
      
    }

    float delay;
    public void Shoot()
    {
        if ((delay +=Time.deltaTime)>1f)
        {
            Fire();
            delay = 0;
        }
    }
    public void Chase()
    {
        agent.SetDestination(target.position);
    }   
    public void Patrol()
    {
        agent.SetDestination(wayPointsPositions[index]);
    }

    public void LookAt()
    {
        LookAt(target);
    }

    protected override IEnumerator LookAt(Transform target)
    {
        while (Vector3.Angle(turret.forward, (target.position - transform.position)) > 5f)
        {
            turret.Rotate(turret.up, 4f);
            yield return null;
        }
    }

    public void FindNewWaypoint()
    {
        switch (index)
        {
            case 0:
                index = 1;
                break;
            case 1:
                index = 0;
                break;
        }
        agent.SetDestination(wayPointsPositions[index]);
    }
}
