using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IAMotion : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    private AudioSource audioSource;
    public GenerateEnemies generateEnemies;

    NavMeshAgent agent;
    Animator animator;
    Collider collider;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
        generateEnemies.GetComponent<GenerateEnemies>().bajas.text = "Bajas: " + GenerateEnemies.enemigosMuertos;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            animator.SetTrigger("Death");
            collider.enabled = false;
            Destroy(gameObject, 2f);
            audioSource.Play();
            GenerateEnemies.enemigosMuertos++;
            Debug.Log(GenerateEnemies.enemigosMuertos);
        }
    }
}
