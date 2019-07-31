using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    private Transform body;
    private Transform innerLeg, outerLeg;
    private ConfigurableJoint innerJoint, outerJoint;

    [SerializeField]
    public float speed = 200;

    // Input keys for moving the leg parts
    // These are initialized in the editor
    public KeyCode KEY_IN_UP;
    public KeyCode KEY_IN_DOWN;
    public KeyCode KEY_IN_LEFT;
    public KeyCode KEY_IN_RIGHT;

    public KeyCode KEY_OUT_UP;
    public KeyCode KEY_OUT_DOWN;

    // Start is called before the first frame update
    void Start()
    {
        body = transform;
        innerLeg = transform.Find("X");
        outerLeg = transform.Find("Y");

        innerJoint = innerLeg.GetComponent<ConfigurableJoint>();
        outerJoint = outerLeg.GetComponent<ConfigurableJoint>();

        body = innerJoint.connectedBody.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Up and down (Inner part)
        if (Input.GetKey(KEY_IN_UP)) {
            innerJoint.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KEY_IN_DOWN)) {
            innerJoint.transform.Rotate(Vector3.back * speed * Time.deltaTime);
        }

        // Left and right (Inner part)
        if (Input.GetKey(KEY_IN_LEFT)) {
            innerJoint.transform.Rotate(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KEY_IN_RIGHT)) {
            innerJoint.transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }

        // Up and down (Outer part)
        if (Input.GetKey(KEY_OUT_UP)) {
            outerJoint.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KEY_OUT_DOWN)) {
            outerJoint.transform.Rotate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
