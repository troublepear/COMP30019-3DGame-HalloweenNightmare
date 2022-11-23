using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int startHealth = 10;	
	public int currentHealth;		
	public int restLife = 3;		


	public float respawnTime  = 5.0f;	
	public Transform spawnTransform;	
	public GameObject gun;				
	public int maxMana = 10;
	public int currentMana;
	public float manaRecoveryTime = 2.0f;
	public int manaRecovery = 1;

	public bool isAlive { get { return currentHealth > 0; } }

	private Animator anim;
	private Rigidbody rigid;
	private CapsuleCollider capsuleCollider;
	private PlayerWeaponSwitcher playerWeaponSwitcher;	
	private IKController userIKController;
	private float timer = 0.0f;

	void Start () {
		currentHealth = startHealth;
		currentMana = maxMana;
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		playerWeaponSwitcher = GetComponent<PlayerWeaponSwitcher> ();
		userIKController = GetComponent<IKController> ();
	}

	void Update() {
		if (timer > manaRecoveryTime) {
			timer = 0.0f;
			AddMana(manaRecovery);
		}
		timer += Time.deltaTime;
	}

	public void TakeDamage(int damage){
		currentHealth -= damage;
		if (currentHealth < 0)
			currentHealth = 0;

		if (currentHealth == 0) {
			if (restLife > 0) {
				Invoke ("respawn", respawnTime);
			}

			if (anim != null) {
				anim.SetBool ("isDead", true);
				anim.applyRootMotion = true;
			}
			rigid.useGravity = false;
			capsuleCollider.enabled = false;

			if (userIKController != null) {
				userIKController.enabled = false;
			}

			if (playerWeaponSwitcher != null) {
				foreach (Transform trans in playerWeaponSwitcher.weaponList) {
					trans.gameObject.SetActive (false);
				}
			} else if(gun!=null) {
				gun.SetActive (false);
			}
		}
	}

	public void AddHealth(int value){
		currentHealth += value;
		if (currentHealth > startHealth)	
			currentHealth = startHealth;
	}

	public void AddMana(int value) {
		currentMana+=value;
		if (currentMana > maxMana) {
			currentMana = maxMana;
		}
	}

	public void decreaseMana(int value) {
		currentMana-=value;
		if (currentMana < 0) {
			currentMana = 0;
		}
	}

	public bool checkMana(){
		if (currentMana <= 0) {
			return false;
		} 
		return true;
	}

	public void respawn()
	{
		currentHealth = startHealth;
		restLife--;
		transform.position = spawnTransform.position;
		transform.rotation = spawnTransform.rotation;

		rigid.useGravity = true;
		capsuleCollider.enabled = true;
		if (anim != null) {
			anim.SetBool ("isDead", false);
			anim.applyRootMotion = false;
		}
		if (userIKController != null) {
			userIKController.enabled = true;
		}

		if (playerWeaponSwitcher != null) {
			playerWeaponSwitcher.changeNextWeapon ();
		} else if (gun != null) {
			gun.SetActive (true);
		}
	}
}
