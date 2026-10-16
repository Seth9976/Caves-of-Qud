using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000055 RID: 85
	public static class UniTaskExtensions
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00007820 File Offset: 0x00005A20
		public static UniTask.Awaiter GetAwaiter(this UniTask[] tasks)
		{
			return UniTask.WhenAll(tasks).GetAwaiter();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000783C File Offset: 0x00005A3C
		public static UniTask.Awaiter GetAwaiter(this IEnumerable<UniTask> tasks)
		{
			return UniTask.WhenAll(tasks).GetAwaiter();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007858 File Offset: 0x00005A58
		public static UniTask<T[]>.Awaiter GetAwaiter<T>(this UniTask<T>[] tasks)
		{
			return UniTask.WhenAll<T>(tasks).GetAwaiter();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007874 File Offset: 0x00005A74
		public static UniTask<T[]>.Awaiter GetAwaiter<T>(this IEnumerable<UniTask<T>> tasks)
		{
			return UniTask.WhenAll<T>(tasks).GetAwaiter();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007890 File Offset: 0x00005A90
		public static UniTask<ValueTuple<T1, T2>>.Awaiter GetAwaiter<T1, T2>([TupleElementNames(new string[] { "task1", "task2" })] this ValueTuple<UniTask<T1>, UniTask<T2>> tasks)
		{
			return UniTask.WhenAll<T1, T2>(tasks.Item1, tasks.Item2).GetAwaiter();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000078B8 File Offset: 0x00005AB8
		public static UniTask<ValueTuple<T1, T2, T3>>.Awaiter GetAwaiter<T1, T2, T3>([TupleElementNames(new string[] { "task1", "task2", "task3" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3>(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000078E4 File Offset: 0x00005AE4
		public static UniTask<ValueTuple<T1, T2, T3, T4>>.Awaiter GetAwaiter<T1, T2, T3, T4>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4).GetAwaiter();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007918 File Offset: 0x00005B18
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5).GetAwaiter();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007950 File Offset: 0x00005B50
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6).GetAwaiter();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007990 File Offset: 0x00005B90
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7" })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7).GetAwaiter();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000079D4 File Offset: 0x00005BD4
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8>([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", null })] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1).GetAwaiter();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007A24 File Offset: 0x00005C24
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", null,
			null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2).GetAwaiter();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007A80 File Offset: 0x00005C80
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3).GetAwaiter();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4).GetAwaiter();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007B58 File Offset: 0x00005D58
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", null, null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5).GetAwaiter();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", null, null, null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>, UniTask<T13>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5, tasks.Rest.Item6).GetAwaiter();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007C5C File Offset: 0x00005E5C
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", null, null, null, null, null, null,
			null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>, UniTask<T13>, UniTask<T14>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5, tasks.Rest.Item6, tasks.Rest.Item7).GetAwaiter();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public static UniTask<ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", "task15", null, null, null, null, null,
			null, null, null, null
		})] this ValueTuple<UniTask<T1>, UniTask<T2>, UniTask<T3>, UniTask<T4>, UniTask<T5>, UniTask<T6>, UniTask<T7>, ValueTuple<UniTask<T8>, UniTask<T9>, UniTask<T10>, UniTask<T11>, UniTask<T12>, UniTask<T13>, UniTask<T14>, ValueTuple<UniTask<T15>>>> tasks)
		{
			return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7, tasks.Rest.Item1, tasks.Rest.Item2, tasks.Rest.Item3, tasks.Rest.Item4, tasks.Rest.Item5, tasks.Rest.Item6, tasks.Rest.Item7, tasks.Rest.Rest.Item1).GetAwaiter();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007D94 File Offset: 0x00005F94
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2" })] this ValueTuple<UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2 }).GetAwaiter();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3" })] this ValueTuple<UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3 }).GetAwaiter();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007E18 File Offset: 0x00006018
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4 }).GetAwaiter();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007E6C File Offset: 0x0000606C
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5 }).GetAwaiter();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007ED0 File Offset: 0x000060D0
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6 }).GetAwaiter();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007F40 File Offset: 0x00006140
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7" })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask> tasks)
		{
			return UniTask.WhenAll(new UniTask[] { tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5, tasks.Item6, tasks.Item7 }).GetAwaiter();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007FBC File Offset: 0x000061BC
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[] { "task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", null })] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1
			}).GetAwaiter();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000804C File Offset: 0x0000624C
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", null,
			null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2
			}).GetAwaiter();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000080EC File Offset: 0x000062EC
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3
			}).GetAwaiter();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000081A0 File Offset: 0x000063A0
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4
			}).GetAwaiter();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008268 File Offset: 0x00006468
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", null, null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5
			}).GetAwaiter();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008344 File Offset: 0x00006544
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", null, null, null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5,
				tasks.Rest.Item6
			}).GetAwaiter();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008430 File Offset: 0x00006630
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", null, null, null, null, null, null,
			null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5,
				tasks.Rest.Item6,
				tasks.Rest.Item7
			}).GetAwaiter();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008530 File Offset: 0x00006730
		public static UniTask.Awaiter GetAwaiter([TupleElementNames(new string[]
		{
			"task1", "task2", "task3", "task4", "task5", "task6", "task7", "task8", "task9", "task10",
			"task11", "task12", "task13", "task14", "task15", null, null, null, null, null,
			null, null, null, null
		})] this ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, UniTask, ValueTuple<UniTask>>> tasks)
		{
			return UniTask.WhenAll(new UniTask[]
			{
				tasks.Item1,
				tasks.Item2,
				tasks.Item3,
				tasks.Item4,
				tasks.Item5,
				tasks.Item6,
				tasks.Item7,
				tasks.Rest.Item1,
				tasks.Rest.Item2,
				tasks.Rest.Item3,
				tasks.Rest.Item4,
				tasks.Rest.Item5,
				tasks.Rest.Item6,
				tasks.Rest.Item7,
				tasks.Rest.Rest.Item1
			}).GetAwaiter();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008648 File Offset: 0x00006848
		public static UniTask<T> AsUniTask<T>(this Task<T> task, bool useCurrentSynchronizationContext = true)
		{
			UniTaskCompletionSource<T> uniTaskCompletionSource = new UniTaskCompletionSource<T>();
			task.ContinueWith(delegate(Task<T> x, object state)
			{
				UniTaskCompletionSource<T> uniTaskCompletionSource2 = (UniTaskCompletionSource<T>)state;
				switch (x.Status)
				{
				case TaskStatus.RanToCompletion:
					uniTaskCompletionSource2.TrySetResult(x.Result);
					return;
				case TaskStatus.Canceled:
					uniTaskCompletionSource2.TrySetCanceled(default(CancellationToken));
					return;
				case TaskStatus.Faulted:
					uniTaskCompletionSource2.TrySetException(x.Exception.InnerException ?? x.Exception);
					return;
				default:
					throw new NotSupportedException();
				}
			}, uniTaskCompletionSource, useCurrentSynchronizationContext ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);
			return uniTaskCompletionSource.Task;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008698 File Offset: 0x00006898
		public static UniTask AsUniTask(this Task task, bool useCurrentSynchronizationContext = true)
		{
			UniTaskCompletionSource uniTaskCompletionSource = new UniTaskCompletionSource();
			task.ContinueWith(delegate(Task x, object state)
			{
				UniTaskCompletionSource uniTaskCompletionSource2 = (UniTaskCompletionSource)state;
				switch (x.Status)
				{
				case TaskStatus.RanToCompletion:
					uniTaskCompletionSource2.TrySetResult();
					return;
				case TaskStatus.Canceled:
					uniTaskCompletionSource2.TrySetCanceled(default(CancellationToken));
					return;
				case TaskStatus.Faulted:
					uniTaskCompletionSource2.TrySetException(x.Exception.InnerException ?? x.Exception);
					return;
				default:
					throw new NotSupportedException();
				}
			}, uniTaskCompletionSource, useCurrentSynchronizationContext ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);
			return uniTaskCompletionSource.Task;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000086E8 File Offset: 0x000068E8
		public static Task<T> AsTask<T>(this UniTask<T> task)
		{
			Task<T> task2;
			try
			{
				UniTask<T>.Awaiter awaiter;
				try
				{
					awaiter = task.GetAwaiter();
				}
				catch (Exception ex)
				{
					return Task.FromException<T>(ex);
				}
				if (awaiter.IsCompleted)
				{
					try
					{
						return Task.FromResult<T>(awaiter.GetResult());
					}
					catch (Exception ex2)
					{
						return Task.FromException<T>(ex2);
					}
				}
				TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
				awaiter.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<TaskCompletionSource<T>, UniTask<T>.Awaiter> stateTuple = (StateTuple<TaskCompletionSource<T>, UniTask<T>.Awaiter>)state)
					{
						TaskCompletionSource<T> taskCompletionSource2;
						UniTask<T>.Awaiter awaiter2;
						stateTuple.Deconstruct(out taskCompletionSource2, out awaiter2);
						TaskCompletionSource<T> taskCompletionSource3 = taskCompletionSource2;
						UniTask<T>.Awaiter awaiter3 = awaiter2;
						try
						{
							T result = awaiter3.GetResult();
							taskCompletionSource3.SetResult(result);
						}
						catch (Exception ex4)
						{
							taskCompletionSource3.SetException(ex4);
						}
					}
				}, StateTuple.Create<TaskCompletionSource<T>, UniTask<T>.Awaiter>(taskCompletionSource, awaiter));
				task2 = taskCompletionSource.Task;
			}
			catch (Exception ex3)
			{
				task2 = Task.FromException<T>(ex3);
			}
			return task2;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008794 File Offset: 0x00006994
		public static Task AsTask(this UniTask task)
		{
			Task task2;
			try
			{
				UniTask.Awaiter awaiter;
				try
				{
					awaiter = task.GetAwaiter();
				}
				catch (Exception ex)
				{
					return Task.FromException(ex);
				}
				if (awaiter.IsCompleted)
				{
					try
					{
						awaiter.GetResult();
						return Task.CompletedTask;
					}
					catch (Exception ex2)
					{
						return Task.FromException(ex2);
					}
				}
				TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
				awaiter.SourceOnCompleted(delegate(object state)
				{
					using (StateTuple<TaskCompletionSource<object>, UniTask.Awaiter> stateTuple = (StateTuple<TaskCompletionSource<object>, UniTask.Awaiter>)state)
					{
						TaskCompletionSource<object> taskCompletionSource2;
						UniTask.Awaiter awaiter2;
						stateTuple.Deconstruct(out taskCompletionSource2, out awaiter2);
						TaskCompletionSource<object> taskCompletionSource3 = taskCompletionSource2;
						UniTask.Awaiter awaiter3 = awaiter2;
						try
						{
							awaiter3.GetResult();
							taskCompletionSource3.SetResult(null);
						}
						catch (Exception ex4)
						{
							taskCompletionSource3.SetException(ex4);
						}
					}
				}, StateTuple.Create<TaskCompletionSource<object>, UniTask.Awaiter>(taskCompletionSource, awaiter));
				task2 = taskCompletionSource.Task;
			}
			catch (Exception ex3)
			{
				task2 = Task.FromException(ex3);
			}
			return task2;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008840 File Offset: 0x00006A40
		public static AsyncLazy ToAsyncLazy(this UniTask task)
		{
			return new AsyncLazy(task);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008848 File Offset: 0x00006A48
		public static AsyncLazy<T> ToAsyncLazy<T>(this UniTask<T> task)
		{
			return new AsyncLazy<T>(task);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008850 File Offset: 0x00006A50
		public static UniTask AttachExternalCancellation(this UniTask task, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled)
			{
				return task;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				task.Forget();
				return UniTask.FromCanceled(cancellationToken);
			}
			if (task.Status.IsCompleted())
			{
				return task;
			}
			return new UniTask(new UniTaskExtensions.AttachExternalCancellationSource(task, cancellationToken), 0);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008890 File Offset: 0x00006A90
		public static UniTask<T> AttachExternalCancellation<T>(this UniTask<T> task, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled)
			{
				return task;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				task.Forget<T>();
				return UniTask.FromCanceled<T>(cancellationToken);
			}
			if (task.Status.IsCompleted())
			{
				return task;
			}
			return new UniTask<T>(new UniTaskExtensions.AttachExternalCancellationSource<T>(task, cancellationToken), 0);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000088D0 File Offset: 0x00006AD0
		public static IEnumerator ToCoroutine<T>(this UniTask<T> task, Action<T> resultHandler = null, Action<Exception> exceptionHandler = null)
		{
			return new UniTaskExtensions.ToCoroutineEnumerator<T>(task, resultHandler, exceptionHandler);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000088DA File Offset: 0x00006ADA
		public static IEnumerator ToCoroutine(this UniTask task, Action<Exception> exceptionHandler = null)
		{
			return new UniTaskExtensions.ToCoroutineEnumerator(task, exceptionHandler);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000088E4 File Offset: 0x00006AE4
		public static async UniTask Timeout(this UniTask task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> uniTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int item;
			bool item2;
			try
			{
				object obj = await UniTask.WhenAny<bool, bool>(task.SuppressCancellationThrow(), uniTask);
				item = obj.Item1;
				item2 = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				throw;
			}
			if (item == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				string text = "Exceed Timeout:";
				TimeSpan timeSpan = timeout;
				throw new TimeoutException(text + timeSpan.ToString());
			}
			delayCancellationTokenSource.Cancel();
			delayCancellationTokenSource.Dispose();
			if (item2)
			{
				Error.ThrowOperationCanceledException();
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008948 File Offset: 0x00006B48
		public static async UniTask<T> Timeout<T>(this UniTask<T> task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> uniTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int item;
			ValueTuple<bool, T> item2;
			try
			{
				object obj = await UniTask.WhenAny<ValueTuple<bool, T>, bool>(task.SuppressCancellationThrow(), uniTask);
				item = obj.Item1;
				item2 = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				throw;
			}
			if (item == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				string text = "Exceed Timeout:";
				TimeSpan timeSpan = timeout;
				throw new TimeoutException(text + timeSpan.ToString());
			}
			delayCancellationTokenSource.Cancel();
			delayCancellationTokenSource.Dispose();
			if (item2.Item1)
			{
				Error.ThrowOperationCanceledException();
			}
			return item2.Item2;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000089AC File Offset: 0x00006BAC
		public static async UniTask<bool> TimeoutWithoutException(this UniTask task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> uniTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int item;
			bool item2;
			try
			{
				object obj = await UniTask.WhenAny<bool, bool>(task.SuppressCancellationThrow(), uniTask);
				item = obj.Item1;
				item2 = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				return 1;
			}
			bool flag;
			if (item == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				flag = true;
			}
			else
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				if (item2)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008A10 File Offset: 0x00006C10
		[return: TupleElementNames(new string[] { "IsTimeout", "Result" })]
		public static async UniTask<ValueTuple<bool, T>> TimeoutWithoutException<T>(this UniTask<T> task, TimeSpan timeout, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
		{
			CancellationTokenSource delayCancellationTokenSource = new CancellationTokenSource();
			UniTask<bool> uniTask = UniTask.Delay(timeout, delayType, timeoutCheckTiming, delayCancellationTokenSource.Token, false).SuppressCancellationThrow();
			int item;
			ValueTuple<bool, T> item2;
			try
			{
				object obj = await UniTask.WhenAny<ValueTuple<bool, T>, bool>(task.SuppressCancellationThrow(), uniTask);
				item = obj.Item1;
				item2 = obj.Item2;
			}
			catch
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				return new ValueTuple<bool, T>(true, default(T));
			}
			ValueTuple<bool, T> valueTuple;
			if (item == 1)
			{
				if (taskCancellationTokenSource != null)
				{
					taskCancellationTokenSource.Cancel();
					taskCancellationTokenSource.Dispose();
				}
				valueTuple = new ValueTuple<bool, T>(true, default(T));
			}
			else
			{
				delayCancellationTokenSource.Cancel();
				delayCancellationTokenSource.Dispose();
				if (item2.Item1)
				{
					valueTuple = new ValueTuple<bool, T>(true, default(T));
				}
				else
				{
					valueTuple = new ValueTuple<bool, T>(false, item2.Item2);
				}
			}
			return valueTuple;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008A74 File Offset: 0x00006C74
		public static void Forget(this UniTask task)
		{
			UniTask.Awaiter awaiter = task.GetAwaiter();
			if (awaiter.IsCompleted)
			{
				try
				{
					awaiter.GetResult();
					return;
				}
				catch (Exception ex)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex);
					return;
				}
			}
			awaiter.SourceOnCompleted(delegate(object state)
			{
				using (StateTuple<UniTask.Awaiter> stateTuple = (StateTuple<UniTask.Awaiter>)state)
				{
					try
					{
						stateTuple.Item1.GetResult();
					}
					catch (Exception ex2)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
					}
				}
			}, StateTuple.Create<UniTask.Awaiter>(awaiter));
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public static void Forget(this UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
		{
			if (exceptionHandler == null)
			{
				task.Forget();
				return;
			}
			UniTaskExtensions.ForgetCoreWithCatch(task, exceptionHandler, handleExceptionOnMainThread).Forget();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008B08 File Offset: 0x00006D08
		private static async UniTaskVoid ForgetCoreWithCatch(UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
		{
			int num = 0;
			try
			{
				await task;
			}
			catch (Exception obj)
			{
				num = 1;
			}
			object obj;
			if (num == 1)
			{
				Exception ex = (Exception)obj;
				try
				{
					if (handleExceptionOnMainThread)
					{
						await UniTask.SwitchToMainThread(default(CancellationToken));
					}
					exceptionHandler(ex);
				}
				catch (Exception ex2)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex2);
				}
				ex = null;
			}
			obj = null;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008B5C File Offset: 0x00006D5C
		public static void Forget<T>(this UniTask<T> task)
		{
			UniTask<T>.Awaiter awaiter = task.GetAwaiter();
			if (awaiter.IsCompleted)
			{
				try
				{
					awaiter.GetResult();
					return;
				}
				catch (Exception ex)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex);
					return;
				}
			}
			awaiter.SourceOnCompleted(delegate(object state)
			{
				using (StateTuple<UniTask<T>.Awaiter> stateTuple = (StateTuple<UniTask<T>.Awaiter>)state)
				{
					try
					{
						stateTuple.Item1.GetResult();
					}
					catch (Exception ex2)
					{
						UniTaskScheduler.PublishUnobservedTaskException(ex2);
					}
				}
			}, StateTuple.Create<UniTask<T>.Awaiter>(awaiter));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public static void Forget<T>(this UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
		{
			if (exceptionHandler == null)
			{
				task.Forget<T>();
				return;
			}
			UniTaskExtensions.ForgetCoreWithCatch<T>(task, exceptionHandler, handleExceptionOnMainThread).Forget();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008BF0 File Offset: 0x00006DF0
		private static async UniTaskVoid ForgetCoreWithCatch<T>(UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
		{
			int num = 0;
			try
			{
				await task;
			}
			catch (Exception obj)
			{
				num = 1;
			}
			object obj;
			if (num == 1)
			{
				Exception ex = (Exception)obj;
				try
				{
					if (handleExceptionOnMainThread)
					{
						await UniTask.SwitchToMainThread(default(CancellationToken));
					}
					exceptionHandler(ex);
				}
				catch (Exception ex2)
				{
					UniTaskScheduler.PublishUnobservedTaskException(ex2);
				}
				ex = null;
			}
			obj = null;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008C44 File Offset: 0x00006E44
		public static async UniTask ContinueWith<T>(this UniTask<T> task, Action<T> continuationFunction)
		{
			Action<T> action = continuationFunction;
			T t = await task;
			action(t);
			action = null;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008C90 File Offset: 0x00006E90
		public static async UniTask ContinueWith<T>(this UniTask<T> task, Func<T, UniTask> continuationFunction)
		{
			Func<T, UniTask> func = continuationFunction;
			T t = await task;
			await func(t);
			func = null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008CDC File Offset: 0x00006EDC
		public static async UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, TR> continuationFunction)
		{
			T t = await task;
			return continuationFunction(t);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008D28 File Offset: 0x00006F28
		public static async UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, UniTask<TR>> continuationFunction)
		{
			Func<T, UniTask<TR>> func = continuationFunction;
			T t = await task;
			UniTask<TR> uniTask = await func(t);
			func = null;
			return uniTask;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008D74 File Offset: 0x00006F74
		public static async UniTask ContinueWith(this UniTask task, Action continuationFunction)
		{
			await task;
			continuationFunction();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008DC0 File Offset: 0x00006FC0
		public static async UniTask ContinueWith(this UniTask task, Func<UniTask> continuationFunction)
		{
			await task;
			await continuationFunction();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008E0C File Offset: 0x0000700C
		public static async UniTask<T> ContinueWith<T>(this UniTask task, Func<T> continuationFunction)
		{
			await task;
			return continuationFunction();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008E58 File Offset: 0x00007058
		public static async UniTask<T> ContinueWith<T>(this UniTask task, Func<UniTask<T>> continuationFunction)
		{
			await task;
			return await continuationFunction();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008EA4 File Offset: 0x000070A4
		public static async UniTask<T> Unwrap<T>(this UniTask<UniTask<T>> task)
		{
			return await (await task);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008EE8 File Offset: 0x000070E8
		public static async UniTask Unwrap(this UniTask<UniTask> task)
		{
			await (await task);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008F2C File Offset: 0x0000712C
		public static async UniTask<T> Unwrap<T>(this Task<UniTask<T>> task)
		{
			return await (await task);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008F70 File Offset: 0x00007170
		public static async UniTask<T> Unwrap<T>(this Task<UniTask<T>> task, bool continueOnCapturedContext)
		{
			return await (await task.ConfigureAwait(continueOnCapturedContext));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008FBC File Offset: 0x000071BC
		public static async UniTask Unwrap(this Task<UniTask> task)
		{
			await (await task);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009000 File Offset: 0x00007200
		public static async UniTask Unwrap(this Task<UniTask> task, bool continueOnCapturedContext)
		{
			await (await task.ConfigureAwait(continueOnCapturedContext));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000904C File Offset: 0x0000724C
		public static async UniTask<T> Unwrap<T>(this UniTask<Task<T>> task)
		{
			return await (await task);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009090 File Offset: 0x00007290
		public static async UniTask<T> Unwrap<T>(this UniTask<Task<T>> task, bool continueOnCapturedContext)
		{
			return await (await task).ConfigureAwait(continueOnCapturedContext);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000090DC File Offset: 0x000072DC
		public static async UniTask Unwrap(this UniTask<Task> task)
		{
			await (await task);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009120 File Offset: 0x00007320
		public static async UniTask Unwrap(this UniTask<Task> task, bool continueOnCapturedContext)
		{
			await (await task).ConfigureAwait(continueOnCapturedContext);
		}

		// Token: 0x020001C9 RID: 457
		private sealed class AttachExternalCancellationSource : IUniTaskSource, IValueTaskSource
		{
			// Token: 0x06000A97 RID: 2711 RVA: 0x00025338 File Offset: 0x00023538
			public AttachExternalCancellationSource(UniTask task, CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
				this.tokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskExtensions.AttachExternalCancellationSource.cancellationCallbackDelegate, this);
				this.RunTask(task).Forget();
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x00025374 File Offset: 0x00023574
			private async UniTaskVoid RunTask(UniTask task)
			{
				try
				{
					await task;
					this.core.TrySetResult(AsyncUnit.Default);
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
				}
				finally
				{
					this.tokenRegistration.Dispose();
				}
			}

			// Token: 0x06000A99 RID: 2713 RVA: 0x000253C0 File Offset: 0x000235C0
			private static void CancellationCallback(object state)
			{
				UniTaskExtensions.AttachExternalCancellationSource attachExternalCancellationSource = (UniTaskExtensions.AttachExternalCancellationSource)state;
				attachExternalCancellationSource.core.TrySetCanceled(attachExternalCancellationSource.cancellationToken);
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x000253E6 File Offset: 0x000235E6
			public void GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x06000A9B RID: 2715 RVA: 0x000253F5 File Offset: 0x000235F5
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000A9C RID: 2716 RVA: 0x00025403 File Offset: 0x00023603
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000A9D RID: 2717 RVA: 0x00025413 File Offset: 0x00023613
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x040003B5 RID: 949
			private static readonly Action<object> cancellationCallbackDelegate = new Action<object>(UniTaskExtensions.AttachExternalCancellationSource.CancellationCallback);

			// Token: 0x040003B6 RID: 950
			private CancellationToken cancellationToken;

			// Token: 0x040003B7 RID: 951
			private CancellationTokenRegistration tokenRegistration;

			// Token: 0x040003B8 RID: 952
			private UniTaskCompletionSourceCore<AsyncUnit> core;
		}

		// Token: 0x020001CA RID: 458
		private sealed class AttachExternalCancellationSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x06000A9F RID: 2719 RVA: 0x00025434 File Offset: 0x00023634
			public AttachExternalCancellationSource(UniTask<T> task, CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
				this.tokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(UniTaskExtensions.AttachExternalCancellationSource<T>.cancellationCallbackDelegate, this);
				this.RunTask(task).Forget();
			}

			// Token: 0x06000AA0 RID: 2720 RVA: 0x00025470 File Offset: 0x00023670
			private async UniTaskVoid RunTask(UniTask<T> task)
			{
				try
				{
					T t = await task;
					this.core.TrySetResult(t);
				}
				catch (Exception ex)
				{
					this.core.TrySetException(ex);
				}
				finally
				{
					this.tokenRegistration.Dispose();
				}
			}

			// Token: 0x06000AA1 RID: 2721 RVA: 0x000254BC File Offset: 0x000236BC
			private static void CancellationCallback(object state)
			{
				UniTaskExtensions.AttachExternalCancellationSource<T> attachExternalCancellationSource = (UniTaskExtensions.AttachExternalCancellationSource<T>)state;
				attachExternalCancellationSource.core.TrySetCanceled(attachExternalCancellationSource.cancellationToken);
			}

			// Token: 0x06000AA2 RID: 2722 RVA: 0x000254E2 File Offset: 0x000236E2
			void IUniTaskSource.GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x000254F1 File Offset: 0x000236F1
			public T GetResult(short token)
			{
				return this.core.GetResult(token);
			}

			// Token: 0x06000AA4 RID: 2724 RVA: 0x000254FF File Offset: 0x000236FF
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000AA5 RID: 2725 RVA: 0x0002550D File Offset: 0x0002370D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000AA6 RID: 2726 RVA: 0x0002551D File Offset: 0x0002371D
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x040003B9 RID: 953
			private static readonly Action<object> cancellationCallbackDelegate = new Action<object>(UniTaskExtensions.AttachExternalCancellationSource<T>.CancellationCallback);

			// Token: 0x040003BA RID: 954
			private CancellationToken cancellationToken;

			// Token: 0x040003BB RID: 955
			private CancellationTokenRegistration tokenRegistration;

			// Token: 0x040003BC RID: 956
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x020001CB RID: 459
		private sealed class ToCoroutineEnumerator : IEnumerator
		{
			// Token: 0x06000AA8 RID: 2728 RVA: 0x0002553D File Offset: 0x0002373D
			public ToCoroutineEnumerator(UniTask task, Action<Exception> exceptionHandler)
			{
				this.completed = false;
				this.exceptionHandler = exceptionHandler;
				this.task = task;
			}

			// Token: 0x06000AA9 RID: 2729 RVA: 0x0002555C File Offset: 0x0002375C
			private async UniTaskVoid RunTask(UniTask task)
			{
				try
				{
					await task;
				}
				catch (Exception ex)
				{
					if (this.exceptionHandler != null)
					{
						this.exceptionHandler(ex);
					}
					else
					{
						this.exception = ExceptionDispatchInfo.Capture(ex);
					}
				}
				finally
				{
					this.completed = true;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000255A7 File Offset: 0x000237A7
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000AAB RID: 2731 RVA: 0x000255AC File Offset: 0x000237AC
			public bool MoveNext()
			{
				if (!this.isStarted)
				{
					this.isStarted = true;
					this.RunTask(this.task).Forget();
				}
				if (this.exception != null)
				{
					this.exception.Throw();
					return false;
				}
				return !this.completed;
			}

			// Token: 0x06000AAC RID: 2732 RVA: 0x000255FA File Offset: 0x000237FA
			void IEnumerator.Reset()
			{
			}

			// Token: 0x040003BD RID: 957
			private bool completed;

			// Token: 0x040003BE RID: 958
			private UniTask task;

			// Token: 0x040003BF RID: 959
			private Action<Exception> exceptionHandler;

			// Token: 0x040003C0 RID: 960
			private bool isStarted;

			// Token: 0x040003C1 RID: 961
			private ExceptionDispatchInfo exception;
		}

		// Token: 0x020001CC RID: 460
		private sealed class ToCoroutineEnumerator<T> : IEnumerator
		{
			// Token: 0x06000AAD RID: 2733 RVA: 0x000255FC File Offset: 0x000237FC
			public ToCoroutineEnumerator(UniTask<T> task, Action<T> resultHandler, Action<Exception> exceptionHandler)
			{
				this.completed = false;
				this.task = task;
				this.resultHandler = resultHandler;
				this.exceptionHandler = exceptionHandler;
			}

			// Token: 0x06000AAE RID: 2734 RVA: 0x00025620 File Offset: 0x00023820
			private async UniTaskVoid RunTask(UniTask<T> task)
			{
				try
				{
					T t = await task;
					this.current = t;
					if (this.resultHandler != null)
					{
						this.resultHandler(t);
					}
				}
				catch (Exception ex)
				{
					if (this.exceptionHandler != null)
					{
						this.exceptionHandler(ex);
					}
					else
					{
						this.exception = ExceptionDispatchInfo.Capture(ex);
					}
				}
				finally
				{
					this.completed = true;
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002566B File Offset: 0x0002386B
			public object Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06000AB0 RID: 2736 RVA: 0x00025674 File Offset: 0x00023874
			public bool MoveNext()
			{
				if (!this.isStarted)
				{
					this.isStarted = true;
					this.RunTask(this.task).Forget();
				}
				if (this.exception != null)
				{
					this.exception.Throw();
					return false;
				}
				return !this.completed;
			}

			// Token: 0x06000AB1 RID: 2737 RVA: 0x000256C2 File Offset: 0x000238C2
			void IEnumerator.Reset()
			{
			}

			// Token: 0x040003C2 RID: 962
			private bool completed;

			// Token: 0x040003C3 RID: 963
			private Action<T> resultHandler;

			// Token: 0x040003C4 RID: 964
			private Action<Exception> exceptionHandler;

			// Token: 0x040003C5 RID: 965
			private bool isStarted;

			// Token: 0x040003C6 RID: 966
			private UniTask<T> task;

			// Token: 0x040003C7 RID: 967
			private object current;

			// Token: 0x040003C8 RID: 968
			private ExceptionDispatchInfo exception;
		}
	}
}
