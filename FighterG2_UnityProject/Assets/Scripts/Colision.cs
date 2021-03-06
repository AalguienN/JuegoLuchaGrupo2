using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public Vector2 potenciaV2;
    public float daño;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            
        {
            if (collision.gameObject.name == "BunnyP1") collision.gameObject.GetComponent<Movimiento>().BunnyGolpeado();
            if (collision.gameObject.name == "Doge") collision.gameObject.GetComponent<Movimiento2>().damaged = true;
            if(this.gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * potenciaV2.x, potenciaV2.y));
                collision.GetComponent<Atributos>().changeHP(-daño);
            } else if (this.gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * potenciaV2.x, potenciaV2.y));
                    collision.GetComponent<Atributos>().changeHP(-daño); 
            }

            
        }


    }
}
