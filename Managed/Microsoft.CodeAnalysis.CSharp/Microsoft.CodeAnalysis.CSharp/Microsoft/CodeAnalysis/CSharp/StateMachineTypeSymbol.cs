using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000211 RID: 529
	internal abstract class StateMachineTypeSymbol : SynthesizedContainer, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x000D05A0 File Offset: 0x000CE7A0
		public StateMachineTypeSymbol(VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, MethodSymbol kickoffMethod, int kickoffMethodOrdinal)
			: base(StateMachineTypeSymbol.MakeName(slotAllocatorOpt, compilationState, kickoffMethod, kickoffMethodOrdinal), kickoffMethod)
		{
			this.KickoffMethod = kickoffMethod;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000D05BA File Offset: 0x000CE7BA
		private static string MakeName(VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, MethodSymbol kickoffMethod, int kickoffMethodOrdinal)
		{
			return ((slotAllocatorOpt != null) ? slotAllocatorOpt.PreviousStateMachineTypeName : null) ?? GeneratedNames.MakeStateMachineTypeName(kickoffMethod.Name, kickoffMethodOrdinal, compilationState.ModuleBuilderOpt.CurrentGenerationOrdinal);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000D05E4 File Offset: 0x000CE7E4
		private static int SequenceNumber(MethodSymbol kickoffMethod)
		{
			int num = 0;
			foreach (Symbol symbol in kickoffMethod.ContainingType.GetMembers(kickoffMethod.Name))
			{
				num++;
				if (kickoffMethod == symbol)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000D062B File Offset: 0x000CE82B
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.KickoffMethod.ContainingType;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000D0638 File Offset: 0x000CE838
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000D063B File Offset: 0x000CE83B
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return this.KickoffMethod;
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000D0644 File Offset: 0x000CE844
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._attributes.IsDefault)
			{
				ArrayBuilder<CSharpAttributeData> arrayBuilder = null;
				NamedTypeSymbol containingType = this.KickoffMethod.ContainingType;
				foreach (CSharpAttributeData csharpAttributeData in containingType.GetAttributes())
				{
					if (csharpAttributeData.IsTargetAttribute(containingType, AttributeDescription.DebuggerNonUserCodeAttribute) || csharpAttributeData.IsTargetAttribute(containingType, AttributeDescription.DebuggerStepThroughAttribute))
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<CSharpAttributeData>.GetInstance(2);
						}
						arrayBuilder.Add(csharpAttributeData);
					}
				}
				ImmutableInterlocked.InterlockedCompareExchange<CSharpAttributeData>(ref this._attributes, (arrayBuilder == null) ? ImmutableArray<CSharpAttributeData>.Empty : arrayBuilder.ToImmutableAndFree(), default(ImmutableArray<CSharpAttributeData>));
			}
			return this._attributes;
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000D06ED File Offset: 0x000CE8ED
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.KickoffMethod.AreLocalsZeroed;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000D06FA File Offset: 0x000CE8FA
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001114 RID: 4372
		private ImmutableArray<CSharpAttributeData> _attributes;

		// Token: 0x04001115 RID: 4373
		public readonly MethodSymbol KickoffMethod;
	}
}
