using NeavaAGBF.Common.Items;
using NeavaAGBF.Common.Players;
using NeavaAGBF.Common.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace NeavaAGBF.Content.Items
{
    public class ChargeAttacks
    {
        //private static readonly StatHandler playerMod = Main.LocalPlayer.GetModPlayer<StatHandler>();
        public static readonly Dictionary<string, Action<Player, Item, float>> chargeAttackDict = new()
{
            { "Test", ChargeAttacks.Test },
        };


        public static void Test(Player player, Item item, float multi)
        {
            Main.NewText($"{player.name} used the Basic Sword Charge Attack!", Color.Yellow);


            Vector2 playerCenter = player.Center;
            Vector2 cursorPosition = Main.MouseWorld;

            Vector2 velocity = Vector2.Normalize(cursorPosition - playerCenter) * 10f;

            int projectileType = ModContent.ProjectileType<ProjectilNoCharge>();
            int proj = Projectile.NewProjectile(
                spawnSource: player.GetSource_ItemUse(item),
                position: playerCenter,
                velocity: velocity,
                Type: projectileType,
                Damage: (int)(item.damage * multi),
                KnockBack: 0,
                Owner: player.whoAmI
            );

            if (Main.projectile[proj].TryGetGlobalProjectile(out ChargeControlGlobalProjectile globalProj))
            {
                globalProj.CanGainCharge = false;
                globalProj.IsChargeAttack = true;
            }

            player.AddBuff(BuffID.WellFed, 600);
        }
    }

}
