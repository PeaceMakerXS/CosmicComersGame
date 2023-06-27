using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suricsan : MonoBehaviour
{
    public float speed;
    public int damage = 1;
    private void Update()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + transform.right * 3;

        float distanceCovered = 0f;

        while (distanceCovered < Vector3.Distance(startPos, endPos))
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            distanceCovered = Vector3.Distance(startPos, transform.position);

            yield return null;
        }
    }
}
