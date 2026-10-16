using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200051D RID: 1309
	internal class GlobalExpressionVariable : SourceMemberFieldSymbol
	{
		// Token: 0x06006DE0 RID: 28128 RVA: 0x001947BD File Offset: 0x001929BD
		internal GlobalExpressionVariable(SourceMemberContainerTypeSymbol containingType, DeclarationModifiers modifiers, TypeSyntax typeSyntax, string name, SyntaxReference syntax, Location location)
			: base(containingType, modifiers, name, syntax, location)
		{
			this._typeSyntaxOpt = ((typeSyntax != null) ? typeSyntax.GetReference() : null);
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x001947E0 File Offset: 0x001929E0
		internal static GlobalExpressionVariable Create(SourceMemberContainerTypeSymbol containingType, DeclarationModifiers modifiers, TypeSyntax typeSyntax, string name, SyntaxNode syntax, Location location, FieldSymbol containingFieldOpt, SyntaxNode nodeToBind)
		{
			SyntaxReference reference = syntax.GetReference();
			if (typeSyntax != null && !typeSyntax.IsVar)
			{
				return new GlobalExpressionVariable(containingType, modifiers, typeSyntax, name, reference, location);
			}
			return new GlobalExpressionVariable.InferrableGlobalExpressionVariable(containingType, modifiers, typeSyntax, name, reference, location, containingFieldOpt, nodeToBind);
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x06006DE2 RID: 28130 RVA: 0x00194820 File Offset: 0x00192A20
		protected override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			get
			{
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x06006DE3 RID: 28131 RVA: 0x00194838 File Offset: 0x00192A38
		protected override TypeSyntax TypeSyntax
		{
			get
			{
				SyntaxReference typeSyntaxOpt = this._typeSyntaxOpt;
				return (TypeSyntax)((typeSyntaxOpt != null) ? typeSyntaxOpt.GetSyntax(default(CancellationToken)) : null);
			}
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x06006DE4 RID: 28132 RVA: 0x00194868 File Offset: 0x00192A68
		protected override SyntaxTokenList ModifiersTokenList
		{
			get
			{
				return default(SyntaxTokenList);
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x06006DE5 RID: 28133 RVA: 0x0019487E File Offset: 0x00192A7E
		public override bool HasInitializer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x00194881 File Offset: 0x00192A81
		protected override ConstantValue MakeConstantValue(HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics)
		{
			return null;
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x00194884 File Offset: 0x00192A84
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			if (this._lazyType != null)
			{
				return this._lazyType.Value;
			}
			TypeSyntax typeSyntax = this.TypeSyntax;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			Binder binder = declaringCompilation.GetBinderFactory(base.SyntaxTree, false).GetBinder(typeSyntax ?? base.SyntaxNode, null, null);
			bool flag;
			TypeWithAnnotations typeWithAnnotations;
			if (typeSyntax != null)
			{
				typeWithAnnotations = binder.BindTypeOrVarKeyword(typeSyntax, instance, out flag);
			}
			else
			{
				flag = true;
				typeWithAnnotations = default(TypeWithAnnotations);
			}
			if (flag && !fieldsBeingBound.ContainsReference(this))
			{
				this.InferFieldType(fieldsBeingBound, binder);
			}
			else
			{
				if (flag)
				{
					instance.Add(ErrorCode.ERR_RecursivelyTypedVariable, this.ErrorLocation, new object[] { this });
					typeWithAnnotations = TypeWithAnnotations.Create(binder.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
				this.SetType(declaringCompilation, instance, typeWithAnnotations);
			}
			instance.Free();
			return this._lazyType.Value;
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x00194968 File Offset: 0x00192B68
		private TypeWithAnnotations SetType(CSharpCompilation compilation, BindingDiagnosticBag diagnostics, TypeWithAnnotations type)
		{
			TypeWithAnnotations.Boxed lazyType = this._lazyType;
			if (lazyType != null)
			{
				TypeWithAnnotations value = lazyType.Value;
			}
			if (Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyType, new TypeWithAnnotations.Boxed(type), null) == null)
			{
				base.TypeChecks(type.Type, diagnostics);
				this.AddDeclarationDiagnostics(diagnostics);
				this.state.NotePartComplete(CompletionPart.Type);
			}
			return this._lazyType.Value;
		}

		// Token: 0x06006DE9 RID: 28137 RVA: 0x001949C8 File Offset: 0x00192BC8
		internal TypeWithAnnotations SetTypeWithAnnotations(TypeWithAnnotations type, BindingDiagnosticBag diagnostics)
		{
			return this.SetType(this.DeclaringCompilation, diagnostics, type);
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x001949D8 File Offset: 0x00192BD8
		protected virtual void InferFieldType(ConsList<FieldSymbol> fieldsBeingBound, Binder binder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001B1D RID: 6941
		private TypeWithAnnotations.Boxed _lazyType;

		// Token: 0x04001B1E RID: 6942
		private readonly SyntaxReference _typeSyntaxOpt;

		// Token: 0x02000910 RID: 2320
		private class InferrableGlobalExpressionVariable : GlobalExpressionVariable
		{
			// Token: 0x06008FA5 RID: 36773 RVA: 0x001F9451 File Offset: 0x001F7651
			internal InferrableGlobalExpressionVariable(SourceMemberContainerTypeSymbol containingType, DeclarationModifiers modifiers, TypeSyntax typeSyntax, string name, SyntaxReference syntax, Location location, FieldSymbol containingFieldOpt, SyntaxNode nodeToBind)
				: base(containingType, modifiers, typeSyntax, name, syntax, location)
			{
				this._containingFieldOpt = containingFieldOpt;
				this._nodeToBind = nodeToBind.GetReference();
			}

			// Token: 0x06008FA6 RID: 36774 RVA: 0x001F9478 File Offset: 0x001F7678
			protected override void InferFieldType(ConsList<FieldSymbol> fieldsBeingBound, Binder binder)
			{
				SyntaxNode syntax = this._nodeToBind.GetSyntax(default(CancellationToken));
				if (this._containingFieldOpt != null && syntax.Kind() != SyntaxKind.VariableDeclarator)
				{
					binder = binder.WithContainingMemberOrLambda(this._containingFieldOpt).WithAdditionalFlags(BinderFlags.FieldInitializer);
				}
				fieldsBeingBound = new ConsList<FieldSymbol>(this, fieldsBeingBound);
				binder = new ImplicitlyTypedFieldBinder(binder, fieldsBeingBound);
				if (syntax.Kind() == SyntaxKind.VariableDeclarator)
				{
					binder.BindDeclaratorArguments((VariableDeclaratorSyntax)syntax, BindingDiagnosticBag.Discarded);
					return;
				}
				binder.BindExpression((ExpressionSyntax)syntax, BindingDiagnosticBag.Discarded);
			}

			// Token: 0x0400275C RID: 10076
			private readonly FieldSymbol _containingFieldOpt;

			// Token: 0x0400275D RID: 10077
			private readonly SyntaxReference _nodeToBind;
		}
	}
}
