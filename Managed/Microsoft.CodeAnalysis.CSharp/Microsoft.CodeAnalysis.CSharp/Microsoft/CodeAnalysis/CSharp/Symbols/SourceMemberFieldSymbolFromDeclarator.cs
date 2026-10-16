using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000541 RID: 1345
	internal class SourceMemberFieldSymbolFromDeclarator : SourceMemberFieldSymbol
	{
		// Token: 0x06007137 RID: 28983 RVA: 0x001A4380 File Offset: 0x001A2580
		internal SourceMemberFieldSymbolFromDeclarator(SourceMemberContainerTypeSymbol containingType, VariableDeclaratorSyntax declarator, DeclarationModifiers modifiers, bool modifierErrors, BindingDiagnosticBag diagnostics)
			: base(containingType, modifiers, declarator.Identifier.ValueText, declarator.GetReference(), declarator.Identifier.GetLocation())
		{
			this._hasInitializer = declarator.Initializer != null;
			base.CheckAccessibility(diagnostics);
			if (!modifierErrors)
			{
				base.ReportModifiersDiagnostics(diagnostics);
			}
			if (containingType.IsInterface)
			{
				if (this.IsStatic)
				{
					Binder.CheckFeatureAvailability(declarator, MessageID.IDS_DefaultInterfaceImplementation, diagnostics, this.ErrorLocation);
					if (!this.ContainingAssembly.RuntimeSupportsDefaultInterfaceImplementation)
					{
						diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation, this.ErrorLocation);
						return;
					}
				}
				else
				{
					diagnostics.Add(ErrorCode.ERR_InterfacesCantContainFields, this.ErrorLocation);
				}
			}
		}

		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06007138 RID: 28984 RVA: 0x001A4432 File Offset: 0x001A2632
		protected sealed override TypeSyntax TypeSyntax
		{
			get
			{
				return SourceMemberFieldSymbolFromDeclarator.GetFieldDeclaration(this.VariableDeclaratorNode).Declaration.Type;
			}
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x001A4449 File Offset: 0x001A2649
		protected sealed override SyntaxTokenList ModifiersTokenList
		{
			get
			{
				return SourceMemberFieldSymbolFromDeclarator.GetFieldDeclaration(this.VariableDeclaratorNode).Modifiers;
			}
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x0600713A RID: 28986 RVA: 0x001A445B File Offset: 0x001A265B
		public sealed override bool HasInitializer
		{
			get
			{
				return this._hasInitializer;
			}
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x0600713B RID: 28987 RVA: 0x001A4463 File Offset: 0x001A2663
		protected VariableDeclaratorSyntax VariableDeclaratorNode
		{
			get
			{
				return (VariableDeclaratorSyntax)base.SyntaxNode;
			}
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x001A4470 File Offset: 0x001A2670
		private static BaseFieldDeclarationSyntax GetFieldDeclaration(CSharpSyntaxNode declarator)
		{
			return (BaseFieldDeclarationSyntax)declarator.Parent.Parent;
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x0600713D RID: 28989 RVA: 0x001A4484 File Offset: 0x001A2684
		protected override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			get
			{
				if (this.containingType.AnyMemberHasAttributes)
				{
					return SourceMemberFieldSymbolFromDeclarator.GetFieldDeclaration(base.SyntaxNode).AttributeLists;
				}
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x0600713E RID: 28990 RVA: 0x001A44B8 File Offset: 0x001A26B8
		internal override bool HasPointerType
		{
			get
			{
				if (this._lazyType != null)
				{
					SymbolKind kind = this._lazyType.Value.DefaultType.Kind;
					return kind == SymbolKind.PointerType || kind == SymbolKind.FunctionPointerType;
				}
				return this.IsPointerFieldSyntactically();
			}
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x001A4504 File Offset: 0x001A2704
		private bool IsPointerFieldSyntactically()
		{
			SyntaxKind syntaxKind = SourceMemberFieldSymbolFromDeclarator.GetFieldDeclaration(this.VariableDeclaratorNode).Declaration.Type.Kind();
			bool flag = syntaxKind == SyntaxKind.PointerType || syntaxKind == SyntaxKind.FunctionPointerType;
			return flag || this.IsFixedSizeBuffer;
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x001A4554 File Offset: 0x001A2754
		internal sealed override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			if (this._lazyType != null)
			{
				return this._lazyType.Value;
			}
			VariableDeclaratorSyntax variableDeclaratorNode = this.VariableDeclaratorNode;
			BaseFieldDeclarationSyntax fieldDeclaration = SourceMemberFieldSymbolFromDeclarator.GetFieldDeclaration(variableDeclaratorNode);
			TypeSyntax type = fieldDeclaration.Declaration.Type;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			BindingDiagnosticBag instance2 = BindingDiagnosticBag.GetInstance();
			Symbol associatedSymbol = this.AssociatedSymbol;
			TypeWithAnnotations typeWithAnnotations;
			if (associatedSymbol != null && associatedSymbol.Kind == SymbolKind.Event)
			{
				EventSymbol eventSymbol = (EventSymbol)associatedSymbol;
				if (eventSymbol.IsWindowsRuntimeEvent)
				{
					NamedTypeSymbol wellKnownType = this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T);
					Binder.ReportUseSite(wellKnownType, instance2, this.ErrorLocation);
					typeWithAnnotations = TypeWithAnnotations.Create(wellKnownType.Construct(ImmutableArray.Create<TypeWithAnnotations>(eventSymbol.TypeWithAnnotations)), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
				else
				{
					typeWithAnnotations = eventSymbol.TypeWithAnnotations;
				}
			}
			else
			{
				Binder binder = declaringCompilation.GetBinderFactory(base.SyntaxTree, false).GetBinder(type, null, null);
				binder = binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.SuppressConstraintChecks, this);
				if (!this.ContainingType.IsScriptClass)
				{
					typeWithAnnotations = binder.BindType(type, instance2, null, false);
				}
				else
				{
					bool flag;
					typeWithAnnotations = binder.BindTypeOrVarKeyword(type, instance, out flag);
					if (flag)
					{
						if (this.IsConst)
						{
							instance2.Add(ErrorCode.ERR_ImplicitlyTypedVariableCannotBeConst, type.Location);
						}
						if (fieldsBeingBound.ContainsReference(this))
						{
							instance.Add(ErrorCode.ERR_RecursivelyTypedVariable, this.ErrorLocation, new object[] { this });
							typeWithAnnotations = default(TypeWithAnnotations);
						}
						else if (fieldDeclaration.Declaration.Variables.Count > 1)
						{
							instance2.Add(ErrorCode.ERR_ImplicitlyTypedVariableMultipleDeclarator, type.Location);
						}
						else if (this.IsConst && this.ContainingType.IsScriptClass)
						{
							typeWithAnnotations = default(TypeWithAnnotations);
						}
						else
						{
							fieldsBeingBound = new ConsList<FieldSymbol>(this, fieldsBeingBound);
							BoundExpression boundExpression = new ImplicitlyTypedFieldBinder(binder, fieldsBeingBound).BindInferredVariableInitializer(instance, RefKind.None, variableDeclaratorNode.Initializer, variableDeclaratorNode);
							if (boundExpression != null)
							{
								if (boundExpression.Type != null && !boundExpression.Type.IsErrorType())
								{
									typeWithAnnotations = TypeWithAnnotations.Create(boundExpression.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
								}
								this._lazyFieldTypeInferred = 1;
							}
						}
						if (!typeWithAnnotations.HasType)
						{
							typeWithAnnotations = TypeWithAnnotations.Create(binder.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
						}
					}
				}
				if (this.IsFixedSizeBuffer)
				{
					typeWithAnnotations = TypeWithAnnotations.Create(new PointerTypeSymbol(typeWithAnnotations), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					if (this.ContainingType.TypeKind != TypeKind.Struct)
					{
						instance.Add(ErrorCode.ERR_FixedNotInStruct, this.ErrorLocation);
					}
					if (((PointerTypeSymbol)typeWithAnnotations.Type).PointedAtType.FixedBufferElementSizeInBytes() == 0)
					{
						Location location = type.Location;
						instance.Add(ErrorCode.ERR_IllegalFixedType, location);
					}
					if (!binder.InUnsafeRegion)
					{
						instance2.Add(ErrorCode.ERR_UnsafeNeeded, variableDeclaratorNode.Location);
					}
				}
			}
			if (Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyType, new TypeWithAnnotations.Boxed(typeWithAnnotations.WithModifiers(base.RequiredCustomModifiers)), null) == null)
			{
				base.TypeChecks(typeWithAnnotations.Type, instance);
				this.AddDeclarationDiagnostics(instance);
				if (fieldDeclaration.Declaration.Variables[0] == variableDeclaratorNode)
				{
					this.AddDeclarationDiagnostics(instance2);
				}
				this.state.NotePartComplete(CompletionPart.Type);
			}
			instance.Free();
			instance2.Free();
			return this._lazyType.Value;
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x001A48A5 File Offset: 0x001A2AA5
		internal bool FieldTypeInferred(ConsList<FieldSymbol> fieldsBeingBound)
		{
			if (!this.ContainingType.IsScriptClass)
			{
				return false;
			}
			this.GetFieldType(fieldsBeingBound);
			return this._lazyFieldTypeInferred != 0 || Volatile.Read(ref this._lazyFieldTypeInferred) != 0;
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x001A48D6 File Offset: 0x001A2AD6
		protected sealed override ConstantValue MakeConstantValue(HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics)
		{
			if (!this.IsConst || this.VariableDeclaratorNode.Initializer == null)
			{
				return null;
			}
			return ConstantValueUtils.EvaluateFieldConstant(this, this.VariableDeclaratorNode.Initializer, dependencies, earlyDecodingWellKnownAttributes, diagnostics);
		}

		// Token: 0x06007143 RID: 28995 RVA: 0x001A4904 File Offset: 0x001A2B04
		internal override bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.SyntaxTree != tree)
			{
				return false;
			}
			if (definedWithinSpan == null)
			{
				return true;
			}
			BaseFieldDeclarationSyntax fieldDeclaration = SourceMemberFieldSymbolFromDeclarator.GetFieldDeclaration(base.SyntaxNode);
			return fieldDeclaration.SyntaxTree.HasCompilationUnitRoot && fieldDeclaration.Span.IntersectsWith(definedWithinSpan.Value);
		}

		// Token: 0x06007144 RID: 28996 RVA: 0x001A4957 File Offset: 0x001A2B57
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			if (!this.IsFixedSizeBuffer)
			{
				base.Type.CheckAllConstraints(this.DeclaringCompilation, conversions, this.ErrorLocation, diagnostics);
			}
			base.AfterAddingTypeMembersChecks(conversions, diagnostics);
		}

		// Token: 0x04001BA4 RID: 7076
		private readonly bool _hasInitializer;

		// Token: 0x04001BA5 RID: 7077
		private TypeWithAnnotations.Boxed _lazyType;

		// Token: 0x04001BA6 RID: 7078
		private int _lazyFieldTypeInferred;
	}
}
