using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Dialogue.Editor
{
    public class DialogueNode : Node
    {
        public DialogueNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, UnityAction<ConnectionPoint> OnClickInPoint, UnityAction<ConnectionPoint> OnClickOutPoint, UnityAction<Node> OnClickRemoveNode) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
        {

        }

        public override void DrawContent()
        {
            var content = new GUIContent("This is a box");
            GUI.Box(rect, content);
        }
    }
}