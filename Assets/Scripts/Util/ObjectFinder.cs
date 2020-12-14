using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectFinder
{
    public static T[] FindEvenInactiveComponents<T>(bool searchAllScenes = false) where T : Component
    {
        IEnumerable<T> results = FindEvenInactiveComponentsInValidScenes<T>(searchAllScenes);
        return results.ToArray();
    }

    private static IEnumerable<T> FindEvenInactiveComponentsInValidScenes<T>(bool searchAllScenes, bool stopOnMatch = false) where T : Component
    {
        IEnumerable<T> results;
        if (searchAllScenes)
        {
            List<T> allSceneResults = new List<T>();
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                allSceneResults.AddRange(FindEvenInactiveComponentsInScene<T>(SceneManager.GetSceneAt(sceneIndex), stopOnMatch));
            }
            results = allSceneResults;
        }
        else
        {
            results = FindEvenInactiveComponentsInScene<T>(SceneManager.GetActiveScene(), stopOnMatch);
        }

        return results;
    }

    private static IEnumerable<T> FindEvenInactiveComponentsInScene<T>(Scene scene, bool stopOnMatch = false)
    {
        List<T> results = new List<T>();
        if (!scene.isLoaded)
        {
            return results;
        }

        foreach (GameObject rootObject in scene.GetRootGameObjects())
        {
            if (stopOnMatch)
            {
                T foundComponent = rootObject.GetComponentInChildren<T>(true);
                if (foundComponent != null)
                {
                    results.Add(foundComponent);
                    return results;
                }
            }
            else
            {
                results.AddRange(rootObject.GetComponentsInChildren<T>(true));
            }
        }

        return results;
    }
}
