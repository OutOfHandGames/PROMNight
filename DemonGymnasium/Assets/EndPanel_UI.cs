using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel_UI : MonoBehaviour {

	public AudioSource AS;
	public Text winText;
	public Image BG;
	private Image image;

	public AudioClip demonWinMusic;
	public string demonWinText;
	public Sprite demonWinBG;
	public Sprite demonWinImage;

	public AudioClip janitorWinMusic;
	public string janitorWinText;
	public Sprite janitorWinBG;
	public Sprite janitorWinImage;



	public void Awake(){
		AS = GetComponent<AudioSource> ();
		BG = GetComponent<Image> ();
		winText = ((RectTransform)transform).Find ("WinText").gameObject.GetComponent<Text> ();
		image = ((RectTransform)transform).Find ("Image").gameObject.GetComponent<Image> ();
	}


	//0 means Janitor; 1 means Demon
	public void Setup(int winner){

		//Janitor wins
		if (winner == 0) {
			gameObject.SetActive (true);
			AS.Stop ();
			AS.clip = janitorWinMusic;
			AS.Play ();
			winText.text = janitorWinText;
			BG.sprite = janitorWinBG;
			image.sprite = janitorWinImage;
		}
		//Demon wins
		else if (winner == 1) {
			gameObject.SetActive (true);
			AS.Stop ();
			AS.clip = demonWinMusic;
			AS.Play ();
			winText.text = demonWinText;
			BG.sprite = demonWinBG;
			image.sprite = demonWinImage;
		} 
		else {
			Debug.Log ("Unknown winner");
		}



	}


	public void MainMenuBtnClicked(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void RematchBtnClicked(){
		SceneManager.LoadScene ("MainScene");
	}
}
