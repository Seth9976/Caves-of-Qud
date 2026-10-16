using System;

namespace System.Web.Security.AntiXss
{
	/// <summary>Specifies values for the lower region of the UTF-8 Unicode code charts, from U0000 to U0FFF.</summary>
	// Token: 0x020004D2 RID: 1234
	[Flags]
	public enum LowerCodeCharts : long
	{
		/// <summary>None of the UTF-8 Unicode code charts from the lower region are marked as safe.</summary>
		// Token: 0x04001E2D RID: 7725
		None = 0L,
		/// <summary>The Basic Latin code chart.</summary>
		// Token: 0x04001E2E RID: 7726
		BasicLatin = 1L,
		/// <summary>The C1 Controls and Latin-1 Supplement code chart.</summary>
		// Token: 0x04001E2F RID: 7727
		C1ControlsAndLatin1Supplement = 2L,
		/// <summary>The Latin Extended-A code chart.</summary>
		// Token: 0x04001E30 RID: 7728
		LatinExtendedA = 4L,
		/// <summary>The Latin Extended-B code chart.</summary>
		// Token: 0x04001E31 RID: 7729
		LatinExtendedB = 8L,
		/// <summary>The IPA Extensions code chart.</summary>
		// Token: 0x04001E32 RID: 7730
		IpaExtensions = 16L,
		/// <summary>The Spacing Modifier Letters code chart.</summary>
		// Token: 0x04001E33 RID: 7731
		SpacingModifierLetters = 32L,
		/// <summary>The Combining Diacritical Marks code chart.</summary>
		// Token: 0x04001E34 RID: 7732
		CombiningDiacriticalMarks = 64L,
		/// <summary>The Greek and Coptic code chart.</summary>
		// Token: 0x04001E35 RID: 7733
		GreekAndCoptic = 128L,
		/// <summary>The Cyrillic code chart.</summary>
		// Token: 0x04001E36 RID: 7734
		Cyrillic = 256L,
		/// <summary>The Cyrillic Supplement code chart.</summary>
		// Token: 0x04001E37 RID: 7735
		CyrillicSupplement = 512L,
		/// <summary>The Armenian code chart.</summary>
		// Token: 0x04001E38 RID: 7736
		Armenian = 1024L,
		/// <summary>The Hebrew code chart.</summary>
		// Token: 0x04001E39 RID: 7737
		Hebrew = 2048L,
		/// <summary>The Arabic code chart.</summary>
		// Token: 0x04001E3A RID: 7738
		Arabic = 4096L,
		/// <summary>The Syriac code chart.</summary>
		// Token: 0x04001E3B RID: 7739
		Syriac = 8192L,
		/// <summary>The Arabic Supplement code chart.</summary>
		// Token: 0x04001E3C RID: 7740
		ArabicSupplement = 16384L,
		/// <summary>The Thaana code chart.</summary>
		// Token: 0x04001E3D RID: 7741
		Thaana = 32768L,
		/// <summary>The N'ko code chart.</summary>
		// Token: 0x04001E3E RID: 7742
		Nko = 65536L,
		/// <summary>The Samaritan code chart.</summary>
		// Token: 0x04001E3F RID: 7743
		Samaritan = 131072L,
		/// <summary>The Devanagari code chart.</summary>
		// Token: 0x04001E40 RID: 7744
		Devanagari = 262144L,
		/// <summary>The Bengali code chart.</summary>
		// Token: 0x04001E41 RID: 7745
		Bengali = 524288L,
		/// <summary>The Gurmukhi code chart.</summary>
		// Token: 0x04001E42 RID: 7746
		Gurmukhi = 1048576L,
		/// <summary>The Gujarati code chart.</summary>
		// Token: 0x04001E43 RID: 7747
		Gujarati = 2097152L,
		/// <summary>The Oriya code chart.</summary>
		// Token: 0x04001E44 RID: 7748
		Oriya = 4194304L,
		/// <summary>The Tamil code chart.</summary>
		// Token: 0x04001E45 RID: 7749
		Tamil = 8388608L,
		/// <summary>The Telugu code chart.</summary>
		// Token: 0x04001E46 RID: 7750
		Telugu = 16777216L,
		/// <summary>The Kannada code chart.</summary>
		// Token: 0x04001E47 RID: 7751
		Kannada = 33554432L,
		/// <summary>The Malayalam code chart.</summary>
		// Token: 0x04001E48 RID: 7752
		Malayalam = 67108864L,
		/// <summary>The Sinhala code chart.</summary>
		// Token: 0x04001E49 RID: 7753
		Sinhala = 134217728L,
		/// <summary>The Thai code chart.</summary>
		// Token: 0x04001E4A RID: 7754
		Thai = 268435456L,
		/// <summary>The Lao code chart.</summary>
		// Token: 0x04001E4B RID: 7755
		Lao = 536870912L,
		/// <summary>The Tibetan code table.</summary>
		// Token: 0x04001E4C RID: 7756
		Tibetan = 1073741824L,
		/// <summary>The code charts that are marked as safe on initialization.</summary>
		// Token: 0x04001E4D RID: 7757
		Default = 127L
	}
}
