using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000213 RID: 531
	internal sealed class SynthesizedStateMachineMoveNextMethod : SynthesizedStateMachineMethod
	{
		// Token: 0x06002617 RID: 9751 RVA: 0x000D074A File Offset: 0x000CE94A
		public SynthesizedStateMachineMoveNextMethod(MethodSymbol interfaceMethod, StateMachineTypeSymbol stateMachineType)
			: base("MoveNext", interfaceMethod, stateMachineType, null, true, true)
		{
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000D075C File Offset: 0x000CE95C
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._attributes.IsDefault)
			{
				ArrayBuilder<CSharpAttributeData> arrayBuilder = null;
				MethodSymbol kickoffMethod = base.StateMachineType.KickoffMethod;
				foreach (CSharpAttributeData csharpAttributeData in kickoffMethod.GetAttributes())
				{
					if (csharpAttributeData.IsTargetAttribute(kickoffMethod, AttributeDescription.DebuggerHiddenAttribute) || csharpAttributeData.IsTargetAttribute(kickoffMethod, AttributeDescription.DebuggerNonUserCodeAttribute) || csharpAttributeData.IsTargetAttribute(kickoffMethod, AttributeDescription.DebuggerStepperBoundaryAttribute) || csharpAttributeData.IsTargetAttribute(kickoffMethod, AttributeDescription.DebuggerStepThroughAttribute))
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<CSharpAttributeData>.GetInstance(4);
						}
						arrayBuilder.Add(csharpAttributeData);
					}
				}
				ImmutableInterlocked.InterlockedCompareExchange<CSharpAttributeData>(ref this._attributes, (arrayBuilder == null) ? ImmutableArray<CSharpAttributeData>.Empty : arrayBuilder.ToImmutableAndFree(), default(ImmutableArray<CSharpAttributeData>));
			}
			return this._attributes;
		}

		// Token: 0x04001117 RID: 4375
		private ImmutableArray<CSharpAttributeData> _attributes;
	}
}
