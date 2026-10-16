using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x020010A4 RID: 4260
	[Serializable]
	public class SultanMask : IPart
	{
		// Token: 0x0600A994 RID: 43412 RVA: 0x003DFE2C File Offset: 0x003DE02C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A995 RID: 43413 RVA: 0x003DFE2F File Offset: 0x003DE02F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == EquippedEvent.ID;
		}

		// Token: 0x0600A996 RID: 43414 RVA: 0x003DFE50 File Offset: 0x003DE050
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Period <= 5)
			{
				foreach (string text in HistoryAPI.GetLikedFactionsForSultan(this.Period))
				{
					AddsRep.AddModifier(this.ParentObject, text, 200);
				}
				foreach (string text2 in HistoryAPI.GetDislikedFactionsForSultan(this.Period))
				{
					AddsRep.AddModifier(this.ParentObject, text2, -200);
				}
				foreach (string text3 in HistoryAPI.GetDomainsForSultan(this.Period).ShuffleInPlace<string>(null))
				{
					if (RelicGenerator.ApplyElementBestowal(this.ParentObject, text3, "Face", this.ParentObject.GetIntProperty("RelicTemplateTier", 0), "armor"))
					{
						break;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A997 RID: 43415 RVA: 0x003DFF8C File Offset: 0x003DE18C
		public override bool HandleEvent(EquippedEvent E)
		{
			AchievementManager.IncrementStat("STAT_WEAR_FACE_" + this.Period.ToString());
			return base.HandleEvent(E);
		}

		// Token: 0x040045EF RID: 17903
		public int Period = 1;
	}
}
