using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000522 RID: 1314
	public struct UQueryBuilder<T> : IEquatable<UQueryBuilder<T>> where T : VisualElement
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x00093EF8 File Offset: 0x000920F8
		private List<StyleSelector> styleSelectors
		{
			get
			{
				List<StyleSelector> list;
				if ((list = this.m_StyleSelectors) == null)
				{
					list = (this.m_StyleSelectors = new List<StyleSelector>());
				}
				return list;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x00093F24 File Offset: 0x00092124
		private List<StyleSelectorPart> parts
		{
			get
			{
				List<StyleSelectorPart> list;
				if ((list = this.m_Parts) == null)
				{
					list = (this.m_Parts = new List<StyleSelectorPart>());
				}
				return list;
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00093F50 File Offset: 0x00092150
		public UQueryBuilder(VisualElement visualElement)
		{
			this = default(UQueryBuilder<T>);
			this.m_Element = visualElement;
			this.m_Parts = null;
			this.m_StyleSelectors = null;
			this.m_Relationship = StyleSelectorRelationship.None;
			this.m_Matchers = new List<RuleMatcher>();
			this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x00093F9C File Offset: 0x0009219C
		public UQueryBuilder<T> Class(string classname)
		{
			this.AddClass(classname);
			return this;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x00093FBC File Offset: 0x000921BC
		public UQueryBuilder<T> Name(string id)
		{
			this.AddName(id);
			return this;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x00093FDC File Offset: 0x000921DC
		public UQueryBuilder<T2> Descendents<T2>(string name = null, params string[] classNames) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classNames);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x00094014 File Offset: 0x00092214
		public UQueryBuilder<T2> Descendents<T2>(string name = null, string classname = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(classname);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0009404C File Offset: 0x0009224C
		public UQueryBuilder<T2> Children<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00094084 File Offset: 0x00092284
		public UQueryBuilder<T2> Children<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000940BC File Offset: 0x000922BC
		public UQueryBuilder<T2> OfType<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000940EC File Offset: 0x000922EC
		public UQueryBuilder<T2> OfType<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0009411C File Offset: 0x0009231C
		internal UQueryBuilder<T> SingleBaseType()
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance));
			return this;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x0009414C File Offset: 0x0009234C
		public UQueryBuilder<T> Where(Func<T, bool> selectorPredicate)
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(new UQuery.PredicateWrapper<T>(selectorPredicate)));
			return this;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0009417C File Offset: 0x0009237C
		private void AddClass(string c)
		{
			bool flag = c != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateClass(c));
			}
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000941A4 File Offset: 0x000923A4
		private void AddClasses(params string[] classes)
		{
			bool flag = classes != null;
			if (flag)
			{
				for (int i = 0; i < classes.Length; i++)
				{
					this.AddClass(classes[i]);
				}
			}
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000941D8 File Offset: 0x000923D8
		private void AddName(string id)
		{
			bool flag = id != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateId(id));
			}
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00094200 File Offset: 0x00092400
		private void AddType<T2>() where T2 : VisualElement
		{
			bool flag = typeof(T2) != typeof(VisualElement);
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T2>.s_Instance));
			}
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00094244 File Offset: 0x00092444
		private UQueryBuilder<T> AddPseudoState(PseudoStates s)
		{
			this.pseudoStatesMask |= (int)s;
			return this;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0009426C File Offset: 0x0009246C
		private UQueryBuilder<T> AddNegativePseudoState(PseudoStates s)
		{
			this.negatedPseudoStatesMask |= (int)s;
			return this;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00094294 File Offset: 0x00092494
		public UQueryBuilder<T> Active()
		{
			return this.AddPseudoState(PseudoStates.Active);
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000942B0 File Offset: 0x000924B0
		public UQueryBuilder<T> NotActive()
		{
			return this.AddNegativePseudoState(PseudoStates.Active);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000942CC File Offset: 0x000924CC
		public UQueryBuilder<T> Visible()
		{
			return this.Where((T e) => e.visible);
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x00094304 File Offset: 0x00092504
		public UQueryBuilder<T> NotVisible()
		{
			return this.Where((T e) => !e.visible);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0009433C File Offset: 0x0009253C
		public UQueryBuilder<T> Hovered()
		{
			return this.AddPseudoState(PseudoStates.Hover);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00094358 File Offset: 0x00092558
		public UQueryBuilder<T> NotHovered()
		{
			return this.AddNegativePseudoState(PseudoStates.Hover);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00094374 File Offset: 0x00092574
		public UQueryBuilder<T> Checked()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00094390 File Offset: 0x00092590
		public UQueryBuilder<T> NotChecked()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000943AC File Offset: 0x000925AC
		[Obsolete("Use Checked() instead")]
		public UQueryBuilder<T> Selected()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000943C8 File Offset: 0x000925C8
		[Obsolete("Use NotChecked() instead")]
		public UQueryBuilder<T> NotSelected()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000943E4 File Offset: 0x000925E4
		public UQueryBuilder<T> Enabled()
		{
			return this.AddNegativePseudoState(PseudoStates.Disabled);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00094400 File Offset: 0x00092600
		public UQueryBuilder<T> NotEnabled()
		{
			return this.AddPseudoState(PseudoStates.Disabled);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0009441C File Offset: 0x0009261C
		public UQueryBuilder<T> Focused()
		{
			return this.AddPseudoState(PseudoStates.Focus);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00094438 File Offset: 0x00092638
		public UQueryBuilder<T> NotFocused()
		{
			return this.AddNegativePseudoState(PseudoStates.Focus);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x00094454 File Offset: 0x00092654
		private UQueryBuilder<T2> AddRelationship<T2>(StyleSelectorRelationship relationship) where T2 : VisualElement
		{
			return new UQueryBuilder<T2>(this.m_Element)
			{
				m_Matchers = this.m_Matchers,
				m_Parts = this.m_Parts,
				m_StyleSelectors = this.m_StyleSelectors,
				m_Relationship = ((relationship == StyleSelectorRelationship.None) ? this.m_Relationship : relationship),
				pseudoStatesMask = this.pseudoStatesMask,
				negatedPseudoStatesMask = this.negatedPseudoStatesMask
			};
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000944C8 File Offset: 0x000926C8
		private void AddPseudoStatesRuleIfNecessasy()
		{
			bool flag = this.pseudoStatesMask != 0 || this.negatedPseudoStatesMask != 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.PseudoClass
				});
			}
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x00094510 File Offset: 0x00092710
		private void FinishSelector()
		{
			this.FinishCurrentSelector();
			bool flag = this.styleSelectors.Count > 0;
			if (flag)
			{
				StyleComplexSelector styleComplexSelector = new StyleComplexSelector();
				styleComplexSelector.selectors = this.styleSelectors.ToArray();
				this.styleSelectors.Clear();
				this.m_Matchers.Add(new RuleMatcher
				{
					complexSelector = styleComplexSelector
				});
			}
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0009457C File Offset: 0x0009277C
		private bool CurrentSelectorEmpty()
		{
			return this.parts.Count == 0 && this.m_Relationship == StyleSelectorRelationship.None && this.pseudoStatesMask == 0 && this.negatedPseudoStatesMask == 0;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000945B8 File Offset: 0x000927B8
		private void FinishCurrentSelector()
		{
			bool flag = !this.CurrentSelectorEmpty();
			if (flag)
			{
				StyleSelector styleSelector = new StyleSelector();
				styleSelector.previousRelationship = this.m_Relationship;
				this.AddPseudoStatesRuleIfNecessasy();
				styleSelector.parts = this.m_Parts.ToArray();
				styleSelector.pseudoStateMask = this.pseudoStatesMask;
				styleSelector.negatedPseudoStateMask = this.negatedPseudoStatesMask;
				this.styleSelectors.Add(styleSelector);
				this.m_Parts.Clear();
				this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x00094644 File Offset: 0x00092844
		public UQueryState<T> Build()
		{
			this.FinishSelector();
			bool flag = this.m_Matchers.Count == 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.Wildcard
				});
				this.FinishSelector();
			}
			return new UQueryState<T>(this.m_Element, this.m_Matchers);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000946A8 File Offset: 0x000928A8
		public static implicit operator T(UQueryBuilder<T> s)
		{
			return s.First();
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000946C4 File Offset: 0x000928C4
		public static bool operator ==(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return builder1.Equals(builder2);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000946E0 File Offset: 0x000928E0
		public static bool operator !=(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return !(builder1 == builder2);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000946FC File Offset: 0x000928FC
		public T First()
		{
			return this.Build().First();
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0009471C File Offset: 0x0009291C
		public T Last()
		{
			return this.Build().Last();
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0009473C File Offset: 0x0009293C
		public List<T> ToList()
		{
			return this.Build().ToList();
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0009475C File Offset: 0x0009295C
		public void ToList(List<T> results)
		{
			this.Build().ToList(results);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0009477C File Offset: 0x0009297C
		public T AtIndex(int index)
		{
			return this.Build().AtIndex(index);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000947A0 File Offset: 0x000929A0
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			this.Build().ForEach<T2>(result, funcCall);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000947C0 File Offset: 0x000929C0
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			return this.Build().ForEach<T2>(funcCall);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000947E4 File Offset: 0x000929E4
		public void ForEach(Action<T> funcCall)
		{
			this.Build().ForEach(funcCall);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x00094804 File Offset: 0x00092A04
		public bool Equals(UQueryBuilder<T> other)
		{
			return EqualityComparer<List<StyleSelector>>.Default.Equals(this.m_StyleSelectors, other.m_StyleSelectors) && EqualityComparer<List<StyleSelector>>.Default.Equals(this.styleSelectors, other.styleSelectors) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.m_Parts, other.m_Parts) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.parts, other.parts) && this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers) && this.m_Relationship == other.m_Relationship && this.pseudoStatesMask == other.pseudoStatesMask && this.negatedPseudoStatesMask == other.negatedPseudoStatesMask;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000948D4 File Offset: 0x00092AD4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryBuilder<T>);
			return !flag && this.Equals((UQueryBuilder<T>)obj);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00094908 File Offset: 0x00092B08
		public override int GetHashCode()
		{
			int num = -949812380;
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.m_StyleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.styleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.m_Parts);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.parts);
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			num = num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
			num = num * -1521134295 + this.m_Relationship.GetHashCode();
			num = num * -1521134295 + this.pseudoStatesMask.GetHashCode();
			return num * -1521134295 + this.negatedPseudoStatesMask.GetHashCode();
		}

		// Token: 0x0400124D RID: 4685
		private List<StyleSelector> m_StyleSelectors;

		// Token: 0x0400124E RID: 4686
		private List<StyleSelectorPart> m_Parts;

		// Token: 0x0400124F RID: 4687
		private VisualElement m_Element;

		// Token: 0x04001250 RID: 4688
		private List<RuleMatcher> m_Matchers;

		// Token: 0x04001251 RID: 4689
		private StyleSelectorRelationship m_Relationship;

		// Token: 0x04001252 RID: 4690
		private int pseudoStatesMask;

		// Token: 0x04001253 RID: 4691
		private int negatedPseudoStatesMask;
	}
}
