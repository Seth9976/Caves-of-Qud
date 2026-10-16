using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class LocalizedProductDescription
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00008A12 File Offset: 0x00006C12
		public LocalizedProductDescription Clone()
		{
			return new LocalizedProductDescription
			{
				googleLocale = this.googleLocale,
				Title = this.Title,
				Description = this.Description
			};
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008A3D File Offset: 0x00006C3D
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00008A4A File Offset: 0x00006C4A
		public string Title
		{
			get
			{
				return LocalizedProductDescription.DecodeNonLatinCharacters(this.title);
			}
			set
			{
				this.title = LocalizedProductDescription.EncodeNonLatinCharacters(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00008A58 File Offset: 0x00006C58
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00008A65 File Offset: 0x00006C65
		public string Description
		{
			get
			{
				return LocalizedProductDescription.DecodeNonLatinCharacters(this.description);
			}
			set
			{
				this.description = LocalizedProductDescription.EncodeNonLatinCharacters(value);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00008A74 File Offset: 0x00006C74
		private static string EncodeNonLatinCharacters(string s)
		{
			if (s == null)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (c > '\u007f')
				{
					string text = "\\u";
					int num = (int)c;
					string text2 = text + num.ToString("x4");
					stringBuilder.Append(text2);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008AE0 File Offset: 0x00006CE0
		private static string DecodeNonLatinCharacters(string s)
		{
			if (s == null)
			{
				return s;
			}
			return Regex.Replace(s, "\\\\u(?<Value>[a-zA-Z0-9]{4})", (Match m) => ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());
		}

		// Token: 0x0400015A RID: 346
		public TranslationLocale googleLocale = TranslationLocale.en_US;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		private string title;

		// Token: 0x0400015C RID: 348
		[SerializeField]
		private string description;
	}
}
