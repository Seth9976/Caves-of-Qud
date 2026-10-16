using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000097 RID: 151
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundInterpolatedStringBase : BoundExpression
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00053BE7 File Offset: 0x00051DE7
		public sealed override ConstantValue ConstantValue
		{
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00053BEF File Offset: 0x00051DEF
		protected BoundInterpolatedStringBase(BoundKind kind, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> parts, ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.Parts = parts;
			this.ConstantValueOpt = constantValueOpt;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00053C0C File Offset: 0x00051E0C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Parts
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00053C14 File Offset: 0x00051E14
		public ConstantValue ConstantValueOpt { get; }
	}
}
