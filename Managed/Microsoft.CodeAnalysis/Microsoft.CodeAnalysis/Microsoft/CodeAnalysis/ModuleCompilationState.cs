using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200013B RID: 315
	[NullableContext(1)]
	[Nullable(0)]
	internal class ModuleCompilationState<TNamedTypeSymbol, TMethodSymbol> : CommonModuleCompilationState where TNamedTypeSymbol : class, INamedTypeSymbolInternal where TMethodSymbol : class, IMethodSymbolInternal
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0002627C File Offset: 0x0002447C
		internal void SetStateMachineType(TMethodSymbol method, TNamedTypeSymbol stateMachineClass)
		{
			if (this._lazyStateMachineTypes == null)
			{
				Interlocked.CompareExchange<Dictionary<TMethodSymbol, TNamedTypeSymbol>>(ref this._lazyStateMachineTypes, new Dictionary<TMethodSymbol, TNamedTypeSymbol>(), null);
			}
			Dictionary<TMethodSymbol, TNamedTypeSymbol> lazyStateMachineTypes = this._lazyStateMachineTypes;
			lock (lazyStateMachineTypes)
			{
				this._lazyStateMachineTypes.Add(method, stateMachineClass);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000262E0 File Offset: 0x000244E0
		internal bool TryGetStateMachineType(TMethodSymbol method, [Nullable(2)] [NotNullWhen(true)] out TNamedTypeSymbol stateMachineType)
		{
			stateMachineType = default(TNamedTypeSymbol);
			return this._lazyStateMachineTypes != null && this._lazyStateMachineTypes.TryGetValue(method, ref stateMachineType);
		}

		// Token: 0x04000368 RID: 872
		[Nullable(new byte[] { 2, 1, 1 })]
		private Dictionary<TMethodSymbol, TNamedTypeSymbol> _lazyStateMachineTypes;
	}
}
