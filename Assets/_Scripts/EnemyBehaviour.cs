/*
 * ControlPanelController.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator anim;
    public NavMeshAgent navMeshAgent;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.forward.magnitude > 0)
        {
            anim.SetBool("Walk Forward", true);
        }
        navMeshAgent.SetDestination(player.position);
    }
}
