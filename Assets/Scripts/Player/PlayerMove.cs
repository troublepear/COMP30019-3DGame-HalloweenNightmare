using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float moveSpeed = 8.0f;
	public float rotateSpeed = 8.0f;
	public float jumpVelocity = 3.0f;

	float minMouseRotateX = -45.0f;
	float maxMouseRotateX = 45.0f;
	float mouseRotateX;
	bool isGrounded;

	Camera myCamera;
	Animator anim;
	Rigidbody rigid;
	CapsuleCollider capsuleCollider;
	PlayerHealth playerHealth;

	void Start(){
		myCamera = Camera.main;
		mouseRotateX = myCamera.transform.localEulerAngles.x;
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		playerHealth = GetComponent<PlayerHealth> ();
	}
	void FixedUpdate(){
		Debug.Log(moveSpeed);
		if (!playerHealth.isAlive)
			return;
		checkGround();
		if (isGrounded == false && anim != null)
			anim.SetBool ("isJump", false);
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		Move(h, v);

		float rv = Input.GetAxisRaw("Mouse X");
		float rh = Input.GetAxisRaw("Mouse Y");
		Rotate(rh, rv);

		Jump(isGrounded);
	}
	void checkGround()
	{            
		RaycastHit hitInfo;
		float shellOffset = 0.01f;
		float groundCheckDistance = 0.01f;
		Vector3 currentPos = transform.position;
		currentPos.y += capsuleCollider.height / 2f;
		if (Physics.SphereCast (currentPos, capsuleCollider.radius * (1.0f - shellOffset), Vector3.down, out hitInfo,
			    ((capsuleCollider.height / 2f) - capsuleCollider.radius) + groundCheckDistance, ~0, QueryTriggerInteraction.Ignore)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

	}
	void Jump(bool isGround){
		
		if (Input.GetButtonDown ("Jump") && isGround) {
			rigid.AddForce (Vector3.up * jumpVelocity, ForceMode.VelocityChange);
			if (anim != null)
				anim.SetBool ("isJump", true);
		} else {
			if(anim != null)
				anim.SetBool ("isJump", false);
		}
	}
	void Update()
	{
		if (!playerHealth.isAlive) 
			return;
		
		if (GameManager.gm.gameState != GameManager.GameState.Playing)
			return;
		
		
	}
	void Move(float h,float v){
		transform.Translate ((Vector3.forward * v + Vector3.right * h) * moveSpeed * Time.deltaTime);
		if (h != 0.0f || v != 0.0f) {
			if (anim != null)
				anim.SetBool ("isMove", true);
		} else {
			if (anim != null)
				anim.SetBool ("isMove", false);
		}
	}
	void Rotate(float rh,float rv){
		transform.Rotate (0, rv * rotateSpeed, 0);
		mouseRotateX -= rh * rotateSpeed;
		mouseRotateX = Mathf.Clamp (mouseRotateX, minMouseRotateX, maxMouseRotateX);
		myCamera.transform.localEulerAngles = new Vector3 (mouseRotateX, 0.0f, 0.0f);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obsticle")
        {	
			Debug.Log("Obsticle enter");
			moveSpeed = 0.3f;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
		if (collision.gameObject.tag == "Obsticle")
		{
			Debug.Log("Obsticle leave");
			moveSpeed = 8.0f;
		}
	}
}






















