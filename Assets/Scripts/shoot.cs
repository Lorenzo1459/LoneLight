using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform gunPoint;
    bool isFiring = false;
    float fireInput;


    float speed;

    // Use this for initialization
    void Start () {
        speed = 8f;
	}

    // Update is called once per frame
    void Update() {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y+speed * Time.deltaTime);
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(transform.position.y > max.y+5) {
            gameObject.SetActive(false);
        }
    }
}

/*fireInput = Input.GetAxis("Fire1");

        if (fireInput != 0) {
            Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
            Destroy(bulletPrefab, 2f);
        }
*/
    
