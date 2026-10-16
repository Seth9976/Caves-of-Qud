using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200008D RID: 141
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTypeExpression : BoundExpression
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00052E10 File Offset: 0x00051010
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.AliasOpt ?? this.Type;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00052E24 File Offset: 0x00051024
		public override LookupResultKind ResultKind
		{
			get
			{
				ErrorTypeSymbol errorTypeSymbol = this.Type.OriginalDefinition as ErrorTypeSymbol;
				if (errorTypeSymbol != null)
				{
					return errorTypeSymbol.ResultKind;
				}
				return LookupResultKind.Viable;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00052E4E File Offset: 0x0005104E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public BoundTypeExpression([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, AliasSymbol aliasOpt, BoundTypeExpression boundContainingTypeOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> boundDimensionsOpt, TypeWithAnnotations typeWithAnnotations, bool hasErrors = false)
			: this(syntax, aliasOpt, boundContainingTypeOpt, boundDimensionsOpt, typeWithAnnotations, typeWithAnnotations.Type, hasErrors)
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00052E66 File Offset: 0x00051066
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public BoundTypeExpression([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, AliasSymbol aliasOpt, BoundTypeExpression boundContainingTypeOpt, TypeWithAnnotations typeWithAnnotations, bool hasErrors = false)
			: this(syntax, aliasOpt, boundContainingTypeOpt, ImmutableArray<BoundExpression>.Empty, typeWithAnnotations, hasErrors)
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00052E7A File Offset: 0x0005107A
		public BoundTypeExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt, TypeWithAnnotations typeWithAnnotations, bool hasErrors = false)
			: this(syntax, aliasOpt, null, typeWithAnnotations, hasErrors)
		{
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00052E88 File Offset: 0x00051088
		public BoundTypeExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, aliasOpt, null, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), hasErrors)
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00052EB0 File Offset: 0x000510B0
		public BoundTypeExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> dimensionsOpt, TypeWithAnnotations typeWithAnnotations, bool hasErrors = false)
			: this(syntax, aliasOpt, null, dimensionsOpt, typeWithAnnotations, hasErrors)
		{
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00052EC0 File Offset: 0x000510C0
		public BoundTypeExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundTypeExpression boundContainingTypeOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> boundDimensionsOpt, TypeWithAnnotations typeWithAnnotations, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.TypeExpression, syntax, type, hasErrors || boundContainingTypeOpt.HasErrors() || boundDimensionsOpt.HasErrors<BoundExpression>())
		{
			this.AliasOpt = aliasOpt;
			this.BoundContainingTypeOpt = boundContainingTypeOpt;
			this.BoundDimensionsOpt = boundDimensionsOpt;
			this.TypeWithAnnotations = typeWithAnnotations;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00052F0C File Offset: 0x0005110C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public AliasSymbol AliasOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00052F14 File Offset: 0x00051114
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundTypeExpression BoundContainingTypeOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00052F1C File Offset: 0x0005111C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> BoundDimensionsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00052F24 File Offset: 0x00051124
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00052F2C File Offset: 0x0005112C
		public TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x06000B95 RID: 2965 RVA: 0x00052F34 File Offset: 0x00051134
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTypeExpression(this);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00052F40 File Offset: 0x00051140
		public BoundTypeExpression Update([global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundTypeExpression boundContainingTypeOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> boundDimensionsOpt, TypeWithAnnotations typeWithAnnotations, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(aliasOpt, this.AliasOpt) || boundContainingTypeOpt != this.BoundContainingTypeOpt || boundDimensionsOpt != this.BoundDimensionsOpt || new TypeWithAnnotations?(typeWithAnnotations) != new TypeWithAnnotations?(this.TypeWithAnnotations) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundTypeExpression boundTypeExpression = new BoundTypeExpression(this.Syntax, aliasOpt, boundContainingTypeOpt, boundDimensionsOpt, typeWithAnnotations, type, base.HasErrors);
				boundTypeExpression.CopyAttributes(this);
				return boundTypeExpression;
			}
			return this;
		}
	}
}
