using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000254 RID: 596
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class CSharpPragmaWarningStateMap : AbstractWarningStateMap<PragmaWarningState>
	{
		// Token: 0x060038BA RID: 14522 RVA: 0x000FB736 File Offset: 0x000F9936
		public CSharpPragmaWarningStateMap(SyntaxTree syntaxTree)
			: base(syntaxTree)
		{
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000FB740 File Offset: 0x000F9940
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })]
		protected override AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry[] CreateWarningStateMapEntries(SyntaxTree syntaxTree)
		{
			ArrayBuilder<DirectiveTriviaSyntax> instance = ArrayBuilder<DirectiveTriviaSyntax>.GetInstance();
			CSharpPragmaWarningStateMap.GetAllPragmaWarningDirectives(syntaxTree, instance);
			AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry[] array = CSharpPragmaWarningStateMap.CreatePragmaWarningStateEntries(instance);
			instance.Free();
			return array;
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000FB768 File Offset: 0x000F9968
		private static void GetAllPragmaWarningDirectives(SyntaxTree syntaxTree, ArrayBuilder<DirectiveTriviaSyntax> directiveList)
		{
			foreach (DirectiveTriviaSyntax directiveTriviaSyntax in syntaxTree.GetRoot(default(CancellationToken)).GetDirectives(null))
			{
				if (directiveTriviaSyntax.IsActive && directiveTriviaSyntax.Kind() == SyntaxKind.PragmaWarningDirectiveTrivia)
				{
					PragmaWarningDirectiveTriviaSyntax pragmaWarningDirectiveTriviaSyntax = (PragmaWarningDirectiveTriviaSyntax)directiveTriviaSyntax;
					if (!pragmaWarningDirectiveTriviaSyntax.DisableOrRestoreKeyword.IsMissing && !pragmaWarningDirectiveTriviaSyntax.WarningKeyword.IsMissing)
					{
						directiveList.Add(pragmaWarningDirectiveTriviaSyntax);
					}
				}
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000FB804 File Offset: 0x000F9A04
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })]
		private static AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry[] CreatePragmaWarningStateEntries(ArrayBuilder<DirectiveTriviaSyntax> directiveList)
		{
			AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry[] array = new AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry[directiveList.Count + 1];
			int i = 0;
			ImmutableDictionary<string, PragmaWarningState> immutableDictionary = ImmutableDictionary.Create<string, PragmaWarningState>();
			PragmaWarningState pragmaWarningState = PragmaWarningState.Default;
			AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry warningStateMapEntry = new AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry(0, PragmaWarningState.Default, immutableDictionary);
			array[i] = warningStateMapEntry;
			while (i < directiveList.Count)
			{
				DirectiveTriviaSyntax directiveTriviaSyntax = directiveList[i];
				PragmaWarningDirectiveTriviaSyntax pragmaWarningDirectiveTriviaSyntax = (PragmaWarningDirectiveTriviaSyntax)directiveTriviaSyntax;
				SyntaxKind syntaxKind = pragmaWarningDirectiveTriviaSyntax.DisableOrRestoreKeyword.Kind();
				PragmaWarningState pragmaWarningState2;
				if (syntaxKind != SyntaxKind.DisableKeyword)
				{
					if (syntaxKind != SyntaxKind.RestoreKeyword)
					{
						if (syntaxKind != SyntaxKind.EnableKeyword)
						{
							throw ExceptionUtilities.UnexpectedValue(syntaxKind);
						}
						pragmaWarningState2 = PragmaWarningState.Enabled;
					}
					else
					{
						pragmaWarningState2 = PragmaWarningState.Default;
					}
				}
				else
				{
					pragmaWarningState2 = PragmaWarningState.Disabled;
				}
				PragmaWarningState pragmaWarningState3 = pragmaWarningState2;
				if (pragmaWarningDirectiveTriviaSyntax.ErrorCodes.Count == 0)
				{
					pragmaWarningState = pragmaWarningState3;
					immutableDictionary = ImmutableDictionary.Create<string, PragmaWarningState>();
				}
				else
				{
					for (int j = 0; j < pragmaWarningDirectiveTriviaSyntax.ErrorCodes.Count; j++)
					{
						ExpressionSyntax expressionSyntax = pragmaWarningDirectiveTriviaSyntax.ErrorCodes[j];
						if (!expressionSyntax.IsMissing && !expressionSyntax.ContainsDiagnostics)
						{
							string text = string.Empty;
							if (expressionSyntax.Kind() == SyntaxKind.NumericLiteralExpression)
							{
								SyntaxToken token = ((LiteralExpressionSyntax)expressionSyntax).Token;
								text = MessageProvider.Instance.GetIdForErrorCode((int)token.Value);
							}
							else if (expressionSyntax.Kind() == SyntaxKind.IdentifierName)
							{
								text = ((IdentifierNameSyntax)expressionSyntax).Identifier.ValueText;
							}
							if (!string.IsNullOrWhiteSpace(text))
							{
								immutableDictionary = immutableDictionary.SetItem(text, pragmaWarningState3);
							}
						}
					}
				}
				warningStateMapEntry = new AbstractWarningStateMap<PragmaWarningState>.WarningStateMapEntry(directiveTriviaSyntax.Location.SourceSpan.End, pragmaWarningState, immutableDictionary);
				i++;
				array[i] = warningStateMapEntry;
			}
			return array;
		}
	}
}
