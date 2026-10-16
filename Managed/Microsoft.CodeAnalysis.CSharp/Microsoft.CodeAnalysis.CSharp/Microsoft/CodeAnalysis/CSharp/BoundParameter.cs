using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000095 RID: 149
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundParameter : BoundExpression
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00053991 File Offset: 0x00051B91
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.ParameterSymbol;
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00053999 File Offset: 0x00051B99
		public BoundParameter(SyntaxNode syntax, ParameterSymbol parameterSymbol, bool hasErrors = false)
			: this(syntax, parameterSymbol, parameterSymbol.Type, hasErrors)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000539AA File Offset: 0x00051BAA
		public BoundParameter(SyntaxNode syntax, ParameterSymbol parameterSymbol)
			: this(syntax, parameterSymbol, parameterSymbol.Type)
		{
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000539BA File Offset: 0x00051BBA
		public BoundParameter(SyntaxNode syntax, ParameterSymbol parameterSymbol, TypeSymbol type, bool hasErrors)
			: base(BoundKind.Parameter, syntax, type, hasErrors)
		{
			this.ParameterSymbol = parameterSymbol;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000539CF File Offset: 0x00051BCF
		public BoundParameter(SyntaxNode syntax, ParameterSymbol parameterSymbol, TypeSymbol type)
			: base(BoundKind.Parameter, syntax, type)
		{
			this.ParameterSymbol = parameterSymbol;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000539E2 File Offset: 0x00051BE2
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000539EA File Offset: 0x00051BEA
		public ParameterSymbol ParameterSymbol { get; }

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000539F2 File Offset: 0x00051BF2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000539FB File Offset: 0x00051BFB
		public BoundParameter Update(ParameterSymbol parameterSymbol, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(parameterSymbol, this.ParameterSymbol) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundParameter boundParameter = new BoundParameter(this.Syntax, parameterSymbol, type, base.HasErrors);
				boundParameter.CopyAttributes(this);
				return boundParameter;
			}
			return this;
		}
	}
}
