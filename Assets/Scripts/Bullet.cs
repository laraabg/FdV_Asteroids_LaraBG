using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int speed = 20;
    public float maxLifeTime = 3;
    public Vector3 targetVector;

    void Start()
    {      
        // nada más nacer, le damos unos segundos de vida, 
        // lo suficiente para salir de la pantalla
        Destroy(gameObject, maxLifeTime);  
    }
        
    void Update()
    {   
        // la bala se mueve en la dirección del jugador al disparar
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
    }

    private void IncreaseScore()
    {
        // cuando un asteroide es destruido, llama a esta función para darnos puntos. 
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {  
        // llamamos a esta función cada vez que ganamos puntos para actualizar el marcador
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }
}
