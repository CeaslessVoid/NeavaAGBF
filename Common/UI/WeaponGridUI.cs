using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;

namespace NeavaAGBF.Common.UI
{
    public class WeaponGridUI : UIPanel
    {
        private Vector2 dragOffset;
        private bool dragging;

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);

            if (evt.Target == this)
            {
                StartDrag(evt);
            }
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);

            if (evt.Target == this)
            {
                EndDrag(evt);
            }
        }

        private void StartDrag(UIMouseEvent evt)
        {
            dragging = true;
            dragOffset = evt.MousePosition - new Vector2(Left.Pixels, Top.Pixels);
        }

        private void EndDrag(UIMouseEvent evt)
        {
            dragging = false;
            Left.Set(evt.MousePosition.X - dragOffset.X, 0f);
            Top.Set(evt.MousePosition.Y - dragOffset.Y, 0f);
            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (dragging)
            {
                Left.Set(Main.mouseX - dragOffset.X, 0f);
                Top.Set(Main.mouseY - dragOffset.Y, 0f);
                Recalculate();
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
        }
    }

    internal class WeaponGridUIState : UIState
    {
        public WeaponGridUI weaponGridPanel;

        public override void OnInitialize()
        {
            weaponGridPanel = new WeaponGridUI();
            weaponGridPanel.SetPadding(0);
            weaponGridPanel.Width.Set(200f, 0f);
            weaponGridPanel.Height.Set(200f, 0f);
            weaponGridPanel.BackgroundColor = new Color(73, 94, 171, 200);

            weaponGridPanel.Left.Set(Main.screenWidth / 2 - Width.Pixels / 2, 0f);
            weaponGridPanel.Top.Set(Main.screenHeight / 2 - Height.Pixels / 2, 0f);

            Append(weaponGridPanel);
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class WeaponGridUISystem : ModSystem
    {
        private UserInterface weaponGridInterface;
        internal WeaponGridUIState gridUIState;

        public override void Load()
        {
            weaponGridInterface = new UserInterface();
            gridUIState = new WeaponGridUIState();
            gridUIState.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (weaponGridInterface?.CurrentState != null)
            {
                weaponGridInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "WeaponGridUI: Panel",
                    delegate {
                        if (weaponGridInterface?.CurrentState != null)
                        {
                            weaponGridInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public void ToggleGrid()
        {
            if (weaponGridInterface.CurrentState == null)
            {
                weaponGridInterface.SetState(gridUIState);
            }
            else
            {
                weaponGridInterface.SetState(null);
            }
        }
    }

}
