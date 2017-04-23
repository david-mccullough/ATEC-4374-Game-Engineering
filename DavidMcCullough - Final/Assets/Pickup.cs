using UnityEngine;

public enum PickupType
{
	coin,
	gem,
	xp,
	xpCase
}

public class Pickup : MonoBehaviour {

	public PickupType type;
	private Vector3 velocity = Vector3.zero;

	public GameManager manager;
	public GameObject xpObject;
	private Transform player;
	private float speed = .01f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Transform>();
		if (type == PickupType.xp)
			velocity = new Vector3(Random.Range(-1f,1f),Random.Range(0f,1f),Random.Range(-1f,1f));
	}

	// Update is called once per frame
	void Update () {
		if (type == PickupType.xp)
		{
			FloatToward(player);
		}
		else
		{
			transform.Rotate(180*Vector3.forward * Time.deltaTime);
		}
		transform.position += velocity;
	}

	/*void OnCollisionEnter(Collision hit)
	{
		if (hit.transform.tag == "Player")
		{
			manager.Collect(type);
			//feedback here
			switch (type)
			{
				case PickupType.coin:
					
					Destroy(this.gameObject);
				break;

				case PickupType.gem:
					Destroy(this.gameObject);
				break;

				case PickupType.xp:
					Destroy(this.gameObject);
				break;

				case PickupType.xpCase:
					int i = (int) Mathf.Round(Random.Range(4f,6f));
					while (i > 0)
					{
						Pickup orb = Instantiate(xpObject, transform.position, transform.rotation).GetComponent<Pickup>();
						orb.velocity = new Vector3(Random.Range(-1f,1f),Random.Range(0f,1f),Random.Range(-1f,1f));
					}
					Destroy(this.gameObject);
				break;
			}
		}
	}*/

	private void FloatToward(Transform t)
	{
		speed = Mathf.Clamp(speed*1.02f, 0f, .75f);

		Vector3 target = t.position;
		transform.position = Vector3.MoveTowards(transform.position, target, speed);
		/*Vector3 accelDir = t.position - transform.position;
		float projVel = Vector3.Dot(velocity, accelDir); // Vector projection of Current velocity onto accelDir.
		float accelVel = .05f * Time.deltaTime; // Accelerated velocity in direction of movment

		// If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
		if(projVel + accelVel > .5f)
			accelVel =.5f - projVel;

		Vector3 newVel = velocity + accelDir * accelVel;

		Vector3 tempVel = new Vector3(newVel.x, 0f, newVel.z);
		gameObject.transform.LookAt(gameObject.transform.position, Vector3.up);

		velocity = velocity + accelDir * accelVel;*/
	}

}
