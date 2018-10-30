using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Serialized object appears in inspector
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip weakHitSound;

    [SerializeField]
    GameObject missileprefab;
    [SerializeField]
    private string robotType;

    public int health;
    public int range;
    public float fireRate;

    public Transform missileFireSpot;
    public Animator robot;

    UnityEngine.AI.NavMeshAgent agent;

    private Transform player;
    private float timeLastFired;
    private bool isDead;
    void Start()
    {
        // 1    by default all robots are alive
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 2    Check if robot is dead
        if (isDead)
        {
            return;
        }
        // 3    look at player
        transform.LookAt(player);

        // 4    use NavMes to find the player
        agent.SetDestination(player.position);

        // 5    check if player within firing range
        if (Vector3.Distance(transform.position, player.position) < range
        && Time.time - timeLastFired > fireRate)
        {
            // 6    logs fire time to the console
            timeLastFired = Time.time;
            fire();
        }
    }
    private void fire()
    {
        GameObject missile = Instantiate(missileprefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;
        robot.Play("Fire");
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    // 1 Checks if the robot is dead, if yes then play dierobot sound and remove enemy(robot) gameobject
    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");
            Game.RemoveEnemy();
            GetComponent<AudioSource>().PlayOneShot(deathSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);
        }
    }
    // 2    coroutine with 1.5 thread delay
    IEnumerator DestroyRobot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
