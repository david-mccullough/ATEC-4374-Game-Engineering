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
	public GameObject sparkle;
	private Transform player;

	public float spinSpeed = 180f;
	public bool picked = false;
	private float speed = .01f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Transform>();
		if (type == PickupType.xp)
		{
			velocity = new Vector3(Random.Range(-.02f,.02f),0f,Random.Range(-.02f,.02f));
			transform.position += velocity.normalized*2;
			velocity += new Vector3(0f, Random.Range(0f,.02f), 0f);
		}
		
	}

	// Update is called once per frame
	void Update () {

		// xp orbs float toward player
		if (type == PickupType.xp)
		{
			FloatToward(player);
		}
		// gems shoot into the sky
		else if (type == PickupType.gem && picked)
		{
			FloatToward(manager.transform);
			spinSpeed+= 60*Time.deltaTime;
			transform.Rotate(spinSpeed*Vector3.forward * Time.deltaTime);
			if (Vector3.Distance(transform.position, manager.transform.position) < 1f)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			transform.Rotate(spinSpeed*Vector3.forward * Time.deltaTime);
		}
		transform.position += velocity;
	}
		
	private void FloatToward(Transform t)
	{
		//accelerate speed
		speed = Mathf.Clamp(speed*1.06f, 0f, .75f);

		Vector3 target = t.position;
		transform.position = Vector3.MoveTowards(transform.position, target, speed);
	}

}
