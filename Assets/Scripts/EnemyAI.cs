using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player's transform.
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform enemy;

    // Reference to the NavMeshAgent component for pathfinding.
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the NavMeshAgent component attached to this object.
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame.
    void Update()
    {
        // If there's a reference to the player...
        if (GameHandler.Instance.isGamePlaying() && player != null)
        {
            // Set the enemy's destination to the player's current position.
            navMeshAgent.SetDestination(player.position);

            if (UnityEngine.Vector3.Distance(enemy.transform.position, player.transform.position) < .5f)
            {
                Destroy(player.gameObject);
            }
        }

     
        
    }

    

}
