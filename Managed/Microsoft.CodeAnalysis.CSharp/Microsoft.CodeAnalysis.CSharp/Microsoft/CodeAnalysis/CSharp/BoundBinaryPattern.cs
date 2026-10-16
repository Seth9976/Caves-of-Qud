using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001DC RID: 476
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBinaryPattern : BoundPattern
	{
		// Token: 0x06002035 RID: 8245 RVA: 0x000A3DC1 File Offset: 0x000A1FC1
		public BoundBinaryPattern(SyntaxNode syntax, bool disjunction, BoundPattern left, BoundPattern right, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.BinaryPattern, syntax, inputType, narrowedType, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.Disjunction = disjunction;
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000A3DFF File Offset: 0x000A1FFF
		public bool Disjunction { get; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x000A3E07 File Offset: 0x000A2007
		public BoundPattern Left { get; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x000A3E0F File Offset: 0x000A200F
		public BoundPattern Right { get; }

		// Token: 0x06002039 RID: 8249 RVA: 0x000A3E17 File Offset: 0x000A2017
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBinaryPattern(this);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000A3E20 File Offset: 0x000A2020
		public BoundBinaryPattern Update(bool disjunction, BoundPattern left, BoundPattern right, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (disjunction != this.Disjunction || left != this.Left || right != this.Right || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundBinaryPattern boundBinaryPattern = new BoundBinaryPattern(this.Syntax, disjunction, left, right, inputType, narrowedType, base.HasErrors);
				boundBinaryPattern.CopyAttributes(this);
				return boundBinaryPattern;
			}
			return this;
		}
	}
}
