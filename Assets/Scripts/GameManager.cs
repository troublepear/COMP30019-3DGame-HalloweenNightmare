using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static public GameManager gm;		

	public GameObject player;			
	private PlayerHealth playerHealth;	

	public int TargetScore=10;			
	private int currentScore;			

	public enum GameState {Playing,GameOver,Winning};	
	public GameState gameState;			

    public Text scoreText;				
	public HealthBar healthBar;
	public Manabar manabar;

	public bool generateEnemy = false;


	public GameObject playingCanvas;	
	public GameObject gameWinCanvas;			
	public GameObject gameFailCanvas;			
	public GameObject gamePauseCanvas;
	public GameObject moveTutorial;
	public GameObject jumpTutorial;
	public GameObject switchTutorial;
	public GameObject attackTutorial;
	public GameObject Task;


	public static bool GameIsPaused = false;
	private bool cursor;					
	private bool isGameOver=false;		
	public Image hurtImage;
	private Color flashColor = new Color (1.0f, 0.0f, 0.0f, 0.3f);
	private float flashSpeed = 2.0f;								
	private int moved = 0;
	private int jumped = 0;
	private int switched = 0;
	private int attacked = 0;

	void Start () {
		Cursor.visible = false;	
		gm = GetComponent<GameManager> ();	
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");	

		currentScore = 0;
		playerHealth = player.GetComponent<PlayerHealth> ();
		gm.gameState = GameState.Playing;	
		Time.timeScale = 1f;

		if (playerHealth) {
			healthBar.SetMaxHealth(playerHealth.currentHealth);
			manabar.SetMaxMana(playerHealth.currentMana);
		}

		playingCanvas.SetActive (true);		
		moveTutorial.SetActive (true);
		jumpTutorial.SetActive(false);
		switchTutorial.SetActive(false);
		attackTutorial.SetActive(false);
		Task.SetActive(false);

		gameWinCanvas.SetActive(false);	
		gameFailCanvas.SetActive(false);
		gamePauseCanvas.SetActive(false);
		



		
	}

	public void Resume(){
        gamePauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

	public void LoadMenu(){
		SceneManager.LoadScene("StartScene");
	}

	public void QuitGame(){
		Application.Quit();
	}

    void Pause(){
        gamePauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

	
	void Update () {
		
		hurtImage.color = Color.Lerp (
			hurtImage.color, 
			Color.clear, 
			flashSpeed * Time.deltaTime
		);


		switch (gameState) {	

		case GameState.Playing:	
			 if (Input.GetKeyDown(KeyCode.Escape)) {
				if (GameIsPaused) {
					Resume();
				}else {
					Pause();
				}
			}

			if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && moved == 0 ) {
				moveTutorial.SetActive (false);
				jumpTutorial.SetActive(true);
				moved = 1;
			}

			if (moved == 1 && Input.GetKeyDown(KeyCode.Space) && jumped == 0) {
				jumpTutorial.SetActive(false);
				switchTutorial.SetActive(true);
				jumped =1;
			}

			if (jumped == 1 && switched == 0 && moved == 1 && Input.GetKeyDown(KeyCode.Q)) {
				switchTutorial.SetActive(false);
				attackTutorial.SetActive(true);
				switched = 1;
			}

			if (jumped == 1 && switched == 1 && moved == 1 && attacked == 0 && Input.GetKeyDown(KeyCode.Mouse0)) {
				attackTutorial.SetActive(false);
				Task.SetActive(true);
				generateEnemy = true;
				attacked = 1;
			}

            scoreText.text = "Score:" + currentScore;			
			healthBar.SetHealth(playerHealth.currentHealth);
			manabar.SetMana(playerHealth.currentMana);
			if (Input.GetKeyDown (KeyCode.Escape))		
				Cursor.visible = !Cursor.visible;
			if (playerHealth.isAlive == false)
				gm.gameState = GameState.GameOver;
			else if (currentScore >= TargetScore)
				gm.gameState = GameState.Winning;
			break;
		
		case GameState.Winning:
            if (!isGameOver) {
				Cursor.visible = true;					

				playingCanvas.SetActive (false);	
				gameWinCanvas.SetActive(true);	
				gameFailCanvas.SetActive(false);
				gamePauseCanvas.SetActive(false);
				isGameOver = true;
				Time.timeScale = 0f;
			}
			break;
		
		case GameState.GameOver:
			if (!isGameOver) {
				Cursor.visible = true;				
				playingCanvas.SetActive (false);	
				gameWinCanvas.SetActive(false);	
				gameFailCanvas.SetActive(true);
				gamePauseCanvas.SetActive(false);

				isGameOver = true;
			}
			break;
		}
	}

	public void AddScore(int value){
		currentScore += value;
	}

	public void AddMana(int value){
		if (playerHealth != null)
			playerHealth.AddMana(value);
	}

	public void DecreaseMana(int value) {
		if (playerHealth != null)
			playerHealth.decreaseMana(value);
	}

	public bool checkMana() {
		if (playerHealth != null)
			return playerHealth.checkMana();
		return false;;
	}

	public void PlayerTakeDamage(int value){
		if (playerHealth != null)
			playerHealth.TakeDamage(value);
		hurtImage.color = flashColor;
	}

	public void PlayerAddHealth(int value){
		if (playerHealth != null)
			playerHealth.AddHealth(value);
	}

	public void PlayAgain(){
		SceneManager.LoadScene("gameplay 1");
	}

	public void BackToMain(){
		SceneManager.LoadScene("StartScene");
	}

}