using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour{
    public static AStarManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Node> GeneratePath(Node start, Node end)
    {
        List<Node> openSet = new List<Node>();

    foreach (Node n in FindObjectsByType<Node>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
    {
        n.gScore = float.MaxValue;
    }

        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);

        while(openSet.Count > 0)
        {
            int lowestF = default;

            for(int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore())
                {
                    lowestF = i;
                }
            }

            Node currentNode = openSet[lowestF];
            openSet.Remove(currentNode);

            if(currentNode == end)
            {
                List<Node> path = new List<Node>();

                path.Insert(0, end);

                while(currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }

                path.Reverse();
                return path;
            }

            foreach (Node connectedNode in FindObjectsByType<Node>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                float heldGScore = currentNode.gScore + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if(heldGScore < connectedNode.gScore)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = heldGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform.position, end.transform.position);

                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                }
            }
        }

        return null;
    }
    public Node InitializeClosestNode(Vector2 pos)
    {
        Node closestNode = null;
        float minDistance = float.MaxValue;

        foreach (Node node in AllNodes()){
            float currentDistance = Vector2.Distance(pos, node.transform.position);
            if (currentDistance < minDistance){
                minDistance = currentDistance;
                closestNode = node;
            }
        }

        return closestNode;
    }
    //Finds the closest connected node to a point. This instructions is repeated for adaptability
    public Node FindNearestNode(Node currentNode, Vector2 pos){
        Node foundNode = null;
        float minDistance = float.MaxValue;
        foreach (Node node in currentNode.connections){
            float currentDistance = Vector2.Distance(pos, node.transform.position);
            if(currentDistance < minDistance){
                minDistance = currentDistance;
                foundNode = node;
            }
        }
        return foundNode;
    }

    //Finds the furthest connected node to a point. This instructions is repeated for adaptability
    public Node FindFurthestNode(Node currentNode,Vector2 pos){
        Node foundNode = null;
        float maxDistance = default;
        foreach (Node node in currentNode.connections){
            float currentDistance = Vector2.Distance(pos, node.transform.position);
            if(currentDistance > maxDistance){
                maxDistance = currentDistance;
                foundNode = node;
            }
        }
        return foundNode;
    }


    public Node[] AllNodes()
    {
        return FindObjectsByType<Node>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }
}
