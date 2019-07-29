using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    private Transform body;
    private Transform innerLeg, outerLeg;
    private ConfigurableJoint innerJoint, outerJoint;

    [SerializeField]
    public float rotationSpeedX = 20F;
    public float rotationSpeedY = 10;
    public float rotationSpeedZ = 20F;


    public bool oUp, oDown,
                iUp, iDown,
                iLeft, iRight;


    // Start is called before the first frame update
    void Start()
    {
        body = transform;
        innerLeg = transform.Find("A");
        outerLeg = transform.Find("C");

        innerJoint = innerLeg.GetComponent<ConfigurableJoint>();
        outerJoint = outerLeg.GetComponent<ConfigurableJoint>();

        body = innerJoint.connectedBody.transform;
    }

    #region Movement Button Toggles
    public void InnerUp()
    {
        iDown = false;
        iUp = true;
    }

    public void InnerDown()
    {
        iDown = true;
        iUp = false;
    }

    public void InnerLeft()
    {
        iRight = false;
        iLeft = true;
    }

    public void InnerRight()
    {
        iRight = true;
        iLeft = false;
    }

    public void OuterUp()
    {
        oDown = false;
        oUp = true;
    }

    public void OuterDown()
    {
        oDown = true;
        oUp = false;
    }

    public void InnerStop()
    {
        iDown = iUp = iLeft = iRight = false;
    }

    public void OuterStop()
    {
        oDown = oUp = false;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        oUp = Input.GetKey(KeyCode.UpArrow);
        oDown = Input.GetKey(KeyCode.DownArrow);

        iLeft = Input.GetKey(KeyCode.LeftArrow);
        iRight = Input.GetKey(KeyCode.RightArrow);


        if (oUp)
        {
            outerJoint.transform.Rotate(Vector3.forward * rotationSpeedY);
        }
        else if (oDown)
        {
            outerJoint.transform.Rotate(Vector3.back * rotationSpeedY);
        }

        if (iUp)
        {
            innerJoint.transform.Rotate(Vector3.forward * rotationSpeedX);
        }
        else if (iDown)
        {
            innerJoint.transform.Rotate(Vector3.back * rotationSpeedX);
        }

        if (iLeft)
            innerJoint.transform.Rotate(Vector3.left * rotationSpeedZ);
        else if (iRight)
            innerJoint.transform.Rotate(Vector3.right * rotationSpeedZ);

    }
}
