using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000232 RID: 562
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ObsoleteAttributeData
	{
		// Token: 0x060015EF RID: 5615 RVA: 0x0004531D File Offset: 0x0004351D
		[NullableContext(2)]
		public ObsoleteAttributeData(ObsoleteAttributeKind kind, string message, bool isError, string diagnosticId, string urlFormat)
		{
			this.Kind = kind;
			this.Message = message;
			this.IsError = isError;
			this.DiagnosticId = diagnosticId;
			this.UrlFormat = urlFormat;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0004534A File Offset: 0x0004354A
		internal bool IsUninitialized
		{
			get
			{
				return this == ObsoleteAttributeData.Uninitialized;
			}
		}

		// Token: 0x0400090A RID: 2314
		public static readonly ObsoleteAttributeData Uninitialized = new ObsoleteAttributeData(ObsoleteAttributeKind.Uninitialized, null, false, null, null);

		// Token: 0x0400090B RID: 2315
		public static readonly ObsoleteAttributeData Experimental = new ObsoleteAttributeData(ObsoleteAttributeKind.Experimental, null, false, null, null);

		// Token: 0x0400090C RID: 2316
		public const string DiagnosticIdPropertyName = "DiagnosticId";

		// Token: 0x0400090D RID: 2317
		public const string UrlFormatPropertyName = "UrlFormat";

		// Token: 0x0400090E RID: 2318
		public readonly ObsoleteAttributeKind Kind;

		// Token: 0x0400090F RID: 2319
		public readonly bool IsError;

		// Token: 0x04000910 RID: 2320
		[Nullable(2)]
		public readonly string Message;

		// Token: 0x04000911 RID: 2321
		[Nullable(2)]
		public readonly string DiagnosticId;

		// Token: 0x04000912 RID: 2322
		[Nullable(2)]
		public readonly string UrlFormat;
	}
}
