using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000543 RID: 1347
	internal abstract class SourceMethodSymbol : MethodSymbol
	{
		// Token: 0x06007183 RID: 29059
		public abstract ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes();

		// Token: 0x06007184 RID: 29060
		public abstract ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds();

		// Token: 0x06007185 RID: 29061 RVA: 0x001A54C0 File Offset: 0x001A36C0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected static void ReportBadRefToken(TypeSyntax returnTypeSyntax, BindingDiagnosticBag diagnostics)
		{
			if (!returnTypeSyntax.HasErrors)
			{
				SyntaxToken firstToken = returnTypeSyntax.GetFirstToken(false, false, false, false);
				diagnostics.Add(ErrorCode.ERR_UnexpectedToken, firstToken.GetLocation(), new object[] { firstToken.ToString() });
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06007186 RID: 29062 RVA: 0x001A550C File Offset: 0x001A370C
		protected bool AreContainingSymbolLocalsZeroed
		{
			get
			{
				SourceMethodSymbol sourceMethodSymbol = this.ContainingSymbol as SourceMethodSymbol;
				if (sourceMethodSymbol != null)
				{
					return sourceMethodSymbol.AreLocalsZeroed;
				}
				SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = this.ContainingType as SourceMemberContainerTypeSymbol;
				return sourceMemberContainerTypeSymbol == null || sourceMemberContainerTypeSymbol.AreLocalsZeroed;
			}
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x001A5548 File Offset: 0x001A3748
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal void ReportAsyncParameterErrors(BindingDiagnosticBag diagnostics, Location location)
		{
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				if (parameterSymbol.RefKind != RefKind.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadAsyncArgType, SourceMethodSymbol.<ReportAsyncParameterErrors>g__getLocation|5_0(parameterSymbol, location));
				}
				else if (parameterSymbol.Type.IsUnsafe())
				{
					diagnostics.Add(ErrorCode.ERR_UnsafeAsyncArgType, SourceMethodSymbol.<ReportAsyncParameterErrors>g__getLocation|5_0(parameterSymbol, location));
				}
				else if (parameterSymbol.Type.IsRestrictedType(false))
				{
					diagnostics.Add(ErrorCode.ERR_BadSpecialByRefLocal, SourceMethodSymbol.<ReportAsyncParameterErrors>g__getLocation|5_0(parameterSymbol, location), new object[] { parameterSymbol.Type });
				}
			}
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x001A55EE File Offset: 0x001A37EE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static Location <ReportAsyncParameterErrors>g__getLocation|5_0(ParameterSymbol parameter, Location location)
		{
			return parameter.Locations.FirstOrDefault<Location>() ?? location;
		}
	}
}
