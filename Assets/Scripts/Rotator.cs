using UnityEngine;

public sealed class Rotator : MonoBehaviour
{
    private Vector3 rotVector;


    private void Start()
    {
        rotVector = new Vector3(Random.Range(5f, 15f), Random.Range(25f, 35f), Random.Range(40f, 50f));
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(rotVector * Time.deltaTime);
    }
}
