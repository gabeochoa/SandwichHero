using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	public float gravity = 20;
	public float speed = 2;
	public float accel = 64;
	public float jumpH = 4;
	
	private float curspeed;
	private float tarspeed;
	
	private Vector2 toMove;
	
	private PlayerPhysics playerPhys;
	// Use this for initialization
	void Start () {
		playerPhys = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void Update () {
		tarspeed = Input.GetAxisRaw("Horizontal") * speed;
		curspeed = IncrementTowards(curspeed, tarspeed, accel);
		
		if(playerPhys.grounded)
		{
			toMove.y = 0;
			if(Input.GetAxisRaw("Vertical") != 0 || Input.GetButton("Jump"))
			{
				toMove.y = jumpH;
			}			
		}
		
		toMove.x = curspeed;
		toMove.y -= gravity * Time.deltaTime;
		playerPhys.Move(toMove * Time.deltaTime);
	}
	
	private float IncrementTowards(float c, float t, float tween)
	{
		if(c == t)
		{
			return c;
		}
		else
		{
			float dir = Mathf.Sign(t-c);
			c += tween * Time.deltaTime * dir;
			return (dir == Mathf.Sign(t-c))? c: t;
		}
	}
}
