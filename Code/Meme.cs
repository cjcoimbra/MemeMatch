using UnityEngine;
using System.Collections;

public class Meme {
	
	public bool isKilled;
	public Meme(Vector2 p, int id)
	{
		uniqueId = id;
		isFalling = false;
		isSelected = false;
		isKilled = false;
		position = p;
		int typeRandom = Random.Range(0, 12);
		if (typeRandom == 0)
		{
			type = MemeType.Indiferent;
			points = 10;
		}
		else if (typeRandom == 1)
		{
			type = MemeType.Derpina;
			points = 15;
		}
		else if (typeRandom == 2)
		{
			type = MemeType.Brave;
			points = 20;
		}
		else if (typeRandom == 3)
		{
			type = MemeType.Fap;
			points = 25;
		}
		else if (typeRandom == 4)
		{
			type = MemeType.Cereal;
			points = 30;
		}
		else if (typeRandom == 5)
		{
			type = MemeType.Alone;
			points = 35;
		}
		else if (typeRandom == 6)
		{
			type = MemeType.Lol;
			points = 40;
		}
		else if (typeRandom == 7)
		{
			type = MemeType.Rage;
			points = 45;
		}
		else if (typeRandom == 8)
		{
			type = MemeType.Gusta;
			points = 50;
		}
		else if (typeRandom == 9)
		{
			type = MemeType.Troll;
			points = 60;
		}
		else if (typeRandom == 10)
		{
			type = MemeType.Creep;
			points = 35;
		}
		else if (typeRandom == 11)
		{
			type = MemeType.Poker;
			points = 30;
		}
	}
	
	public int uniqueId;
	public bool isFalling;
	public bool isSelected;
	public Vector2 position;
	public MemeType type;
	public int points;
	public enum MemeType
	{
		Cereal,
		Fap,
		Brave,
		Indiferent,
		Rage,
		Derpina,
		Alone,
		Lol,
		Gusta,
		Troll,
		Creep,
		Poker
	}
	
	private Vector2 fallStart;
	private Vector2 fallEnd;
	public Vector2 currentFallSituation;
	public void SetToFall(Vector2 fstart, Vector2 fend)
	{
		isFalling = true;
		fallStart = fstart;
		currentFallSituation = fallStart;
		fallEnd = fend;
	}
	
	private float fallSpeed = 160.0f;
	public void UpdateMeme()
	{
		if (isFalling)
		{
			if (currentFallSituation != fallEnd)
				currentFallSituation = Vector2.MoveTowards(currentFallSituation, fallEnd, fallSpeed * Time.deltaTime);
			else
			{
				isFalling = false;
				position = new Vector2(position.x, position.y + 1);
			}
		}
	}
}
