using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D6 RID: 470
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDiscardPattern : BoundPattern
	{
		// Token: 0x06002010 RID: 8208 RVA: 0x000A38EE File Offset: 0x000A1AEE
		public BoundDiscardPattern(SyntaxNode syntax, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors)
			: base(BoundKind.DiscardPattern, syntax, inputType, narrowedType, hasErrors)
		{
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000A3900 File Offset: 0x000A1B00
		public BoundDiscardPattern(SyntaxNode syntax, TypeSymbol inputType, TypeSymbol narrowedType)
			: base(BoundKind.DiscardPattern, syntax, inputType, narrowedType)
		{
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000A3910 File Offset: 0x000A1B10
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDiscardPattern(this);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000A3919 File Offset: 0x000A1B19
		public BoundDiscardPattern Update(TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (!TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundDiscardPattern boundDiscardPattern = new BoundDiscardPattern(this.Syntax, inputType, narrowedType, base.HasErrors);
				boundDiscardPattern.CopyAttributes(this);
				return boundDiscardPattern;
			}
			return this;
		}
	}
}
