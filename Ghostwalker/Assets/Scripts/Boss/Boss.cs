using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    [SerializeField] private int maxHealth;
    private float currentHealth;
    [SerializeField] private int speed;
    public Transform[] spawnPoints;
    
    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamimgDistanceMin = 3f;
    [SerializeField] private float _roamimgTimerMax = 2f;

    public bool isDie;

    private float _roamingTimer;
    private Vector3 _roamPosition;
    private Vector3 _startingPosition;

    [SerializeField] private GameObject projectilePrefabFire;
    [SerializeField] private GameObject projectilePrefabIce;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float attackRate;
    private float nextAttackTime;

    private float currentAngle; 
    [SerializeField] private float angleStep;

    private GameObject currentProjectilePrefab;
    private float projectileSwitchTimer;
    private float slowingTime;
    [SerializeField] private float slowingInterval;
    [SerializeField] private float projectileSwitchInterval;
    
    [SerializeField] private Slider healthSlider;
    private float dieTimer = 4f;
    
    private float abilityCooldown = 20f;
    private float abilityTimer;

    private Animator Animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        boxCollider2D = GetComponent<BoxCollider2D>();

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        currentProjectilePrefab = projectilePrefabFire;
        abilityTimer = abilityCooldown;
        
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            isDie = true;
            if (dieTimer <= 0)
                SceneManager.LoadScene("EndGame");
            else
                dieTimer -= Time.deltaTime;
            navMeshAgent.speed = 0;
            Animator.SetBool("IsDie", true);
            return;
        }

        _roamingTimer -= Time.deltaTime;
        if (_roamingTimer < 0)
        {
            Roaming();
            _roamingTimer = _roamimgTimerMax;
        }

        projectileSwitchTimer -= Time.deltaTime;
        if (projectileSwitchTimer <= 0)
        {
            SwitchProjectilePrefab();
            SwitchRotation();
            projectileSwitchTimer = projectileSwitchInterval;
        }
        
        slowingTime -= Time.deltaTime;
        if (slowingTime <= 0)
        {
            player.moveSpeed = 5;
            slowingTime = slowingInterval;
        }

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackRate;
        }
        
        abilityTimer -= Time.deltaTime;
        if (abilityTimer <= 0)
        {
            UseLaserAbility();
            abilityTimer = abilityCooldown;
            var random = new System.Random();
            var enemyClone = Instantiate(enemy, spawnPoints[random.Next(spawnPoints.Length)].position,
                quaternion.identity);
            var enemyClass = enemyClone.GetComponent<Enemy>();
            enemyClass.PlayerPosition = player.transform;
            enemyClass.doChasingEnemy = true;
        }

        if (currentHealth < maxHealth / 2)
        {
            navMeshAgent.speed += 1f;
            attackRate = 0.25f;
            abilityCooldown = 15;
            Animator.SetBool("IsAngry", true);
        }
    }

    private void Roaming()
    {
        navMeshAgent.speed = speed;
        _startingPosition = transform.position;
        _roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(_roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        var directionToPlayer = (player.transform.position - transform.position).normalized;

        var randomAngle = Random.Range(-60, 60) * Mathf.Deg2Rad;
        var randomDirection = Quaternion.Euler(0, 0, randomAngle) * directionToPlayer;

        return transform.position + randomDirection * Random.Range(_roamimgDistanceMin, _roamingDistanceMax);
    }

    private void Attack()
    {
        foreach (Transform firePoint in firePoints)
        {
            var direction = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 
                                        Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0);
            
            var projectile = Instantiate(currentProjectilePrefab, firePoint.position, Quaternion.identity);
            var projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(direction);
                projectileScript.fireType = currentProjectilePrefab;
            }
            currentAngle += angleStep;
        }
    }

    private void UseLaserAbility()
    {
        foreach (Transform firePoint in firePoints)
        {
            var laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            var laserScript = laser.GetComponent<Laser>();
            if (laserScript != null)
            {
                laserScript.boss = this;
                laserScript.RotateAroundBoss(transform);
            }
        }
    }

    private void SwitchProjectilePrefab()
    {
        currentProjectilePrefab = currentProjectilePrefab == projectilePrefabFire 
            ? projectilePrefabIce 
            : projectilePrefabFire;
    }
    
    private void SwitchRotation()
    {
        angleStep = -angleStep;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
    }
}
