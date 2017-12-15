using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	private int currentScore;
	private int record;
	private bool newRecord;
	private string recordInfoPrefix;
	private string rawOnlineRecord = "?";
	void Start () {
		
		Instantiate(music, transform.position, transform.rotation);
		
		record = PlayerPrefs.GetInt("record");
		currentScore = PlayerPrefs.GetInt("currentScore");
		
		if (currentScore > record)
		{
			newRecord = true;
			PlayerPrefs.SetInt("record", currentScore);
			recordInfoPrefix = "new local record!";
		}
		else
		{
			newRecord = false;
		}
		
		rawOnlineRecord = PlayerPrefs.GetString("online");
		if (rawOnlineRecord != "?")
		{
			if (currentScore > int.Parse(rawOnlineRecord))
			{
				StartCoroutine(util.UpdateRanking(currentScore.ToString()));
				PlayerPrefs.SetString("online", currentScore.ToString());
				recordInfoPrefix = "new online record!";
			}
		}
		
		
	}
	
	public GameObject music;
	public Texture2D background;
	
	public GUIStyle buttonStyle;
	public GUIStyle buttonStyleAndroid;
	
	public GUIStyle gameOverStyle;
	public GUIStyle scoreStyle;
	public GUIStyle recordStyle;
	private UtilityBag util = new UtilityBag();
	public GUIStyle gameOverStyleAndroid;
	public GUIStyle	scoreStyleAndroid;
	public GUIStyle	recordStyleAndroid;
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), background);
		
		if (Screen.width >= 960)
		{
			GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(100), util.GetRelativeWidth(1000), util.GetRelativeHeigth(100)), "GAME OVER", gameOverStyle);
			GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(275), util.GetRelativeWidth(1000), util.GetRelativeHeigth(100)), "score " + currentScore.ToString(), scoreStyle);
			
			if (newRecord)
			{
				GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(340), util.GetRelativeWidth(1000), util.GetRelativeHeigth(100)), recordInfoPrefix, recordStyle);
			}
			else
			{
				GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(340), util.GetRelativeWidth(1000), util.GetRelativeHeigth(400)), "local record -> " + record.ToString() + "\n\nonline record -> " + rawOnlineRecord, recordStyle);
			}
			
			if (GUI.Button(new Rect(util.GetRelativeWidth(20), Screen.height - util.GetRelativeHeigth(120), util.GetRelativeWidth(300), util.GetRelativeHeigth(120)), "Back", buttonStyle))
			{
	
				Application.LoadLevel("Menu");
			}
			else if (GUI.Button(new Rect(Screen.width - util.GetRelativeWidth(320), Screen.height - util.GetRelativeHeigth(120), util.GetRelativeWidth(300), util.GetRelativeHeigth(120)), "Again", buttonStyle))
			{
				Application.LoadLevel("Game");
			}
		}
		else
		{
			GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(400), util.GetRelativeHeigth(100), util.GetRelativeWidth(800), util.GetRelativeHeigth(100)), "GAME OVER", gameOverStyleAndroid);
			GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(275), util.GetRelativeWidth(1000), util.GetRelativeHeigth(100)), "score " + currentScore.ToString(), scoreStyleAndroid);
			
			if (newRecord)
			{
				GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(340), util.GetRelativeWidth(1000), util.GetRelativeHeigth(100)), recordInfoPrefix, recordStyleAndroid);
			}
			else
			{
				GUI.Label(new Rect(Screen.width/2 - util.GetRelativeWidth(500), util.GetRelativeHeigth(340), util.GetRelativeWidth(1000), util.GetRelativeHeigth(400)), "local record -> " + record.ToString() + "\n\nonline record -> " + rawOnlineRecord, recordStyleAndroid);
			}
			
			if (GUI.Button(new Rect(util.GetRelativeWidth(20), Screen.height - util.GetRelativeHeigth(120), util.GetRelativeWidth(300), util.GetRelativeHeigth(120)), "Back", buttonStyleAndroid))
			{
	
				Application.LoadLevel("Menu");
			}
			else if (GUI.Button(new Rect(Screen.width - util.GetRelativeWidth(320), Screen.height - util.GetRelativeHeigth(120), util.GetRelativeWidth(300), util.GetRelativeHeigth(120)), "Again", buttonStyleAndroid))
			{
				Application.LoadLevel("Game");
			}
		}
	}
}
