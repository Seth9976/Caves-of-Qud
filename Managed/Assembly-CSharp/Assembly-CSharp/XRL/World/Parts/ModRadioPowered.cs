using System;

namespace XRL.World.Parts
{
	// Token: 0x0200115F RID: 4447
	[Serializable]
	public class ModRadioPowered : IModification
	{
		// Token: 0x0600B0D1 RID: 45265 RVA: 0x004101F6 File Offset: 0x0040E3F6
		public ModRadioPowered()
		{
		}

		// Token: 0x0600B0D2 RID: 45266 RVA: 0x004101FE File Offset: 0x0040E3FE
		public ModRadioPowered(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0D3 RID: 45267 RVA: 0x00410207 File Offset: 0x0040E407
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B0D4 RID: 45268 RVA: 0x00410210 File Offset: 0x0040E410
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<EnergyCell>();
		}

		// Token: 0x0600B0D5 RID: 45269 RVA: 0x00410218 File Offset: 0x0040E418
		public override void ApplyModification(GameObject Object)
		{
			int num = this.Tier * 10;
			BroadcastPowerReceiver broadcastPowerReceiver = Object.RequirePart<BroadcastPowerReceiver>(false);
			if (broadcastPowerReceiver.ChargeRate < num)
			{
				broadcastPowerReceiver.ChargeRate = num;
			}
			int num2 = 11 + this.Tier;
			if (broadcastPowerReceiver.MaxSatellitePowerDepth < num2)
			{
				broadcastPowerReceiver.MaxSatellitePowerDepth = num2;
			}
			broadcastPowerReceiver.CanReceiveSatellitePower = true;
			broadcastPowerReceiver.Obvious = true;
			broadcastPowerReceiver.SatellitePowerOcclusionReadout = true;
			IntegralRecharger integralRecharger = Object.RequirePart<IntegralRecharger>(false);
			if (integralRecharger.ChargeRate != 0 && integralRecharger.ChargeRate < num)
			{
				integralRecharger.ChargeRate = num;
			}
			EnergyCell energyCell = Object.RequirePart<EnergyCell>(false);
			if (energyCell.ChargeRate < num)
			{
				energyCell.ChargeRate = num;
			}
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B0D6 RID: 45270 RVA: 0x004102BA File Offset: 0x0040E4BA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B0D7 RID: 45271 RVA: 0x004102D8 File Offset: 0x0040E4D8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{C|radio-powered}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0D8 RID: 45272 RVA: 0x00410302 File Offset: 0x0040E502
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModRadioPowered.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0D9 RID: 45273 RVA: 0x00410322 File Offset: 0x0040E522
		public static string GetDescription(int Tier)
		{
			return "Radio-powered: This item can be recharged via broadcast power.";
		}
	}
}
