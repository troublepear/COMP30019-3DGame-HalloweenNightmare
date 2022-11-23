using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour {
	
	public GameObject InitSubPanel;		

	public InputField usernameInputField;	
	public Toggle soundToggle;				

	
	public void StartGame(){
		SceneManager.LoadScene("gameplay 1");							
	}

	
	public void SwitchSound(){
		if (soundToggle.isOn) PlayerPrefs.SetInt ("SoundOff", 0);	
		else PlayerPrefs.SetInt ("SoundOff", 1);					
	}

	
	public void ExitGame(){
		Application.Quit ();	
	}

	
	void Start () {
		ActiveInitPanel ();	
	}

	
	public void ActiveInitPanel(){
		InitSubPanel.SetActive (true);
	}


}
