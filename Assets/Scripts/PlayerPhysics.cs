using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	private BoxCollider coll;
	private Vector3 size;
	private Vector3 pos;
	
	[HideInInspector]
	public bool grounded;

	private Ray ray;
	private RaycastHit hit;
	private float bubble = .005f;
	public LayerMask collidesWith;

	void Start()
	{
		coll = GetComponent<BoxCollider>();
		size = coll.size;
		pos = coll.center;
	}

	public void Move(Vector2 moveamt)
	{
		float dX = moveamt.x;
		float dY = moveamt.y;
		Vector2 p = transform.position;
		float dir = Mathf.Sign(dY);		
		grounded = false;
		for (int i=0; i<3; i++)
		{
			float x = (p.x + pos.x + size.x/2) + size.x/2 *i;
			float y = p.y + pos.y + size.y/2 * dir;
			
			ray = new Ray(new Vector2(x,y), new Vector2(0, dir));
			if(Physics.Raycast(ray, out hit, Mathf.Abs(dY), collidesWith))
			{
				float dst = Vector3.Distance(ray.origin, hit.point);
				if(dst > bubble)
				{
					dY = dst*dir + bubble;
				}			
				else
				{
					dY = 0;
				}
				
				grounded = true;
				break;
			}
		}
		transform.Translate(new Vector2(dX, dY));
	}
}
