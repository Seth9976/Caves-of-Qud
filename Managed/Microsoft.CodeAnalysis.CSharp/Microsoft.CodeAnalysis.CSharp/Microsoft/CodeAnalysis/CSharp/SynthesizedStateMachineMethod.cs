using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000212 RID: 530
	internal abstract class SynthesizedStateMachineMethod : SynthesizedImplementationMethod, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x000D06FD File Offset: 0x000CE8FD
		protected SynthesizedStateMachineMethod(string name, MethodSymbol interfaceMethod, StateMachineTypeSymbol stateMachineType, PropertySymbol associatedProperty, bool generateDebugInfo, bool hasMethodBodyDependency)
			: base(interfaceMethod, stateMachineType, name, generateDebugInfo, associatedProperty)
		{
			this._hasMethodBodyDependency = hasMethodBodyDependency;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000D0714 File Offset: 0x000CE914
		public StateMachineTypeSymbol StateMachineType
		{
			get
			{
				return (StateMachineTypeSymbol)this.ContainingSymbol;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000D0721 File Offset: 0x000CE921
		public bool HasMethodBodyDependency
		{
			get
			{
				return this._hasMethodBodyDependency;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000D0729 File Offset: 0x000CE929
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return this.StateMachineType.KickoffMethod;
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000D0736 File Offset: 0x000CE936
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return this.StateMachineType.KickoffMethod.CalculateLocalSyntaxOffset(localPosition, localTree);
		}

		// Token: 0x04001116 RID: 4374
		private readonly bool _hasMethodBodyDependency;
	}
}
