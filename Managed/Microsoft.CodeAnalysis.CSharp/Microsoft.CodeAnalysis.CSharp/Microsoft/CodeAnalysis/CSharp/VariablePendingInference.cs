using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000FB RID: 251
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class VariablePendingInference : BoundExpression
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x00064E05 File Offset: 0x00063005
		internal BoundExpression SetInferredTypeWithAnnotations(TypeWithAnnotations type, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnosticsOpt)
		{
			return this.SetInferredTypeWithAnnotations(type, null, diagnosticsOpt);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00064E10 File Offset: 0x00063010
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal BoundExpression SetInferredTypeWithAnnotations(TypeWithAnnotations type, Binder binderOpt, BindingDiagnosticBag diagnosticsOpt)
		{
			bool flag = !type.HasType;
			if (flag)
			{
				type = TypeWithAnnotations.Create(binderOpt.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			SymbolKind kind = this.VariableSymbol.Kind;
			if (kind == SymbolKind.Field)
			{
				GlobalExpressionVariable globalExpressionVariable = (GlobalExpressionVariable)this.VariableSymbol;
				BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(DiagnosticBag.GetInstance());
				if (flag)
				{
					this.ReportInferenceFailure(bindingDiagnosticBag);
				}
				type = globalExpressionVariable.SetTypeWithAnnotations(type, bindingDiagnosticBag);
				bindingDiagnosticBag.Free();
				return new BoundFieldAccess(this.Syntax, this.ReceiverOpt, globalExpressionVariable, null, LookupResultKind.Viable, true, type.Type, base.HasErrors || flag);
			}
			if (kind == SymbolKind.Local)
			{
				SourceLocalSymbol sourceLocalSymbol = (SourceLocalSymbol)this.VariableSymbol;
				if (((diagnosticsOpt != null) ? diagnosticsOpt.DiagnosticBag : null) != null)
				{
					if (flag)
					{
						this.ReportInferenceFailure(diagnosticsOpt);
					}
					else
					{
						SyntaxNode syntaxNode = ((this.Syntax.Kind() == SyntaxKind.DeclarationExpression) ? ((DeclarationExpressionSyntax)this.Syntax).Type : this.Syntax);
						Binder.CheckRestrictedTypeInAsyncMethod(sourceLocalSymbol.ContainingSymbol, type.Type, diagnosticsOpt, syntaxNode);
					}
				}
				sourceLocalSymbol.SetTypeWithAnnotations(type);
				return new BoundLocal(this.Syntax, sourceLocalSymbol, BoundLocalDeclarationKind.WithInferredType, null, false, type.Type, base.HasErrors || flag).WithWasConverted();
			}
			throw ExceptionUtilities.UnexpectedValue(this.VariableSymbol.Kind);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00064F68 File Offset: 0x00063168
		internal BoundExpression FailInference(Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnosticsOpt)
		{
			return this.SetInferredTypeWithAnnotations(default(TypeWithAnnotations), binder, diagnosticsOpt);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00064F88 File Offset: 0x00063188
		private void ReportInferenceFailure(BindingDiagnosticBag diagnostics)
		{
			SyntaxKind syntaxKind = this.Syntax.Kind();
			SingleVariableDesignationSyntax singleVariableDesignationSyntax;
			if (syntaxKind != SyntaxKind.SingleVariableDesignation)
			{
				if (syntaxKind != SyntaxKind.DeclarationExpression)
				{
					throw ExceptionUtilities.Unreachable;
				}
				singleVariableDesignationSyntax = (SingleVariableDesignationSyntax)((DeclarationExpressionSyntax)this.Syntax).Designation;
			}
			else
			{
				singleVariableDesignationSyntax = (SingleVariableDesignationSyntax)this.Syntax;
			}
			Binder.Error(diagnostics, this.InferenceFailedError, singleVariableDesignationSyntax.Identifier, new object[] { singleVariableDesignationSyntax.Identifier.ValueText });
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600131C RID: 4892
		protected abstract ErrorCode InferenceFailedError { get; }

		// Token: 0x0600131D RID: 4893 RVA: 0x00065006 File Offset: 0x00063206
		protected VariablePendingInference(BoundKind kind, SyntaxNode syntax, Symbol variableSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, bool hasErrors = false)
			: base(kind, syntax, null, hasErrors)
		{
			this.VariableSymbol = variableSymbol;
			this.ReceiverOpt = receiverOpt;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00065022 File Offset: 0x00063222
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0006502A File Offset: 0x0006322A
		public Symbol VariableSymbol { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00065032 File Offset: 0x00063232
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}
	}
}
