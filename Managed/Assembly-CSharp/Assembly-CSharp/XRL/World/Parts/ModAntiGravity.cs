using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001183 RID: 4483
	[Serializable]
	public class ModAntiGravity : IModification
	{
		// Token: 0x0600B236 RID: 45622 RVA: 0x00413710 File Offset: 0x00411910
		public ModAntiGravity()
		{
		}

		// Token: 0x0600B237 RID: 45623 RVA: 0x00413718 File Offset: 0x00411918
		public ModAntiGravity(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B238 RID: 45624 RVA: 0x00413721 File Offset: 0x00411921
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.GetWeight() > 0.0;
		}

		// Token: 0x0600B239 RID: 45625 RVA: 0x00413738 File Offset: 0x00411938
		public override void ApplyModification(GameObject Object)
		{
			Suspensor suspensor = Object.GetPart<Suspensor>();
			int forceLevel = ModAntiGravity.GetForceLevel(this.Tier);
			int percentageForceLevel = ModAntiGravity.GetPercentageForceLevel(this.Tier);
			if (suspensor == null)
			{
				suspensor = new Suspensor();
				suspensor.Force = forceLevel;
				suspensor.PercentageForce = percentageForceLevel;
				Object.AddPart<Suspensor>(suspensor, true, false);
			}
			else
			{
				suspensor.Force += forceLevel;
				suspensor.PercentageForce = percentageForceLevel;
				if (suspensor.ChargeUse < 1)
				{
					suspensor.ChargeUse = 1;
				}
				suspensor.IsRealityDistortionBased = true;
				suspensor.IsEMPSensitive = true;
				suspensor.IsBootSensitive = true;
			}
			int num = Math.Max((this.Tier >= 5) ? (5 - this.Tier / 2) : this.Tier, 2);
			BootSequence bootSequence = Object.GetPart<BootSequence>();
			if (bootSequence == null)
			{
				bootSequence = new BootSequence();
				bootSequence.BootTime = num;
				bootSequence.WorksOn(false, false, false, false, false, false, false, false, true, false);
				Object.AddPart<BootSequence>(bootSequence, true, false);
			}
			else if (bootSequence.WorksOnSelf)
			{
				if (bootSequence.BootTime < num)
				{
					bootSequence.BootTime = num;
				}
			}
			else
			{
				suspensor.IsBootSensitive = false;
			}
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(1, 2, null);
		}

		// Token: 0x0600B23A RID: 45626 RVA: 0x00413851 File Offset: 0x00411A51
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B23B RID: 45627 RVA: 0x0041386F File Offset: 0x00411A6F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{B|anti-gravity}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B23C RID: 45628 RVA: 0x00413899 File Offset: 0x00411A99
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModAntiGravity.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B23D RID: 45629 RVA: 0x004138CC File Offset: 0x00411ACC
		public static string GetDescription(int Tier)
		{
			int forceLevel = ModAntiGravity.GetForceLevel(Tier);
			return string.Concat(new string[]
			{
				"Anti-gravity: When powered, this item's weight is reduced by ",
				ModAntiGravity.GetPercentageForceLevel(Tier).ToString(),
				"% plus ",
				forceLevel.ToString(),
				" ",
				(forceLevel == 1) ? "lb" : "lbs",
				"."
			});
		}

		// Token: 0x0600B23E RID: 45630 RVA: 0x00413939 File Offset: 0x00411B39
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B23F RID: 45631 RVA: 0x00413940 File Offset: 0x00411B40
		public static int GetForceLevel(int Tier)
		{
			if (Tier <= 0)
			{
				return 2;
			}
			return Tier + 1 + Tier / 3;
		}

		// Token: 0x0600B240 RID: 45632 RVA: 0x0041394F File Offset: 0x00411B4F
		public static int GetPercentageForceLevel(int Tier)
		{
			if (Tier <= 0)
			{
				return 20;
			}
			return (Tier + 3) * 5;
		}
	}
}
