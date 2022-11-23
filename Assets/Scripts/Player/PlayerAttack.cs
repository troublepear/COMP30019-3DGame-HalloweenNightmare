using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public int shootingDamage = 1;				
	public float shootingRange = 50.0f;			
	public float shootingInterval = 1.0f;
	public AudioClip shootingAudio;				

	public Transform shootingEffectTransform;	
	
	private bool isShooting;			
	private Camera myCamera;			
	private Ray ray;					
	private RaycastHit hitInfo;
	private GameObject instantiation;

	private float nextShootingTime;
	private float timer = 0.0f;
//
	
	public GameObject firePoint;
	public GameObject effectToSpawn;

	private Vector3 direction;
	private Quaternion rotation;

	void Start () {	
		myCamera = GetComponentInParent<Camera> ();		
	}

	void LateUpdate () {	
		isShooting=Input.GetKeyDown("mouse 0");	
		if (isShooting && timer >= shootingInterval && (GameManager.gm==null || GameManager.gm.gameState == GameManager.GameState.Playing) && GameManager.gm.checkMana() && Time.timeScale == 1f) {
			if (GameManager.gm != null) {	
				GameManager.gm.DecreaseMana(1);
			}
			Shoot ();
			SpawnVFX();
			timer = 0;
		} 
		timer += Time.deltaTime;
	}


	void SpawnVFX() {
		GameObject vfx;
		if (firePoint != null) {
			vfx = Instantiate(effectToSpawn,firePoint.transform.position,Quaternion.identity);
			vfx.transform.localRotation  = rotation;
		}else {
			Debug.Log("no fire point");
		}
	}

	void RotateToMouseDirection(GameObject obj,Vector3 destination) {
		direction = destination - obj.transform.position;
		rotation = Quaternion.LookRotation(direction);
		obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation,rotation,1);
	}
	void Shoot()
	{
		if (shootingAudio != null) {
			AudioSource.PlayClipAtPoint (shootingAudio, transform.position);	
		}

		ray.origin = Camera.main.transform.position;	
        ray.direction = Camera.main.transform.forward;	


		if (Physics.Raycast (ray, out hitInfo, shootingRange)) {

			RotateToMouseDirection(gameObject,hitInfo.point);
			

		}else {
			var position = ray.origin + ray.direction * shootingRange;
			RotateToMouseDirection(gameObject,position);
		}
	}

}
