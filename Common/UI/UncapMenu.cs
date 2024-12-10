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
using NeavaAGBF.Common.Players;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Terraria.GameContent;
using Terraria.GameInput;
using NeavaAGBF.Common.Items;
using Terraria.ModLoader.IO;
using Microsoft.Build.Utilities;

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

            NeavaAGBFPlayer.UpdateIsClicking();

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

            if (clickCooldownTimer > 0)
            {
                clickCooldownTimer--;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(backgroundTexture.Value, dimensions.ToRectangle(), Color.White);

            Texture2D weaponSlotTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/Empty").Value;

            Texture2D materialTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/WeaponSlot").Value;

            Color slotColor = new Color(255, 255, 255, 222);

            Vector2 mainPosition = new Vector2(
                dimensions.X + (dimensions.Width / 2) - (weaponSlotTexture.Width / 2),
                dimensions.Y + (dimensions.Height / 2) - (weaponSlotTexture.Height / 2)
            );

            Vector2 realPostion = new Vector2(
                dimensions.X + (dimensions.Width / 2),
                dimensions.Y + (dimensions.Height / 2)
            );

            spriteBatch.Draw(
                weaponSlotTexture,
                mainPosition,
                null,
                slotColor,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
            );

            // Bullshit
            // Everything here is horrible code


            if (CloseToGui(realPostion))
            {
                NeavaAGBFPlayer._isMouseOverSlot = true;
                Main.instance.MouseText(Language.GetText("Mods.NeavaAGBF.SimpleText.UncapItem").Value);



                if (PlayerInput.Triggers.Current.MouseLeft && (Main.mouseItem.type != ItemID.None || playerMod.UncapTarget.type != ItemID.None))
                {
                    if (!boolShit && clickCooldownTimer == 0)
                    {
                        boolShit = true;
                        clickCooldownTimer = 10;
                        playerMod.boolHasChecked = false;
                        Main.playerInventory = true;

                        foreach (Item inputMaterial in playerMod.inputMaterials)
                        {
                            if (inputMaterial != null && inputMaterial.type != ItemID.None)
                            {
                                Item leftover = Main.LocalPlayer.GetItem(Main.myPlayer, inputMaterial, GetItemSettings.InventoryEntityToPlayerInventorySettings);

                                if (leftover != null && leftover.type != ItemID.None && leftover.stack > 0)
                                {
                                    Item.NewItem(Main.LocalPlayer.GetSource_Misc("UncapEject"), Main.LocalPlayer.Center, leftover.type, leftover.stack);
                                }
                            }
                        }

                        playerMod.requirementItems.Clear();
                        playerMod.inputMaterials.Clear();

                        SoundEngine.PlaySound(SoundID.Grab);

                        Item temp = playerMod.UncapTarget;

                        playerMod.UncapTarget = Main.mouseItem;
                        Main.mouseItem = temp;
                    }
                }


                if (!NeavaAGBFPlayer.IsClicking)
                {
                    boolShit = false;
                }

                if (playerMod.UncapTarget.type != ItemID.None)
                {
                    Main.HoverItem = playerMod.UncapTarget;
                    Main.instance.MouseText(playerMod.UncapTarget.Name, playerMod.UncapTarget.rare);
                }

            }

            if (playerMod.UncapTarget.type != ItemID.None )
            {
                WeaponSkillsGlobalItem globalItem = playerMod.UncapTarget.GetGlobalItem<WeaponSkillsGlobalItem>();

                if (globalItem != null && !playerMod.boolHasChecked)
                {
                    if (globalItem.currentUncap >= globalItem.maxUncap)
                    {
                        Utils.DrawBorderString(spriteBatch,
                            Language.GetText("Mods.NeavaAGBF.SimpleText.NoMoreUncapItem").Value,
                            new Vector2(realPostion.X, realPostion.Y + 60),
                            Color.Red,
                            1f,
                            0.5f,
                            0.5f
                        );
                    }
                    else if (globalItem.currentUncap < 3)
                    {
                        Item reqItem = new Item();

                        playerMod.inputMaterials.Add(reqItem.Clone());

                        reqItem.SetDefaults(playerMod.UncapTarget.type);
                        playerMod.requirementItems.Add(reqItem);
                        playerMod.boolHasChecked = true;


                    }
                    else
                    {
                        List<UncapRequirement> requirements = globalItem.UncapGroup.GetRequirements(globalItem.currentUncap - 2);
                        foreach (var requirement in requirements)
                        {
                            Item reqItem = new Item();

                            playerMod.inputMaterials.Add(reqItem.Clone());

                            reqItem.SetDefaults(requirement.ItemID);
                            reqItem.stack = requirement.Quantity;
                            playerMod.requirementItems.Add(reqItem);
                            
                        }

                        playerMod.boolHasChecked = true;
                    }

                }

                Vector2 slotPosition = mainPosition + new Vector2(0, 70);
                DrawRequirementSlots(spriteBatch, slotPosition);
            }

            Texture2D itemTexture = TextureAssets.Item[playerMod.UncapTarget.type].Value;
            Main.spriteBatch.Draw(itemTexture, realPostion, null, Color.White, 0f, Utils.Size(itemTexture) / 2f, NeavaAGBFPlayer.ScaleToFit(itemTexture), SpriteEffects.None, 0f);

            

        }

        public bool CloseToGui(Vector2 position)
        {
            return Vector2.Distance(position, Main.MouseScreen) <= 26f;
        }

        private void DrawRequirementSlots(SpriteBatch spriteBatch, Vector2 basePosition)
        {
            const int boxSpacing = 60;
            const float boxScale = 1.0f;
            
            Color boxColor2 = new Color(255, 255, 255, 255);
            Color Color1 = new Color(0, 0, 0, 0);
            Color Color2 = new Color(255, 255, 255, 255);

            int totalSlots = playerMod.requirementItems.Count;
            float totalWidth = (totalSlots - 1) * boxSpacing;
            Vector2 startPosition = basePosition - new Vector2(totalWidth / 2, 0) + new Vector2(25,20);

            if (playerMod.requirementItems == null || playerMod.inputMaterials == null)
                return;

            for (int i = 0; i < totalSlots; i++)
            {
                Vector2 slotPosition = startPosition + new Vector2(i * boxSpacing, 0);
                Item reqItem = playerMod.requirementItems[i];
                Item inputItem = playerMod.inputMaterials[i];

                Texture2D slotTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/WeaponSlot").Value;
                spriteBatch.Draw(slotTexture, slotPosition, null, boxColor2, 0f, Utils.Size(slotTexture) / 2f, boxScale, SpriteEffects.None, 0f);

                if (reqItem.type != ItemID.None)
                {
                    Color boxColor = new Color(255, 255, 255, 50);

                    Main.instance.LoadItem(reqItem.type);

                    Texture2D itemTexture = TextureAssets.Item[reqItem.type].Value;

                    if (inputItem != null && inputItem.type != ItemID.None)
                        boxColor = boxColor2;

                    int frameCount = Main.itemAnimations[reqItem.type]?.FrameCount ?? 1;
                    int frameHeight = frameCount > 1 ? itemTexture.Height / frameCount : itemTexture.Height;
                    int frameY = frameCount > 1 ? ((int)(Main.GlobalTimeWrappedHourly * 10) % frameCount) * frameHeight : 0;

                    //if (frameCount > 1)
                    //{
                    //    int frame = (int)(Main.GlobalTimeWrappedHourly * 10) % frameCount; // Adjust speed as needed
                    //    frameY = frame * frameHeight;
                    //}

                    Rectangle sourceRectangle = new Rectangle(0, frameY, itemTexture.Width, frameHeight);

                    Vector2 origin = new Vector2(itemTexture.Width / 2f, frameHeight / 2f);

                    spriteBatch.Draw(
                        itemTexture,
                        slotPosition,
                        sourceRectangle,
                        boxColor,
                        0f,
                        origin,
                        boxScale,
                        SpriteEffects.None,
                        0f
                    );

                    if (Main.mouseX >= slotPosition.X - (slotTexture.Width / 2 * boxScale) &&
                        Main.mouseX <= slotPosition.X + (slotTexture.Width / 2 * boxScale) &&
                        Main.mouseY >= slotPosition.Y - (slotTexture.Height / 2 * boxScale) &&
                        Main.mouseY <= slotPosition.Y + (slotTexture.Height / 2 * boxScale))
                    {
                        Main.HoverItem = reqItem.Clone();
                        Main.instance.MouseText(reqItem.Name, reqItem.rare);
                    }

                    if (reqItem.stack > 1)
                    {
                        string stack = (inputItem != null && inputItem.type != ItemID.None) ? inputItem.stack.ToString() : reqItem.stack.ToString();
                        Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, stack, slotPosition.X - 18f, slotPosition.Y, Color2, Color1, new Vector2(0f), 0.85f);
                    }

                    if (CloseToGui(slotPosition)) // + new Vector2(10, 10)
                    {
                        HandleRequirementSlotClick(reqItem, i);
                    }
                }
                
            }


            Texture2D confirmTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/UncapWeapon").Value;
            Vector2 iconPosition = basePosition - new Vector2(60, 60);
            

            if (CloseToGui(iconPosition + new Vector2(15, 15)))
            {
                confirmTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/UncapWeapon_2").Value;
                spriteBatch.Draw(confirmTexture, iconPosition, null, Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);

                if (PlayerInput.Triggers.Current.MouseLeft && !boolShit)
                {
                    if (AreRequirementsMet())
                    {
                        boolShit = true;
                        PerformUncap();
                    }
                    
                }

                if (!NeavaAGBFPlayer.IsClicking)
                {
                    boolShit = false;
                }

            }
            else
            {
                spriteBatch.Draw(confirmTexture, iconPosition, null, Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            }
        }

        private void PerformUncap()
        {
            if (playerMod.UncapTarget.type == ItemID.None)
                return;

            WeaponSkillsGlobalItem globalItem = playerMod.UncapTarget.GetGlobalItem<WeaponSkillsGlobalItem>();

            if (globalItem == null)
                return;

            playerMod.inputMaterials.Clear();
            playerMod.requirementItems.Clear();

            globalItem.currentUncap++;
            globalItem.maxLevel += globalItem.skillLevelPerCap;
        }

        private void HandleRequirementSlotClick(Item requiredItem, int index)
        {
            if (clickCooldownTimer > 0) return;

            if (PlayerInput.Triggers.Current.MouseLeft && !boolShit )
            {
                boolShit = true;
                clickCooldownTimer = 10;

                Main.playerInventory = true;

                if (Main.mouseItem.type == requiredItem.type)
                {
                    

                    if (playerMod.inputMaterials[index].type == ItemID.None)
                    {
                        SoundEngine.PlaySound(SoundID.Grab);
                        playerMod.inputMaterials[index] = Main.mouseItem.Clone();
                        playerMod.inputMaterials[index].stack = Math.Min(Main.mouseItem.stack, requiredItem.stack);


                        Player player = Main.LocalPlayer?.GetModPlayer<NeavaAGBFPlayer>().Player;
                        ref Item item = ref player.inventory[58];
                        item.stack -= playerMod.inputMaterials[index].stack;

                        if (item.stack <= 0)
                            item.SetDefaults();

                        item = ref Main.mouseItem;
                        item.stack -= playerMod.inputMaterials[index].stack;

                        if (item.stack <= 0)
                            item.SetDefaults();

                    }

                    else if (playerMod.inputMaterials[index].type == requiredItem.type)
                    {
                        SoundEngine.PlaySound(SoundID.Grab);
                        int materialsNeeded = playerMod.requirementItems[index].stack - playerMod.inputMaterials[index].stack;
                        playerMod.inputMaterials[index].stack += Math.Min(Main.mouseItem.stack, materialsNeeded);


                        Player player = Main.LocalPlayer?.GetModPlayer<NeavaAGBFPlayer>().Player;
                        ref Item item = ref player.inventory[58];
                        item.stack -= materialsNeeded;

                        if (item.stack <= 0)
                            item.SetDefaults();

                        item = ref Main.mouseItem;
                        item.stack -= materialsNeeded;

                        if (item.stack <= 0)
                            item.SetDefaults();
                    }
                }

                else if ((Main.mouseItem == null || Main.mouseItem.IsAir) & playerMod.inputMaterials[index].type != ItemID.None)
                {
                    SoundEngine.PlaySound(SoundID.Grab);

                    Main.mouseItem = playerMod.inputMaterials[index];
                    playerMod.inputMaterials[index] = new Item();
                }
            }

            if (!NeavaAGBFPlayer.IsClicking)
            {
                boolShit = false;
            }
        }

        private bool AreRequirementsMet()
        {
            if (playerMod.requirementItems.Count < 1) return false;

            for (int i = 0; i < playerMod.requirementItems.Count; i++)
            {
                if (playerMod.inputMaterials[i].type != playerMod.requirementItems[i].type || playerMod.inputMaterials[i].stack < playerMod.requirementItems[i].stack)
                {
                    return false; 
                }
            }
            return true;
        }


        private bool boolShit = false;

        //private List<Item> requirementItems = new();
        private NeavaAGBFPlayer playerMod => Main.LocalPlayer?.GetModPlayer<NeavaAGBFPlayer>();

        //private List<Item> inputMaterials = new List<Item>();

        //private bool boolHasChecked = false;

        private int clickCooldownTimer = 0;

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

        public override void Update(GameTime gameTime)
        {
            Point? coord = Main.LocalPlayer.GetModPlayer<NeavaAGBFPlayer>().ForgeLocation;
            if (coord != null)
            {
                if (Vector2.Distance(Main.LocalPlayer.Center, Utils.ToVector2(coord.Value) * 16f) > 256f)
                {
                    ModContent.GetInstance<AnvilUISystem>().HideMyUI();
                }
            }

            base.Update(gameTime);
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

            Player player = Main.LocalPlayer?.GetModPlayer<NeavaAGBFPlayer>().Player;
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
