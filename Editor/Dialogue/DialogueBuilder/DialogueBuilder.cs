using UnityEditor;
using UnityEngine;

namespace Elysium.Dialogue.Editor
{
    public class DialogueBuilder : NodeBasedEditor<DialogueNode>
    {
        private const string WINDOW_NAME = "Dialogue Builder";

        [MenuItem("Window/" + WINDOW_NAME)]
        protected static void OpenWindow()
        {
            DialogueBuilder window = GetWindow<DialogueBuilder>();
            window.titleContent = new GUIContent(WINDOW_NAME);
        }

        protected override DialogueNode CreateNode(Vector2 mousePosition)
        {
            return new DialogueNode(
                mousePosition,
                200,
                50,
                nodeStyle,
                selectedNodeStyle,
                inPointStyle,
                outPointStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode
            );
        }
    }
}