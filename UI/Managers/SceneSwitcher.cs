using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightFight.UI.Managers
{

    /**
     * 场景切换器
     */
    internal class SceneSwitcher: MonoBehaviour
    {
        public void LoadScene(string sceneName, GameObject GlobalKellner)
        {
            StartCoroutine(_LoadScene(sceneName));

        }

        private System.Collections.IEnumerator _LoadScene(string sceneName)
        {
            AsyncOperation _asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!_asyncLoad.isDone)
                yield return null;
        }
    }

}
