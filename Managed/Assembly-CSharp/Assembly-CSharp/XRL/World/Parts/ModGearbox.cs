using System;

namespace XRL.World.Parts
{
	// Token: 0x0200113E RID: 4414
	[Serializable]
	public class ModGearbox : IModification
	{
		// Token: 0x0600AF18 RID: 44824 RVA: 0x00409C43 File Offset: 0x00407E43
		public ModGearbox()
		{
		}

		// Token: 0x0600AF19 RID: 44825 RVA: 0x00409C4B File Offset: 0x00407E4B
		public ModGearbox(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF1A RID: 44826 RVA: 0x00409C54 File Offset: 0x00407E54
		public override void ApplyModification(GameObject Object)
		{
			MechanicalPowerTransmission mechanicalPowerTransmission = Object.GetPart<MechanicalPowerTransmission>();
			if (mechanicalPowerTransmission == null)
			{
				mechanicalPowerTransmission = new MechanicalPowerTransmission();
				if (this.Tier > 1)
				{
					mechanicalPowerTransmission.ChargeRate = 100 + this.Tier * 20;
				}
				Object.AddPart<MechanicalPowerTransmission>(mechanicalPowerTransmission, true, false);
			}
			else if (mechanicalPowerTransmission.ChargeRate < 100 + this.Tier * 20)
			{
				mechanicalPowerTransmission.ChargeRate = 100 + this.Tier * 20;
			}
			base.IncreaseDifficulty(1, null);
		}

		// Token: 0x0600AF1B RID: 44827 RVA: 0x00409CC6 File Offset: 0x00407EC6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GenericRatingEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF1C RID: 44828 RVA: 0x00409CEC File Offset: 0x00407EEC
		public override bool HandleEvent(GenericRatingEvent E)
		{
			if (E.Type == "WallDigNavigationWeight" && E.Object == this.ParentObject)
			{
				E.Rating += 8;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF1D RID: 44829 RVA: 0x00409D23 File Offset: 0x00407F23
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("gearbox");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF1E RID: 44830 RVA: 0x00409D4C File Offset: 0x00407F4C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModGearbox.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF1F RID: 44831 RVA: 0x00409D6C File Offset: 0x00407F6C
		public static string GetDescription(int Tier)
		{
			return "Gearbox: Has mechanical power transmission components installed.";
		}
	}
}
