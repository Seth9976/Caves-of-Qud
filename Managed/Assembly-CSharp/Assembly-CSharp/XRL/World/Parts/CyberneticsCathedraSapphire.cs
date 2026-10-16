using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F21 RID: 3873
	[Serializable]
	public class CyberneticsCathedraSapphire : CyberneticsCathedra
	{
		// Token: 0x06009A7D RID: 39549 RVA: 0x00380E58 File Offset: 0x0037F058
		public override void OnImplanted(GameObject Object)
		{
			base.OnImplanted(Object);
			this.ActivatedAbilityID = Object.AddActivatedAbility("Stunning Force", "CommandActivateCathedra", "Cybernetics", null, "#", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, "CommandCathedraSapphire", null, null, null, null);
		}

		// Token: 0x06009A7E RID: 39550 RVA: 0x00380EA4 File Offset: 0x0037F0A4
		public override void CollectStats(Templates.StatCollector stats)
		{
			base.CollectStats(stats);
			StunningForce.CollectProxyStats(stats, base.GetLevel(null));
		}

		// Token: 0x06009A7F RID: 39551 RVA: 0x00380EBA File Offset: 0x0037F0BA
		public override void Activate(GameObject Actor)
		{
			StunningForce.Concussion(Actor.CurrentCell, Actor, base.GetLevel(Actor), 3, Actor.GetPhase(), null, true, true);
			base.Activate(Actor);
		}
	}
}
