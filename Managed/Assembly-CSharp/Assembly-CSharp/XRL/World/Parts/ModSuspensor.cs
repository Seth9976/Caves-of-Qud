using System;

namespace XRL.World.Parts
{
	// Token: 0x02001171 RID: 4465
	[Serializable]
	public class ModSuspensor : IModification
	{
		// Token: 0x0600B1A5 RID: 45477 RVA: 0x00412288 File Offset: 0x00410488
		public ModSuspensor()
		{
		}

		// Token: 0x0600B1A6 RID: 45478 RVA: 0x00412290 File Offset: 0x00410490
		public ModSuspensor(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1A7 RID: 45479 RVA: 0x00412299 File Offset: 0x00410499
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.GetWeight() > 0.0;
		}

		// Token: 0x0600B1A8 RID: 45480 RVA: 0x004122B0 File Offset: 0x004104B0
		public override void ApplyModification(GameObject Object)
		{
			Suspensor suspensor = Object.GetPart<Suspensor>();
			if (suspensor == null)
			{
				suspensor = new Suspensor();
				suspensor.PercentageForce = 100;
				Object.AddPart<Suspensor>(suspensor, true, false);
			}
			else
			{
				suspensor.PercentageForce = 100;
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

		// Token: 0x0600B1A9 RID: 45481 RVA: 0x00412395 File Offset: 0x00410595
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B1AA RID: 45482 RVA: 0x004123B3 File Offset: 0x004105B3
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{watery|suspensors}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1AB RID: 45483 RVA: 0x004123DC File Offset: 0x004105DC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModSuspensor.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1AC RID: 45484 RVA: 0x004123FC File Offset: 0x004105FC
		public static string GetDescription(int Tier)
		{
			return "Fitted with suspensors: When powered, this item is weightless.";
		}
	}
}
