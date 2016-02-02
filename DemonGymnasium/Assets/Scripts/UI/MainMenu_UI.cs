using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour {

	AudioSource PlayBtnAS;
	AudioSource CreditBtnAS;
	AudioSource QuitBtnAS;

	public AudioClip hoveringSFX;
	public AudioClip clickingSFX;

	void Awake(){
		PlayBtnAS = ((RectTransform)transform).Find ("PlayBtn").gameObject.GetComponent<AudioSource> ();
		CreditBtnAS = ((RectTransform)transform).Find ("CreditBtn").gameObject.GetComponent<AudioSource> ();
		QuitBtnAS = ((RectTransform)transform).Find ("QuitBtn").gameObject.GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayBtnClicked(){
		PlayBtnAS.clip = clickingSFX;
		PlayBtnAS.Stop ();
		PlayBtnAS.Play ();
		SceneManager.LoadScene ("MainScene");
	}

	public void PlayBtnHovered(){
		PlayBtnAS.clip = hoveringSFX;
		PlayBtnAS.Stop ();
		PlayBtnAS.Play ();
	}

	public void CreditBtnClicked(){
		CreditBtnAS.clip = clickingSFX;
		CreditBtnAS.Stop ();
		CreditBtnAS.Play ();
	}

	public void CreditBtnHovered(){
		CreditBtnAS.clip = hoveringSFX;
		CreditBtnAS.Stop ();
		CreditBtnAS.Play ();
	}

	public void QuitBtnClicked(){
		Application.Quit ();
	}

}
