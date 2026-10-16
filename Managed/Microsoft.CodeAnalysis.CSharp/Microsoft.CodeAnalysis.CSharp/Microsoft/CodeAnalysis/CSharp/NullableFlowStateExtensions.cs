using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000221 RID: 545
	internal static class NullableFlowStateExtensions
	{
		// Token: 0x060027E4 RID: 10212 RVA: 0x000DA77A File Offset: 0x000D897A
		public static bool MayBeNull(this NullableFlowState state)
		{
			return state > NullableFlowState.NotNull;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000DA780 File Offset: 0x000D8980
		public static bool IsNotNull(this NullableFlowState state)
		{
			return state == NullableFlowState.NotNull;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000DA786 File Offset: 0x000D8986
		public static NullableFlowState Join(this NullableFlowState a, NullableFlowState b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000DA78F File Offset: 0x000D898F
		public static NullableFlowState Meet(this NullableFlowState a, NullableFlowState b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000DA798 File Offset: 0x000D8998
		internal static NullableFlowState ToPublicFlowState(this NullableFlowState nullableFlowState)
		{
			switch (nullableFlowState)
			{
			case NullableFlowState.NotNull:
				return NullableFlowState.NotNull;
			case NullableFlowState.MaybeNull:
				return NullableFlowState.MaybeNull;
			case NullableFlowState.MaybeDefault:
				return NullableFlowState.MaybeNull;
			}
			throw ExceptionUtilities.UnexpectedValue(nullableFlowState);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000DA7D8 File Offset: 0x000D89D8
		public static NullableFlowState ToInternalFlowState(this NullableFlowState flowState)
		{
			NullableFlowState nullableFlowState;
			switch (flowState)
			{
			case NullableFlowState.None:
				nullableFlowState = NullableFlowState.NotNull;
				break;
			case NullableFlowState.NotNull:
				nullableFlowState = NullableFlowState.NotNull;
				break;
			case NullableFlowState.MaybeNull:
				nullableFlowState = NullableFlowState.MaybeNull;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(flowState);
			}
			return nullableFlowState;
		}
	}
}
