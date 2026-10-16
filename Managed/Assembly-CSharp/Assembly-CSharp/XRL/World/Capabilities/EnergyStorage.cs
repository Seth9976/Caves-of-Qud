using System;
using System.Collections.Generic;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E5 RID: 2533
	public static class EnergyStorage
	{
		// Token: 0x06006C08 RID: 27656 RVA: 0x0022E4F8 File Offset: 0x0022C6F8
		public static string GetSlotTypeName(string SlotType)
		{
			string text;
			if (SlotType != null && EnergyStorage.SlotTypeNames.TryGetValue(SlotType, out text))
			{
				return text;
			}
			return "energy cell";
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x0022E520 File Offset: 0x0022C720
		public static string GetSlotTypeShortName(string SlotType)
		{
			string text;
			if (SlotType != null && EnergyStorage.SlotTypeShortNames.TryGetValue(SlotType, out text))
			{
				return text;
			}
			return "cell";
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x0022E548 File Offset: 0x0022C748
		public static int GetChargePercentage(int Charge, int MaxCharge)
		{
			if (MaxCharge == 0)
			{
				return 100;
			}
			int num = Charge * 100 / MaxCharge;
			if (num < 0)
			{
				num = 0;
			}
			else if (num > 100)
			{
				num = 100;
			}
			if (num == 0 && Charge > 0)
			{
				num = 1;
			}
			else if (num == 100 && Charge < MaxCharge)
			{
				num = 99;
			}
			return num;
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x0022E58C File Offset: 0x0022C78C
		public static int GetChargeLevel(int Charge, int MaxCharge)
		{
			float num = (float)EnergyStorage.GetChargePercentage(Charge, MaxCharge);
			if (num == 0f)
			{
				return 0;
			}
			if (num <= 10f)
			{
				return 1;
			}
			if (num <= 25f)
			{
				return 2;
			}
			if (num <= 50f)
			{
				return 3;
			}
			if (num <= 75f)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x0022E5D8 File Offset: 0x0022C7D8
		public static string GetChargeLevelColor(int Level)
		{
			switch (Level)
			{
			case 0:
				return "K";
			case 1:
				return "r";
			case 2:
				return "R";
			case 3:
				return "W";
			case 4:
				return "g";
			case 5:
				return "G";
			default:
				throw new Exception("invalid charge level " + Level.ToString());
			}
		}

		// Token: 0x06006C0D RID: 27661 RVA: 0x0022E63F File Offset: 0x0022C83F
		public static string GetChargeColor(int Charge, int MaxCharge)
		{
			return EnergyStorage.GetChargeLevelColor(EnergyStorage.GetChargeLevel(Charge, MaxCharge));
		}

		// Token: 0x06006C0E RID: 27662 RVA: 0x0022E650 File Offset: 0x0022C850
		public static string ColorByChargeLevel(string text, int chargeLevel)
		{
			string chargeLevelColor = EnergyStorage.GetChargeLevelColor(chargeLevel);
			if (!string.IsNullOrEmpty(chargeLevelColor))
			{
				text = Event.NewStringBuilder(null).Append("{{").Append(chargeLevelColor)
					.Append('|')
					.Append(text)
					.Append("}}")
					.ToString();
			}
			return text;
		}

		// Token: 0x06006C0F RID: 27663 RVA: 0x0022E6A4 File Offset: 0x0022C8A4
		public static string ColorByChargeLevel(int number, int chargeLevel)
		{
			string chargeLevelColor = EnergyStorage.GetChargeLevelColor(chargeLevel);
			if (!string.IsNullOrEmpty(chargeLevelColor))
			{
				return Event.NewStringBuilder(null).Append("{{").Append(chargeLevelColor)
					.Append('|')
					.Append(number)
					.Append("}}")
					.ToString();
			}
			return number.ToString();
		}

		// Token: 0x06006C10 RID: 27664 RVA: 0x0022E6FC File Offset: 0x0022C8FC
		public static string GetChargeStatus(int Charge, int MaxCharge, string Style = "electrical")
		{
			if (Style == "amount")
			{
				return EnergyStorage.ColorByChargeLevel(Charge, 3);
			}
			int chargeLevel = EnergyStorage.GetChargeLevel(Charge, MaxCharge);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Style);
			if (num <= 1782065084U)
			{
				if (num <= 688666014U)
				{
					if (num != 131714989U)
					{
						if (num == 688666014U)
						{
							if (Style == "roughpercentage")
							{
								if (EnergyStorage.roughpercentageStatuses[chargeLevel] == null)
								{
									EnergyStorage.roughpercentageStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.roughpercentageDescriptions[chargeLevel], chargeLevel);
								}
								return EnergyStorage.roughpercentageStatuses[chargeLevel];
							}
						}
					}
					else if (Style == "tension")
					{
						if (EnergyStorage.tensionStatuses[chargeLevel] == null)
						{
							EnergyStorage.tensionStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.tensionDescriptions[chargeLevel], chargeLevel);
						}
						return EnergyStorage.tensionStatuses[chargeLevel];
					}
				}
				else if (num != 1540020817U)
				{
					if (num == 1782065084U)
					{
						if (Style == "glow")
						{
							if (EnergyStorage.glowStatuses[chargeLevel] == null)
							{
								EnergyStorage.glowStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.glowDescriptions[chargeLevel], chargeLevel);
							}
							return EnergyStorage.glowStatuses[chargeLevel];
						}
					}
				}
				else if (Style == "bio")
				{
					if (EnergyStorage.bioStatuses[chargeLevel] == null)
					{
						EnergyStorage.bioStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.bioDescriptions[chargeLevel], chargeLevel);
					}
					return EnergyStorage.bioStatuses[chargeLevel];
				}
			}
			else if (num <= 2113310899U)
			{
				if (num != 1877989396U)
				{
					if (num == 2113310899U)
					{
						if (Style == "electrical")
						{
							if (EnergyStorage.electricalStatuses[chargeLevel] == null)
							{
								EnergyStorage.electricalStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.electricalDescriptions[chargeLevel], chargeLevel);
							}
							return EnergyStorage.electricalStatuses[chargeLevel];
						}
					}
				}
				else if (Style == "kinetic")
				{
					if (EnergyStorage.kineticStatuses[chargeLevel] == null)
					{
						EnergyStorage.kineticStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.kineticDescriptions[chargeLevel], chargeLevel);
					}
					return EnergyStorage.kineticStatuses[chargeLevel];
				}
			}
			else if (num != 2538831285U)
			{
				if (num != 2816022776U)
				{
					if (num == 3732367685U)
					{
						if (Style == "dark")
						{
							if (EnergyStorage.darkStatuses[chargeLevel] == null)
							{
								EnergyStorage.darkStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.darkDescriptions[chargeLevel], chargeLevel);
							}
							return EnergyStorage.darkStatuses[chargeLevel];
						}
					}
				}
				else if (Style == "clockwork")
				{
					if (EnergyStorage.clockworkStatuses[chargeLevel] == null)
					{
						EnergyStorage.clockworkStatuses[chargeLevel] = EnergyStorage.ColorByChargeLevel(EnergyStorage.clockworkDescriptions[chargeLevel], chargeLevel);
					}
					return EnergyStorage.clockworkStatuses[chargeLevel];
				}
			}
			else if (Style == "percentage")
			{
				if (EnergyStorage.percentageStatuses.Count == 0)
				{
					for (int i = 0; i <= 100; i++)
					{
						EnergyStorage.percentageStatuses.Add(null);
					}
				}
				int chargePercentage = EnergyStorage.GetChargePercentage(Charge, MaxCharge);
				if (EnergyStorage.percentageStatuses[chargePercentage] == null)
				{
					EnergyStorage.percentageStatuses[chargePercentage] = EnergyStorage.ColorByChargeLevel(chargePercentage.ToString() + "%", chargeLevel);
				}
				return EnergyStorage.percentageStatuses[chargePercentage];
			}
			throw new Exception("invalid charge style " + Style);
		}

		// Token: 0x0400315C RID: 12636
		private static readonly string[] electricalDescriptions = new string[] { "Drained", "Very Low", "Low", "Used", "Fresh", "Full" };

		// Token: 0x0400315D RID: 12637
		private static List<string> electricalStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x0400315E RID: 12638
		private static readonly string[] clockworkDescriptions = new string[] { "Run Down", "Very Run Down", "Fairly Run Down", "Somewhat Run Down", "Well-Wound", "Fully Wound" };

		// Token: 0x0400315F RID: 12639
		private static List<string> clockworkStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x04003160 RID: 12640
		private static readonly string[] kineticDescriptions = new string[] { "Stopped", "Very Slow", "Somewhat Slow", "Fairly Fast", "Nearly Full Speed", "Full Speed" };

		// Token: 0x04003161 RID: 12641
		private static List<string> kineticStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x04003162 RID: 12642
		private static readonly string[] tensionDescriptions = new string[] { "Slack", "Very Slack", "Fairly Slack", "Somewhat Slack", "Tense", "Fully Tensed" };

		// Token: 0x04003163 RID: 12643
		private static List<string> tensionStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x04003164 RID: 12644
		private static readonly string[] glowDescriptions = new string[] { "Dark", "Very Dim", "Somewhat Dim", "Somewhat Bright", "Fairly Bright", "Bright" };

		// Token: 0x04003165 RID: 12645
		private static List<string> glowStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x04003166 RID: 12646
		private static readonly string[] darkDescriptions = new string[] { "Gray", "Dark Gray", "Murky Black", "Black", "Deep Black", "Pure Black" };

		// Token: 0x04003167 RID: 12647
		private static List<string> darkStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x04003168 RID: 12648
		private static string[] bioDescriptions = new string[] { "Exhausted", "Flagging", "Enervated", "Fatigued", "Lively", "Vigorous" };

		// Token: 0x04003169 RID: 12649
		private static List<string> bioStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x0400316A RID: 12650
		private static readonly string[] roughpercentageDescriptions = new string[] { "0%", "~10%", "~25%", "~50%", "~75%", "~100%" };

		// Token: 0x0400316B RID: 12651
		private static List<string> roughpercentageStatuses = new List<string> { null, null, null, null, null, null };

		// Token: 0x0400316C RID: 12652
		private static List<string> percentageStatuses = new List<string>(101);

		// Token: 0x0400316D RID: 12653
		private static readonly Dictionary<string, string> SlotTypeNames = new Dictionary<string, string>
		{
			{ "EnergyCell", "energy cell" },
			{ "PowerCore", "power core" }
		};

		// Token: 0x0400316E RID: 12654
		private static readonly Dictionary<string, string> SlotTypeShortNames = new Dictionary<string, string>
		{
			{ "EnergyCell", "cell" },
			{ "PowerCore", "core" }
		};
	}
}
