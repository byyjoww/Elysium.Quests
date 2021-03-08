using Elysium.Utils;
using Elysium.Utils.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Elysium.Quests
{
    public class LoadSceneObjective : QuestObjective
    {
        [Separator("Settings", true)]
        [SerializeField] private SceneReference scene = default;

        public override float GetObjectiveProgress() => 0;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.path == this.scene)
            {
                SetObjectiveAsComplete();
            }
            else
            {
                SetObjectiveAsIncomplete();
            }
        }

        public override void CheckForObjectiveComplete()
        {
            if (SceneManager.GetActiveScene().path == scene)
            {
                SetObjectiveAsComplete();
            }
        }

        public override void StartListeningForObjectives()
        {
            // Debug.Log("OnSceneLoaded subscribed to scene load.");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void StopListeningForObjectives()
        {
            // Debug.Log("OnSceneLoaded unsubscribed to scene load.");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}