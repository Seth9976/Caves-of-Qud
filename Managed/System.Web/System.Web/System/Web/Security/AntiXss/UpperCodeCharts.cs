using System;

namespace System.Web.Security.AntiXss
{
	/// <summary>Specifies values for the upper region of the UTF-8 Unicode code charts, from UA8E0 to UFFFD.</summary>
	// Token: 0x020004D6 RID: 1238
	[Flags]
	public enum UpperCodeCharts
	{
		/// <summary>None of the UTF-8 Unicode code charts from the upper region are marked as safe.</summary>
		// Token: 0x04001EB2 RID: 7858
		None = 0,
		/// <summary>The Devanagari Extended code chart.</summary>
		// Token: 0x04001EB3 RID: 7859
		DevanagariExtended = 1,
		/// <summary>The Kayah Li code chart.</summary>
		// Token: 0x04001EB4 RID: 7860
		KayahLi = 2,
		/// <summary>The Rejang code chart.</summary>
		// Token: 0x04001EB5 RID: 7861
		Rejang = 4,
		/// <summary>The Hangul Jamo Extended-A code chart.</summary>
		// Token: 0x04001EB6 RID: 7862
		HangulJamoExtendedA = 8,
		/// <summary>The Javanese code chart.</summary>
		// Token: 0x04001EB7 RID: 7863
		Javanese = 16,
		/// <summary>The Cham code chart.</summary>
		// Token: 0x04001EB8 RID: 7864
		Cham = 32,
		/// <summary>The Myanmar Extended-A code chart.</summary>
		// Token: 0x04001EB9 RID: 7865
		MyanmarExtendedA = 64,
		/// <summary>The Tai Viet code chart.</summary>
		// Token: 0x04001EBA RID: 7866
		TaiViet = 128,
		/// <summary>The Meetei Mayek code chart.</summary>
		// Token: 0x04001EBB RID: 7867
		MeeteiMayek = 256,
		/// <summary>The Hangul Syllables code chart.</summary>
		// Token: 0x04001EBC RID: 7868
		HangulSyllables = 512,
		/// <summary>The Hangul Jamo Extended-B code chart.</summary>
		// Token: 0x04001EBD RID: 7869
		HangulJamoExtendedB = 1024,
		/// <summary>The CJK Compatibility Ideographs code chart.</summary>
		// Token: 0x04001EBE RID: 7870
		CjkCompatibilityIdeographs = 2048,
		/// <summary>The Alphabetic Presentation Forms code chart.</summary>
		// Token: 0x04001EBF RID: 7871
		AlphabeticPresentationForms = 4096,
		/// <summary>The Arabic Presentation Forms-A code chart.</summary>
		// Token: 0x04001EC0 RID: 7872
		ArabicPresentationFormsA = 8192,
		/// <summary>The Variation Selectors code chart.</summary>
		// Token: 0x04001EC1 RID: 7873
		VariationSelectors = 16384,
		/// <summary>The Vertical Forms code chart.</summary>
		// Token: 0x04001EC2 RID: 7874
		VerticalForms = 32768,
		/// <summary>The Combining Half Marks code chart.</summary>
		// Token: 0x04001EC3 RID: 7875
		CombiningHalfMarks = 65536,
		/// <summary>The CJK Compatibility Forms code chart.</summary>
		// Token: 0x04001EC4 RID: 7876
		CjkCompatibilityForms = 131072,
		/// <summary>The Small Form Variants code chart.</summary>
		// Token: 0x04001EC5 RID: 7877
		SmallFormVariants = 262144,
		/// <summary>The Arabic Presentation Forms-B code chart.</summary>
		// Token: 0x04001EC6 RID: 7878
		ArabicPresentationFormsB = 524288,
		/// <summary>The Halfwidth and Fullwidth Forms code chart</summary>
		// Token: 0x04001EC7 RID: 7879
		HalfWidthAndFullWidthForms = 1048576,
		/// <summary>The Specials code chart.</summary>
		// Token: 0x04001EC8 RID: 7880
		Specials = 2097152
	}
}
