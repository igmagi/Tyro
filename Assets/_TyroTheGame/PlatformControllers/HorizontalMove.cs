using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();

    public float speed = 10f;
    public int index = 0;

    public Vector3 newPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        newPosition = positions[index];
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, newPosition, speed * TimeManager.instance.globalSpeed * Time.deltaTime);
        this.transform.position = Vector3.Lerp(transform.position, newPosition, 0.2f * speed * Time.deltaTime);
        //Debug.Log(Vector3.Distance(transform.position, newPosition));
        if (Vector3.Distance(transform.position, newPosition) < 1f)
        {
            //Debug.Log("AAA");
            index++;
            if (index >= positions.Count) index = 0;
            newPosition = positions[index];
        }
    }
}
