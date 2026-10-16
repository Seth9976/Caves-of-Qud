using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000258 RID: 600
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class DirectiveTriviaSyntax : StructuredTriviaSyntax
	{
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000FC9F4 File Offset: 0x000FABF4
		public SyntaxToken DirectiveNameToken
		{
			get
			{
				SyntaxKind syntaxKind = base.Kind();
				if (syntaxKind <= SyntaxKind.ShebangDirectiveTrivia)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.IfDirectiveTrivia:
						return ((IfDirectiveTriviaSyntax)this).IfKeyword;
					case SyntaxKind.ElifDirectiveTrivia:
						return ((ElifDirectiveTriviaSyntax)this).ElifKeyword;
					case SyntaxKind.ElseDirectiveTrivia:
						return ((ElseDirectiveTriviaSyntax)this).ElseKeyword;
					case SyntaxKind.EndIfDirectiveTrivia:
						return ((EndIfDirectiveTriviaSyntax)this).EndIfKeyword;
					case SyntaxKind.RegionDirectiveTrivia:
						return ((RegionDirectiveTriviaSyntax)this).RegionKeyword;
					case SyntaxKind.EndRegionDirectiveTrivia:
						return ((EndRegionDirectiveTriviaSyntax)this).EndRegionKeyword;
					case SyntaxKind.DefineDirectiveTrivia:
						return ((DefineDirectiveTriviaSyntax)this).DefineKeyword;
					case SyntaxKind.UndefDirectiveTrivia:
						return ((UndefDirectiveTriviaSyntax)this).UndefKeyword;
					case SyntaxKind.ErrorDirectiveTrivia:
						return ((ErrorDirectiveTriviaSyntax)this).ErrorKeyword;
					case SyntaxKind.WarningDirectiveTrivia:
						return ((WarningDirectiveTriviaSyntax)this).WarningKeyword;
					case SyntaxKind.LineDirectiveTrivia:
						return ((LineDirectiveTriviaSyntax)this).LineKeyword;
					case SyntaxKind.PragmaWarningDirectiveTrivia:
						return ((PragmaWarningDirectiveTriviaSyntax)this).PragmaKeyword;
					case SyntaxKind.PragmaChecksumDirectiveTrivia:
						return ((PragmaChecksumDirectiveTriviaSyntax)this).PragmaKeyword;
					case SyntaxKind.ReferenceDirectiveTrivia:
						return ((ReferenceDirectiveTriviaSyntax)this).ReferenceKeyword;
					case SyntaxKind.BadDirectiveTrivia:
						return ((BadDirectiveTriviaSyntax)this).Identifier;
					default:
						if (syntaxKind == SyntaxKind.ShebangDirectiveTrivia)
						{
							return ((ShebangDirectiveTriviaSyntax)this).ExclamationToken;
						}
						break;
					}
				}
				else
				{
					if (syntaxKind == SyntaxKind.LoadDirectiveTrivia)
					{
						return ((LoadDirectiveTriviaSyntax)this).LoadKeyword;
					}
					if (syntaxKind == SyntaxKind.NullableDirectiveTrivia)
					{
						return ((NullableDirectiveTriviaSyntax)this).NullableKeyword;
					}
				}
				throw ExceptionUtilities.UnexpectedValue(base.Kind());
			}
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000FCB6C File Offset: 0x000FAD6C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public DirectiveTriviaSyntax GetNextDirective([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<DirectiveTriviaSyntax, bool> predicate = null)
		{
			SyntaxToken syntaxToken = this.ParentTrivia.Token;
			bool flag = false;
			while (syntaxToken.Kind() != SyntaxKind.None)
			{
				foreach (SyntaxTrivia syntaxTrivia in syntaxToken.LeadingTrivia)
				{
					if (flag)
					{
						if (syntaxTrivia.IsDirective)
						{
							DirectiveTriviaSyntax directiveTriviaSyntax = (DirectiveTriviaSyntax)syntaxTrivia.GetStructure();
							if (predicate == null || predicate.Invoke(directiveTriviaSyntax))
							{
								return directiveTriviaSyntax;
							}
						}
					}
					else if (syntaxTrivia.UnderlyingNode == base.Green)
					{
						flag = true;
					}
				}
				syntaxToken = syntaxToken.GetNextToken(DirectiveTriviaSyntax.s_hasDirectivesFunction, null);
			}
			return null;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000FCC04 File Offset: 0x000FAE04
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public DirectiveTriviaSyntax GetPreviousDirective([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<DirectiveTriviaSyntax, bool> predicate = null)
		{
			SyntaxToken syntaxToken = this.ParentTrivia.Token;
			bool flag = false;
			while (syntaxToken.Kind() != SyntaxKind.None)
			{
				foreach (SyntaxTrivia syntaxTrivia in syntaxToken.LeadingTrivia.Reverse())
				{
					if (flag)
					{
						if (syntaxTrivia.IsDirective)
						{
							DirectiveTriviaSyntax directiveTriviaSyntax = (DirectiveTriviaSyntax)syntaxTrivia.GetStructure();
							if (predicate == null || predicate.Invoke(directiveTriviaSyntax))
							{
								return directiveTriviaSyntax;
							}
						}
					}
					else if (syntaxTrivia.UnderlyingNode == base.Green)
					{
						flag = true;
					}
				}
				syntaxToken = syntaxToken.GetPreviousToken(DirectiveTriviaSyntax.s_hasDirectivesFunction, null);
			}
			return null;
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000FCCA4 File Offset: 0x000FAEA4
		public List<DirectiveTriviaSyntax> GetRelatedDirectives()
		{
			List<DirectiveTriviaSyntax> list = new List<DirectiveTriviaSyntax>();
			this.GetRelatedDirectives(list);
			return list;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000FCCC0 File Offset: 0x000FAEC0
		private void GetRelatedDirectives(List<DirectiveTriviaSyntax> list)
		{
			list.Clear();
			for (DirectiveTriviaSyntax directiveTriviaSyntax = this.GetPreviousRelatedDirective(); directiveTriviaSyntax != null; directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousRelatedDirective())
			{
				list.Add(directiveTriviaSyntax);
			}
			list.Reverse();
			list.Add(this);
			for (DirectiveTriviaSyntax directiveTriviaSyntax2 = this.GetNextRelatedDirective(); directiveTriviaSyntax2 != null; directiveTriviaSyntax2 = directiveTriviaSyntax2.GetNextRelatedDirective())
			{
				list.Add(directiveTriviaSyntax2);
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000FCD14 File Offset: 0x000FAF14
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private DirectiveTriviaSyntax GetNextRelatedDirective()
		{
			DirectiveTriviaSyntax directiveTriviaSyntax = this;
			switch (directiveTriviaSyntax.Kind())
			{
			case SyntaxKind.IfDirectiveTrivia:
				while (directiveTriviaSyntax != null)
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind - SyntaxKind.ElifDirectiveTrivia <= 2)
					{
						return directiveTriviaSyntax;
					}
					directiveTriviaSyntax = directiveTriviaSyntax.GetNextPossiblyRelatedDirective();
				}
				break;
			case SyntaxKind.ElifDirectiveTrivia:
				for (directiveTriviaSyntax = directiveTriviaSyntax.GetNextPossiblyRelatedDirective(); directiveTriviaSyntax != null; directiveTriviaSyntax = directiveTriviaSyntax.GetNextPossiblyRelatedDirective())
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind - SyntaxKind.ElifDirectiveTrivia <= 2)
					{
						return directiveTriviaSyntax;
					}
				}
				break;
			case SyntaxKind.ElseDirectiveTrivia:
				while (directiveTriviaSyntax != null)
				{
					if (directiveTriviaSyntax.Kind() == SyntaxKind.EndIfDirectiveTrivia)
					{
						return directiveTriviaSyntax;
					}
					directiveTriviaSyntax = directiveTriviaSyntax.GetNextPossiblyRelatedDirective();
				}
				break;
			case SyntaxKind.RegionDirectiveTrivia:
				while (directiveTriviaSyntax != null)
				{
					if (directiveTriviaSyntax.Kind() == SyntaxKind.EndRegionDirectiveTrivia)
					{
						return directiveTriviaSyntax;
					}
					directiveTriviaSyntax = directiveTriviaSyntax.GetNextPossiblyRelatedDirective();
				}
				break;
			}
			return null;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000FCDC8 File Offset: 0x000FAFC8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private DirectiveTriviaSyntax GetNextPossiblyRelatedDirective()
		{
			DirectiveTriviaSyntax directiveTriviaSyntax = this;
			while (directiveTriviaSyntax != null)
			{
				directiveTriviaSyntax = directiveTriviaSyntax.GetNextDirective(null);
				if (directiveTriviaSyntax != null)
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind == SyntaxKind.IfDirectiveTrivia)
					{
						while (directiveTriviaSyntax != null)
						{
							if (directiveTriviaSyntax.Kind() == SyntaxKind.EndIfDirectiveTrivia)
							{
								break;
							}
							directiveTriviaSyntax = directiveTriviaSyntax.GetNextRelatedDirective();
						}
						continue;
					}
					if (syntaxKind == SyntaxKind.RegionDirectiveTrivia)
					{
						while (directiveTriviaSyntax != null)
						{
							if (directiveTriviaSyntax.Kind() == SyntaxKind.EndRegionDirectiveTrivia)
							{
								break;
							}
							directiveTriviaSyntax = directiveTriviaSyntax.GetNextRelatedDirective();
						}
						continue;
					}
				}
				return directiveTriviaSyntax;
			}
			return null;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000FCE38 File Offset: 0x000FB038
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private DirectiveTriviaSyntax GetPreviousRelatedDirective()
		{
			DirectiveTriviaSyntax directiveTriviaSyntax = this;
			switch (directiveTriviaSyntax.Kind())
			{
			case SyntaxKind.ElifDirectiveTrivia:
				for (directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousPossiblyRelatedDirective(); directiveTriviaSyntax != null; directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousPossiblyRelatedDirective())
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind - SyntaxKind.IfDirectiveTrivia <= 1)
					{
						return directiveTriviaSyntax;
					}
				}
				break;
			case SyntaxKind.ElseDirectiveTrivia:
				while (directiveTriviaSyntax != null)
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind - SyntaxKind.IfDirectiveTrivia <= 1)
					{
						return directiveTriviaSyntax;
					}
					directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousPossiblyRelatedDirective();
				}
				break;
			case SyntaxKind.EndIfDirectiveTrivia:
				while (directiveTriviaSyntax != null)
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind - SyntaxKind.IfDirectiveTrivia <= 2)
					{
						return directiveTriviaSyntax;
					}
					directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousPossiblyRelatedDirective();
				}
				break;
			case SyntaxKind.EndRegionDirectiveTrivia:
				while (directiveTriviaSyntax != null)
				{
					if (directiveTriviaSyntax.Kind() == SyntaxKind.RegionDirectiveTrivia)
					{
						return directiveTriviaSyntax;
					}
					directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousPossiblyRelatedDirective();
				}
				break;
			}
			return null;
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000FCEF0 File Offset: 0x000FB0F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private DirectiveTriviaSyntax GetPreviousPossiblyRelatedDirective()
		{
			DirectiveTriviaSyntax directiveTriviaSyntax = this;
			while (directiveTriviaSyntax != null)
			{
				directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousDirective(null);
				if (directiveTriviaSyntax != null)
				{
					SyntaxKind syntaxKind = directiveTriviaSyntax.Kind();
					if (syntaxKind == SyntaxKind.EndIfDirectiveTrivia)
					{
						while (directiveTriviaSyntax != null)
						{
							if (directiveTriviaSyntax.Kind() == SyntaxKind.IfDirectiveTrivia)
							{
								break;
							}
							directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousRelatedDirective();
						}
						continue;
					}
					if (syntaxKind == SyntaxKind.EndRegionDirectiveTrivia)
					{
						while (directiveTriviaSyntax != null)
						{
							if (directiveTriviaSyntax.Kind() == SyntaxKind.RegionDirectiveTrivia)
							{
								break;
							}
							directiveTriviaSyntax = directiveTriviaSyntax.GetPreviousRelatedDirective();
						}
						continue;
					}
				}
				return directiveTriviaSyntax;
			}
			return null;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000FCF5D File Offset: 0x000FB15D
		internal DirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600392B RID: 14635
		public abstract SyntaxToken HashToken { get; }

		// Token: 0x0600392C RID: 14636 RVA: 0x000FCF68 File Offset: 0x000FB168
		public DirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.WithHashTokenCore(hashToken);
		}

		// Token: 0x0600392D RID: 14637
		internal abstract DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken);

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600392E RID: 14638
		public abstract SyntaxToken EndOfDirectiveToken { get; }

		// Token: 0x0600392F RID: 14639 RVA: 0x000FCF71 File Offset: 0x000FB171
		public DirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveTokenCore(endOfDirectiveToken);
		}

		// Token: 0x06003930 RID: 14640
		internal abstract DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken);

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003931 RID: 14641
		public abstract bool IsActive { get; }

		// Token: 0x040013B8 RID: 5048
		private static readonly Func<SyntaxToken, bool> s_hasDirectivesFunction = (SyntaxToken t) => t.ContainsDirectives;
	}
}
