using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200026C RID: 620
	[NullableContext(1)]
	[Nullable(0)]
	public static class WellKnownMemberNames
	{
		// Token: 0x040009A4 RID: 2468
		public const string EnumBackingFieldName = "value__";

		// Token: 0x040009A5 RID: 2469
		public const string InstanceConstructorName = ".ctor";

		// Token: 0x040009A6 RID: 2470
		public const string StaticConstructorName = ".cctor";

		// Token: 0x040009A7 RID: 2471
		public const string Indexer = "this[]";

		// Token: 0x040009A8 RID: 2472
		public const string DestructorName = "Finalize";

		// Token: 0x040009A9 RID: 2473
		public const string DelegateInvokeName = "Invoke";

		// Token: 0x040009AA RID: 2474
		public const string DelegateBeginInvokeName = "BeginInvoke";

		// Token: 0x040009AB RID: 2475
		public const string DelegateEndInvokeName = "EndInvoke";

		// Token: 0x040009AC RID: 2476
		public const string EntryPointMethodName = "Main";

		// Token: 0x040009AD RID: 2477
		public const string DefaultScriptClassName = "Script";

		// Token: 0x040009AE RID: 2478
		public const string ObjectToString = "ToString";

		// Token: 0x040009AF RID: 2479
		public const string ObjectEquals = "Equals";

		// Token: 0x040009B0 RID: 2480
		public const string ObjectGetHashCode = "GetHashCode";

		// Token: 0x040009B1 RID: 2481
		public const string ImplicitConversionName = "op_Implicit";

		// Token: 0x040009B2 RID: 2482
		public const string ExplicitConversionName = "op_Explicit";

		// Token: 0x040009B3 RID: 2483
		public const string AdditionOperatorName = "op_Addition";

		// Token: 0x040009B4 RID: 2484
		public const string BitwiseAndOperatorName = "op_BitwiseAnd";

		// Token: 0x040009B5 RID: 2485
		public const string BitwiseOrOperatorName = "op_BitwiseOr";

		// Token: 0x040009B6 RID: 2486
		public const string DecrementOperatorName = "op_Decrement";

		// Token: 0x040009B7 RID: 2487
		public const string DivisionOperatorName = "op_Division";

		// Token: 0x040009B8 RID: 2488
		public const string EqualityOperatorName = "op_Equality";

		// Token: 0x040009B9 RID: 2489
		public const string ExclusiveOrOperatorName = "op_ExclusiveOr";

		// Token: 0x040009BA RID: 2490
		public const string FalseOperatorName = "op_False";

		// Token: 0x040009BB RID: 2491
		public const string GreaterThanOperatorName = "op_GreaterThan";

		// Token: 0x040009BC RID: 2492
		public const string GreaterThanOrEqualOperatorName = "op_GreaterThanOrEqual";

		// Token: 0x040009BD RID: 2493
		public const string IncrementOperatorName = "op_Increment";

		// Token: 0x040009BE RID: 2494
		public const string InequalityOperatorName = "op_Inequality";

		// Token: 0x040009BF RID: 2495
		public const string LeftShiftOperatorName = "op_LeftShift";

		// Token: 0x040009C0 RID: 2496
		public const string UnsignedLeftShiftOperatorName = "op_UnsignedLeftShift";

		// Token: 0x040009C1 RID: 2497
		public const string LessThanOperatorName = "op_LessThan";

		// Token: 0x040009C2 RID: 2498
		public const string LessThanOrEqualOperatorName = "op_LessThanOrEqual";

		// Token: 0x040009C3 RID: 2499
		public const string LogicalNotOperatorName = "op_LogicalNot";

		// Token: 0x040009C4 RID: 2500
		public const string LogicalOrOperatorName = "op_LogicalOr";

		// Token: 0x040009C5 RID: 2501
		public const string LogicalAndOperatorName = "op_LogicalAnd";

		// Token: 0x040009C6 RID: 2502
		public const string ModulusOperatorName = "op_Modulus";

		// Token: 0x040009C7 RID: 2503
		public const string MultiplyOperatorName = "op_Multiply";

		// Token: 0x040009C8 RID: 2504
		public const string OnesComplementOperatorName = "op_OnesComplement";

		// Token: 0x040009C9 RID: 2505
		public const string RightShiftOperatorName = "op_RightShift";

		// Token: 0x040009CA RID: 2506
		public const string UnsignedRightShiftOperatorName = "op_UnsignedRightShift";

		// Token: 0x040009CB RID: 2507
		public const string SubtractionOperatorName = "op_Subtraction";

		// Token: 0x040009CC RID: 2508
		public const string TrueOperatorName = "op_True";

		// Token: 0x040009CD RID: 2509
		public const string UnaryNegationOperatorName = "op_UnaryNegation";

		// Token: 0x040009CE RID: 2510
		public const string UnaryPlusOperatorName = "op_UnaryPlus";

		// Token: 0x040009CF RID: 2511
		public const string ConcatenateOperatorName = "op_Concatenate";

		// Token: 0x040009D0 RID: 2512
		public const string ExponentOperatorName = "op_Exponent";

		// Token: 0x040009D1 RID: 2513
		public const string IntegerDivisionOperatorName = "op_IntegerDivision";

		// Token: 0x040009D2 RID: 2514
		public const string LikeOperatorName = "op_Like";

		// Token: 0x040009D3 RID: 2515
		public const string GetEnumeratorMethodName = "GetEnumerator";

		// Token: 0x040009D4 RID: 2516
		public const string GetAsyncEnumeratorMethodName = "GetAsyncEnumerator";

		// Token: 0x040009D5 RID: 2517
		public const string MoveNextAsyncMethodName = "MoveNextAsync";

		// Token: 0x040009D6 RID: 2518
		public const string DeconstructMethodName = "Deconstruct";

		// Token: 0x040009D7 RID: 2519
		public const string MoveNextMethodName = "MoveNext";

		// Token: 0x040009D8 RID: 2520
		public const string CurrentPropertyName = "Current";

		// Token: 0x040009D9 RID: 2521
		public const string ValuePropertyName = "Value";

		// Token: 0x040009DA RID: 2522
		public const string CollectionInitializerAddMethodName = "Add";

		// Token: 0x040009DB RID: 2523
		public const string GetAwaiter = "GetAwaiter";

		// Token: 0x040009DC RID: 2524
		public const string IsCompleted = "IsCompleted";

		// Token: 0x040009DD RID: 2525
		public const string GetResult = "GetResult";

		// Token: 0x040009DE RID: 2526
		public const string OnCompleted = "OnCompleted";

		// Token: 0x040009DF RID: 2527
		public const string DisposeMethodName = "Dispose";

		// Token: 0x040009E0 RID: 2528
		public const string DisposeAsyncMethodName = "DisposeAsync";

		// Token: 0x040009E1 RID: 2529
		public const string CountPropertyName = "Count";

		// Token: 0x040009E2 RID: 2530
		public const string LengthPropertyName = "Length";

		// Token: 0x040009E3 RID: 2531
		public const string SliceMethodName = "Slice";

		// Token: 0x040009E4 RID: 2532
		internal const string CloneMethodName = "<Clone>$";

		// Token: 0x040009E5 RID: 2533
		public const string PrintMembersMethodName = "PrintMembers";

		// Token: 0x040009E6 RID: 2534
		public const string TopLevelStatementsEntryPointMethodName = "<Main>$";

		// Token: 0x040009E7 RID: 2535
		public const string TopLevelStatementsEntryPointTypeName = "<Program>$";
	}
}
