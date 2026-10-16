using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D7 RID: 1239
	internal sealed class SubstitutedNestedTypeSymbol : SubstitutedNamedTypeSymbol
	{
		// Token: 0x060068E5 RID: 26853 RVA: 0x0018A368 File Offset: 0x00188568
		internal SubstitutedNestedTypeSymbol(SubstitutedNamedTypeSymbol newContainer, NamedTypeSymbol originalDefinition)
			: base(newContainer, newContainer.TypeSubstitution, originalDefinition, null, newContainer.IsUnboundGenericType && originalDefinition.Arity == 0, null)
		{
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x0018A38E File Offset: 0x0018858E
		internal override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return this.OriginalDefinition.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes);
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x060068E7 RID: 26855 RVA: 0x0018A39E File Offset: 0x0018859E
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x060068E8 RID: 26856 RVA: 0x0018A3A6 File Offset: 0x001885A6
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x060068E9 RID: 26857 RVA: 0x0018A3A9 File Offset: 0x001885A9
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060068EA RID: 26858 RVA: 0x0018A3B0 File Offset: 0x001885B0
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}
	}
}
