using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float speed = 0;
    private int count;
    //public float cameraSpeed = 0;

    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private string actualScene;
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
        actualScene = Application.loadedLevelName;
        
        Debug.Log("escena actual: "+actualScene);
        if (!GameObject.FindGameObjectWithTag("PickUp"))
        {   
            if (actualScene.Equals("level1"))
            {
                SceneManager.LoadScene(1);
            }
            else if (actualScene.Equals("level2"))
            {
                SceneManager.LoadScene(2);
            }
            else if (actualScene.Equals("level3"))
            {
                SceneManager.LoadScene(3);
            }
            else if (actualScene.Equals("level4"))
            {
                winTextObject.SetActive(true);
            }

            // Set the text value of your 'winText'


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
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //Vector3 cameraMovement = new Vector3(movementCameraX, movementCameraY, 0.0f);

        rb.AddForce(movement * speed);
        //transform.Rotate(cameraMovement);

        if (Input.GetKeyDown("space"))
        {
            Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
            GetComponent<Rigidbody>().AddForce(jump);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            setCountText();
        }

        if (other.gameObject.CompareTag("Gravity"))
        {
            other.gameObject.SetActive(false);
            Physics.gravity = new Vector3(0, 9.8f, 0);
        }
    }



}