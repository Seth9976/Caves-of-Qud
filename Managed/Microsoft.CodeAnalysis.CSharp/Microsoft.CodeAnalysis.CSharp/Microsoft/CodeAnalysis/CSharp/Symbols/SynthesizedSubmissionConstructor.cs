using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200059E RID: 1438
	internal sealed class SynthesizedSubmissionConstructor : SynthesizedInstanceConstructor
	{
		// Token: 0x0600786B RID: 30827 RVA: 0x001B91A0 File Offset: 0x001B73A0
		internal SynthesizedSubmissionConstructor(NamedTypeSymbol containingType, BindingDiagnosticBag diagnostics)
			: base(containingType)
		{
			CSharpCompilation declaringCompilation = containingType.DeclaringCompilation;
			ArrayTypeSymbol arrayTypeSymbol = declaringCompilation.CreateArrayTypeSymbol(declaringCompilation.GetSpecialType(SpecialType.System_Object), 1, NullableAnnotation.Oblivious);
			UseSiteInfo<AssemblySymbol> useSiteInfo = arrayTypeSymbol.GetUseSiteInfo();
			diagnostics.Add(useSiteInfo, NoLocation.Singleton);
			this._parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(arrayTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "submissionArray", default(ImmutableArray<CustomModifier>), null));
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x0600786C RID: 30828 RVA: 0x001B920F File Offset: 0x001B740F
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x04001CFC RID: 7420
		private readonly ImmutableArray<ParameterSymbol> _parameters;
	}
}
