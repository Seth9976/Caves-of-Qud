using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004A6 RID: 1190
	internal sealed class SynthesizedExplicitImplementationForwardingMethod : SynthesizedImplementationMethod
	{
		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x0600626B RID: 25195 RVA: 0x0017A57E File Offset: 0x0017877E
		internal override bool SynthesizesLoweredBoundBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x0017A584 File Offset: 0x00178784
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
			syntheticBoundNodeFactory.CurrentFunction = this.OriginalDefinition;
			try
			{
				MethodSymbol methodSymbol = (this.IsGenericMethod ? this.ImplementingMethod.Construct(this.TypeParameters.Cast<TypeParameterSymbol, TypeSymbol>()) : this.ImplementingMethod);
				syntheticBoundNodeFactory.CloseMethod(MethodBodySynthesizer.ConstructSingleInvocationMethodBody(syntheticBoundNodeFactory, methodSymbol, false));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x0017A610 File Offset: 0x00178810
		public SynthesizedExplicitImplementationForwardingMethod(MethodSymbol interfaceMethod, MethodSymbol implementingMethod, NamedTypeSymbol implementingType)
			: base(interfaceMethod, implementingType, null, false, null)
		{
			this._implementingMethod = implementingMethod;
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x0600626E RID: 25198 RVA: 0x0017A624 File Offset: 0x00178824
		public MethodSymbol ImplementingMethod
		{
			get
			{
				return this._implementingMethod;
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x0600626F RID: 25199 RVA: 0x0017A62C File Offset: 0x0017882C
		public override MethodKind MethodKind
		{
			get
			{
				if (!this._implementingMethod.IsAccessor())
				{
					return MethodKind.ExplicitInterfaceImplementation;
				}
				return this._implementingMethod.MethodKind;
			}
		}

		// Token: 0x0400197F RID: 6527
		private readonly MethodSymbol _implementingMethod;
	}
}
