using Chronos;
using UnityEngine;
public class DashBoss : StateMachineBehaviour
{
    [SerializeField] float dashSpeed =10;
    private Vector2 target;
    private Transform transform;
    private Timeline time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Waypoint")[0].transform.position;

        transform = animator.GetComponent<Transform>();
        time = animator.GetComponent<Timeline>();
        Debug.Log(Vector3.Distance(target, transform.position));
        if(Vector3.Distance(target, transform.position) < 0.1f)
        {
            target = GameObject.FindGameObjectsWithTag("Waypoint")[1].transform.position;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * time.deltaTime);
        if (Mathf.Abs(Vector3.Distance(target, transform.position)) < 0.001f)
        {
            animator.SetTrigger("dashAttack");
            Debug.Log("TRUE");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
