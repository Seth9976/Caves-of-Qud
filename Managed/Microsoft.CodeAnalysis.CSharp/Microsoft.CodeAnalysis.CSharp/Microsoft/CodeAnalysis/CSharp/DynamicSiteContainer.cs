using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000203 RID: 515
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DynamicSiteContainer : SynthesizedContainer, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x000BA11C File Offset: 0x000B831C
		internal DynamicSiteContainer(string name, MethodSymbol topLevelMethod, MethodSymbol containingMethod)
			: base(name, containingMethod)
		{
			this._topLevelMethod = topLevelMethod;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000BA12D File Offset: 0x000B832D
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._topLevelMethod.ContainingSymbol;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000BA13A File Offset: 0x000B833A
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Class;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x000BA13D File Offset: 0x000B833D
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000BA144 File Offset: 0x000B8344
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000BA147 File Offset: 0x000B8347
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000BA14A File Offset: 0x000B834A
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000BA14D File Offset: 0x000B834D
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000BA150 File Offset: 0x000B8350
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return this._topLevelMethod;
			}
		}

		// Token: 0x040010C6 RID: 4294
		private readonly MethodSymbol _topLevelMethod;
	}
}
