using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameManager gm;
    public int finishedPlayers;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("TurtlePlayer") || other.gameObject.CompareTag("LionPlayer"))
        {
            finishedPlayers += 1;

            if(finishedPlayers == 2)
            {
                gm.bothFinished = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("TurtlePlayer") || other.gameObject.CompareTag("LionPlayer"))
        {
            finishedPlayers -= 1;
        }
    }
}
