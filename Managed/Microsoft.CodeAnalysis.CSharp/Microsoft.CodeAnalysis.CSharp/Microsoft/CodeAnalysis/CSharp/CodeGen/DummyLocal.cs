using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005FB RID: 1531
	internal sealed class DummyLocal : LocalSymbol
	{
		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x0600814D RID: 33101 RVA: 0x001D702C File Offset: 0x001D522C
		internal override bool IsImportedFromMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700264D RID: 9805
		// (get) Token: 0x0600814E RID: 33102 RVA: 0x001D702F File Offset: 0x001D522F
		internal override LocalDeclarationKind DeclarationKind
		{
			get
			{
				return LocalDeclarationKind.None;
			}
		}

		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x0600814F RID: 33103 RVA: 0x001D7032 File Offset: 0x001D5232
		internal override SynthesizedLocalKind SynthesizedKind
		{
			get
			{
				return SynthesizedLocalKind.OptimizerTemp;
			}
		}

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x06008150 RID: 33104 RVA: 0x001D7036 File Offset: 0x001D5236
		internal override SyntaxNode ScopeDesignatorOpt
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x001D7039 File Offset: 0x001D5239
		internal override LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x06008152 RID: 33106 RVA: 0x001D7040 File Offset: 0x001D5240
		internal override SyntaxToken IdentifierToken
		{
			get
			{
				return default(SyntaxToken);
			}
		}

		// Token: 0x17002651 RID: 9809
		// (get) Token: 0x06008153 RID: 33107 RVA: 0x001D7056 File Offset: 0x001D5256
		internal override bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x06008154 RID: 33108 RVA: 0x001D7059 File Offset: 0x001D5259
		public override Symbol ContainingSymbol
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x06008155 RID: 33109 RVA: 0x001D7060 File Offset: 0x001D5260
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002654 RID: 9812
		// (get) Token: 0x06008156 RID: 33110 RVA: 0x001D7067 File Offset: 0x001D5267
		public override ImmutableArray<Location> Locations
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002655 RID: 9813
		// (get) Token: 0x06008157 RID: 33111 RVA: 0x001D706E File Offset: 0x001D526E
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x001D7075 File Offset: 0x001D5275
		internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002656 RID: 9814
		// (get) Token: 0x06008159 RID: 33113 RVA: 0x001D707C File Offset: 0x001D527C
		internal override bool IsCompilerGenerated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600815A RID: 33114 RVA: 0x001D707F File Offset: 0x001D527F
		internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x001D7086 File Offset: 0x001D5286
		internal override SyntaxNode GetDeclaratorSyntax()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x0600815C RID: 33116 RVA: 0x001D708D File Offset: 0x001D528D
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17002658 RID: 9816
		// (get) Token: 0x0600815D RID: 33117 RVA: 0x001D7090 File Offset: 0x001D5290
		internal override uint ValEscapeScope
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x0600815E RID: 33118 RVA: 0x001D7097 File Offset: 0x001D5297
		internal override uint RefEscapeScope
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}
	}
}
