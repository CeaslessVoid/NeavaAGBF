using NeavaAGBF.Content.Items.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria;

namespace NeavaAGBF.Common.Npc
{
    public class SkillGemGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!npc.friendly && npc.lifeMax > 5 && !npc.boss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new SkillGemDropCondition(), ModContent.ItemType<SkillGem>(), 1, 1, 2));
            }
        }
    }

    public class SkillGemDropCondition : IItemDropRuleCondition
    {

        public bool CanDrop(DropAttemptInfo info)
        {
            if (info.npc.friendly || info.npc.lifeMax <= 5 || info.npc.boss)
            {
                return false;
            }

            if (NPC.downedPlantBoss)
            {
                return Main.rand.NextBool(5);
            }
            else if (Main.hardMode)
            {
                return Main.rand.NextBool(25);
            }
            else
            {
                return Main.rand.NextBool(50);
            }
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops Skill Gems";
        }
    }
}
