using UnityEngine;

public sealed class Rotator : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0f, 360f)]
    private Vector2 xVector = Vector2.zero;
    [SerializeField, MinMaxSlider(0f, 360f)]
    private Vector2 yVector = Vector2.zero;
    [SerializeField, MinMaxSlider(0f, 360f)]
    private Vector2 zVector = Vector2.zero;

    private Vector3 rotVector;

    private void Start()
    {
        rotVector = new Vector3(xVector.RandomInRange(), yVector.RandomInRange(), zVector.RandomInRange());
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(rotVector * Time.deltaTime);
    }
}
