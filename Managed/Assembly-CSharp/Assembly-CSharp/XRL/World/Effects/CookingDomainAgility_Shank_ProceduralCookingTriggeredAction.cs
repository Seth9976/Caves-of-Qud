using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000C4F RID: 3151
	[Serializable]
	public class CookingDomainAgility_Shank_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008094 RID: 32916 RVA: 0x002F4D0A File Offset: 0x002F2F0A
		public override string GetDescription()
		{
			return "@they shank @their opponent.";
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x002F4D11 File Offset: 0x002F2F11
		public override string GetNotification()
		{
			if (this.didFire)
			{
				return "@they perform@s an act of nimble violence.";
			}
			return null;
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x002F4D22 File Offset: 0x002F2F22
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				ShortBlades_Shank.Cast(go, null, null);
				this.didFire = true;
			}
		}

		// Token: 0x040038FF RID: 14591
		private bool didFire;
	}
}
