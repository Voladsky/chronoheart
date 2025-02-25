using UnityEngine;

public class EnemyFollow : EnemyBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float min_distance;
    [SerializeField] private bool isStatic;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    [SerializeField] private float seeDistance;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        leftEdge.SetParent(null, true);
        rightEdge.SetParent(null, true);
    }
    private void Update()
    {
        Vector3 project = Vector3.Project(player.position - enemy.position, new Vector3(1, 0, 0));
        if (project.magnitude < seeDistance) FollowPlayer(project);
        else MoveInIdle();
    }

    void FollowPlayer(Vector3 project)
    {
        //rotate to look at the player
        if (project.x > 0 && movingLeft)
        {
            DirectionChange();
        }
        else if (project.x < 0 && !movingLeft)
        {
            DirectionChange();
        }
        //move to player
        else if (project.magnitude > min_distance) MoveInDirection((int)project.normalized.x);
        else anim.SetBool("moving", false);
    }

    void MoveInIdle()
    {
        if (isStatic)
            return;

        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        if (isStatic)
            return;

        idleTimer = 0;
        anim.SetBool("moving", true);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
