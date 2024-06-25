using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Game_Management : MonoBehaviour {

    public static Game_Management instance;
    Pool pool;
    public GameObject xuriken,Restart,Ingame,navePlayer;
    public GameObject pointLightPlayer,pointLightTiro;
    public GameObject pLight1,pLight2,pLight3;
    private Vector3 posInic;
    public int contador = 0;
    private bool iniciar = false;
    private bool pause=false;
    public Text pontuacao;
    public Text placarfinal;
    public Text highscore;
    public int pontos = 0;
    public int pontosmax = 0;
    public bool gameover = false;
    public float tempo = 0;
    public float dificuldade = 0;

    public int moedas = 0;


    public AudioSource mus1,mus2;
    private bool muted = false;

    // Use this for initialization
    void Start () {

        pool = Pool.Instance;
        Time.timeScale = 0;        
	}
     void Awake() {
        instance = this;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (iniciar == true)
        {
            Controlador();
            Time.timeScale = 1;
        }
        tempo += Time.deltaTime;
    }

    void Controlador() {

        dificuldade = tempo / 3000;

        if (contador > 20 - dificuldade*200) {
            Criar();
            contador = 0;
        }
        contador++;
    }

    void Criar()
    {
        float posX = Random.Range(-2.8f,3.4f);
        posInic = new Vector3(posX, 6.15f, 1.42f);
        GameObject enemy = pool.GetPooledObject("Inimigo");
        enemy.transform.position = posInic;
        enemy.SetActive(true);
        enemy.GetComponent<Rigidbody2D>().gravityScale += dificuldade;                
    }
    public void Comecar()
    {
        iniciar = true;
        Time.timeScale = 1;
        pointLightPlayer = GameObject.FindGameObjectWithTag("PointLightPlayer");
        pointLightTiro = GameObject.FindGameObjectWithTag("PointLightTiro");
        pLight1 = GameObject.FindGameObjectWithTag("PointLight1");
        pLight2 = GameObject.FindGameObjectWithTag("PointLight2");
        pLight3 = GameObject.FindGameObjectWithTag("PointLight3");
    }

    public void Pause() {
        if (pause==false) {
            Time.timeScale = 0;
            pause = true;            
        } else {
            Time.timeScale = 1;
            pause = false;
        }
    }

    public void tomaDano1() {
        pLight1.SetActive(false);        
        pointLightPlayer.GetComponent<Light>().color = Color.magenta;
        pointLightTiro.GetComponent<Light>().color = Color.magenta;        
    }

    public void tomaDano2() {
        pLight2.SetActive(false);        
        pointLightPlayer.GetComponent<Light>().color = Color.red;
        pointLightTiro.GetComponent<Light>().color = Color.red;        
    }

    public void restart() {
        gameover = true;
        Restart.SetActive(true);
        Ingame.SetActive(false);        
    }
    public void score() {
        pontos++;
        pontuacao.text = pontos.ToString();
        pontosmax = PlayerPrefs.GetInt("hscore");
        if (pontosmax <= pontos) 
        {
            PlayerPrefs.SetInt("hscore", pontos);
            placarfinal.text = pontos.ToString();
        }
        else 
        {
            placarfinal.text = pontos.ToString();
            highscore.text = pontosmax.ToString();
        }
        

    }
    public void resetcena() {
        SceneManager.LoadScene("Trab_fab");
    }

    public void tocar1() {
        mus1.Play();
    }

    public void tocar2() {
        mus2.Play();
        mus1.mute = true;
    }

    public void mute() {        
        if (muted == false) {
            mus1.mute = true;
            mus2.mute = true;
            muted = true;
        }
        else if (muted == true) {
            mus2.mute = false;
            muted = false;
        }        
    }
}
