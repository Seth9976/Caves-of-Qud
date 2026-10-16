using System;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001160 RID: 4448
	[Serializable]
	public class ModRecycling : IModification
	{
		// Token: 0x0600B0DA RID: 45274 RVA: 0x00410329 File Offset: 0x0040E529
		public ModRecycling()
		{
		}

		// Token: 0x0600B0DB RID: 45275 RVA: 0x00410331 File Offset: 0x0040E531
		public ModRecycling(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0DC RID: 45276 RVA: 0x0041033A File Offset: 0x0040E53A
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B0DD RID: 45277 RVA: 0x00410344 File Offset: 0x0040E544
		public override bool ModificationApplicable(GameObject Object)
		{
			return !Object.HasPart<LiquidProducer>() && IModification.CheckWornSlot(Object, "Body", "Back", null, null, true, true, true, false);
		}

		// Token: 0x0600B0DE RID: 45278 RVA: 0x00410374 File Offset: 0x0040E574
		public override void ApplyModification(GameObject Object)
		{
			if (!Object.HasPart<LiquidVolume>())
			{
				LiquidVolume liquidVolume = new LiquidVolume();
				Object.AddPart<LiquidVolume>(liquidVolume, true, false);
				liquidVolume.MaxVolume = 8;
				liquidVolume.Volume = Stat.Random(0, liquidVolume.MaxVolume);
				liquidVolume.ComponentLiquids.Clear();
				if (liquidVolume.Volume > 0)
				{
					liquidVolume.ComponentLiquids.Add("water", 1000);
				}
				liquidVolume.Update();
			}
			if (!Object.HasPart<LiquidProducer>())
			{
				Object.AddPart<LiquidProducer>(new LiquidProducer
				{
					Liquid = "water",
					Rate = 10000,
					WorksOnSelf = false,
					WorksOnEquipper = true
				}, true, false);
			}
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B0DF RID: 45279 RVA: 0x00410426 File Offset: 0x0040E626
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B0E0 RID: 45280 RVA: 0x0041044C File Offset: 0x0040E64C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{B|recycling}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0E1 RID: 45281 RVA: 0x00410478 File Offset: 0x0040E678
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append("\n{{rules|Recycling: This item collects");
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null)
			{
				E.Postfix.Append(", purifies, and stores up to ").Append(Grammar.Cardinal(liquidVolume.MaxVolume)).Append(' ')
					.Append((liquidVolume.MaxVolume == 1) ? "dram" : "drams")
					.Append(" of the wearer's wastewater.");
			}
			else
			{
				E.Postfix.Append(" and purifies the wearer's wastewater.");
			}
			base.AddStatusSummary(E.Postfix);
			E.Postfix.Append("}}");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0E2 RID: 45282 RVA: 0x00410528 File Offset: 0x0040E728
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 1);
				E.Add("salt", 3);
			}
			return base.HandleEvent(E);
		}
	}
}
