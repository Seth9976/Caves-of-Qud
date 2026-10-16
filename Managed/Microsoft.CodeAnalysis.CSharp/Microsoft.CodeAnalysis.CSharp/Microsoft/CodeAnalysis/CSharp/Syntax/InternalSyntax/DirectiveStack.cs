using System;
using System.Diagnostics;
using System.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200035D RID: 861
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct DirectiveStack
	{
		// Token: 0x06004854 RID: 18516 RVA: 0x0011CC80 File Offset: 0x0011AE80
		private DirectiveStack(ConsList<Directive> directives)
		{
			this._directives = directives;
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x0011CC89 File Offset: 0x0011AE89
		public bool IsNull
		{
			get
			{
				return this._directives == null;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x0011CC94 File Offset: 0x0011AE94
		public bool IsEmpty
		{
			get
			{
				return this._directives == ConsList<Directive>.Empty;
			}
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x0011CCA4 File Offset: 0x0011AEA4
		public DefineState IsDefined(string id)
		{
			ConsList<Directive> consList = this._directives;
			while (consList != null && consList.Any())
			{
				switch (consList.Head.Kind)
				{
				case SyntaxKind.ElifDirectiveTrivia:
				case SyntaxKind.ElseDirectiveTrivia:
					for (;;)
					{
						consList = consList.Tail;
						if (consList == null || !consList.Any())
						{
							break;
						}
						if (consList.Head.Kind == SyntaxKind.IfDirectiveTrivia)
						{
							goto IL_009E;
						}
					}
					return DefineState.Unspecified;
				case SyntaxKind.DefineDirectiveTrivia:
					if (consList.Head.GetIdentifier() == id)
					{
						return DefineState.Defined;
					}
					break;
				case SyntaxKind.UndefDirectiveTrivia:
					if (consList.Head.GetIdentifier() == id)
					{
						return DefineState.Undefined;
					}
					break;
				}
				IL_009E:
				consList = consList.Tail;
			}
			return DefineState.Unspecified;
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0011CD68 File Offset: 0x0011AF68
		public bool PreviousBranchTaken()
		{
			ConsList<Directive> consList = this._directives;
			while (consList != null && consList.Any())
			{
				if (consList.Head.BranchTaken)
				{
					return true;
				}
				if (consList.Head.Kind == SyntaxKind.IfDirectiveTrivia)
				{
					return false;
				}
				consList = consList.Tail;
			}
			return false;
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x0011CDBC File Offset: 0x0011AFBC
		public bool HasUnfinishedIf()
		{
			ConsList<Directive> previousIfElifElseOrRegion = DirectiveStack.GetPreviousIfElifElseOrRegion(this._directives);
			return previousIfElifElseOrRegion != null && previousIfElifElseOrRegion.Any() && previousIfElifElseOrRegion.Head.Kind != SyntaxKind.RegionDirectiveTrivia;
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x0011CDFC File Offset: 0x0011AFFC
		public bool HasPreviousIfOrElif()
		{
			ConsList<Directive> previousIfElifElseOrRegion = DirectiveStack.GetPreviousIfElifElseOrRegion(this._directives);
			return previousIfElifElseOrRegion != null && previousIfElifElseOrRegion.Any() && (previousIfElifElseOrRegion.Head.Kind == SyntaxKind.IfDirectiveTrivia || previousIfElifElseOrRegion.Head.Kind == SyntaxKind.ElifDirectiveTrivia);
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x0011CE50 File Offset: 0x0011B050
		public bool HasUnfinishedRegion()
		{
			ConsList<Directive> previousIfElifElseOrRegion = DirectiveStack.GetPreviousIfElifElseOrRegion(this._directives);
			return previousIfElifElseOrRegion != null && previousIfElifElseOrRegion.Any() && previousIfElifElseOrRegion.Head.Kind == SyntaxKind.RegionDirectiveTrivia;
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x0011CE8C File Offset: 0x0011B08C
		public DirectiveStack Add(Directive directive)
		{
			SyntaxKind kind = directive.Kind;
			if (kind != SyntaxKind.EndIfDirectiveTrivia)
			{
				if (kind == SyntaxKind.EndRegionDirectiveTrivia)
				{
					ConsList<Directive> previousRegion = DirectiveStack.GetPreviousRegion(this._directives);
					if (previousRegion != null && previousRegion.Any())
					{
						return new DirectiveStack(DirectiveStack.CompleteRegion(this._directives));
					}
				}
			}
			else
			{
				ConsList<Directive> previousIf = DirectiveStack.GetPreviousIf(this._directives);
				if (previousIf != null && previousIf.Any())
				{
					bool flag;
					return new DirectiveStack(DirectiveStack.CompleteIf(this._directives, out flag));
				}
			}
			return new DirectiveStack(new ConsList<Directive>(directive, (this._directives != null) ? this._directives : ConsList<Directive>.Empty));
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x0011CF28 File Offset: 0x0011B128
		private static ConsList<Directive> CompleteIf(ConsList<Directive> stack, out bool include)
		{
			if (!stack.Any())
			{
				include = true;
				return stack;
			}
			if (stack.Head.Kind == SyntaxKind.IfDirectiveTrivia)
			{
				include = stack.Head.BranchTaken;
				return stack.Tail;
			}
			ConsList<Directive> consList = DirectiveStack.CompleteIf(stack.Tail, out include);
			SyntaxKind kind = stack.Head.Kind;
			if (kind - SyntaxKind.ElifDirectiveTrivia <= 1)
			{
				include = stack.Head.BranchTaken;
			}
			else if (include)
			{
				consList = new ConsList<Directive>(stack.Head, consList);
			}
			return consList;
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0011CFB8 File Offset: 0x0011B1B8
		private static ConsList<Directive> CompleteRegion(ConsList<Directive> stack)
		{
			if (!stack.Any())
			{
				return stack;
			}
			if (stack.Head.Kind == SyntaxKind.RegionDirectiveTrivia)
			{
				return stack.Tail;
			}
			ConsList<Directive> consList = DirectiveStack.CompleteRegion(stack.Tail);
			return new ConsList<Directive>(stack.Head, consList);
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0011D008 File Offset: 0x0011B208
		private static ConsList<Directive> GetPreviousIf(ConsList<Directive> directives)
		{
			ConsList<Directive> consList = directives;
			while (consList != null && consList.Any())
			{
				if (consList.Head.Kind == SyntaxKind.IfDirectiveTrivia)
				{
					return consList;
				}
				consList = consList.Tail;
			}
			return consList;
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0011D044 File Offset: 0x0011B244
		private static ConsList<Directive> GetPreviousIfElifElseOrRegion(ConsList<Directive> directives)
		{
			ConsList<Directive> consList = directives;
			while (consList != null && consList.Any())
			{
				SyntaxKind kind = consList.Head.Kind;
				if (kind - SyntaxKind.IfDirectiveTrivia <= 2 || kind == SyntaxKind.RegionDirectiveTrivia)
				{
					return consList;
				}
				consList = consList.Tail;
			}
			return consList;
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x0011D08C File Offset: 0x0011B28C
		private static ConsList<Directive> GetPreviousRegion(ConsList<Directive> directives)
		{
			ConsList<Directive> consList = directives;
			while (consList != null && consList.Any() && consList.Head.Kind != SyntaxKind.RegionDirectiveTrivia)
			{
				consList = consList.Tail;
			}
			return consList;
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x0011D0C8 File Offset: 0x0011B2C8
		private string GetDebuggerDisplay()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ConsList<Directive> consList = this._directives;
			while (consList != null && consList.Any())
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Insert(0, " | ");
				}
				stringBuilder.Insert(0, consList.Head.GetDebuggerDisplay());
				consList = consList.Tail;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x0011D128 File Offset: 0x0011B328
		public bool IncrementallyEquivalent(DirectiveStack other)
		{
			ConsList<Directive> consList = DirectiveStack.SkipInsignificantDirectives(this._directives);
			ConsList<Directive> consList2 = DirectiveStack.SkipInsignificantDirectives(other._directives);
			bool flag = consList != null && consList.Any();
			bool flag2 = consList2 != null && consList2.Any();
			while (flag && flag2)
			{
				if (!consList.Head.IncrementallyEquivalent(consList2.Head))
				{
					return false;
				}
				consList = DirectiveStack.SkipInsignificantDirectives(consList.Tail);
				consList2 = DirectiveStack.SkipInsignificantDirectives(consList2.Tail);
				flag = consList != null && consList.Any();
				flag2 = consList2 != null && consList2.Any();
			}
			return flag == flag2;
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x0011D1C0 File Offset: 0x0011B3C0
		private static ConsList<Directive> SkipInsignificantDirectives(ConsList<Directive> directives)
		{
			while (directives != null && directives.Any())
			{
				SyntaxKind kind = directives.Head.Kind;
				if (kind - SyntaxKind.IfDirectiveTrivia <= 7)
				{
					return directives;
				}
				directives = directives.Tail;
			}
			return directives;
		}

		// Token: 0x04001541 RID: 5441
		public static readonly DirectiveStack Empty = new DirectiveStack(ConsList<Directive>.Empty);

		// Token: 0x04001542 RID: 5442
		public static readonly DirectiveStack Null = new DirectiveStack(null);

		// Token: 0x04001543 RID: 5443
		private readonly ConsList<Directive> _directives;
	}
}
