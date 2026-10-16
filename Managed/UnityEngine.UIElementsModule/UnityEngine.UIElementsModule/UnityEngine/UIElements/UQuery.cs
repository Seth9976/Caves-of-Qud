using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000513 RID: 1299
	public static class UQuery
	{
		// Token: 0x02000514 RID: 1300
		internal interface IVisualPredicateWrapper
		{
			// Token: 0x06002850 RID: 10320
			bool Predicate(object e);
		}

		// Token: 0x02000515 RID: 1301
		internal class IsOfType<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x06002851 RID: 10321 RVA: 0x000936D8 File Offset: 0x000918D8
			public bool Predicate(object e)
			{
				return e is T;
			}

			// Token: 0x04001236 RID: 4662
			public static UQuery.IsOfType<T> s_Instance = new UQuery.IsOfType<T>();
		}

		// Token: 0x02000516 RID: 1302
		internal class PredicateWrapper<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x06002854 RID: 10324 RVA: 0x000936FF File Offset: 0x000918FF
			public PredicateWrapper(Func<T, bool> p)
			{
				this.predicate = p;
			}

			// Token: 0x06002855 RID: 10325 RVA: 0x00093710 File Offset: 0x00091910
			public bool Predicate(object e)
			{
				T t = e as T;
				bool flag = t != null;
				return flag && this.predicate(t);
			}

			// Token: 0x04001237 RID: 4663
			private Func<T, bool> predicate;
		}

		// Token: 0x02000517 RID: 1303
		internal abstract class UQueryMatcher : HierarchyTraversal
		{
			// Token: 0x06002857 RID: 10327 RVA: 0x00093756 File Offset: 0x00091956
			public override void Traverse(VisualElement element)
			{
				base.Traverse(element);
			}

			// Token: 0x06002858 RID: 10328 RVA: 0x00093764 File Offset: 0x00091964
			protected virtual bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				return false;
			}

			// Token: 0x06002859 RID: 10329 RVA: 0x00002152 File Offset: 0x00000352
			private static void NoProcessResult(VisualElement e, MatchResultInfo i)
			{
			}

			// Token: 0x0600285A RID: 10330 RVA: 0x00093778 File Offset: 0x00091978
			public override void TraverseRecursive(VisualElement element, int depth)
			{
				int count = this.m_Matchers.Count;
				int count2 = this.m_Matchers.Count;
				for (int j = 0; j < count2; j++)
				{
					RuleMatcher ruleMatcher = this.m_Matchers[j];
					bool flag = StyleSelectorHelper.MatchRightToLeft(element, ruleMatcher.complexSelector, delegate(VisualElement e, MatchResultInfo i)
					{
						UQuery.UQueryMatcher.NoProcessResult(e, i);
					});
					if (flag)
					{
						bool flag2 = this.OnRuleMatchedElement(ruleMatcher, element);
						if (flag2)
						{
							return;
						}
					}
				}
				base.Recurse(element, depth);
				bool flag3 = this.m_Matchers.Count > count;
				if (flag3)
				{
					this.m_Matchers.RemoveRange(count, this.m_Matchers.Count - count);
					return;
				}
			}

			// Token: 0x0600285B RID: 10331 RVA: 0x0009383C File Offset: 0x00091A3C
			public virtual void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.m_Matchers = matchers;
				this.Traverse(root);
			}

			// Token: 0x04001238 RID: 4664
			internal List<RuleMatcher> m_Matchers;
		}

		// Token: 0x02000519 RID: 1305
		internal abstract class SingleQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000A2A RID: 2602
			// (get) Token: 0x0600285F RID: 10335 RVA: 0x00093864 File Offset: 0x00091A64
			// (set) Token: 0x06002860 RID: 10336 RVA: 0x0009386C File Offset: 0x00091A6C
			public VisualElement match { get; set; }

			// Token: 0x06002861 RID: 10337 RVA: 0x00093875 File Offset: 0x00091A75
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.match = null;
				base.Run(root, matchers);
				this.m_Matchers = null;
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x00093890 File Offset: 0x00091A90
			public bool IsInUse()
			{
				return this.m_Matchers != null;
			}

			// Token: 0x06002863 RID: 10339
			public abstract UQuery.SingleQueryMatcher CreateNew();
		}

		// Token: 0x0200051A RID: 1306
		internal class FirstQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x06002865 RID: 10341 RVA: 0x000938B4 File Offset: 0x00091AB4
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				bool flag = base.match == null;
				if (flag)
				{
					base.match = element;
				}
				return true;
			}

			// Token: 0x06002866 RID: 10342 RVA: 0x000938DC File Offset: 0x00091ADC
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.FirstQueryMatcher();
			}

			// Token: 0x0400123C RID: 4668
			public static readonly UQuery.FirstQueryMatcher Instance = new UQuery.FirstQueryMatcher();
		}

		// Token: 0x0200051B RID: 1307
		internal class LastQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x06002869 RID: 10345 RVA: 0x000938F8 File Offset: 0x00091AF8
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				base.match = element;
				return false;
			}

			// Token: 0x0600286A RID: 10346 RVA: 0x00093913 File Offset: 0x00091B13
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.LastQueryMatcher();
			}

			// Token: 0x0400123D RID: 4669
			public static readonly UQuery.LastQueryMatcher Instance = new UQuery.LastQueryMatcher();
		}

		// Token: 0x0200051C RID: 1308
		internal class IndexQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x17000A2B RID: 2603
			// (get) Token: 0x0600286D RID: 10349 RVA: 0x00093928 File Offset: 0x00091B28
			// (set) Token: 0x0600286E RID: 10350 RVA: 0x00093940 File Offset: 0x00091B40
			public int matchIndex
			{
				get
				{
					return this._matchIndex;
				}
				set
				{
					this.matchCount = -1;
					this._matchIndex = value;
				}
			}

			// Token: 0x0600286F RID: 10351 RVA: 0x00093951 File Offset: 0x00091B51
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.matchCount = -1;
				base.Run(root, matchers);
			}

			// Token: 0x06002870 RID: 10352 RVA: 0x00093964 File Offset: 0x00091B64
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matchCount++;
				bool flag = this.matchCount == this._matchIndex;
				if (flag)
				{
					base.match = element;
				}
				return this.matchCount >= this._matchIndex;
			}

			// Token: 0x06002871 RID: 10353 RVA: 0x000939B1 File Offset: 0x00091BB1
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.IndexQueryMatcher();
			}

			// Token: 0x0400123E RID: 4670
			public static readonly UQuery.IndexQueryMatcher Instance = new UQuery.IndexQueryMatcher();

			// Token: 0x0400123F RID: 4671
			private int matchCount = -1;

			// Token: 0x04001240 RID: 4672
			private int _matchIndex;
		}
	}
}
