using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E5 RID: 1253
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ExtendedErrorTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x060069E2 RID: 27106 RVA: 0x0018AE07 File Offset: 0x00189007
		internal ExtendedErrorTypeSymbol(CSharpCompilation compilation, string name, int arity, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo errorInfo, bool unreported = false, bool variableUsedBeforeDeclaration = false)
			: this(compilation.Assembly.GlobalNamespace, name, arity, errorInfo, unreported, variableUsedBeforeDeclaration)
		{
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0018AE22 File Offset: 0x00189022
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ExtendedErrorTypeSymbol(NamespaceOrTypeSymbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(1)] string name, int arity, DiagnosticInfo errorInfo, bool unreported = false, bool variableUsedBeforeDeclaration = false)
			: base(null)
		{
			this._name = name;
			this._errorInfo = errorInfo;
			this._containingSymbol = containingSymbol;
			this._arity = arity;
			this._unreported = unreported;
			this.VariableUsedBeforeDeclaration = variableUsedBeforeDeclaration;
			this._resultKind = LookupResultKind.Empty;
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x0018AE60 File Offset: 0x00189060
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private ExtendedErrorTypeSymbol(NamespaceOrTypeSymbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(1)] string name, int arity, DiagnosticInfo errorInfo, bool unreported, bool variableUsedBeforeDeclaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> candidateSymbols, LookupResultKind resultKind)
			: base(null)
		{
			this._name = name;
			this._errorInfo = errorInfo;
			this._containingSymbol = containingSymbol;
			this._arity = arity;
			this._unreported = unreported;
			this.VariableUsedBeforeDeclaration = variableUsedBeforeDeclaration;
			this._candidateSymbols = candidateSymbols;
			this._resultKind = resultKind;
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x0018AEB1 File Offset: 0x001890B1
		internal ExtendedErrorTypeSymbol(NamespaceOrTypeSymbol guessSymbol, LookupResultKind resultKind, DiagnosticInfo errorInfo, bool unreported = false)
			: this(guessSymbol.ContainingNamespaceOrType(), guessSymbol, resultKind, errorInfo, unreported)
		{
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x0018AEC4 File Offset: 0x001890C4
		internal ExtendedErrorTypeSymbol([global::System.Runtime.CompilerServices.Nullable(2)] NamespaceOrTypeSymbol containingSymbol, Symbol guessSymbol, LookupResultKind resultKind, DiagnosticInfo errorInfo, bool unreported = false)
			: this(containingSymbol, ImmutableArray.Create<Symbol>(guessSymbol), resultKind, errorInfo, ExtendedErrorTypeSymbol.GetArity(guessSymbol), unreported)
		{
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x0018AEDE File Offset: 0x001890DE
		internal ExtendedErrorTypeSymbol([global::System.Runtime.CompilerServices.Nullable(2)] NamespaceOrTypeSymbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> candidateSymbols, LookupResultKind resultKind, DiagnosticInfo errorInfo, int arity, bool unreported = false)
			: this(containingSymbol, candidateSymbols[0].Name, arity, errorInfo, unreported, false)
		{
			this._candidateSymbols = ExtendedErrorTypeSymbol.UnwrapErrorCandidates(candidateSymbols);
			this._resultKind = resultKind;
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x0018AF0E File Offset: 0x0018910E
		internal ExtendedErrorTypeSymbol AsUnreported()
		{
			if (!this.Unreported)
			{
				return new ExtendedErrorTypeSymbol(this._containingSymbol, this._name, this._arity, this._errorInfo, true, this.VariableUsedBeforeDeclaration, this._candidateSymbols, this._resultKind);
			}
			return this;
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x0018AF4C File Offset: 0x0018914C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<Symbol> UnwrapErrorCandidates([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> candidateSymbols)
		{
			ErrorTypeSymbol errorTypeSymbol = (candidateSymbols.IsEmpty ? null : (candidateSymbols[0] as ErrorTypeSymbol));
			if (errorTypeSymbol == null || errorTypeSymbol.CandidateSymbols.IsEmpty)
			{
				return candidateSymbols;
			}
			return errorTypeSymbol.CandidateSymbols;
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x0018AF8E File Offset: 0x0018918E
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x060069EB RID: 27115 RVA: 0x0018AF95 File Offset: 0x00189195
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override DiagnosticInfo ErrorInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._errorInfo;
			}
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x060069EC RID: 27116 RVA: 0x0018AF9D File Offset: 0x0018919D
		internal override LookupResultKind ResultKind
		{
			get
			{
				return this._resultKind;
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x060069ED RID: 27117 RVA: 0x0018AFA5 File Offset: 0x001891A5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> CandidateSymbols
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._candidateSymbols.NullToEmpty<Symbol>();
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x0018AFB2 File Offset: 0x001891B2
		internal override bool Unreported
		{
			get
			{
				return this._unreported;
			}
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x0018AFBC File Offset: 0x001891BC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (this._unreported)
			{
				return new UseSiteInfo<AssemblySymbol>(this.ErrorInfo);
			}
			return default(UseSiteInfo<AssemblySymbol>);
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x060069F0 RID: 27120 RVA: 0x0018AFE6 File Offset: 0x001891E6
		public override int Arity
		{
			get
			{
				return this._arity;
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x060069F1 RID: 27121 RVA: 0x0018AFEE File Offset: 0x001891EE
		internal override bool MangleName
		{
			get
			{
				return this._arity > 0;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x060069F2 RID: 27122 RVA: 0x0018AFF9 File Offset: 0x001891F9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x060069F3 RID: 27123 RVA: 0x0018B001 File Offset: 0x00189201
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x060069F4 RID: 27124 RVA: 0x0018B009 File Offset: 0x00189209
		public override NamedTypeSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x060069F5 RID: 27125 RVA: 0x0018B00C File Offset: 0x0018920C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x060069F6 RID: 27126 RVA: 0x0018B013 File Offset: 0x00189213
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x0018B016 File Offset: 0x00189216
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return null;
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x0018B019 File Offset: 0x00189219
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x0018B020 File Offset: 0x00189220
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static TypeSymbol ExtractNonErrorType(TypeSymbol oldSymbol)
		{
			if (oldSymbol == null || oldSymbol.TypeKind != TypeKind.Error)
			{
				return oldSymbol;
			}
			ExtendedErrorTypeSymbol extendedErrorTypeSymbol = oldSymbol.OriginalDefinition as ExtendedErrorTypeSymbol;
			if (extendedErrorTypeSymbol != null && !extendedErrorTypeSymbol._candidateSymbols.IsDefault && extendedErrorTypeSymbol._candidateSymbols.Length == 1)
			{
				TypeSymbol typeSymbol = extendedErrorTypeSymbol._candidateSymbols[0] as TypeSymbol;
				if (typeSymbol != null)
				{
					return typeSymbol.GetNonErrorGuess();
				}
			}
			return null;
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x0018B08C File Offset: 0x0018928C
		internal static TypeKind ExtractNonErrorTypeKind(TypeSymbol oldSymbol)
		{
			if (oldSymbol.TypeKind != TypeKind.Error)
			{
				return oldSymbol.TypeKind;
			}
			ExtendedErrorTypeSymbol extendedErrorTypeSymbol = oldSymbol.OriginalDefinition as ExtendedErrorTypeSymbol;
			TypeKind typeKind = TypeKind.Error;
			if (extendedErrorTypeSymbol != null && !extendedErrorTypeSymbol._candidateSymbols.IsDefault && extendedErrorTypeSymbol._candidateSymbols.Length > 0)
			{
				foreach (Symbol symbol in extendedErrorTypeSymbol._candidateSymbols)
				{
					TypeSymbol typeSymbol = symbol as TypeSymbol;
					if (typeSymbol != null && typeSymbol.TypeKind != TypeKind.Error)
					{
						if (typeKind == TypeKind.Error)
						{
							typeKind = typeSymbol.TypeKind;
						}
						else if (typeKind != typeSymbol.TypeKind)
						{
							return TypeKind.Error;
						}
					}
				}
			}
			return typeKind;
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x0018B12C File Offset: 0x0018932C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			if (this == t2)
			{
				return true;
			}
			ExtendedErrorTypeSymbol extendedErrorTypeSymbol = t2 as ExtendedErrorTypeSymbol;
			return extendedErrorTypeSymbol != null && !this._unreported && !extendedErrorTypeSymbol._unreported && (((this.ContainingType != null) ? this.ContainingType.Equals(extendedErrorTypeSymbol.ContainingType, comparison) : ((this.ContainingSymbol == null) ? (extendedErrorTypeSymbol.ContainingSymbol == null) : this.ContainingSymbol.Equals(extendedErrorTypeSymbol.ContainingSymbol))) && this.Name == extendedErrorTypeSymbol.Name) && this.Arity == extendedErrorTypeSymbol.Arity;
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x0018B1C0 File Offset: 0x001893C0
		public override int GetHashCode()
		{
			return Hash.Combine(this.Arity, Hash.Combine((this.ContainingSymbol != null) ? this.ContainingSymbol.GetHashCode() : 0, (this.Name != null) ? this.Name.GetHashCode() : 0));
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x0018B200 File Offset: 0x00189400
		private static int GetArity(Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind != SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.Method)
				{
					return ((MethodSymbol)symbol).Arity;
				}
				if (kind != SymbolKind.NamedType)
				{
					return 0;
				}
			}
			return ((NamedTypeSymbol)symbol).Arity;
		}

		// Token: 0x04001A53 RID: 6739
		private readonly string _name;

		// Token: 0x04001A54 RID: 6740
		private readonly int _arity;

		// Token: 0x04001A55 RID: 6741
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly DiagnosticInfo _errorInfo;

		// Token: 0x04001A56 RID: 6742
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly NamespaceOrTypeSymbol _containingSymbol;

		// Token: 0x04001A57 RID: 6743
		private readonly bool _unreported;

		// Token: 0x04001A58 RID: 6744
		public readonly bool VariableUsedBeforeDeclaration;

		// Token: 0x04001A59 RID: 6745
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<Symbol> _candidateSymbols;

		// Token: 0x04001A5A RID: 6746
		private readonly LookupResultKind _resultKind;
	}
}
