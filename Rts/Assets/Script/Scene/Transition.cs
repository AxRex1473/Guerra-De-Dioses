using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject menuPause;
    public GameObject HUD;

    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Test_GAME_Ax");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        // Verifica si se presionó la tecla 'esc'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alterna el estado de pausa
            isPaused = !isPaused;

            // Alterna el estado del menú de pausa
            menuPause.SetActive(isPaused);
            HUD.SetActive(!isPaused);

            // Establece el Time.timeScale según corresponda
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
