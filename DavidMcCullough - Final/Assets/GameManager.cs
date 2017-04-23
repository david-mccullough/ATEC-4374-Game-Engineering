using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int coinCount = 0;
	public int gemCount = 0;
	public int xpCount = 0;

	public int xpLevel = 0;
	private int levelUpThreshold = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Collect(PickupType type)
	{
		//feedback here
		switch (type)
		{
		case PickupType.coin:
			coinCount++;
		break;

		case PickupType.gem:
			gemCount++;
		break;

		case PickupType.xp:
			xpCount++;
			if (xpCount == levelUpThreshold)
			{
				LevelUp();
			}
		break;

		case PickupType.xpCase:
			//do nothing
		break;
		}
	}

	public void LevelUp()
	{
		xpLevel++;
		xpCount = 0;
		levelUpThreshold = levelUpThreshold + levelUpThreshold/2;
	}
}
