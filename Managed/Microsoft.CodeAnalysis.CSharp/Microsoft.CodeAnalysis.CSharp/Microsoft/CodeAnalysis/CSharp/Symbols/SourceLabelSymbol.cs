using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200053B RID: 1339
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SourceLabelSymbol : LabelSymbol
	{
		// Token: 0x06007037 RID: 28727 RVA: 0x0019C061 File Offset: 0x0019A261
		public SourceLabelSymbol(MethodSymbol containingMethod, SyntaxNodeOrToken identifierNodeOrToken, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue switchCaseLabelConstant = null)
		{
			this._containingMethod = containingMethod;
			this._identifierNodeOrToken = identifierNodeOrToken;
			this._switchCaseLabelConstant = switchCaseLabelConstant;
		}

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06007038 RID: 28728 RVA: 0x0019C080 File Offset: 0x0019A280
		public override string Name
		{
			get
			{
				string text;
				if ((text = this._lazyName) == null)
				{
					text = (this._lazyName = this.MakeLabelName());
				}
				return text;
			}
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x0019C0A8 File Offset: 0x0019A2A8
		private string MakeLabelName()
		{
			SyntaxNode syntaxNode = this._identifierNodeOrToken.AsNode();
			if (syntaxNode != null)
			{
				if (syntaxNode.Kind() == SyntaxKind.DefaultSwitchLabel)
				{
					return ((DefaultSwitchLabelSyntax)syntaxNode).Keyword.ToString();
				}
				return syntaxNode.ToString();
			}
			else
			{
				SyntaxToken syntaxToken = this._identifierNodeOrToken.AsToken();
				if (syntaxToken.Kind() != SyntaxKind.None)
				{
					return syntaxToken.ValueText;
				}
				ConstantValue switchCaseLabelConstant = this._switchCaseLabelConstant;
				return ((switchCaseLabelConstant != null) ? switchCaseLabelConstant.ToString() : null) ?? "";
			}
		}

		// Token: 0x0600703A RID: 28730 RVA: 0x0019C12C File Offset: 0x0019A32C
		public SourceLabelSymbol(MethodSymbol containingMethod, ConstantValue switchCaseLabelConstant)
		{
			this._containingMethod = containingMethod;
			this._identifierNodeOrToken = default(SyntaxToken);
			this._switchCaseLabelConstant = switchCaseLabelConstant;
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x0600703B RID: 28731 RVA: 0x0019C161 File Offset: 0x0019A361
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (!this._identifierNodeOrToken.IsToken || this._identifierNodeOrToken.Parent != null)
				{
					return ImmutableArray.Create<Location>(this._identifierNodeOrToken.GetLocation());
				}
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x0600703C RID: 28732 RVA: 0x0019C194 File Offset: 0x0019A394
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				CSharpSyntaxNode csharpSyntaxNode = null;
				if (this._identifierNodeOrToken.IsToken)
				{
					if (this._identifierNodeOrToken.Parent != null)
					{
						csharpSyntaxNode = this._identifierNodeOrToken.Parent.FirstAncestorOrSelf<LabeledStatementSyntax>(null, true);
					}
				}
				else
				{
					csharpSyntaxNode = this._identifierNodeOrToken.AsNode().FirstAncestorOrSelf<SwitchLabelSyntax>(null, true);
				}
				if (csharpSyntaxNode != null)
				{
					return ImmutableArray.Create<SyntaxReference>(csharpSyntaxNode.GetReference());
				}
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x0600703D RID: 28733 RVA: 0x0019C1F9 File Offset: 0x0019A3F9
		public override MethodSymbol ContainingMethod
		{
			get
			{
				return this._containingMethod;
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x0600703E RID: 28734 RVA: 0x0019C201 File Offset: 0x0019A401
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingMethod;
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x0600703F RID: 28735 RVA: 0x0019C209 File Offset: 0x0019A409
		internal override SyntaxNodeOrToken IdentifierNodeOrToken
		{
			get
			{
				return this._identifierNodeOrToken;
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x06007040 RID: 28736 RVA: 0x0019C211 File Offset: 0x0019A411
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue SwitchCaseLabelConstant
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._switchCaseLabelConstant;
			}
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x0019C21C File Offset: 0x0019A41C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (obj == this)
			{
				return true;
			}
			SourceLabelSymbol sourceLabelSymbol = obj as SourceLabelSymbol;
			return sourceLabelSymbol != null && sourceLabelSymbol._identifierNodeOrToken.Kind() != SyntaxKind.None && sourceLabelSymbol._identifierNodeOrToken.Equals(this._identifierNodeOrToken) && sourceLabelSymbol._containingMethod.Equals(this._containingMethod, compareKind);
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x0019C26D File Offset: 0x0019A46D
		public override int GetHashCode()
		{
			return this._identifierNodeOrToken.GetHashCode();
		}

		// Token: 0x04001B7F RID: 7039
		private readonly MethodSymbol _containingMethod;

		// Token: 0x04001B80 RID: 7040
		private readonly SyntaxNodeOrToken _identifierNodeOrToken;

		// Token: 0x04001B81 RID: 7041
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly ConstantValue _switchCaseLabelConstant;

		// Token: 0x04001B82 RID: 7042
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private string _lazyName;
	}
}
