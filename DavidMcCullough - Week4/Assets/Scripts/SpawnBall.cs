using UnityEngine;
using System.Collections;

public class SpawnBall : MonoBehaviour {

	public GameObject ballPrefab;

	void Update ()
	{
		SpawnOnClick ();	
	}

	void SpawnOnClick()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Debug.Log ("fire");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000f))
			{
				if (hit.collider.tag == "SpawnArea")
				{
					Debug.Log ("spawn ball");
					Instantiate (ballPrefab, hit.point+ (hit.normal/2), Quaternion.identity);
				}
			}
		}
	}
}
