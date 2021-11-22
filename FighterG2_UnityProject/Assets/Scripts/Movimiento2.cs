using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento2 : MonoBehaviour
{
    [SerializeField]

    [Header("Salto")]
    public float fuerzasalto;
    public int nsaltos = 0;
    public int saltostotales = 2;
    private bool canJump = true;
    public float stiempo = 0.3f;

    [Header("Movimiento")]
    public float velocidad;
    public bool pared;
    public float gpared;
    public bool mirandoderecha = false;
    public float gravedad;
    public float diagonal = 1f;

    [Header("Varios")]
    private Controls1 Input1;
    private Rigidbody2D rb;

    [Header("SFX")]
    public SFXScript sfx;

    void Start()///////////////////////////////////////COSAS QUE SE EJECUTAN AL EMPEZAR//////////////////////////////////////////////
    {
        rb = GetComponent<Rigidbody2D>();
        gravedad = rb.gravityScale;
    }

    void FixedUpdate() ///////////////////////////////////////COSAS QUE SE EJECUTAN EN CADA FRAME///////////////////////////////////////////////
    {

        Move();
    }

    ////////////////////////////                           A PARTIR DE AQUI VAN LAS ACCIONES AAAAAAAAAAAAAA                ////////////////////////

    private void Move()         ///////////MOVERSE
    {
        float mve = Input1.Player2.Move.ReadValue<float>();
        if (mirandoderecha == true && mve < 0)
        {
            transform.Rotate(0f, 180f, 0f, Space.World);
            mirandoderecha = false;
        }
        else if (mirandoderecha == false && mve > 0)
        {
            transform.Rotate(0f, -180f, 0f, Space.World);
            mirandoderecha = true;
        }
        Vector2 x = new Vector2(mve, 0f);
        rb.AddForce(x * velocidad, ForceMode2D.Force);

    }
    private void Jump(InputAction.CallbackContext c)///////////////////////////SALTAR//////////////////////////////////////////////////////
    {
        float salto = Input1.Player2.Jump.ReadValue<float>();
        Vector2 saltito;
        if (nsaltos < saltostotales && canJump == true)
        {
            if (salto > 0.5f)
            {
                saltito = new Vector2(0f, 1f);
                if (pared)
                {
                    if (mirandoderecha)
                    {
                        saltito = new Vector2(-diagonal, 1f);
                    }
                    else
                    {
                        saltito = new Vector2(diagonal, 1f);
                    }

                }
                //Aqu� el sonido
                if (sfx == null) sfx = GameObject.Find("SFXManager").GetComponent<SFXScript>();
                sfx.PlaySound("Jump1");
                //Aqu� la fuerza
                rb.AddForce(saltito * fuerzasalto, ForceMode2D.Force);
                nsaltos = nsaltos + 1;
                DisableS(stiempo);
            }
        }


    }
    private void Crouch(InputAction.CallbackContext c)///////////////AGACHARSE  ??? ///////////////////////////////////////////////////////////
    {

    }
    private void Punch(InputAction.CallbackContext c)////////////////GOLPE NORMAL/////////////////////////////////////////////////////////////
    {

    }
    private void PunchF(InputAction.CallbackContext c)///////////////GOLPE FUERTE/////////////////////////////////////////////////////
    {

    }
    private void PunchE(InputAction.CallbackContext c)////////////////////ESPECIAL//////////////////////////////////////////////////////
    {

    }
    ///////////////////////////                          HASTA AQUI VAN LAS ACCIONES AAAAAAAAAAAAAA                     //////////////////////////

    private void OnCollisionEnter2D(Collision2D collision) ///////////DETECTA COLISIONES/////////////////////////////////////////////////
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Pared")
        {
            nsaltos = 0;
            if (collision.collider.tag == "Pared")
            {
                rb.gravityScale = gpared;
                pared = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Pared")
        {
            rb.gravityScale = gravedad;
            pared = false;
        }

    }


    void DisableS(float t)//////////////////ACTIVAR Y DESACTIVAR TIEMPO ENTRE SALTOS//////////////////////////////////////////////////////////
    {
        canJump = false;
        CancelInvoke("EnableS");
        Invoke("EnableS", t);
    }
    void EnableS()
    {
        canJump = true;
    }
    public void Awake()//////////////////////////////////COSAS DEL INPUT QUE TAMPOCO ENTIENDO DEMASIADO Y MEJOR NO TOCAR P2//////////////////////////
    {
        Input1 = new Controls1();
        Input1.Player2.Jump.started += Jump;
        Input1.Player2.Jump.performed += Jump;
        Input1.Player2.Jump.canceled += Jump;
        Input1.Player2.Crouch.started += Crouch;
        Input1.Player2.Crouch.performed += Crouch;
        Input1.Player2.Crouch.canceled += Crouch;
        Input1.Player2.Punch.started += Punch;
        Input1.Player2.Punch.performed += Punch;
        Input1.Player2.Punch.canceled += Punch;
        Input1.Player2.PunchE.started += PunchE;
        Input1.Player2.PunchE.performed += PunchE;
        Input1.Player2.PunchE.canceled += PunchE;
        Input1.Player2.PunchF.started += PunchF;
        Input1.Player2.PunchF.performed += PunchF;
        Input1.Player2.PunchF.canceled += PunchF;
    }
    public void OnEnable()//////////////////////COSAS RARAS DEL INPUT QUE TAMPOCO ENTIENDO DEMASIADO Y MEJOR NO TOCAR P2////////////////////////////
    {
        Input1.Enable();
    }

    public void OnDisable()
    {
        Input1.Disable();
    }

}////////////////////////////////////ya dejo de gritar :)/////////////////////////////////////////////////////////////////////////////////////////////