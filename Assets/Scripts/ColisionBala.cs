using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionBala : MonoBehaviour
{
    public GameObject prefabExplosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // si la bala dura 3 segundos desaparece
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemigo")
        {
            GameObject effect = Instantiate(prefabExplosion, transform.position, transform.rotation);
            col.gameObject.GetComponent<Enemigo>().tocado();
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
    }
}
