using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int hp = 1;
	public float speed = 1f;
	public MeshRenderer mesh;
	public Collider col;

	public ParticleSystem particleSystem;

	public Transform target;

	Color ogColor;

	// Use this for initialization
	void Start () {
		ogColor = mesh.material.color;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance(target.position, transform.position) < 5f) {
			Vector3 move = target.position - transform.position;
			transform.position += move.normalized * speed * Time.deltaTime;
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

}
