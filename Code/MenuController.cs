using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {
	
	// Just replace the IDs below with your appIDs.
    private static readonly Dictionary<string, string> appIds = new Dictionary<string, string>() {
	{ "Android", "504a3f96bbf98f0c0000008c"},
	{ "IOS", "504a3f860ff3eb080000009d" }
    };
    private RevMob revmob;

    void Awake() 
	{
		//Debug.Log("Creating RevMob Session");
		revmob = RevMob.Start(appIds);
    }
	
	public enum MenuState
	{
		Splash,
		Main,
		Tutorial,
		Loading,
		Credits
	}
	
	public GameObject music;
	private MenuState menuState;
	public Texture2D logoTexture;
	public GUIStyle logoMessage;
	public GUIStyle logoMessageAndroid;
	private UtilityBag utilities = new UtilityBag();
	
	public GUIStyle tutorialStyle;
	public GUIStyle tutorialStyleAndroid;
	
	private RevMobIOSBanner  banner;
	void Start () 
	{
		//PlayerPrefs.SetInt("record", 0);
		#if (UNITY_IPHONE && !UNITY_EDITOR)
        	banner = (RevMobIOSBanner)revmob.CreateBanner();
        	banner.Show();
        #endif
		
		if (PlayerPrefs.GetString("hasPlayedOnce").Equals("true"))
		{
			PlayerPrefs.SetString("hasPlayedOnce","false");
			#if (UNITY_IPHONE && !UNITY_EDITOR)
        		revmob.ShowPopup();
	        #endif
			#if (UNITY_ANDROID && !UNITY_EDITOR)
				revmob.ShowPopup();
			#endif
		}
		menuState = MenuState.Splash;
		Instantiate(music, transform.position, transform.rotation);
		recordBlinkTimer = Time.time;
		PlayerPrefs.SetInt("currentScore", 0);
		
		StartCoroutine(utilities.GetRanking());
		
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			clickToPlayString = "tap to play!";
			tutorialString = "Score points by matching 3+ similar memes before the time runs out. Move them around with touch!\nStart!";
			//clickToPlayString = "toque para jogar!";
			//tutorialString = "Pontue combinando 3+ memes iguais antes que acabe seu tempo. Mexa os memes arrastando e soltando.\nok!";
		}
		else
		{
			clickToPlayString = "tap to play!";
			tutorialString = "Score points by matching 3+ similar memes before the time runs out. Move them around with touch!\nStart!";
			//clickToPlayString = "clique para jogar!";
			//tutorialString = "Pontue combinando 3+ memes iguais antes que acabe seu tempo. Mexa os memes clicando para selecionar.\nok!";
		}
		
	}
	
	private float recordBlinkTimer;
	private bool showRecord = true;
	public GUIStyle recordStyle;
	public GUIStyle MoreGamesStyle;
	public GUIStyle MoreGamesStyleAndroid;
	private string clickToPlayString;
	private string tutorialString;
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), logoTexture);
		
		switch (menuState)
		{	
			case MenuState.Splash:
				
				if (Screen.width >= 960)
				{
					if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(300), Screen.height/2 - utilities.GetRelativeHeigth(80), utilities.GetRelativeWidth(600), utilities.GetRelativeHeigth(240)), clickToPlayString, logoMessage))
					{
						#if (UNITY_IPHONE && !UNITY_EDITOR)
				        	banner.Hide();
				        #endif
						menuState = MenuState.Tutorial;
					}
					if (GUI.Button(new Rect(Screen.width - utilities.GetRelativeWidth(200), Screen.height - utilities.GetRelativeHeigth(80), utilities.GetRelativeWidth(200), utilities.GetRelativeHeigth(80)), "credits", logoMessage))
					{
						menuState = MenuState.Credits;
					}
				}
				else
				{
					if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(300), Screen.height/2 - utilities.GetRelativeHeigth(80), utilities.GetRelativeWidth(600), utilities.GetRelativeHeigth(240)), clickToPlayString, logoMessageAndroid))
					{
						#if (UNITY_IPHONE && !UNITY_EDITOR)
				        	banner.Hide();
				        #endif
						menuState = MenuState.Tutorial;
					}
					if (GUI.Button(new Rect(Screen.width - utilities.GetRelativeWidth(200), Screen.height - utilities.GetRelativeHeigth(80), utilities.GetRelativeWidth(200), utilities.GetRelativeHeigth(80)), "credits", logoMessageAndroid))
					{
						menuState = MenuState.Credits;
					}
				}
				if (showRecord)
				{
					if (Screen.width >= 960)
					{
						GUI.Label(new Rect(Screen.width/2 - utilities.GetRelativeWidth(500), Screen.height - utilities.GetRelativeHeigth(230), utilities.GetRelativeWidth(1000), utilities.GetRelativeHeigth(80)), "Local Record -> " + PlayerPrefs.GetInt("record").ToString(), recordStyle);
					}
					else
					{
						GUI.Label(new Rect(Screen.width/2 - utilities.GetRelativeWidth(500), Screen.height - utilities.GetRelativeHeigth(230), utilities.GetRelativeWidth(1000), utilities.GetRelativeHeigth(80)), "Local Record -> " + PlayerPrefs.GetInt("record").ToString(), recordStyleAndroid);
					}
				
					if (Screen.width >= 960)
					{
						GUI.Label(new Rect(Screen.width/2 - utilities.GetRelativeWidth(500), Screen.height - utilities.GetRelativeHeigth(130), utilities.GetRelativeWidth(1000), utilities.GetRelativeHeigth(80)), "Online Record -> " + PlayerPrefs.GetString("online"), recordStyle);
					}
					else
					{
						GUI.Label(new Rect(Screen.width/2 - utilities.GetRelativeWidth(500), Screen.height - utilities.GetRelativeHeigth(130), utilities.GetRelativeWidth(1000), utilities.GetRelativeHeigth(80)), "Online Record -> " + PlayerPrefs.GetString("online"), recordStyleAndroid);
					}
					
				}
			break;
			
		case MenuState.Loading:
				if (Screen.width >= 960)
				{
					if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(300), Screen.height - utilities.GetRelativeHeigth(180), utilities.GetRelativeWidth(600), utilities.GetRelativeHeigth(80)), "Loading", LoadingStyle))
					{
						
					}
				}
				else
				{
					if (GUI.Button(new Rect(Screen.width /2- utilities.GetRelativeWidth(300), Screen.height - utilities.GetRelativeHeigth(180), utilities.GetRelativeWidth(600), utilities.GetRelativeHeigth(80)), "Loading", LoadingStyleAndroid))
					{
						
					}
				}
				Application.LoadLevel("Game");
			break;
			
		case MenuState.Credits:
			if (Screen.width >= 960)
			{
				if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(500), Screen.height/2 - utilities.GetRelativeHeigth(100), utilities.GetRelativeWidth(1000), utilities.GetRelativeHeigth(440)), "Developer -> Christian Coimbra\nMusic -> http://www.nosoapradio.us\n\nback", logoMessage))
				{
					menuState = MenuState.Splash;
				}
			}
			else
			{
				if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(500), Screen.height/2 - utilities.GetRelativeHeigth(100), utilities.GetRelativeWidth(1000), utilities.GetRelativeHeigth(440)), "Developer -> Christian Coimbra\nMusic -> http://www.nosoapradio.us\n\nback", logoMessageAndroid))
				{
					menuState = MenuState.Splash;
				}
			}
			break;
			
		case MenuState.Tutorial:
			if (Screen.width >= 960)
			{
				if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(400), Screen.height/2 - utilities.GetRelativeHeigth(150), utilities.GetRelativeWidth(800), utilities.GetRelativeHeigth(600)), tutorialString, tutorialStyle))
				{
					menuState = MenuState.Loading;
				}		
			}
			else
			{
				if (GUI.Button(new Rect(Screen.width/2 - utilities.GetRelativeWidth(400), Screen.height/2 - utilities.GetRelativeHeigth(150), utilities.GetRelativeWidth(800), utilities.GetRelativeHeigth(600)), tutorialString, tutorialStyleAndroid))
				{
					menuState = MenuState.Loading;
				}
			}
			break;
		}
	}
	
	
	public GUIStyle LoadingStyle;
	public GUIStyle LoadingStyleAndroid;
	private void ConfigureOnlineRankingString()
	{
		PlayerPrefs.SetString("online", utilities.rankingData);
	}
	
	public GUIStyle recordStyleAndroid;
	void Update()
	{
		if (Time.time - recordBlinkTimer >= 0.25f)
		{
			recordBlinkTimer = Time.time;
			showRecord = !showRecord;
		}
		
		if (utilities.request.isDone)
		{
			ConfigureOnlineRankingString();
		}
	}
}