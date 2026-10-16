using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000BE RID: 190
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class BoundStatementExtensions
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00056EC8 File Offset: 0x000550C8
		[Conditional("DEBUG")]
		internal static void AssertIsLabeledStatement(this BoundStatement node)
		{
			BoundKind kind = node.Kind;
			if (kind != BoundKind.LabelStatement && kind != BoundKind.LabeledStatement && kind != BoundKind.SwitchSection)
			{
				throw ExceptionUtilities.UnexpectedValue(node.Kind);
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00056F00 File Offset: 0x00055100
		[Conditional("DEBUG")]
		internal static void AssertIsLabeledStatementWithLabel(this BoundStatement node, LabelSymbol label)
		{
			BoundKind kind = node.Kind;
			if (kind == BoundKind.LabelStatement || kind == BoundKind.LabeledStatement)
			{
				return;
			}
			if (kind == BoundKind.SwitchSection)
			{
				ImmutableArray<BoundSwitchLabel>.Enumerator enumerator = ((BoundSwitchSection)node).SwitchLabels.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Label == label)
					{
						return;
					}
				}
				throw ExceptionUtilities.Unreachable;
			}
			throw ExceptionUtilities.UnexpectedValue(node.Kind);
		}
	}
}
