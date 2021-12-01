using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class SlashEffect : MonoBehaviour

{
    [SerializeField] float attackRangeCircle;

    [SerializeField] float slashAnimationSpeed;

    private Vector3 direction;
    private Timeline time;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Timeline>();

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.localScale = transform.localScale;
        //gameobj.transform.parent = gameObject.transform;
        transform.rotation = transform.rotation * rotation; //magic number -68.43f karena rotasi dari animasinya ga lurus

        transform.localScale *= 1.5f;
        Vector2 temp2 = direction;
        setTraditionalNormalize(ref temp2);
        Vector3 temp3 = temp2;
        transform.position = transform.position - (0.5f * temp3);
    }

    // Update is called once per frame
    void Update()
    {
         transform.position = Vector2.MoveTowards(transform.position, (direction + transform.position) * attackRangeCircle, slashAnimationSpeed * time.deltaTime); //10f magic number
    }

    private void setTraditionalNormalize(ref Vector2 vector2)
    {
        float temp = Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y);
        vector2.x = vector2.x / temp;
        vector2.y = vector2.y / temp;

    }
}
