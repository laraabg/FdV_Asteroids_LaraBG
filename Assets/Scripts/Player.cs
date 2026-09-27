using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    public float thrustForce = 5f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;
    
    private Vector2 thrustDirection;
    private Rigidbody _rigidbody;  

    public static int SCORE = 0; 
    public static float xBorderLimit, yBorderLimit;
    
    void Start()   
    {   
        Time.timeScale = 1f;     
        // rigidbody nos permite aplicar fuerzas en el jugador      
        _rigidbody = GetComponent<Rigidbody>();   

        yBorderLimit = Camera.main.orthographicSize+1;
        xBorderLimit = (Camera.main.orthographicSize+1) * Screen.width / Screen.height;
    }
        
    private void FixedUpdate() 
    {        
        // obtenemos las pulsaciones de teclado   
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime; 
        float thrust = Input.GetAxis("Thrust") * thrustForce; 
        // la dirección de empuje por defecto es .right (el eje X positivo)
        thrustDirection = transform.right; 
        // rotamos con el eje "Rotate" negativo para que la dirección sea correcta
        transform.Rotate(Vector3.forward, -rotation);  
        // añadimos la fuerza capturada arriba a la nave del jugador
        _rigidbody.AddForce(thrust * thrustDirection);
    }

    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit+1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit-1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit+1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit-1;
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // al pulsar espacio, instanciamos una bala
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // cargamos el script Bullet del GO bullet en la variable balaScript
            Bullet balaScript = bullet.GetComponent<Bullet>();

            // le damos dirección a la bala
            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
