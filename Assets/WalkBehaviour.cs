using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBehaviour : StateMachineBehaviour
{
    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;  
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach(Transform t in pointsObject){
            points.Add(t);
        } 
        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(points[0].position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if(agent.remainingDistance <= agent.)
        //    agent.SetDestination(points[Random.Range(0, points.Count)].position);
        
        if(timer > 10){
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
    }

}
