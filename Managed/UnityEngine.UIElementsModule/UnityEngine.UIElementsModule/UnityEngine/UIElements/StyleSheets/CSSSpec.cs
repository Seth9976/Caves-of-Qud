using System;
using System.Text.RegularExpressions;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006D5 RID: 1749
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class CSSSpec
	{
		// Token: 0x060032A4 RID: 12964 RVA: 0x000C2BEC File Offset: 0x000C0DEC
		public static int GetSelectorSpecificity(string selector)
		{
			int num = 0;
			StyleSelectorPart[] array;
			bool flag = CSSSpec.ParseSelector(selector, out array);
			if (flag)
			{
				num = CSSSpec.GetSelectorSpecificity(array);
			}
			return num;
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000C2C18 File Offset: 0x000C0E18
		public static int GetSelectorSpecificity(StyleSelectorPart[] parts)
		{
			int num = 1;
			for (int i = 0; i < parts.Length; i++)
			{
				switch (parts[i].type)
				{
				case StyleSelectorType.Type:
					num++;
					break;
				case StyleSelectorType.Class:
				case StyleSelectorType.PseudoClass:
					num += 10;
					break;
				case StyleSelectorType.RecursivePseudoClass:
					throw new ArgumentException("Recursive pseudo classes are not supported");
				case StyleSelectorType.ID:
					num += 100;
					break;
				}
			}
			return num;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000C2C94 File Offset: 0x000C0E94
		public static bool ParseSelector(string selector, out StyleSelectorPart[] parts)
		{
			MatchCollection matchCollection = CSSSpec.rgx.Matches(selector);
			int count = matchCollection.Count;
			bool flag = count < 1;
			bool flag2;
			if (flag)
			{
				parts = null;
				flag2 = false;
			}
			else
			{
				parts = new StyleSelectorPart[count];
				for (int i = 0; i < count; i++)
				{
					Match match = matchCollection[i];
					StyleSelectorType styleSelectorType = StyleSelectorType.Unknown;
					string text = string.Empty;
					bool flag3 = !string.IsNullOrEmpty(match.Groups["wildcard"].Value);
					if (flag3)
					{
						text = "*";
						styleSelectorType = StyleSelectorType.Wildcard;
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(match.Groups["id"].Value);
						if (flag4)
						{
							text = match.Groups["id"].Value.Substring(1);
							styleSelectorType = StyleSelectorType.ID;
						}
						else
						{
							bool flag5 = !string.IsNullOrEmpty(match.Groups["class"].Value);
							if (flag5)
							{
								text = match.Groups["class"].Value.Substring(1);
								styleSelectorType = StyleSelectorType.Class;
							}
							else
							{
								bool flag6 = !string.IsNullOrEmpty(match.Groups["pseudoclass"].Value);
								if (flag6)
								{
									string value = match.Groups["param"].Value;
									bool flag7 = !string.IsNullOrEmpty(value);
									if (flag7)
									{
										text = value;
										styleSelectorType = StyleSelectorType.RecursivePseudoClass;
									}
									else
									{
										text = match.Groups["pseudoclass"].Value.Substring(1);
										styleSelectorType = StyleSelectorType.PseudoClass;
									}
								}
								else
								{
									bool flag8 = !string.IsNullOrEmpty(match.Groups["type"].Value);
									if (flag8)
									{
										text = match.Groups["type"].Value;
										styleSelectorType = StyleSelectorType.Type;
									}
								}
							}
						}
					}
					parts[i] = new StyleSelectorPart
					{
						type = styleSelectorType,
						value = text
					};
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x040019C4 RID: 6596
		private static readonly Regex rgx = new Regex("(?<id>#[-]?\\w[\\w-]*)|(?<class>\\.[\\w-]+)|(?<pseudoclass>:[\\w-]+(\\((?<param>.+)\\))?)|(?<type>([^\\-]\\w+|\\w+))|(?<wildcard>\\*)|\\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040019C5 RID: 6597
		private const int typeSelectorWeight = 1;

		// Token: 0x040019C6 RID: 6598
		private const int classSelectorWeight = 10;

		// Token: 0x040019C7 RID: 6599
		private const int idSelectorWeight = 100;
	}
}
