using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ThirdPersonShooterController : MonoBehaviour
{
    public GameObject gameOver;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private AudioSource audioSource;
    [SerializeField] private AudioClip walk;

    private StarterAssetsInputs starterAssetsInputs;
    private Collider characterCollider;
    private ThirdPersonController thirdPersonController;
    private Animator animator;
    private bool isMoving = false;
    public Text puntuacionFinal;
    

    private bool muerto = false;


    // Start is called before the first frame update
    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        characterCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        isMoving = Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f;
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            //debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if(starterAssetsInputs.shoot && starterAssetsInputs.aim)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;

        }
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else audioSource.Stop();

        if (Input.GetKeyDown(KeyCode.Escape) && muerto)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator EsperarYDetenerJuego()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos

        // Detener el juego
        Time.timeScale = 0;
        gameOver.SetActive(true);
        GenerateEnemies.enemigosMuertos = 0;
        puntuacionFinal.text = "Bajas: " + GenerateEnemies.enemigosMuertos;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!muerto && other.gameObject.CompareTag("Enemy")) {

            muerto = true;

            characterCollider.enabled = false;
            animator.SetTrigger("Death");

            StartCoroutine(EsperarYDetenerJuego());

            
        }
    }
}
