using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

	// 本当はUI部分は別スクリプトにまとめたい
	public GameObject nameInputPanel;

	void Start ()
	{
		if (!PlayerPrefs.HasKey ("name"))
			ShowNameInputPanel ();
	}




	void ShowNameInputPanel ()
	{
		nameInputPanel.SetActive (true);
	}

	void HideNameInputPanel ()
	{
		nameInputPanel.SetActive (false);
	}

	void SaveName ()
	{
		InputField nameInputField = GameObject.Find ("NameInput").GetComponent<InputField> ();
		PlayerPrefs.SetString ("name", nameInputField.text);
	}

	public void NameSubmitButton ()
	{
		SaveName ();
		HideNameInputPanel ();
	}

	public void ResetNameButton ()
	{
		ShowNameInputPanel ();
	}

	public void SendScoreSceneButton ()
	{
		SceneManager.LoadScene ("SendScore");
	}

	public void RankSceneButton ()
	{
		SceneManager.LoadScene ("Ranking");
	}
}
