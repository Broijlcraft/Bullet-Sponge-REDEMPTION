using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMovement))]

public class EnemyCombat : StatsBase
{

    [Header("Till Animations")]
    public float damage;
    public float attackCooldown;

    NavMeshAgent myAgent;
    PlayerStats player;

    protected override void Start()
    {
        base.Start();
        myAgent = GetComponent<NavMeshAgent>();
        player = PlayerStats.single;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetCurrentHealth() <= 0)
        {
            Death();
        }

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
        player.SetCurrentHealth(player.GetCurrentHealth() - damage);
    }

    public void IAttackCD()
    {
        Debug.Log("AttackCD");
    }
}
