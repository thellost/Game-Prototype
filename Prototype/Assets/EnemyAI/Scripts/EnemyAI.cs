
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] float attackCooldown;
    [SerializeField] Collider2D hitBox;
    [SerializeField] Collider2D detectionBox;
    public enum State {
        Patroling,
        ChaseTarget,
        AttackTarget,
        GoingBackToStart,
    }

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float nextShootTime;
    private State state;


    private void Awake() {
        state = State.Patroling;
    }

    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        switch (state) {
        default:
        case State.Patroling:

            float reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                // Reached Roam Position
                roamPosition = GetRoamingPosition();
            }

            FindTarget();
            break;
        case State.ChaseTarget:
            break;
        case State.AttackTarget:
            break;
        case State.GoingBackToStart:
           
            break;
        }
    }

    private Vector3 GetRoamingPosition() {
        return Vector3.left;
    }

    private void FindTarget() {
        
    }

    public void setState(State stateParameter)
    {
        state = stateParameter;
    }

}
