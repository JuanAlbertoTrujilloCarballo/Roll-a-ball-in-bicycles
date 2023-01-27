using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BallController : MonoBehaviour
{
    public float speed = 0;
    private int count;
    //public float cameraSpeed = 0;

    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    //public GameObject player;

    private float movementX;
    private float movementY;
    //private float movementCameraX;
    //private float movementCameraY;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        setCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 3)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

   /*
    private void CameraMove(InputValue lookValue)
    {
        Vector2 lookVector = lookValue.Get<Vector2>();

        movementCameraX = lookVector.x;
        movementCameraY = lookVector.y;
    }
   */

    private void changeGravity(InputValue inputValue)
    {
        if (inputValue.Equals("V"))
        {
            Physics.gravity = new Vector3(0, 9.8f, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //Vector3 cameraMovement = new Vector3(movementCameraX, movementCameraY, 0.0f);

        rb.AddForce(movement * speed);
        //transform.Rotate(cameraMovement);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            setCountText();
        }
    }



}