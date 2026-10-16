using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000214 RID: 532
	internal sealed class SynthesizedStateMachineDebuggerHiddenMethod : SynthesizedStateMachineMethod
	{
		// Token: 0x06002619 RID: 9753 RVA: 0x000D0823 File Offset: 0x000CEA23
		public SynthesizedStateMachineDebuggerHiddenMethod(string name, MethodSymbol interfaceMethod, StateMachineTypeSymbol stateMachineType, PropertySymbol associatedProperty, bool hasMethodBodyDependency)
			: base(name, interfaceMethod, stateMachineType, associatedProperty, false, hasMethodBodyDependency)
		{
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000D0834 File Offset: 0x000CEA34
		internal sealed override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerHiddenAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
		}
	}
}
