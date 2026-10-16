using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200052D RID: 1325
	internal abstract class SourceConstructorSymbolBase : SourceMemberMethodSymbol
	{
		// Token: 0x06006F52 RID: 28498 RVA: 0x0019940C File Offset: 0x0019760C
		protected SourceConstructorSymbolBase(SourceMemberContainerTypeSymbol containingType, Location location, CSharpSyntaxNode syntax, bool isIterator)
			: base(containingType, syntax.GetReference(), ImmutableArray.Create<Location>(location), isIterator)
		{
		}

		// Token: 0x06006F53 RID: 28499 RVA: 0x00199424 File Offset: 0x00197624
		protected sealed override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
			BinderFactory binderFactory = this.DeclaringCompilation.GetBinderFactory(csharpSyntaxNode.SyntaxTree, false);
			ParameterListSyntax parameterList = this.GetParameterList();
			Binder binder = binderFactory.GetBinder(parameterList, csharpSyntaxNode, this).WithContainingMemberOrLambda(this);
			Binder binder2 = binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
			Binder binder3 = binder2;
			BaseParameterListSyntax baseParameterListSyntax = parameterList;
			bool allowRefOrOut = this.AllowRefOrOut;
			SyntaxToken syntaxToken;
			this._lazyParameters = ParameterHelpers.MakeParameters(binder3, this, baseParameterListSyntax, out syntaxToken, diagnostics, allowRefOrOut, false, false);
			this._lazyIsVararg = syntaxToken.Kind() == SyntaxKind.ArgListKeyword;
			this._lazyReturnType = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_Void, diagnostics, csharpSyntaxNode), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			Location location = this.Locations[0];
			if (this.MethodKind == MethodKind.StaticConstructor && this._lazyParameters.Length != 0 && this.ContainingType.Name == ((ConstructorDeclarationSyntax)this.SyntaxNode).Identifier.ValueText)
			{
				diagnostics.Add(ErrorCode.ERR_StaticConstParam, location, new object[] { this });
			}
			base.CheckEffectiveAccessibility(this._lazyReturnType, this._lazyParameters, diagnostics);
			if (this._lazyIsVararg && (this.IsGenericMethod || this.ContainingType.IsGenericType || (this._lazyParameters.Length > 0 && this._lazyParameters[this._lazyParameters.Length - 1].IsParams)))
			{
				diagnostics.Add(ErrorCode.ERR_BadVarargs, location);
			}
		}

		// Token: 0x06006F54 RID: 28500
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract ParameterListSyntax GetParameterList();

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x06006F55 RID: 28501
		protected abstract bool AllowRefOrOut { get; }

		// Token: 0x06006F56 RID: 28502 RVA: 0x001995A4 File Offset: 0x001977A4
		internal sealed override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			base.AfterAddingTypeMembersChecks(conversions, diagnostics);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			ParameterHelpers.EnsureIsReadOnlyAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			ParameterHelpers.EnsureNativeIntegerAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
			ParameterHelpers.EnsureNullableAttributeExists(declaringCompilation, this, this.Parameters, diagnostics, true);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				parameterSymbol.Type.CheckAllConstraints(declaringCompilation, conversions, parameterSymbol.Locations[0], diagnostics);
			}
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x06006F57 RID: 28503 RVA: 0x0019962B File Offset: 0x0019782B
		public sealed override bool IsVararg
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyIsVararg;
			}
		}

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x00199639 File Offset: 0x00197839
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return base.IsImplicitlyDeclared;
			}
		}

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x06006F59 RID: 28505 RVA: 0x00199641 File Offset: 0x00197841
		internal sealed override int ParameterCount
		{
			get
			{
				if (!this._lazyParameters.IsDefault)
				{
					return this._lazyParameters.Length;
				}
				return this.GetParameterList().ParameterCount;
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x00199667 File Offset: 0x00197867
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyParameters;
			}
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x06006F5B RID: 28507 RVA: 0x00199675 File Offset: 0x00197875
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x06006F5C RID: 28508 RVA: 0x0019967C File Offset: 0x0019787C
		public sealed override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x00199683 File Offset: 0x00197883
		public sealed override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x06006F5E RID: 28510 RVA: 0x0019968A File Offset: 0x0019788A
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x0019968D File Offset: 0x0019788D
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyReturnType;
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x06006F60 RID: 28512 RVA: 0x0019969B File Offset: 0x0019789B
		public sealed override string Name
		{
			get
			{
				if (!this.IsStatic)
				{
					return ".ctor";
				}
				return ".cctor";
			}
		}

		// Token: 0x06006F61 RID: 28513 RVA: 0x001996B0 File Offset: 0x001978B0
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetReturnTypeAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x06006F62 RID: 28514 RVA: 0x001996CB File Offset: 0x001978CB
		protected sealed override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return base.AttributeOwner;
			}
		}

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x06006F63 RID: 28515 RVA: 0x001996D3 File Offset: 0x001978D3
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x001996D8 File Offset: 0x001978D8
		internal sealed override int CalculateLocalSyntaxOffset(int position, SyntaxTree tree)
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
			if (tree == csharpSyntaxNode.SyntaxTree)
			{
				int num;
				if (this.IsWithinExpressionOrBlockBody(position, out num))
				{
					return num;
				}
				if (position == csharpSyntaxNode.SpanStart)
				{
					return -1;
				}
			}
			CSharpSyntaxNode initializer = this.GetInitializer();
			int num2;
			if (tree == ((initializer != null) ? initializer.SyntaxTree : null))
			{
				TextSpan span = initializer.Span;
				num2 = span.Length;
				if (span.Contains(position))
				{
					return -num2 + (position - span.Start);
				}
			}
			else
			{
				num2 = 0;
			}
			int num3;
			if (((SourceNamedTypeSymbol)this.ContainingType).TryCalculateSyntaxOffsetOfPositionInInitializer(position, tree, this.IsStatic, num2, out num3))
			{
				return num3;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006F65 RID: 28517
		internal abstract override bool IsNullableAnalysisEnabled();

		// Token: 0x06006F66 RID: 28518
		protected abstract CSharpSyntaxNode GetInitializer();

		// Token: 0x06006F67 RID: 28519
		protected abstract bool IsWithinExpressionOrBlockBody(int position, out int offset);

		// Token: 0x04001B4F RID: 6991
		protected ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001B50 RID: 6992
		private TypeWithAnnotations _lazyReturnType;

		// Token: 0x04001B51 RID: 6993
		private bool _lazyIsVararg;
	}
}
