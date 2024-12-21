using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject playzone;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpForwardForce = 5f;
    private Rigidbody2D playerRB;
    private bool jumping;

    void Start(){
        playerRB = GetComponent<Rigidbody2D>();
        jumpForce *= playerRB.gravityScale;
        Jump();
    }

    void Update(){
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameUI.isGameOver){
            Vector2 touchPosition = Input.GetTouch(0).position;

            // get the screen postion.
            Vector2 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            // check the direction of touch.
            if (worldTouchPosition.x < 0)
            {
                Jump(-jumpForwardForce);
            }
            else
            {
                Jump(jumpForwardForce);
            }
            jumping = true;
        }

        // fall the level on every jump.
        if(jumping){
            MoveLevel();
            Invoke("DisableJump", 0.5f);
        }
    }

    private void MoveLevel(){
        playzone.transform.position = Vector2.Lerp(playzone.transform.position, new Vector2(playzone.transform.position.x, playzone.transform.position.y - 100), 5 * Time.deltaTime);
    }

    private void DisableJump(){
        jumping = false;
    }

    private void Jump(){
        // jump forward
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
    }

    private void Jump(float jumpForwardForce){
        // jump on the touched direction
        playerRB.velocity = new Vector2(jumpForwardForce * 10, jumpForce);
    }

    // Detect collision with the level blocks.
    private void OnCollisionEnter2D(Collision2D collision){
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GameUI.isGameOver = true;
        Debug.LogWarning("GAME OVER");
        Invoke("LoadMenuScene", 3f);
    }

    private void LoadMenuScene(){
        sceneLoader.LaodMenuScene();
    }
}

