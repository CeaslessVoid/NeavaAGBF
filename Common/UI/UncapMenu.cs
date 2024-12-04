using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader.UI;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NeavaAGBF.Common.UI
{
    // From Example Mod (ExampleDraggableUIPanel)
    public class UncapMenu : UIPanel
    {
        private Vector2 offset;
        private bool dragging;

        private Asset<Texture2D> backgroundTexture;

        public override void OnInitialize()
        {
            Width.Set(200f, 0f);
            Height.Set(60f, 0f);
            Left.Set(Main.screenWidth / 2 - Width.Pixels / 2, 0f);
            Top.Set(Main.screenHeight / 2 - Height.Pixels / 2, 0f);
            //BackgroundColor = new Color(73, 94, 171);

            backgroundTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Items/Tiles/UncapUi");

            Recalculate();
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);

            if (evt.Target == this)
            {
                DragStart(evt);
            }
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);

            if (evt.Target == this)
            {
                DragEnd(evt);
            }
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 endMousePosition = evt.MousePosition;
            dragging = false;

            Left.Set(endMousePosition.X - offset.X, 0f);
            Top.Set(endMousePosition.Y - offset.Y, 0f);

            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);

                Recalculate();
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(backgroundTexture.Value, dimensions.ToRectangle(), Color.White);
        }
    }
    internal class ExampleUIHoverImageButton : UIImageButton
    {
        internal string hoverText;

        public ExampleUIHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
            this.hoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                UICommon.TooltipMouseText(hoverText);
            }
        }
    }


    internal class UncapUIState : UIState
    {
        public UncapMenu uncapMenu;

        public override void OnInitialize()
        {
            uncapMenu = new UncapMenu();
            uncapMenu.SetPadding(0);

            SetRectangle(uncapMenu, left: 400f, top: 100f, width: 170f, height: 70f);
            uncapMenu.BackgroundColor = new Color(73, 94, 171);


            Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
            ExampleUIHoverImageButton closeButton = new ExampleUIHoverImageButton(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52"));
            SetRectangle(closeButton, left: 170f, top: 10f, width: 22f, height: 22f);
            closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
            uncapMenu.Append(closeButton);

            Append(uncapMenu);
        }

        private void SetRectangle(UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            ModContent.GetInstance<AnvilUISystem>().HideMyUI();
        }

    }

    [Autoload(Side = ModSide.Client)] // This attribute makes this class only load on a particular side. Naturally this makes sense here since UI should only be a thing clientside. Be wary though that accessing this class serverside will error
    public class AnvilUISystem : ModSystem
    {
        private UserInterface anivlUserInterface;
        internal UncapUIState uncapUI;

        public void ShowMyUI()
        {
            anivlUserInterface?.SetState(uncapUI);
        }

        public void HideMyUI()
        {
            anivlUserInterface?.SetState(null);
        }

        public override void Load()
        {
            anivlUserInterface = new UserInterface();
            uncapUI = new UncapUIState();

            uncapUI.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (anivlUserInterface?.CurrentState != null)
            {
                anivlUserInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "UncapMenu: UncapMenu",
                    delegate {
                        if (anivlUserInterface?.CurrentState != null)
                        {
                            anivlUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }

}
