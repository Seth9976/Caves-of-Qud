using System;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000365 RID: 869
	internal class LexerCache
	{
		// Token: 0x06004AD8 RID: 19160 RVA: 0x001362EB File Offset: 0x001344EB
		internal LexerCache()
		{
			this._triviaMap = TextKeyedCache<SyntaxTrivia>.GetInstance();
			this._tokenMap = TextKeyedCache<SyntaxToken>.GetInstance();
			this._keywordKindMap = LexerCache.s_keywordKindPool.Allocate();
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00136319 File Offset: 0x00134519
		internal void Free()
		{
			this._keywordKindMap.Free();
			this._triviaMap.Free();
			this._tokenMap.Free();
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x0013633C File Offset: 0x0013453C
		internal bool TryGetKeywordKind(string key, out SyntaxKind kind)
		{
			if (key.Length > 10)
			{
				kind = SyntaxKind.None;
				return false;
			}
			kind = this._keywordKindMap.GetOrMakeValue(key);
			return kind > SyntaxKind.None;
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00136360 File Offset: 0x00134560
		internal SyntaxTrivia LookupTrivia(char[] textBuffer, int keyStart, int keyLength, int hashCode, Func<SyntaxTrivia> createTriviaFunction)
		{
			SyntaxTrivia syntaxTrivia = this._triviaMap.FindItem(textBuffer, keyStart, keyLength, hashCode);
			if (syntaxTrivia == null)
			{
				syntaxTrivia = createTriviaFunction.Invoke();
				this._triviaMap.AddItem(textBuffer, keyStart, keyLength, hashCode, syntaxTrivia);
			}
			return syntaxTrivia;
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0013639C File Offset: 0x0013459C
		internal SyntaxToken LookupToken(char[] textBuffer, int keyStart, int keyLength, int hashCode, Func<SyntaxToken> createTokenFunction)
		{
			SyntaxToken syntaxToken = this._tokenMap.FindItem(textBuffer, keyStart, keyLength, hashCode);
			if (syntaxToken == null)
			{
				syntaxToken = createTokenFunction.Invoke();
				this._tokenMap.AddItem(textBuffer, keyStart, keyLength, hashCode, syntaxToken);
			}
			return syntaxToken;
		}

		// Token: 0x04001598 RID: 5528
		private static readonly ObjectPool<CachingIdentityFactory<string, SyntaxKind>> s_keywordKindPool = CachingIdentityFactory<string, SyntaxKind>.CreatePool(512, delegate(string key)
		{
			SyntaxKind syntaxKind = SyntaxFacts.GetKeywordKind(key);
			if (syntaxKind == SyntaxKind.None)
			{
				syntaxKind = SyntaxFacts.GetContextualKeywordKind(key);
			}
			return syntaxKind;
		});

		// Token: 0x04001599 RID: 5529
		private readonly TextKeyedCache<SyntaxTrivia> _triviaMap;

		// Token: 0x0400159A RID: 5530
		private readonly TextKeyedCache<SyntaxToken> _tokenMap;

		// Token: 0x0400159B RID: 5531
		private readonly CachingIdentityFactory<string, SyntaxKind> _keywordKindMap;

		// Token: 0x0400159C RID: 5532
		internal const int MaxKeywordLength = 10;
	}
}
