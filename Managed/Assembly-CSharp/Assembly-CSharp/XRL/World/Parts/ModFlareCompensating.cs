using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001186 RID: 4486
	[Serializable]
	public class ModFlareCompensating : IModification
	{
		// Token: 0x0600B258 RID: 45656 RVA: 0x00413C9F File Offset: 0x00411E9F
		public ModFlareCompensating()
		{
		}

		// Token: 0x0600B259 RID: 45657 RVA: 0x00413CA7 File Offset: 0x00411EA7
		public ModFlareCompensating(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B25A RID: 45658 RVA: 0x00413CB0 File Offset: 0x00411EB0
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B25B RID: 45659 RVA: 0x00413CBC File Offset: 0x00411EBC
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", "Face", null, null, true, true, false, false);
		}

		// Token: 0x0600B25C RID: 45660 RVA: 0x00413CE0 File Offset: 0x00411EE0
		public override void ApplyModification(GameObject Object)
		{
			FlareCompensation part = Object.GetPart<FlareCompensation>();
			int modificationLevel = ModFlareCompensating.GetModificationLevel(this.Tier);
			if (part == null)
			{
				Object.AddPart<FlareCompensation>(new FlareCompensation
				{
					Level = modificationLevel,
					ChargeUse = 1,
					IsEMPSensitive = true,
					ShowInShortDescription = false,
					ComputePowerFactor = 1.5f
				}, true, false);
			}
			else if (part.IsEMPSensitive)
			{
				part.Level += modificationLevel;
				if (part.ChargeUse < 1)
				{
					part.ChargeUse = 1;
				}
				if ((double)part.ComputePowerFactor < 1.5)
				{
					part.ComputePowerFactor = 1.5f;
				}
			}
			else
			{
				part.LevelHardened += part.Level;
				part.Level = modificationLevel;
				if (part.ChargeUse < 1)
				{
					part.ChargeUse = 1;
				}
				if ((double)part.ComputePowerFactor < 1.5)
				{
					part.ComputePowerFactor = 1.5f;
				}
				part.IsEMPSensitive = true;
			}
			int num = Math.Max((this.Tier >= 5) ? (5 - this.Tier / 2) : this.Tier, 2);
			BootSequence part2 = Object.GetPart<BootSequence>();
			if (part2 == null)
			{
				Object.AddPart<BootSequence>(new BootSequence
				{
					BootTime = num,
					ReadoutInName = true,
					ReadoutInDescription = true
				}, true, false);
			}
			else if (part2.BootTime < num)
			{
				part2.BootTime = num;
			}
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(2, 1, null);
		}

		// Token: 0x0600B25D RID: 45661 RVA: 0x00413E46 File Offset: 0x00412046
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B25E RID: 45662 RVA: 0x00413E64 File Offset: 0x00412064
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{K|flare-compensating}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B25F RID: 45663 RVA: 0x00413E8E File Offset: 0x0041208E
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModFlareCompensating.GetDescription(this.Tier), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B260 RID: 45664 RVA: 0x00413EB5 File Offset: 0x004120B5
		public static int GetModificationLevel(int Tier)
		{
			return Tier * 2;
		}

		// Token: 0x0600B261 RID: 45665 RVA: 0x00413EBA File Offset: 0x004120BA
		public static string GetDescription(int Tier)
		{
			return "Flare-compensating: When powered and started up, the item offers protection against visual flash effects.";
		}
	}
}
