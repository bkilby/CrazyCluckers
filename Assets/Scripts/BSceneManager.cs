//---------------------------------------------------------------------------
// Usings
//---------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;


//---------------------------------------------------------------------------
// Class: BSceneManager
//---------------------------------------------------------------------------
class BSceneManager : MonoBehaviour
{

    //---------------------------------------------------------------------------
    /// <summary>
    /// GotoScene
    /// </summary>
    /// <param name="sceneName"></param>
    //---------------------------------------------------------------------------
    public void GotoScene(string sceneName)
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);

    }


    //---------------------------------------------------------------------------
    /// <summary>
    /// Quit
    /// </summary>
    //---------------------------------------------------------------------------
    public void Quit()
    {

        Application.Quit();

    }

}