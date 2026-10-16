using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001FD RID: 509
	internal sealed class IteratorConstructor : SynthesizedInstanceConstructor, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x06002356 RID: 9046 RVA: 0x000B8BF0 File Offset: 0x000B6DF0
		internal IteratorConstructor(StateMachineTypeSymbol container)
			: base(container)
		{
			NamedTypeSymbol specialType = container.DeclaringCompilation.GetSpecialType(SpecialType.System_Int32);
			this._parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, GeneratedNames.MakeStateMachineStateFieldName(), default(ImmutableArray<CustomModifier>), null));
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000B8C44 File Offset: 0x000B6E44
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerHiddenAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000B8C81 File Offset: 0x000B6E81
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x000B8C89 File Offset: 0x000B6E89
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Public;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000B8C8C File Offset: 0x000B6E8C
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return ((ISynthesizedMethodBodyImplementationSymbol)this.ContainingSymbol).Method;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000B8C9E File Offset: 0x000B6E9E
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040010B5 RID: 4277
		private readonly ImmutableArray<ParameterSymbol> _parameters;
	}
}
