using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000181 RID: 385
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFieldInfo : BoundExpression
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x000A089B File Offset: 0x0009EA9B
		public BoundFieldInfo(SyntaxNode syntax, FieldSymbol field, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getFieldFromHandle, TypeSymbol type, bool hasErrors)
			: base(BoundKind.FieldInfo, syntax, type, hasErrors)
		{
			this.Field = field;
			this.GetFieldFromHandle = getFieldFromHandle;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000A08B8 File Offset: 0x0009EAB8
		public BoundFieldInfo(SyntaxNode syntax, FieldSymbol field, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getFieldFromHandle, TypeSymbol type)
			: base(BoundKind.FieldInfo, syntax, type)
		{
			this.Field = field;
			this.GetFieldFromHandle = getFieldFromHandle;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x000A08D3 File Offset: 0x0009EAD3
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000A08DB File Offset: 0x0009EADB
		public FieldSymbol Field { get; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x000A08E3 File Offset: 0x0009EAE3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol GetFieldFromHandle
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000A08EB File Offset: 0x0009EAEB
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFieldInfo(this);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000A08F4 File Offset: 0x0009EAF4
		public BoundFieldInfo Update(FieldSymbol field, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getFieldFromHandle, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(field, this.Field) || !SymbolEqualityComparer.ConsiderEverything.Equals(getFieldFromHandle, this.GetFieldFromHandle) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundFieldInfo boundFieldInfo = new BoundFieldInfo(this.Syntax, field, getFieldFromHandle, type, base.HasErrors);
				boundFieldInfo.CopyAttributes(this);
				return boundFieldInfo;
			}
			return this;
		}
	}
}
