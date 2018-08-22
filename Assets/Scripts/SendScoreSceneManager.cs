using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SendScoreSceneManager : MonoBehaviour {

	public void ScoreSubmitButton ()
	{
		InputField scoreInputField = GameObject.Find ("ScoreInput").GetComponent<InputField> ();
		GameObject.FindObjectOfType<HighScoreManager>().SendHighScore(int.Parse(scoreInputField.text));
		PlayerPrefs.SetInt("score", int.Parse(scoreInputField.text));
	}

	public void BackButton ()
	{
		SceneManager.LoadScene ("Start");
	}
}
