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

namespace NeavaAGBF.Common.Players
{
    public class NeavaAGBFPlayer : ModPlayer
    {
        public static Item[] WeaponGrid = new Item[9];

        public static Chest Chest = new Chest(true);

        public static int ChestId = -114514;

        // Delete when not needed
        private bool JC_SB;

        private bool CSH;

        public static bool DSJJH = false;

        public static bool DKUI = false;

        public static bool ZZSY = false;

        static NeavaAGBFPlayer()
        {
            for (int i = 0; i < WeaponGrid.Length; i++)
            {
                WeaponGrid[i] = new Item();
            }
        }


        public static float ITDX(Texture2D texture2D)
        {
            float num = 0.85f;
            if ((float)texture2D.Height > 42.5f || (float)texture2D.Width > 42.5f)
            {
                num = ((texture2D.Width <= texture2D.Height) ? (38.25f / (float)texture2D.Height) : (38.25f / (float)texture2D.Width));
            }
            return num;
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
                NeavaAGBFPlayer.ZZSY = true;
                Main.spriteBatch.Draw(GridOpenTexture2, vector2, null, new Color(255, 255, 155), 0f, Utils.Size(GridOpenTexture2) / 2f, 0.9f, 0, 0f);
                Main.instance.MouseText(Language.GetText("Mods.NeavaAGBF.OpenGrid").Value, 0, 0, -1, -1, -1, -1, 0);

            }
            Main.spriteBatch.Draw(GridOpenTexture, vector2, null, new Color(255, 255, 255), 0f, Utils.Size(GridOpenTexture) / 2f, 0.9f, 0, 0f);

            if (flag && !NeavaAGBFPlayer.DSJJH && PlayerInput.Triggers.Current.MouseLeft)
            {
                NeavaAGBFPlayer.ZZSY = true;
                if (!NeavaAGBFPlayer.DKUI)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.MenuClose, null, null);
                }
                NeavaAGBFPlayer.DKUI = !NeavaAGBFPlayer.DKUI;
            }
            if (NeavaAGBFPlayer.DKUI)
            {
                Texture2D UID = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/WeaponSlot", AssetRequestMode.AsyncLoad).Value;

                Color color;
                color = new Color(255, 255, 255, 222);
                Color color2;
                color2 = new Color(0, 0, 0);
                Color color3;
                color3 = new Color(255, 255, 255);

                Vector2 item_0 = new Vector2(580f, 345f) + UI;

                const int SlotCount = 9; // Number of slots
                const int SlotsPerRow = 3; // Slots per row in the grid
                const float SlotSize = 50f; // Size of each slot
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

                    // Check mouse interaction
                    if (Vector2.Distance(slotPosition, Main.MouseScreen) <= SlotSize / 2f)
                    {
                        NeavaAGBFPlayer.ZZSY = true;
                        if (!NeavaAGBFPlayer.DSJJH && PlayerInput.Triggers.Current.MouseLeft && (Main.mouseItem.type != 0 || NeavaAGBFPlayer.WeaponGrid[i].type != 0))
                        {
                            SoundEngine.PlaySound(SoundID.Grab, null, null);
                            Item temp = Main.mouseItem;
                            Main.mouseItem = NeavaAGBFPlayer.WeaponGrid[i];
                            NeavaAGBFPlayer.WeaponGrid[i] = temp;
                        }

                        // Display hover text
                        Main.HoverItem = NeavaAGBFPlayer.WeaponGrid[i];
                        Main.instance.MouseText(NeavaAGBFPlayer.WeaponGrid[i].Name, NeavaAGBFPlayer.WeaponGrid[i].rare, 0, -1, -1, -1, -1, 0);
                    }

                    // Draw the slot background
                    Main.spriteBatch.Draw(UID, slotPosition, null, color, 0f, Utils.Size(UID) / 2f, 0.85f, 0, 0f);

                    // Draw the item in the slot
                    //if (NeavaAGBFPlayer.WeaponGrid[i].type != 0)
                    //{
                    //    Texture2D itemTexture = TextureAssets.Item[NeavaAGBFPlayer.WeaponGrid[i].type].Value;
                    //    Main.spriteBatch.Draw(itemTexture, slotPosition, null, color3, 0f, Utils.Size(itemTexture) / 2f, NeavaAGBFPlayer.ITDX(itemTexture), 0, 0f);
                    //}
                    if (NeavaAGBFPlayer.WeaponGrid[i].type != 0)
                    {
                        Main.HoverItem = NeavaAGBFPlayer.WeaponGrid[i];
                        Main.instance.MouseText(NeavaAGBFPlayer.WeaponGrid[i].Name, NeavaAGBFPlayer.WeaponGrid[i].rare, 0, -1, -1, -1, -1, 0);
                    }
                }


            }

        }

    }

}