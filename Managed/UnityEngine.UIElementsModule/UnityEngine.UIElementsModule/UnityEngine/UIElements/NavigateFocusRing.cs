using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BD RID: 701
	internal class NavigateFocusRing : IFocusRing
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00050676 File Offset: 0x0004E876
		private FocusController focusController
		{
			get
			{
				return this.m_Root.focusController;
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00050683 File Offset: 0x0004E883
		public NavigateFocusRing(VisualElement root)
		{
			this.m_Root = root;
			this.m_Ring = new VisualElementFocusRing(root, VisualElementFocusRing.DefaultFocusOrder.ChildOrder);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag)
			{
				Focusable focusable;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.elementTarget, out focusable);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(focusable);
				}
			}
			bool flag2 = e.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
			if (flag2)
			{
				switch (((NavigationMoveEvent)e).direction)
				{
				case NavigationMoveEvent.Direction.Left:
					return NavigateFocusRing.Left;
				case NavigationMoveEvent.Direction.Up:
					return NavigateFocusRing.Up;
				case NavigationMoveEvent.Direction.Right:
					return NavigateFocusRing.Right;
				case NavigationMoveEvent.Direction.Down:
					return NavigateFocusRing.Down;
				case NavigationMoveEvent.Direction.Next:
					return NavigateFocusRing.Next;
				case NavigationMoveEvent.Direction.Previous:
					return NavigateFocusRing.Previous;
				}
			}
			return FocusChangeDirection.none;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0005076C File Offset: 0x0004E96C
		public virtual Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == NavigateFocusRing.Up || direction == NavigateFocusRing.Down || direction == NavigateFocusRing.Right || direction == NavigateFocusRing.Left;
			Focusable focusable;
			if (flag)
			{
				focusable = this.GetNextFocusable2D(currentFocusable, (NavigateFocusRing.ChangeDirection)direction);
			}
			else
			{
				focusable = this.m_Ring.GetNextFocusable(currentFocusable, direction);
			}
			return focusable;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000507C4 File Offset: 0x0004E9C4
		private Focusable GetNextFocusable2D(Focusable currentFocusable, NavigateFocusRing.ChangeDirection direction)
		{
			VisualElement visualElement = currentFocusable as VisualElement;
			bool flag = visualElement == null;
			if (flag)
			{
				visualElement = this.m_Root;
			}
			Rect worldBoundingBox = this.m_Root.worldBoundingBox;
			Rect rect = new Rect(worldBoundingBox.position - Vector2.one, worldBoundingBox.size + Vector2.one * 2f);
			Rect worldBound = visualElement.worldBound;
			Rect rect2 = new Rect(worldBound.position - Vector2.one, worldBound.size + Vector2.one * 2f);
			bool flag2 = direction == NavigateFocusRing.Up;
			if (flag2)
			{
				rect2.yMin = rect.yMin;
			}
			else
			{
				bool flag3 = direction == NavigateFocusRing.Down;
				if (flag3)
				{
					rect2.yMax = rect.yMax;
				}
				else
				{
					bool flag4 = direction == NavigateFocusRing.Left;
					if (flag4)
					{
						rect2.xMin = rect.xMin;
					}
					else
					{
						bool flag5 = direction == NavigateFocusRing.Right;
						if (flag5)
						{
							rect2.xMax = rect.xMax;
						}
					}
				}
			}
			NavigateFocusRing.FocusableHierarchyTraversal focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
			focusableHierarchyTraversal.currentFocusable = visualElement;
			focusableHierarchyTraversal.direction = direction;
			focusableHierarchyTraversal.validRect = rect2;
			focusableHierarchyTraversal.firstPass = true;
			Focusable focusable = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
			bool flag6 = focusable != null;
			Focusable focusable2;
			if (flag6)
			{
				focusable2 = focusable;
			}
			else
			{
				rect2 = new Rect(worldBound.position - Vector2.one, worldBound.size + Vector2.one * 2f);
				bool flag7 = direction == NavigateFocusRing.Down;
				if (flag7)
				{
					rect2.yMin = rect.yMin;
				}
				else
				{
					bool flag8 = direction == NavigateFocusRing.Up;
					if (flag8)
					{
						rect2.yMax = rect.yMax;
					}
					else
					{
						bool flag9 = direction == NavigateFocusRing.Right;
						if (flag9)
						{
							rect2.xMin = rect.xMin;
						}
						else
						{
							bool flag10 = direction == NavigateFocusRing.Left;
							if (flag10)
							{
								rect2.xMax = rect.xMax;
							}
						}
					}
				}
				focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
				focusableHierarchyTraversal.currentFocusable = visualElement;
				focusableHierarchyTraversal.direction = direction;
				focusableHierarchyTraversal.validRect = rect2;
				focusableHierarchyTraversal.firstPass = false;
				focusable = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
				bool flag11 = focusable != null;
				if (flag11)
				{
					focusable2 = focusable;
				}
				else
				{
					focusable2 = currentFocusable;
				}
			}
			return focusable2;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00050A2C File Offset: 0x0004EC2C
		private static bool IsActive(VisualElement v)
		{
			return v.resolvedStyle.display != DisplayStyle.None && v.enabledInHierarchy;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00050A58 File Offset: 0x0004EC58
		private static bool IsNavigable(Focusable focusable)
		{
			return focusable.canGrabFocus && focusable.tabIndex >= 0 && !focusable.delegatesFocus && !focusable.excludeFromFocusRing;
		}

		// Token: 0x04000ADA RID: 2778
		public static readonly NavigateFocusRing.ChangeDirection Left = new NavigateFocusRing.ChangeDirection(1);

		// Token: 0x04000ADB RID: 2779
		public static readonly NavigateFocusRing.ChangeDirection Right = new NavigateFocusRing.ChangeDirection(2);

		// Token: 0x04000ADC RID: 2780
		public static readonly NavigateFocusRing.ChangeDirection Up = new NavigateFocusRing.ChangeDirection(3);

		// Token: 0x04000ADD RID: 2781
		public static readonly NavigateFocusRing.ChangeDirection Down = new NavigateFocusRing.ChangeDirection(4);

		// Token: 0x04000ADE RID: 2782
		public static readonly FocusChangeDirection Next = VisualElementFocusChangeDirection.right;

		// Token: 0x04000ADF RID: 2783
		public static readonly FocusChangeDirection Previous = VisualElementFocusChangeDirection.left;

		// Token: 0x04000AE0 RID: 2784
		private readonly VisualElement m_Root;

		// Token: 0x04000AE1 RID: 2785
		private readonly VisualElementFocusRing m_Ring;

		// Token: 0x020002BE RID: 702
		public class ChangeDirection : FocusChangeDirection
		{
			// Token: 0x0600141B RID: 5147 RVA: 0x00050ADD File Offset: 0x0004ECDD
			public ChangeDirection(int i)
				: base(i)
			{
			}
		}

		// Token: 0x020002BF RID: 703
		private struct FocusableHierarchyTraversal
		{
			// Token: 0x0600141C RID: 5148 RVA: 0x00050AE8 File Offset: 0x0004ECE8
			private bool ValidateHierarchyTraversal(VisualElement v)
			{
				return NavigateFocusRing.IsActive(v) && v.worldBoundingBox.Overlaps(this.validRect);
			}

			// Token: 0x0600141D RID: 5149 RVA: 0x00050B1C File Offset: 0x0004ED1C
			private bool ValidateElement(VisualElement v)
			{
				return NavigateFocusRing.IsNavigable(v) && v.worldBound.Overlaps(this.validRect);
			}

			// Token: 0x0600141E RID: 5150 RVA: 0x00050B50 File Offset: 0x0004ED50
			private int Order(VisualElement a, VisualElement b)
			{
				Rect worldBound = a.worldBound;
				Rect worldBound2 = b.worldBound;
				int num = this.StrictOrder(worldBound, worldBound2);
				return (num != 0) ? num : this.TieBreaker(worldBound, worldBound2);
			}

			// Token: 0x0600141F RID: 5151 RVA: 0x00050B88 File Offset: 0x0004ED88
			private int StrictOrder(VisualElement a, VisualElement b)
			{
				return this.StrictOrder(a.worldBound, b.worldBound);
			}

			// Token: 0x06001420 RID: 5152 RVA: 0x00050BAC File Offset: 0x0004EDAC
			private int StrictOrder(Rect ra, Rect rb)
			{
				float num = 0f;
				bool flag = this.direction == NavigateFocusRing.Up;
				if (flag)
				{
					num = rb.yMax - ra.yMax;
				}
				else
				{
					bool flag2 = this.direction == NavigateFocusRing.Down;
					if (flag2)
					{
						num = ra.yMin - rb.yMin;
					}
					else
					{
						bool flag3 = this.direction == NavigateFocusRing.Left;
						if (flag3)
						{
							num = rb.xMax - ra.xMax;
						}
						else
						{
							bool flag4 = this.direction == NavigateFocusRing.Right;
							if (flag4)
							{
								num = ra.xMin - rb.xMin;
							}
						}
					}
				}
				bool flag5 = !Mathf.Approximately(num, 0f);
				int num2;
				if (flag5)
				{
					num2 = ((num > 0f) ? 1 : (-1));
				}
				else
				{
					num2 = 0;
				}
				return num2;
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x00050C78 File Offset: 0x0004EE78
			private int TieBreaker(Rect ra, Rect rb)
			{
				Rect worldBound = this.currentFocusable.worldBound;
				float num = (ra.min - worldBound.min).sqrMagnitude - (rb.min - worldBound.min).sqrMagnitude;
				bool flag = !Mathf.Approximately(num, 0f);
				int num2;
				if (flag)
				{
					num2 = ((num > 0f) ? 1 : (-1));
				}
				else
				{
					num2 = 0;
				}
				return num2;
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x00050CF4 File Offset: 0x0004EEF4
			public VisualElement GetBestOverall(VisualElement candidate, VisualElement bestSoFar = null)
			{
				bool flag = !this.ValidateHierarchyTraversal(candidate);
				VisualElement visualElement;
				if (flag)
				{
					visualElement = bestSoFar;
				}
				else
				{
					bool flag2 = this.ValidateElement(candidate);
					if (flag2)
					{
						bool flag3 = (!this.firstPass || this.StrictOrder(candidate, this.currentFocusable) > 0) && (bestSoFar == null || this.Order(bestSoFar, candidate) > 0);
						if (flag3)
						{
							bestSoFar = candidate;
						}
						visualElement = bestSoFar;
					}
					else
					{
						int childCount = candidate.hierarchy.childCount;
						for (int i = 0; i < childCount; i++)
						{
							VisualElement visualElement2 = candidate.hierarchy[i];
							bestSoFar = this.GetBestOverall(visualElement2, bestSoFar);
						}
						visualElement = bestSoFar;
					}
				}
				return visualElement;
			}

			// Token: 0x04000AE2 RID: 2786
			public VisualElement currentFocusable;

			// Token: 0x04000AE3 RID: 2787
			public Rect validRect;

			// Token: 0x04000AE4 RID: 2788
			public bool firstPass;

			// Token: 0x04000AE5 RID: 2789
			public NavigateFocusRing.ChangeDirection direction;
		}
	}
}
