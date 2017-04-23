using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject target;
	public Vector3 cameraOffset = new Vector3(0f, 4, -2f);
	public float speed = 5f;

	private Camera cam;
	private float cameraAngleX;
	private float cameraAngleY;
	private float targetAngleX;
	private float targetAngleY;

	private float minDistance = 1f;
	private float maxDistance;
	private float distance;


	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		targetAngleY = cam.transform.eulerAngles.y;
		targetAngleX = cam.transform.eulerAngles.x;

		maxDistance = cameraOffset.magnitude;
		distance = maxDistance;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		//check for null ref
		if (cam != null && target != null)
		{
			Color debugColor = Color.cyan;

			Vector3 targetPos = target.transform.position;
			Vector3 offset = cameraOffset;

			float xInput = Input.GetAxis("Mouse Y");
			float yInput = Input.GetAxis("Mouse X");

			cameraAngleX = cam.transform.eulerAngles.x;
			cameraAngleY = cam.transform.eulerAngles.y;
			targetAngleX -= (xInput*2) % 360;
			targetAngleY += (yInput*2) % 360;
			targetAngleX = Mathf.Clamp(targetAngleX,-50f, 30);

			targetAngleY = Mathf.LerpAngle(cameraAngleY, targetAngleY, 1f);
			targetAngleX = Mathf.LerpAngle(cameraAngleX, targetAngleX, 1f);
			offset = Quaternion.Euler(targetAngleX, targetAngleY, 0f) * offset;

			//check if camera should collide with wall
			float distance_ = cameraOffset.magnitude;
			RaycastHit rayHit;
			if (Physics.SphereCast (target.transform.position, 0.5f, offset.normalized, out rayHit, offset.magnitude)) 
			{
				if (rayHit.collider.transform.tag == "Wall")
				{
					distance_ = Mathf.Clamp(rayHit.distance, minDistance, maxDistance);
					debugColor = Color.red;
				}
			}
			Debug.DrawLine(transform.position, targetPos,debugColor,Time.deltaTime,false);

			if (distance >= distance_) {
				distance = distance_;
			} else {
				//Smooth camera distance movement if returning to normal distance
				distance = Mathf.Lerp(distance, distance_, 1.25f*Time.deltaTime);
			}

			offset = offset.normalized * distance;
			cam.transform.position = targetPos + offset;
			cam.transform.LookAt(targetPos);
		}
	}
}
