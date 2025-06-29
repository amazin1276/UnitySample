using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{


    public GameObject target;

    private Vector3 targetPosition
    {
        get
        {
            return target.transform.position;
        }
    }

    void Update()
    {
        FollowTarget();
    }


    private void FollowTarget()
    {
        this.transform.position = targetPosition;
    }
}
