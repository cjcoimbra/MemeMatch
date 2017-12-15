using UnityEngine;
using System.Collections;

public class UtilityBag  {

	private const int MAX_WIDTH = 1024;
	private const int MAX_HEIGTH = 768;
	private const string RANKING_URL = "http://www.spacecatstudio.com/utils/memematch/get_ranking.php";
	private const string UPDATE_RANKING_URL = "http://www.spacecatstudio.com/utils/memematch/send_ranking.php?username=MemeMatch&score=";
	
	public float GetRelativeWidth(int original_w)
	{
		return Screen.width * (((original_w * 100.0f)/MAX_WIDTH)/100.0f);
	}
	
	public float GetRelativeHeigth(int original_h)
	{
		return Screen.height * (((original_h * 100.0f)/MAX_HEIGTH)/100.0f);
	}
	
	public string rankingData = "?";
	public WWW request;
	public IEnumerator GetRanking()
	{

  		request = new WWW(RANKING_URL);
		yield return request;
		//Debug.Log("<<" + request.text + ">>");

		if (request.text == "?")
		{
			rankingData = request.text;
		}
		else if (int.Parse(request.text) >= 0)
		{
			rankingData = request.text;
		}
		else
		{
			rankingData = "?";
		}
		
	}
	
	public IEnumerator UpdateRanking(string newRecord)
	{
		Debug.Log("Trying to update ranking");
  		request = new WWW(UPDATE_RANKING_URL + newRecord);
		yield return request;
		if (request.text == "OK")
		{
			Debug.Log("Ranking updated with success");
		}
		else
		{
			Debug.Log("Ranking update failed");
		}
		
	}

}
