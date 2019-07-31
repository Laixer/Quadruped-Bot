using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bot : MonoBehaviour
    {
        private bool walk_right = false;
        public bool walking = false, invert = false;
        [SerializeField]
        public int WT_MAX = 50;
        private int walk_timer;
        private int walk_elaps = 0;

        public float walk_speed = 100, rotate_speed = 50;

        public ConfigurableJoint outerJointA, outerJointB, outerJointC, outerJointD;
        private ConfigurableJoint innerJointA, innerJointB, innerJointC, innerJointD;

        private void Start()
        {
            walk_timer = WT_MAX;
            innerJointA = outerJointA.connectedBody.transform.GetComponent<ConfigurableJoint>();
            innerJointB = outerJointB.connectedBody.transform.GetComponent<ConfigurableJoint>();
            innerJointC = outerJointC.connectedBody.transform.GetComponent<ConfigurableJoint>();
            innerJointD = outerJointD.connectedBody.transform.GetComponent<ConfigurableJoint>();
        }

        private void Update()
        {
            walking = Input.GetKey(KeyCode.UpArrow);

            if (walking) {
                walk_timer -= 1;
                walk_elaps += 1;

                if (walk_timer <= 0) {
                    walk_right = !walk_right;
                    walk_elaps = 0;
                    walk_timer = WT_MAX;
                    invert = !invert;
                }

                invert = walk_elaps >= walk_timer - (walk_timer/5);

                if (walk_right) {
                    outerJointA.transform.Rotate(Vector3.forward * (invert ? -1 : 1) * walk_speed * Time.deltaTime);
                    outerJointC.transform.Rotate(Vector3.forward * (invert ? -1 : 1) * walk_speed * Time.deltaTime);
                }
                else {
                    outerJointB.transform.Rotate(Vector3.forward * (invert ? -1 : 1) * walk_speed * Time.deltaTime);
                    outerJointD.transform.Rotate(Vector3.forward * (invert ? -1 : 1) * walk_speed * Time.deltaTime);
                }



            }

            if (Input.GetKey(KeyCode.LeftArrow)) {
                innerJointA.transform.Rotate(Vector3.left * rotate_speed * Time.deltaTime);
                innerJointB.transform.Rotate(Vector3.left * rotate_speed * Time.deltaTime);
                innerJointC.transform.Rotate(Vector3.left * rotate_speed * Time.deltaTime);
                innerJointD.transform.Rotate(Vector3.left * rotate_speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
                innerJointA.transform.Rotate(Vector3.right * rotate_speed * Time.deltaTime);
                innerJointB.transform.Rotate(Vector3.right * rotate_speed * Time.deltaTime);
                innerJointC.transform.Rotate(Vector3.right * rotate_speed * Time.deltaTime);
                innerJointD.transform.Rotate(Vector3.right * rotate_speed * Time.deltaTime);
            }

        }
    }
}
