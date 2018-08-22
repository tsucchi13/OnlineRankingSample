using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class HighScoreManager : MonoBehaviour
{
	public int myRank;
	public List<HighScore> highScoreList;
	public List<HighScore> myHighScoreList;

	public void SendHighScore (int score)
	{
		NCMBObject obj = new NCMBObject ("OnlineRanking");
		obj.Add ("UserName", PlayerPrefs.GetString ("name"));
		obj.Add ("HighScore", score);
		//http送って帰ってきたレスポンスがエラーだったら、全部自動でNCMBException型に変換されて帰ってくる、それをeっていう変数に入れてる
		obj.SaveAsync ((NCMBException e) => {      
			if (e != null) {
				//エラー処理
				Debug.Log ("score data failed");
			} else {
				//成功時の処理
				Debug.Log ("score data sent successfully");
				PlayerPrefs.SetString("ObjectID",obj.ObjectId);
			}                   
		});
	}

	public void FetchMyRank (System.Action onSuccess)
	{
		// データスコアの「HighScore」から検索
    NCMBQuery<NCMBObject> rankQuery = new NCMBQuery<NCMBObject> ("OnlineRanking");
    rankQuery.WhereGreaterThan("HighScore", PlayerPrefs.GetInt("score"));
    rankQuery.CountAsync((int count , NCMBException e )=>{
			if(e != null){
				//件数取得失敗
			}else{
				//件数取得成功
				myRank = count+1; // 自分よりスコアが上の人がn人いたら自分はn+1位
				FetchMyRankingData(() => onSuccess());
			}
    });
	}

	public void FetchTopRankingData (System.Action onSuccess)
	{
		// データストアの「HighScore」クラスから検索
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("OnlineRanking");
		query.OrderByDescending ("HighScore");
		query.Limit = 100;
		query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
			if (e != null) {
				//検索失敗時の処理
			} else {
				//検索成功時の処理
				List<HighScore> list = new List<HighScore>();
				// 取得したレコードをHighScoreクラスとして保存
				foreach (NCMBObject obj in objList) {
					int    s = System.Convert.ToInt32(obj["HighScore"]);
					string n = System.Convert.ToString(obj["UserName"]);
					list.Add( new HighScore( s, n ) );
				}
				highScoreList = list;
				onSuccess();
			}
		});
	}

	void FetchMyRankingData (System.Action onSuccess)
	{
    // スキップする数を決める（ただし自分が1位か2位のときは調整する）
    int numSkip = myRank - 10;
    if(numSkip < 0) numSkip = 0;

    // データストアの「HighScore」クラスから検索
    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("OnlineRanking");
    query.OrderByDescending ("HighScore");
    query.Skip  = numSkip;
    query.Limit = 20;
    query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
			if (e != null) {
				//検索失敗時の処理
			} else {
				//検索成功時の処理
				List<HighScore> list = new List<HighScore>();
				// 取得したレコードをHighScoreクラスとして保存
				foreach (NCMBObject obj in objList) {
					int    s = System.Convert.ToInt32(obj["HighScore"]);
					string n = System.Convert.ToString(obj["UserName"]);
					list.Add( new HighScore( s, n ) );
				}
				myHighScoreList = list;
				onSuccess();
			}
		});
	}
}
