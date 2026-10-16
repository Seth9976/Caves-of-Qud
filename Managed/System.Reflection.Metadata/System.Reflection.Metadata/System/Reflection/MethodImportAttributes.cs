using System;

namespace System.Reflection
{
	/// <summary>Specifies flags for the unmanaged method import attributes.</summary>
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum MethodImportAttributes : short
	{
		/// <summary>Specifies default method import attributes.</summary>
		// Token: 0x04000022 RID: 34
		None = 0,
		/// <summary>Specifies that the Common Language Runtime should not try an entry-point names with charset-specific suffixes when searching for the imported method.</summary>
		// Token: 0x04000023 RID: 35
		ExactSpelling = 1,
		/// <summary>Specifies that the best-fit mapping behavior when converting Unicode characters to ANSI characters is disabled.</summary>
		// Token: 0x04000024 RID: 36
		BestFitMappingDisable = 32,
		/// <summary>Specifies that the best-fit mapping behavior when converting Unicode characters to ANSI characters is enabled.</summary>
		// Token: 0x04000025 RID: 37
		BestFitMappingEnable = 16,
		/// <summary>Specifies whether the best-fit mapping behavior when converting Unicode characters to ANSI characters is enabled or disabled.</summary>
		// Token: 0x04000026 RID: 38
		BestFitMappingMask = 48,
		/// <summary>Specifies that strings are marshalled as multiple-byte character strings: the system default Windows (ANSI) code page on Windows, and UTF-8 on Unix.</summary>
		// Token: 0x04000027 RID: 39
		CharSetAnsi = 2,
		/// <summary>Specifies that strings are marshalled as Unicode 2-byte character strings.</summary>
		// Token: 0x04000028 RID: 40
		CharSetUnicode = 4,
		/// <summary>Specifies that the character set is chosen automatically. See Charsets and marshaling for details.</summary>
		// Token: 0x04000029 RID: 41
		CharSetAuto = 6,
		/// <summary>Specifies the character set used for string marshalling.</summary>
		// Token: 0x0400002A RID: 42
		CharSetMask = 6,
		/// <summary>Specifies that an exception should be thrown when an unmappable Unicode character is converted to an ANSI character.</summary>
		// Token: 0x0400002B RID: 43
		ThrowOnUnmappableCharEnable = 4096,
		/// <summary>Specifies that an exception should not be thrown when an unmappable Unicode character is converted to an ANSI character.</summary>
		// Token: 0x0400002C RID: 44
		ThrowOnUnmappableCharDisable = 8192,
		/// <summary>Specifies whether an exception should be thrown when an unmappable Unicode character is converted to an ANSI character.</summary>
		// Token: 0x0400002D RID: 45
		ThrowOnUnmappableCharMask = 12288,
		/// <summary>Specifies that the imported method calls the SetLastError Windows API function before returning.</summary>
		// Token: 0x0400002E RID: 46
		SetLastError = 64,
		/// <summary>Specifies that the default platform calling convention is used.</summary>
		// Token: 0x0400002F RID: 47
		CallingConventionWinApi = 256,
		/// <summary>Specifies that the calling convention is CDecl.</summary>
		// Token: 0x04000030 RID: 48
		CallingConventionCDecl = 512,
		/// <summary>Specifies that the calling convention is StdCall.</summary>
		// Token: 0x04000031 RID: 49
		CallingConventionStdCall = 768,
		/// <summary>Specifies that the calling convention is ThisCall.</summary>
		// Token: 0x04000032 RID: 50
		CallingConventionThisCall = 1024,
		/// <summary>Specifies that the calling convention is FastCall.</summary>
		// Token: 0x04000033 RID: 51
		CallingConventionFastCall = 1280,
		/// <summary>Specifies the calling convention.</summary>
		// Token: 0x04000034 RID: 52
		CallingConventionMask = 1792
	}
}
