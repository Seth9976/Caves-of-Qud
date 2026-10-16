using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F2 RID: 2546
	public static class PropertyDescription
	{
		// Token: 0x06006CA9 RID: 27817 RVA: 0x002356F5 File Offset: 0x002338F5
		public static bool IsInverseBenefit(string Property)
		{
			return PropertyDescription.InverseBenefitProperties.Contains(Property);
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x00235702 File Offset: 0x00233902
		public static bool IsBoolean(string Property)
		{
			return PropertyDescription.BooleanProperties.Contains(Property);
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x00235710 File Offset: 0x00233910
		public static string GetPropertyDisplayName(string Property)
		{
			string text;
			if (!PropertyDescription.PropertyDisplayNames.TryGetValue(Property, out text))
			{
				return Property;
			}
			return text;
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x00235730 File Offset: 0x00233930
		public static int GetPropertyEffectType(string Property)
		{
			int num;
			if (!PropertyDescription.PropertyEffectTypes.TryGetValue(Property, out num))
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x0023574F File Offset: 0x0023394F
		public static void GetPropertyDescription(StringBuilder SB, string Property)
		{
			if (PropertyDescription.IsBoolean(Property))
			{
				SB.Append("provides ");
			}
			else
			{
				SB.Append("affects ");
			}
			SB.Append(PropertyDescription.GetPropertyDisplayName(Property));
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x00235780 File Offset: 0x00233980
		public static string GetPropertyDescription(string Property)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			PropertyDescription.GetPropertyDescription(stringBuilder, Property);
			return stringBuilder.ToString();
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x00235794 File Offset: 0x00233994
		public static void GetPropertyAdjustDescription(StringBuilder SB, string Property, int Adjust)
		{
			if (PropertyDescription.IsInverseBenefit(Property))
			{
				Adjust = -Adjust;
			}
			if (PropertyDescription.IsBoolean(Property))
			{
				SB.Append((Adjust > 0) ? "provides " : "inhibits ");
			}
			else
			{
				if (Adjust > 0)
				{
					SB.Append('+');
				}
				SB.Append(Adjust).Append(" to ");
			}
			SB.Append(PropertyDescription.GetPropertyDisplayName(Property));
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x002357FA File Offset: 0x002339FA
		public static string GetPropertyAdjustDescription(string Property, int Adjust)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			PropertyDescription.GetPropertyAdjustDescription(stringBuilder, Property, Adjust);
			return stringBuilder.ToString();
		}

		// Token: 0x040031C8 RID: 12744
		[NonSerialized]
		public static List<string> InverseBenefitProperties = new List<string>();

		// Token: 0x040031C9 RID: 12745
		[NonSerialized]
		public static List<string> BooleanProperties = new List<string> { "Analgesia", "BioScannerEquipped", "ForcefieldNullifier", "Horrifying", "Serene", "StructureScannerEquipped", "TechScannerEquipped" };

		// Token: 0x040031CA RID: 12746
		[NonSerialized]
		public static Dictionary<string, string> PropertyDisplayNames = new Dictionary<string, string>
		{
			{ "Analgesia", "analgesia" },
			{ "BioScannerEquipped", "bioscanning" },
			{ "ButcheryToolEquipped", "butchery" },
			{ "CarryBonus", "carrying capacity" },
			{ "CyberneticsLicenses", "cybernetics license tier" },
			{ "DisassembleBonus", "artifact disassembly" },
			{ "ForcefieldNullifier", "forcefield nullification" },
			{ "Horrifying", "horrifying presence" },
			{ "InspectorEquipped", "artifact inspection" },
			{ "MentalMutationForceShift", "mental mutations regardless of level" },
			{ "MentalMutationShift", "mental mutations" },
			{ "PhysicalMutationForceShift", "physical mutations regardless of level" },
			{ "PhysicalMutationShift", "physical mutations" },
			{ "Serene", "meditative serenity" },
			{ "StructureScannerEquipped", "structural scanning" },
			{ "TechScannerEquipped", "techscanning" }
		};

		// Token: 0x040031CB RID: 12747
		[NonSerialized]
		public static Dictionary<string, int> PropertyEffectTypes = new Dictionary<string, int>
		{
			{ "Analgesia", 8192 },
			{ "BioScannerEquipped", 128 },
			{ "ButcheryToolEquipped", 128 },
			{ "CarryBonus", 128 },
			{ "CyberneticsLicenses", 128 },
			{ "DisassembleBonus", 128 },
			{ "ForcefieldNullifier", 64 },
			{ "Horrifying", 128 },
			{ "InspectorEquipped", 64 },
			{ "MentalMutationForceShift", 2 },
			{ "MentalMutationShift", 2 },
			{ "PhysicalMutationForceShift", 4 },
			{ "PhysicalMutationShift", 4 },
			{ "Serene", 2 },
			{ "StructureScannerEquipped", 128 },
			{ "TechScannerEquipped", 128 }
		};
	}
}
