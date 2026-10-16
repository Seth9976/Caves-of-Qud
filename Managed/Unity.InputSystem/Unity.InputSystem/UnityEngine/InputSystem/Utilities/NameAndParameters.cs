using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013B RID: 315
	public struct NameAndParameters
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000543A0 File Offset: 0x000525A0
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x000543A8 File Offset: 0x000525A8
		public string name { readonly get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000543B1 File Offset: 0x000525B1
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x000543B9 File Offset: 0x000525B9
		public ReadOnlyArray<NamedValue> parameters { readonly get; set; }

		// Token: 0x0600118D RID: 4493 RVA: 0x000543C4 File Offset: 0x000525C4
		public override string ToString()
		{
			if (this.parameters.Count == 0)
			{
				return this.name;
			}
			string text = string.Join(",", this.parameters.Select<NamedValue, string>((NamedValue x) => x.ToString()).ToArray<string>());
			return this.name + "(" + text + ")";
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0005443D File Offset: 0x0005263D
		internal static string ToSerializableString(IEnumerable<NameAndParameters> list)
		{
			if (list == null)
			{
				return string.Empty;
			}
			return string.Join(",", list.Select<NameAndParameters, string>((NameAndParameters x) => x.ToString()).ToArray<string>());
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0005447C File Offset: 0x0005267C
		internal static NameAndParameters Create(string name, IList<NamedValue> parameters)
		{
			return new NameAndParameters
			{
				name = name,
				parameters = new ReadOnlyArray<NamedValue>(parameters.ToArray<NamedValue>())
			};
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000544AC File Offset: 0x000526AC
		public static IEnumerable<NameAndParameters> ParseMultiple(string text)
		{
			List<NameAndParameters> list = null;
			if (!NameAndParameters.ParseMultiple(text, ref list))
			{
				return Enumerable.Empty<NameAndParameters>();
			}
			return list;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000544CC File Offset: 0x000526CC
		internal static bool ParseMultiple(string text, ref List<NameAndParameters> list)
		{
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (list == null)
			{
				list = new List<NameAndParameters>();
			}
			else
			{
				list.Clear();
			}
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				list.Add(NameAndParameters.ParseNameAndParameters(text, ref i, false));
			}
			return true;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00054520 File Offset: 0x00052720
		internal static string ParseName(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int num = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref num, true).name;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00054550 File Offset: 0x00052750
		public static NameAndParameters Parse(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int num = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref num, false);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00054578 File Offset: 0x00052778
		private static NameAndParameters ParseNameAndParameters(string text, ref int index, bool nameOnly = false)
		{
			int length = text.Length;
			while (index < length && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			int num = index;
			while (index < length)
			{
				char c = text[index];
				if (c == '(' || c == ","[0] || char.IsWhiteSpace(c))
				{
					break;
				}
				index++;
			}
			if (index - num == 0)
			{
				throw new ArgumentException(string.Format("Expecting name at position {0} in '{1}'", num, text), "text");
			}
			string text2 = text.Substring(num, index - num);
			if (nameOnly)
			{
				return new NameAndParameters
				{
					name = text2
				};
			}
			while (index < length && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			NamedValue[] array = null;
			if (index < length && text[index] == '(')
			{
				index++;
				int num2 = text.IndexOf(')', index);
				if (num2 == -1)
				{
					throw new ArgumentException(string.Format("Expecting ')' after '(' at position {0} in '{1}'", index, text), "text");
				}
				array = NamedValue.ParseMultiple(text.Substring(index, num2 - index));
				index = num2 + 1;
			}
			if (index < length && (text[index] == ',' || text[index] == ';'))
			{
				index++;
			}
			return new NameAndParameters
			{
				name = text2,
				parameters = new ReadOnlyArray<NamedValue>(array)
			};
		}
	}
}
