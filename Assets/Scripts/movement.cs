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
    public bool jump;
    public int jumpForce = 20;
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
        // Movimento
        // Adiciona valores em X e Z baseado na velocidade de movimento e no joystick.
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        // Rotação
        moveDirection = new Vector3(joystick.Horizontal * moveSpeed, 0, joystick.Vertical * moveSpeed);
        if(moveDirection != Vector3.zero)
        {
            qTo = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp (transform.rotation, qTo, Time.deltaTime * rotationSpeed);
        }


        // Pulo
        // No primeiro if, limitamos a quantidade de pulo que o personagem pode chegar. Caso o "y" seja inferior a 40f, o personagem consegue
        // pular, adicionando uma força ao Vector3 para cima e multiplicando pelo jumpForce (força de pulo).
        if(!jump && joyButton.Pressed)
        {
            if(transform.position.y <= 40f)
            {
                jump = true;
                rb.AddForce(Vector3.up * jumpForce);
                
            }

            // O segundo if serve para o personagem não continuar pulando infinitamente. Caso o botão já esteja pressionado, ele seta jump como 
            // "false".
            if(jump && joyButton.Pressed)
            {
                jump = false;
            }
        }

        
    }
}
