///File: LevelManager.cs
///Author: Braden Fish
///Date: 1/26/2018
///----------------------------------------------------------------------------------------------------------
///Changelog:
///
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controls the switching between scenes
/// </summary>
public class LevelManager : MonoBehaviour
{

    [Tooltip("Amount of sec till auto load")]
    public float autoLoadNextLevelAfter;

    /// <summary>
    /// On start checks if autoload is set, if set will load next level after the set time
    /// </summary>
	void Start()
    {
        if (autoLoadNextLevelAfter == 0)
        {
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }

    /// <summary>
    /// Loads a level
    /// </summary>
    /// <param name="name">Level to load</param>
    public void LoadLevel(string name)
    {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
	public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    /// <summary>
    /// Loads the level that is next in the Build Settings
    /// </summary>
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
