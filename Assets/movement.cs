using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float m_Thrust = 9f;
    public Canvas parentCanvas;
    public Material lineMaterial;

    Vector3 start_mouse;
    int jumps_left;
    float deltaX;
    float deltaY;

    Vector3 startPosition;
    GameObject currentLineObject;
    LineRenderer currentLineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        jumps_left = 1;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        jumps_left = 1;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        jumps_left = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            start_mouse = Input.mousePosition;
            StartDrawingLine();

        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            deltaX = ((start_mouse.x - mousePos.x) / Screen.width) * 2;
            deltaY = ((start_mouse.y - mousePos.y) / Screen.height) * 2;
            deltaX = Mathf.Min(deltaX, 0.8f);
            deltaY = Mathf.Min(deltaY, 0.8f);

            PreviewLine();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            transform.position = new Vector3(16.5f, 33.5f, 0);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            
            if (jumps_left > 0)
            {
                Vector3 mousePos = Input.mousePosition;
                float deltaX = ((start_mouse.x - mousePos.x)/ Screen.width)*2;
                float deltaY = ((start_mouse.y - mousePos.y)/ Screen.height)*2;
                deltaX = Mathf.Min(deltaX, 0.8f);
                deltaY = Mathf.Min(deltaY, 0.8f);
                Debug.Log("X: "+deltaX+ " Y: " + deltaY);
                if (deltaX+deltaY != 0)
                {
                    rb.velocity = new Vector2(m_Thrust * deltaX, m_Thrust * deltaY);
                    jumps_left = jumps_left - 1;
                }
                
            }

            EndDrawingLine();
        }

        if (transform.position.x < -8.5)
        {
            transform.position = new Vector3((float) -8.5, transform.position.y, transform.position.z);
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        Debug.DrawLine(start, end, color);
    }

    void StartDrawingLine()
    {
        //startPosition = GetMousePosition();
        startPosition = transform.position;
        currentLineObject = new GameObject();
        currentLineObject.transform.position = startPosition;
        currentLineRenderer = currentLineObject.AddComponent<LineRenderer>();
        currentLineRenderer.material = lineMaterial;
        currentLineRenderer.startWidth = (float) 0.1;
        currentLineRenderer.endWidth = (float)0.1;
    }

    void PreviewLine()
    {
        if (jumps_left == 0)
        {
            currentLineRenderer.startColor = Color.red;
            currentLineRenderer.endColor = Color.red;
        } else
        {
            currentLineRenderer.startColor = Color.green;
            currentLineRenderer.endColor = Color.green;
        }
        currentLineRenderer.SetPositions(new Vector3[] { transform.position, new Vector3(transform.position.x + (3*deltaX), transform.position.y + (3 * deltaY), transform.position.z) });
    }

    void EndDrawingLine()
    {
        //currentLineRenderer.SetPositions(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0) });
        Destroy(currentLineObject);
        startPosition = Vector3.zero;
        currentLineObject = null;
        currentLineRenderer = null;
    }
}
