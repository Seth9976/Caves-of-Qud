using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005C4 RID: 1476
	public class VisualElementFocusRing : IFocusRing
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x0009D3E9 File Offset: 0x0009B5E9
		public VisualElementFocusRing(VisualElement root, VisualElementFocusRing.DefaultFocusOrder dfo = VisualElementFocusRing.DefaultFocusOrder.ChildOrder)
		{
			this.defaultFocusOrder = dfo;
			this.root = root;
			this.m_FocusRing = new List<VisualElementFocusRing.FocusRingRecord>();
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x0009D40D File Offset: 0x0009B60D
		private FocusController focusController
		{
			get
			{
				return this.root.focusController;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x0009D41A File Offset: 0x0009B61A
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x0009D422 File Offset: 0x0009B622
		public VisualElementFocusRing.DefaultFocusOrder defaultFocusOrder { get; set; }

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0009D42C File Offset: 0x0009B62C
		private int FocusRingAutoIndexSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			int num;
			switch (this.defaultFocusOrder)
			{
			default:
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			case VisualElementFocusRing.DefaultFocusOrder.PositionXY:
			{
				VisualElement visualElement = a.m_Focusable as VisualElement;
				VisualElement visualElement2 = b.m_Focusable as VisualElement;
				bool flag = visualElement != null && visualElement2 != null;
				if (flag)
				{
					bool flag2 = visualElement.layout.position.x < visualElement2.layout.position.x;
					if (flag2)
					{
						num = -1;
						break;
					}
					bool flag3 = visualElement.layout.position.x > visualElement2.layout.position.x;
					if (flag3)
					{
						num = 1;
						break;
					}
					bool flag4 = visualElement.layout.position.y < visualElement2.layout.position.y;
					if (flag4)
					{
						num = -1;
						break;
					}
					bool flag5 = visualElement.layout.position.y > visualElement2.layout.position.y;
					if (flag5)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionYX:
			{
				VisualElement visualElement3 = a.m_Focusable as VisualElement;
				VisualElement visualElement4 = b.m_Focusable as VisualElement;
				bool flag6 = visualElement3 != null && visualElement4 != null;
				if (flag6)
				{
					bool flag7 = visualElement3.layout.position.y < visualElement4.layout.position.y;
					if (flag7)
					{
						num = -1;
						break;
					}
					bool flag8 = visualElement3.layout.position.y > visualElement4.layout.position.y;
					if (flag8)
					{
						num = 1;
						break;
					}
					bool flag9 = visualElement3.layout.position.x < visualElement4.layout.position.x;
					if (flag9)
					{
						num = -1;
						break;
					}
					bool flag10 = visualElement3.layout.position.x > visualElement4.layout.position.x;
					if (flag10)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			}
			return num;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0009D6D8 File Offset: 0x0009B8D8
		private int FocusRingSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			bool flag = a.m_Focusable.tabIndex == 0 && b.m_Focusable.tabIndex == 0;
			int num;
			if (flag)
			{
				num = this.FocusRingAutoIndexSort(a, b);
			}
			else
			{
				bool flag2 = a.m_Focusable.tabIndex == 0;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					bool flag3 = b.m_Focusable.tabIndex == 0;
					if (flag3)
					{
						num = -1;
					}
					else
					{
						int num2 = Comparer<int>.Default.Compare(a.m_Focusable.tabIndex, b.m_Focusable.tabIndex);
						bool flag4 = num2 == 0;
						if (flag4)
						{
							num2 = this.FocusRingAutoIndexSort(a, b);
						}
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0009D784 File Offset: 0x0009B984
		private void DoUpdate()
		{
			this.m_FocusRing.Clear();
			bool flag = this.root != null;
			if (flag)
			{
				List<VisualElementFocusRing.FocusRingRecord> list = new List<VisualElementFocusRing.FocusRingRecord>();
				int num = 0;
				this.BuildRingForScopeRecursive(this.root, ref num, list);
				this.SortAndFlattenScopeLists(list);
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0009D7D0 File Offset: 0x0009B9D0
		private void BuildRingForScopeRecursive(VisualElement ve, ref int scopeIndex, List<VisualElementFocusRing.FocusRingRecord> scopeList)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.parent != null && visualElement == visualElement.parent.contentContainer;
				bool flag2 = visualElement.isCompositeRoot || flag;
				if (flag2)
				{
					VisualElementFocusRing.FocusRingRecord focusRingRecord = new VisualElementFocusRing.FocusRingRecord();
					int num = scopeIndex;
					scopeIndex = num + 1;
					focusRingRecord.m_AutoIndex = num;
					focusRingRecord.m_Focusable = visualElement;
					focusRingRecord.m_IsSlot = flag;
					focusRingRecord.m_ScopeNavigationOrder = new List<VisualElementFocusRing.FocusRingRecord>();
					VisualElementFocusRing.FocusRingRecord focusRingRecord2 = focusRingRecord;
					scopeList.Add(focusRingRecord2);
					int num2 = 0;
					this.BuildRingForScopeRecursive(visualElement, ref num2, focusRingRecord2.m_ScopeNavigationOrder);
				}
				else
				{
					bool flag3 = visualElement.canGrabFocus && visualElement.areAncestorsAndSelfDisplayed && visualElement.tabIndex >= 0;
					if (flag3)
					{
						VisualElementFocusRing.FocusRingRecord focusRingRecord3 = new VisualElementFocusRing.FocusRingRecord();
						int num = scopeIndex;
						scopeIndex = num + 1;
						focusRingRecord3.m_AutoIndex = num;
						focusRingRecord3.m_Focusable = visualElement;
						focusRingRecord3.m_IsSlot = false;
						focusRingRecord3.m_ScopeNavigationOrder = null;
						scopeList.Add(focusRingRecord3);
					}
					this.BuildRingForScopeRecursive(visualElement, ref scopeIndex, scopeList);
				}
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0009D8FC File Offset: 0x0009BAFC
		private void SortAndFlattenScopeLists(List<VisualElementFocusRing.FocusRingRecord> rootScopeList)
		{
			bool flag = rootScopeList != null;
			if (flag)
			{
				rootScopeList.Sort(new Comparison<VisualElementFocusRing.FocusRingRecord>(this.FocusRingSort));
				foreach (VisualElementFocusRing.FocusRingRecord focusRingRecord in rootScopeList)
				{
					bool flag2 = focusRingRecord.m_Focusable.canGrabFocus && focusRingRecord.m_Focusable.tabIndex >= 0;
					if (flag2)
					{
						bool flag3 = !focusRingRecord.m_Focusable.excludeFromFocusRing;
						if (flag3)
						{
							this.m_FocusRing.Add(focusRingRecord);
						}
						this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
					}
					else
					{
						bool isSlot = focusRingRecord.m_IsSlot;
						if (isSlot)
						{
							this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
						}
					}
					focusRingRecord.m_ScopeNavigationOrder = null;
				}
			}
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0009D9EC File Offset: 0x0009BBEC
		private int GetFocusableInternalIndex(Focusable f)
		{
			bool flag = f != null;
			if (flag)
			{
				for (int i = 0; i < this.m_FocusRing.Count; i++)
				{
					bool flag2 = f == this.m_FocusRing[i].m_Focusable;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0009DA44 File Offset: 0x0009BC44
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag2)
			{
				Focusable focusable;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.elementTarget, out focusable);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(focusable);
				}
			}
			bool flag3 = currentFocusable != null && currentFocusable.isIMGUIContainer;
			FocusChangeDirection focusChangeDirection;
			if (flag3)
			{
				focusChangeDirection = FocusChangeDirection.none;
			}
			else
			{
				bool flag4 = e.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
				if (flag4)
				{
					NavigationMoveEvent.Direction direction = ((NavigationMoveEvent)e).direction;
					focusChangeDirection = ((direction == NavigationMoveEvent.Direction.Next) ? VisualElementFocusChangeDirection.right : ((direction == NavigationMoveEvent.Direction.Previous) ? VisualElementFocusChangeDirection.left : FocusChangeDirection.none));
				}
				else
				{
					focusChangeDirection = FocusChangeDirection.none;
				}
			}
			return focusChangeDirection;
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0009DB04 File Offset: 0x0009BD04
		public Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == FocusChangeDirection.none || direction == FocusChangeDirection.unspecified;
			Focusable focusable;
			if (flag)
			{
				focusable = currentFocusable;
			}
			else
			{
				VisualElementFocusChangeTarget visualElementFocusChangeTarget = direction as VisualElementFocusChangeTarget;
				bool flag2 = visualElementFocusChangeTarget != null;
				if (flag2)
				{
					focusable = visualElementFocusChangeTarget.target;
				}
				else
				{
					this.DoUpdate();
					bool flag3 = this.m_FocusRing.Count == 0;
					if (flag3)
					{
						focusable = null;
					}
					else
					{
						int focusableInternalIndex = this.GetFocusableInternalIndex(currentFocusable);
						bool flag4 = currentFocusable != null && focusableInternalIndex == -1;
						if (flag4)
						{
							bool flag5 = direction == VisualElementFocusChangeDirection.right;
							if (flag5)
							{
								return VisualElementFocusRing.GetNextFocusableInTree(currentFocusable as VisualElement);
							}
							bool flag6 = direction == VisualElementFocusChangeDirection.left;
							if (flag6)
							{
								return VisualElementFocusRing.GetPreviousFocusableInTree(currentFocusable as VisualElement);
							}
						}
						int num = 0;
						bool flag7 = direction == VisualElementFocusChangeDirection.right;
						if (flag7)
						{
							num = focusableInternalIndex + 1;
							bool flag8 = num == this.m_FocusRing.Count;
							if (flag8)
							{
								num = 0;
							}
							while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
							{
								num++;
								bool flag9 = num == this.m_FocusRing.Count;
								if (flag9)
								{
									return null;
								}
							}
						}
						else
						{
							bool flag10 = direction == VisualElementFocusChangeDirection.left;
							if (flag10)
							{
								num = focusableInternalIndex - 1;
								bool flag11 = num < 0;
								if (flag11)
								{
									num = this.m_FocusRing.Count - 1;
								}
								while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
								{
									num--;
									bool flag12 = num == -1;
									if (flag12)
									{
										return null;
									}
								}
							}
						}
						focusable = this.m_FocusRing[num].m_Focusable;
					}
				}
			}
			return focusable;
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0009DCBC File Offset: 0x0009BEBC
		internal static Focusable GetNextFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetNextElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetNextElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = visualElement;
			}
			return focusable;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0009DD30 File Offset: 0x0009BF30
		internal static Focusable GetPreviousFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetPreviousElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetPreviousElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
						while (visualElement.childCount > 0)
						{
							visualElement = visualElement.hierarchy.ElementAt(visualElement.childCount - 1);
						}
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = visualElement;
			}
			return focusable;
		}

		// Token: 0x040013DE RID: 5086
		private readonly VisualElement root;

		// Token: 0x040013E0 RID: 5088
		private List<VisualElementFocusRing.FocusRingRecord> m_FocusRing;

		// Token: 0x020005C5 RID: 1477
		public enum DefaultFocusOrder
		{
			// Token: 0x040013E2 RID: 5090
			ChildOrder,
			// Token: 0x040013E3 RID: 5091
			PositionXY,
			// Token: 0x040013E4 RID: 5092
			PositionYX
		}

		// Token: 0x020005C6 RID: 1478
		private class FocusRingRecord
		{
			// Token: 0x040013E5 RID: 5093
			public int m_AutoIndex;

			// Token: 0x040013E6 RID: 5094
			public Focusable m_Focusable;

			// Token: 0x040013E7 RID: 5095
			public bool m_IsSlot;

			// Token: 0x040013E8 RID: 5096
			public List<VisualElementFocusRing.FocusRingRecord> m_ScopeNavigationOrder;
		}
	}
}
