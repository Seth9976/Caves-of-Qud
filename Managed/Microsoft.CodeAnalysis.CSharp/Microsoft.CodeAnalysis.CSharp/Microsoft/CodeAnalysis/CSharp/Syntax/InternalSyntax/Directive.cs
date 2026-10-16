using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200035B RID: 859
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct Directive
	{
		// Token: 0x0600484D RID: 18509 RVA: 0x0011CB33 File Offset: 0x0011AD33
		internal Directive(DirectiveTriviaSyntax node)
		{
			this._node = node;
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x0011CB3C File Offset: 0x0011AD3C
		public SyntaxKind Kind
		{
			get
			{
				return this._node.Kind;
			}
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x0011CB4C File Offset: 0x0011AD4C
		public bool IncrementallyEquivalent(Directive other)
		{
			if (this.Kind != other.Kind)
			{
				return false;
			}
			bool isActive = this.IsActive;
			bool isActive2 = other.IsActive;
			if (!isActive && !isActive2)
			{
				return true;
			}
			if (isActive != isActive2)
			{
				return false;
			}
			SyntaxKind kind = this.Kind;
			if (kind - SyntaxKind.IfDirectiveTrivia > 2)
			{
				return kind - SyntaxKind.DefineDirectiveTrivia > 1 || this.GetIdentifier() == other.GetIdentifier();
			}
			return this.BranchTaken == other.BranchTaken;
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x0011CBC8 File Offset: 0x0011ADC8
		internal string GetDebuggerDisplay()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this._node.WriteTo(stringWriter, false, false);
			return stringWriter.ToString();
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x0011CBF4 File Offset: 0x0011ADF4
		internal string GetIdentifier()
		{
			SyntaxKind kind = this._node.Kind;
			if (kind == SyntaxKind.DefineDirectiveTrivia)
			{
				return ((DefineDirectiveTriviaSyntax)this._node).Name.ValueText;
			}
			if (kind != SyntaxKind.UndefDirectiveTrivia)
			{
				return null;
			}
			return ((UndefDirectiveTriviaSyntax)this._node).Name.ValueText;
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x0011CC4C File Offset: 0x0011AE4C
		internal bool IsActive
		{
			get
			{
				return this._node.IsActive;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x0011CC5C File Offset: 0x0011AE5C
		internal bool BranchTaken
		{
			get
			{
				BranchingDirectiveTriviaSyntax branchingDirectiveTriviaSyntax = this._node as BranchingDirectiveTriviaSyntax;
				return branchingDirectiveTriviaSyntax != null && branchingDirectiveTriviaSyntax.BranchTaken;
			}
		}

		// Token: 0x0400153C RID: 5436
		private readonly DirectiveTriviaSyntax _node;
	}
}
