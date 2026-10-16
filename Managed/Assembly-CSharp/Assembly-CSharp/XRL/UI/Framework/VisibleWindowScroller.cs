using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x02001522 RID: 5410
	public class VisibleWindowScroller : FrameworkScroller
	{
		// Token: 0x0600D283 RID: 53891 RVA: 0x004E2298 File Offset: 0x004E0498
		public VisibleWindowScroller()
		{
			this.scrollContext = new CyclicScrollContext<FrameworkDataElement, NavigationContext>
			{
				preSelect = new Action<int>(this.ScrollIndexIntoView)
			};
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600D284 RID: 53892 RVA: 0x004E231C File Offset: 0x004E051C
		public float spacerHeight
		{
			get
			{
				float num = this._spacerHeight.GetValueOrDefault();
				if (this._spacerHeight == null)
				{
					num = ((this.spacerPrefab == null) ? 0f : this.spacerPrefab.GetComponent<RectTransform>().rect.height);
					this._spacerHeight = new float?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600D285 RID: 53893 RVA: 0x004E2380 File Offset: 0x004E0580
		public float spacing
		{
			get
			{
				float? spacing = this._spacing;
				if (spacing == null)
				{
					return this.childRoot.GetComponent<VerticalLayoutGroup>().spacing;
				}
				return spacing.GetValueOrDefault();
			}
		}

		// Token: 0x0600D286 RID: 53894 RVA: 0x004E23B8 File Offset: 0x004E05B8
		public float HeightOfItem(int index)
		{
			float num;
			if (this.HeightMemory.TryGetValue(index, out num))
			{
				return num;
			}
			return this.AverageHeight;
		}

		// Token: 0x0600D287 RID: 53895 RVA: 0x004E23E0 File Offset: 0x004E05E0
		public int ItemsFullyAboveScrollTop()
		{
			int num = 0;
			float num2 = 0f;
			while (num2 < this.lastScrollPosition && num < this.choices.Count)
			{
				num2 += this.spacing * 2f + this.spacerHeight + this.HeightOfItem(num);
				if (num2 >= this.lastScrollPosition)
				{
					return num;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600D288 RID: 53896 RVA: 0x004E243C File Offset: 0x004E063C
		public float HeightOfFirstNItems(int n)
		{
			float num = (this.spacing * 2f + this.spacerHeight) * (float)n;
			for (int i = 0; i < n; i++)
			{
				num += this.HeightOfItem(i);
			}
			return num;
		}

		// Token: 0x0600D289 RID: 53897 RVA: 0x004E2478 File Offset: 0x004E0678
		public float HeightOfItemsStartingAtIndexN(int n)
		{
			float num = (this.spacing * 2f + this.spacerHeight) * (float)(this.choices.Count - 1 - n);
			for (int i = n; i < this.choices.Count; i++)
			{
				num += this.HeightOfItem(i);
			}
			return num;
		}

		// Token: 0x0600D28A RID: 53898 RVA: 0x004E24CC File Offset: 0x004E06CC
		public float ItemsInView()
		{
			return Math.Max(1f, this.scrollRect.viewport.rect.height / (this.MinimumHeight + this.spacerHeight + this.spacing * 2f));
		}

		// Token: 0x0600D28B RID: 53899 RVA: 0x004E2516 File Offset: 0x004E0716
		public override void Update()
		{
			if (!this.prefabNullWarned && this.selectionPrefab == null)
			{
				this.prefabNullWarned = true;
				MetricsManager.LogError(base.gameObject.name + " -> selectionPrefab became null!");
			}
			base.Update();
		}

		// Token: 0x0600D28C RID: 53900 RVA: 0x004E2555 File Offset: 0x004E0755
		public override void Awake()
		{
			base.Awake();
			this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollChange));
		}

		// Token: 0x0600D28D RID: 53901 RVA: 0x004E257C File Offset: 0x004E077C
		public virtual void OnScrollChange(Vector2 value)
		{
			this.lastScrollPosition = (1f - value.y) * (this.scrollRect.content.rect.height - this.scrollRect.viewport.rect.height);
			this.ReflowScroller(false);
		}

		// Token: 0x0600D28E RID: 53902 RVA: 0x004E25D4 File Offset: 0x004E07D4
		public override void SelectHighest()
		{
			if (this.selectionClones.Count <= 0)
			{
				return;
			}
			int index = (from a in this.selectionClones.FindAll((FrameworkUnityScrollChild c) => c.enabled && ScrollViewCalcs.GetScrollViewCalcs(c.GetComponent<RectTransform>(), this.scrollCalcs).isAnyInView)
				orderby (a.transform as RectTransform).GetSiblingIndex()
				select a).First<FrameworkUnityScrollChild>().GetComponent<FrameworkUnityScrollChild>().scrollContext.index;
			this.scrollContext.SelectIndex(index);
			this.ScrollIndexIntoView(index);
		}

		// Token: 0x0600D28F RID: 53903 RVA: 0x004E2654 File Offset: 0x004E0854
		public override void SelectLowest()
		{
			if (this.selectionClones.Count <= 0)
			{
				return;
			}
			int index = (from a in this.selectionClones.FindAll((FrameworkUnityScrollChild c) => c.gameObject.activeInHierarchy && ScrollViewCalcs.GetScrollViewCalcs(c.GetComponent<RectTransform>(), this.scrollCalcs).isAnyInView)
				orderby (a.transform as RectTransform).GetSiblingIndex()
				select a).Last<FrameworkUnityScrollChild>().GetComponent<FrameworkUnityScrollChild>().scrollContext.index;
			this.scrollContext.SelectIndex(index);
			this.ScrollIndexIntoView(index);
		}

		// Token: 0x0600D290 RID: 53904 RVA: 0x004E26D4 File Offset: 0x004E08D4
		public override void DoPageUp()
		{
			this.scrollRect.verticalNormalizedPosition += (this.scrollRect.viewport.rect.height - this.AverageHeight) / this.scrollRect.content.rect.height;
			this.scrollRect.verticalNormalizedPosition = Math.Clamp(this.scrollRect.verticalNormalizedPosition, 0f, 1f);
			this.OnScrollChange(this.scrollRect.normalizedPosition);
			this.SelectHighest();
		}

		// Token: 0x0600D291 RID: 53905 RVA: 0x004E2768 File Offset: 0x004E0968
		public override void DoPageDown()
		{
			this.scrollRect.verticalNormalizedPosition -= (this.scrollRect.viewport.rect.height - this.AverageHeight) / this.scrollRect.content.rect.height;
			this.scrollRect.verticalNormalizedPosition = Math.Clamp(this.scrollRect.verticalNormalizedPosition, 0f, 1f);
			this.OnScrollChange(this.scrollRect.normalizedPosition);
			this.SelectLowest();
		}

		// Token: 0x0600D292 RID: 53906 RVA: 0x004E27FB File Offset: 0x004E09FB
		protected override void InvalidatePrefabs()
		{
			base.InvalidatePrefabs();
			this.scrollContext.contexts.Clear();
			this.LastPrefabIndex.Clear();
		}

		// Token: 0x0600D293 RID: 53907 RVA: 0x004E2820 File Offset: 0x004E0A20
		public void ReflowScroller(bool force = false)
		{
			CyclicScrollContext<FrameworkDataElement, NavigationContext> cyclicScrollContext = this.scrollContext as CyclicScrollContext<FrameworkDataElement, NavigationContext>;
			if (cyclicScrollContext != null)
			{
				cyclicScrollContext.intentional = true;
			}
			if (this.OffscreenSelectedContext != null && !this.OffscreenSelectedContext.IsActive(true))
			{
				this.OffscreenSelectedContext = null;
			}
			int num = this.ItemsFullyAboveScrollTop();
			int num2 = Math.Max(0, this.choices.Count - this.ItemsRendered);
			int num3 = Math.Max(0, num - (int)(((double)this.ItemsRendered - Math.Ceiling((double)this.ItemsInView())) / 2.0));
			if (force || Math.Min(num3, num2) != this.topRenderedIndex)
			{
				this.topRenderedIndex = Math.Min(num3, num2);
				this.TopPadder.minHeight = this.HeightOfFirstNItems(this.topRenderedIndex);
				this.BottomPadder.minHeight = this.HeightOfItemsStartingAtIndexN(this.topRenderedIndex + this.ItemsRendered);
				int num4 = 1;
				for (int i = 0; i < Math.Min(this.choices.Count, Math.Min(this.selectionClones.Count, this.ItemsRendered)); i++)
				{
					int num5 = this.topRenderedIndex + i;
					int num6 = num5 % this.ItemsRendered;
					this.selectionClones[num6].gameObject.SetActive(true);
					this.selectionClones[num6].transform.SetSiblingIndex(num4++);
					if (this.spacerPrefab != null)
					{
						this.spacerClones[num6].gameObject.SetActive(true);
						this.spacerClones[num6].transform.SetSiblingIndex(num4++);
					}
					if (!this.LastPrefabIndex.ContainsKey(num6) || this.LastPrefabIndex[num6] != num5)
					{
						ScrollChildContext scrollChildContext = this.scrollContext.GetContextAt(num5) as ScrollChildContext;
						ScrollChildContext offscreenSelectedContext = this.OffscreenSelectedContext;
						if (offscreenSelectedContext != null && offscreenSelectedContext.index == num5)
						{
							scrollChildContext = this.OffscreenSelectedContext;
							this.scrollContext.contexts[num6] = scrollChildContext;
							this.OffscreenSelectedContext = null;
						}
						else if (scrollChildContext.index != num5 && scrollChildContext.IsActive(true))
						{
							ScrollChildContext scrollChildContext2 = scrollChildContext;
							ScrollChildContext offscreenSelectedContext2 = this.OffscreenSelectedContext;
							if (offscreenSelectedContext2 != null && offscreenSelectedContext2.index == num5)
							{
								scrollChildContext = this.OffscreenSelectedContext;
							}
							else
							{
								scrollChildContext = this.MakeContextFor(null, num5);
								scrollChildContext.index = num5;
								scrollChildContext.parentContext = this.scrollContext;
							}
							this.OffscreenSelectedContext = scrollChildContext2;
							this.scrollContext.contexts[num6] = scrollChildContext;
						}
						this.SetupPrefab(this.selectionClones[num6], scrollChildContext, this.scrollContext.GetDataAt(num5), num5);
					}
					FrameworkScroller.IIndexSensitiveScrollChild indexSensitiveScrollChild = this.selectionClones[num6] as FrameworkScroller.IIndexSensitiveScrollChild;
					if (indexSensitiveScrollChild != null)
					{
						indexSensitiveScrollChild.SetIndex(num5 - num);
					}
				}
				this.lastItemsAbove = num;
			}
			if (num != this.lastItemsAbove)
			{
				for (int j = 0; j < Math.Min(this.choices.Count, Math.Min(this.selectionClones.Count, this.ItemsRendered)); j++)
				{
					int num7 = this.topRenderedIndex + j;
					int num8 = num7 % this.ItemsRendered;
					FrameworkScroller.IIndexSensitiveScrollChild indexSensitiveScrollChild2 = this.selectionClones[num8] as FrameworkScroller.IIndexSensitiveScrollChild;
					if (indexSensitiveScrollChild2 != null)
					{
						indexSensitiveScrollChild2.SetIndex(num7 - num);
					}
				}
				this.lastItemsAbove = num;
			}
			if (!this.StaticHeight && this.HeightsToCheck.Count > 0)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.scrollRect.content.transform as RectTransform);
				foreach (KeyValuePair<int, RectTransform> keyValuePair in this.HeightsToCheck)
				{
					this.HeightMemory[keyValuePair.Key] = keyValuePair.Value.rect.height;
				}
				this.HeightsToCheck.Clear();
				this.AverageHeight = 0f;
				if (this.MinimumHeight < 20f)
				{
					this.MinimumHeight = 20f;
				}
				foreach (float num9 in this.HeightMemory.Values)
				{
					this.AverageHeight += num9 / (float)this.HeightMemory.Count;
					this.MinimumHeight = Math.Min(this.MinimumHeight, num9);
				}
				if (this.MinimumHeight < 1f)
				{
					this.MinimumHeight = 1f;
				}
			}
			if (cyclicScrollContext != null)
			{
				cyclicScrollContext.intentional = false;
			}
		}

		// Token: 0x0600D294 RID: 53908 RVA: 0x004E2CE8 File Offset: 0x004E0EE8
		public override void SetupPrefab(FrameworkUnityScrollChild newChild, ScrollChildContext context, FrameworkDataElement data, int index)
		{
			context.index = index;
			base.SetupPrefab(newChild, context, data, index);
			RectTransform rectTransform = newChild.transform as RectTransform;
			this.HeightsToCheck[index] = rectTransform;
			this.LastPrefabIndex.Set(this.selectionClones.IndexOf(newChild), index);
			context.Setup();
		}

		// Token: 0x0600D295 RID: 53909 RVA: 0x004E2D44 File Offset: 0x004E0F44
		public override FrameworkUnityScrollChild GetPrefabForIndex(int x)
		{
			if (this.selectionPrefab == null)
			{
				MetricsManager.LogError("selectionPrefab was null");
				return null;
			}
			while (this.selectionClones.Count < x % this.ItemsRendered + 1)
			{
				this.selectionClones.Add(global::UnityEngine.Object.Instantiate<FrameworkUnityScrollChild>(this.selectionPrefab));
			}
			int num = x % this.ItemsRendered;
			if (x % this.ItemsRendered >= this.selectionClones.Count || x % this.ItemsRendered < 0)
			{
				return this.selectionClones.FirstOrDefault<FrameworkUnityScrollChild>();
			}
			return this.selectionClones[x % this.ItemsRendered];
		}

		// Token: 0x0600D296 RID: 53910 RVA: 0x004E2DE0 File Offset: 0x004E0FE0
		public override void UpdateSelections(IEnumerable<FrameworkDataElement> selections = null)
		{
			base.UpdateSelections(selections);
			this.ReflowScroller(true);
		}

		// Token: 0x0600D297 RID: 53911 RVA: 0x004E2DF0 File Offset: 0x004E0FF0
		public override void LayoutChildren()
		{
			this._spacerHeight = null;
			this.LastPrefabIndex.Clear();
			this.childrenToDelete.Clear();
			for (int i = 0; i < this.childRoot.transform.childCount; i++)
			{
				Transform child = this.childRoot.transform.GetChild(i);
				bool flag = false;
				for (int j = 0; j < this.selectionClones.Count; j++)
				{
					if (this.selectionClones[j].gameObject == child.gameObject)
					{
						flag = true;
						break;
					}
				}
				if (!flag && !this.spacerClones.Contains(child.gameObject))
				{
					LayoutElement topPadder = this.TopPadder;
					if (!(((topPadder != null) ? topPadder.transform : null) == child))
					{
						LayoutElement bottomPadder = this.BottomPadder;
						if (!(((bottomPadder != null) ? bottomPadder.transform : null) == child))
						{
							this.childrenToDelete.Add(child.gameObject);
						}
					}
				}
			}
			for (int k = 0; k < this.childrenToDelete.Count; k++)
			{
				this.childrenToDelete[k].DestroyImmediate();
			}
			this.childrenToDelete.Clear();
			if (this.TopPadder == null)
			{
				this.TopPadder = new GameObject("Top Padder", new Type[]
				{
					typeof(RectTransform),
					typeof(LayoutElement)
				}).GetComponent<LayoutElement>();
				this.TopPadder.transform.SetParent(this.childRoot.transform, false);
			}
			this.HeightMemory.Clear();
			for (;;)
			{
				if (this.lastContexts.Count > 0)
				{
					this.scrollContext.contexts.AddRange(this.lastContexts);
					this.lastContexts.Clear();
				}
				int num = Math.Min(this.ItemsRendered, this.choices.Count);
				for (int l = this.scrollContext.contexts.Count; l < num; l++)
				{
					this.scrollContext.contexts.Add(this.MakeContextFor(null, l));
				}
				if (this.spacerPrefab != null)
				{
					int num2 = Math.Min(this.ItemsRendered, this.choices.Count);
					for (int m = this.spacerClones.Count; m < num2; m++)
					{
						GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.spacerPrefab);
						gameObject.transform.SetParent(this.childRoot.transform, false);
						this.spacerClones.Add(gameObject);
					}
				}
				int num3 = Math.Min(this.ItemsRendered, this.choices.Count);
				for (int n = this.selectionClones.Count; n < num3; n++)
				{
					FrameworkUnityScrollChild prefabForIndex = this.GetPrefabForIndex(n);
					prefabForIndex.gameObject.name = string.Format("#{0} {1}", n, this.selectionPrefab.gameObject.name);
					prefabForIndex.transform.SetParent(this.childRoot.transform, false);
				}
				if (this.BottomPadder == null)
				{
					this.BottomPadder = new GameObject("Bottom Padder", new Type[]
					{
						typeof(RectTransform),
						typeof(LayoutElement)
					}).GetComponent<LayoutElement>();
					this.BottomPadder.transform.SetParent(this.childRoot.transform, false);
				}
				this.ReflowScroller(true);
				if (this.ItemsInView() * 1.5f <= (float)this.ItemsRendered)
				{
					break;
				}
				this.ItemsRendered = (int)Math.Ceiling((double)(this.ItemsInView() * 2.5f));
			}
			if (this.selectionClones.Count > this.choices.Count)
			{
				for (int num4 = this.choices.Count; num4 < this.selectionClones.Count; num4++)
				{
					this.selectionClones[num4].gameObject.SetActive(false);
					if (this.spacerClones.Count > num4)
					{
						this.spacerClones[num4].gameObject.SetActive(false);
					}
				}
			}
		}

		// Token: 0x0600D298 RID: 53912 RVA: 0x004E3204 File Offset: 0x004E1404
		public override ScrollChildContext MakeContextFor(FrameworkDataElement data, int index)
		{
			return base.MakeContextFor(data, index);
		}

		// Token: 0x0600D299 RID: 53913 RVA: 0x004E3210 File Offset: 0x004E1410
		public Vector2 ScrollOffsetForIndex(int index)
		{
			float num = this.HeightOfFirstNItems(index);
			return new Vector2(0f, 1f - num / (this.scrollRect.content.rect.height - this.scrollRect.viewport.rect.height));
		}

		// Token: 0x0600D29A RID: 53914 RVA: 0x004E3268 File Offset: 0x004E1468
		public void ScrollIndexIntoView(int index)
		{
			if (index < this.topRenderedIndex || index >= this.topRenderedIndex + this.ItemsRendered)
			{
				this.OnScrollChange(this.ScrollOffsetForIndex(index));
				Canvas.ForceUpdateCanvases();
			}
			FrameworkUnityScrollChild prefabForIndex = this.GetPrefabForIndex(index);
			if (prefabForIndex != null)
			{
				ScrollViewCalcs.ScrollIntoView(prefabForIndex.GetComponent<RectTransform>(), this.scrollViewCalcs);
				this.OnScrollChange(this.scrollRect.normalizedPosition);
				ScrollViewCalcs.ScrollIntoView(prefabForIndex.GetComponent<RectTransform>(), this.scrollViewCalcs);
			}
		}

		// Token: 0x0600D29B RID: 53915 RVA: 0x004E32E4 File Offset: 0x004E14E4
		public override void ScrollSelectedToTop()
		{
			this.OnScrollChange(this.scrollRect.normalizedPosition);
			Canvas.ForceUpdateCanvases();
			base.ScrollSelectedToTop();
		}

		// Token: 0x0600D29C RID: 53916 RVA: 0x004E3302 File Offset: 0x004E1502
		public override void ScrollSelectedIntoView()
		{
			if (this.ScrollOnSelection())
			{
				this.ScrollIndexIntoView(this.selectedPosition);
			}
		}

		// Token: 0x04005379 RID: 21369
		public LayoutElement TopPadder;

		// Token: 0x0400537A RID: 21370
		public LayoutElement BottomPadder;

		// Token: 0x0400537B RID: 21371
		public int ItemsRendered = 10;

		// Token: 0x0400537C RID: 21372
		public Dictionary<int, float> HeightMemory = new Dictionary<int, float>();

		// Token: 0x0400537D RID: 21373
		public bool StaticHeight;

		// Token: 0x0400537E RID: 21374
		public float AverageHeight = 20f;

		// Token: 0x0400537F RID: 21375
		public float MinimumHeight = 20f;

		// Token: 0x04005380 RID: 21376
		public float lastScrollPosition;

		// Token: 0x04005381 RID: 21377
		public int topRenderedIndex;

		// Token: 0x04005382 RID: 21378
		public Dictionary<int, int> LastPrefabIndex = new Dictionary<int, int>();

		// Token: 0x04005383 RID: 21379
		private float? _spacerHeight;

		// Token: 0x04005384 RID: 21380
		private float? _spacing;

		// Token: 0x04005385 RID: 21381
		private bool prefabNullWarned;

		// Token: 0x04005386 RID: 21382
		public ScrollChildContext OffscreenSelectedContext;

		// Token: 0x04005387 RID: 21383
		private int lastItemsAbove = -1;

		// Token: 0x04005388 RID: 21384
		private Dictionary<int, RectTransform> HeightsToCheck = new Dictionary<int, RectTransform>();

		// Token: 0x04005389 RID: 21385
		private List<GameObject> childrenToDelete = new List<GameObject>();
	}
}
