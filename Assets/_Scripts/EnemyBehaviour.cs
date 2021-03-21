/*
 * EnemyBehaviour.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator anim;
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    public float playerDetectionRange = 25;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.forward.magnitude > 0)
        {
            anim.SetBool("Walk Forward", true);
        }
        if ((transform.position - player.transform.position).magnitude < playerDetectionRange)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}
