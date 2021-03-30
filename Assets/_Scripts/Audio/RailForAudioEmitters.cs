using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailForAudioEmitters : MonoBehaviour
{
    public bool debug = false;
    public Color debugColor = Color.green;

    // stores the waypoints of the rails
    private Vector3[] nodes;
    private int nodeCount;

    private void Start()
    {
        //counts how many child objects this object has
        nodeCount = transform.childCount;
        //Makes vector 3 equal to the amount of child objects
        nodes = new Vector3[nodeCount];

        // runs as long as i is smaller than nodeCount
        for (int i = 0; i < nodeCount; i++)
        {
            //gets the transform position for the child with the index i
            nodes[i] = transform.GetChild(i).position;
        }
    }

    //Update is only used for visualising the rail system
    private void Update()
    {
        if (debug)
        {
            //because we only have a rail if we have more than 1 node
            if (nodeCount > 1)
            {
                // runs as long as i is smalle than nodecount - 1 (because we don't need it to run for the last node)
                for (int i = 0; i < nodeCount - 1; i++)
                {
                    //draws a line between node i and he next node (i + 1)
                    Debug.DrawLine(nodes[i], nodes[i + 1], debugColor);
                }
            }
        }
    }

    // This method will place the emitter on the rail posed on listener position
    public Vector3 ProjectPositionOnRail(Vector3 pos)
    {
        int closestNodeIndex = GetClosestNode(pos);

        if (closestNodeIndex == 0)
        {
            //Project on the first segment
            return ProjectOnSegment(nodes[0], nodes[1], pos);
        }
        else if (closestNodeIndex == nodeCount - 1)
        {
            //Project on the last segment
            return ProjectOnSegment(nodes[nodeCount -1], nodes[nodeCount -2], pos);
        }
        else
        {
            //Project on the 2 connected segments
            //Return the shortest vector

            Vector3 leftSeg = ProjectOnSegment(nodes[closestNodeIndex - 1], nodes[closestNodeIndex], pos);
            Vector3 rightSeg = ProjectOnSegment(nodes[closestNodeIndex + 1], nodes[closestNodeIndex], pos);

            if (debug)
            {
                Debug.DrawLine(pos, leftSeg, Color.red);
                Debug.DrawLine(pos, rightSeg, Color.blue);
            }

            if ((pos - leftSeg).sqrMagnitude <= (pos -rightSeg).sqrMagnitude)
            {
                return leftSeg; 
            }
            else
            {
                return rightSeg;
            }

        }
    }

    private int GetClosestNode (Vector3 pos)
    {
        int closestetNodeIndex = -1;
        float shortestDistance = 0.0f;

        for (int i = 0; i < nodeCount; i++)
        {
            //I have no idea what this black magic math is
            float squareDistance = (nodes[i] - pos).sqrMagnitude;
            if (shortestDistance == 0 || squareDistance < shortestDistance)
            {
                shortestDistance = squareDistance;
                closestetNodeIndex = i;
            }

        }
        return closestetNodeIndex;
    }

    private Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(segDirection, v1ToPos);

        if (distanceFromV1 < 0.0f)
        {
            return v1;
        }
        else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }
        else
        {
            Vector3 fromV1 = segDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }
}

