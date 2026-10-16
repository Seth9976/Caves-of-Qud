using System;
using System.Collections.Generic;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200049A RID: 1178
	internal abstract class TypeMemberReference : ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x060061B4 RID: 25012
		protected abstract Symbol UnderlyingSymbol { get; }

		// Token: 0x060061B5 RID: 25013 RVA: 0x0017827F File Offset: 0x0017647F
		public virtual ITypeReference GetContainingType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingSymbol.ContainingType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x001782AF File Offset: 0x001764AF
		string INamedEntity.Name
		{
			get
			{
				return this.UnderlyingSymbol.MetadataName;
			}
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x001782BC File Offset: 0x001764BC
		public override string ToString()
		{
			return this.UnderlyingSymbol.ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001782CE File Offset: 0x001764CE
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060061B9 RID: 25017
		public abstract void Dispatch(MetadataVisitor visitor);

		// Token: 0x060061BA RID: 25018 RVA: 0x001782D5 File Offset: 0x001764D5
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x001782D8 File Offset: 0x001764D8
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return this.UnderlyingSymbol;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x001782E0 File Offset: 0x001764E0
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x001782E7 File Offset: 0x001764E7
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}
	}
}
