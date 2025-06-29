using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expansion;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    MovePlayer movePlayer;
    [SerializeField]
    float closeness;
    static readonly Vector2 CENTER_POSITION_OF_MAP = new Vector2(0, 0);
    [SerializeField]
    float offsetPositionX;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        SetToSynchronizedRelativePosition();
    }



    void SetToSynchronizedRelativePosition()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 differentToInitPos = CENTER_POSITION_OF_MAP - playerPos;
        Vector2 synchronizedRelativePosition = differentToInitPos * closeness;
        synchronizedRelativePosition.y = 0;
        synchronizedRelativePosition += new Vector2(offsetPositionX, 0);

        this.transform.position = synchronizedRelativePosition;
    }
}
