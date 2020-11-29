using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMovement))]

public class EnemyCombat : StatsBase
{

    [Header("Till Animations")]
    public float attackCooldown;

    NavMeshAgent myAgent;
    PlayerMovement player;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        player = PlayerMovement.single;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<EnemyMovement>().chase)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("No Player Found");
            return;
        }
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis <= myAgent.stoppingDistance)
        {
            myAgent.isStopped = true;
            if (!IsInvoking(nameof(IAttackCD)))
            {
                Attack();
                Invoke(nameof(IAttackCD),attackCooldown);
            }
        }
    }

    public void Attack()
    {
        Debug.Log("Attacking");
    }

    public void IAttackCD()
    {
        Debug.Log("Recharge");
    }
}
