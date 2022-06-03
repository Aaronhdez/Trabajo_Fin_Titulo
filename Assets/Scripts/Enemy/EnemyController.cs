using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    [Header("General Properties")]
    [SerializeField] protected float wanderSpeed;
    [SerializeField] protected float chaseSpeed;
    public int health;
    public int attackDamage;
    [SerializeField] protected bool isDead;
    [SerializeField] protected float fovRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float minimumDistanceToTarget;

    [Header("Game Instances")]
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected SoundController soundController;
    [SerializeField] protected ParticleSystem deadEffect;
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    public float ChaseSpeed { get => chaseSpeed; set => chaseSpeed = value; }
    public float WanderSpeed { get => wanderSpeed; set => wanderSpeed = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float AttackSpeed {
        get => attackSpeed; set => attackSpeed = value;
    }
    public float AttackRange {
        get => attackRange; set => attackRange = value;
    }
    public float FovRange {
        get => fovRange; set => fovRange = value;
    }

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        IsDead = false;
    }

    public virtual void ApplyDamage(int damagedReceived) { }

    protected virtual void CheckHealthStatus() { }
}
