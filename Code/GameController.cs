using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject music;
	private float TILE_SIZE;
	private List<Meme> memeList = new List<Meme>();
	private Vector2 initialOffset;
	private float gap;
	
	private void SpawnAllMemes()
	{
		int index = 0;
		for (int i = 0 ; i < 8; i++)
		{
			for (int j = 0 ; j < 8; j++)
			{
				memeList.Add(new Meme(new Vector2(i,j), index));
				index++;
			}
		}
	}
	
	public GUIStyle floatingScoreStyle;
	public GUIStyle floatingScoreStyleAndroid;
	private void DrawFloatingScores()
	{
		if (Screen.width >= 960)
		{
			foreach (FloatingScore fs in fScores)
				GUI.Label(new Rect(fs.position.x, fs.position.y, util.GetRelativeWidth(300), util.GetRelativeHeigth(120)), fs.score, floatingScoreStyle);	
		}
		else
		{
			foreach (FloatingScore fs in fScores)
				GUI.Label(new Rect(fs.position.x, fs.position.y, util.GetRelativeWidth(300), util.GetRelativeHeigth(120)), fs.score, floatingScoreStyleAndroid);	
		}
	}
	
	private bool somePieceIsSelected = false;
	private int selectedIdCache;
	private void DrawAllMemes()
	{
		foreach (Meme m in memeList)
		{
			if (m.isKilled)
				continue;
			
			if (m.isSelected)
			{
				selectedIdCache = m.uniqueId;
			}
			else if (m.isFalling)
			{
				GUI.DrawTexture(new Rect(m.currentFallSituation.x, m.currentFallSituation.y, TILE_SIZE, TILE_SIZE), GetMemeSprite(m.type));
			}
			else
			{
				GUI.DrawTexture(new Rect(initialOffset.x + (m.position.x * TILE_SIZE) + (m.position.x * gap) , initialOffset.y + (m.position.y * TILE_SIZE) + (m.position.y * gap), TILE_SIZE, TILE_SIZE ), GetMemeSprite(m.type));
			}
		}
		
		if (somePieceIsSelected)
		{
			if (isRunningOnMobile)
				GUI.DrawTexture(new Rect(touchEndPosition.x - TILE_SIZE/2, Screen.height - touchEndPosition.y - TILE_SIZE/2, TILE_SIZE, TILE_SIZE ), GetMemeSprite( memeList[selectedIdCache].type) );
			else
				GUI.DrawTexture(new Rect(Input.mousePosition.x - TILE_SIZE/2, Screen.height - Input.mousePosition.y - TILE_SIZE/2, TILE_SIZE, TILE_SIZE ), GetMemeSprite( memeList[selectedIdCache].type) );
		}
	}
	
	private bool isRunningOnMobile = false;
	public Texture2D[] memeSprites = new Texture2D[12];
	
	private Texture2D GetMemeSprite(Meme.MemeType type)
	{
		int returnCode = 0;
		switch (type)
		{
		case Meme.MemeType.Alone:
			returnCode = 0;
			break;
		case Meme.MemeType.Brave:
			returnCode = 1;
			break;
		case Meme.MemeType.Cereal:
			returnCode = 2;
			break;
		case Meme.MemeType.Derpina:
			returnCode = 3;
			break;
		case Meme.MemeType.Fap:
			returnCode = 4;
			break;
		case Meme.MemeType.Gusta:
			returnCode = 5;
			break;
		case Meme.MemeType.Indiferent:
			returnCode = 6;
			break;
		case Meme.MemeType.Lol:
			returnCode = 7;
			break;
		case Meme.MemeType.Rage:
			returnCode = 8;
			break;
		case Meme.MemeType.Troll:
			returnCode = 9;
			break;
		case Meme.MemeType.Creep:
			returnCode = 10;
			break;
		case Meme.MemeType.Poker:
			returnCode = 11;
			break;
		}
		return memeSprites[returnCode];
	}
	
	private UtilityBag util = new UtilityBag();
	void Start () 
	{
		Instantiate(music, transform.position, transform.rotation);
		SpawnAllMemes();
		TILE_SIZE = util.GetRelativeWidth(80);
		gap = util.GetRelativeWidth(4);
		initialOffset = new Vector2(Screen.width/2 - (4* TILE_SIZE) - (4 * gap), Screen.height - (8 * TILE_SIZE) - (8 * gap) - util.GetRelativeHeigth(10) );
		stageClock.StartClock();
		PlayerPrefs.SetInt("currentScore", 0);
		playerPoints = PlayerPrefs.GetInt("currentScore");
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			isRunningOnMobile = true;
		else
			isRunningOnMobile = false;
	}
	
	
	public Texture2D background;
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), background);
		DrawAllMemes();
		DrawFloatingScores();
		DrawTimer();
		DrawScore();
		DrawControlButtons();
	}
	
	
	private bool isGamePaused = false;
	public GUIStyle pausedStyle;
	public GUIStyle resumeStyle;
	public GUIStyle quitStyle;
	private void DrawControlButtons()
	{
		/*
		if (isGamePaused)
		{
			if (GUI.Button(new Rect(Screen.width -  util.GetRelativeWidth(120), Screen.height/2 - util.GetRelativeHeigth(110), util.GetRelativeWidth(100), util.GetRelativeHeigth(100)), "", resumeStyle))
			{
				isGamePaused = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(Screen.width - util.GetRelativeWidth(120), Screen.height/2 - util.GetRelativeHeigth(110), util.GetRelativeWidth(100), util.GetRelativeHeigth(100)), "", pausedStyle))
			{
				isGamePaused = true;
			}
		}
		*/
		if (GUI.Button(new Rect(Screen.width - util.GetRelativeWidth(75), Screen.height - util.GetRelativeHeigth(75), util.GetRelativeWidth(70), util.GetRelativeHeigth(70)), "", quitStyle))
		{
			Application.LoadLevel("Menu");
		}

	}
	
	public GUIStyle pointsStyle;
	public GUIStyle pointsStyleAndroid;
	
	public GUIStyle scoreStyle;
	public GUIStyle scoreStyleAndroid;
	private Clock stageClock = new Clock();
	private void DrawTimer()
	{
		if (Screen.width >= 960)
			GUI.Label(new Rect(Screen.width - util.GetRelativeWidth(250), util.GetRelativeHeigth(4), util.GetRelativeWidth(240), util.GetRelativeHeigth(120)), stageClock.GetTime(), pointsStyle);
		else
			GUI.Label(new Rect(Screen.width - util.GetRelativeWidth(250), util.GetRelativeHeigth(4), util.GetRelativeWidth(240), util.GetRelativeHeigth(120)), stageClock.GetTime(), pointsStyleAndroid);
	}
	
	private void DrawScore()
	{
		if (Screen.width >= 960)
			GUI.Label(new Rect(util.GetRelativeWidth(10), util.GetRelativeHeigth(4), util.GetRelativeWidth(600), util.GetRelativeHeigth(120)), "SCORE: " + playerPoints.ToString(), scoreStyle);
		else
			GUI.Label(new Rect( util.GetRelativeWidth(10), util.GetRelativeHeigth(4), util.GetRelativeWidth(600), util.GetRelativeHeigth(120)), "SCORE: " + playerPoints.ToString(), scoreStyleAndroid);
	}
	
	private void GetInput()
	{
		if (Input.GetMouseButtonUp(0) && somePieceIsSelected == false) //selecting the meme
		{
			foreach (Meme m in memeList)
			{
				if (m.isKilled)
					continue;
				
				if (new Rect(initialOffset.x + (m.position.x * TILE_SIZE) + (m.position.x * gap) , initialOffset.y + (m.position.y * TILE_SIZE) + (m.position.y * gap), TILE_SIZE, TILE_SIZE ).Contains(new Vector2(Input.mousePosition.x,Screen.height -  Input.mousePosition.y)) )
				{
					m.isSelected = true;
					somePieceIsSelected = true;
					break;
				}
			}
		}
		else if (Input.GetMouseButtonUp(0) && somePieceIsSelected) //releasing the meme
		{
			foreach (Meme m in memeList)
			{
				if (new Rect(initialOffset.x + (m.position.x * TILE_SIZE) + (m.position.x * gap) , initialOffset.y + (m.position.y * TILE_SIZE) + (m.position.y * gap), TILE_SIZE, TILE_SIZE ).Contains(new Vector2(Input.mousePosition.x,Screen.height -  Input.mousePosition.y)) )
				{
					Vector2 cachePosition = memeList[selectedIdCache].position;
					memeList[selectedIdCache].isSelected = false;
					memeList[selectedIdCache].position = m.position;
					m.isKilled = true;
					m.position = cachePosition;
					somePieceIsSelected = false;
					SetNeedToFall(m.position);
					m.position = new Vector2(m.position.x, 0);
					isInputAllowed = false;
					break;
				}
			}
		}
	}
	
	private Vector2 touchStartPosition;
	private Vector2 touchEndPosition;
	private Vector2 testPosition;
	private void GetTouchInput()
	{

		if (isInputAllowed)
		{
			/*
			if (Input.touchCount > 0)
			{
				testPosition = Input.touches[0].position;
			}
			*/
			if (somePieceIsSelected == false)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					touchStartPosition = Input.touches[0].position;
					foreach (Meme m in memeList)
					{
						if (m.isKilled)
							continue;
						
						if (new Rect(initialOffset.x + (m.position.x * TILE_SIZE) + (m.position.x * gap) , initialOffset.y + (m.position.y * TILE_SIZE) + (m.position.y * gap), TILE_SIZE, TILE_SIZE ).Contains(new Vector2(touchStartPosition.x, Screen.height - touchStartPosition.y)) )
						{
							m.isSelected = true;
							somePieceIsSelected = true;
							break;
						}
					}
				}
			}
			else
			{
				if (Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					touchEndPosition = Input.touches[0].position;
				}
				else if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					foreach (Meme m in memeList)
					{
						if (new Rect(initialOffset.x + (m.position.x * TILE_SIZE) + (m.position.x * gap) , initialOffset.y + (m.position.y * TILE_SIZE) + (m.position.y * gap), TILE_SIZE, TILE_SIZE ).Contains(new Vector2(touchEndPosition.x, Screen.height - touchEndPosition.y)) )
						{
							Vector2 cachePosition = memeList[selectedIdCache].position;
							memeList[selectedIdCache].isSelected = false;
							memeList[selectedIdCache].position = m.position;
							m.isKilled = true;
							m.position = cachePosition;
							somePieceIsSelected = false;
							SetNeedToFall(m.position);
							m.position = new Vector2(m.position.x, 0);
							isInputAllowed = false;
							break;
						}
					}
				}
			}
		}
	}
	
	private int GetMemeByPosition(Vector2 comparePosition)
	{
		for (int i = 0; i < memeList.Count; i++)
		{
			if (memeList[i].position == comparePosition)
			{
				return i;
			}
		}
		//Debug.Log("returning wrongly here...");
		return 0;
	}
	
	private List<int> fallingMemes = new List<int>();
	private void SetNeedToFall(Vector2 fallPivot)
	{
		fallingMemes.Clear();
		if (fallPivot.y > 0)
		{
			for (int y = (int)fallPivot.y - 1 ; y >= 0; y--)
			{
				int id = GetMemeByPosition(new Vector2(fallPivot.x, y));
				memeList[id].SetToFall(new Vector2(initialOffset.x + (memeList[id].position.x * TILE_SIZE) + (memeList[id].position.x * gap) , initialOffset.y + (memeList[id].position.y * TILE_SIZE) + (memeList[id].position.y * gap)) , new Vector2(initialOffset.x + (memeList[id].position.x * TILE_SIZE) + (memeList[id].position.x * gap) , initialOffset.y + (memeList[id].position.y * TILE_SIZE) + (memeList[id].position.y * gap) + TILE_SIZE + gap));
				fallingMemes.Add(id);
			}
		}
	}
	
	private bool isInputAllowed = true;
	private bool CheckForFallingMemes()
	{
		if (fallingMemes.Count > 0)
		{
			foreach (int i in fallingMemes)
			{
				if (!memeList[i].isFalling)
				{
					fallingMemes.Remove(i);
				}
			}
			return false;
		}
		else
		{
			return true;
		}
	}
	
	private int playerPoints;
	private List<FloatingScore> fScores = new List<FloatingScore>();
	void Update() 
	{
		foreach (Meme m in memeList)
		{
			m.UpdateMeme();
		}
		
		
		
		foreach (FloatingScore fs in fScores)
		{
			fs.UpdateEffect();
			if(fs.expired)
			{
				fScores.Remove(fs);
			}
		}
		
		stageClock.UpdateClock();
		if (stageClock.TimeOut())
		{
			EndGame();
		}
		
		if (!isInputAllowed)
		{
			if (CheckForFallingMemes())
				RunMatchLogic();
		}
		else
		{
			if (isRunningOnMobile)
			{
				if (Input.touchCount > 0)
					GetTouchInput();
			}
			else
			{
				GetInput();
			}
		}
		
	}
	
	private void EndGame()
	{
		PlayerPrefs.SetInt("currentScore", playerPoints);
		PlayerPrefs.SetString("hasPlayedOnce","true");
		Application.LoadLevel("GameOver");
	}
	
	private bool hadToRespawn;
	private void RespawnKilledMemes()
	{
		hadToRespawn = false;
		for (int i = 0; i < memeList.Count; i ++)
		{
			if (memeList[i].isKilled)
			{
				memeList[i] = new Meme(memeList[i].position, memeList[i].uniqueId);
				hadToRespawn = true;
			}
		}
		
		if (hadToRespawn)
		{
			RunMatchLogic();
		}
		else
		{
			isInputAllowed = true;
		}
	}
	
	private void RecursiveExtraMatchHorizontalRight(Meme baseMeme)
	{
		if (baseMeme.position.x <= 6 )
		{
			int next = GetMemeByPosition(new Vector2(baseMeme.position.x + 1, baseMeme.position.y));
			if (baseMeme.type == memeList[next].type && !memeList[next].isKilled)
			{
				fScores.Add(new FloatingScore(new Vector2((memeList[next].position.x * TILE_SIZE) + initialOffset.x, (memeList[next].position.y * TILE_SIZE) + initialOffset.y), memeList[next].points));
				playerPoints += memeList[next].points;
				memeList[next].isKilled = true;
				RecursiveExtraMatchHorizontalRight(memeList[next]);
			}
		}
	}
	private void RecursiveExtraMatchHorizontalLeft(Meme baseMeme)
	{
		if (baseMeme.position.x >= 1 )
		{
			int next = GetMemeByPosition(new Vector2(baseMeme.position.x - 1, baseMeme.position.y));
			if (baseMeme.type == memeList[next].type && !memeList[next].isKilled)
			{
				fScores.Add(new FloatingScore(new Vector2((memeList[next].position.x * TILE_SIZE) + initialOffset.x, (memeList[next].position.y * TILE_SIZE) + initialOffset.y), memeList[next].points));
				playerPoints += memeList[next].points;
				memeList[next].isKilled = true;
				RecursiveExtraMatchHorizontalLeft(memeList[next]);
			}
		}
	}
	
	private void RecursiveExtraMatchVerticalRight(Meme baseMeme)
	{
		if (baseMeme.position.y <= 6 )
		{
			int next = GetMemeByPosition(new Vector2(baseMeme.position.x, baseMeme.position.y + 1));
			if (baseMeme.type == memeList[next].type && !memeList[next].isKilled)
			{
				fScores.Add(new FloatingScore(new Vector2((memeList[next].position.x * TILE_SIZE) + initialOffset.x, (memeList[next].position.y * TILE_SIZE) + initialOffset.y), memeList[next].points));
				playerPoints += memeList[next].points;
				memeList[next].isKilled = true;
				RecursiveExtraMatchVerticalRight(memeList[next]);
			}
		}
	}
	private void RecursiveExtraMatchVerticalLeft(Meme baseMeme)
	{
		if (baseMeme.position.y >= 1 )
		{
			int next = GetMemeByPosition(new Vector2(baseMeme.position.x, baseMeme.position.y - 1));
			if (baseMeme.type == memeList[next].type && !memeList[next].isKilled)
			{
				fScores.Add(new FloatingScore(new Vector2((memeList[next].position.x * TILE_SIZE) + initialOffset.x, (memeList[next].position.y * TILE_SIZE) + initialOffset.y), memeList[next].points));
				playerPoints += memeList[next].points;
				memeList[next].isKilled = true;
				RecursiveExtraMatchVerticalLeft(memeList[next]);
			}
		}
	}
	
	private void RunMatchLogic()
	{
		foreach (Meme m in memeList)
		{
			if (m.isKilled)
			{
				continue;
			}
			else
			{
				if (m.position.x <= 5 && !m.isKilled)
				{
				
					int id1 = GetMemeByPosition(new Vector2(m.position.x + 1, m.position.y));
					int id2 = GetMemeByPosition(new Vector2(m.position.x + 2, m.position.y));
					if (m.type == memeList[id1].type && m.type == memeList[id2].type && !memeList[id1].isKilled && !memeList[id2].isKilled)
					{
						m.isKilled = true;
						fScores.Add(new FloatingScore(new Vector2((m.position.x * TILE_SIZE) + initialOffset.x, (m.position.y * TILE_SIZE) + initialOffset.y), m.points));
						playerPoints += m.points;
						memeList[id1].isKilled = true;
						fScores.Add(new FloatingScore(new Vector2((memeList[id1].position.x * TILE_SIZE) + initialOffset.x, (memeList[id1].position.y * TILE_SIZE) + initialOffset.y), memeList[id1].points));
						playerPoints += memeList[id1].points;
						memeList[id2].isKilled = true;
						fScores.Add(new FloatingScore(new Vector2((memeList[id2].position.x * TILE_SIZE) + initialOffset.x, (memeList[id2].position.y * TILE_SIZE) + initialOffset.y), memeList[id2].points));
						playerPoints += memeList[id2].points;
						
						RecursiveExtraMatchHorizontalRight(memeList[id2]);
						RecursiveExtraMatchHorizontalLeft(m);
		
					}
				}
				if (m.position.y <= 5 && !m.isKilled)
				{
				
					int id3 = GetMemeByPosition(new Vector2(m.position.x, m.position.y + 1));
					int id4 = GetMemeByPosition(new Vector2(m.position.x, m.position.y + 2));
					if (m.type == memeList[id3].type && m.type == memeList[id4].type && !memeList[id3].isKilled && !memeList[id4].isKilled)
					{
						m.isKilled = true;
						fScores.Add(new FloatingScore(new Vector2((m.position.x * TILE_SIZE) + initialOffset.x, (m.position.y * TILE_SIZE) + initialOffset.y), m.points));
						playerPoints += m.points;
						memeList[id3].isKilled = true;
						fScores.Add(new FloatingScore(new Vector2((memeList[id3].position.x * TILE_SIZE) + initialOffset.x, (memeList[id3].position.y * TILE_SIZE) + initialOffset.y), memeList[id3].points));
						playerPoints += memeList[id3].points;
						memeList[id4].isKilled = true;
						fScores.Add(new FloatingScore(new Vector2((memeList[id4].position.x * TILE_SIZE) + initialOffset.x, (memeList[id4].position.y * TILE_SIZE) + initialOffset.y), memeList[id4].points));
						playerPoints += memeList[id4].points;
						
						RecursiveExtraMatchVerticalRight(memeList[id4]);
						RecursiveExtraMatchVerticalLeft(m);
						
					}
				}
			}
		}
		RespawnKilledMemes();
	}
}
