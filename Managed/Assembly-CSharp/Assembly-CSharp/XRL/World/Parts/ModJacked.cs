using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001149 RID: 4425
	[Serializable]
	public class ModJacked : IModification
	{
		// Token: 0x0600AF9A RID: 44954 RVA: 0x0040B794 File Offset: 0x00409994
		public ModJacked()
		{
		}

		// Token: 0x0600AF9B RID: 44955 RVA: 0x0040B79C File Offset: 0x0040999C
		public ModJacked(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF9C RID: 44956 RVA: 0x0040B7A5 File Offset: 0x004099A5
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AF9D RID: 44957 RVA: 0x0040B7B0 File Offset: 0x004099B0
		public override bool ModificationApplicable(GameObject Object)
		{
			if (Object.HasPart<IntegratedPowerSystems>())
			{
				return false;
			}
			if (!CheckUsesChargeWhileEquippedEvent.Check(Object))
			{
				return false;
			}
			try
			{
				string usesSlots = Object.UsesSlots;
				if (!string.IsNullOrEmpty(usesSlots))
				{
					bool flag = false;
					foreach (string text in usesSlots.CachedCommaExpansion())
					{
						bool? flag2 = Anatomies.GetBodyPartTypeOrFail(text).Contact;
						bool flag3 = false;
						if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				else
				{
					Armor part = Object.GetPart<Armor>();
					if (part != null && part.WornOn != "*")
					{
						bool? flag2 = Anatomies.GetBodyPartTypeOrFail(part.WornOn).Contact;
						bool flag4 = false;
						if ((flag2.GetValueOrDefault() == flag4) & (flag2 != null))
						{
							return false;
						}
					}
					Shield part2 = Object.GetPart<Shield>();
					if (part2 != null && part2.WornOn != "*")
					{
						bool? flag2 = Anatomies.GetBodyPartTypeOrFail(part2.WornOn).Contact;
						bool flag4 = false;
						if ((flag2.GetValueOrDefault() == flag4) & (flag2 != null))
						{
							return false;
						}
					}
					MissileWeapon part3 = Object.GetPart<MissileWeapon>();
					if (part3 != null && part3.SlotType != "*")
					{
						bool? flag2 = Anatomies.GetBodyPartTypeOrFail(part3.SlotType).Contact;
						bool flag4 = false;
						if ((flag2.GetValueOrDefault() == flag4) & (flag2 != null))
						{
							return false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("ModJacked contact check", ex, "game_exception");
				return false;
			}
			return true;
		}

		// Token: 0x0600AF9E RID: 44958 RVA: 0x0040B988 File Offset: 0x00409B88
		public override void ApplyModification(GameObject Object)
		{
			if (!Object.HasPart<IntegratedPowerSystems>())
			{
				Object.AddPart<IntegratedPowerSystems>(new IntegratedPowerSystems
				{
					RequiresEvent = "HasPowerConnectors"
				}, true, false);
			}
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600AF9F RID: 44959 RVA: 0x0040B9C0 File Offset: 0x00409BC0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AFA0 RID: 44960 RVA: 0x0040B9DE File Offset: 0x00409BDE
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{c|jacked}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFA1 RID: 44961 RVA: 0x0040BA08 File Offset: 0x00409C08
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModJacked.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFA2 RID: 44962 RVA: 0x0040BA3A File Offset: 0x00409C3A
		public static string GetDescription(int Tier)
		{
			return "Jacked: When equipped by a robot, cyborg, or mutant with the ability to generate electricity or access to grid power, this item can draw power.";
		}

		// Token: 0x0600AFA3 RID: 44963 RVA: 0x0040BA41 File Offset: 0x00409C41
		public string GetPhysicalDescription()
		{
			return "";
		}
	}
}
