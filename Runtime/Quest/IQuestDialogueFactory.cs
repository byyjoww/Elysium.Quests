namespace Elysium.Quests
{
    public interface IQuestDialogueFactory
    {
        IQuestDialogue GetAcceptDialogue(IQuestFactory factory, IQuestReceiver receiver);
        IQuestDialogue GetInProgressDialogue();
        IQuestDialogue GetDeliveryDialogue(IQuest quest, IQuestReceiver receiver);
    }
}
