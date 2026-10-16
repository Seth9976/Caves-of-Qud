using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D7 RID: 471
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDeclarationPattern : BoundPattern
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x000A3958 File Offset: 0x000A1B58
		public BoundDeclarationPattern(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol variable, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression variableAccess, BoundTypeExpression declaredType, bool isVar, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.DeclarationPattern, syntax, inputType, narrowedType, hasErrors || variableAccess.HasErrors() || declaredType.HasErrors())
		{
			this.Variable = variable;
			this.VariableAccess = variableAccess;
			this.DeclaredType = declaredType;
			this.IsVar = isVar;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x000A39A9 File Offset: 0x000A1BA9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public Symbol Variable
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x000A39B1 File Offset: 0x000A1BB1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression VariableAccess
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000A39B9 File Offset: 0x000A1BB9
		public BoundTypeExpression DeclaredType { get; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x000A39C1 File Offset: 0x000A1BC1
		public bool IsVar { get; }

		// Token: 0x06002019 RID: 8217 RVA: 0x000A39C9 File Offset: 0x000A1BC9
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDeclarationPattern(this);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000A39D4 File Offset: 0x000A1BD4
		public BoundDeclarationPattern Update([global::System.Runtime.CompilerServices.Nullable(2)] Symbol variable, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression variableAccess, BoundTypeExpression declaredType, bool isVar, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(variable, this.Variable) || variableAccess != this.VariableAccess || declaredType != this.DeclaredType || isVar != this.IsVar || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundDeclarationPattern boundDeclarationPattern = new BoundDeclarationPattern(this.Syntax, variable, variableAccess, declaredType, isVar, inputType, narrowedType, base.HasErrors);
				boundDeclarationPattern.CopyAttributes(this);
				return boundDeclarationPattern;
			}
			return this;
		}
	}
}
