using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace NeavaAGBF.Common.Items
{
    public class ChargeControlGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool CanGainCharge { get; set; } = true;

        public bool IsChargeAttack { get; set; } = false;

        //public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        //{

        //}
    }
}
