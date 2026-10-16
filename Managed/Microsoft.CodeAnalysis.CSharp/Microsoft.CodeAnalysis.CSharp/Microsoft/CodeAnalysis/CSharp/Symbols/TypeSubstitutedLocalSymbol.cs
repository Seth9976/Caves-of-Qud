using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A0 RID: 1440
	internal sealed class TypeSubstitutedLocalSymbol : LocalSymbol
	{
		// Token: 0x06007871 RID: 30833 RVA: 0x001B927D File Offset: 0x001B747D
		public TypeSubstitutedLocalSymbol(LocalSymbol originalVariable, TypeWithAnnotations type, Symbol containingSymbol)
		{
			this._originalVariable = originalVariable;
			this._type = type;
			this._containingSymbol = containingSymbol;
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06007872 RID: 30834 RVA: 0x001B929A File Offset: 0x001B749A
		internal override bool IsImportedFromMetadata
		{
			get
			{
				return this._originalVariable.IsImportedFromMetadata;
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x06007873 RID: 30835 RVA: 0x001B92A7 File Offset: 0x001B74A7
		internal override LocalDeclarationKind DeclarationKind
		{
			get
			{
				return this._originalVariable.DeclarationKind;
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x06007874 RID: 30836 RVA: 0x001B92B4 File Offset: 0x001B74B4
		internal override SynthesizedLocalKind SynthesizedKind
		{
			get
			{
				return this._originalVariable.SynthesizedKind;
			}
		}

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06007875 RID: 30837 RVA: 0x001B92C1 File Offset: 0x001B74C1
		internal override SyntaxNode ScopeDesignatorOpt
		{
			get
			{
				return this._originalVariable.ScopeDesignatorOpt;
			}
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x06007876 RID: 30838 RVA: 0x001B92CE File Offset: 0x001B74CE
		public override string Name
		{
			get
			{
				return this._originalVariable.Name;
			}
		}

		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x06007877 RID: 30839 RVA: 0x001B92DB File Offset: 0x001B74DB
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x06007878 RID: 30840 RVA: 0x001B92E3 File Offset: 0x001B74E3
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._originalVariable.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x06007879 RID: 30841 RVA: 0x001B92F0 File Offset: 0x001B74F0
		internal override SyntaxNode GetDeclaratorSyntax()
		{
			return this._originalVariable.GetDeclaratorSyntax();
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x0600787A RID: 30842 RVA: 0x001B92FD File Offset: 0x001B74FD
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._originalVariable.Locations;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x001B930A File Offset: 0x001B750A
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x0600787C RID: 30844 RVA: 0x001B9312 File Offset: 0x001B7512
		internal override SyntaxToken IdentifierToken
		{
			get
			{
				return this._originalVariable.IdentifierToken;
			}
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x0600787D RID: 30845 RVA: 0x001B931F File Offset: 0x001B751F
		internal override bool IsCompilerGenerated
		{
			get
			{
				return this._originalVariable.IsCompilerGenerated;
			}
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x001B932C File Offset: 0x001B752C
		internal override bool IsPinned
		{
			get
			{
				return this._originalVariable.IsPinned;
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x0600787F RID: 30847 RVA: 0x001B9339 File Offset: 0x001B7539
		public override RefKind RefKind
		{
			get
			{
				return this._originalVariable.RefKind;
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x06007880 RID: 30848 RVA: 0x001B9346 File Offset: 0x001B7546
		internal override uint ValEscapeScope
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x001B934D File Offset: 0x001B754D
		internal override uint RefEscapeScope
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x001B9354 File Offset: 0x001B7554
		internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics)
		{
			return this._originalVariable.GetConstantValue(node, inProgress, diagnostics);
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x001B9364 File Offset: 0x001B7564
		internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
		{
			return this._originalVariable.GetConstantValueDiagnostics(boundInitValue);
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x001B9372 File Offset: 0x001B7572
		internal override LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax)
		{
			return new TypeSubstitutedLocalSymbol(((SynthesizedLocal)this._originalVariable).WithSynthesizedLocalKindAndSyntax(kind, syntax), this._type, this._containingSymbol);
		}

		// Token: 0x04001CFD RID: 7421
		private readonly LocalSymbol _originalVariable;

		// Token: 0x04001CFE RID: 7422
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001CFF RID: 7423
		private readonly Symbol _containingSymbol;
	}
}
