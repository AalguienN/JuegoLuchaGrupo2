using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public float potencia;
    public float daño;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(this.gameObject.transform.position.x > 0){}
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( direccion * potencia, 100));
            collision.GetComponent<Atributos>().changeHP(daño);
        }


    }
}
