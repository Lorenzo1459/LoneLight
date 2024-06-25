using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Jogador : MonoBehaviour {
    
    public Game_Management gameManager;
    public GameObject sangue;
    Rigidbody2D myBody;
    public GameObject BulletPosition;
    public GameObject tiro;
    float fireInput;
    bool isFiring = false;
    public float delay;
    private int vidas = 3;
    public Text vida;
    private float posX;
    public Camera camera2;
    Pool pool;

    public float speed;
    private float rotationSpeed;

    private float coolDown = 2f;
    private float coolDownTimer;

    public AudioSource shootAud;

    void Awake() {
        gameManager = Game_Management.instance;
    }
    // Use this for initialization
    void Start() {
        pool = Pool.Instance;
        myBody = GetComponent<Rigidbody2D>();
        gameManager = Game_Management.instance;
    }

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        segue();



        if (coolDown > 0) {
            coolDownTimer -= Time.deltaTime;
        }

        if (coolDownTimer < 0) {
            coolDownTimer = 0;
        }

        if (coolDownTimer == 0) {
            isFiring = true;
            GameObject bala1 = pool.GetPooledObject("tiro");
            bala1.transform.position = BulletPosition.transform.position;
            bala1.SetActive(true);
            shootAud.Play();
            isFiring = false;
            coolDownTimer = coolDown;
        }

        Vector3 screenpos = transform.position;
        screenpos.x = Mathf.Clamp(screenpos.x, -3f, 3f);
        transform.position = screenpos;


    }

    void OnTriggerEnter2D(Collider2D meuColisor) {
        if (meuColisor.gameObject.tag == "Inimigo") {
            Debug.Log("bateu");            
            meuColisor.gameObject.SetActive(false);            
            vidas--;
            if (vidas == 2) {
                gameManager.tomaDano1();
            }
            if (vidas == 1) {
                gameManager.tomaDano2();
            }
            vida.text = vidas.ToString();
            
            if (vidas == 0) {
                gameManager.restart();
                shootAud.mute = true;
            }
        }
    }
    void segue() {
        if (Input.GetMouseButton(0)) {
            Camera cam = camera2;
            Vector3 posicaoAntiga = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane);
            Vector3 posNova = cam.ScreenToWorldPoint(posicaoAntiga);
            transform.position = Vector3.MoveTowards(transform.position, posNova, 0.5f);
        }
    }
}
