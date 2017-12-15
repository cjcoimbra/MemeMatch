using UnityEngine;
using System.Collections;

public class FloatingScore  {

	// Use this for initialization
	public Vector2 position;
	public int points;
	public bool expired;
	private float expirationTimer;
	private float moveRate = 1.6f;
	private float lifeSpan = 1.5f;
	public string score;
		
	public FloatingScore(Vector2 pos, int p)
	{
		position = pos;
		points = p;
		expired = false;
		expirationTimer = Time.time;
		score = points.ToString();
		moveRate = Random.Range(0.8f, 1.6f);
		lifeSpan = Random.Range(1.0f, 1.8f);
	}
	
	public void UpdateEffect()
	{
		if (Time.time - expirationTimer >= lifeSpan)
		{
			expired = true;
		}
		else
		{
			position = new Vector2(position.x, position.y - moveRate);
		}
	}
}