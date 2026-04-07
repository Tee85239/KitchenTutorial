using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private static Scene targetScene;

    public enum Scene
    {
        SampleScene,
        MainMenu,
        Loading




    }

    public static void Load(Scene targetSceneName)
    {
        Loader.targetScene = targetSceneName;
       
        SceneManager.LoadScene(Scene.Loading.ToString());

    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }


}
