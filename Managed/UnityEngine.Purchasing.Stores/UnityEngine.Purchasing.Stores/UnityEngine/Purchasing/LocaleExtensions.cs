using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000073 RID: 115
	public static class LocaleExtensions
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00008788 File Offset: 0x00006988
		public static string[] GetLabelsWithSupportedPlatforms()
		{
			if (LocaleExtensions.LabelsWithSupportedPlatforms != null)
			{
				return LocaleExtensions.LabelsWithSupportedPlatforms;
			}
			LocaleExtensions.LabelsWithSupportedPlatforms = new string[Enum.GetValues(typeof(TranslationLocale)).Length];
			List<TranslationLocale> list = LocaleExtensions.GoogleLocales.ToList<TranslationLocale>();
			List<TranslationLocale> list2 = LocaleExtensions.AppleLocales.ToList<TranslationLocale>();
			int num = 0;
			foreach (object obj in Enum.GetValues(typeof(TranslationLocale)))
			{
				TranslationLocale translationLocale = (TranslationLocale)obj;
				List<string> list3 = new List<string>();
				if (list.Contains(translationLocale))
				{
					list3.Add("Google Play");
				}
				if (list2.Contains(translationLocale))
				{
					list3.Add("Apple");
				}
				string text = string.Join(", ", list3.ToArray());
				LocaleExtensions.LabelsWithSupportedPlatforms[num] = LocaleExtensions.Labels[num] + " (" + text + ")";
				num++;
			}
			return LocaleExtensions.LabelsWithSupportedPlatforms;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008898 File Offset: 0x00006A98
		public static bool SupportedOnApple(this TranslationLocale locale)
		{
			return LocaleExtensions.AppleLocales.Contains(locale);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000088A5 File Offset: 0x00006AA5
		public static bool SupportedOnGoogle(this TranslationLocale locale)
		{
			return LocaleExtensions.GoogleLocales.Contains(locale);
		}

		// Token: 0x04000156 RID: 342
		private static readonly string[] Labels = new string[]
		{
			"Chinese (Traditional)", "Czech", "Danish", "Dutch", "English (U.S.)", "French", "Finnish", "German", "Hebrew", "Hindi",
			"Italian", "Japanese", "Korean", "Norwegian", "Polish", "Portuguese (Portugal)", "Russian", "Spanish (Spain)", "Swedish", "Chinese (Simplified)",
			"English (Australia)", "English (Canada)", "English (U.K.)", "French (Canada)", "Greek", "Indonesian", "Malay", "Portuguese (Brazil)", "Spanish (Mexico)", "Thai",
			"Turkish", "Vietnamese"
		};

		// Token: 0x04000157 RID: 343
		private static readonly TranslationLocale[] GoogleLocales = new TranslationLocale[]
		{
			TranslationLocale.zh_TW,
			TranslationLocale.cs_CZ,
			TranslationLocale.da_DK,
			TranslationLocale.nl_NL,
			TranslationLocale.en_US,
			TranslationLocale.fr_FR,
			TranslationLocale.fi_FI,
			TranslationLocale.de_DE,
			TranslationLocale.iw_IL,
			TranslationLocale.hi_IN,
			TranslationLocale.it_IT,
			TranslationLocale.ja_JP,
			TranslationLocale.ko_KR,
			TranslationLocale.no_NO,
			TranslationLocale.pl_PL,
			TranslationLocale.pt_PT,
			TranslationLocale.ru_RU,
			TranslationLocale.es_ES,
			TranslationLocale.sv_SE
		};

		// Token: 0x04000158 RID: 344
		private static readonly TranslationLocale[] AppleLocales = new TranslationLocale[]
		{
			TranslationLocale.zh_CN,
			TranslationLocale.zh_TW,
			TranslationLocale.da_DK,
			TranslationLocale.nl_NL,
			TranslationLocale.en_AU,
			TranslationLocale.en_CA,
			TranslationLocale.en_GB,
			TranslationLocale.en_US,
			TranslationLocale.fi_FI,
			TranslationLocale.fr_FR,
			TranslationLocale.fr_CA,
			TranslationLocale.de_DE,
			TranslationLocale.el_GR,
			TranslationLocale.id_ID,
			TranslationLocale.it_IT,
			TranslationLocale.ja_JP,
			TranslationLocale.ko_KR,
			TranslationLocale.ms_MY,
			TranslationLocale.no_NO,
			TranslationLocale.pt_BR,
			TranslationLocale.pt_PT,
			TranslationLocale.ru_RU,
			TranslationLocale.es_MX,
			TranslationLocale.es_ES,
			TranslationLocale.sv_SE,
			TranslationLocale.th_TH,
			TranslationLocale.tr_TR,
			TranslationLocale.vi_VN
		};

		// Token: 0x04000159 RID: 345
		private static string[] LabelsWithSupportedPlatforms;
	}
}
