using System.Collections.Generic;
using Elysium.Dialogue;
using UnityEngine;

namespace Elysium.Quests
{
    [CreateAssetMenu(fileName = "QuestFactorySO", menuName = "Scriptable Objects/Quests/Quest Factory")]
    public class QuestFactorySO : ScriptableObject, IQuestFactory
    {
        [SerializeField] private string id = default;
        [SerializeField] private string title = default;
        [SerializeField] private string description = default;
        [SerializeField] private Sprite icon = default;
        [SerializeField] private QuestDialogueFactory dialogue = default;

        public string ID => id;
        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public IQuestDialogueFactory Dialogue => dialogue;

        public bool CanStart(ISet<string> completedQuests)
        {
            // TODO: Add prerequisites
            return !completedQuests.Contains(id);
        }

        public IQuest Create()
        {
            return new Quest
            {
                ID = id,
                Title = title,
                Description = description,
                Icon = icon,
            };
        }
    }
}
