using UnityEngine;
using System.Collections;

public class EnemyHealth :MonoBehaviour{

	public int currentHP = 10;		
	public int maxHP = 10;			
	public int killScore = 5;	
	public AudioClip enemyHurtAudio;		


	public bool IsAlive {
		get {
			return currentHP > 0;
		}
	}


	public void TakeDamage(int damage){
		if (!IsAlive)
			return;
		currentHP -= damage;
		if (currentHP <= 0 ) currentHP = 0;
	
		if (GameManager.gm != null) {	
			GameManager.gm.AddScore (killScore);
		}

		if (enemyHurtAudio != null)				
			AudioSource.PlayClipAtPoint (enemyHurtAudio, transform.position);
	}

}
