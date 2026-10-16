using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E8 RID: 488
	internal sealed class AsyncConstructor : SynthesizedInstanceConstructor, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x0600215E RID: 8542 RVA: 0x000ACFD3 File Offset: 0x000AB1D3
		internal AsyncConstructor(AsyncStateMachine stateMachineType)
			: base(stateMachineType)
		{
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x000ACFDC File Offset: 0x000AB1DC
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return ((ISynthesizedMethodBodyImplementationSymbol)this.ContainingSymbol).Method;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x000ACFEE File Offset: 0x000AB1EE
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return false;
			}
		}
	}
}
