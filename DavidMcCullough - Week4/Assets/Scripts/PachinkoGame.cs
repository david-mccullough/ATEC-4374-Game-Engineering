using UnityEngine;

public class PachinkoGame : MonoBehaviour {

	public GameObject ballPrefab;
	public int score;
	public int ballCount = 10;

	private GUIText guiText;

	void Update ()
	{
		guiText = GetComponent<GUIText> ();
		SpawnOnClick ();
		DrawGUI ();
	}

	void SpawnOnClick()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000f))
			{
				if (hit.collider.tag == "SpawnArea" && ballCount > 0)
				{
					Debug.Log ("spawn ball");
					ballCount--;
					Instantiate (ballPrefab, hit.point+ (hit.normal/2), Quaternion.identity);
				}
			}
		}
	}

	void DrawGUI()
	{
		guiText.text = "Balls: " + ballCount + "\nScore: " + score;
	}
}
