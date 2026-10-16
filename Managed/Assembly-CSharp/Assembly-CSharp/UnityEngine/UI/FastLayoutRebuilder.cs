using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x020003EB RID: 1003
	public class FastLayoutRebuilder : ICanvasElement
	{
		// Token: 0x0600257F RID: 9599 RVA: 0x000BF1AC File Offset: 0x000BD3AC
		public static LayoutGroup[] fetchLayoutGroup(GameObject go)
		{
			if (go == null)
			{
				return FastLayoutRebuilder.emptySet;
			}
			LayoutGroup[] array;
			if (FastLayoutRebuilder.objectLayoutGroups.TryGetValue(go, out array))
			{
				return array;
			}
			array = go.GetComponents<LayoutGroup>();
			if (array == null || array.Length == 0)
			{
				FastLayoutRebuilder.objectLayoutGroups.Add(go, FastLayoutRebuilder.emptySet);
			}
			else
			{
				FastLayoutRebuilder.objectLayoutGroups.Add(go, array);
			}
			return array;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000BF208 File Offset: 0x000BD408
		public static void RefreshLayoutGroupsImmediateAndRecursiveCached(GameObject root)
		{
			LayoutGroup[] array = FastLayoutRebuilder.fetchLayoutGroup(root);
			for (int i = 0; i < array.Length; i++)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(array[i].transform as RectTransform);
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000BF23C File Offset: 0x000BD43C
		private void Initialize(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = controller.GetHashCode();
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000BF251 File Offset: 0x000BD451
		private void Clear()
		{
			this.m_ToRebuild = null;
			this.m_CachedHashFromTransform = 0;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000BF264 File Offset: 0x000BD464
		static FastLayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += FastLayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000BF2CB File Offset: 0x000BD4CB
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			FastLayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x000BF2D3 File Offset: 0x000BD4D3
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000BF2DB File Offset: 0x000BD4DB
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000BF2E9 File Offset: 0x000BD4E9
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && !((Behaviour)e).isActiveAndEnabled);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000BF314 File Offset: 0x000BD514
		public static void ForceRebuildLayoutImmediate(RectTransform layoutRoot)
		{
			FastLayoutRebuilder fastLayoutRebuilder = FastLayoutRebuilder.s_Rebuilders.Get();
			fastLayoutRebuilder.Initialize(layoutRoot);
			fastLayoutRebuilder.Rebuild(CanvasUpdate.Layout);
			FastLayoutRebuilder.s_Rebuilders.Release(fastLayoutRebuilder);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000BF348 File Offset: 0x000BD548
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000BF408 File Offset: 0x000BD608
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			FastLayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						Component component = list[j];
						if (component && component is ScrollRect)
						{
							if (((ScrollRect)component).content != rect)
							{
								action(list[j]);
							}
						}
						else
						{
							action(list[j]);
						}
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000BF50C File Offset: 0x000BD70C
		public List<Component> fetchComponents(RectTransform rect)
		{
			List<Component> list;
			if (this.components.TryGetValue(rect, out list))
			{
				return list;
			}
			list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			FastLayoutRebuilder.StripDisabledBehavioursFromList(list);
			this.components.Add(rect, list);
			return list;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000BF558 File Offset: 0x000BD758
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = this.fetchComponents(rect);
			Component component;
			if (list.Count > 0 || rect.TryGetComponent(typeof(ILayoutGroup), out component))
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000BF5D8 File Offset: 0x000BD7D8
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null || rect.gameObject == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			bool flag = true;
			RectTransform rectTransform = rect;
			RectTransform rectTransform2 = rectTransform.parent as RectTransform;
			while (flag && !(rectTransform2 == null) && !(rectTransform2.gameObject == null))
			{
				flag = false;
				rectTransform2.GetComponents(typeof(ILayoutGroup), list);
				for (int i = 0; i < list.Count; i++)
				{
					Component component = list[i];
					if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
					{
						flag = true;
						rectTransform = rectTransform2;
						break;
					}
				}
				rectTransform2 = rectTransform2.parent as RectTransform;
			}
			if (rectTransform == rect && !FastLayoutRebuilder.ValidController(rectTransform, list))
			{
				CollectionPool<List<Component>, Component>.Release(list);
				return;
			}
			FastLayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000BF6BC File Offset: 0x000BD8BC
		private static bool ValidController(RectTransform layoutRoot, List<Component> comps)
		{
			if (layoutRoot == null || layoutRoot.gameObject == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(ILayoutController), comps);
			for (int i = 0; i < comps.Count; i++)
			{
				Component component = comps[i];
				if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000BF730 File Offset: 0x000BD930
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			FastLayoutRebuilder fastLayoutRebuilder = FastLayoutRebuilder.s_Rebuilders.Get();
			fastLayoutRebuilder.Initialize(controller);
			if (!CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(fastLayoutRebuilder))
			{
				FastLayoutRebuilder.s_Rebuilders.Release(fastLayoutRebuilder);
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000BF76C File Offset: 0x000BD96C
		public void LayoutComplete()
		{
			FastLayoutRebuilder.s_Rebuilders.Release(this);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000BF779 File Offset: 0x000BD979
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000BF77B File Offset: 0x000BD97B
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000BF783 File Offset: 0x000BD983
		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000BF793 File Offset: 0x000BD993
		public override string ToString()
		{
			string text = "(Layout Rebuilder for) ";
			RectTransform toRebuild = this.m_ToRebuild;
			return text + ((toRebuild != null) ? toRebuild.ToString() : null);
		}

		// Token: 0x040012CC RID: 4812
		private static Dictionary<GameObject, LayoutGroup[]> objectLayoutGroups = new Dictionary<GameObject, LayoutGroup[]>();

		// Token: 0x040012CD RID: 4813
		private static LayoutGroup[] emptySet = new LayoutGroup[0];

		// Token: 0x040012CE RID: 4814
		private RectTransform m_ToRebuild;

		// Token: 0x040012CF RID: 4815
		private int m_CachedHashFromTransform;

		// Token: 0x040012D0 RID: 4816
		private static ObjectPool<FastLayoutRebuilder> s_Rebuilders = new ObjectPool<FastLayoutRebuilder>(() => new FastLayoutRebuilder(), null, delegate(FastLayoutRebuilder x)
		{
			x.Clear();
		}, null, true, 10, 10000);

		// Token: 0x040012D1 RID: 4817
		private Dictionary<RectTransform, List<Component>> components = new Dictionary<RectTransform, List<Component>>();
	}
}
