using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    bool puedeSaltar = true;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;

    //Transform transform;//ayuda a cambiar el valor inicial -> es un componente obligatorio porque siempre existe
    //int x = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando script ded player");//mensaje en consola
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity.x; sirve para obtener velocidad actual de x
        //rb.velocity.y; sirve para obtener velocidad actual de y
        //rb.velocity = new Vector2(x, y);

        //GetKey -> cuando mantengo presionado
        //GetKeyUp -> cuando presiono suelto
        //GetKeyDown -> cuando suelto la tecla
        //rb.velocity = new Vector2(0, rb.velocity.y);//con esto se setea la velocidad inicial
        //                                            //animator.SetInteger("Estado", 0);
        //ChangeAnimation(ANIMATION_QUIETO);

        if (Input.GetKey(KeyCode.RightArrow))//si presiono flecha derecha va a la derecha
        {
            rb.velocity = new Vector2(10, rb.velocity.y); //aqí se ajusta el movimiento que se va a realizar
            sr.flipX = false;//con esto se hace que se voltee o no el personaje haciendo una rotación en el eje x
            //animator.SetInteger("Estado", 1);//con esto se asigna el valor a estado para poder cambiar entre animaciones 
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            //animator.SetInteger("Estado", 1);
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && puedeSaltar)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            puedeSaltar = false;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);//con esto se setea la velocidad inicial
                                                        //animator.SetInteger("Estado", 0);
            ChangeAnimation(ANIMATION_QUIETO);
        }
        /*if (Input.GetKey(KeyCode.UpArrow))//si presiono flecha arriba
        {
            ChangeAnimation(ANIMATION_SALTAR);
            rb.AddForce(Vector2.up * fuerzaSalto);
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }*/
        //transform.position = new Vector3(100, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Puede Saltar");
        puedeSaltar = true;
        if (collision.gameObject.tag == "Enemy") 
        {
            Debug.Log("Estas Muerto");
        }
    }

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("Puede Saltar");
    //    puedeSaltar = true;
    //}

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
