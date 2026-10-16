using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D8 RID: 1240
	internal sealed class ConstructedNamedTypeSymbol : SubstitutedNamedTypeSymbol
	{
		// Token: 0x060068EB RID: 26859 RVA: 0x0018A3B7 File Offset: 0x001885B7
		internal ConstructedNamedTypeSymbol(NamedTypeSymbol constructedFrom, ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotations, bool unbound = false, NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(constructedFrom.ContainingSymbol, new TypeMap(constructedFrom.ContainingType, constructedFrom.OriginalDefinition.TypeParameters, typeArgumentsWithAnnotations), constructedFrom.OriginalDefinition, constructedFrom, unbound, tupleData)
		{
			this._typeArgumentsWithAnnotations = typeArgumentsWithAnnotations;
			this._constructedFrom = constructedFrom;
		}

		// Token: 0x060068EC RID: 26860 RVA: 0x0018A3F4 File Offset: 0x001885F4
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new ConstructedNamedTypeSymbol(this._constructedFrom, this._typeArgumentsWithAnnotations, this.IsUnboundGenericType, newData);
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x060068ED RID: 26861 RVA: 0x0018A40E File Offset: 0x0018860E
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this._constructedFrom;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x060068EE RID: 26862 RVA: 0x0018A416 File Offset: 0x00188616
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return this._typeArgumentsWithAnnotations;
			}
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x0018A420 File Offset: 0x00188620
		internal static bool TypeParametersMatchTypeArguments(ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			int length = typeParameters.Length;
			for (int i = 0; i < length; i++)
			{
				if (!typeArguments[i].Is(typeParameters[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0018A460 File Offset: 0x00188660
		internal sealed override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			if (this.ConstructedFrom.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, this._typeArgumentsWithAnnotations, owner, ref checkedTypes))
			{
				return true;
			}
			foreach (TypeWithAnnotations typeWithAnnotations in this.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
			{
				if (Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, typeWithAnnotations.CustomModifiers, owner, ref checkedTypes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x060068F1 RID: 26865 RVA: 0x0018A4C5 File Offset: 0x001886C5
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x04001A3A RID: 6714
		private readonly ImmutableArray<TypeWithAnnotations> _typeArgumentsWithAnnotations;

		// Token: 0x04001A3B RID: 6715
		private readonly NamedTypeSymbol _constructedFrom;
	}
}
