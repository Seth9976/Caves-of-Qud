using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the chance of taking effect is increased by
	/// the standard power load bonus, i.e. 2 for the standard overload power
	/// load of 400.
	/// </remarks>
	// Token: 0x02001153 RID: 4435
	[Serializable]
	public class ModNanon : IModification
	{
		// Token: 0x0600B027 RID: 45095 RVA: 0x0040E033 File Offset: 0x0040C233
		public ModNanon()
		{
		}

		// Token: 0x0600B028 RID: 45096 RVA: 0x0040E03B File Offset: 0x0040C23B
		public ModNanon(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B029 RID: 45097 RVA: 0x0040E044 File Offset: 0x0040C244
		public override void Configure()
		{
			this.ChargeUse = 100;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.WorksOnSelf = true;
			this.NameForStatus = "Nanon";
		}

		// Token: 0x0600B02A RID: 45098 RVA: 0x0040E070 File Offset: 0x0040C270
		public override bool ModificationApplicable(GameObject Object)
		{
			if (!Object.HasPart<MissileWeapon>())
			{
				return false;
			}
			EnergyAmmoLoader part = Object.GetPart<EnergyAmmoLoader>();
			return part != null && part.ChargeUse > 0;
		}

		// Token: 0x0600B02B RID: 45099 RVA: 0x0040E0A0 File Offset: 0x0040C2A0
		public override void ApplyModification(GameObject Object)
		{
			this.Chance = Stat.Random(ModNanon.GetLowChance(this.Tier), ModNanon.GetHighChance(this.Tier));
			EnergyAmmoLoader part = Object.GetPart<EnergyAmmoLoader>();
			if (part != null)
			{
				this.ChargeUse = part.ChargeUse / 5;
			}
			base.IncreaseDifficultyAndComplexity(2, 2, null);
		}

		// Token: 0x0600B02C RID: 45100 RVA: 0x0040E0EF File Offset: 0x0040C2EF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B02D RID: 45101 RVA: 0x0040E10D File Offset: 0x0040C30D
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{K|nanon}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B02E RID: 45102 RVA: 0x0040E137 File Offset: 0x0040C337
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B02F RID: 45103 RVA: 0x0040E152 File Offset: 0x0040C352
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B030 RID: 45104 RVA: 0x0040E155 File Offset: 0x0040C355
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LauncherProjectileHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B031 RID: 45105 RVA: 0x0040E16C File Offset: 0x0040C36C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LauncherProjectileHit")
			{
				int num = this.MyPowerLoadLevel();
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3) && E.GetIntParameter("Penetrations", 0) > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
					if (GameObject.Validate(ref gameObjectParameter2) && !gameObjectParameter2.IsNowhere() && GameObject.Validate(ref gameObjectParameter) && !gameObjectParameter.IsNowhere())
					{
						GameObject gameObject = gameObjectParameter;
						GameObject parentObject = this.ParentObject;
						string text = "Modification ModNanon Dismember";
						GameObject gameObject2 = gameObjectParameter2;
						GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
						if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance + IComponent<GameObject>.PowerLoadBonus(num, 100, 150), gameObject2, gameObjectParameter3, true, false).in100())
						{
							bool flag11 = 1.in1000();
							Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter2, null, null, this.ParentObject, null, "sfx_characterTrigger_dismember", flag11, !flag11, true, false);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B032 RID: 45106 RVA: 0x0040E283 File Offset: 0x0040C483
		public static int GetLowChance(int Tier)
		{
			return Tier;
		}

		// Token: 0x0600B033 RID: 45107 RVA: 0x0040E286 File Offset: 0x0040C486
		public static int GetHighChance(int Tier)
		{
			return Tier + 1;
		}

		// Token: 0x0600B034 RID: 45108 RVA: 0x0040E28C File Offset: 0x0040C48C
		public static string GetDescription(int Tier)
		{
			if (Tier == 0)
			{
				return "Nanon: This weapon has a chance to dismember on penetration.";
			}
			int lowChance = ModNanon.GetLowChance(Tier);
			int highChance = ModNanon.GetHighChance(Tier);
			if (lowChance == highChance)
			{
				return "Nanon: " + highChance.ToString() + "% chance to dismember on penetration";
			}
			return string.Concat(new string[]
			{
				"Nanon: ",
				lowChance.ToString(),
				"-",
				highChance.ToString(),
				"% chance to dismember on penetration"
			});
		}

		// Token: 0x0600B035 RID: 45109 RVA: 0x0040E304 File Offset: 0x0040C504
		public string GetInstanceDescription()
		{
			return "Nanon: " + GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModNanon Dismember", this.Chance + this.MyPowerLoadBonus(int.MinValue, 100, 150), null, null, true, false).ToString() + "% chance to dismember upon penetration.";
		}

		// Token: 0x04004817 RID: 18455
		public int Chance;
	}
}
