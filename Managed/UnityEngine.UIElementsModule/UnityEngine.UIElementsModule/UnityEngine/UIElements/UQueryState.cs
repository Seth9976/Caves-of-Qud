using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200051D RID: 1309
	public struct UQueryState<T> : IEnumerable<T>, IEnumerable, IEquatable<UQueryState<T>> where T : VisualElement
	{
		// Token: 0x06002874 RID: 10356 RVA: 0x000939D4 File Offset: 0x00091BD4
		internal UQueryState(VisualElement element, List<RuleMatcher> matchers)
		{
			this.m_Element = element;
			this.m_Matchers = matchers;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000939E8 File Offset: 0x00091BE8
		public UQueryState<T> RebuildOn(VisualElement element)
		{
			return new UQueryState<T>(element, this.m_Matchers);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00093A08 File Offset: 0x00091C08
		private T Single(UQuery.SingleQueryMatcher matcher)
		{
			bool flag = matcher.IsInUse();
			if (flag)
			{
				matcher = matcher.CreateNew();
			}
			matcher.Run(this.m_Element, this.m_Matchers);
			T t = matcher.match as T;
			matcher.match = null;
			return t;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00093A5B File Offset: 0x00091C5B
		public T First()
		{
			return this.Single(UQuery.FirstQueryMatcher.Instance);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00093A68 File Offset: 0x00091C68
		public T Last()
		{
			return this.Single(UQuery.LastQueryMatcher.Instance);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x00093A75 File Offset: 0x00091C75
		public void ToList(List<T> results)
		{
			UQueryState<T>.s_List.matches = results;
			UQueryState<T>.s_List.Run(this.m_Element, this.m_Matchers);
			UQueryState<T>.s_List.Reset();
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x00093AA8 File Offset: 0x00091CA8
		public List<T> ToList()
		{
			List<T> list = new List<T>();
			this.ToList(list);
			return list;
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00093ACC File Offset: 0x00091CCC
		public T AtIndex(int index)
		{
			UQuery.IndexQueryMatcher instance = UQuery.IndexQueryMatcher.Instance;
			instance.matchIndex = index;
			return this.Single(instance);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00093AF4 File Offset: 0x00091CF4
		public void ForEach(Action<T> funcCall)
		{
			UQueryState<T>.ActionQueryMatcher actionQueryMatcher = UQueryState<T>.s_Action;
			bool flag = actionQueryMatcher.callBack != null;
			if (flag)
			{
				actionQueryMatcher = new UQueryState<T>.ActionQueryMatcher();
			}
			try
			{
				actionQueryMatcher.callBack = funcCall;
				actionQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				actionQueryMatcher.callBack = null;
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x00093B58 File Offset: 0x00091D58
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			UQueryState<T>.DelegateQueryMatcher<T2> delegateQueryMatcher = UQueryState<T>.DelegateQueryMatcher<T2>.s_Instance;
			bool flag = delegateQueryMatcher.callBack != null;
			if (flag)
			{
				delegateQueryMatcher = new UQueryState<T>.DelegateQueryMatcher<T2>();
			}
			try
			{
				delegateQueryMatcher.callBack = funcCall;
				delegateQueryMatcher.result = result;
				delegateQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				delegateQueryMatcher.callBack = null;
				delegateQueryMatcher.result = null;
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x00093BCC File Offset: 0x00091DCC
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			List<T2> list = new List<T2>();
			this.ForEach<T2>(list, funcCall);
			return list;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x00093BEE File Offset: 0x00091DEE
		public UQueryState<T>.Enumerator GetEnumerator()
		{
			return new UQueryState<T>.Enumerator(this);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00093BFB File Offset: 0x00091DFB
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00093BFB File Offset: 0x00091DFB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x00093C08 File Offset: 0x00091E08
		public bool Equals(UQueryState<T> other)
		{
			return this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x00093C44 File Offset: 0x00091E44
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryState<T>);
			return !flag && this.Equals((UQueryState<T>)obj);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x00093C78 File Offset: 0x00091E78
		public override int GetHashCode()
		{
			int num = 488160421;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			return num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x00093CC4 File Offset: 0x00091EC4
		public static bool operator ==(UQueryState<T> state1, UQueryState<T> state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x00093CE0 File Offset: 0x00091EE0
		public static bool operator !=(UQueryState<T> state1, UQueryState<T> state2)
		{
			return !(state1 == state2);
		}

		// Token: 0x04001241 RID: 4673
		private static UQueryState<T>.ActionQueryMatcher s_Action = new UQueryState<T>.ActionQueryMatcher();

		// Token: 0x04001242 RID: 4674
		private readonly VisualElement m_Element;

		// Token: 0x04001243 RID: 4675
		internal readonly List<RuleMatcher> m_Matchers;

		// Token: 0x04001244 RID: 4676
		private static readonly UQueryState<T>.ListQueryMatcher<T> s_List = new UQueryState<T>.ListQueryMatcher<T>();

		// Token: 0x04001245 RID: 4677
		private static readonly UQueryState<T>.ListQueryMatcher<VisualElement> s_EnumerationList = new UQueryState<T>.ListQueryMatcher<VisualElement>();

		// Token: 0x0200051E RID: 1310
		private class ListQueryMatcher<TElement> : UQuery.UQueryMatcher where TElement : VisualElement
		{
			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x06002888 RID: 10376 RVA: 0x00093D1C File Offset: 0x00091F1C
			// (set) Token: 0x06002889 RID: 10377 RVA: 0x00093D24 File Offset: 0x00091F24
			public List<TElement> matches { get; set; }

			// Token: 0x0600288A RID: 10378 RVA: 0x00093D30 File Offset: 0x00091F30
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matches.Add(element as TElement);
				return false;
			}

			// Token: 0x0600288B RID: 10379 RVA: 0x00093D5A File Offset: 0x00091F5A
			public void Reset()
			{
				this.matches = null;
			}
		}

		// Token: 0x0200051F RID: 1311
		private class ActionQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000A2D RID: 2605
			// (get) Token: 0x0600288D RID: 10381 RVA: 0x00093D65 File Offset: 0x00091F65
			// (set) Token: 0x0600288E RID: 10382 RVA: 0x00093D6D File Offset: 0x00091F6D
			internal Action<T> callBack { get; set; }

			// Token: 0x0600288F RID: 10383 RVA: 0x00093D78 File Offset: 0x00091F78
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.callBack(t);
				}
				return false;
			}
		}

		// Token: 0x02000520 RID: 1312
		private class DelegateQueryMatcher<TReturnType> : UQuery.UQueryMatcher
		{
			// Token: 0x17000A2E RID: 2606
			// (get) Token: 0x06002891 RID: 10385 RVA: 0x00093DB3 File Offset: 0x00091FB3
			// (set) Token: 0x06002892 RID: 10386 RVA: 0x00093DBB File Offset: 0x00091FBB
			public Func<T, TReturnType> callBack { get; set; }

			// Token: 0x17000A2F RID: 2607
			// (get) Token: 0x06002893 RID: 10387 RVA: 0x00093DC4 File Offset: 0x00091FC4
			// (set) Token: 0x06002894 RID: 10388 RVA: 0x00093DCC File Offset: 0x00091FCC
			public List<TReturnType> result { get; set; }

			// Token: 0x06002895 RID: 10389 RVA: 0x00093DD8 File Offset: 0x00091FD8
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.result.Add(this.callBack(t));
				}
				return false;
			}

			// Token: 0x0400124A RID: 4682
			public static UQueryState<T>.DelegateQueryMatcher<TReturnType> s_Instance = new UQueryState<T>.DelegateQueryMatcher<TReturnType>();
		}

		// Token: 0x02000521 RID: 1313
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06002898 RID: 10392 RVA: 0x00093E2C File Offset: 0x0009202C
			internal Enumerator(UQueryState<T> queryState)
			{
				this.iterationList = VisualElementListPool.Get(0);
				UQueryState<T>.s_EnumerationList.matches = this.iterationList;
				UQueryState<T>.s_EnumerationList.Run(queryState.m_Element, queryState.m_Matchers);
				UQueryState<T>.s_EnumerationList.Reset();
				this.currentIndex = -1;
			}

			// Token: 0x17000A30 RID: 2608
			// (get) Token: 0x06002899 RID: 10393 RVA: 0x00093E80 File Offset: 0x00092080
			public T Current
			{
				get
				{
					return (T)((object)this.iterationList[this.currentIndex]);
				}
			}

			// Token: 0x17000A31 RID: 2609
			// (get) Token: 0x0600289A RID: 10394 RVA: 0x00093E98 File Offset: 0x00092098
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600289B RID: 10395 RVA: 0x00093EA8 File Offset: 0x000920A8
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.iterationList.Count;
			}

			// Token: 0x0600289C RID: 10396 RVA: 0x00093ED8 File Offset: 0x000920D8
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x0600289D RID: 10397 RVA: 0x00093EE2 File Offset: 0x000920E2
			public void Dispose()
			{
				VisualElementListPool.Release(this.iterationList);
				this.iterationList = null;
			}

			// Token: 0x0400124B RID: 4683
			private List<VisualElement> iterationList;

			// Token: 0x0400124C RID: 4684
			private int currentIndex;
		}
	}
}
