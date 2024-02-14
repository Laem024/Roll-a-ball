using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorController : MonoBehaviour
{
    private Rigidbody rb;

    private int contador;
    public Text textoContador;
    public Text textoGanar;
    public Text timerText;

    public float speed;

    public float timer = 0.00f;
    public float timerToEnd = 0.00f;
    //
    private bool timeIsRunning = false;
    private bool timerToEndIsRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;

        setTextoContador();

        textoGanar.text="";
        timerText.text ="";
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if(timer >= 0)
            {
                timer += Time.deltaTime;
                DisplayTime(timer);
            }
        }   

        

        if(timerToEndIsRunning)
        {
            if(timerToEnd >= 0)
            {
                timerToEnd += Time.deltaTime;
                DisplaytimerToEnd(timerToEnd);
            } 
        }    
    }

    // FixedUpdate is called 0, 1, or more times per frame
    void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

        rb.AddForce(movimiento*speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            contador++;
            setTextoContador();
        }
    }

    void setTextoContador()
    {
        textoContador.text = "Contador: "+ contador.ToString();

        if(contador >= 12)
        {
            textoGanar.text="¡Ganaste!";
            timerToEndIsRunning = false;
            timeIsRunning= true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay %60);
        //timerText.text = seconds.ToString();
        if(seconds == 5)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void DisplaytimerToEnd(float timerToEndDisplay)
    {
        
        float seconds = Mathf.FloorToInt(timerToEndDisplay %60);
        timerText.text = (60 - seconds).ToString();
        if(seconds >= 59)
        {
            textoGanar.text="¡Perdiste!";
            timerToEndIsRunning = false;
            timeIsRunning= true;
        } 

        timerToEndDisplay += 1;
    }
    
}
