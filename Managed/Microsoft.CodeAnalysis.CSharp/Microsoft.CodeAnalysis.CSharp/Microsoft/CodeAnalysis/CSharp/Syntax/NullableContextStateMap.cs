using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000271 RID: 625
	internal readonly struct NullableContextStateMap
	{
		// Token: 0x06003B7B RID: 15227 RVA: 0x00102F02 File Offset: 0x00101102
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static NullableContextStateMap Create(SyntaxTree tree)
		{
			return new NullableContextStateMap(NullableContextStateMap.GetContexts(tree));
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00102F0F File Offset: 0x0010110F
		private NullableContextStateMap(ImmutableArray<NullableContextState> contexts)
		{
			this._contexts = contexts;
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x00102F18 File Offset: 0x00101118
		private static NullableContextState GetContextForFileStart()
		{
			return new NullableContextState(0, NullableContextState.State.Unknown, NullableContextState.State.Unknown);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x00102F24 File Offset: 0x00101124
		private int GetContextStateIndex(int position)
		{
			NullableContextState nullableContextState = new NullableContextState(position, NullableContextState.State.Unknown, NullableContextState.State.Unknown);
			int num = this._contexts.BinarySearch(nullableContextState, NullableContextStateMap.PositionComparer.Instance);
			if (num < 0)
			{
				num = ~num - 1;
			}
			return num;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x00102F58 File Offset: 0x00101158
		internal NullableContextState GetContextState(int position)
		{
			int contextStateIndex = this.GetContextStateIndex(position);
			if (contextStateIndex >= 0)
			{
				return this._contexts[contextStateIndex];
			}
			return NullableContextStateMap.GetContextForFileStart();
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x00102F88 File Offset: 0x00101188
		internal bool? IsNullableAnalysisEnabled(TextSpan span)
		{
			bool flag = false;
			int num = this.GetContextStateIndex(span.Start);
			NullableContextState nullableContextState = ((num < 0) ? NullableContextStateMap.GetContextForFileStart() : this._contexts[num]);
			for (;;)
			{
				switch (nullableContextState.WarningsState)
				{
				case NullableContextState.State.Unknown:
				case NullableContextState.State.ExplicitlyRestored:
					flag = true;
					break;
				case NullableContextState.State.Enabled:
					goto IL_004D;
				}
				num++;
				if (num >= this._contexts.Length)
				{
					goto IL_008B;
				}
				nullableContextState = this._contexts[num];
				if (nullableContextState.Position >= span.End)
				{
					goto IL_008B;
				}
			}
			IL_004D:
			return new bool?(true);
			IL_008B:
			if (!flag)
			{
				return new bool?(false);
			}
			return default(bool?);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x00103034 File Offset: 0x00101234
		private static ImmutableArray<NullableContextState> GetContexts([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxTree tree)
		{
			NullableContextState nullableContextState = NullableContextStateMap.GetContextForFileStart();
			ArrayBuilder<NullableContextState> instance = ArrayBuilder<NullableContextState>.GetInstance();
			foreach (DirectiveTriviaSyntax directiveTriviaSyntax in tree.GetRoot(default(CancellationToken)).GetDirectives(null))
			{
				if (directiveTriviaSyntax.Kind() == SyntaxKind.NullableDirectiveTrivia)
				{
					NullableDirectiveTriviaSyntax nullableDirectiveTriviaSyntax = (NullableDirectiveTriviaSyntax)directiveTriviaSyntax;
					if (!nullableDirectiveTriviaSyntax.SettingToken.IsMissing && nullableDirectiveTriviaSyntax.IsActive)
					{
						int endPosition = nullableDirectiveTriviaSyntax.EndPosition;
						SyntaxKind syntaxKind = nullableDirectiveTriviaSyntax.SettingToken.Kind();
						NullableContextState.State state;
						if (syntaxKind != SyntaxKind.DisableKeyword)
						{
							if (syntaxKind != SyntaxKind.RestoreKeyword)
							{
								if (syntaxKind != SyntaxKind.EnableKeyword)
								{
									throw ExceptionUtilities.UnexpectedValue(syntaxKind);
								}
								state = NullableContextState.State.Enabled;
							}
							else
							{
								state = NullableContextState.State.ExplicitlyRestored;
							}
						}
						else
						{
							state = NullableContextState.State.Disabled;
						}
						NullableContextState.State state2 = state;
						SyntaxKind syntaxKind2 = nullableDirectiveTriviaSyntax.TargetToken.Kind();
						NullableContextState nullableContextState2;
						if (syntaxKind2 != SyntaxKind.None)
						{
							if (syntaxKind2 != SyntaxKind.WarningsKeyword)
							{
								if (syntaxKind2 != SyntaxKind.AnnotationsKeyword)
								{
									throw ExceptionUtilities.UnexpectedValue(syntaxKind2);
								}
								nullableContextState2 = new NullableContextState(endPosition, nullableContextState.WarningsState, state2);
							}
							else
							{
								nullableContextState2 = new NullableContextState(endPosition, state2, nullableContextState.AnnotationsState);
							}
						}
						else
						{
							nullableContextState2 = new NullableContextState(endPosition, state2, state2);
						}
						NullableContextState nullableContextState3 = nullableContextState2;
						instance.Add(nullableContextState3);
						nullableContextState = nullableContextState3;
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x04001401 RID: 5121
		private readonly ImmutableArray<NullableContextState> _contexts;

		// Token: 0x02000786 RID: 1926
		private sealed class PositionComparer : IComparer<NullableContextState>
		{
			// Token: 0x06008874 RID: 34932 RVA: 0x001ECFC8 File Offset: 0x001EB1C8
			public int Compare(NullableContextState x, NullableContextState y)
			{
				return x.Position.CompareTo(y.Position);
			}

			// Token: 0x040023C3 RID: 9155
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal static readonly NullableContextStateMap.PositionComparer Instance = new NullableContextStateMap.PositionComparer();
		}
	}
}
