using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private Text countText = null;
    [SerializeField] private Text winText = null;
    [SerializeField] private Text restartText = null;
    [SerializeField] private float speed = 0f;
    [SerializeField] private AudioSource stageTheme = null;
    [SerializeField] private AudioSource failureTheme = null;
    [SerializeField] private AudioSource victoryTheme = null;
    [SerializeField] private AudioSource pickupNoise = null;

    private Rigidbody rb;
    private int count;
    private int numPrefabs = -1;
    private InstantiatePickups script;

    enum State
    {
        STATE_PLAYING,
        STATE_VICTORY,
        STATE_FAILURE
    };

    State state_;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = $"Count: {count}";
        winText.text = string.Empty;
        restartText.text = string.Empty;
        script = Object.FindObjectOfType<InstantiatePickups>();
        
        stageTheme.Play();
        numPrefabs = script.GetNumPickups();
        state_ = State.STATE_PLAYING;
    }

    private void Update()
    {
        switch (state_)
        {
            case State.STATE_PLAYING:
                
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                var movement = new Vector3(moveHorizontal, 0, moveVertical);
                rb.AddForce(movement * speed);
                break;

            

            case State.STATE_FAILURE:
                winText.text = "You lose!";
                restartText.text = "Press Enter to Play Again!";
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                    failureTheme.Stop();
                    RestartPlay();
                }
                break;

            case State.STATE_VICTORY:
                winText.text = "You win!";
                restartText.text = "Press Enter to Play Again!";
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    victoryTheme.Stop();
                    RestartPlay();
                }
                break;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Pickup>())
        {
            other.gameObject.SetActive(false);
            count++;
            pickupNoise.Play();
            SetCountText();
        }else if(other.gameObject.GetComponent<DeathPlane>())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.gameObject.GetComponent<MeshRenderer>().enabled = false;
            rb.useGravity = false;
            countText.text = "Count: :(";
            stageTheme.Stop();
            victoryTheme.Stop();
            failureTheme.Play();
            state_ = State.STATE_FAILURE;
        }
    }

    private void SetCountText()
    {
        countText.text = $"Count: {count}";
        if (count >= numPrefabs)
        {
            stageTheme.Stop();
            victoryTheme.Play();
            state_ = State.STATE_VICTORY;
        }
    }

    private void RestartPlay()
    {
        count = 0;
        countText.text = $"Count: {count}";
        winText.text = string.Empty;
        restartText.text = string.Empty;
        script.Clear();
        
        script.Initialize();

        rb.gameObject.GetComponent<MeshRenderer>().enabled = true;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.gameObject.transform.position = new Vector3(0f, .5f, 0f);
        stageTheme.Play();
        state_ = State.STATE_PLAYING;
    }
}
