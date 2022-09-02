﻿using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Dialogue.Editor
{
    public class ConnectionPoint
    {
        public Rect rect;

        public ConnectionPointType type;

        public Node node;

        public GUIStyle style;

        public UnityAction<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, UnityAction<ConnectionPoint> OnClickConnectionPoint)
        {
            this.node = node;
            this.type = type;
            this.style = style;
            this.OnClickConnectionPoint = OnClickConnectionPoint;
            rect = new Rect(0, 0, 10f, 20f);
        }

        public void Draw()
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

            switch (type)
            {
                case ConnectionPointType.In:
                    rect.x = node.rect.x - rect.width + 8f;
                    break;

                case ConnectionPointType.Out:
                    rect.x = node.rect.x + node.rect.width - 8f;
                    break;
            }

            if (GUI.Button(rect, "", style))
            {
                if (OnClickConnectionPoint != null)
                {
                    OnClickConnectionPoint(this);
                }
            }
        }
    }
}