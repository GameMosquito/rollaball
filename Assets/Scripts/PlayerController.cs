using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private Text countText = null;
    [SerializeField] private Text winText = null;
    [SerializeField] private float speed = 0f;
 
    private Rigidbody rb;
    private int count;
    private int numPrefabs;
    private InstantiatePickups script;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = string.Empty;
        script = Object.FindObjectOfType<InstantiatePickups>();
        numPrefabs = script.GetNumPickups();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Pickup>())
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = $"Count: {count}";
        if (count >= numPrefabs)
        {
            winText.text = "You win!";
        }
    }
}
