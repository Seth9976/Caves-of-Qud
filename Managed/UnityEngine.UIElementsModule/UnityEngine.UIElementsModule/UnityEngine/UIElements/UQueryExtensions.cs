using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000524 RID: 1316
	public static class UQueryExtensions
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x00094A24 File Offset: 0x00092C24
		public static T Q<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			return e.Query<T>(name, classes).Build().First();
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x00094A50 File Offset: 0x00092C50
		public static VisualElement Q(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query<VisualElement>(name, classes).Build().First();
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x00094A7C File Offset: 0x00092C7C
		public static T Q<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = typeof(T) == typeof(VisualElement);
			T t;
			if (flag2)
			{
				t = e.Q(name, className) as T;
			}
			else
			{
				bool flag3 = name == null;
				if (flag3)
				{
					bool flag4 = className == null;
					if (flag4)
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						t = uqueryState.First() as T;
					}
					else
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndClassQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
						t = uqueryState.First() as T;
					}
				}
				else
				{
					bool flag5 = className == null;
					if (flag5)
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndNameQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						t = uqueryState.First() as T;
					}
					else
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndNameAndClassQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[2] = StyleSelectorPart.CreateClass(className);
						t = uqueryState.First() as T;
					}
				}
			}
			return t;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00094CDC File Offset: 0x00092EDC
		internal static T MandatoryQ<T>(this VisualElement e, string name, string className = null) where T : VisualElement
		{
			T t = e.Q<T>(name, className);
			bool flag = t == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return t;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00094D18 File Offset: 0x00092F18
		public static VisualElement Q(this VisualElement e, string name = null, string className = null)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = name == null;
			VisualElement visualElement;
			if (flag2)
			{
				bool flag3 = className == null;
				if (flag3)
				{
					visualElement = UQueryExtensions.SingleElementEmptyQuery.RebuildOn(e).First();
				}
				else
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementClassQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateClass(className);
					visualElement = uqueryState.First();
				}
			}
			else
			{
				bool flag4 = className == null;
				if (flag4)
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementNameQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					visualElement = uqueryState.First();
				}
				else
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementNameAndClassQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
					visualElement = uqueryState.First();
				}
			}
			return visualElement;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x00094E60 File Offset: 0x00093060
		internal static VisualElement MandatoryQ(this VisualElement e, string name, string className = null)
		{
			VisualElement visualElement = e.Q<VisualElement>(name, className);
			bool flag = visualElement == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return visualElement;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00094E98 File Offset: 0x00093098
		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query<VisualElement>(name, classes);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00094EB4 File Offset: 0x000930B4
		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, string className = null)
		{
			return e.Query<VisualElement>(name, className);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00094ED0 File Offset: 0x000930D0
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, classes);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x00094F08 File Offset: 0x00093108
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, className);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00094F40 File Offset: 0x00093140
		public static UQueryBuilder<VisualElement> Query(this VisualElement e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e);
		}

		// Token: 0x04001257 RID: 4695
		private static UQueryState<VisualElement> SingleElementEmptyQuery = new UQueryBuilder<VisualElement>(null).Build();

		// Token: 0x04001258 RID: 4696
		private static UQueryState<VisualElement> SingleElementNameQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Build();

		// Token: 0x04001259 RID: 4697
		private static UQueryState<VisualElement> SingleElementClassQuery = new UQueryBuilder<VisualElement>(null).Class(string.Empty).Build();

		// Token: 0x0400125A RID: 4698
		private static UQueryState<VisualElement> SingleElementNameAndClassQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Class(string.Empty).Build();

		// Token: 0x0400125B RID: 4699
		private static UQueryState<VisualElement> SingleElementTypeQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Build();

		// Token: 0x0400125C RID: 4700
		private static UQueryState<VisualElement> SingleElementTypeAndNameQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Build();

		// Token: 0x0400125D RID: 4701
		private static UQueryState<VisualElement> SingleElementTypeAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Class(string.Empty).Build();

		// Token: 0x0400125E RID: 4702
		private static UQueryState<VisualElement> SingleElementTypeAndNameAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Class(string.Empty)
			.Build();

		// Token: 0x02000525 RID: 1317
		private class MissingVisualElementException : Exception
		{
			// Token: 0x060028E3 RID: 10467 RVA: 0x00095099 File Offset: 0x00093299
			public MissingVisualElementException()
			{
			}

			// Token: 0x060028E4 RID: 10468 RVA: 0x000950A3 File Offset: 0x000932A3
			public MissingVisualElementException(string message)
				: base(message)
			{
			}
		}
	}
}
