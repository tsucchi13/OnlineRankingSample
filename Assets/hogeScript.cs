using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hogeScript : MonoBehaviour {

	List<Quiz> quizlist = new List<Quiz>();


	// Use this for initialization
	void Start () {

		Quiz quiz = new Quiz ();
		quiz.question = "hoge";
		Debug.Log (quiz.question);

		Quiz quiz1 = new Quiz("aa",("bb","cc","dd"),1);
		//this.transform.position = new Vector3 (0, 0, 0);と同じ！
		quiz1.question = "aaa";
		quiz1.answers[0] = "bbb";
		quiz1.answers[1] = "ccc";
		quiz1.answers[2] = "ddd";
		quiz1.answers[3] = "eee";
		quiz1.answerIndex = 2;

		quizlist.Add (quiz);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
