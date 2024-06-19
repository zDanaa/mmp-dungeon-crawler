using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody2D playerBody;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerBody.velocity = new Vector3(horizontal * playerSpeed, vertical * playerSpeed, 0 );
    }
}
