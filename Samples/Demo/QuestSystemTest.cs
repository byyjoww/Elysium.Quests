using Elysium.Quests;
using Elysium.Quests.Flags;
using Elysium.Utils;
using Elysium.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestSystemTest : MonoBehaviour
{
    public QuestFlag flag;
    public SceneReference scene;
    public QuestSystemSO questSystem;
    public QuestDatabase quests;

    [Separator("Debug", true)]
    public List<Text> debugTextComponents = new List<Text>();

    private void Start()
    {
        // questSystem.Init();

        for (int i = 0; i < questSystem.AllQuests.Length; i++)
        {
            var obj = Instantiate(debugTextComponents[0], debugTextComponents[0].transform.parent);
            debugTextComponents.Add(obj);
        }
        
        debugTextComponents[0].gameObject.SetActive(false);
        questSystem.OnValueChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        questSystem.OnValueChanged -= Refresh;
    }

    private void Refresh()
    {
        for (int i = 0; i < questSystem.AllQuests.Length; i++)
        {
            SetText(debugTextComponents[i+1], questSystem.AllQuests[i]);
        }
    }

    private void SetText(Text _textComponent, QuestSO _quest)
    {
        _textComponent.text = $"{_quest.Title}: {_quest.CurrentState}";
    }

    [ContextMenu("Start Quest")]
    private void StartQuest()
    {
        questSystem.StartQuest(quests.Elements[0]);
        questSystem.StartQuest(quests.Elements[1]);
    }

    [ContextMenu("Complete Quest")]
    private void CompleteQuest()
    {
        quests.Elements[0].ForceCompleteQuest();
        quests.Elements[1].ForceCompleteQuest();
    }

    [ContextMenu("Collect Quest")]
    private void CollectQuest()
    {
        questSystem.CollectQuest(quests.Elements[0]);
        questSystem.CollectQuest(quests.Elements[1]);
    }

    [ContextMenu("Reset Quest")]
    private void ResetQuest()
    {
        quests.Elements[0].ResetQuest();
        quests.Elements[1].ResetQuest();
    }

    [ContextMenu("Load Scene")]
    private void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }

    [ContextMenu("Get Quest Flag")]
    private void GetFlag()
    {
        questSystem.QuestFlagDatabase.Add(flag, 1);
    }

    private void OnValidate()
    {
        quests.Refresh();
    }
}
