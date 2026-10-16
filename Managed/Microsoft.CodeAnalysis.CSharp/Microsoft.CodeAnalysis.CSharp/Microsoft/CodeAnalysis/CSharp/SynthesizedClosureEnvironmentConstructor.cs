using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F5 RID: 501
	internal sealed class SynthesizedClosureEnvironmentConstructor : SynthesizedInstanceConstructor, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x06002238 RID: 8760 RVA: 0x000B47FC File Offset: 0x000B29FC
		internal SynthesizedClosureEnvironmentConstructor(SynthesizedClosureEnvironment frame)
			: base(frame)
		{
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000B4805 File Offset: 0x000B2A05
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return ((ISynthesizedMethodBodyImplementationSymbol)this.ContainingSymbol).Method;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000B4817 File Offset: 0x000B2A17
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return false;
			}
		}
	}
}
