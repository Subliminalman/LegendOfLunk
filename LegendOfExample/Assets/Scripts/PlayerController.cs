using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int hp = 5;
	public float speed = 5f;
	public float attackTime = 1f;
	public GameObject sword;
	public MeshRenderer mesh;
	public Collider col;
	public ParticleSystem particleSystem;
	public Transform cam;
	public Bullet bullet;
	
	bool canAttack = true;
	int direction = 0;
	float currentAttackTime = 0f;
	Color ogColor;

	// Use this for initialization
	void Start () {
		ogColor = mesh.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		cam.position = new Vector3(transform.position.x, cam.position.y, transform.position.z);
		if (canAttack == true) {

			if (Input.GetKey (KeyCode.W)) {
				//DO WHATEVER IS IN HERE
				transform.position += Vector3.forward * speed * Time.deltaTime;
				direction = 0;
				//x , y , z
				//0 , 0 , 1
				//* speed
				//0, 0, 5
			}

			else if (Input.GetKey(KeyCode.S)) {
				transform.position += Vector3.back * speed * Time.deltaTime;
				direction = 2;
			}

			else if (Input.GetKey (KeyCode.A)) {
				transform.position += Vector3.left * speed * Time.deltaTime;
				direction = 3;
			}

			else if (Input.GetKey (KeyCode.D)) {
				transform.position += Vector3.right * speed * Time.deltaTime;
				direction = 1;
			}
		}

		transform.eulerAngles = Vector3.up * 90f * direction;


		if (Input.GetKeyDown (KeyCode.Space)) {
			//SHOW OUR SWORD
			if (canAttack == true) {

				canAttack = false;

				currentAttackTime = 0f;

				sword.SetActive(true);
			}
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			Bullet b = Instantiate<Bullet>(bullet);

			b.transform.rotation = transform.rotation;
			b.transform.position = transform.position + (transform.forward * 1.5f);
		}

		if (canAttack == false) {
			currentAttackTime += Time.deltaTime;
			if (currentAttackTime >= attackTime ) {

				currentAttackTime = 0f;

				sword.SetActive(false);

				canAttack = true;
			}
		}
	}

	public void  Hit () {
		//FLASH
		
		hp--;
		
		col.enabled = false;
		
		if (hp <= 0) {
			//DIE
			StartCoroutine(Die ());
		} else {
			StartCoroutine(Flash ());
		}
	}
	
	IEnumerator Die () {
		mesh.enabled = false;
		particleSystem.Play();
		yield return new WaitForSeconds(2f);
		Destroy (gameObject);
	}
	
	IEnumerator Flash () {
		mesh.sharedMaterial.color = Color.red;
		yield return new WaitForSeconds (0.1f);
		mesh.sharedMaterial.color = ogColor;
		yield return new WaitForSeconds (0.1f);
		mesh.sharedMaterial.color = Color.red;
		yield return new WaitForSeconds (0.1f);
		mesh.sharedMaterial.color = ogColor;
		
		col.enabled = true;
	}



	void OnCollisionEnter (Collision _collision) {
		Debug.Log ("COLLIDING");
		if (_collision.gameObject.tag == "Enemy") {

			Enemy enemy = _collision.gameObject.GetComponent<Enemy>();

			if (canAttack == false) { 
				enemy.Hit ();
			} else {
				//GET HIT
				Hit ();
			}
		}
	}
}
