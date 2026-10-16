using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200116E RID: 4462
	[Serializable]
	public class ModSpiked : IModification
	{
		// Token: 0x0600B182 RID: 45442 RVA: 0x00411CAB File Offset: 0x0040FEAB
		public ModSpiked()
		{
		}

		// Token: 0x0600B183 RID: 45443 RVA: 0x00411CB3 File Offset: 0x0040FEB3
		public ModSpiked(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B184 RID: 45444 RVA: 0x00411CBC File Offset: 0x0040FEBC
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "PenetrationModule";
		}

		// Token: 0x0600B185 RID: 45445 RVA: 0x00411CD0 File Offset: 0x0040FED0
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<Shield>() || (IModification.CheckWornSlot(Object, "Hands", null, null, null, true, true, true, false) && !Object.HasPart<BleedingOnHit>());
		}

		// Token: 0x0600B186 RID: 45446 RVA: 0x00411D08 File Offset: 0x0040FF08
		public override void ApplyModification(GameObject Object)
		{
			if (!Object.HasPart<Shield>())
			{
				Object.AddPart<BleedingOnHit>(new BleedingOnHit
				{
					Amount = "1d3",
					SaveTarget = 20 + this.Tier * 2,
					WorksOnEquipper = true,
					RequireDamageAttribute = "Unarmed",
					Stack = true
				}, true, false);
			}
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B187 RID: 45447 RVA: 0x00411D6A File Offset: 0x0040FF6A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShieldSlamDamageEvent.ID || ID == GetShortDescriptionEvent.ID || ID == ShieldSlamPerformedEvent.ID;
		}

		// Token: 0x0600B188 RID: 45448 RVA: 0x00411D98 File Offset: 0x0040FF98
		public override bool HandleEvent(GetShieldSlamDamageEvent E)
		{
			if (E.Shield == this.ParentObject)
			{
				E.Damage += E.StrengthMod;
				if (E.Attributes == "Bludgeoning")
				{
					E.Attributes = "Stabbing";
				}
				else if (E.Attributes.HasDelimitedSubstring(' ', "Bludgeoning", StringComparison.Ordinal))
				{
					string[] array = E.Attributes.Split(' ', StringSplitOptions.None);
					array[Array.IndexOf<string>(array, "Bludgeoning")] = "Stabbing";
					E.Attributes = string.Join(' ', array);
				}
				else
				{
					E.Attributes = E.Attributes.AddDelimitedSubstring(' ', "Stabbing", StringComparison.Ordinal);
				}
				if (E.ExtraDesc != null)
				{
					E.ExtraDesc += "+bleeding";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B189 RID: 45449 RVA: 0x00411E6C File Offset: 0x0041006C
		public override bool HandleEvent(ShieldSlamPerformedEvent E)
		{
			if (E.Shield == this.ParentObject && GameObject.Validate(E.Target) && !E.Target.IsNowhere())
			{
				E.Target.ApplyEffect(new Bleeding("1d2", 20 + E.ShieldAV, this.ParentObject, true, false, false, false), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B18A RID: 45450 RVA: 0x00411ED2 File Offset: 0x004100D2
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{spiked|spiked}}", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B18B RID: 45451 RVA: 0x00411EFC File Offset: 0x004100FC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B18C RID: 45452 RVA: 0x00411F17 File Offset: 0x00410117
		public static string GetDescription(int Tier)
		{
			return "Spiked: This item causes additional bleeding damage.";
		}

		// Token: 0x0600B18D RID: 45453 RVA: 0x00411F1E File Offset: 0x0041011E
		public static string GetDescription(int Tier, GameObject obj)
		{
			if (obj.HasPart<Shield>())
			{
				return "Spiked: This item adds bonus damage to Shield Slam equal to your Strength modifier and causes your target to bleed.";
			}
			return "Spiked: Unarmed attacks performed while this item is equipped cause bleeding.";
		}

		// Token: 0x0600B18E RID: 45454 RVA: 0x00411F33 File Offset: 0x00410133
		public string GetInstanceDescription()
		{
			return ModSpiked.GetDescription(this.Tier, this.ParentObject);
		}
	}
}
