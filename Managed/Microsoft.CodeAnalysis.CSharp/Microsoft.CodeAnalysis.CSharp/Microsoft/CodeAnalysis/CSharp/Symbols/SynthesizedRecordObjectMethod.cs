using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200057A RID: 1402
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SynthesizedRecordObjectMethod : SynthesizedRecordOrdinaryMethod
	{
		// Token: 0x06007568 RID: 30056 RVA: 0x001B5B40 File Offset: 0x001B3D40
		protected SynthesizedRecordObjectMethod(SourceMemberContainerTypeSymbol containingType, string name, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, name, true, memberOffset, diagnostics)
		{
		}

		// Token: 0x06007569 RID: 30057 RVA: 0x001B5B4E File Offset: 0x001B3D4E
		protected sealed override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			return DeclarationModifiers.Public | DeclarationModifiers.Override;
		}

		// Token: 0x0600756A RID: 30058 RVA: 0x001B5B55 File Offset: 0x001B3D55
		protected sealed override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			base.MethodChecks(diagnostics);
			SynthesizedRecordObjectMethod.VerifyOverridesMethodFromObject(this, this.OverriddenSpecialMember, diagnostics);
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x0600756B RID: 30059
		protected abstract SpecialMember OverriddenSpecialMember { get; }

		// Token: 0x0600756C RID: 30060 RVA: 0x001B5B6C File Offset: 0x001B3D6C
		internal static bool VerifyOverridesMethodFromObject(MethodSymbol overriding, SpecialMember overriddenSpecialMember, BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			if (!overriding.IsOverride)
			{
				flag = true;
			}
			else
			{
				MethodSymbol overriddenMethod = overriding.OverriddenMethod;
				MethodSymbol methodSymbol = ((overriddenMethod != null) ? overriddenMethod.OriginalDefinition : null);
				if (methodSymbol != null)
				{
					SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = methodSymbol.ContainingType as SourceMemberContainerTypeSymbol;
					if (sourceMemberContainerTypeSymbol == null || !sourceMemberContainerTypeSymbol.IsRecord || !(methodSymbol.ContainingModule == overriding.ContainingModule))
					{
						MethodSymbol leastOverriddenMethod = overriding.GetLeastOverriddenMethod(null);
						flag = leastOverriddenMethod != overriding.ContainingAssembly.GetSpecialTypeMember(overriddenSpecialMember) && leastOverriddenMethod.ReturnType.Equals(overriding.ReturnType, TypeCompareKind.AllIgnoreOptions);
					}
				}
			}
			if (flag)
			{
				diagnostics.Add(ErrorCode.ERR_DoesNotOverrideMethodFromObject, overriding.Locations[0], new object[] { overriding });
			}
			return flag;
		}
	}
}
