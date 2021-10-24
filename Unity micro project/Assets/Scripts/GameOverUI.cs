using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;
    public GameObject canvas;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        this.canvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry() {
        this.canvas.SetActive(false);
        Time.timeScale = 1;
        GameController.instance.ResetScore();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
