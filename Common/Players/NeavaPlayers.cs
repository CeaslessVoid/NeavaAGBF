using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using NeavaAGBF.Content.Items;
using Terraria.UI.Chat;
using ReLogic.Graphics;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.ID;
using System.Security.Policy;
using Terraria.ModLoader.IO;
using System;

namespace NeavaAGBF.Common.Players
{
    public class NeavaAGBFPlayer : ModPlayer
    {
        public static Item[] WeaponGrid = new Item[9];

        public static Chest Chest = new Chest(true);

        public static int ChestId = -114514;

        // Delete when not needed
        private bool JC_SB;

        private static bool CSH = false;

        private bool ResetGrid;

        public static bool DKUI = false;

        public static bool IsClicking = false;


        public static float ITDX(Texture2D texture2D)
        {
            float maxSize = 42.5f; // Adjust maximum size
            return Math.Min(maxSize / texture2D.Width, maxSize / texture2D.Height);
        }

        public override void ResetEffects()
        {
            if (!this.ResetGrid)
            {
                ResetGrid = true;
                for (int i = 0; i < WeaponGrid.Length; i++)
                {
                    WeaponGrid[i] = new Item(0,1,0);
                }
            }

            if (CSH)
            {
                if (!PlayerInput.Triggers.Current.MouseLeft)
                {
                    CSH = false;
                }
                base.Player.delayUseItem = true;
                base.Player.controlUseItem = false;
            }
        }



        public static void DrawWeaponGrid()
        {
            Vector2 UI;
            UI = new Vector2(0, 0);

            Texture2D GridOpenTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/UI_1", AssetRequestMode.AsyncLoad).Value;
            Texture2D GridOpenTexture2 = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/UI_2", AssetRequestMode.AsyncLoad).Value;

            Player Player = Main.LocalPlayer;
            Vector2 vector2 = new Vector2(586f, 295f) + UI;
            bool flag = Vector2.Distance(vector2, Main.MouseScreen) <= 19f;

            if (flag)
            {
                NeavaAGBFPlayer.CSH = true;
                Main.spriteBatch.Draw(GridOpenTexture2, vector2, null, new Color(255, 255, 155), 0f, Utils.Size(GridOpenTexture2) / 2f, 0.9f, 0, 0f);
                Main.instance.MouseText(Language.GetText("Mods.NeavaAGBF.OpenGrid").Value, 0, 0, -1, -1, -1, -1, 0);

            }
            Main.spriteBatch.Draw(GridOpenTexture, vector2, null, new Color(255, 255, 255), 0f, Utils.Size(GridOpenTexture) / 2f, 0.9f, 0, 0f);

            if (flag && !IsClicking && PlayerInput.Triggers.Current.MouseLeft) // Ensures one click per toggle
            {
                NeavaAGBFPlayer.CSH = true;
                if (!NeavaAGBFPlayer.DKUI)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
                NeavaAGBFPlayer.DKUI = !NeavaAGBFPlayer.DKUI;
                Main.mouseLeftRelease = false; // Prevent further toggles in the same click
            }

            if (NeavaAGBFPlayer.DKUI)
            {
                Texture2D UID = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/WeaponSlot", AssetRequestMode.AsyncLoad).Value;

                Color color;
                color = new Color(255, 255, 255, 222);

                Vector2 item_0 = new Vector2(580f, 345f) + UI;

                const int SlotCount = 9; // Number of slots
                const int SlotsPerRow = 3; // Slots per row in the grid
                const float SlotSize = 52f; // Size of each slot
                const float SlotSpacing = 60f; // Spacing between slots
                Vector2 StartingPosition = new Vector2(580f, 345f); // Top-left corner of the grid
                Vector2 uiOffset = UI; // Offset for UI positioning


                // Loop through all slots
                for (int i = 0; i < SlotCount; i++)
                {
                    // Calculate row and column
                    int row = i / SlotsPerRow;
                    int column = i % SlotsPerRow;

                    // Calculate position
                    Vector2 slotPosition = StartingPosition + uiOffset + new Vector2(column * SlotSpacing, row * SlotSpacing);

                    // Draw the slot background
                    Main.spriteBatch.Draw(UID, slotPosition, null, color, 0f, Utils.Size(UID) / 2f, 0.85f, 0, 0f);

                    Texture2D itemTexture = TextureAssets.Item[NeavaAGBFPlayer.WeaponGrid[i].type].Value;
                    Main.spriteBatch.Draw(itemTexture, slotPosition, null, Color.White, 0f, Utils.Size(itemTexture) / 2f, ITDX(itemTexture), SpriteEffects.None, 0f);

                    // Check mouse interaction
                    if (Vector2.Distance(slotPosition, Main.MouseScreen) <= SlotSize / 2f)
                    {
                        NeavaAGBFPlayer.CSH = true;
                        if (!IsClicking && PlayerInput.Triggers.Current.MouseLeft && (Main.mouseItem.type != ItemID.None || NeavaAGBFPlayer.WeaponGrid[i].type != ItemID.None))
                        {
                            SoundEngine.PlaySound(SoundID.Grab, null, null);

                            Item temp = Main.mouseItem;
                            Item mouseItem = NeavaAGBFPlayer.WeaponGrid[i];
                            Main.mouseItem = NeavaAGBFPlayer.WeaponGrid[i];

                            NeavaAGBFPlayer.WeaponGrid[i] = temp;
                            Main.mouseItem = mouseItem;

                            Main.NewText($"WeaponGrid[{i}] = {NeavaAGBFPlayer.WeaponGrid[i]?.Name ?? "None"}, Main.mouseItem = {Main.mouseItem?.Name ?? "None"}");
                        }

                        if (NeavaAGBFPlayer.WeaponGrid[i].type != ItemID.None)
                        {
                            Main.HoverItem = NeavaAGBFPlayer.WeaponGrid[i];
                            Main.instance.MouseText(NeavaAGBFPlayer.WeaponGrid[i].Name, NeavaAGBFPlayer.WeaponGrid[i].rare, 0, -1, -1, -1, -1, 0);
                        }

                        //Main.NewText($"WeaponGrid[{i}] = {NeavaAGBFPlayer.WeaponGrid[i]?.Name ?? "None"}");

                    }

                    
                }


            }

        }

        public static void IsCLickingCheck()
        {
            IsClicking = PlayerInput.Triggers.Current.MouseLeft;
        }

    }

}