/*
 * EnemySpawner.cs
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

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private EnemyFactory factory;
    [SerializeField] private float areaWidth = 1;
    [SerializeField] private float areaLength = 1;
    public int spawnCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        // spawn the specified number of enemies on start()
        for (int i = 0; i < spawnCount; i++)
        {
            float x = transform.position.x + Random.Range(0f, areaWidth);
            float z = transform.position.z + Random.Range(0f, areaLength);

            GameObject newEnemy = factory.NewInstance();
            // set position
            NavMeshAgent navMesh = newEnemy.GetComponent<NavMeshAgent>();
            if (navMesh)
            {
                navMesh.Warp(new Vector3(x, transform.position.y, z));
            }
            else
            {
                newEnemy.transform.position = new Vector3(x, transform.position.y, z);
            }
        }
    }

    // visualize the spawning area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + (areaWidth / 2), transform.position.y + 1f, transform.position.z + (areaLength / 2)),
            new Vector3(areaWidth, 2f, areaLength));
    }

    // we can put other "spawning" functions here, such as spawning enemies from the save data
}
