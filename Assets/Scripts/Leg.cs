using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    private Transform body;
    private Transform innerLeg, outerLeg;
    private ConfigurableJoint innerJoint, outerJoint;


    // Start is called before the first frame update
    void Start()
    {
        innerLeg = transform.Find("LegA");
        innerLeg = transform.Find("LegB");

        innerJoint = innerLeg.GetComponent<ConfigurableJoint>();
        outerJoint = outerLeg.GetComponent<ConfigurableJoint>();

        body = innerJoint.connectedBody.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
