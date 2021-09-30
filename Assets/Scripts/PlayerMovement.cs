using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    private float speed = 3.0f;
    public bool grounded;
    private int dash = 1;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if(Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            m_Rigidbody.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.E) && grounded == false && dash == 1)
        {
            m_Rigidbody.AddForce(new Vector3(7, -7, 0), ForceMode.Impulse);
            m_Rigidbody.velocity = new Vector3(0, Mathf.Clamp(m_Rigidbody.velocity.y, -1, 500), 0);
            dash = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q) && grounded == false && dash == 1)
        {
            m_Rigidbody.AddForce(new Vector3(-7, -7, 0), ForceMode.Impulse);
            m_Rigidbody.velocity = new Vector3(0, Mathf.Clamp(m_Rigidbody.velocity.y, -1, 500), 0);
            dash = 0;
        }
        m_Rigidbody.MovePosition(transform.position + movement * Time.fixedDeltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            dash = 1;
        }
        if (collision.gameObject.tag == "Respawn")
        {
            SceneManager.LoadScene("Level_1");
        }
        if (collision.gameObject.tag == "End")
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
