using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NorthLab
{
    [ExecuteInEditMode]
    public class ScreenInspector : MonoBehaviour
    {

        public enum Types { None, iPhone, WaterDrop, PunchHole };
        public enum AlignTypes { Left, Center, Right };

        [SerializeField]
        private bool show = true;
        [SerializeField]
        private bool onlyInEditMode = true;
        [SerializeField]
        private bool mirrored = false;
        [SerializeField]
        private Color color = Color.black;
        [SerializeField, Space]
        private bool corners = true;
        [SerializeField]
        private Types type = Types.None;
        [SerializeField]
        private bool homeButton = true;
        [SerializeField]
        private AlignTypes alignType = AlignTypes.Center;

        private const int widthReference = 375;
        private const int heightReference = 812;

        private Texture[] textures;
#if UNITY_EDITOR

        private void OnEnable()
        {
            textures = new Texture[8];

            textures[0] = Resources.Load<Texture>("Corner");
            textures[1] = Resources.Load<Texture>("Notch");
            textures[2] = Resources.Load<Texture>("Notch_Rotated");
            textures[3] = Resources.Load<Texture>("HomeButton");
            textures[4] = Resources.Load<Texture>("HomeButton_Rotated");
            textures[5] = Resources.Load<Texture>("WaterDrop");
            textures[6] = Resources.Load<Texture>("WaterDrop_Rotated");
            textures[7] = Resources.Load<Texture>("PunchHole");
        }

        private void OnGUI()
        {
            if (!show)
                return;

            if (Application.isPlaying && onlyInEditMode)
                return;

            bool portrait = Screen.height > Screen.width;

            GUI.color = color;

            float sizeMultiplier;
            if (portrait)
            {
                sizeMultiplier = (float)Screen.width / widthReference;
            }
            else
            {
                sizeMultiplier = (float)Screen.height / heightReference;
            }

            if (corners)
            {
                float cornerSize = 64 * sizeMultiplier;
                GUI.DrawTexture(new Rect(0, 0, cornerSize, cornerSize), textures[0]);
                GUI.DrawTexture(new Rect(Screen.width, 0, -cornerSize, cornerSize), textures[0]);
                GUI.DrawTexture(new Rect(0, Screen.height, cornerSize, -cornerSize), textures[0]);
                GUI.DrawTexture(new Rect(Screen.width, Screen.height, -cornerSize, -cornerSize), textures[0]);
            }

            if (type == Types.iPhone)
            {
                float notchSize = 256 * sizeMultiplier;

                if (portrait)
                {
                    if (!mirrored)
                    {
                        GUI.DrawTexture(new Rect(Screen.width / 2 - notchSize / 2, 0, notchSize, notchSize), textures[1]);

                        if (homeButton)
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 - notchSize / 2, Screen.height - notchSize, notchSize, notchSize), textures[3]);
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(new Rect(Screen.width / 2 - notchSize / 2, Screen.height, notchSize, -notchSize), textures[1]);

                        if (homeButton)
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 - notchSize / 2, notchSize, notchSize, -notchSize), textures[3]);
                        }
                    }
                }
                else
                {
                    if (!mirrored)
                    {
                        GUI.DrawTexture(new Rect(0, Screen.height / 2 - notchSize / 2, notchSize, notchSize), textures[2]);

                        if (homeButton)
                        {
                            GUI.DrawTexture(new Rect(Screen.width - notchSize, Screen.height / 2 - notchSize / 2, notchSize, notchSize), textures[4]);
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(new Rect(Screen.width, Screen.height / 2 - notchSize / 2, -notchSize, notchSize), textures[2]);

                        if (homeButton)
                        {
                            GUI.DrawTexture(new Rect(notchSize, Screen.height / 2 - notchSize / 2, -notchSize, notchSize), textures[4]);
                        }
                    }
                }
            }
            else if (type == Types.WaterDrop)
            {
                float waterDropSize = 128 * sizeMultiplier;

                if (portrait)
                {
                    if (!mirrored)
                    {
                        GUI.DrawTexture(new Rect(Screen.width / 2 - waterDropSize / 2, 0, waterDropSize, waterDropSize), textures[5]);
                    }
                    else
                    {
                        GUI.DrawTexture(new Rect(Screen.width / 2 - waterDropSize / 2, Screen.height, waterDropSize, -waterDropSize), textures[5]);
                    }
                }
                else
                {
                    if (!mirrored)
                    {
                        GUI.DrawTexture(new Rect(0, Screen.height / 2 - waterDropSize / 2, waterDropSize, waterDropSize), textures[6]);
                    }
                    else
                    {
                        GUI.DrawTexture(new Rect(Screen.width, Screen.height / 2 - waterDropSize / 2, -waterDropSize, waterDropSize), textures[6]);
                    }
                }
            }
            else if (type == Types.PunchHole)
            {
                float punchHoleSize = 32 * sizeMultiplier;
                float offsetSize = 80 * sizeMultiplier;
                Vector2 pos;

                if (portrait)
                {
                    if (!mirrored)
                    {
                        pos.x = Screen.width / 2 - punchHoleSize / 2;
                        pos.y = 0;

                        switch (alignType)
                        {
                            case AlignTypes.Left:
                                pos.x = offsetSize - punchHoleSize;
                                break;

                            case AlignTypes.Right:
                                pos.x = Screen.width - offsetSize;
                                break;
                        }

                        GUI.DrawTexture(new Rect(pos.x, pos.y, punchHoleSize, punchHoleSize), textures[7]);
                    }
                    else
                    {
                        pos.x = Screen.width / 2 - punchHoleSize / 2;
                        pos.y = Screen.height - punchHoleSize;

                        switch (alignType)
                        {
                            case AlignTypes.Left:
                                pos.x = offsetSize - punchHoleSize;
                                break;

                            case AlignTypes.Right:
                                pos.x = Screen.width - offsetSize;
                                break;
                        }

                        GUI.DrawTexture(new Rect(pos.x, pos.y, punchHoleSize, punchHoleSize), textures[7]);
                    }
                }
                else
                {
                    if (!mirrored)
                    {
                        pos.x = 0;
                        pos.y = Screen.height / 2 - punchHoleSize / 2;

                        switch (alignType)
                        {
                            case AlignTypes.Left:
                                pos.y = Screen.height - offsetSize - punchHoleSize;
                                break;

                            case AlignTypes.Right:
                                pos.y = offsetSize;
                                break;
                        }

                        GUI.DrawTexture(new Rect(pos.x, pos.y, punchHoleSize, punchHoleSize), textures[7]);
                    }
                    else
                    {
                        pos.x = Screen.width - punchHoleSize;
                        pos.y = Screen.height / 2 - punchHoleSize / 2;

                        switch (alignType)
                        {
                            case AlignTypes.Left:
                                pos.y = Screen.height - offsetSize - punchHoleSize;
                                break;

                            case AlignTypes.Right:
                                pos.y = offsetSize;
                                break;
                        }

                        GUI.DrawTexture(new Rect(pos.x, pos.y, punchHoleSize, punchHoleSize), textures[7]);
                    }
                }
            }
        }
#endif
    }
}