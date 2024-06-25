using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_controller : MonoBehaviour {
    Game_Management gameManager;

    void Awake() {
        gameManager = Game_Management.instance;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.gameover) {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D colisor) {
        if (colisor.gameObject.tag == "box") {
            gameObject.SetActive(false);
            gameManager.score();
        }
    }
}
