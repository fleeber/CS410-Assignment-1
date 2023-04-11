using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; //text ui

public class playerController : MonoBehaviour
{
	public float speed = 0;
	public TextMeshProUGUI counttext;
	public GameObject winTextObject;
	
	private Rigidbody rb;
	private bool canDouble = false;
	private int count;
	private float movementX;
	private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
		winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(rb.position.y < 1 && !(Input.GetKey("space")))
			canDouble = false;
        if(Input.GetKeyDown("space"))
        {
			if(rb.position.y < 1)
            {
				rb.velocity = Vector3.up * 10;
				canDouble = true;
            }
			else if(canDouble)
            {
				rb.velocity = Vector3.up * 10;
				canDouble = false;
            }
		
			/*if (canDouble) //player in air and has not double jumped yet
			{
				//rb.velocity.y = 0;
				rb.AddForce(new Vector3(0, 4000 * Time.deltaTime, 0));
				canDouble = false;
			}
			if (rb.position.y < 1) //player is grounded
			{
				canDouble = true;
				rb.AddForce(new Vector3(0, 2000 * Time.deltaTime, 0));
			}*/

        }
		//if(Input.GetButtonUp("space") && rb.velocity.x, rb.velocity.)
    }
	void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
	}

	void SetCountText()
    {
		counttext.text = "count: " + count.ToString();
		if(count >= 5)
        {
			winTextObject.SetActive(true);
        }
    }
	
	void FixedUpdate() {
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);
	}

    private void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.CompareTag("pickup"))
        {
			other.gameObject.SetActive(false);
			count = count+1;

			
			SetCountText();
			//winTextObject.SetActive(false);
		}
		
    }
}
