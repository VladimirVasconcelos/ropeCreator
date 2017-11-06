using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinRope : MonoBehaviour {

    public float distanceFromChainEnd = 0;

    public void ConnectRopeEnd(Rigidbody2D endRB) {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
    }
}