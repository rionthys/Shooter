
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask GroundLayer;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;

    private Vector3 spherePos;
    private Vector3 velocity;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public int timer;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public Animator anim;
    
    [HideInInspector] public bool isDead;
    bool run = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        isDead = false;
    }

    private void Update()
    {
        Gravity();

        if (Vector3.Distance(player.transform.position, transform.position) > 30 && !isDead){ 
            Patroling();
            anim.SetBool("Walk", true);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) <= 30 && !isDead){
            agent.SetDestination(player.position);
            anim.SetBool("Running", true);
        }
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    public bool isGrounded
    {
        get
        {
            spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
            return Physics.CheckSphere(spherePos, controller.radius - 0.05f, GroundLayer);
        }
    }

    private void Gravity()
    {
        velocity.y += Time.deltaTime * gravity;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

}
