using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200055C RID: 1372
	internal sealed class TypeParameterBuilder
	{
		// Token: 0x06007435 RID: 29749 RVA: 0x001B220D File Offset: 0x001B040D
		internal TypeParameterBuilder(SyntaxReference syntaxRef, SourceNamedTypeSymbol owner, Location location)
		{
			this._syntaxRef = syntaxRef;
			this._owner = owner;
			this._location = location;
		}

		// Token: 0x06007436 RID: 29750 RVA: 0x001B222C File Offset: 0x001B042C
		internal TypeParameterSymbol MakeSymbol(int ordinal, IList<TypeParameterBuilder> builders, BindingDiagnosticBag diagnostics)
		{
			TypeParameterSyntax typeParameterSyntax = (TypeParameterSyntax)this._syntaxRef.GetSyntax(default(CancellationToken));
			SourceTypeParameterSymbol sourceTypeParameterSymbol = new SourceTypeParameterSymbol(this._owner, typeParameterSyntax.Identifier.ValueText, ordinal, typeParameterSyntax.VarianceKeyword.VarianceKindFromToken(), TypeParameterBuilder.ToLocations(builders), TypeParameterBuilder.ToSyntaxRefs(builders));
			if (sourceTypeParameterSymbol.Name == sourceTypeParameterSymbol.ContainingSymbol.Name)
			{
				diagnostics.Add(ErrorCode.ERR_TypeVariableSameAsParent, sourceTypeParameterSymbol.Locations[0], new object[] { sourceTypeParameterSymbol.Name });
			}
			return sourceTypeParameterSymbol;
		}

		// Token: 0x06007437 RID: 29751 RVA: 0x001B22CC File Offset: 0x001B04CC
		private static ImmutableArray<Location> ToLocations(IList<TypeParameterBuilder> builders)
		{
			ArrayBuilder<Location> instance = ArrayBuilder<Location>.GetInstance(builders.Count);
			foreach (TypeParameterBuilder typeParameterBuilder in builders)
			{
				instance.Add(typeParameterBuilder._location);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x001B232C File Offset: 0x001B052C
		private static ImmutableArray<SyntaxReference> ToSyntaxRefs(IList<TypeParameterBuilder> builders)
		{
			ArrayBuilder<SyntaxReference> instance = ArrayBuilder<SyntaxReference>.GetInstance(builders.Count);
			foreach (TypeParameterBuilder typeParameterBuilder in builders)
			{
				instance.Add(typeParameterBuilder._syntaxRef);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x04001C27 RID: 7207
		private readonly SyntaxReference _syntaxRef;

		// Token: 0x04001C28 RID: 7208
		private readonly SourceNamedTypeSymbol _owner;

		// Token: 0x04001C29 RID: 7209
		private readonly Location _location;
	}
}
