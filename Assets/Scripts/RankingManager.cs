using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

	public Transform topRankContentTarget;
	public Transform myRankContentTarget;
	public GameObject rankItem;

	HighScoreManager highScoreManager;

	void Start () {
		highScoreManager = GameObject.FindObjectOfType<HighScoreManager>();
		
		highScoreManager.FetchTopRankingData(() => ShowTopRankers());
		highScoreManager.FetchMyRank(() => ShowMyRivals());
	}

	void ShowTopRankers(){
		for(int i = 0; i < highScoreManager.highScoreList.Count; i++){
			GameObject rankItemClone = Instantiate(rankItem, topRankContentTarget);
			rankItemClone.transform.Find("NumberText").GetComponent<Text>().text = (1 + i).ToString();
			rankItemClone.transform.Find("NameText").GetComponent<Text>().text = highScoreManager.highScoreList[i].name;
			rankItemClone.transform.Find("ScoreText").GetComponent<Text>().text = highScoreManager.highScoreList[i].score.ToString();
		}
	}

	void ShowMyRivals(){
		for(int i = 0; i < highScoreManager.myHighScoreList.Count; i++){
			GameObject rankItemClone = Instantiate(rankItem, myRankContentTarget);
			rankItemClone.transform.Find("NumberText").GetComponent<Text>().text = (highScoreManager.myRank - 10 + i).ToString();
			rankItemClone.transform.Find("NameText").GetComponent<Text>().text = highScoreManager.myHighScoreList[i].name;
			rankItemClone.transform.Find("ScoreText").GetComponent<Text>().text = highScoreManager.myHighScoreList[i].score.ToString();
		}
	}
}
