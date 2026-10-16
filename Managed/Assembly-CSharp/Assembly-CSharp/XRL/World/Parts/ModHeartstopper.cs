using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001187 RID: 4487
	[Serializable]
	public class ModHeartstopper : IModification
	{
		// Token: 0x0600B262 RID: 45666 RVA: 0x00413EC1 File Offset: 0x004120C1
		public ModHeartstopper()
		{
		}

		// Token: 0x0600B263 RID: 45667 RVA: 0x00413EC9 File Offset: 0x004120C9
		public ModHeartstopper(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B264 RID: 45668 RVA: 0x00413ED2 File Offset: 0x004120D2
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B265 RID: 45669 RVA: 0x00413EDC File Offset: 0x004120DC
		public override bool ModificationApplicable(GameObject Object)
		{
			if (Object.HasPart<CardiacArrestOnHit>())
			{
				return false;
			}
			GameObject gameObject = null;
			string text = null;
			GetMissileWeaponProjectileEvent.GetFor(Object, ref gameObject, ref text);
			if (gameObject != null && gameObject.HasPart<CardiacArrestOnHit>())
			{
				return false;
			}
			if (!text.IsNullOrEmpty())
			{
				GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(text);
				if (blueprintIfExists != null && blueprintIfExists.HasPart("CardiacArrestOnHit"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600B266 RID: 45670 RVA: 0x00413F38 File Offset: 0x00412138
		public override void ApplyModification(GameObject Object)
		{
			CardiacArrestOnHit cardiacArrestOnHit = Object.RequirePart<CardiacArrestOnHit>(false);
			cardiacArrestOnHit.Chance = this.GetBaseChance();
			cardiacArrestOnHit.SaveTarget = this.GetSaveTarget();
			cardiacArrestOnHit.SaveAttribute = this.GetSaveAttribute();
			cardiacArrestOnHit.ChargeUse = 200;
			cardiacArrestOnHit.IsBootSensitive = true;
			cardiacArrestOnHit.IsEMPSensitive = true;
			cardiacArrestOnHit.IsPowerSwitchSensitive = true;
			cardiacArrestOnHit.IsPowerLoadSensitive = true;
			cardiacArrestOnHit.IsTechScannable = true;
			cardiacArrestOnHit.NameForStatus = "CardiacDisruptor";
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexityIfComplex(2, 2, null);
		}

		// Token: 0x0600B267 RID: 45671 RVA: 0x00413FB9 File Offset: 0x004121B9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B268 RID: 45672 RVA: 0x00413FDF File Offset: 0x004121DF
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{lovesickness|heartstopper}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B269 RID: 45673 RVA: 0x00414009 File Offset: 0x00412209
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B26A RID: 45674 RVA: 0x00414024 File Offset: 0x00412224
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("circuitry", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B26B RID: 45675 RVA: 0x00414047 File Offset: 0x00412247
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B26C RID: 45676 RVA: 0x0041404A File Offset: 0x0041224A
		public static string GetDescription(int Tier)
		{
			return "Heartstopper: When powered, this weapon has a chance to put opponents into cardiac arrest.";
		}

		// Token: 0x0600B26D RID: 45677 RVA: 0x00414054 File Offset: 0x00412254
		public string GetInstanceDescription()
		{
			CardiacArrestOnHit part = this.ParentObject.GetPart<CardiacArrestOnHit>();
			if (part == null)
			{
				return ModHeartstopper.GetDescription(this.Tier);
			}
			return string.Concat(new string[]
			{
				"Heartstopper: When powered, this weapon has ",
				Grammar.A(part.GetActivationChance(this.ParentObject.Holder, null, null, null), false),
				"% chance to put opponents into cardiac arrest",
				part.SaveAttribute.IsNullOrEmpty() ? "" : string.Concat(new string[]
				{
					" if they fail a difficulty ",
					part.GetSaveTarget(null).ToString(),
					" ",
					part.SaveAttribute,
					" save"
				}),
				"."
			});
		}

		// Token: 0x0600B26E RID: 45678 RVA: 0x00414121 File Offset: 0x00412321
		public int GetBaseChance()
		{
			return 9 + this.Tier;
		}

		// Token: 0x0600B26F RID: 45679 RVA: 0x0041412C File Offset: 0x0041232C
		public int GetSaveTarget()
		{
			return 18 + this.Tier * 2;
		}

		// Token: 0x0600B270 RID: 45680 RVA: 0x00414139 File Offset: 0x00412339
		public string GetSaveAttribute()
		{
			return "Toughness";
		}
	}
}
