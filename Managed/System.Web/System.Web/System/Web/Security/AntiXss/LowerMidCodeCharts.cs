using System;

namespace System.Web.Security.AntiXss
{
	/// <summary>Specifies values for the lower-middle region of the UTF-8 Unicode code charts, from U1000 to U1EFF.</summary>
	// Token: 0x020004D3 RID: 1235
	[Flags]
	public enum LowerMidCodeCharts : long
	{
		/// <summary>None of the UTF-8 Unicode code charts from the lower-middle region are marked as safe.</summary>
		// Token: 0x04001E4F RID: 7759
		None = 0L,
		/// <summary>The Myanmar code chart.</summary>
		// Token: 0x04001E50 RID: 7760
		Myanmar = 1L,
		/// <summary>The Georgian code chart.</summary>
		// Token: 0x04001E51 RID: 7761
		Georgian = 2L,
		/// <summary>The Hangul Jamo code chart</summary>
		// Token: 0x04001E52 RID: 7762
		HangulJamo = 4L,
		/// <summary>The Ethiopic code chart.</summary>
		// Token: 0x04001E53 RID: 7763
		Ethiopic = 8L,
		/// <summary>The Ethiopic Supplement code chart.</summary>
		// Token: 0x04001E54 RID: 7764
		EthiopicSupplement = 16L,
		/// <summary>The Cherokee code chart.</summary>
		// Token: 0x04001E55 RID: 7765
		Cherokee = 32L,
		/// <summary>The Unified Canadian Aboriginal Syllabics code chart.</summary>
		// Token: 0x04001E56 RID: 7766
		UnifiedCanadianAboriginalSyllabics = 64L,
		/// <summary>The Ogham code chart.</summary>
		// Token: 0x04001E57 RID: 7767
		Ogham = 128L,
		/// <summary>The Runic code chart.</summary>
		// Token: 0x04001E58 RID: 7768
		Runic = 256L,
		/// <summary>The Tagalog code chart.</summary>
		// Token: 0x04001E59 RID: 7769
		Tagalog = 512L,
		/// <summary>The Hanunoo code chart.</summary>
		// Token: 0x04001E5A RID: 7770
		Hanunoo = 1024L,
		/// <summary>The Buhid code chart</summary>
		// Token: 0x04001E5B RID: 7771
		Buhid = 2048L,
		/// <summary>The Tagbanwa code chart.</summary>
		// Token: 0x04001E5C RID: 7772
		Tagbanwa = 4096L,
		/// <summary>The Khmer code chart.</summary>
		// Token: 0x04001E5D RID: 7773
		Khmer = 8192L,
		/// <summary>The Mongolian code chart.</summary>
		// Token: 0x04001E5E RID: 7774
		Mongolian = 16384L,
		/// <summary>The Unified Canadian Aboriginal Syllabics Extended code chart.</summary>
		// Token: 0x04001E5F RID: 7775
		UnifiedCanadianAboriginalSyllabicsExtended = 32768L,
		/// <summary>The Limbu code chart.</summary>
		// Token: 0x04001E60 RID: 7776
		Limbu = 65536L,
		/// <summary>The Tai Le code chart.</summary>
		// Token: 0x04001E61 RID: 7777
		TaiLe = 131072L,
		/// <summary>The New Tai Lue code chart.</summary>
		// Token: 0x04001E62 RID: 7778
		NewTaiLue = 262144L,
		/// <summary>The Khmer Symbols code chart.</summary>
		// Token: 0x04001E63 RID: 7779
		KhmerSymbols = 524288L,
		/// <summary>The Buginese code chart</summary>
		// Token: 0x04001E64 RID: 7780
		Buginese = 1048576L,
		/// <summary>The Tai Tham code chart.</summary>
		// Token: 0x04001E65 RID: 7781
		TaiTham = 2097152L,
		/// <summary>The Balinese code chart.</summary>
		// Token: 0x04001E66 RID: 7782
		Balinese = 4194304L,
		/// <summary>The Sudanese code chart.</summary>
		// Token: 0x04001E67 RID: 7783
		Sudanese = 8388608L,
		/// <summary>The Lepcha code chart.</summary>
		// Token: 0x04001E68 RID: 7784
		Lepcha = 16777216L,
		/// <summary>The Ol Chiki code chart.</summary>
		// Token: 0x04001E69 RID: 7785
		OlChiki = 33554432L,
		/// <summary>The Vedic Extensions code chart.</summary>
		// Token: 0x04001E6A RID: 7786
		VedicExtensions = 67108864L,
		/// <summary>The Phonetic Extensions code chart.</summary>
		// Token: 0x04001E6B RID: 7787
		PhoneticExtensions = 134217728L,
		/// <summary>The Phonetic Extensions Supplement code chart.</summary>
		// Token: 0x04001E6C RID: 7788
		PhoneticExtensionsSupplement = 268435456L,
		/// <summary>The Combining Diacritical Marks Supplement code chart.</summary>
		// Token: 0x04001E6D RID: 7789
		CombiningDiacriticalMarksSupplement = 536870912L,
		/// <summary>The Latin Extended Additional code chart.</summary>
		// Token: 0x04001E6E RID: 7790
		LatinExtendedAdditional = 1073741824L
	}
}
