using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000085 RID: 133
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagTemp : BoundNode
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00051F51 File Offset: 0x00050151
		public bool IsOriginalInput
		{
			get
			{
				return this.Source == null;
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00051F5C File Offset: 0x0005015C
		public static BoundDagTemp ForOriginalInput(SyntaxNode syntax, TypeSymbol type)
		{
			return new BoundDagTemp(syntax, type, null, 0, false);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00051F68 File Offset: 0x00050168
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			BoundDagTemp boundDagTemp = obj as BoundDagTemp;
			return boundDagTemp != null && this.Equals(boundDagTemp);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00051F88 File Offset: 0x00050188
		public bool Equals(BoundDagTemp other)
		{
			return other != null && this.Type.Equals(other.Type, TypeCompareKind.AllIgnoreOptions) && object.Equals(this.Source, other.Source) && this.Index == other.Index;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00051FC5 File Offset: 0x000501C5
		public override int GetHashCode()
		{
			int hashCode = this.Type.GetHashCode();
			BoundDagEvaluation source = this.Source;
			return Hash.Combine(hashCode, Hash.Combine((source != null) ? source.GetHashCode() : 0, this.Index));
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00051FF4 File Offset: 0x000501F4
		public BoundDagTemp(SyntaxNode syntax, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundDagEvaluation source)
			: this(syntax, type, source, 0, false)
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00052001 File Offset: 0x00050201
		public static BoundDagTemp ForOriginalInput(BoundExpression expr)
		{
			return new BoundDagTemp(expr.Syntax, expr.Type, null);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00052015 File Offset: 0x00050215
		public BoundDagTemp(SyntaxNode syntax, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundDagEvaluation source, int index, bool hasErrors = false)
			: base(BoundKind.DagTemp, syntax, hasErrors || source.HasErrors())
		{
			this.Type = type;
			this.Source = source;
			this.Index = index;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00052043 File Offset: 0x00050243
		public TypeSymbol Type { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0005204B File Offset: 0x0005024B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundDagEvaluation Source
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00052053 File Offset: 0x00050253
		public int Index { get; }

		// Token: 0x06000B38 RID: 2872 RVA: 0x0005205B File Offset: 0x0005025B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagTemp(this);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00052064 File Offset: 0x00050264
		public BoundDagTemp Update(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundDagEvaluation source, int index)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything) || source != this.Source || index != this.Index)
			{
				BoundDagTemp boundDagTemp = new BoundDagTemp(this.Syntax, type, source, index, base.HasErrors);
				boundDagTemp.CopyAttributes(this);
				return boundDagTemp;
			}
			return this;
		}
	}
}
