using UnityEngine;
using System.Collections;

public class AutoCreateObject : MonoBehaviour {

	public GameObject createGameObject;			

	public float minSecond=5.0f;				
	public float maxSecond=10.0f;				

	private float timer;		
	private float createTime;   
    public AudioClip generateAudioClip;
    public int bound = 8;
    private int count = 0;

    
    void Start () {
		timer = 0.0f;			
		createTime = Random.Range (minSecond, maxSecond);	
	}

	
	void Update () {
		
		if (GameManager.gm != null 
			&& GameManager.gm.gameState != GameManager.GameState.Playing)	
			return;
		timer += Time.deltaTime;	
		if (timer >= createTime && count <= bound && GameManager.gm.generateEnemy) {	
			CreateObject ();		
			timer = 0.0f;			
			createTime = Random.Range (minSecond, maxSecond);	
		}
	}

	
	void CreateObject(){	
        count+=1;
		Vector3 deltaVector = new Vector3 (1.0f, 1.0f, 1.0f);	
		GameObject newGameObject = Instantiate (				
			createGameObject, 					
			transform.position-deltaVector, 	
			transform.rotation					
		) as GameObject;
        if (generateAudioClip != null) {
            AudioSource.PlayClipAtPoint(generateAudioClip, transform.position);
        }
        
	}
}
