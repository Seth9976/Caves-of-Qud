using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200003E RID: 62
	internal struct MethodGroupResolution
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x0003BB4B File Offset: 0x00039D4B
		public MethodGroupResolution(MethodGroup methodGroup, ImmutableBindingDiagnostic<AssemblySymbol> diagnostics)
		{
			this = new MethodGroupResolution(methodGroup, null, null, null, methodGroup.ResultKind, diagnostics);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0003BB5E File Offset: 0x00039D5E
		public MethodGroupResolution(Symbol otherSymbol, LookupResultKind resultKind, ImmutableBindingDiagnostic<AssemblySymbol> diagnostics)
		{
			this = new MethodGroupResolution(null, otherSymbol, null, null, resultKind, diagnostics);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0003BB6C File Offset: 0x00039D6C
		public MethodGroupResolution(MethodGroup methodGroup, Symbol otherSymbol, OverloadResolutionResult<MethodSymbol> overloadResolutionResult, AnalyzedArguments analyzedArguments, LookupResultKind resultKind, ImmutableBindingDiagnostic<AssemblySymbol> diagnostics)
		{
			this.MethodGroup = methodGroup;
			this.OtherSymbol = otherSymbol;
			this.OverloadResolutionResult = overloadResolutionResult;
			this.AnalyzedArguments = analyzedArguments;
			this.ResultKind = resultKind;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0003BB9B File Offset: 0x00039D9B
		public bool IsEmpty
		{
			get
			{
				return this.MethodGroup == null && this.OtherSymbol == null;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		public bool HasAnyErrors
		{
			get
			{
				return this.Diagnostics.Diagnostics.HasAnyErrors<Diagnostic>();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0003BBC2 File Offset: 0x00039DC2
		public bool HasAnyApplicableMethod
		{
			get
			{
				return this.MethodGroup != null && this.ResultKind == LookupResultKind.Viable && (this.OverloadResolutionResult == null || this.OverloadResolutionResult.HasAnyApplicableMember);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0003BBED File Offset: 0x00039DED
		public bool IsExtensionMethodGroup
		{
			get
			{
				return this.MethodGroup != null && this.MethodGroup.IsExtensionMethodGroup;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0003BC04 File Offset: 0x00039E04
		public bool IsLocalFunctionInvocation
		{
			get
			{
				MethodGroup methodGroup = this.MethodGroup;
				return methodGroup != null && methodGroup.Methods.Count == 1 && this.MethodGroup.Methods[0].MethodKind == MethodKind.LocalFunction;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0003BC3E File Offset: 0x00039E3E
		public void Free()
		{
			AnalyzedArguments analyzedArguments = this.AnalyzedArguments;
			if (analyzedArguments != null)
			{
				analyzedArguments.Free();
			}
			MethodGroup methodGroup = this.MethodGroup;
			if (methodGroup != null)
			{
				methodGroup.Free();
			}
			OverloadResolutionResult<MethodSymbol> overloadResolutionResult = this.OverloadResolutionResult;
			if (overloadResolutionResult == null)
			{
				return;
			}
			overloadResolutionResult.Free();
		}

		// Token: 0x040000C7 RID: 199
		public readonly MethodGroup MethodGroup;

		// Token: 0x040000C8 RID: 200
		public readonly Symbol OtherSymbol;

		// Token: 0x040000C9 RID: 201
		public readonly OverloadResolutionResult<MethodSymbol> OverloadResolutionResult;

		// Token: 0x040000CA RID: 202
		public readonly AnalyzedArguments AnalyzedArguments;

		// Token: 0x040000CB RID: 203
		public readonly ImmutableBindingDiagnostic<AssemblySymbol> Diagnostics;

		// Token: 0x040000CC RID: 204
		public readonly LookupResultKind ResultKind;
	}
}
