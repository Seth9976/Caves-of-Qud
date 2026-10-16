using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000532 RID: 1330
	internal abstract class SourceEnumConstantSymbol : SourceFieldSymbolWithSyntaxReference
	{
		// Token: 0x06006FA1 RID: 28577 RVA: 0x0019A354 File Offset: 0x00198554
		public static SourceEnumConstantSymbol CreateExplicitValuedConstant(SourceMemberContainerTypeSymbol containingEnum, EnumMemberDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			EqualsValueClauseSyntax equalsValue = syntax.EqualsValue;
			return new SourceEnumConstantSymbol.ExplicitValuedEnumConstantSymbol(containingEnum, syntax, equalsValue, diagnostics);
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x0019A371 File Offset: 0x00198571
		public static SourceEnumConstantSymbol CreateImplicitValuedConstant(SourceMemberContainerTypeSymbol containingEnum, EnumMemberDeclarationSyntax syntax, SourceEnumConstantSymbol otherConstant, int otherConstantOffset, BindingDiagnosticBag diagnostics)
		{
			if (otherConstant == null)
			{
				return new SourceEnumConstantSymbol.ZeroValuedEnumConstantSymbol(containingEnum, syntax, diagnostics);
			}
			return new SourceEnumConstantSymbol.ImplicitValuedEnumConstantSymbol(containingEnum, syntax, otherConstant, (uint)otherConstantOffset, diagnostics);
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x0019A38C File Offset: 0x0019858C
		protected SourceEnumConstantSymbol(SourceMemberContainerTypeSymbol containingEnum, EnumMemberDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
			: base(containingEnum, syntax.Identifier.ValueText, syntax.GetReference(), syntax.Identifier.GetLocation())
		{
			if (this.Name == "value__")
			{
				diagnostics.Add(ErrorCode.ERR_ReservedEnumerator, this.ErrorLocation, new object[] { "value__" });
			}
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x0019A3F4 File Offset: 0x001985F4
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return TypeWithAnnotations.Create(this.ContainingType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x06006FA5 RID: 28581 RVA: 0x0019A416 File Offset: 0x00198616
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x06006FA6 RID: 28582 RVA: 0x0019A419 File Offset: 0x00198619
		protected sealed override DeclarationModifiers Modifiers
		{
			get
			{
				return DeclarationModifiers.Static | DeclarationModifiers.Public | DeclarationModifiers.Const;
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06006FA7 RID: 28583 RVA: 0x0019A420 File Offset: 0x00198620
		public new EnumMemberDeclarationSyntax SyntaxNode
		{
			get
			{
				return (EnumMemberDeclarationSyntax)base.SyntaxNode;
			}
		}

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x0019A430 File Offset: 0x00198630
		protected override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			get
			{
				if (this.containingType.AnyMemberHasAttributes)
				{
					return this.SyntaxNode.AttributeLists;
				}
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x06006FA9 RID: 28585 RVA: 0x0019A460 File Offset: 0x00198660
		internal sealed override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this.state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.Attributes)
				{
					if (nextIncompletePart == CompletionPart.None)
					{
						break;
					}
					if (nextIncompletePart != CompletionPart.Attributes)
					{
						goto IL_0071;
					}
					this.GetAttributes();
				}
				else if (nextIncompletePart != CompletionPart.Type)
				{
					if (nextIncompletePart != CompletionPart.Members)
					{
						if (nextIncompletePart != CompletionPart.TypeMembers)
						{
							goto IL_0071;
						}
						this.GetConstantValue(ConstantFieldsInProgress.Empty, false);
					}
					else
					{
						this.state.NotePartComplete(CompletionPart.Members);
					}
				}
				else
				{
					this.state.NotePartComplete(CompletionPart.Type);
				}
				IL_0082:
				this.state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_0071:
				this.state.NotePartComplete(CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.StartBaseType | CompletionPart.FinishBaseType | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
				goto IL_0082;
			}
		}

		// Token: 0x02000919 RID: 2329
		private sealed class ZeroValuedEnumConstantSymbol : SourceEnumConstantSymbol
		{
			// Token: 0x06008FC5 RID: 36805 RVA: 0x001F9A32 File Offset: 0x001F7C32
			public ZeroValuedEnumConstantSymbol(SourceMemberContainerTypeSymbol containingEnum, EnumMemberDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
				: base(containingEnum, syntax, diagnostics)
			{
			}

			// Token: 0x06008FC6 RID: 36806 RVA: 0x001F9A3D File Offset: 0x001F7C3D
			protected override ConstantValue MakeConstantValue(HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics)
			{
				return Microsoft.CodeAnalysis.ConstantValue.Default(this.ContainingType.EnumUnderlyingType.SpecialType);
			}
		}

		// Token: 0x0200091A RID: 2330
		private sealed class ExplicitValuedEnumConstantSymbol : SourceEnumConstantSymbol
		{
			// Token: 0x06008FC7 RID: 36807 RVA: 0x001F9A54 File Offset: 0x001F7C54
			public ExplicitValuedEnumConstantSymbol(SourceMemberContainerTypeSymbol containingEnum, EnumMemberDeclarationSyntax syntax, EqualsValueClauseSyntax initializer, BindingDiagnosticBag diagnostics)
				: base(containingEnum, syntax, diagnostics)
			{
				this._equalsValueNodeRef = initializer.GetReference();
			}

			// Token: 0x06008FC8 RID: 36808 RVA: 0x001F9A6C File Offset: 0x001F7C6C
			protected override ConstantValue MakeConstantValue(HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics)
			{
				return ConstantValueUtils.EvaluateFieldConstant(this, (EqualsValueClauseSyntax)this._equalsValueNodeRef.GetSyntax(default(CancellationToken)), dependencies, earlyDecodingWellKnownAttributes, diagnostics);
			}

			// Token: 0x0400276E RID: 10094
			private readonly SyntaxReference _equalsValueNodeRef;
		}

		// Token: 0x0200091B RID: 2331
		private sealed class ImplicitValuedEnumConstantSymbol : SourceEnumConstantSymbol
		{
			// Token: 0x06008FC9 RID: 36809 RVA: 0x001F9A9B File Offset: 0x001F7C9B
			public ImplicitValuedEnumConstantSymbol(SourceMemberContainerTypeSymbol containingEnum, EnumMemberDeclarationSyntax syntax, SourceEnumConstantSymbol otherConstant, uint otherConstantOffset, BindingDiagnosticBag diagnostics)
				: base(containingEnum, syntax, diagnostics)
			{
				this._otherConstant = otherConstant;
				this._otherConstantOffset = otherConstantOffset;
			}

			// Token: 0x06008FCA RID: 36810 RVA: 0x001F9AB8 File Offset: 0x001F7CB8
			protected override ConstantValue MakeConstantValue(HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics)
			{
				ConstantValue constantValue = this._otherConstant.GetConstantValue(new ConstantFieldsInProgress(this, dependencies), earlyDecodingWellKnownAttributes);
				if (constantValue == Microsoft.CodeAnalysis.ConstantValue.Unset)
				{
					return Microsoft.CodeAnalysis.ConstantValue.Unset;
				}
				if (constantValue.IsBad)
				{
					return Microsoft.CodeAnalysis.ConstantValue.Bad;
				}
				ConstantValue constantValue2;
				if (EnumConstantHelper.OffsetValue(constantValue, this._otherConstantOffset, out constantValue2) == EnumOverflowKind.OverflowReport)
				{
					diagnostics.Add(ErrorCode.ERR_EnumeratorOverflow, this.Locations[0], new object[] { this });
				}
				return constantValue2;
			}

			// Token: 0x0400276F RID: 10095
			private readonly SourceEnumConstantSymbol _otherConstant;

			// Token: 0x04002770 RID: 10096
			private readonly uint _otherConstantOffset;
		}
	}
}
