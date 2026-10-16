using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000CB8 RID: 3256
	[Serializable]
	public class CookingDomainFungus_FungusReputationUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x06008218 RID: 33304 RVA: 0x002F730E File Offset: 0x002F550E
		public override string GetDescription()
		{
			return "+300 reputation with fungi";
		}

		// Token: 0x06008219 RID: 33305 RVA: 0x002F7315 File Offset: 0x002F5515
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.Applied = false;
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x002F7325 File Offset: 0x002F5525
		public override void Apply(GameObject Object, Effect parent)
		{
			if (Object.IsPlayer())
			{
				XRLCore.Core.Game.PlayerReputation.Modify("Fungi", 300, "Cooking", null, null, true, true);
				this.Applied = true;
			}
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x002F735E File Offset: 0x002F555E
		public override void Remove(GameObject Object, Effect parent)
		{
			if (this.Applied)
			{
				XRLCore.Core.Game.PlayerReputation.Modify("Fungi", -300, "Cooking", null, null, true, true);
			}
		}

		// Token: 0x0400392B RID: 14635
		public int Tier;

		// Token: 0x0400392C RID: 14636
		private bool Applied;
	}
}
