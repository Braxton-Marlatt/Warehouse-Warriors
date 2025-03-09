using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 3f;
    public int damageAmount = 1;
    public StateMachine currentState = StateMachine.Evade;
    public Node currentNode;
    public int enemyType = 0; // 0 = melee, 1 = ranged, 2 = turret
    public int disengageDistance = 3; // for ranged
    public List<Node> path = new List<Node>();
    public bool isSpawned = false;

    public enum StateMachine{
        Engage, //Chase down
        Evade, //Move Randomly
        Flee, //Run away
        Freeze //Do nothing
    }


    protected virtual void Update(){
        if(!isSpawned) return;
        switch (currentState){
            case StateMachine.Engage:
                Engage();
                break;
            case StateMachine.Flee:
                Flee();
                break;
            case StateMachine.Evade:
                Evade();
                break;
            case StateMachine.Freeze: // Do nothing
                break;
        }
        if(enemyType == 0 && currentState != StateMachine.Engage){
            currentState = StateMachine.Engage;
            path.Clear();
        }else if(enemyType == 1 && currentState != StateMachine.Flee && Vector2.Distance(transform.position, player.position) <= disengageDistance){
            currentState = StateMachine.Flee;
            path.Clear();
        }else if(enemyType == 1 && currentState != StateMachine.Evade && Vector2.Distance(transform.position, player.position) > disengageDistance){
            currentState = StateMachine.Evade;
            path.Clear();
        }else if (enemyType == 2 && currentState != StateMachine.Freeze){
            currentState = StateMachine.Freeze;
            path.Clear();
            return;
        }
        CreatePath();
    }

    void Flee(){
        if (path.Count == 0){
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindFurthestNode(currentNode,player.position));
        }
    }
    void Engage(){
        if (path.Count == 0){
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(currentNode,player.position));
        }
    }
    
    
    void Evade(){ 
        if(path.Count == 0){
            System.Random rand = new System.Random(); // Use fully qualified name
            int randomNumber = rand.Next(0, 2); // Generates either 0 or 1
            if(randomNumber == 0) Flee();
            else Engage();
        }
        //path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.AllNodes()[Random.Range(0, AStarManager.instance.AllNodes().Length)]);
    }
    
    public void CreatePath(){
        if (path.Count > 0){
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f){
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }
    public void Spawn(){
        isSpawned = true;
    }
}

