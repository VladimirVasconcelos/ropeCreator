using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHook : MonoBehaviour {
    public int nodesNumber;

    public GameObject linkPrefab;

    private Rigidbody2D hook;
    private JointMotor2D motor;

    [SerializeField]
    private PinRope pinRope;

    [SerializeField]
    private Vector2 distanceAnchor;
    [SerializeField]
    private Vector2 connectedAnchor;

    private bool isClimbing;
    public RopeZone ropeZone;

    private void Start() {

        this.hook = GetComponent<Rigidbody2D>();

        GenRope();
    }

    private void GenRope() {

        Rigidbody2D lastChild = hook;
        for (int i = 0; i < nodesNumber; i++) {

            GameObject link = Instantiate(linkPrefab, this.transform);
            link.AddComponent<HingeJoint2D>();
            link.AddComponent<BoxCollider2D>();
            this.ropeZone = link.GetComponent<RopeZone>();

            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();

            this.ropeZone.isGrabable = false;

            //joint.enableCollision = true;
            joint.connectedBody = lastChild;
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = distanceAnchor;
            joint.connectedAnchor = connectedAnchor;
            link.GetComponent<Rigidbody2D>().mass = 1;

            Rigidbody2D linkRb = link.GetComponent<Rigidbody2D>() as Rigidbody2D;
            linkRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            if (i < nodesNumber - 1) {

                lastChild = link.GetComponent<Rigidbody2D>();
            } else {
                this.ropeZone.isGrabable = true;
                BoxCollider2D col = link.GetComponent<BoxCollider2D>();
                col.isTrigger = true;
                link.GetComponent<Rigidbody2D>().mass = 1;
                link.transform.rotation = Quaternion.Euler(0, 0, 90);

                /// Pins the end of the rope into another object.
                //pinRope.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
            }

            lastChild = link.GetComponent<Rigidbody2D>();
        }
    }
}