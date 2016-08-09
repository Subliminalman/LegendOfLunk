using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 1f;
	public int direction = 0;


	Vector3 ogPosition;


	// Use this for initialization
	void Start () {
		ogPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * speed * Time.deltaTime;

		if (Vector3.Distance(transform.position, ogPosition) > 10f) {
			Destroy(gameObject);
		}



	}

	void OnCollisionEnter (Collision _collision) {

		if (_collision.gameObject.tag == "Enemy") {
			
			Enemy enemy = _collision.gameObject.GetComponent<Enemy>();			

			enemy.Hit ();

			Destroy (gameObject);
		}

	}
}
