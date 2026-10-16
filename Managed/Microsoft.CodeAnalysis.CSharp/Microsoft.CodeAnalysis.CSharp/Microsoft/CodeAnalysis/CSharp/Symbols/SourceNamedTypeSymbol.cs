using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000546 RID: 1350
	internal sealed class SourceNamedTypeSymbol : SourceMemberContainerTypeSymbol, IAttributeTargetSymbol
	{
		// Token: 0x060071EC RID: 29164 RVA: 0x001A758C File Offset: 0x001A578C
		protected override Location GetCorrespondingBaseListLocation(NamedTypeSymbol @base)
		{
			Location location = null;
			foreach (SyntaxReference syntaxReference in base.SyntaxReferences)
			{
				BaseListSyntax baseList = ((TypeDeclarationSyntax)syntaxReference.GetSyntax(default(CancellationToken))).BaseList;
				if (baseList != null)
				{
					SeparatedSyntaxList<BaseTypeSyntax> types = baseList.Types;
					Binder binder = this.DeclaringCompilation.GetBinder(baseList);
					binder = binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
					if (location == null)
					{
						location = types[0].Type.GetLocation();
					}
					foreach (BaseTypeSyntax baseTypeSyntax in types)
					{
						TypeSyntax type = baseTypeSyntax.Type;
						if (TypeSymbol.Equals(binder.BindType(type, BindingDiagnosticBag.Discarded, null, false).Type, @base, TypeCompareKind.ConsiderEverything))
						{
							return type.GetLocation();
						}
					}
				}
			}
			return location;
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x001A7668 File Offset: 0x001A5868
		internal SourceNamedTypeSymbol(NamespaceOrTypeSymbol containingSymbol, MergedTypeDeclaration declaration, BindingDiagnosticBag diagnostics, NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(containingSymbol, declaration, diagnostics, tupleData)
		{
			DeclarationKind kind = declaration.Kind;
			if (kind - DeclarationKind.Class > 4)
			{
				byte b = kind - DeclarationKind.Record;
			}
			if (containingSymbol.Kind == SymbolKind.NamedType)
			{
				this._lazyIsExplicitDefinitionOfNoPiaLocalType = ThreeState.False;
			}
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x001A76BB File Offset: 0x001A58BB
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new SourceNamedTypeSymbol(this.ContainingType, this.declaration, BindingDiagnosticBag.Discarded, newData);
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x001A76D4 File Offset: 0x001A58D4
		private static SyntaxToken GetName(CSharpSyntaxNode node)
		{
			SyntaxKind syntaxKind = node.Kind();
			switch (syntaxKind)
			{
			case SyntaxKind.ClassDeclaration:
			case SyntaxKind.StructDeclaration:
			case SyntaxKind.InterfaceDeclaration:
				break;
			case SyntaxKind.EnumDeclaration:
				return ((EnumDeclarationSyntax)node).Identifier;
			case SyntaxKind.DelegateDeclaration:
				return ((DelegateDeclarationSyntax)node).Identifier;
			default:
				if (syntaxKind != SyntaxKind.RecordDeclaration && syntaxKind != SyntaxKind.RecordStructDeclaration)
				{
					return default(SyntaxToken);
				}
				break;
			}
			return ((BaseTypeDeclarationSyntax)node).Identifier;
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x001A7748 File Offset: 0x001A5948
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			ref string ptr = (ref expandIncludes ? ref this._lazyExpandedDocComment : ref this._lazyDocComment);
			return SourceDocumentationCommentUtils.GetAndCacheDocumentationComment(this, expandIncludes, ref ptr);
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x001A7770 File Offset: 0x001A5970
		private ImmutableArray<TypeParameterSymbol> MakeTypeParameters(BindingDiagnosticBag diagnostics)
		{
			if (this.declaration.Arity == 0)
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
			bool flag = false;
			string[] array = new string[this.declaration.Arity];
			string[] array2 = new string[this.declaration.Arity];
			List<List<TypeParameterBuilder>> list = new List<List<TypeParameterBuilder>>();
			foreach (SyntaxReference syntaxReference in base.SyntaxReferences)
			{
				CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)syntaxReference.GetSyntax(default(CancellationToken));
				SyntaxTree syntaxTree = syntaxReference.SyntaxTree;
				SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
				TypeParameterListSyntax typeParameterListSyntax;
				switch (syntaxKind)
				{
				case SyntaxKind.ClassDeclaration:
				case SyntaxKind.StructDeclaration:
				case SyntaxKind.InterfaceDeclaration:
					goto IL_00C2;
				case SyntaxKind.EnumDeclaration:
					goto IL_00E2;
				case SyntaxKind.DelegateDeclaration:
					typeParameterListSyntax = ((DelegateDeclarationSyntax)csharpSyntaxNode).TypeParameterList;
					break;
				default:
					if (syntaxKind == SyntaxKind.RecordDeclaration || syntaxKind == SyntaxKind.RecordStructDeclaration)
					{
						goto IL_00C2;
					}
					goto IL_00E2;
				}
				IL_00F4:
				bool flag2 = syntaxKind == SyntaxKind.InterfaceDeclaration || syntaxKind == SyntaxKind.DelegateDeclaration;
				List<TypeParameterBuilder> list2 = new List<TypeParameterBuilder>();
				list.Add(list2);
				int num = 0;
				foreach (TypeParameterSyntax typeParameterSyntax in typeParameterListSyntax.Parameters)
				{
					SyntaxToken syntaxToken;
					if (typeParameterSyntax.VarianceKeyword.Kind() != SyntaxKind.None && !flag2)
					{
						BindingDiagnosticBag diagnostics2 = diagnostics;
						ErrorCode errorCode = ErrorCode.ERR_IllegalVarianceSyntax;
						syntaxToken = typeParameterSyntax.VarianceKeyword;
						diagnostics2.Add(errorCode, syntaxToken.GetLocation());
					}
					string text = array[num];
					syntaxToken = typeParameterSyntax.Identifier;
					SourceLocation sourceLocation = new SourceLocation(in syntaxToken);
					string text2 = array2[num];
					syntaxToken = typeParameterSyntax.Identifier;
					SourceMemberContainerTypeSymbol.ReportTypeNamedRecord(syntaxToken.Text, this.DeclaringCompilation, diagnostics.DiagnosticBag, sourceLocation);
					if (text == null)
					{
						string[] array3 = array;
						int num2 = num;
						syntaxToken = typeParameterSyntax.Identifier;
						text = (array3[num2] = syntaxToken.ValueText);
						string[] array4 = array2;
						int num3 = num;
						syntaxToken = typeParameterSyntax.VarianceKeyword;
						string text3 = (array4[num3] = syntaxToken.ValueText);
						for (int j = 0; j < num; j++)
						{
							if (text == array[j])
							{
								flag = true;
								diagnostics.Add(ErrorCode.ERR_DuplicateTypeParameter, sourceLocation, new object[] { text });
								goto IL_0319;
							}
						}
						if (this.ContainingType != null)
						{
							TypeParameterSymbol typeParameterSymbol = this.ContainingType.FindEnclosingTypeParameter(text);
							if (typeParameterSymbol != null)
							{
								diagnostics.Add(ErrorCode.WRN_TypeParameterSameAsOuterTypeParameter, sourceLocation, new object[] { text, typeParameterSymbol.ContainingType });
							}
						}
					}
					else if (!flag)
					{
						string text4 = text2;
						syntaxToken = typeParameterSyntax.VarianceKeyword;
						if (text4 != syntaxToken.ValueText)
						{
							flag = true;
							diagnostics.Add(ErrorCode.ERR_PartialWrongTypeParamsVariance, this.declaration.NameLocations.First<SourceLocation>(), new object[] { this });
						}
						else
						{
							string text5 = text;
							syntaxToken = typeParameterSyntax.Identifier;
							if (text5 != syntaxToken.ValueText)
							{
								flag = true;
								diagnostics.Add(ErrorCode.ERR_PartialWrongTypeParams, this.declaration.NameLocations.First<SourceLocation>(), new object[] { this });
							}
						}
					}
					IL_0319:
					list2.Add(new TypeParameterBuilder(syntaxTree.GetReference(typeParameterSyntax), this, sourceLocation));
					num++;
				}
				continue;
				IL_00C2:
				typeParameterListSyntax = ((TypeDeclarationSyntax)csharpSyntaxNode).TypeParameterList;
				goto IL_00F4;
				IL_00E2:
				throw ExceptionUtilities.UnexpectedValue(csharpSyntaxNode.Kind());
			}
			return Enumerable.Select<IList<TypeParameterBuilder>, TypeParameterSymbol>(list.Transpose<TypeParameterBuilder>(), (IList<TypeParameterBuilder> builders, int i) => builders[0].MakeSymbol(i, builders, diagnostics)).AsImmutable<TypeParameterSymbol>();
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x001A7AEC File Offset: 0x001A5CEC
		internal ImmutableArray<TypeWithAnnotations> GetTypeParameterConstraintTypes(int ordinal)
		{
			ImmutableArray<ImmutableArray<TypeWithAnnotations>> typeParameterConstraintTypes = this.GetTypeParameterConstraintTypes();
			if (typeParameterConstraintTypes.Length <= 0)
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
			return typeParameterConstraintTypes[ordinal];
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x001A7B18 File Offset: 0x001A5D18
		private ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			ImmutableArray<ImmutableArray<TypeWithAnnotations>> immutableArray = this._lazyTypeParameterConstraintTypes;
			if (immutableArray.IsDefault)
			{
				this.GetTypeParameterConstraintKinds();
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				if (ImmutableInterlocked.InterlockedInitialize<ImmutableArray<TypeWithAnnotations>>(ref this._lazyTypeParameterConstraintTypes, this.MakeTypeParameterConstraintTypes(instance)))
				{
					this.AddDeclarationDiagnostics(instance);
				}
				instance.Free();
				immutableArray = this._lazyTypeParameterConstraintTypes;
			}
			return immutableArray;
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x001A7B6C File Offset: 0x001A5D6C
		internal TypeParameterConstraintKind GetTypeParameterConstraintKind(int ordinal)
		{
			ImmutableArray<TypeParameterConstraintKind> typeParameterConstraintKinds = this.GetTypeParameterConstraintKinds();
			if (typeParameterConstraintKinds.Length <= 0)
			{
				return TypeParameterConstraintKind.None;
			}
			return typeParameterConstraintKinds[ordinal];
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x001A7B94 File Offset: 0x001A5D94
		private ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			ImmutableArray<TypeParameterConstraintKind> immutableArray = this._lazyTypeParameterConstraintKinds;
			if (immutableArray.IsDefault)
			{
				ImmutableInterlocked.InterlockedInitialize<TypeParameterConstraintKind>(ref this._lazyTypeParameterConstraintKinds, this.MakeTypeParameterConstraintKinds());
				immutableArray = this._lazyTypeParameterConstraintKinds;
			}
			return immutableArray;
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x001A7BCC File Offset: 0x001A5DCC
		private ImmutableArray<ImmutableArray<TypeWithAnnotations>> MakeTypeParameterConstraintTypes(BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<TypeParameterSymbol> typeParameters = this.TypeParameters;
			ImmutableArray<TypeParameterConstraintClause> immutableArray = ImmutableArray<TypeParameterConstraintClause>.Empty;
			if (typeParameters.Length > 0)
			{
				bool flag = this.SkipPartialDeclarationsWithoutConstraintClauses();
				ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>> arrayBuilder = null;
				foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
				{
					SyntaxReference syntaxReference = singleTypeDeclaration.SyntaxReference;
					TypeParameterListSyntax typeParameterListSyntax;
					SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses = SourceNamedTypeSymbol.GetConstraintClauses((CSharpSyntaxNode)syntaxReference.GetSyntax(default(CancellationToken)), out typeParameterListSyntax);
					if (!flag || constraintClauses.Count != 0)
					{
						BinderFactory binderFactory = this.DeclaringCompilation.GetBinderFactory(syntaxReference.SyntaxTree, false);
						ImmutableArray<TypeParameterConstraintClause> immutableArray2;
						if (constraintClauses.Count == 0)
						{
							immutableArray2 = binderFactory.GetBinder(typeParameterListSyntax.Parameters[0], null, null).GetDefaultTypeParameterConstraintClauses(typeParameterListSyntax);
						}
						else
						{
							immutableArray2 = binderFactory.GetBinder(constraintClauses[0], null, null).WithContainingMemberOrLambda(this).WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.GenericConstraintsClause)
								.BindTypeParameterConstraintClauses(this, typeParameters, typeParameterListSyntax, constraintClauses, diagnostics, false, false);
						}
						if (immutableArray.Length == 0)
						{
							immutableArray = immutableArray2;
						}
						else
						{
							ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>> arrayBuilder2;
							if ((arrayBuilder2 = arrayBuilder) == null)
							{
								arrayBuilder2 = (arrayBuilder = ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>>.GetInstance());
							}
							arrayBuilder2.Add(immutableArray2);
						}
					}
				}
				immutableArray = this.MergeConstraintTypesForPartialDeclarations(immutableArray, arrayBuilder, diagnostics);
				if (immutableArray.All<TypeParameterConstraintClause>((TypeParameterConstraintClause clause) => clause.ConstraintTypes.IsEmpty))
				{
					immutableArray = ImmutableArray<TypeParameterConstraintClause>.Empty;
				}
				if (arrayBuilder != null)
				{
					arrayBuilder.Free();
				}
			}
			return immutableArray.SelectAsArray<TypeParameterConstraintClause, ImmutableArray<TypeWithAnnotations>>((TypeParameterConstraintClause clause) => clause.ConstraintTypes);
		}

		// Token: 0x060071F7 RID: 29175 RVA: 0x001A7D5C File Offset: 0x001A5F5C
		private bool SkipPartialDeclarationsWithoutConstraintClauses()
		{
			ImmutableArray<SingleTypeDeclaration>.Enumerator enumerator = this.declaration.Declarations.GetEnumerator();
			while (enumerator.MoveNext())
			{
				TypeParameterListSyntax typeParameterListSyntax;
				if (SourceNamedTypeSymbol.GetConstraintClauses((CSharpSyntaxNode)enumerator.Current.SyntaxReference.GetSyntax(default(CancellationToken)), out typeParameterListSyntax).Count != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x001A7DBC File Offset: 0x001A5FBC
		private ImmutableArray<TypeParameterConstraintKind> MakeTypeParameterConstraintKinds()
		{
			ImmutableArray<TypeParameterSymbol> typeParameters = this.TypeParameters;
			ImmutableArray<TypeParameterConstraintClause> immutableArray = ImmutableArray<TypeParameterConstraintClause>.Empty;
			if (typeParameters.Length > 0)
			{
				bool flag = this.SkipPartialDeclarationsWithoutConstraintClauses();
				ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>> arrayBuilder = null;
				foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
				{
					SyntaxReference syntaxReference = singleTypeDeclaration.SyntaxReference;
					TypeParameterListSyntax typeParameterListSyntax;
					SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses = SourceNamedTypeSymbol.GetConstraintClauses((CSharpSyntaxNode)syntaxReference.GetSyntax(default(CancellationToken)), out typeParameterListSyntax);
					if (!flag || constraintClauses.Count != 0)
					{
						BinderFactory binderFactory = this.DeclaringCompilation.GetBinderFactory(syntaxReference.SyntaxTree, false);
						ImmutableArray<TypeParameterConstraintClause> immutableArray2;
						if (constraintClauses.Count == 0)
						{
							immutableArray2 = binderFactory.GetBinder(typeParameterListSyntax.Parameters[0], null, null).GetDefaultTypeParameterConstraintClauses(typeParameterListSyntax);
						}
						else
						{
							immutableArray2 = binderFactory.GetBinder(constraintClauses[0], null, null).WithContainingMemberOrLambda(this).WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.GenericConstraintsClause | BinderFlags.SuppressTypeArgumentBinding)
								.BindTypeParameterConstraintClauses(this, typeParameters, typeParameterListSyntax, constraintClauses, BindingDiagnosticBag.Discarded, true, false);
						}
						if (immutableArray.Length == 0)
						{
							immutableArray = immutableArray2;
						}
						else
						{
							ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>> arrayBuilder2;
							if ((arrayBuilder2 = arrayBuilder) == null)
							{
								arrayBuilder2 = (arrayBuilder = ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>>.GetInstance());
							}
							arrayBuilder2.Add(immutableArray2);
						}
					}
				}
				immutableArray = this.MergeConstraintKindsForPartialDeclarations(immutableArray, arrayBuilder);
				immutableArray = ConstraintsHelper.AdjustConstraintKindsBasedOnConstraintTypes(this, typeParameters, immutableArray);
				if (immutableArray.All<TypeParameterConstraintClause>((TypeParameterConstraintClause clause) => clause.Constraints == TypeParameterConstraintKind.None))
				{
					immutableArray = ImmutableArray<TypeParameterConstraintClause>.Empty;
				}
				if (arrayBuilder != null)
				{
					arrayBuilder.Free();
				}
			}
			return immutableArray.SelectAsArray<TypeParameterConstraintClause, TypeParameterConstraintKind>((TypeParameterConstraintClause clause) => clause.Constraints);
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x001A7F58 File Offset: 0x001A6158
		private static SyntaxList<TypeParameterConstraintClauseSyntax> GetConstraintClauses(CSharpSyntaxNode node, out TypeParameterListSyntax typeParameterList)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.DelegateDeclaration)
			{
				if (syntaxKind - SyntaxKind.ClassDeclaration > 2)
				{
					if (syntaxKind != SyntaxKind.DelegateDeclaration)
					{
						goto IL_005F;
					}
					DelegateDeclarationSyntax delegateDeclarationSyntax = (DelegateDeclarationSyntax)node;
					typeParameterList = delegateDeclarationSyntax.TypeParameterList;
					return delegateDeclarationSyntax.ConstraintClauses;
				}
			}
			else if (syntaxKind != SyntaxKind.RecordDeclaration && syntaxKind != SyntaxKind.RecordStructDeclaration)
			{
				goto IL_005F;
			}
			TypeDeclarationSyntax typeDeclarationSyntax = (TypeDeclarationSyntax)node;
			typeParameterList = typeDeclarationSyntax.TypeParameterList;
			return typeDeclarationSyntax.ConstraintClauses;
			IL_005F:
			throw ExceptionUtilities.UnexpectedValue(node.Kind());
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x001A7FD4 File Offset: 0x001A61D4
		private ImmutableArray<TypeParameterConstraintClause> MergeConstraintTypesForPartialDeclarations(ImmutableArray<TypeParameterConstraintClause> constraintClauses, ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>> otherPartialClauses, BindingDiagnosticBag diagnostics)
		{
			if (otherPartialClauses == null)
			{
				return constraintClauses;
			}
			ArrayBuilder<TypeParameterConstraintClause> arrayBuilder = null;
			ImmutableArray<TypeParameterSymbol> typeParameters = this.TypeParameters;
			int length = typeParameters.Length;
			for (int i = 0; i < length; i++)
			{
				TypeParameterConstraintClause typeParameterConstraintClause = constraintClauses[i];
				ImmutableArray<TypeWithAnnotations> constraintTypes = typeParameterConstraintClause.ConstraintTypes;
				ArrayBuilder<TypeWithAnnotations> arrayBuilder2 = null;
				SmallDictionary<TypeWithAnnotations, int> smallDictionary = null;
				bool flag = (this.GetTypeParameterConstraintKind(i) & TypeParameterConstraintKind.PartialMismatch) > TypeParameterConstraintKind.None;
				foreach (ImmutableArray<TypeParameterConstraintClause> immutableArray in otherPartialClauses)
				{
					if (!SourceNamedTypeSymbol.<MergeConstraintTypesForPartialDeclarations>g__mergeConstraints|21_0(constraintTypes, ref smallDictionary, ref arrayBuilder2, immutableArray[i]))
					{
						flag = true;
					}
				}
				if (flag)
				{
					diagnostics.Add(ErrorCode.ERR_PartialWrongConstraints, this.Locations[0], new object[]
					{
						this,
						typeParameters[i]
					});
				}
				if (arrayBuilder2 != null)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<TypeParameterConstraintClause>.GetInstance(constraintClauses.Length);
						arrayBuilder.AddRange(constraintClauses);
					}
					arrayBuilder[i] = TypeParameterConstraintClause.Create(typeParameterConstraintClause.Constraints, (arrayBuilder2 != null) ? arrayBuilder2.ToImmutableAndFree() : constraintTypes);
				}
			}
			if (arrayBuilder != null)
			{
				constraintClauses = arrayBuilder.ToImmutableAndFree();
			}
			return constraintClauses;
		}

		// Token: 0x060071FB RID: 29179 RVA: 0x001A80E8 File Offset: 0x001A62E8
		private ImmutableArray<TypeParameterConstraintClause> MergeConstraintKindsForPartialDeclarations(ImmutableArray<TypeParameterConstraintClause> constraintClauses, ArrayBuilder<ImmutableArray<TypeParameterConstraintClause>> otherPartialClauses)
		{
			if (otherPartialClauses == null)
			{
				return constraintClauses;
			}
			ArrayBuilder<TypeParameterConstraintClause> arrayBuilder = null;
			int length = this.TypeParameters.Length;
			for (int i = 0; i < length; i++)
			{
				TypeParameterConstraintClause typeParameterConstraintClause = constraintClauses[i];
				TypeParameterConstraintKind constraints = typeParameterConstraintClause.Constraints;
				ImmutableArray<TypeWithAnnotations> constraintTypes = typeParameterConstraintClause.ConstraintTypes;
				foreach (ImmutableArray<TypeParameterConstraintClause> immutableArray in otherPartialClauses)
				{
					SourceNamedTypeSymbol.<MergeConstraintKindsForPartialDeclarations>g__mergeConstraints|22_0(ref constraints, constraintTypes, immutableArray[i]);
				}
				if (typeParameterConstraintClause.Constraints != constraints)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<TypeParameterConstraintClause>.GetInstance(constraintClauses.Length);
						arrayBuilder.AddRange(constraintClauses);
					}
					arrayBuilder[i] = TypeParameterConstraintClause.Create(constraints, constraintTypes);
				}
			}
			if (arrayBuilder != null)
			{
				constraintClauses = arrayBuilder.ToImmutableAndFree();
			}
			return constraintClauses;
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x060071FC RID: 29180 RVA: 0x001A81A1 File Offset: 0x001A63A1
		internal sealed override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x060071FD RID: 29181 RVA: 0x001A81AC File Offset: 0x001A63AC
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				if (this._lazyTypeParameters.IsDefault)
				{
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					if (ImmutableInterlocked.InterlockedInitialize<TypeParameterSymbol>(ref this._lazyTypeParameters, this.MakeTypeParameters(instance)))
					{
						this.AddDeclarationDiagnostics(instance);
					}
					instance.Free();
				}
				return this._lazyTypeParameters;
			}
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x001A81F3 File Offset: 0x001A63F3
		internal ImmutableArray<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return this.declaration.GetAttributeDeclarations();
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x060071FF RID: 29183 RVA: 0x001A8200 File Offset: 0x001A6400
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x06007200 RID: 29184 RVA: 0x001A8203 File Offset: 0x001A6403
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Type;
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x06007201 RID: 29185 RVA: 0x001A8208 File Offset: 0x001A6408
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				switch (this.TypeKind)
				{
				case TypeKind.Class:
				case TypeKind.Struct:
					return AttributeLocation.Type;
				case TypeKind.Delegate:
					return AttributeLocation.Type | AttributeLocation.Return;
				case TypeKind.Enum:
				case TypeKind.Interface:
					return AttributeLocation.Type;
				}
				return AttributeLocation.None;
			}
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x001A8258 File Offset: 0x001A6458
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
			if (lazyCustomAttributesBag != null && lazyCustomAttributesBag.IsSealed)
			{
				return lazyCustomAttributesBag;
			}
			if (base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetAttributeDeclarations()), ref this._lazyCustomAttributesBag, AttributeLocation.None, false, null, null))
			{
				this.state.NotePartComplete(CompletionPart.Attributes);
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x001A82A9 File Offset: 0x001A64A9
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x001A82B8 File Offset: 0x001A64B8
		private TypeWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (TypeWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x001A82EC File Offset: 0x001A64EC
		internal CommonTypeEarlyWellKnownAttributeData GetEarlyDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (CommonTypeEarlyWellKnownAttributeData)customAttributesBag.EarlyDecodedWellKnownAttributeData;
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x001A8320 File Offset: 0x001A6520
		internal override CSharpAttributeData EarlyDecodeWellKnownAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			CSharpAttributeData csharpAttributeData;
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.ComImportAttribute))
			{
				bool flag;
				csharpAttributeData = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
				if (!csharpAttributeData.HasErrors)
				{
					arguments.GetOrCreateData<CommonTypeEarlyWellKnownAttributeData>().HasComImportAttribute = true;
					if (!flag)
					{
						return csharpAttributeData;
					}
				}
				return null;
			}
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.CodeAnalysisEmbeddedAttribute))
			{
				bool flag;
				csharpAttributeData = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
				if (!csharpAttributeData.HasErrors)
				{
					arguments.GetOrCreateData<CommonTypeEarlyWellKnownAttributeData>().HasCodeAnalysisEmbeddedAttribute = true;
					if (!flag)
					{
						return csharpAttributeData;
					}
				}
				return null;
			}
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.ConditionalAttribute))
			{
				bool flag;
				csharpAttributeData = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
				if (!csharpAttributeData.HasErrors)
				{
					string constructorArgument = csharpAttributeData.GetConstructorArgument<string>(0, SpecialType.System_String);
					arguments.GetOrCreateData<CommonTypeEarlyWellKnownAttributeData>().AddConditionalSymbol(constructorArgument);
					if (!flag)
					{
						return csharpAttributeData;
					}
				}
				return null;
			}
			ObsoleteAttributeData obsoleteAttributeData;
			if (Symbol.EarlyDecodeDeprecatedOrExperimentalOrObsoleteAttribute(ref arguments, out csharpAttributeData, out obsoleteAttributeData))
			{
				if (obsoleteAttributeData != null)
				{
					arguments.GetOrCreateData<CommonTypeEarlyWellKnownAttributeData>().ObsoleteAttributeData = obsoleteAttributeData;
				}
				return csharpAttributeData;
			}
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.AttributeUsageAttribute))
			{
				bool flag;
				csharpAttributeData = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
				if (!csharpAttributeData.HasErrors)
				{
					AttributeUsageInfo attributeUsageInfo = this.DecodeAttributeUsageAttribute(csharpAttributeData, arguments.AttributeSyntax, false, null);
					if (!attributeUsageInfo.IsNull)
					{
						CommonTypeEarlyWellKnownAttributeData orCreateData = arguments.GetOrCreateData<CommonTypeEarlyWellKnownAttributeData>();
						if (orCreateData.AttributeUsageInfo.IsNull)
						{
							orCreateData.AttributeUsageInfo = attributeUsageInfo;
						}
						if (!flag)
						{
							return csharpAttributeData;
						}
					}
				}
				return null;
			}
			return base.EarlyDecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x06007207 RID: 29191 RVA: 0x001A84C0 File Offset: 0x001A66C0
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			CommonTypeEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
			if (earlyDecodedWellKnownAttributeData != null && !earlyDecodedWellKnownAttributeData.AttributeUsageInfo.IsNull)
			{
				return earlyDecodedWellKnownAttributeData.AttributeUsageInfo;
			}
			if (this.BaseTypeNoUseSiteDiagnostics == null)
			{
				return AttributeUsageInfo.Default;
			}
			return this.BaseTypeNoUseSiteDiagnostics.GetAttributeUsageInfo();
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x06007208 RID: 29192 RVA: 0x001A8508 File Offset: 0x001A6708
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
				if (lazyCustomAttributesBag == null || !lazyCustomAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
				{
					ImmutableArray<SingleTypeDeclaration>.Enumerator enumerator = this.declaration.Declarations.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasAnyAttributes)
						{
							return ObsoleteAttributeData.Uninitialized;
						}
					}
					return null;
				}
				CommonTypeEarlyWellKnownAttributeData commonTypeEarlyWellKnownAttributeData = (CommonTypeEarlyWellKnownAttributeData)lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData;
				if (commonTypeEarlyWellKnownAttributeData == null)
				{
					return null;
				}
				return commonTypeEarlyWellKnownAttributeData.ObsoleteAttributeData;
			}
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x001A8574 File Offset: 0x001A6774
		internal sealed override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			CSharpAttributeData attribute = arguments.Attribute;
			if (attribute.IsTargetAttribute(this, AttributeDescription.AttributeUsageAttribute))
			{
				this.DecodeAttributeUsageAttribute(attribute, arguments.AttributeSyntaxOpt, true, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.DefaultMemberAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasDefaultMemberAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.CoClassAttribute))
			{
				this.DecodeCoClassAttribute(ref arguments);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.ConditionalAttribute))
			{
				this.ValidateConditionalAttribute(attribute, arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.GuidAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().GuidString = attribute.DecodeGuidAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SpecialNameAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasSpecialNameAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SerializableAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasSerializableAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.ExcludeFromCodeCoverageAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasExcludeFromCodeCoverageAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.StructLayoutAttribute))
			{
				AttributeData.DecodeStructLayoutAttribute<TypeWellKnownAttributeData, AttributeSyntax, CSharpAttributeData, AttributeLocation>(ref arguments, base.DefaultMarshallingCharSet, 0, MessageProvider.Instance);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SuppressUnmanagedCodeSecurityAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasSuppressUnmanagedCodeSecurityAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.ClassInterfaceAttribute))
			{
				attribute.DecodeClassInterfaceAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.InterfaceTypeAttribute))
			{
				attribute.DecodeInterfaceTypeAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.WindowsRuntimeImportAttribute))
			{
				arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasWindowsRuntimeImportAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.RequiredAttributeAttribute))
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_CantUseRequiredAttribute, arguments.AttributeSyntaxOpt.Name.Location);
				return;
			}
			if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.DynamicAttribute | Symbol.ReservedAttributes.IsReadOnlyAttribute | Symbol.ReservedAttributes.IsUnmanagedAttribute | Symbol.ReservedAttributes.IsByRefLikeAttribute | Symbol.ReservedAttributes.TupleElementNamesAttribute | Symbol.ReservedAttributes.NullableAttribute | Symbol.ReservedAttributes.NullableContextAttribute | Symbol.ReservedAttributes.NativeIntegerAttribute | Symbol.ReservedAttributes.CaseSensitiveExtensionAttribute))
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.SecurityCriticalAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.SecuritySafeCriticalAttribute))
				{
					arguments.GetOrCreateData<TypeWellKnownAttributeData>().HasSecurityCriticalAttributes = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.SkipLocalsInitAttribute))
				{
					CSharpAttributeData.DecodeSkipLocalsInitAttribute<TypeWellKnownAttributeData>(this.DeclaringCompilation, ref arguments);
					return;
				}
				if (this._lazyIsExplicitDefinitionOfNoPiaLocalType == ThreeState.Unknown && attribute.IsTargetAttribute(this, AttributeDescription.TypeIdentifierAttribute))
				{
					this._lazyIsExplicitDefinitionOfNoPiaLocalType = ThreeState.True;
					return;
				}
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				if (attribute.IsSecurityAttribute(declaringCompilation))
				{
					attribute.DecodeSecurityAttribute<TypeWellKnownAttributeData>(this, declaringCompilation, ref arguments);
				}
			}
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x0600720A RID: 29194 RVA: 0x001A87BC File Offset: 0x001A69BC
		internal override bool IsExplicitDefinitionOfNoPiaLocalType
		{
			get
			{
				if (this._lazyIsExplicitDefinitionOfNoPiaLocalType == ThreeState.Unknown)
				{
					this.CheckPresenceOfTypeIdentifierAttribute();
					if (this._lazyIsExplicitDefinitionOfNoPiaLocalType == ThreeState.Unknown)
					{
						this._lazyIsExplicitDefinitionOfNoPiaLocalType = ThreeState.False;
					}
				}
				return this._lazyIsExplicitDefinitionOfNoPiaLocalType == ThreeState.True;
			}
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x001A87E4 File Offset: 0x001A69E4
		private void CheckPresenceOfTypeIdentifierAttribute()
		{
			CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
			if (lazyCustomAttributesBag != null && lazyCustomAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				return;
			}
			foreach (SyntaxList<AttributeListSyntax> syntaxList in this.GetAttributeDeclarations())
			{
				SyntaxTree syntaxTree = syntaxList.Node.SyntaxTree;
				QuickAttributeChecker quickAttributeChecker = this.DeclaringCompilation.GetBinderFactory(syntaxList.Node.SyntaxTree, false).GetBinder(syntaxList.Node, null, null).QuickAttributeChecker;
				foreach (AttributeListSyntax attributeListSyntax in syntaxList)
				{
					foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
					{
						if (quickAttributeChecker.IsPossibleMatch(attributeSyntax, QuickAttributes.TypeIdentifier))
						{
							this.GetAttributes();
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x001A88BC File Offset: 0x001A6ABC
		private AttributeUsageInfo DecodeAttributeUsageAttribute(CSharpAttributeData attribute, AttributeSyntax node, bool diagnose, BindingDiagnosticBag diagnosticsOpt = null)
		{
			if (!this.DeclaringCompilation.IsAttributeType(this))
			{
				if (diagnose)
				{
					diagnosticsOpt.Add(ErrorCode.ERR_AttributeUsageOnNonAttributeClass, node.Name.Location, new object[] { node.GetErrorDisplayName() });
				}
				return AttributeUsageInfo.Null;
			}
			AttributeUsageInfo attributeUsageInfo = attribute.DecodeAttributeUsageAttribute();
			if (!attributeUsageInfo.HasValidAttributeTargets)
			{
				if (diagnose)
				{
					CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(0, node);
					diagnosticsOpt.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntax.Location, new object[] { node.GetErrorDisplayName() });
				}
				return AttributeUsageInfo.Null;
			}
			return attributeUsageInfo;
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x001A894C File Offset: 0x001A6B4C
		private void DecodeCoClassAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			if (this.IsInterfaceType() && (!arguments.HasDecodedData || ((TypeWellKnownAttributeData)arguments.DecodedData).ComImportCoClass == null))
			{
				NamedTypeSymbol namedTypeSymbol = attribute.CommonConstructorArguments[0].ValueInternal as NamedTypeSymbol;
				if (namedTypeSymbol != null && namedTypeSymbol.TypeKind == TypeKind.Class)
				{
					arguments.GetOrCreateData<TypeWellKnownAttributeData>().ComImportCoClass = namedTypeSymbol;
				}
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x0600720E RID: 29198 RVA: 0x001A89B8 File Offset: 0x001A6BB8
		internal override bool IsComImport
		{
			get
			{
				CommonTypeEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
				return earlyDecodedWellKnownAttributeData != null && earlyDecodedWellKnownAttributeData.HasComImportAttribute;
			}
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x0600720F RID: 29199 RVA: 0x001A89D8 File Offset: 0x001A6BD8
		internal override NamedTypeSymbol ComImportCoClass
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return null;
				}
				return decodedWellKnownAttributeData.ComImportCoClass;
			}
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x001A89F8 File Offset: 0x001A6BF8
		private void ValidateConditionalAttribute(CSharpAttributeData attribute, AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			if (!this.DeclaringCompilation.IsAttributeType(this))
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalOnNonAttributeClass, node.Location, new object[] { node.GetErrorDisplayName() });
				return;
			}
			string constructorArgument = attribute.GetConstructorArgument<string>(0, SpecialType.System_String);
			if (constructorArgument == null || !SyntaxFacts.IsValidIdentifier(constructorArgument))
			{
				CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(0, node);
				diagnostics.Add(ErrorCode.ERR_BadArgumentToAttribute, attributeArgumentSyntax.Location, new object[] { node.GetErrorDisplayName() });
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06007211 RID: 29201 RVA: 0x001A8A74 File Offset: 0x001A6C74
		internal override bool HasSpecialName
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSpecialNameAttribute;
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x06007212 RID: 29202 RVA: 0x001A8A94 File Offset: 0x001A6C94
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				CommonTypeEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
				return earlyDecodedWellKnownAttributeData != null && earlyDecodedWellKnownAttributeData.HasCodeAnalysisEmbeddedAttribute;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x06007213 RID: 29203 RVA: 0x001A8AB3 File Offset: 0x001A6CB3
		internal sealed override bool ShouldAddWinRTMembers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x06007214 RID: 29204 RVA: 0x001A8AB8 File Offset: 0x001A6CB8
		internal sealed override bool IsWindowsRuntimeImport
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasWindowsRuntimeImportAttribute;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x06007215 RID: 29205 RVA: 0x001A8AD8 File Offset: 0x001A6CD8
		public sealed override bool IsSerializable
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSerializableAttribute;
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06007216 RID: 29206 RVA: 0x001A8AF7 File Offset: 0x001A6CF7
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSkipLocalsInitAttribute)
				{
					return false;
				}
				NamedTypeSymbol containingType = this.ContainingType;
				if (containingType == null)
				{
					return this.ContainingModule.AreLocalsZeroed;
				}
				return containingType.AreLocalsZeroed;
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x001A8B2D File Offset: 0x001A6D2D
		internal override bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasExcludeFromCodeCoverageAttribute;
			}
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x001A8B40 File Offset: 0x001A6D40
		private bool HasInstanceFields()
		{
			using (IEnumerator<FieldSymbol> enumerator = this.GetFieldsToEmit().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsStatic)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06007219 RID: 29209 RVA: 0x001A8B94 File Offset: 0x001A6D94
		internal sealed override TypeLayout Layout
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasStructLayoutAttribute)
				{
					return decodedWellKnownAttributeData.Layout;
				}
				if (this.TypeKind == TypeKind.Struct)
				{
					return new TypeLayout(0, this.HasInstanceFields() ? 0 : 1, 0);
				}
				return default(TypeLayout);
			}
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x0600721A RID: 29210 RVA: 0x001A8BE4 File Offset: 0x001A6DE4
		internal bool HasStructLayoutAttribute
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasStructLayoutAttribute;
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x0600721B RID: 29211 RVA: 0x001A8C04 File Offset: 0x001A6E04
		internal override CharSet MarshallingCharSet
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null || !decodedWellKnownAttributeData.HasStructLayoutAttribute)
				{
					return base.DefaultMarshallingCharSet;
				}
				return decodedWellKnownAttributeData.MarshallingCharSet;
			}
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x0600721C RID: 29212 RVA: 0x001A8C30 File Offset: 0x001A6E30
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasDeclarativeSecurity;
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x0600721D RID: 29213 RVA: 0x001A8C50 File Offset: 0x001A6E50
		internal bool HasSecurityCriticalAttributes
		{
			get
			{
				TypeWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSecurityCriticalAttributes;
			}
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x001A8C70 File Offset: 0x001A6E70
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			CustomAttributesBag<CSharpAttributeData> attributesBag = this.GetAttributesBag();
			TypeWellKnownAttributeData typeWellKnownAttributeData = (TypeWellKnownAttributeData)attributesBag.DecodedWellKnownAttributeData;
			if (typeWellKnownAttributeData != null)
			{
				SecurityWellKnownAttributeData securityInformation = typeWellKnownAttributeData.SecurityInformation;
				if (securityInformation != null)
				{
					return securityInformation.GetSecurityAttributes<CSharpAttributeData>(attributesBag.Attributes);
				}
			}
			return null;
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x001A8CAC File Offset: 0x001A6EAC
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			CommonTypeEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
			if (earlyDecodedWellKnownAttributeData == null)
			{
				return ImmutableArray<string>.Empty;
			}
			return earlyDecodedWellKnownAttributeData.ConditionalSymbols;
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x001A8CD0 File Offset: 0x001A6ED0
		internal override void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
			TypeWellKnownAttributeData typeWellKnownAttributeData = (TypeWellKnownAttributeData)decodedData;
			if (this.IsComImport)
			{
				if (typeWellKnownAttributeData == null || typeWellKnownAttributeData.GuidString == null)
				{
					int num = boundAttributes.IndexOfAttribute(this, AttributeDescription.ComImportAttribute);
					diagnostics.Add(ErrorCode.ERR_ComImportWithoutUuidAttribute, allAttributeSyntaxNodes[num].Name.Location, new object[] { this.Name });
				}
				if (this.TypeKind == TypeKind.Class)
				{
					NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
					if (baseTypeNoUseSiteDiagnostics != null && baseTypeNoUseSiteDiagnostics.SpecialType != SpecialType.System_Object)
					{
						diagnostics.Add(ErrorCode.ERR_ComImportWithBase, this.Locations[0], new object[] { this.Name });
					}
					ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>> immutableArray = base.StaticInitializers;
					if (!immutableArray.IsDefaultOrEmpty)
					{
						foreach (ImmutableArray<FieldOrPropertyInitializer> immutableArray2 in immutableArray)
						{
							foreach (FieldOrPropertyInitializer fieldOrPropertyInitializer in immutableArray2)
							{
								if (!fieldOrPropertyInitializer.FieldOpt.IsMetadataConstant)
								{
									diagnostics.Add(ErrorCode.ERR_ComImportWithInitializers, fieldOrPropertyInitializer.Syntax.GetLocation(), new object[] { this.Name });
								}
							}
						}
					}
					immutableArray = base.InstanceInitializers;
					if (!immutableArray.IsDefaultOrEmpty)
					{
						foreach (ImmutableArray<FieldOrPropertyInitializer> immutableArray3 in immutableArray)
						{
							foreach (FieldOrPropertyInitializer fieldOrPropertyInitializer2 in immutableArray3)
							{
								diagnostics.Add(ErrorCode.ERR_ComImportWithInitializers, fieldOrPropertyInitializer2.Syntax.GetLocation(), new object[] { this.Name });
							}
						}
					}
				}
			}
			else if (this.ComImportCoClass != null)
			{
				int num2 = boundAttributes.IndexOfAttribute(this, AttributeDescription.CoClassAttribute);
				diagnostics.Add(ErrorCode.WRN_CoClassWithoutComImport, allAttributeSyntaxNodes[num2].Location, new object[] { this.Name });
			}
			if (typeWellKnownAttributeData != null && typeWellKnownAttributeData.HasDefaultMemberAttribute && base.Indexers.Any<PropertySymbol>())
			{
				int num3 = boundAttributes.IndexOfAttribute(this, AttributeDescription.DefaultMemberAttribute);
				diagnostics.Add(ErrorCode.ERR_DefaultMemberOnIndexedType, allAttributeSyntaxNodes[num3].Name.Location);
			}
			base.PostDecodeWellKnownAttributes(boundAttributes, allAttributeSyntaxNodes, diagnostics, symbolPart, decodedData);
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x001A8F08 File Offset: 0x001A7108
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (base.ContainsExtensionMethods)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_ExtensionAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			if (this.IsRefLikeType)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsByRefLikeAttribute(this));
				if (this.ObsoleteAttributeData == null && !this.IsRestrictedType(true))
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_ObsoleteAttribute__ctor, ImmutableArray.Create<TypedConstant>(new TypedConstant(declaringCompilation.GetSpecialType(SpecialType.System_String), TypedConstantKind.Primitive, "Types with embedded references are not supported in this version of your compiler."), new TypedConstant(declaringCompilation.GetSpecialType(SpecialType.System_Boolean), TypedConstantKind.Primitive, true)), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), true));
				}
			}
			if (this.IsReadOnly)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsReadOnlyAttribute(this));
			}
			if (base.Indexers.Any<PropertySymbol>())
			{
				string metadataName = base.Indexers.First<PropertySymbol>().MetadataName;
				TypedConstant typedConstant = new TypedConstant(declaringCompilation.GetSpecialType(SpecialType.System_String), TypedConstantKind.Primitive, metadataName);
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Reflection_DefaultMemberAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x001A901C File Offset: 0x001A721C
		internal override NamedTypeSymbol AsNativeInteger()
		{
			return this.ContainingAssembly.GetNativeIntegerType(this);
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x06007223 RID: 29219 RVA: 0x001A902A File Offset: 0x001A722A
		internal override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x001A9030 File Offset: 0x001A7230
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			NativeIntegerTypeSymbol nativeIntegerTypeSymbol = t2 as NativeIntegerTypeSymbol;
			if (nativeIntegerTypeSymbol == null)
			{
				return base.Equals(t2, comparison);
			}
			return nativeIntegerTypeSymbol.Equals(this, comparison);
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x06007225 RID: 29221 RVA: 0x001A9058 File Offset: 0x001A7258
		internal sealed override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				if (this._lazyBaseType == ErrorTypeSymbol.UnknownResultType)
				{
					if (this.ContainingType != null)
					{
						NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.ContainingType.BaseTypeNoUseSiteDiagnostics;
					}
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					NamedTypeSymbol namedTypeSymbol = this.MakeAcyclicBaseType(instance);
					if (Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyBaseType, namedTypeSymbol, ErrorTypeSymbol.UnknownResultType) == ErrorTypeSymbol.UnknownResultType)
					{
						this.AddDeclarationDiagnostics(instance);
					}
					instance.Free();
				}
				return this._lazyBaseType;
			}
		}

		// Token: 0x06007226 RID: 29222 RVA: 0x001A90C0 File Offset: 0x001A72C0
		internal sealed override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyInterfaces.IsDefault)
			{
				if (basesBeingResolved != null && basesBeingResolved.ContainsReference(this.OriginalDefinition))
				{
					return ImmutableArray<NamedTypeSymbol>.Empty;
				}
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				ImmutableArray<NamedTypeSymbol> immutableArray = this.MakeAcyclicInterfaces(basesBeingResolved, instance);
				if (ImmutableInterlocked.InterlockedCompareExchange<NamedTypeSymbol>(ref this._lazyInterfaces, immutableArray, default(ImmutableArray<NamedTypeSymbol>)).IsDefault)
				{
					this.AddDeclarationDiagnostics(instance);
				}
				instance.Free();
			}
			return this._lazyInterfaces;
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x001A9134 File Offset: 0x001A7334
		protected override void CheckBase(BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics == null)
			{
				return;
			}
			Location location = null;
			bool flag = baseTypeNoUseSiteDiagnostics.ContainsErrorType();
			if (!flag)
			{
				location = this.FindBaseRefSyntax(baseTypeNoUseSiteDiagnostics);
			}
			if (base.IsGenericType && !flag && this.DeclaringCompilation.IsAttributeType(baseTypeNoUseSiteDiagnostics))
			{
				diagnostics.Add(ErrorCode.ERR_GenericDerivingFromAttribute, location, new object[] { baseTypeNoUseSiteDiagnostics });
			}
			SingleTypeDeclaration singleTypeDeclaration = this.FirstDeclarationWithExplicitBases();
			if (singleTypeDeclaration != null)
			{
				TypeConversions typeConversions = new TypeConversions(this.ContainingAssembly.CorLibrary, false);
				SourceLocation nameLocation = singleTypeDeclaration.NameLocation;
				baseTypeNoUseSiteDiagnostics.CheckAllConstraints(this.DeclaringCompilation, typeConversions, nameLocation, diagnostics);
			}
			if (this.IsClassType() && !baseTypeNoUseSiteDiagnostics.IsObjectType() && !flag)
			{
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
				if (this.declaration.Kind == DeclarationKind.Record)
				{
					if (SynthesizedRecordClone.FindValidCloneMethod(baseTypeNoUseSiteDiagnostics, ref compoundUseSiteInfo) == null)
					{
						diagnostics.Add(ErrorCode.ERR_BadRecordBase, location);
					}
				}
				else if (SynthesizedRecordClone.FindValidCloneMethod(baseTypeNoUseSiteDiagnostics, ref compoundUseSiteInfo) != null)
				{
					diagnostics.Add(ErrorCode.ERR_BadInheritanceFromRecord, location);
				}
				diagnostics.Add(location, compoundUseSiteInfo);
			}
		}

		// Token: 0x06007228 RID: 29224 RVA: 0x001A9230 File Offset: 0x001A7430
		protected override void CheckInterfaces(BindingDiagnosticBag diagnostics)
		{
			MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics = base.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics;
			if (interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.IsEmpty)
			{
				return;
			}
			SingleTypeDeclaration singleTypeDeclaration = this.FirstDeclarationWithExplicitBases();
			if (singleTypeDeclaration != null)
			{
				TypeConversions typeConversions = new TypeConversions(this.ContainingAssembly.CorLibrary, false);
				SourceLocation nameLocation = singleTypeDeclaration.NameLocation;
				foreach (KeyValuePair<NamedTypeSymbol, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>.ValueSet> keyValuePair in interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics)
				{
					MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>.ValueSet value = keyValuePair.Value;
					foreach (NamedTypeSymbol namedTypeSymbol in value)
					{
						namedTypeSymbol.CheckAllConstraints(this.DeclaringCompilation, typeConversions, nameLocation, diagnostics);
					}
					if (value.Count > 1)
					{
						NamedTypeSymbol key = keyValuePair.Key;
						foreach (NamedTypeSymbol namedTypeSymbol2 in value)
						{
							if (key != namedTypeSymbol2)
							{
								if (key.Equals(namedTypeSymbol2, TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
								{
									if (!key.Equals(namedTypeSymbol2, TypeCompareKind.ObliviousNullableModifierMatchesAny))
									{
										diagnostics.Add(ErrorCode.WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList, nameLocation, new object[] { namedTypeSymbol2, this });
									}
								}
								else if (key.Equals(namedTypeSymbol2, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
								{
									diagnostics.Add(ErrorCode.ERR_DuplicateInterfaceWithTupleNamesInBaseList, nameLocation, new object[] { namedTypeSymbol2, key, this });
								}
								else
								{
									diagnostics.Add(ErrorCode.ERR_DuplicateInterfaceWithDifferencesInBaseList, nameLocation, new object[] { namedTypeSymbol2, key, this });
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x001A9410 File Offset: 0x001A7610
		private SourceLocation FindBaseRefSyntax(NamedTypeSymbol baseSym)
		{
			foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
			{
				BaseListSyntax baseListOpt = SourceNamedTypeSymbol.GetBaseListOpt(singleTypeDeclaration);
				if (baseListOpt != null)
				{
					Binder binder = this.DeclaringCompilation.GetBinder(baseListOpt);
					binder = binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
					foreach (BaseTypeSyntax baseTypeSyntax in baseListOpt.Types)
					{
						TypeSyntax type = baseTypeSyntax.Type;
						TypeSymbol type2 = binder.BindType(type, BindingDiagnosticBag.Discarded, null, false).Type;
						if (baseSym.Equals(type2))
						{
							return new SourceLocation(type);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x001A94BC File Offset: 0x001A76BC
		private SingleTypeDeclaration FirstDeclarationWithExplicitBases()
		{
			foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
			{
				if (SourceNamedTypeSymbol.GetBaseListOpt(singleTypeDeclaration) != null)
				{
					return singleTypeDeclaration;
				}
			}
			return null;
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x001A94FC File Offset: 0x001A76FC
		internal Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>> GetDeclaredBases(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyDeclaredBases == null)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				if (Interlocked.CompareExchange<Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>>>(ref this._lazyDeclaredBases, this.MakeDeclaredBases(basesBeingResolved, instance), null) == null)
				{
					this.AddDeclarationDiagnostics(instance);
				}
				instance.Free();
			}
			return this._lazyDeclaredBases;
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x001A9540 File Offset: 0x001A7740
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetDeclaredBases(basesBeingResolved).Item1;
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x001A954E File Offset: 0x001A774E
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetDeclaredBases(basesBeingResolved).Item2;
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x001A955C File Offset: 0x001A775C
		private Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>> MakeDeclaredBases(ConsList<TypeSymbol> basesBeingResolved, BindingDiagnosticBag diagnostics)
		{
			if (this.TypeKind == TypeKind.Enum)
			{
				return new Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>>(null, ImmutableArray<NamedTypeSymbol>.Empty);
			}
			bool flag = false;
			ConsList<TypeSymbol> consList = basesBeingResolved.Prepend(this.OriginalDefinition);
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			NamedTypeSymbol namedTypeSymbol = null;
			SourceLocation sourceLocation = null;
			PooledDictionary<NamedTypeSymbol, SourceLocation> pooledSymbolDictionaryInstance = SpecializedSymbolCollections.GetPooledSymbolDictionaryInstance<NamedTypeSymbol, SourceLocation>();
			foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
			{
				Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>> tuple = this.MakeOneDeclaredBases(consList, singleTypeDeclaration, diagnostics);
				if (tuple != null)
				{
					NamedTypeSymbol item = tuple.Item1;
					ImmutableArray<NamedTypeSymbol> immutableArray = tuple.Item2;
					if (!flag)
					{
						if (namedTypeSymbol == null)
						{
							namedTypeSymbol = item;
							sourceLocation = singleTypeDeclaration.NameLocation;
						}
						else if (namedTypeSymbol.TypeKind == TypeKind.Error && item != null)
						{
							immutableArray = immutableArray.Add(namedTypeSymbol);
							namedTypeSymbol = item;
							sourceLocation = singleTypeDeclaration.NameLocation;
						}
						else if (item != null && !TypeSymbol.Equals(item, namedTypeSymbol, TypeCompareKind.ConsiderEverything) && item.TypeKind != TypeKind.Error)
						{
							CSDiagnosticInfo csdiagnosticInfo = diagnostics.Add(ErrorCode.ERR_PartialMultipleBases, this.Locations[0], new object[] { this });
							namedTypeSymbol = new ExtendedErrorTypeSymbol(namedTypeSymbol, LookupResultKind.Ambiguous, csdiagnosticInfo, false);
							sourceLocation = singleTypeDeclaration.NameLocation;
							flag = true;
						}
					}
					foreach (NamedTypeSymbol namedTypeSymbol2 in immutableArray)
					{
						if (!pooledSymbolDictionaryInstance.ContainsKey(namedTypeSymbol2))
						{
							instance.Add(namedTypeSymbol2);
							pooledSymbolDictionaryInstance.Add(namedTypeSymbol2, singleTypeDeclaration.NameLocation);
						}
					}
				}
			}
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			DeclarationKind kind = this.declaration.Kind;
			if (kind == DeclarationKind.Record || kind == DeclarationKind.RecordStruct)
			{
				NamedTypeSymbol namedTypeSymbol3 = this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_IEquatable_T).Construct(new TypeSymbol[] { this });
				if (instance.IndexOf(namedTypeSymbol3, SymbolEqualityComparer.AllIgnoreOptions) < 0)
				{
					instance.Add(namedTypeSymbol3);
					namedTypeSymbol3.AddUseSiteInfo(ref compoundUseSiteInfo, true);
				}
			}
			if (namedTypeSymbol != null)
			{
				if (namedTypeSymbol.IsStatic)
				{
					diagnostics.Add(ErrorCode.ERR_StaticBaseClass, sourceLocation, new object[] { namedTypeSymbol, this });
				}
				if (!this.IsNoMoreVisibleThan(namedTypeSymbol, ref compoundUseSiteInfo))
				{
					diagnostics.Add(ErrorCode.ERR_BadVisBaseClass, sourceLocation, new object[] { this, namedTypeSymbol });
				}
			}
			ImmutableArray<NamedTypeSymbol> immutableArray2 = instance.ToImmutableAndFree();
			if (this.DeclaredAccessibility != Accessibility.Private && this.IsInterface)
			{
				foreach (NamedTypeSymbol namedTypeSymbol4 in immutableArray2)
				{
					if (!namedTypeSymbol4.IsAtLeastAsVisibleAs(this, ref compoundUseSiteInfo))
					{
						diagnostics.Add(ErrorCode.ERR_BadVisBaseInterface, pooledSymbolDictionaryInstance[namedTypeSymbol4], new object[] { this, namedTypeSymbol4 });
					}
				}
			}
			pooledSymbolDictionaryInstance.Free();
			diagnostics.Add(this.Locations[0], compoundUseSiteInfo);
			return new Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>>(namedTypeSymbol, immutableArray2);
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x001A9808 File Offset: 0x001A7A08
		private static BaseListSyntax GetBaseListOpt(SingleTypeDeclaration decl)
		{
			if (decl.HasBaseDeclarations)
			{
				return ((BaseTypeDeclarationSyntax)decl.SyntaxReference.GetSyntax(default(CancellationToken))).BaseList;
			}
			return null;
		}

		// Token: 0x06007230 RID: 29232 RVA: 0x001A9840 File Offset: 0x001A7A40
		private Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>> MakeOneDeclaredBases(ConsList<TypeSymbol> newBasesBeingResolved, SingleTypeDeclaration decl, BindingDiagnosticBag diagnostics)
		{
			SourceNamedTypeSymbol.<>c__DisplayClass101_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.decl = decl;
			CS$<>8__locals1.diagnostics = diagnostics;
			BaseListSyntax baseListOpt = SourceNamedTypeSymbol.GetBaseListOpt(CS$<>8__locals1.decl);
			if (baseListOpt == null)
			{
				return null;
			}
			NamedTypeSymbol namedTypeSymbol = null;
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			Binder binder = this.DeclaringCompilation.GetBinder(baseListOpt);
			binder = binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
			int num = -1;
			foreach (BaseTypeSyntax baseTypeSyntax in baseListOpt.Types)
			{
				num++;
				TypeSyntax type = baseTypeSyntax.Type;
				if (type.Kind() != SyntaxKind.PredefinedType && !SyntaxFacts.IsName(type.Kind()))
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_BadBaseType, type.GetLocation());
				}
				SourceLocation sourceLocation = new SourceLocation(type);
				TypeSymbol typeSymbol;
				if (num == 0 && this.TypeKind == TypeKind.Class)
				{
					typeSymbol = binder.BindType(type, CS$<>8__locals1.diagnostics, newBasesBeingResolved, false).Type;
					SpecialType specialType = typeSymbol.SpecialType;
					if (SourceNamedTypeSymbol.IsRestrictedBaseType(specialType) && (this.SpecialType != SpecialType.System_Enum || specialType != SpecialType.System_ValueType) && (this.SpecialType != SpecialType.System_MulticastDelegate || specialType != SpecialType.System_Delegate) && (specialType != SpecialType.System_Array || !(this.ContainingAssembly.CorLibrary == this.ContainingAssembly)))
					{
						CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_DeriveFromEnumOrValueType, sourceLocation, new object[] { this, typeSymbol });
						continue;
					}
					if (typeSymbol.IsSealed && !this.IsStatic)
					{
						CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_CantDeriveFromSealedType, sourceLocation, new object[] { this, typeSymbol });
						continue;
					}
					bool flag = false;
					if (typeSymbol.TypeKind == TypeKind.Error)
					{
						flag = true;
						if (typeSymbol.GetNonErrorTypeKindGuess() == TypeKind.Interface)
						{
							flag = false;
						}
					}
					if ((typeSymbol.TypeKind == TypeKind.Class || typeSymbol.TypeKind == TypeKind.Delegate || typeSymbol.TypeKind == TypeKind.Struct || flag) && namedTypeSymbol == null)
					{
						namedTypeSymbol = (NamedTypeSymbol)typeSymbol;
						if (this.IsStatic && namedTypeSymbol.SpecialType != SpecialType.System_Object)
						{
							CSDiagnosticInfo csdiagnosticInfo = CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_StaticDerivedFromNonObject, sourceLocation, new object[] { this, namedTypeSymbol });
							namedTypeSymbol = new ExtendedErrorTypeSymbol(namedTypeSymbol, LookupResultKind.NotReferencable, csdiagnosticInfo, false);
						}
						this.<MakeOneDeclaredBases>g__checkPrimaryConstructorBaseType|101_0(baseTypeSyntax, namedTypeSymbol, ref CS$<>8__locals1);
						continue;
					}
				}
				else
				{
					typeSymbol = binder.BindType(type, CS$<>8__locals1.diagnostics, newBasesBeingResolved, false).Type;
				}
				if (num == 0)
				{
					this.<MakeOneDeclaredBases>g__checkPrimaryConstructorBaseType|101_0(baseTypeSyntax, typeSymbol, ref CS$<>8__locals1);
				}
				switch (typeSymbol.TypeKind)
				{
				case TypeKind.Class:
					if (this.TypeKind == TypeKind.Class)
					{
						if (namedTypeSymbol == null)
						{
							namedTypeSymbol = (NamedTypeSymbol)typeSymbol;
							CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_BaseClassMustBeFirst, sourceLocation, new object[] { typeSymbol });
							continue;
						}
						CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_NoMultipleInheritance, sourceLocation, new object[] { this, namedTypeSymbol, typeSymbol });
						continue;
					}
					break;
				case TypeKind.Dynamic:
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_DeriveFromDynamic, sourceLocation, new object[] { this });
					continue;
				case TypeKind.Error:
					instance.Add((NamedTypeSymbol)typeSymbol);
					continue;
				case TypeKind.Interface:
					foreach (NamedTypeSymbol namedTypeSymbol2 in instance)
					{
						if (namedTypeSymbol2.Equals(typeSymbol, TypeCompareKind.ConsiderEverything))
						{
							CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_DuplicateInterfaceInBaseList, sourceLocation, new object[] { typeSymbol });
						}
						else if (namedTypeSymbol2.Equals(typeSymbol, TypeCompareKind.ObliviousNullableModifierMatchesAny))
						{
							CS$<>8__locals1.diagnostics.Add(ErrorCode.WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList, sourceLocation, new object[] { typeSymbol, this });
						}
					}
					if (this.IsStatic)
					{
						CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_StaticClassInterfaceImpl, sourceLocation, new object[] { this, typeSymbol });
					}
					if (this.IsRefLikeType)
					{
						CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_RefStructInterfaceImpl, sourceLocation, new object[] { this, typeSymbol });
					}
					if (typeSymbol.ContainsDynamic())
					{
						CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_DeriveFromConstructedDynamic, sourceLocation, new object[] { this, typeSymbol });
					}
					instance.Add((NamedTypeSymbol)typeSymbol);
					continue;
				case TypeKind.TypeParameter:
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_DerivingFromATyVar, sourceLocation, new object[] { typeSymbol });
					continue;
				case TypeKind.Submission:
					throw ExceptionUtilities.UnexpectedValue(typeSymbol.TypeKind);
				}
				CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_NonInterfaceInInterfaceList, sourceLocation, new object[] { typeSymbol });
			}
			if (this.SpecialType == SpecialType.System_Object && (namedTypeSymbol != null || instance.Count != 0))
			{
				SyntaxToken name = SourceNamedTypeSymbol.GetName(baseListOpt.Parent);
				CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ObjectCantHaveBases, new SourceLocation(in name));
			}
			return new Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>>(namedTypeSymbol, instance.ToImmutableAndFree());
		}

		// Token: 0x06007231 RID: 29233 RVA: 0x001A9D24 File Offset: 0x001A7F24
		private static bool IsRestrictedBaseType(SpecialType specialType)
		{
			return specialType - SpecialType.System_Enum <= 3 || specialType == SpecialType.System_Array;
		}

		// Token: 0x06007232 RID: 29234 RVA: 0x001A9D34 File Offset: 0x001A7F34
		private ImmutableArray<NamedTypeSymbol> MakeAcyclicInterfaces(ConsList<TypeSymbol> basesBeingResolved, BindingDiagnosticBag diagnostics)
		{
			TypeKind typeKind = this.TypeKind;
			if (typeKind == TypeKind.Enum)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
			ImmutableArray<NamedTypeSymbol> declaredInterfaces = this.GetDeclaredInterfaces(basesBeingResolved);
			bool flag = typeKind == TypeKind.Interface;
			ArrayBuilder<NamedTypeSymbol> arrayBuilder = (flag ? ArrayBuilder<NamedTypeSymbol>.GetInstance() : null);
			foreach (NamedTypeSymbol namedTypeSymbol in declaredInterfaces)
			{
				if (flag)
				{
					if (BaseTypeAnalysis.TypeDependsOn(namedTypeSymbol, this))
					{
						arrayBuilder.Add(new ExtendedErrorTypeSymbol(namedTypeSymbol, LookupResultKind.NotReferencable, diagnostics.Add(ErrorCode.ERR_CycleInInterfaceInheritance, this.Locations[0], new object[] { this, namedTypeSymbol }), false));
						continue;
					}
					arrayBuilder.Add(namedTypeSymbol);
				}
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
				if (namedTypeSymbol.DeclaringCompilation != this.DeclaringCompilation)
				{
					namedTypeSymbol.AddUseSiteInfo(ref compoundUseSiteInfo, true);
					foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics)
					{
						if (namedTypeSymbol2.DeclaringCompilation != this.DeclaringCompilation)
						{
							namedTypeSymbol2.AddUseSiteInfo(ref compoundUseSiteInfo, true);
						}
					}
				}
				diagnostics.Add(this.Locations[0], compoundUseSiteInfo);
			}
			if (!flag)
			{
				return declaredInterfaces;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06007233 RID: 29235 RVA: 0x001A9E6C File Offset: 0x001A806C
		private NamedTypeSymbol MakeAcyclicBaseType(BindingDiagnosticBag diagnostics)
		{
			TypeKind typeKind = this.TypeKind;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			NamedTypeSymbol namedTypeSymbol;
			if (typeKind == TypeKind.Enum)
			{
				namedTypeSymbol = declaringCompilation.GetSpecialType(SpecialType.System_Enum);
			}
			else
			{
				namedTypeSymbol = this.GetDeclaredBaseType(null);
			}
			if (namedTypeSymbol == null)
			{
				if (typeKind <= TypeKind.Delegate)
				{
					if (typeKind != TypeKind.Class)
					{
						if (typeKind == TypeKind.Delegate)
						{
							namedTypeSymbol = declaringCompilation.GetSpecialType(SpecialType.System_MulticastDelegate);
							goto IL_0077;
						}
					}
					else
					{
						if (this.SpecialType == SpecialType.System_Object)
						{
							return null;
						}
						namedTypeSymbol = declaringCompilation.GetSpecialType(SpecialType.System_Object);
						goto IL_0077;
					}
				}
				else
				{
					if (typeKind == TypeKind.Interface)
					{
						return null;
					}
					if (typeKind == TypeKind.Struct)
					{
						namedTypeSymbol = declaringCompilation.GetSpecialType(SpecialType.System_ValueType);
						goto IL_0077;
					}
				}
				throw ExceptionUtilities.UnexpectedValue(typeKind);
			}
			IL_0077:
			if (BaseTypeAnalysis.TypeDependsOn(namedTypeSymbol, this))
			{
				return new ExtendedErrorTypeSymbol(namedTypeSymbol, LookupResultKind.NotReferencable, diagnostics.Add(ErrorCode.ERR_CircularBase, this.Locations[0], new object[] { namedTypeSymbol, this }), false);
			}
			base.SetKnownToHaveNoDeclaredBaseCycles();
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol;
			while (namedTypeSymbol2.DeclaringCompilation != this.DeclaringCompilation)
			{
				namedTypeSymbol2.AddUseSiteInfo(ref compoundUseSiteInfo, true);
				namedTypeSymbol2 = namedTypeSymbol2.BaseTypeNoUseSiteDiagnostics;
				if (namedTypeSymbol2 == null)
				{
					break;
				}
			}
			diagnostics.Add(compoundUseSiteInfo.Diagnostics.IsNullOrEmpty<DiagnosticInfo>() ? Location.None : (this.FindBaseRefSyntax(namedTypeSymbol) ?? this.Locations[0]), compoundUseSiteInfo);
			return namedTypeSymbol;
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x06007234 RID: 29236 RVA: 0x001A9FA4 File Offset: 0x001A81A4
		public override NamedTypeSymbol EnumUnderlyingType
		{
			get
			{
				if (this._lazyEnumUnderlyingType == ErrorTypeSymbol.UnknownResultType)
				{
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					if (Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyEnumUnderlyingType, this.GetEnumUnderlyingType(instance), ErrorTypeSymbol.UnknownResultType) == ErrorTypeSymbol.UnknownResultType)
					{
						this.AddDeclarationDiagnostics(instance);
						this.state.NotePartComplete(CompletionPart.EnumUnderlyingType);
					}
					instance.Free();
				}
				return this._lazyEnumUnderlyingType;
			}
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x001AA008 File Offset: 0x001A8208
		private NamedTypeSymbol GetEnumUnderlyingType(BindingDiagnosticBag diagnostics)
		{
			if (this.TypeKind != TypeKind.Enum)
			{
				return null;
			}
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			BaseListSyntax baseListOpt = SourceNamedTypeSymbol.GetBaseListOpt(this.declaration.Declarations[0]);
			if (baseListOpt != null)
			{
				SeparatedSyntaxList<BaseTypeSyntax> types = baseListOpt.Types;
				if (types.Count > 0)
				{
					TypeSyntax type = types[0].Type;
					TypeSymbol typeSymbol = declaringCompilation.GetBinder(baseListOpt).BindType(type, diagnostics, null, false).Type;
					if (!typeSymbol.SpecialType.IsValidEnumUnderlyingType())
					{
						diagnostics.Add(ErrorCode.ERR_IntegralTypeExpected, type.Location);
						typeSymbol = declaringCompilation.GetSpecialType(SpecialType.System_Int32);
					}
					return (NamedTypeSymbol)typeSymbol;
				}
			}
			NamedTypeSymbol specialType = declaringCompilation.GetSpecialType(SpecialType.System_Int32);
			Binder.ReportUseSite(specialType, diagnostics, this.Locations[0]);
			return specialType;
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x06007236 RID: 29238 RVA: 0x001AA0D3 File Offset: 0x001A82D3
		internal FieldSymbol EnumValueField
		{
			get
			{
				if (this.TypeKind != TypeKind.Enum)
				{
					return null;
				}
				if (this._lazyEnumValueField == null)
				{
					Interlocked.CompareExchange<SynthesizedEnumValueFieldSymbol>(ref this._lazyEnumValueField, new SynthesizedEnumValueFieldSymbol(this), null);
				}
				return this._lazyEnumValueField;
			}
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x001AA104 File Offset: 0x001A8304
		[CompilerGenerated]
		internal static bool <MergeConstraintTypesForPartialDeclarations>g__mergeConstraints|21_0(ImmutableArray<TypeWithAnnotations> originalConstraintTypes, ref SmallDictionary<TypeWithAnnotations, int> originalConstraintTypesMap, ref ArrayBuilder<TypeWithAnnotations> mergedConstraintTypes, TypeParameterConstraintClause clause)
		{
			bool flag = true;
			if (originalConstraintTypes.Length == 0)
			{
				return clause.ConstraintTypes.Length == 0 && flag;
			}
			if (clause.ConstraintTypes.Length == 0)
			{
				return false;
			}
			if (originalConstraintTypesMap == null)
			{
				originalConstraintTypesMap = SourceNamedTypeSymbol.<MergeConstraintTypesForPartialDeclarations>g__toDictionary|21_1(originalConstraintTypes, TypeWithAnnotations.EqualsComparer.IgnoreNullableModifiersForReferenceTypesComparer);
			}
			SmallDictionary<TypeWithAnnotations, int> smallDictionary = SourceNamedTypeSymbol.<MergeConstraintTypesForPartialDeclarations>g__toDictionary|21_1(clause.ConstraintTypes, originalConstraintTypesMap.Comparer);
			foreach (int num in originalConstraintTypesMap.Values)
			{
				ArrayBuilder<TypeWithAnnotations> arrayBuilder = mergedConstraintTypes;
				TypeWithAnnotations typeWithAnnotations = ((arrayBuilder != null) ? arrayBuilder[num] : originalConstraintTypes[num]);
				int num2;
				if (!smallDictionary.TryGetValue(typeWithAnnotations, out num2))
				{
					flag = false;
				}
				else
				{
					TypeWithAnnotations typeWithAnnotations2 = clause.ConstraintTypes[num2];
					if (!typeWithAnnotations.Equals(typeWithAnnotations2, TypeCompareKind.ObliviousNullableModifierMatchesAny))
					{
						flag = false;
					}
					else if (!typeWithAnnotations.Equals(typeWithAnnotations2, TypeCompareKind.ConsiderEverything))
					{
						if (mergedConstraintTypes == null)
						{
							mergedConstraintTypes = ArrayBuilder<TypeWithAnnotations>.GetInstance(originalConstraintTypes.Length);
							mergedConstraintTypes.AddRange(originalConstraintTypes);
						}
						mergedConstraintTypes[num] = typeWithAnnotations.MergeEquivalentTypes(typeWithAnnotations2, VarianceKind.None);
					}
				}
			}
			foreach (TypeWithAnnotations typeWithAnnotations3 in smallDictionary.Keys)
			{
				if (!originalConstraintTypesMap.ContainsKey(typeWithAnnotations3))
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x001AA24C File Offset: 0x001A844C
		[CompilerGenerated]
		internal static SmallDictionary<TypeWithAnnotations, int> <MergeConstraintTypesForPartialDeclarations>g__toDictionary|21_1(ImmutableArray<TypeWithAnnotations> constraintTypes, IEqualityComparer<TypeWithAnnotations> comparer)
		{
			SmallDictionary<TypeWithAnnotations, int> smallDictionary = new SmallDictionary<TypeWithAnnotations, int>(comparer);
			for (int i = constraintTypes.Length - 1; i >= 0; i--)
			{
				smallDictionary[constraintTypes[i]] = i;
			}
			return smallDictionary;
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x001AA284 File Offset: 0x001A8484
		[CompilerGenerated]
		internal static void <MergeConstraintKindsForPartialDeclarations>g__mergeConstraints|22_0(ref TypeParameterConstraintKind mergedKind, ImmutableArray<TypeWithAnnotations> originalConstraintTypes, TypeParameterConstraintClause clause)
		{
			if ((mergedKind & (TypeParameterConstraintKind.ReferenceType | TypeParameterConstraintKind.ValueType | TypeParameterConstraintKind.Constructor | TypeParameterConstraintKind.Unmanaged | TypeParameterConstraintKind.NotNull)) != (clause.Constraints & (TypeParameterConstraintKind.ReferenceType | TypeParameterConstraintKind.ValueType | TypeParameterConstraintKind.Constructor | TypeParameterConstraintKind.Unmanaged | TypeParameterConstraintKind.NotNull)))
			{
				mergedKind |= TypeParameterConstraintKind.PartialMismatch;
			}
			if ((mergedKind & TypeParameterConstraintKind.ReferenceType) != TypeParameterConstraintKind.None && (clause.Constraints & TypeParameterConstraintKind.ReferenceType) != TypeParameterConstraintKind.None)
			{
				TypeParameterConstraintKind typeParameterConstraintKind = mergedKind & TypeParameterConstraintKind.AllReferenceTypeKinds;
				TypeParameterConstraintKind typeParameterConstraintKind2 = clause.Constraints & TypeParameterConstraintKind.AllReferenceTypeKinds;
				if (typeParameterConstraintKind != typeParameterConstraintKind2)
				{
					if (typeParameterConstraintKind == TypeParameterConstraintKind.ReferenceType)
					{
						mergedKind = (mergedKind & (TypeParameterConstraintKind)(-50)) | typeParameterConstraintKind2;
					}
					else if (typeParameterConstraintKind2 != TypeParameterConstraintKind.ReferenceType)
					{
						mergedKind |= TypeParameterConstraintKind.PartialMismatch;
					}
				}
			}
			if (originalConstraintTypes.Length == 0 && clause.ConstraintTypes.Length == 0 && ((mergedKind | clause.Constraints) & ~(TypeParameterConstraintKind.Constructor | TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType)) == TypeParameterConstraintKind.None && (mergedKind & TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType) != TypeParameterConstraintKind.None && (clause.Constraints & TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType) == TypeParameterConstraintKind.None)
			{
				mergedKind &= ~TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType;
			}
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x001AA334 File Offset: 0x001A8534
		[CompilerGenerated]
		private void <MakeOneDeclaredBases>g__checkPrimaryConstructorBaseType|101_0(BaseTypeSyntax baseTypeSyntax, TypeSymbol baseType, ref SourceNamedTypeSymbol.<>c__DisplayClass101_0 A_3)
		{
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = baseTypeSyntax as PrimaryConstructorBaseTypeSyntax;
			if (primaryConstructorBaseTypeSyntax != null && (!this.IsRecord || this.TypeKind != TypeKind.Class || baseType.TypeKind == TypeKind.Interface || ((RecordDeclarationSyntax)A_3.decl.SyntaxReference.GetSyntax(default(CancellationToken))).ParameterList == null))
			{
				A_3.diagnostics.Add(ErrorCode.ERR_UnexpectedArgumentList, primaryConstructorBaseTypeSyntax.ArgumentList.Location);
			}
		}

		// Token: 0x04001BBF RID: 7103
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x04001BC0 RID: 7104
		private ImmutableArray<ImmutableArray<TypeWithAnnotations>> _lazyTypeParameterConstraintTypes;

		// Token: 0x04001BC1 RID: 7105
		private ImmutableArray<TypeParameterConstraintKind> _lazyTypeParameterConstraintKinds;

		// Token: 0x04001BC2 RID: 7106
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001BC3 RID: 7107
		private string _lazyDocComment;

		// Token: 0x04001BC4 RID: 7108
		private string _lazyExpandedDocComment;

		// Token: 0x04001BC5 RID: 7109
		private ThreeState _lazyIsExplicitDefinitionOfNoPiaLocalType;

		// Token: 0x04001BC6 RID: 7110
		private Tuple<NamedTypeSymbol, ImmutableArray<NamedTypeSymbol>> _lazyDeclaredBases;

		// Token: 0x04001BC7 RID: 7111
		private NamedTypeSymbol _lazyBaseType = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x04001BC8 RID: 7112
		private ImmutableArray<NamedTypeSymbol> _lazyInterfaces;

		// Token: 0x04001BC9 RID: 7113
		private SynthesizedEnumValueFieldSymbol _lazyEnumValueField;

		// Token: 0x04001BCA RID: 7114
		private NamedTypeSymbol _lazyEnumUnderlyingType = ErrorTypeSymbol.UnknownResultType;
	}
}
