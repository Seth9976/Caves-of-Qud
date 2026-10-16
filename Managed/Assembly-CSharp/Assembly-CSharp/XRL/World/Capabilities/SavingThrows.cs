using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F4 RID: 2548
	public static class SavingThrows
	{
		// Token: 0x06006CB5 RID: 27829 RVA: 0x00235DD0 File Offset: 0x00233FD0
		public static List<string> VsList(string Text)
		{
			List<string> list;
			if (SavingThrows.VsLists.TryGetValue(Text, out list))
			{
				return list;
			}
			list = new List<string>(Text.Split(',', StringSplitOptions.None));
			SavingThrows.VsLists.Add(Text, list);
			return list;
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x00235E0C File Offset: 0x0023400C
		public static List<string> VsSet(string Text)
		{
			List<string> list;
			if (SavingThrows.VsSets.TryGetValue(Text, out list))
			{
				return list;
			}
			list = new List<string>(Text.Split(' ', StringSplitOptions.None));
			SavingThrows.VsSets.Add(Text, list);
			return list;
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x00235E48 File Offset: 0x00234048
		public static bool Applicable(string ApplyVs, string SaveVs)
		{
			if (ApplyVs.IsNullOrEmpty())
			{
				return true;
			}
			if (SaveVs == null)
			{
				return false;
			}
			if (ApplyVs.Contains(","))
			{
				return SavingThrows.Applicable(SavingThrows.VsList(ApplyVs), SaveVs);
			}
			if (ApplyVs.Contains(" "))
			{
				foreach (string text in SavingThrows.VsSet(ApplyVs))
				{
					if (!SaveVs.Contains(text))
					{
						return false;
					}
				}
				return true;
			}
			return SaveVs.Contains(ApplyVs);
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x00235EE4 File Offset: 0x002340E4
		public static bool Applicable(string[] ApplyVs, string SaveVs)
		{
			if (ApplyVs == null || ApplyVs.Length == 0)
			{
				return true;
			}
			if (SaveVs == null)
			{
				return false;
			}
			foreach (string text in ApplyVs)
			{
				if (text.Contains(" "))
				{
					bool flag = false;
					foreach (string text2 in SavingThrows.VsSet(text))
					{
						if (!SaveVs.Contains(text2))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return true;
					}
				}
				else if (SaveVs.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x00235F84 File Offset: 0x00234184
		public static bool Applicable(List<string> ApplyVs, string SaveVs)
		{
			if (ApplyVs == null || ApplyVs.Count == 0)
			{
				return true;
			}
			if (SaveVs == null)
			{
				return false;
			}
			foreach (string text in ApplyVs)
			{
				if (text.Contains(" "))
				{
					bool flag = false;
					foreach (string text2 in SavingThrows.VsSet(text))
					{
						if (!SaveVs.Contains(text2))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return true;
					}
				}
				else if (SaveVs.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x00236050 File Offset: 0x00234250
		public static bool Applicable(string ApplyVs, Event E)
		{
			return SavingThrows.Applicable(ApplyVs, E.GetStringParameter("Vs", null));
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x00236064 File Offset: 0x00234264
		public static bool Applicable(string[] ApplyVs, Event E)
		{
			return SavingThrows.Applicable(ApplyVs, E.GetStringParameter("Vs", null));
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x00236078 File Offset: 0x00234278
		public static bool Applicable(List<string> ApplyVs, Event E)
		{
			return SavingThrows.Applicable(ApplyVs, E.GetStringParameter("Vs", null));
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x0023608C File Offset: 0x0023428C
		public static bool Applicable(string ApplyVs, ISaveEvent E)
		{
			return SavingThrows.Applicable(ApplyVs, E.Vs);
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x0023609A File Offset: 0x0023429A
		public static bool Applicable(string[] ApplyVs, ISaveEvent E)
		{
			return SavingThrows.Applicable(ApplyVs, E.Vs);
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x002360A8 File Offset: 0x002342A8
		public static bool Applicable(List<string> ApplyVs, ISaveEvent E)
		{
			return SavingThrows.Applicable(ApplyVs, E.Vs);
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x002360B8 File Offset: 0x002342B8
		private static string SingleDesc(string Item)
		{
			string text;
			if (SavingThrows.VariantDescriptions.TryGetValue(Item, out text))
			{
				return text;
			}
			BaseLiquid liquid = LiquidVolume.GetLiquid(Item.ToLower());
			if (liquid != null)
			{
				string text2 = "effects of " + liquid.GetName().Strip();
				SavingThrows.VariantDescriptions[Item] = text2;
				return text2;
			}
			SavingThrows.SingleDescStringBuilder.Clear();
			int i = 0;
			int length = Item.Length;
			while (i < length)
			{
				char c = Item[i];
				if (char.IsUpper(c))
				{
					if (i > 0)
					{
						SavingThrows.SingleDescStringBuilder.Append(' ');
					}
					SavingThrows.SingleDescStringBuilder.Append(char.ToLower(c));
				}
				else
				{
					SavingThrows.SingleDescStringBuilder.Append(c);
				}
				i++;
			}
			return SavingThrows.SingleDescStringBuilder.ToString();
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x0023617C File Offset: 0x0023437C
		public static string GetSaveBonusTypeDescription(string Text)
		{
			string text;
			if (SavingThrows.Descriptions.TryGetValue(Text, out text))
			{
				return text;
			}
			List<string> list = new List<string>(SavingThrows.VsList(Text));
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				list[i] = SavingThrows.SingleDesc(list[i]);
				i++;
			}
			return Grammar.MakeAndList(list, true);
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x002361D4 File Offset: 0x002343D4
		public static void AppendSaveBonusDescription(StringBuilder Store, int Bonus, string Text, bool HighlightNumber = false, bool Highlight = false, bool LeadingNewline = true)
		{
			if (Bonus == 0)
			{
				return;
			}
			if (LeadingNewline && Store.Length > 0 && !Store.EndsWith('\n'))
			{
				Store.Append('\n');
			}
			if (Highlight)
			{
				Store.Append("{{rules|");
			}
			if (Bonus >= SavingThrows.IMMUNITY && !Text.IsNullOrEmpty())
			{
				Store.Append("Immune to ").Append(SavingThrows.GetSaveBonusTypeDescription(Text));
			}
			else
			{
				if (Bonus > 0)
				{
					Store.Append('+');
				}
				if (HighlightNumber)
				{
					Store.Append("{{rules|");
				}
				Store.Append(Bonus);
				if (HighlightNumber)
				{
					Store.Append("}}");
				}
				Store.Append(" to saves");
				if (!Text.IsNullOrEmpty())
				{
					Store.Append(" vs. ").Append(SavingThrows.GetSaveBonusTypeDescription(Text));
				}
			}
			if (Highlight)
			{
				Store.Append("}}");
			}
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x002362AC File Offset: 0x002344AC
		public static string GetSaveBonusDescription(int Bonus, string Text, bool HighlightNumber = false)
		{
			SavingThrows.GetSaveBonusDescriptionStringBuilder.Clear();
			SavingThrows.AppendSaveBonusDescription(SavingThrows.GetSaveBonusDescriptionStringBuilder, Bonus, Text, false, false, true);
			if (SavingThrows.GetSaveBonusDescriptionStringBuilder.Length <= 0)
			{
				return null;
			}
			return SavingThrows.GetSaveBonusDescriptionStringBuilder.ToString();
		}

		// Token: 0x040031CC RID: 12748
		public static readonly int IMMUNITY = 9999;

		// Token: 0x040031CD RID: 12749
		[NonSerialized]
		private static Dictionary<string, List<string>> VsLists = new Dictionary<string, List<string>>();

		// Token: 0x040031CE RID: 12750
		[NonSerialized]
		private static Dictionary<string, List<string>> VsSets = new Dictionary<string, List<string>>();

		// Token: 0x040031CF RID: 12751
		[NonSerialized]
		private static Dictionary<string, string> Descriptions = new Dictionary<string, string>();

		// Token: 0x040031D0 RID: 12752
		[NonSerialized]
		private static Dictionary<string, string> VariantDescriptions = new Dictionary<string, string>
		{
			{ "Axe", "Axe skills" },
			{ "Beam", "energy beams" },
			{ "Blade", "bladed weapons" },
			{ "Contact", "contact agents" },
			{ "Cudgel", "Cudgel skills" },
			{ "Decarbonizer", "Decarbonizer attacks" },
			{ "Disarm", "disarmament" },
			{ "Drag", "being dragged" },
			{ "EMP", "electromagnetic pulses" },
			{ "Escape", "being captured" },
			{ "Fungal", "fungal infestation" },
			{ "Gas", "gases" },
			{ "Gaze", "gaze effects" },
			{ "Grab", "being grabbed" },
			{ "HookAndDrag", "Hook and Drag" },
			{ "Hologram", "holographic effects" },
			{ "Inhaled", "inhaled toxins" },
			{ "Injected", "injected toxins" },
			{ "LatchOn", "being latched onto" },
			{ "Lithofex", "Lithofex attacks" },
			{ "LongBlades", "Long Blades skills" },
			{ "Move", "forced movement" },
			{ "Onset", "the initial onset of disease" },
			{ "Phase", "phase effects" },
			{ "Pistol", "Pistol skills" },
			{ "Restraint", "being restrained" },
			{ "Rifle", "Bow and Rifle skills" },
			{ "RobotStop", "adversarial signage" },
			{ "ShieldSlam", "Shield Slam" },
			{ "ShortBlades", "Short Blades skills" },
			{ "Sleep", "forced sleep" },
			{ "Slip", "slipping" },
			{ "SlogGlands", "the bilge sphincter" },
			{ "Stoning", "petrification" },
			{ "Stuck", "becoming stuck" },
			{ "Stun", "being stunned" },
			{ "StunningForce", "Stunning Force" },
			{ "Swipe", "Swipe" },
			{ "Taunt", "taunting" },
			{ "Tinkering", "Tinkering effects" },
			{ "Verbal", "speech effects" },
			{ "Web", "webbing" }
		};

		// Token: 0x040031D1 RID: 12753
		[NonSerialized]
		private static StringBuilder SingleDescStringBuilder = new StringBuilder();

		// Token: 0x040031D2 RID: 12754
		[NonSerialized]
		private static StringBuilder GetSaveBonusDescriptionStringBuilder = new StringBuilder();
	}
}
