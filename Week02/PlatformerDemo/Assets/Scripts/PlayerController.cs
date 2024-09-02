using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D selfRd;
    private Animator selfAnim;
    
    public CapsuleCollider2D selfColl;
    public LayerMask ground;
    
    public float speed;
    public float jumpSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        selfRd = GetComponent<Rigidbody2D>();
        selfAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        DropHandler();
    }

    void Movement()
    {
        float vectorBuffer = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        float jumpTrigger = Input.GetAxisRaw("Jump");
        
        if (vectorBuffer != 0.0f)
            selfRd.velocity = new Vector2(vectorBuffer * speed * Time.deltaTime, selfRd.velocity.y);

        if (faceDirection != 0.0f)
            transform.localScale = new Vector3(faceDirection, 1.0f, 1.0f);

        if ((jumpTrigger != 0.0f) && (!selfAnim.GetBool("jumpFlag")) && !selfAnim.GetBool("dropFlag"))
        {
            selfRd.velocity = new Vector2(selfRd.velocity.x, jumpSpeed * Time.deltaTime);
            selfAnim.SetBool("jumpFlag", true);
        }
        
        selfAnim.SetFloat("runFlag", Mathf.Abs(selfRd.velocity.x));
        
    }

    void DropHandler()
    {
        selfAnim.SetBool("idleFlag", false);
        if (selfAnim.GetBool("jumpFlag") && (selfRd.velocity.y < 0.0f))
        {
            selfAnim.SetBool("jumpFlag", false);
            selfAnim.SetBool("dropFlag", true);
        }
        else if (selfAnim.GetBool("dropFlag") && (selfColl.IsTouchingLayers(ground)))
        {
            selfAnim.SetBool("dropFlag", false);
            selfAnim.SetBool("idleFlag", true);
        }
    }
}
