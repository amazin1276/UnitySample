using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Expansion.Functions;



namespace Expansion.Components
{
    public class Hightlight : MonoBehaviour
    {
        [Space()]
        public SpriteRenderer spriteRenderer;
        public int Strength = 40;
        public bool isHightlight;

        private Color previousRGB;
        private bool isHightlighted;

        void Start()
        {
            SetSpriteRenderer();
        }

        void Update()
        {
            if (isHightlight) LightUp();
            else LightDown();

            Warning();
        }

        private void SetSpriteRenderer()
        {
            bool isNull = spriteRenderer == null;

            if (isNull)
            {
                spriteRenderer = this.GetComponent<SpriteRenderer>();
            }
        }

        private void LightUp()
        {
            void SaveColor()
            {
                if (!isHightlighted) previousRGB = spriteRenderer.color;
            }

            Color ComputeLighterColor()
            {
                float add = Strength / 255;
                Color correntColor = spriteRenderer.color;
                Color LightUpColor = new Color(
                    correntColor.r + add,
                    correntColor.g + add,
                    correntColor.b + add
                );

                return LightUpColor;
            }

            SaveColor();
            spriteRenderer.color = ComputeLighterColor();
            isHightlighted = true;
        }

        private void LightDown()
        {
            if (!isHightlighted) return;

            spriteRenderer.color = previousRGB;
            isHightlighted = false;
        }

        private void Warning()
        {
            bool isSpriteRendererNull = spriteRenderer == null;
            if (isSpriteRendererNull)
            {
                Expansion.Functions.Warning("SpriteRendereコンポーネントが取得できませんでした。", "Highlight/Warning");
            }
        }
    }
}