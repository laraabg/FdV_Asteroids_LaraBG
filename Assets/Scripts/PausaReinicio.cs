using UnityEngine;

public class PausaReinicio : MonoBehaviour
{
    public GameObject menu;

    private bool pausa = false;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (pausa)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Reanudar()
    {
        if(menu != null)
            menu.SetActive(false);
        Time.timeScale = 1f;
        pausa = false;
    }

    public void Pausar()
    {
        if(menu != null)
            menu.SetActive(true);
        Time.timeScale = 0f;
        pausa = true;
    }
}
