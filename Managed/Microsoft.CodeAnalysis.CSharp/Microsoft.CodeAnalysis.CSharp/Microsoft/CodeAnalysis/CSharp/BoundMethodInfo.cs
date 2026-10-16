using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000180 RID: 384
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundMethodInfo : BoundExpression
	{
		// Token: 0x06001E42 RID: 7746 RVA: 0x000A07E2 File Offset: 0x0009E9E2
		public BoundMethodInfo(SyntaxNode syntax, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getMethodFromHandle, TypeSymbol type, bool hasErrors)
			: base(BoundKind.MethodInfo, syntax, type, hasErrors)
		{
			this.Method = method;
			this.GetMethodFromHandle = getMethodFromHandle;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000A07FF File Offset: 0x0009E9FF
		public BoundMethodInfo(SyntaxNode syntax, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getMethodFromHandle, TypeSymbol type)
			: base(BoundKind.MethodInfo, syntax, type)
		{
			this.Method = method;
			this.GetMethodFromHandle = getMethodFromHandle;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x000A081A File Offset: 0x0009EA1A
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000A0822 File Offset: 0x0009EA22
		public MethodSymbol Method { get; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000A082A File Offset: 0x0009EA2A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol GetMethodFromHandle
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000A0832 File Offset: 0x0009EA32
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitMethodInfo(this);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000A083C File Offset: 0x0009EA3C
		public BoundMethodInfo Update(MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getMethodFromHandle, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(method, this.Method) || !SymbolEqualityComparer.ConsiderEverything.Equals(getMethodFromHandle, this.GetMethodFromHandle) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundMethodInfo boundMethodInfo = new BoundMethodInfo(this.Syntax, method, getMethodFromHandle, type, base.HasErrors);
				boundMethodInfo.CopyAttributes(this);
				return boundMethodInfo;
			}
			return this;
		}
	}
}
