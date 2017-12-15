using UnityEngine;
using System.Collections;

public class Clock {
	
	private const int TOTAL_SECONDS = 180;
	private int currentSeconds;
	private string formattedTime;
	public void StartClock()
	{
		currentSeconds = TOTAL_SECONDS;
		formattedTime = "3:00";
		lastTimeChange = Time.time;
	}
	
	private const float ONE_SECOND_INTERVAL = 1.0f;
	private float lastTimeChange;
	public void UpdateClock()
	{
		if (Time.time - lastTimeChange >= ONE_SECOND_INTERVAL)
		{
			currentSeconds--;
			if (currentSeconds % 60 >= 10)
				formattedTime = ((int)(currentSeconds/60)).ToString() + ":" + (currentSeconds % 60).ToString();
			else
				formattedTime = ((int)(currentSeconds/60)).ToString() + ":0" + (currentSeconds % 60).ToString();
			
			lastTimeChange = Time.time;
			
		}
	}
	
	public string GetTime()
	{
		return formattedTime;
	}
	
	public bool TimeOut()
	{
		if (currentSeconds <= 0)
			return true;
		else
			return false;
	}
	
}
