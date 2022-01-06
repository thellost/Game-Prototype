using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class FollowBeizer : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    [SerializeField] bool disableOnFinish = false;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    public float speedModifier = 0.5f;

    public bool coroutineAllowed;

    private Coroutine function;

    private Timeline time;

    // Start is called before the first frame update
    void Awake()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
        time = GetComponent<Timeline>();
    }
    public void bossJump(int route)
    {
        StartCoroutine(GoByTheRoute(route, disableOnFinish));
    }
    private IEnumerator GoByTheRoute(int routeNum, bool disableOnFinish = false)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
        if (disableOnFinish)
        {
            disableGameObject();
        }

    }
    private void disableGameObject()
    {
        gameObject.SetActive(false);
    }
}
