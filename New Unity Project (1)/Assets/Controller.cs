using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //float rotSpeed = 20;
    // Start is called before the first frame update

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float rotationRate = 3.0f;
    [SerializeField] private bool xRotation;
    [SerializeField] private bool yRotation;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;
    [SerializeField] private bool touchAnywhere;
    private float m_previousX;
    private float m_previousY;
    private Camera m_camera;
    private bool m_rotating = false;

    private void Awake()
    {
        m_camera = Camera.main;
    }

    private void Update()
    {
        
        if (!touchAnywhere)
        {
            //No need to check if already rotating
            if (!m_rotating)
            {
                RaycastHit hit;
                Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit, 1000, targetLayer))
                {
                    return;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_rotating = true;
            m_previousX = Input.mousePosition.x;
            m_previousY = Input.mousePosition.y;
        }
        // get the user touch input
        if (Input.GetMouseButton(0) )
        {
            
            var touch = Input.mousePosition;
            var deltaX = -(Input.mousePosition.y - m_previousY) * rotationRate;
            var deltaY = -(Input.mousePosition.x - m_previousX) * rotationRate;
            if (!yRotation) deltaX = 0;
            if (!xRotation) deltaY = 0;
            if (invertX) deltaY *= -1;
            if (invertY) deltaX *= -1;
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("A");
                transform.Rotate(deltaX, deltaY, 0, Space.World);
            }           
            m_previousX = Input.mousePosition.x;
            m_previousY = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0))
            m_rotating = false;
    }
    /*if (Input.GetMouseButton(0))
          {
              float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
              float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
              transform.Rotate(Vector3.up, -rotX);
              transform.Rotate(Vector3.right, rotX);

          }*/
    private Vector3 mOffset;
    private float mZCoord;
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;


        mousePoint.z = mZCoord;


        return Camera.main.ScreenToWorldPoint(mousePoint);

    }
    private void OnMouseDrag()
    {

        transform.position = GetMouseWorldPos() + mOffset;
        // Debug.Log("A");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (true)
        {

        }
    }
}
