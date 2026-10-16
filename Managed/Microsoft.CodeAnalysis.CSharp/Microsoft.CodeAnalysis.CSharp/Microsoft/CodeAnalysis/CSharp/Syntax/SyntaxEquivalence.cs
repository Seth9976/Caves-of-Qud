using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000281 RID: 641
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class SyntaxEquivalence
	{
		// Token: 0x06003CD5 RID: 15573 RVA: 0x001062C4 File Offset: 0x001044C4
		internal static bool AreEquivalent(SyntaxTree before, SyntaxTree after, Func<SyntaxKind, bool> ignoreChildNode, bool topLevel)
		{
			return before == after || (before != null && after != null && SyntaxEquivalence.AreEquivalent(before.GetRoot(default(CancellationToken)), after.GetRoot(default(CancellationToken)), ignoreChildNode, topLevel));
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00106304 File Offset: 0x00104504
		public static bool AreEquivalent(SyntaxNode before, SyntaxNode after, Func<SyntaxKind, bool> ignoreChildNode, bool topLevel)
		{
			if (before == null || after == null)
			{
				return before == after;
			}
			return SyntaxEquivalence.AreEquivalentRecursive(before.Green, after.Green, ignoreChildNode, topLevel);
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x00106324 File Offset: 0x00104524
		public static bool AreEquivalent(SyntaxTokenList before, SyntaxTokenList after)
		{
			return SyntaxEquivalence.AreEquivalentRecursive(before.Node, after.Node, null, false);
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x0010633B File Offset: 0x0010453B
		public static bool AreEquivalent(SyntaxToken before, SyntaxToken after)
		{
			return before.RawKind == after.RawKind && (before.Node == null || SyntaxEquivalence.AreTokensEquivalent(before.Node, after.Node, null));
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00106370 File Offset: 0x00104570
		private static bool AreTokensEquivalent(GreenNode before, GreenNode after, Func<SyntaxKind, bool> ignoreChildNode)
		{
			if (before == null || after == null)
			{
				return before == null && after == null;
			}
			if (before.IsMissing != after.IsMissing)
			{
				return false;
			}
			SyntaxKind syntaxKind = (SyntaxKind)before.RawKind;
			if (syntaxKind != SyntaxKind.IdentifierToken)
			{
				if (syntaxKind - SyntaxKind.NumericLiteralToken <= 2 || syntaxKind == SyntaxKind.InterpolatedStringTextToken)
				{
					if (((SyntaxToken)before).Text != ((SyntaxToken)after).Text)
					{
						return false;
					}
				}
			}
			else if (((SyntaxToken)before).ValueText != ((SyntaxToken)after).ValueText)
			{
				return false;
			}
			return SyntaxEquivalence.AreNullableDirectivesEquivalent(before, after, ignoreChildNode);
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00106408 File Offset: 0x00104608
		private static bool AreEquivalentRecursive(GreenNode before, GreenNode after, Func<SyntaxKind, bool> ignoreChildNode, bool topLevel)
		{
			if (before == after)
			{
				return true;
			}
			if (before == null || after == null)
			{
				return false;
			}
			if (before.RawKind != after.RawKind)
			{
				return false;
			}
			if (before.IsToken)
			{
				return SyntaxEquivalence.AreTokensEquivalent(before, after, ignoreChildNode);
			}
			if (topLevel)
			{
				SyntaxKind syntaxKind = (SyntaxKind)before.RawKind;
				if (syntaxKind == SyntaxKind.Block || syntaxKind == SyntaxKind.ArrowExpressionClause)
				{
					return SyntaxEquivalence.AreNullableDirectivesEquivalent(before, after, ignoreChildNode);
				}
				if ((ushort)before.RawKind == 8873)
				{
					MemberDeclarationSyntax memberDeclarationSyntax = (FieldDeclarationSyntax)before;
					FieldDeclarationSyntax fieldDeclarationSyntax = (FieldDeclarationSyntax)after;
					bool flag = memberDeclarationSyntax.Modifiers.Any(8350);
					bool flag2 = fieldDeclarationSyntax.Modifiers.Any(8350);
					if (!flag && !flag2)
					{
						ignoreChildNode = (SyntaxKind childKind) => childKind == SyntaxKind.EqualsValueClause;
					}
				}
			}
			if (ignoreChildNode != null)
			{
				ChildSyntaxList.Enumerator enumerator = before.ChildNodesAndTokens().GetEnumerator();
				ChildSyntaxList.Enumerator enumerator2 = after.ChildNodesAndTokens().GetEnumerator();
				GreenNode greenNode;
				GreenNode greenNode2;
				for (;;)
				{
					IL_00E5:
					greenNode = null;
					greenNode2 = null;
					while (enumerator.MoveNext())
					{
						GreenNode greenNode3 = enumerator.Current;
						if (greenNode3 != null && (greenNode3.IsToken || !ignoreChildNode.Invoke((SyntaxKind)greenNode3.RawKind)))
						{
							greenNode = greenNode3;
							IL_0150:
							while (enumerator2.MoveNext())
							{
								GreenNode greenNode4 = enumerator2.Current;
								if (greenNode4 != null && (greenNode4.IsToken || !ignoreChildNode.Invoke((SyntaxKind)greenNode4.RawKind)))
								{
									greenNode2 = greenNode4;
									break;
								}
							}
							if (greenNode == null || greenNode2 == null)
							{
								goto IL_0161;
							}
							if (!SyntaxEquivalence.AreEquivalentRecursive(greenNode, greenNode2, ignoreChildNode, topLevel))
							{
								return false;
							}
							goto IL_00E5;
						}
					}
					goto IL_0150;
				}
				IL_0161:
				return greenNode == greenNode2;
			}
			int slotCount = before.SlotCount;
			if (slotCount != after.SlotCount)
			{
				return false;
			}
			for (int i = 0; i < slotCount; i++)
			{
				GreenNode slot = before.GetSlot(i);
				GreenNode slot2 = after.GetSlot(i);
				if (!SyntaxEquivalence.AreEquivalentRecursive(slot, slot2, ignoreChildNode, topLevel))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x001065D4 File Offset: 0x001047D4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool AreNullableDirectivesEquivalent(GreenNode before, GreenNode after, [global::System.Runtime.CompilerServices.Nullable(2)] Func<SyntaxKind, bool> ignoreChildNode)
		{
			if (ignoreChildNode != null && ignoreChildNode.Invoke(SyntaxKind.NullableDirectiveTrivia))
			{
				return true;
			}
			bool flag;
			using (IEnumerator<DirectiveTriviaSyntax> enumerator = ((CSharpSyntaxNode)before).GetDirectives().GetEnumerator())
			{
				using (IEnumerator<DirectiveTriviaSyntax> enumerator2 = ((CSharpSyntaxNode)after).GetDirectives().GetEnumerator())
				{
					DirectiveTriviaSyntax directiveTriviaSyntax;
					DirectiveTriviaSyntax directiveTriviaSyntax2;
					for (;;)
					{
						directiveTriviaSyntax = SyntaxEquivalence.<AreNullableDirectivesEquivalent>g__getNextNullableDirective|6_0(enumerator, ignoreChildNode);
						directiveTriviaSyntax2 = SyntaxEquivalence.<AreNullableDirectivesEquivalent>g__getNextNullableDirective|6_0(enumerator2, ignoreChildNode);
						if (directiveTriviaSyntax == null || directiveTriviaSyntax2 == null)
						{
							break;
						}
						if (!SyntaxEquivalence.AreEquivalentRecursive(directiveTriviaSyntax, directiveTriviaSyntax2, ignoreChildNode, false))
						{
							goto Block_7;
						}
					}
					return directiveTriviaSyntax == directiveTriviaSyntax2;
					Block_7:
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00106678 File Offset: 0x00104878
		[CompilerGenerated]
		internal static DirectiveTriviaSyntax <AreNullableDirectivesEquivalent>g__getNextNullableDirective|6_0([global::System.Runtime.CompilerServices.Nullable(1)] IEnumerator<DirectiveTriviaSyntax> enumerator, Func<SyntaxKind, bool> ignoreChildNode)
		{
			while (enumerator.MoveNext())
			{
				DirectiveTriviaSyntax directiveTriviaSyntax = enumerator.Current;
				if (directiveTriviaSyntax.Kind == SyntaxKind.NullableDirectiveTrivia)
				{
					return directiveTriviaSyntax;
				}
			}
			return null;
		}
	}
}
