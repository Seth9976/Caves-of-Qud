using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200048F RID: 1167
	internal sealed class ParameterTypeInformation : IParameterTypeInformation, IParameterListEntry
	{
		// Token: 0x060060FB RID: 24827 RVA: 0x00175B6B File Offset: 0x00173D6B
		public ParameterTypeInformation(ParameterSymbol underlyingParameter)
		{
			this._underlyingParameter = underlyingParameter;
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060060FC RID: 24828 RVA: 0x00175B7C File Offset: 0x00173D7C
		ImmutableArray<ICustomModifier> IParameterTypeInformation.CustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this._underlyingParameter.TypeWithAnnotations.CustomModifiers);
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060060FD RID: 24829 RVA: 0x00175BA1 File Offset: 0x00173DA1
		bool IParameterTypeInformation.IsByReference
		{
			get
			{
				return this._underlyingParameter.RefKind > RefKind.None;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x00175BB1 File Offset: 0x00173DB1
		ImmutableArray<ICustomModifier> IParameterTypeInformation.RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this._underlyingParameter.RefCustomModifiers);
			}
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x00175BC3 File Offset: 0x00173DC3
		ITypeReference IParameterTypeInformation.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this._underlyingParameter.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06006100 RID: 24832 RVA: 0x00175BF1 File Offset: 0x00173DF1
		ushort IParameterListEntry.Index
		{
			get
			{
				return (ushort)this._underlyingParameter.Ordinal;
			}
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x00175BFF File Offset: 0x00173DFF
		public override string ToString()
		{
			return this._underlyingParameter.ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x00175C11 File Offset: 0x00173E11
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x00175C18 File Offset: 0x00173E18
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0400195B RID: 6491
		private readonly ParameterSymbol _underlyingParameter;
	}
}
