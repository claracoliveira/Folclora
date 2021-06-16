using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //Joystick
    protected Joystick joystick;

    //Movimento e rotação
    public float moveSpeed = 0;
    public float xAngle, yAngle, zAngle;
    Vector3 moveDirection;
    private Quaternion qTo;
    
    // Pulo
    JoyButton joyButton;
    Rigidbody rb;
    // Checa se ele está pulando
    private bool isJumping;
    // Checa se o botão de pulo está pressionado
    private bool isPressed;
    // Checa se o personagem está no chão
    private bool isGrounded;
    public float jumpForce = 20f;
    public float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();

        // Instancia o botão de pulo
        joyButton = FindObjectOfType<JoyButton>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotação
        moveDirection = new Vector3(joystick.Horizontal * moveSpeed, 0, joystick.Vertical * moveSpeed);
        if(moveDirection != Vector3.zero)
        {
            qTo = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp (transform.rotation, qTo, Time.deltaTime * rotationSpeed);
        }

        // Pulo
        // Checa e troca o valor de "isPressed" para true ao apertar o botão de pulo
        isPressed = joyButton.Pressed;
    }

    void FixedUpdate()
    {
        // Movimento
        // Adiciona valores em X e Z baseado na velocidade de movimento e no joystick.
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        // Pulo
        // Se isPressed e isGrounded estiverem como "true", adiciona uma força em y
        if(isPressed && isGrounded)
        {  
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            // Seta isGrounded como false p/ que ele não pule mais
            isGrounded = false;
        }
    }

    // Função chamada toda vez que se "entra" em colisão
    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Ground")) 
        {
            // Colidiu com o chão
            isGrounded = true;
        }
    }
}
