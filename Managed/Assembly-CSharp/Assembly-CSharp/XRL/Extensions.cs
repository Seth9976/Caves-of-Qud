using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Collections;
using XRL.Language;
using XRL.Rules;
using XRL.World;

namespace XRL
{
	// Token: 0x020004DC RID: 1244
	public static class Extensions
	{
		// Token: 0x06003540 RID: 13632 RVA: 0x001011F4 File Offset: 0x000FF3F4
		public static RectTransform CopyFrom(this RectTransform self, RectTransform other)
		{
			if (self.anchorMin != other.anchorMin)
			{
				self.anchorMin = other.anchorMin;
			}
			if (self.anchorMax != other.anchorMax)
			{
				self.anchorMax = other.anchorMax;
			}
			if (self.anchoredPosition != other.anchoredPosition)
			{
				self.anchoredPosition = other.anchoredPosition;
			}
			if (self.sizeDelta != other.sizeDelta)
			{
				self.sizeDelta = other.sizeDelta;
			}
			if (self.pivot != other.pivot)
			{
				self.pivot = other.pivot;
			}
			return self;
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x0010129D File Offset: 0x000FF49D
		public static string Things(this int num, string what, string whatPlural = null)
		{
			if (num == 1)
			{
				return "1 " + what;
			}
			return num.ToString() + " " + (whatPlural ?? Grammar.Pluralize(what));
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x001012CB File Offset: 0x000FF4CB
		public static string Things(this float num, string what, string whatPlural = null)
		{
			if (num == 1f)
			{
				return "1 " + what;
			}
			return num.ToString() + " " + (whatPlural ?? Grammar.Pluralize(what));
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x001012FD File Offset: 0x000FF4FD
		public static string Things(this double num, string what, string whatPlural = null)
		{
			if (num == 1.0)
			{
				return "1 " + what;
			}
			return num.ToString() + " " + (whatPlural ?? Grammar.Pluralize(what));
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x00101334 File Offset: 0x000FF534
		public static StringBuilder DumpStringBuilder(this List<XRL.World.GameObject> list, StringBuilder SB = null)
		{
			if (SB == null)
			{
				SB = new StringBuilder();
			}
			for (int i = 0; i < list.Count; i++)
			{
				SB.Append(i).Append(": ").Append(list[i].DebugName)
					.Append('\n');
			}
			return SB;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x00101387 File Offset: 0x000FF587
		public static StringBuilder AppendColored(this StringBuilder SB, string color, string text)
		{
			return SB.Append("{{").Append(color).Append("|")
				.Append(text)
				.Append("}}");
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x001013B4 File Offset: 0x000FF5B4
		public static StringBuilder AppendRules(this StringBuilder SB, string text)
		{
			if (!text.IsNullOrEmpty())
			{
				SB.Append("\n{{rules|").Append(text).Append("}}");
			}
			return SB;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x001013DB File Offset: 0x000FF5DB
		public static StringBuilder AppendRules(this StringBuilder SB, string text, Action<StringBuilder> proc)
		{
			if (!text.IsNullOrEmpty())
			{
				SB.Append("\n{{rules|").Append(text);
				if (proc != null)
				{
					proc(SB);
				}
				SB.Append("}}");
			}
			return SB;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x0010140E File Offset: 0x000FF60E
		public static StringBuilder AppendRules(this StringBuilder SB, Action<StringBuilder> appender)
		{
			SB.Append("\n{{rules|");
			appender(SB);
			SB.Append("}}");
			return SB;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x00101430 File Offset: 0x000FF630
		public static StringBuilder AppendRules(this StringBuilder SB, Action<StringBuilder> appender, Action<StringBuilder> proc)
		{
			SB.Append("\n{{rules|");
			appender(SB);
			proc(SB);
			SB.Append("}}");
			return SB;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x00101459 File Offset: 0x000FF659
		public static string Dump(this List<XRL.World.GameObject> list)
		{
			return list.DumpStringBuilder(null).ToString();
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x00101467 File Offset: 0x000FF667
		public static int Roll(this string Dice)
		{
			return Stat.Roll(Dice, null);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x00101470 File Offset: 0x000FF670
		public static int RollMin(this string Dice)
		{
			return Stat.RollMin(Dice);
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x00101478 File Offset: 0x000FF678
		public static int RollMax(this string Dice)
		{
			return Stat.RollMax(Dice);
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x00101480 File Offset: 0x000FF680
		public static int RollCached(this string Dice)
		{
			return Stat.RollCached(Dice);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x00101488 File Offset: 0x000FF688
		public static int RollMinCached(this string Dice)
		{
			return Stat.RollMinCached(Dice);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x00101490 File Offset: 0x000FF690
		public static int RollMaxCached(this string Dice)
		{
			return Stat.RollMaxCached(Dice);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x00101498 File Offset: 0x000FF698
		public static double RollAverageCached(this string Dice)
		{
			return Stat.RollAverageCached(Dice);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x001014A0 File Offset: 0x000FF6A0
		public static DieRoll GetCachedDieRoll(this string Dice)
		{
			return Stat.GetCachedDieRoll(Dice);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x001014A8 File Offset: 0x000FF6A8
		public static SynchronizationContextAwaiter GetAwaiter(this SynchronizationContext context)
		{
			return new SynchronizationContextAwaiter(context);
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x001014B0 File Offset: 0x000FF6B0
		public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			List<T> list2 = list as List<T>;
			if (list2 != null)
			{
				list2.AddRange(items);
				return;
			}
			PooledList<T> pooledList = list as PooledList<T>;
			if (pooledList != null)
			{
				pooledList.AddRange(items);
				return;
			}
			Rack<T> rack = list as Rack<T>;
			if (rack != null)
			{
				rack.AddRange(items);
				return;
			}
			foreach (T t in items)
			{
				list.Add(t);
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x0010154C File Offset: 0x000FF74C
		public static void StableSortInPlace<T>(this IList<T> List) where T : IComparable<T>
		{
			Algorithms.StableSortInPlace<T>(List);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x00101554 File Offset: 0x000FF754
		public static void StableSortInPlace<T>(this IList<T> List, Comparison<T> Comparison)
		{
			Algorithms.StableSortInPlace<T>(List, Comparison);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x0010155D File Offset: 0x000FF75D
		public static void StableSortInPlace<T>(this IList<T> List, IComparer<T> Comparer)
		{
			Algorithms.StableSortInPlace<T>(List, Comparer);
		}

		/// <summary>
		///     Gets a <see cref="!:ScopeDisposedList" /> copy of the source collection.
		/// </summary>
		/// <remarks>  The resulting collection must be <c>Dispose()</c>ed of. Intended to be used
		///     with <c>using</c>: <c>using var copy = list.GetScopeDisposedCopy();</c>
		/// </remarks>
		// Token: 0x06003558 RID: 13656 RVA: 0x00101566 File Offset: 0x000FF766
		public static ScopeDisposedList<T> GetScopeDisposedCopy<T>(this IEnumerable<T> sourceList)
		{
			return ScopeDisposedList<T>.GetFromPoolFilledWith(sourceList);
		}

		/// <summary>
		///     Gets a <see cref="!:ScopeDisposedList" /> copy of the source collection.
		/// </summary>
		/// <remarks>  The resulting collection must be <c>Dispose()</c>ed of. Intended to be used
		///     with <c>using</c>: <c>using var copy = list.GetScopeDisposedCopy();</c>
		/// </remarks>
		// Token: 0x06003559 RID: 13657 RVA: 0x0010156E File Offset: 0x000FF76E
		public static ScopeDisposedList<T> GetScopeDisposedCopy<T>(this IReadOnlyCollection<T> sourceList)
		{
			return ScopeDisposedList<T>.GetFromPoolFilledWith(sourceList);
		}

		/// <summary>
		///     Gets a <see cref="!:ScopeDisposedList" /> copy of the source collection.
		/// </summary>
		/// <remarks>  The resulting collection must be <c>Dispose()</c>ed of. Intended to be used
		///     with <c>using</c>: <c>using var copy = list.GetScopeDisposedCopy();</c>
		/// </remarks>
		// Token: 0x0600355A RID: 13658 RVA: 0x00101576 File Offset: 0x000FF776
		public static ScopeDisposedList<T> GetScopeDisposedCopy<T>(this IReadOnlyList<T> sourceList)
		{
			return ScopeDisposedList<T>.GetFromPoolFilledWith(sourceList);
		}
	}
}
