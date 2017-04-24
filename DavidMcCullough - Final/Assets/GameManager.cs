using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int coinCount = 0;
	public int gemCount = 0;
	public int xpCount = 0;

	public int xpLevel = 0;
	private int levelUpThreshold = 15;

	public GameObject sndLevelUp;
	public GameObject sndXPCase;
	public GameObject sndXP;
	public GameObject sndCoin;
	public GameObject sndGem;

	public Slider uiXPBar;
	public Text uiXPLevel;
	public Text uiCoins;
	public Text uiGems;
	public Text uiWinText;

	public void Collect(PickupType type)
	{
		// increment respective collectables
		switch (type)
		{
		case PickupType.coin:
			coinCount++;
			uiCoins.text = "Coins: " + coinCount;
		break;

		case PickupType.gem:
			gemCount++;
			uiGems.text = "Gems: " + gemCount + "/5";
			if (gemCount>=5)
				uiWinText.enabled = true;
		break;

		case PickupType.xp:
			xpCount++;
			uiXPBar.value = ((float) xpCount)/((float)levelUpThreshold);
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
		// increment xp, if we reach the level up capacity reset the count and increment level
		xpLevel++;
		xpCount = 0;
		levelUpThreshold = levelUpThreshold + levelUpThreshold/3;
		uiXPBar.value = ((float) xpCount)/((float)levelUpThreshold);
		GameObject player = GameObject.Find("Player");
		Instantiate(sndLevelUp, player.transform.position, player.transform.rotation);

		uiXPLevel.text = "Level " + xpLevel;
	}
}
