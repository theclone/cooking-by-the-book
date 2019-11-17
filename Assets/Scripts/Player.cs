using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Input")]
    public Vector3 inputVelocity;
    public float yaw;
    public float pitch;
    [Header("Attributes")]
    public float speed;
    public float sensitivity;
    public float grabRange;
    public float grabRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputVelocity = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        inputVelocity *= speed;
        this.transform.Translate(inputVelocity);
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        if (Input.GetButton("Fire1"))
        {
            Grab();
        }
    }

    void Grab()
    {
        Collider[] grabObjects = new Collider[10];
        Physics.OverlapCapsuleNonAlloc(transform.position,transform.position + grabRange * transform.forward, grabRadius, grabObjects, 1 << LayerMask.NameToLayer("Grabbable"));
        if (grabObjects != null)
        {
            float nearestDistance = float.MaxValue;
            float distance;
            GameObject grabbed;
            foreach(Collider grabObject in grabObjects)
            {
                if (grabObject != null)
                {
                    distance = (grabObject.transform.position - this.transform.position).sqrMagnitude;
                    if (distance < nearestDistance)
                    {
                        grabbed = grabObject.gameObject;
                        nearestDistance = distance;
                    }
                }
            }
        }
    }
}
