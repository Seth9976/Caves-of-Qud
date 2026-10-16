using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers that indicate special features of a language.</summary>
	// Token: 0x02000357 RID: 855
	[Flags]
	public enum LanguageOptions
	{
		/// <summary>The language has default characteristics.</summary>
		// Token: 0x04000E8E RID: 3726
		None = 0,
		/// <summary>The language is case-insensitive.</summary>
		// Token: 0x04000E8F RID: 3727
		CaseInsensitive = 1
	}
}
