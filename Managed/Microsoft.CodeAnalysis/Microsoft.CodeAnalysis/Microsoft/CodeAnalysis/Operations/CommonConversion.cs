using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000460 RID: 1120
	[NullableContext(2)]
	[Nullable(0)]
	public struct CommonConversion
	{
		// Token: 0x06002C1A RID: 11290 RVA: 0x0007CC88 File Offset: 0x0007AE88
		internal CommonConversion(bool exists, bool isIdentity, bool isNumeric, bool isReference, bool isImplicit, bool isNullable, IMethodSymbol methodSymbol)
		{
			this._conversionKind = (exists ? CommonConversion.ConversionKind.Exists : CommonConversion.ConversionKind.None) | (isIdentity ? CommonConversion.ConversionKind.IsIdentity : CommonConversion.ConversionKind.None) | (isNumeric ? CommonConversion.ConversionKind.IsNumeric : CommonConversion.ConversionKind.None) | (isReference ? CommonConversion.ConversionKind.IsReference : CommonConversion.ConversionKind.None) | (isImplicit ? CommonConversion.ConversionKind.IsImplicit : CommonConversion.ConversionKind.None) | (isNullable ? CommonConversion.ConversionKind.IsNullable : CommonConversion.ConversionKind.None);
			this.MethodSymbol = methodSymbol;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x0007CCD7 File Offset: 0x0007AED7
		public bool Exists
		{
			get
			{
				return (this._conversionKind & CommonConversion.ConversionKind.Exists) == CommonConversion.ConversionKind.Exists;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x0007CCE4 File Offset: 0x0007AEE4
		public bool IsIdentity
		{
			get
			{
				return (this._conversionKind & CommonConversion.ConversionKind.IsIdentity) == CommonConversion.ConversionKind.IsIdentity;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x0007CCF1 File Offset: 0x0007AEF1
		public bool IsNullable
		{
			get
			{
				return (this._conversionKind & CommonConversion.ConversionKind.IsNullable) == CommonConversion.ConversionKind.IsNullable;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x0007CD00 File Offset: 0x0007AF00
		public bool IsNumeric
		{
			get
			{
				return (this._conversionKind & CommonConversion.ConversionKind.IsNumeric) == CommonConversion.ConversionKind.IsNumeric;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0007CD0D File Offset: 0x0007AF0D
		public bool IsReference
		{
			get
			{
				return (this._conversionKind & CommonConversion.ConversionKind.IsReference) == CommonConversion.ConversionKind.IsReference;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x0007CD1A File Offset: 0x0007AF1A
		public bool IsImplicit
		{
			get
			{
				return (this._conversionKind & CommonConversion.ConversionKind.IsImplicit) == CommonConversion.ConversionKind.IsImplicit;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x0007CD29 File Offset: 0x0007AF29
		[MemberNotNullWhen(true, "MethodSymbol")]
		public bool IsUserDefined
		{
			[MemberNotNullWhen(true, "MethodSymbol")]
			get
			{
				return this.MethodSymbol != null;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x0007CD34 File Offset: 0x0007AF34
		public readonly IMethodSymbol MethodSymbol { get; }

		// Token: 0x0400116D RID: 4461
		private readonly CommonConversion.ConversionKind _conversionKind;

		// Token: 0x02000725 RID: 1829
		[NullableContext(0)]
		[Flags]
		private enum ConversionKind
		{
			// Token: 0x04001C76 RID: 7286
			None = 0,
			// Token: 0x04001C77 RID: 7287
			Exists = 1,
			// Token: 0x04001C78 RID: 7288
			IsIdentity = 2,
			// Token: 0x04001C79 RID: 7289
			IsNumeric = 4,
			// Token: 0x04001C7A RID: 7290
			IsReference = 8,
			// Token: 0x04001C7B RID: 7291
			IsImplicit = 16,
			// Token: 0x04001C7C RID: 7292
			IsNullable = 32
		}
	}
}
