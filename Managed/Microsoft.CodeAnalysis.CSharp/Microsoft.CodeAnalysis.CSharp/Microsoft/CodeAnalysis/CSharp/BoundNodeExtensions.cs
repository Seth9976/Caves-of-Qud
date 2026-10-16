using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B7 RID: 183
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class BoundNodeExtensions
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x00056A78 File Offset: 0x00054C78
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool HasErrors<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> nodeArray) where T : BoundNode
		{
			if (nodeArray.IsDefault)
			{
				return false;
			}
			int i = 0;
			int length = nodeArray.Length;
			while (i < length)
			{
				if (nodeArray[i].HasErrors)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00056ABB File Offset: 0x00054CBB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool HasErrors([NotNullWhen(true)] this BoundNode node)
		{
			return node != null && node.HasErrors;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00056AC8 File Offset: 0x00054CC8
		public static bool IsConstructorInitializer(this BoundStatement statement)
		{
			if (statement.Kind == BoundKind.ExpressionStatement)
			{
				BoundExpression boundExpression = ((BoundExpressionStatement)statement).Expression;
				if (boundExpression.Kind == BoundKind.Sequence && ((BoundSequence)boundExpression).SideEffects.IsDefaultOrEmpty)
				{
					boundExpression = ((BoundSequence)boundExpression).Value;
				}
				return boundExpression.Kind == BoundKind.Call && ((BoundCall)boundExpression).IsConstructorInitializer();
			}
			return false;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00056B38 File Offset: 0x00054D38
		public static bool IsConstructorInitializer(this BoundCall call)
		{
			MethodSymbol method = call.Method;
			BoundExpression receiverOpt = call.ReceiverOpt;
			return method.MethodKind == MethodKind.Constructor && receiverOpt != null && (receiverOpt.Kind == BoundKind.ThisReference || receiverOpt.Kind == BoundKind.BaseReference);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00056B75 File Offset: 0x00054D75
		public static T MakeCompilerGenerated<[global::System.Runtime.CompilerServices.Nullable(0)] T>(this T node) where T : BoundNode
		{
			node.WasCompilerGenerated = true;
			return node;
		}
	}
}
