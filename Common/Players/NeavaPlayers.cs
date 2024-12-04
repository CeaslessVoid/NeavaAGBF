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
using NeavaAGBF.WeaponSkills;
using NeavaAGBF.Common.UI;

namespace NeavaAGBF.Common.Players
{
    public class NeavaAGBFPlayer : ModPlayer
    {
        public Item[] WeaponGrid = new Item[9];

        private static bool _isMouseOverSlot = false;
        private static bool _isWeaponGridInitialized = false;

        public static bool IsWeaponGridOpen = false;
        public static bool IsClicking = false;

        public static float ScaleToFit(Texture2D texture)
        {
            const float maxSize = 42.5f; // Maximum size for scaling
            return Math.Min(maxSize / texture.Width, maxSize / texture.Height);
        }

        public override void ResetEffects()
        {
            if (!_isWeaponGridInitialized)
            {
                _isWeaponGridInitialized = true;
                for (int i = 0; i < WeaponGrid.Length; i++)
                {
                    if (WeaponGrid[i] == null)
                        WeaponGrid[i] = new Item(0,1,0);
                }
            }

            if (_isMouseOverSlot)
            {
                if (!PlayerInput.Triggers.Current.MouseLeft)
                {
                    _isMouseOverSlot = false;
                }
                Player.delayUseItem = true;
                Player.controlUseItem = false;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            var gridData = new List<TagCompound>();

            foreach (var item in WeaponGrid)
            {
                if (item != null && !item.IsAir)
                {
                    gridData.Add(new TagCompound
                    {
                        ["type"] = item.type,
                        ["stack"] = item.stack
                    });
                }
                else
                {
                    gridData.Add(new TagCompound
                    {
                        ["type"] = 0,
                        ["stack"] = 0
                    });
                }
            }

            tag["WeaponGrid"] = gridData;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("WeaponGrid"))
            {
                var gridData = tag.Get<List<TagCompound>>("WeaponGrid");
                for (int i = 0; i < WeaponGrid.Length; i++)
                {
                    if (gridData[i] != null && gridData[i].Get<int>("type") > 0)
                    {
                        WeaponGrid[i] = new Item();
                        WeaponGrid[i].SetDefaults(gridData[i].Get<int>("type"));
                        WeaponGrid[i].stack = gridData[i].Get<int>("stack");
                    }
                    else
                    {
                        WeaponGrid[i] = new Item();
                    }
                }
            }
        }


        public static void DrawWeaponGrid()
        {
            Vector2 uiOffset = Vector2.Zero;

            Texture2D gridClosedTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/UI_1", AssetRequestMode.AsyncLoad).Value;
            Texture2D gridOpenTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/UI_2", AssetRequestMode.AsyncLoad).Value;
            Player player = Main.LocalPlayer;

            Vector2 toggleButtonPosition = new Vector2(586f, 295f) + uiOffset;
            bool isMouseOverButton = Vector2.Distance(toggleButtonPosition, Main.MouseScreen) <= 19f;

            Main.spriteBatch.Draw(
                IsWeaponGridOpen ? gridOpenTexture : gridClosedTexture,
                toggleButtonPosition,
                null,
                IsWeaponGridOpen ? new Color(255, 255, 155) : new Color(255, 255, 255),
                0f,
                Utils.Size(gridClosedTexture) / 2f,
                0.9f,
                SpriteEffects.None,
                0f
            );

            if (isMouseOverButton)
            {
                Main.instance.MouseText(Language.GetText("Mods.NeavaAGBF.SimpleText.OpenGrid").Value);
                _isMouseOverSlot = true;

                if (!IsClicking && PlayerInput.Triggers.Current.MouseLeft)
                {
                    IsWeaponGridOpen = !IsWeaponGridOpen;
                    SoundEngine.PlaySound(IsWeaponGridOpen ? SoundID.MenuOpen : SoundID.MenuClose);
                    Main.mouseLeftRelease = false;

                    ModContent.GetInstance<WeaponGridUISystem>().ToggleGrid();

                }
            }

            if (IsWeaponGridOpen)
            {
                DrawWeaponSlots(uiOffset);
            }
        }

        private static void DrawWeaponSlots(Vector2 uiOffset)
        {
            Texture2D slotBackgroundTexture = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/WeaponSlot", AssetRequestMode.AsyncLoad).Value;

            NeavaAGBFPlayer playerMod = Main.LocalPlayer.GetModPlayer<NeavaAGBFPlayer>();

            WeaponGridUIState uiState = ModContent.GetInstance<WeaponGridUISystem>().gridUIState;
            Vector2 panelPosition = new Vector2(uiState.weaponGridPanel.Left.Pixels, uiState.weaponGridPanel.Top.Pixels);   

            const int slotCount = 9;
            const int slotsPerRow = 3;
            const float slotSize = 52f;
            const float slotSpacing = 60f;
            Vector2 startingPosition = panelPosition + new Vector2(40f, 40f);

            Color slotColor = new Color(255, 255, 255, 222);

            for (int i = 0; i < slotCount; i++)
            {
                int row = i / slotsPerRow;
                int column = i % slotsPerRow;
                Vector2 slotPosition = startingPosition + uiOffset + new Vector2(column * slotSpacing, row * slotSpacing);

                Main.spriteBatch.Draw(slotBackgroundTexture, slotPosition, null, slotColor, 0f, Utils.Size(slotBackgroundTexture) / 2f, 0.85f, SpriteEffects.None, 0f);

                Texture2D itemTexture = TextureAssets.Item[playerMod.WeaponGrid[i].type].Value;
                Main.spriteBatch.Draw(itemTexture, slotPosition, null, Color.White, 0f, Utils.Size(itemTexture) / 2f, ScaleToFit(itemTexture), SpriteEffects.None, 0f);

                if (Vector2.Distance(slotPosition, Main.MouseScreen) <= slotSize / 2f)
                {
                    _isMouseOverSlot = true;

                    if (!IsClicking && PlayerInput.Triggers.Current.MouseLeft && (Main.mouseItem.type != ItemID.None || playerMod.WeaponGrid[i].type != ItemID.None))
                    {
                        SoundEngine.PlaySound(SoundID.Grab);

                        Item temp = playerMod.WeaponGrid[i];


                        playerMod.WeaponGrid[i] = Main.mouseItem;
                        Main.mouseItem = temp;

                        //Main.NewText($"WeaponGrid[{i}] = {WeaponGrid[i]?.Name ?? "None"}, Main.mouseItem = {Main.mouseItem?.Name ?? "None"}");
                    }

                    if (playerMod.WeaponGrid[i].type != ItemID.None)
                    {
                        Main.HoverItem = playerMod.WeaponGrid[i];
                        Main.instance.MouseText(playerMod.WeaponGrid[i].Name, playerMod.WeaponGrid[i].rare);
                    }
                }
            }
        }

        public static void UpdateIsClicking()
        {
            IsClicking = PlayerInput.Triggers.Current.MouseLeft;
        }

        public override void PostUpdateEquips()
        {
            base.PostUpdate();

            WeaponGridHandler weaponGridHandler = new WeaponGridHandler();
            weaponGridHandler.ApplyWeaponGridEffects(Player);
        }
    }

}