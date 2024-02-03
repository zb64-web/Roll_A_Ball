using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject Door;
    public float jumpForce = 7;
    public SphereCollider col;
    public Material[] material = new Material[3];
    public LayerMask groundLayers;
    public float originalSpeed = 5.0f;
    public AudioSource audioPlayer;

    private Rigidbody rb;
    private int count;
    private Renderer rend;
    private float boostTimer;
    private bool boosting;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        currentSpeed = originalSpeed;
        boostTimer = 0;
        boosting = false;

        count = 0;
        SetCountText();
        winText.text = "";
        Door.SetActive(true);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Pick Up"))
        {
            otherCollider.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (otherCollider.gameObject.CompareTag("SpeedBoost"))
        {
            Boost(2.0f); 
            otherCollider.gameObject.SetActive(false);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 13)
        {
            winText.text = "You Win!";
        }

        if (count >= 4)
        {
            Door.SetActive(false);
        }

        if (rend != null && count > 0 && count < material.Length)
        {
            rend.sharedMaterial = material[count - 1];
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Pick Up")
    {
        rend.sharedMaterial = material.Length > 1 ? material[1] : rend.sharedMaterial;
        audioPlayer.Play();
    }
    else
    {
        rend.sharedMaterial = material.Length > 2 ? material[2] : rend.sharedMaterial;
    }

}

    }

    void Update()
    {
        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)
            {
                ResetBoost();
            }
        }
    }

    public void Boost(float boostValue)
    {
        currentSpeed = originalSpeed * boostValue;
        boosting = true;
    }

    public void ResetBoost()
    {
        currentSpeed = originalSpeed;
        boosting = false;
        boostTimer = 0;
    }

}





