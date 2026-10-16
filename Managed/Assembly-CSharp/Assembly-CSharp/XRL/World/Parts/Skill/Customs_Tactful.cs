using System;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001339 RID: 4921
	[Serializable]
	public class Customs_Tactful : BaseSkill
	{
		// Token: 0x0600C03C RID: 49212 RVA: 0x00467334 File Offset: 0x00465534
		public static int GetBonusFor(GameObject Object)
		{
			GivesRep givesRep;
			if (Object != null && Object.TryGetPart<GivesRep>(out givesRep))
			{
				return Mathf.RoundToInt((float)givesRep.repValue / 20f) * 5;
			}
			return RuleSettings.REPUTATION_BASE_UNIT / 2;
		}

		// Token: 0x0600C03D RID: 49213 RVA: 0x0046736A File Offset: 0x0046556A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetRitualSifrahSetupEvent>.ID || ID == PooledEvent<GetSocialSifrahSetupEvent>.ID || ID == PooledEvent<GetWaterRitualReputationAmountEvent>.ID || ID == PooledEvent<WaterRitualStartEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C03E RID: 49214 RVA: 0x004673A0 File Offset: 0x004655A0
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C03F RID: 49215 RVA: 0x004673C4 File Offset: 0x004655C4
		public override bool HandleEvent(WaterRitualStartEvent E)
		{
			if (E.Actor == this.ParentObject && !E.Record.Has("usedTactful") && E.Actor.IsPlayer())
			{
				E.Record.attributes.Add("usedTactful");
				The.Game.PlayerReputation.Modify(E.Record.faction, Customs_Tactful.GetBonusFor(E.SpeakingWith), "WaterRitualTactfulAward", null, null, false, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C040 RID: 49216 RVA: 0x0046744C File Offset: 0x0046564C
		public override bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			if (E.Actor == this.ParentObject && !E.Record.Has("usedTactful") && E.Actor.IsPlayer() && E.Faction == E.Record.faction)
			{
				E.Record.attributes.Add("usedTactful");
				E.Amount += Customs_Tactful.GetBonusFor(E.SpeakingWith);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C041 RID: 49217 RVA: 0x004674D2 File Offset: 0x004656D2
		public override bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			if (E.Type == "FormalWaterRitual")
			{
				E.Rating++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C042 RID: 49218 RVA: 0x004674FB File Offset: 0x004656FB
		public override bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			E.Rating++;
			return base.HandleEvent(E);
		}
	}
}
