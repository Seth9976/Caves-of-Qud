using System;
using XRL.Language;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001157 RID: 4439
	[Serializable]
	public class ModPadded : IModification
	{
		// Token: 0x0600B05C RID: 45148 RVA: 0x0040ECFC File Offset: 0x0040CEFC
		public ModPadded()
		{
		}

		// Token: 0x0600B05D RID: 45149 RVA: 0x0040ED04 File Offset: 0x0040CF04
		public ModPadded(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B05E RID: 45150 RVA: 0x0040ED0D File Offset: 0x0040CF0D
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Padding";
		}

		// Token: 0x0600B05F RID: 45151 RVA: 0x0040ED24 File Offset: 0x0040CF24
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", null, null, null, true, true, true, false);
		}

		// Token: 0x0600B060 RID: 45152 RVA: 0x0040ED43 File Offset: 0x0040CF43
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B061 RID: 45153 RVA: 0x0040ED4D File Offset: 0x0040CF4D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B062 RID: 45154 RVA: 0x0040ED83 File Offset: 0x0040CF83
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("padded", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B063 RID: 45155 RVA: 0x0040EDAD File Offset: 0x0040CFAD
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageIncrease += 50;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B064 RID: 45156 RVA: 0x0040EDC5 File Offset: 0x0040CFC5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModPadded.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B065 RID: 45157 RVA: 0x0040EDE5 File Offset: 0x0040CFE5
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "ApplyDazed");
			E.Actor.RegisterPartEvent(this, "ApplyStun");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B066 RID: 45158 RVA: 0x0040EE10 File Offset: 0x0040D010
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "ApplyDazed");
			E.Actor.UnregisterPartEvent(this, "ApplyStun");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B067 RID: 45159 RVA: 0x0040EE3B File Offset: 0x0040D03B
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B068 RID: 45160 RVA: 0x0040EE3E File Offset: 0x0040D03E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GetShakeItOffChance");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B069 RID: 45161 RVA: 0x0040EE54 File Offset: 0x0040D054
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyDazed" || E.ID == "ApplyStun")
			{
				if (this.GetProtectionChance().in100())
				{
					GameObject equipped = this.ParentObject.Equipped;
					if (equipped != null && IComponent<GameObject>.Visible(equipped))
					{
						if (!equipped.IsPlayer())
						{
							GameObject gameObject = equipped;
							GameObject thePlayer = IComponent<GameObject>.ThePlayer;
							if (gameObject != ((thePlayer != null) ? thePlayer.Target : null))
							{
								goto IL_011D;
							}
						}
						char c = ColorCoding.ConsequentialColorChar(equipped, null);
						if (E.ID == "ApplyStun")
						{
							if (equipped != null)
							{
								equipped.ParticleText("Resisted stun!", c, false, 1.5f, -8f);
							}
						}
						else if (E.ID == "ApplyDazed")
						{
							equipped.ParticleText("Resisted daze!", c, false, 1.5f, -8f);
						}
						IComponent<GameObject>.AddPlayerMessage(equipped.Poss("padding", true) + " softened the blow.", c, true);
					}
				}
			}
			else if (E.ID == "GetShakeItOffChance")
			{
				E.SetParameter("Chance", E.GetIntParameter("Chance", 0) * 2);
			}
			IL_011D:
			return base.FireEvent(E);
		}

		// Token: 0x0600B06A RID: 45162 RVA: 0x0040EF85 File Offset: 0x0040D185
		public static int GetProtectionChance(int Tier)
		{
			return 10;
		}

		// Token: 0x0600B06B RID: 45163 RVA: 0x0040EF89 File Offset: 0x0040D189
		public int GetProtectionChance()
		{
			return ModPadded.GetProtectionChance(this.Tier);
		}

		// Token: 0x0600B06C RID: 45164 RVA: 0x0040EF98 File Offset: 0x0040D198
		public static string GetDescription(int Tier)
		{
			return string.Concat(new string[]
			{
				"Padded: This item grants ",
				Grammar.AOrAnBeforeNumber(ModPadded.GetProtectionChance(Tier)),
				" ",
				ModPadded.GetProtectionChance(Tier).ToString(),
				"% chance to prevent daze or stun effects, and it doubles the chance for the wearer to shake off daze and stun effects with the Shake It Off skill power."
			});
		}
	}
}
