using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200116C RID: 4460
	[Serializable]
	public class ModSmart : IModification
	{
		// Token: 0x0600B16C RID: 45420 RVA: 0x004119B5 File Offset: 0x0040FBB5
		public ModSmart()
		{
		}

		// Token: 0x0600B16D RID: 45421 RVA: 0x004119BD File Offset: 0x0040FBBD
		public ModSmart(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B16E RID: 45422 RVA: 0x004119C6 File Offset: 0x0040FBC6
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B16F RID: 45423 RVA: 0x004119CF File Offset: 0x0040FBCF
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MissileWeapon>() && Object.HasPart<ModScoped>();
		}

		// Token: 0x0600B170 RID: 45424 RVA: 0x004119E8 File Offset: 0x0040FBE8
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			Smartgun part = Object.GetPart<Smartgun>();
			int modificationLevel = ModSmart.GetModificationLevel(this.Tier);
			if (part == null)
			{
				Object.AddPart<Smartgun>(new Smartgun
				{
					Level = modificationLevel
				}, true, false);
			}
			else if (part.Level < modificationLevel)
			{
				part.Level = modificationLevel;
			}
			int num = Math.Max((this.Tier >= 5) ? (10 - this.Tier) : (this.Tier + 2), 2);
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
			base.IncreaseDifficultyAndComplexity(5, 2, null);
		}

		// Token: 0x0600B171 RID: 45425 RVA: 0x00411AA9 File Offset: 0x0040FCA9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B172 RID: 45426 RVA: 0x00411AC7 File Offset: 0x0040FCC7
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{c|smart}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B173 RID: 45427 RVA: 0x00411AF1 File Offset: 0x0040FCF1
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModSmart.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B174 RID: 45428 RVA: 0x00411B11 File Offset: 0x0040FD11
		public static int GetModificationLevel(int Tier)
		{
			return (int)Math.Ceiling((double)(((float)Tier + 1f) / 1.8f));
		}

		// Token: 0x0600B175 RID: 45429 RVA: 0x00411B28 File Offset: 0x0040FD28
		public static string GetDescription(int Tier)
		{
			return "Smart: When powered and started up and the wielder has a HUD or techscanner equipped, this weapon's tracking scope makes it more accurate and gives " + ((Tier > 0) ? ModSmart.GetModificationLevel(Tier).Signed(false) : "a bonus") + " to hit a target aimed at.";
		}

		// Token: 0x0600B176 RID: 45430 RVA: 0x00411B50 File Offset: 0x0040FD50
		public string GetPhysicalDescription()
		{
			return "";
		}
	}
}
