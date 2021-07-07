using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MovementBase
{
    [Header("Start")]
    public bool chase;

    [Header("Stats")]
    public float runningDis;
    public float extraRotSpeed;

    EnemyCombat myStats;
    NavMeshAgent myAgent;
    PlayerMovement player;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        player = PlayerMovement.single;
        myStats = GetComponent<EnemyCombat>();
    }
    // Update is called once per frame
    void Update()
    {
        if (chase && CheckIfOnGround(bottemOfCharacter) && !myStats.IsInvoking(nameof(myStats.IAttackCD)))
        {
            if (myAgent.isStopped)
            {
                myAgent.isStopped = false;
            }
            ExtraRotation();
            SetDestination();
            ApplySpeed();
        }
    }

    public void ExtraRotation()
    {
        Vector3 lookRot = myAgent.steeringTarget - transform.position;
        lookRot.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRot), extraRotSpeed * Time.deltaTime);
    }

    private void ApplySpeed()
    {
        if(myAgent.destination != Vector3.zero)
        {
            if (myAgent.remainingDistance > runningDis)
            {
                myAgent.speed = sprintSpeed;
                isRunning = true;
            }
            else
            {
                myAgent.speed = walkSpeed;
                isRunning = false;
            }
        }
    }

    private void SetDestination()
    {
        myAgent.destination = player.transform.position;
    }
}
