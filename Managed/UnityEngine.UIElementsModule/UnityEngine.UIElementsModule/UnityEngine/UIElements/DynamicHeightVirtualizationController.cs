using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000070 RID: 112
	internal class DynamicHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00011C43 File Offset: 0x0000FE43
		internal IReadOnlyDictionary<int, float> itemHeightCache
		{
			get
			{
				return this.m_ItemHeightCache;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00011C4C File Offset: 0x0000FE4C
		private float defaultExpectedHeight
		{
			get
			{
				bool flag = this.m_MinimumItemHeight > 0f;
				float num;
				if (flag)
				{
					num = this.m_MinimumItemHeight;
				}
				else
				{
					bool flag2 = this.m_CollectionView.m_ItemHeightIsInline && this.m_CollectionView.fixedItemHeight > 0f;
					if (flag2)
					{
						num = this.m_CollectionView.fixedItemHeight;
					}
					else
					{
						num = 22f;
					}
				}
				return num;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00011CB1 File Offset: 0x0000FEB1
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00011CBE File Offset: 0x0000FEBE
		private float contentPadding
		{
			get
			{
				return base.serializedData.contentPadding;
			}
			set
			{
				this.m_CollectionView.scrollView.contentContainer.style.paddingTop = value;
				base.serializedData.contentPadding = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00011CFA File Offset: 0x0000FEFA
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00011D07 File Offset: 0x0000FF07
		private float contentHeight
		{
			get
			{
				return base.serializedData.contentHeight;
			}
			set
			{
				this.m_CollectionView.scrollView.contentContainer.style.height = value;
				base.serializedData.contentHeight = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00011D43 File Offset: 0x0000FF43
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00011D50 File Offset: 0x0000FF50
		private int anchoredIndex
		{
			get
			{
				return base.serializedData.anchoredItemIndex;
			}
			set
			{
				base.serializedData.anchoredItemIndex = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00011D6B File Offset: 0x0000FF6B
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00011D78 File Offset: 0x0000FF78
		private float anchorOffset
		{
			get
			{
				return base.serializedData.anchorOffset;
			}
			set
			{
				base.serializedData.anchorOffset = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00011D94 File Offset: 0x0000FF94
		private float viewportMaxOffset
		{
			get
			{
				return this.m_ScrollView.scrollOffset.y + this.m_ScrollView.contentViewport.layout.height;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00011DCA File Offset: 0x0000FFCA
		protected override bool alwaysRebindOnRefresh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public DynamicHeightVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView)
		{
			this.m_FillCallback = new Action(this.Fill);
			this.m_GeometryChangedCallback = new Action<ReusableCollectionItem>(this.OnRecycledItemGeometryChanged);
			this.m_IndexOutOfBoundsPredicate = new Predicate<int>(this.IsIndexOutOfBounds);
			this.m_ScrollResetCallback = new Action(this.ResetScroll);
			collectionView.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanelEvent), TrickleDown.NoTrickleDown);
			collectionView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChangedEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011EA8 File Offset: 0x000100A8
		private void OnGeometryChangedEvent(GeometryChangedEvent _)
		{
			bool flag = this.m_ScrolledToItemIndex != null;
			if (flag)
			{
				bool flag2 = base.ShouldDeferScrollToItem(this.m_ScrolledToItemIndex ?? (-1));
				if (flag2)
				{
					base.ScheduleDeferredScrollToItem();
				}
				this.m_ScrolledToItemIndex = null;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00011F00 File Offset: 0x00010100
		public override void Refresh(bool rebuild)
		{
			this.CleanItemHeightCache();
			int count = this.m_ActiveItems.Count;
			bool flag = false;
			if (rebuild)
			{
				this.m_WaitingCache.Clear();
			}
			else
			{
				flag |= this.m_WaitingCache.RemoveWhere(this.m_IndexOutOfBoundsPredicate) > 0;
			}
			base.Refresh(rebuild);
			this.m_ScrollDirection = DynamicHeightVirtualizationController<T>.ScrollDirection.Idle;
			this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.None;
			bool flag2 = this.m_CollectionView.HasValidDataAndBindings();
			if (flag2)
			{
				bool flag3 = flag || count != this.m_ActiveItems.Count;
				if (flag3)
				{
					bool flag4 = this.m_RefreshScrollOffsetScheduledItem == null;
					if (flag4)
					{
						this.m_RefreshScrollOffsetScheduledItem = this.m_CollectionView.schedule.Execute(new Action(this.RefreshScrollOffset));
					}
					else
					{
						bool flag5 = !this.m_RefreshScrollOffsetScheduledItem.isActive;
						if (flag5)
						{
							this.m_RefreshScrollOffsetScheduledItem.Resume();
						}
					}
				}
				this.ScheduleFill();
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011FF8 File Offset: 0x000101F8
		public override void ScrollToItem(int index)
		{
			bool flag = index < -1;
			if (!flag)
			{
				bool flag2 = this.visibleItemCount == 0;
				if (flag2)
				{
					this.m_ScrolledToItemIndex = new int?(index);
				}
				else
				{
					base.ShouldDeferScrollToItem(index);
					float height = this.m_ScrollView.contentContainer.layout.height;
					float height2 = this.m_ScrollView.contentViewport.layout.height;
					Vector2 scrollOffset = this.m_ScrollView.scrollOffset;
					bool flag3 = index == -1;
					if (flag3)
					{
						this.m_ForcedLastVisibleItem = base.itemsCount - 1;
						this.m_ForcedFirstVisibleItem = -1;
						this.m_StickToBottom = true;
						this.m_ScrollView.scrollOffset = new Vector2(0f, (height2 >= height) ? 0f : height);
					}
					else
					{
						bool flag4 = this.firstVisibleIndex >= index;
						if (flag4)
						{
							Vector2 vector = new Vector2(0f, this.GetContentHeightForIndex(index - 1));
							bool flag5 = vector == this.m_ScrollView.scrollOffset;
							if (flag5)
							{
								return;
							}
							this.m_ForcedFirstVisibleItem = index;
							this.m_ForcedLastVisibleItem = -1;
							this.m_ScrollView.scrollOffset = vector;
						}
						else
						{
							float contentHeightForIndex = this.GetContentHeightForIndex(index);
							bool flag6 = float.IsNaN(height2) || contentHeightForIndex < this.contentPadding + height2;
							if (flag6)
							{
								return;
							}
							float num = contentHeightForIndex - height2 + 22f;
							this.m_ForcedLastVisibleItem = index;
							this.m_ForcedFirstVisibleItem = -1;
							this.m_ScrollView.scrollOffset = new Vector2(0f, num);
						}
					}
					bool flag7 = scrollOffset == this.m_ScrollView.scrollOffset;
					if (flag7)
					{
						this.OnScrollUpdate();
					}
				}
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000121AC File Offset: 0x000103AC
		public override void Resize(Vector2 size)
		{
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.contentHeight = Mathf.Max(expectedContentHeight, this.contentHeight);
			float height = this.m_ScrollView.contentViewport.layout.height;
			float num = Mathf.Max(0f, this.contentHeight - height);
			float num2 = Mathf.Min(base.serializedData.scrollOffset.y, num);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(num2);
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
			float num3 = this.m_CollectionView.ResolveItemHeight(size.y);
			int num4 = Mathf.CeilToInt(num3 / this.defaultExpectedHeight);
			int num5 = num4;
			bool flag = num5 <= 0;
			if (!flag)
			{
				num5 += 2;
				int num6 = Mathf.Min(num5, base.itemsCount);
				bool flag2 = this.m_ActiveItems.Count != num6;
				if (flag2)
				{
					int count = this.m_ActiveItems.Count;
					bool flag3 = count > num6;
					if (flag3)
					{
						int num7 = count - num6;
						for (int i = 0; i < num7; i++)
						{
							int num8 = this.m_ActiveItems.Count - 1;
							this.ReleaseItem(num8);
						}
					}
					else
					{
						int num9 = num6 - this.m_ActiveItems.Count;
						int num10 = ((this.firstVisibleIndex < 0) ? 0 : this.firstVisibleIndex);
						for (int j = 0; j < num9; j++)
						{
							int num11 = j + num10 + count;
							T orMakeItemAtIndex = this.GetOrMakeItemAtIndex(-1, -1);
							bool flag4 = this.IsIndexOutOfBounds(num11);
							if (flag4)
							{
								this.HideItem(this.m_ActiveItems.Count - 1);
							}
							else
							{
								base.Setup(orMakeItemAtIndex, num11);
								this.MarkWaitingForLayout(orMakeItemAtIndex);
							}
						}
					}
				}
				long num12 = DateTime.UtcNow.Ticks / 10000L;
				bool flag5 = (float)(num12 - this.m_TimeSinceFillScheduledMs) > 100f && this.m_TimeSinceFillScheduledMs != 0L && !this.m_FillExecuted;
				if (flag5)
				{
					this.Fill();
					this.ResetScroll();
					this.m_TimeSinceFillScheduledMs = 0L;
				}
				else
				{
					bool flag6 = this.m_TimeSinceFillScheduledMs == 0L;
					if (flag6)
					{
						this.m_TimeSinceFillScheduledMs = DateTime.UtcNow.Ticks / 10000L;
					}
					this.ScheduleFill();
					this.ScheduleScrollDirectionReset();
					this.m_FillExecuted = false;
				}
				this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001245C File Offset: 0x0001065C
		public override void OnScroll(Vector2 scrollOffset)
		{
			bool flag = this.m_DelayedScrollOffset == scrollOffset;
			if (!flag)
			{
				this.m_DelayedScrollOffset = scrollOffset;
				bool flag2 = this.m_ForcedFirstVisibleItem != -1 || this.m_ForcedLastVisibleItem != -1;
				if (flag2)
				{
					this.OnScrollUpdate();
					this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.ForcedScroll;
				}
				else
				{
					DynamicHeightVirtualizationController<T>.VirtualizationChange lastChange = this.m_LastChange;
					bool flag3 = lastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize || lastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.ForcedScroll;
					if (flag3)
					{
						float height = this.m_ScrollView.contentViewport.layout.height;
						float num = Mathf.Max(0f, this.contentHeight - height);
						float num2 = Mathf.Min(this.m_ScrollView.scrollOffset.y, num);
						this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
						this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(num2);
					}
					else
					{
						base.ScheduleScroll();
					}
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012558 File Offset: 0x00010758
		private void OnDetachFromPanelEvent(DetachFromPanelEvent evt)
		{
			IVisualElementScheduledItem scheduledItem = this.m_ScheduledItem;
			bool flag = scheduledItem != null && scheduledItem.isActive;
			if (flag)
			{
				this.m_ScheduledItem.Pause();
				this.m_ScheduledItem = null;
			}
			IVisualElementScheduledItem scrollResetScheduledItem = this.m_ScrollResetScheduledItem;
			bool flag2 = scrollResetScheduledItem != null && scrollResetScheduledItem.isActive;
			if (flag2)
			{
				this.m_ScrollResetScheduledItem.Pause();
				this.m_ScrollResetScheduledItem = null;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000125BC File Offset: 0x000107BC
		protected override void OnScrollUpdate()
		{
			Vector2 vector = (float.IsNegativeInfinity(this.m_DelayedScrollOffset.y) ? this.m_ScrollView.scrollOffset : this.m_DelayedScrollOffset);
			bool flag = float.IsNaN(this.m_ScrollView.contentViewport.layout.height) || float.IsNaN(vector.y);
			if (!flag)
			{
				this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.Scroll;
				float expectedContentHeight = this.GetExpectedContentHeight();
				this.contentHeight = Mathf.Max(expectedContentHeight, this.contentHeight);
				this.m_ScrollDirection = ((vector.y < this.m_ScrollView.scrollOffset.y) ? DynamicHeightVirtualizationController<T>.ScrollDirection.Up : DynamicHeightVirtualizationController<T>.ScrollDirection.Down);
				float num = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
				bool flag2 = vector.y <= 0f;
				if (flag2)
				{
					this.m_ForcedFirstVisibleItem = 0;
				}
				this.m_StickToBottom = num > 0f && Math.Abs(vector.y - this.m_ScrollView.verticalScroller.highValue) < float.Epsilon;
				this.m_ScrollView.SetScrollOffsetWithoutNotify(vector);
				base.serializedData.scrollOffset = this.m_ScrollView.scrollOffset;
				this.m_CollectionView.SaveViewData();
				int num2 = ((this.m_ForcedFirstVisibleItem != -1) ? this.m_ForcedFirstVisibleItem : this.GetFirstVisibleItem(this.m_ScrollView.scrollOffset.y));
				float contentHeightForIndex = this.GetContentHeightForIndex(num2 - 1);
				this.contentPadding = contentHeightForIndex;
				this.m_ForcedFirstVisibleItem = -1;
				bool flag3 = num2 != this.firstVisibleIndex;
				if (flag3)
				{
					this.CycleItems(num2);
				}
				else
				{
					this.Fill();
				}
				this.ScheduleScrollDirectionReset();
				this.m_DelayedScrollOffset = Vector2.negativeInfinity;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00012798 File Offset: 0x00010998
		private void CycleItems(int firstIndex)
		{
			bool flag = firstIndex == this.firstVisibleIndex;
			if (!flag)
			{
				T firstVisibleItem = base.firstVisibleItem;
				this.contentPadding = this.GetContentHeightForIndex(firstIndex - 1);
				this.firstVisibleIndex = firstIndex;
				bool flag2 = this.m_ActiveItems.Count > 0;
				if (flag2)
				{
					bool flag3 = firstVisibleItem == null || this.m_ActiveItems.Count <= Mathf.Abs(this.firstVisibleIndex - firstVisibleItem.index);
					if (!flag3)
					{
						bool flag4 = this.firstVisibleIndex < firstVisibleItem.index;
						if (flag4)
						{
							int num = firstVisibleItem.index - this.firstVisibleIndex;
							List<T> scrollInsertionList = this.m_ScrollInsertionList;
							for (int i = 0; i < num; i++)
							{
								List<T> activeItems = this.m_ActiveItems;
								T t = activeItems[activeItems.Count - 1];
								scrollInsertionList.Insert(0, t);
								this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
								t.rootElement.SendToBack();
							}
							this.m_ActiveItems.InsertRange(0, scrollInsertionList);
							this.m_ScrollInsertionList.Clear();
						}
						else
						{
							List<T> scrollInsertionList2 = this.m_ScrollInsertionList;
							int num2 = 0;
							int num3 = -1;
							while (this.firstVisibleIndex > this.m_ActiveItems[num2].index && num3 < this.m_ActiveItems[num2].index)
							{
								T t2 = this.m_ActiveItems[num2];
								num3 = t2.index;
								scrollInsertionList2.Add(t2);
								num2++;
								t2.rootElement.BringToFront();
							}
							this.m_ActiveItems.RemoveRange(0, num2);
							this.m_ActiveItems.AddRange(scrollInsertionList2);
							this.m_ScrollInsertionList.Clear();
						}
					}
					float num4 = this.contentPadding;
					for (int j = 0; j < this.m_ActiveItems.Count; j++)
					{
						T t3 = this.m_ActiveItems[j];
						int num5 = this.firstVisibleIndex + j;
						int index = t3.index;
						bool flag5 = t3.rootElement.style.display == DisplayStyle.Flex;
						this.m_WaitingCache.Remove(index);
						bool flag6 = this.IsIndexOutOfBounds(num5);
						if (flag6)
						{
							this.HideItem(j);
						}
						else
						{
							base.Setup(t3, num5);
							bool flag7 = num4 > this.viewportMaxOffset;
							bool flag8 = flag7;
							if (flag8)
							{
								this.HideItem(j);
							}
							else
							{
								bool flag9 = num5 != index || !flag5;
								if (flag9)
								{
									this.MarkWaitingForLayout(t3);
								}
							}
							num4 += this.GetExpectedItemHeight(num5);
						}
					}
				}
				bool flag10 = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
				if (flag10)
				{
					this.UpdateAnchor();
				}
				this.ScheduleFill();
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00012AC8 File Offset: 0x00010CC8
		private bool NeedsFill()
		{
			bool flag = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.None || this.anchoredIndex < 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				T t = base.lastVisibleItem;
				int num = ((t != null) ? t.index : (-1));
				float num2 = this.contentPadding;
				bool flag3 = num2 > this.m_ScrollView.scrollOffset.y;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					for (int i = this.firstVisibleIndex; i < base.itemsCount; i++)
					{
						bool flag4 = num2 > this.viewportMaxOffset || (num2 == this.viewportMaxOffset && !this.m_StickToBottom);
						if (flag4)
						{
							break;
						}
						num2 += this.GetExpectedItemHeight(i);
						bool flag5 = i > num;
						if (flag5)
						{
							return true;
						}
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00012B9C File Offset: 0x00010D9C
		private void Fill()
		{
			bool flag = !this.m_CollectionView.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_FillExecuted = true;
				bool flag2 = this.m_ActiveItems.Count == 0;
				if (flag2)
				{
					this.contentHeight = 0f;
					this.contentPadding = 0f;
				}
				else
				{
					bool flag3 = this.anchoredIndex < 0 || this.firstVisibleIndex < 0;
					if (!flag3)
					{
						bool flag4 = this.contentPadding > this.contentHeight;
						if (flag4)
						{
							this.OnScrollUpdate();
						}
						else
						{
							float num = this.contentPadding;
							float num2 = this.contentPadding;
							int num3 = 0;
							for (int i = this.firstVisibleIndex; i < base.itemsCount; i++)
							{
								bool flag5 = num2 > this.viewportMaxOffset || (num2 == this.viewportMaxOffset && !this.m_StickToBottom);
								if (flag5)
								{
									break;
								}
								num2 += this.GetExpectedItemHeight(i);
								T t = this.m_ActiveItems[num3++];
								bool flag6 = t.index != i || t.rootElement.style.display == DisplayStyle.None;
								if (flag6)
								{
									base.Setup(t, i);
									this.MarkWaitingForLayout(t);
								}
								bool flag7 = num3 >= this.m_ActiveItems.Count;
								if (flag7)
								{
									break;
								}
							}
							bool flag8 = this.firstVisibleIndex > 0 && this.contentPadding > this.m_ScrollView.scrollOffset.y;
							if (flag8)
							{
								List<T> scrollInsertionList = this.m_ScrollInsertionList;
								for (int j = this.m_ActiveItems.Count - 1; j >= num3; j--)
								{
									bool flag9 = this.firstVisibleIndex == 0;
									if (flag9)
									{
										break;
									}
									T t2 = this.m_ActiveItems[j];
									scrollInsertionList.Insert(0, t2);
									this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
									t2.rootElement.SendToBack();
									int num4 = this.firstVisibleIndex - 1;
									this.firstVisibleIndex = num4;
									int num5 = num4;
									base.Setup(t2, num5);
									this.MarkWaitingForLayout(t2);
									num -= this.GetExpectedItemHeight(num5);
									bool flag10 = num < this.m_ScrollView.scrollOffset.y;
									if (flag10)
									{
										break;
									}
								}
								this.m_ActiveItems.InsertRange(0, scrollInsertionList);
								this.m_ScrollInsertionList.Clear();
							}
							this.contentPadding = num;
							this.contentHeight = this.GetExpectedContentHeight();
							bool flag11 = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
							if (flag11)
							{
								this.UpdateAnchor();
							}
							bool flag12 = this.m_WaitingCache.Count == 0;
							if (flag12)
							{
								this.m_ScrollView.scrollOffset = base.serializedData.scrollOffset;
								this.ResetScroll();
								this.ApplyScrollViewUpdate(true);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00012EBC File Offset: 0x000110BC
		private void UpdateScrollViewContainer(float previousHeight, float newHeight)
		{
			bool stickToBottom = this.m_StickToBottom;
			if (!stickToBottom)
			{
				bool flag = this.m_ForcedLastVisibleItem >= 0;
				if (flag)
				{
					float contentHeightForIndex = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					this.m_ScrollView.SetScrollOffsetWithoutNotify(new Vector2(this.m_ScrollView.scrollOffset.x, contentHeightForIndex + 22f - this.m_ScrollView.contentViewport.layout.height));
				}
				else
				{
					bool flag2 = this.m_ScrollDirection == DynamicHeightVirtualizationController<T>.ScrollDirection.Up;
					if (flag2)
					{
						this.m_ScrollView.SetScrollOffsetWithoutNotify(new Vector2(this.m_ScrollView.scrollOffset.x, this.m_ScrollView.scrollOffset.y + (newHeight - previousHeight)));
					}
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012F84 File Offset: 0x00011184
		private void ApplyScrollViewUpdate(bool dimensionsOnly = false)
		{
			float contentPadding = this.contentPadding;
			float y = this.m_ScrollView.scrollOffset.y;
			float num = y - contentPadding;
			bool flag = this.anchoredIndex >= 0;
			if (flag)
			{
				bool flag2 = this.firstVisibleIndex != this.anchoredIndex;
				if (flag2)
				{
					this.CycleItems(this.anchoredIndex);
					this.ScheduleFill();
				}
				this.firstVisibleIndex = this.anchoredIndex;
				num = this.anchorOffset;
			}
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.contentHeight = expectedContentHeight;
			this.contentPadding = this.GetContentHeightForIndex(this.firstVisibleIndex - 1);
			float num2 = Mathf.Max(0f, this.m_ScrollView.RoundToPanelPixelSize(expectedContentHeight - this.m_ScrollView.contentViewport.layout.height));
			float num3 = Mathf.Min(this.contentPadding + num, num2);
			bool flag3 = this.m_StickToBottom && num2 > 0f;
			if (flag3)
			{
				num3 = num2;
			}
			else
			{
				bool flag4 = this.m_ForcedLastVisibleItem != -1;
				if (flag4)
				{
					float contentHeightForIndex = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					float num4 = contentHeightForIndex + 22f - this.m_ScrollView.contentViewport.layout.height;
					num3 = Mathf.Clamp(num4, 0f, num2);
					this.m_ForcedLastVisibleItem = -1;
				}
			}
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num2);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(num3);
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.slider.value;
			bool flag5 = dimensionsOnly || this.m_LastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
			if (flag5)
			{
				this.ScheduleScrollDirectionReset();
			}
			else
			{
				bool flag6 = this.NeedsFill();
				if (flag6)
				{
					this.Fill();
				}
				else
				{
					float num5 = this.contentPadding;
					int firstVisibleIndex = this.firstVisibleIndex;
					List<T> scrollInsertionList = this.m_ScrollInsertionList;
					int num6 = 0;
					for (int i = 0; i < this.m_ActiveItems.Count; i++)
					{
						T t = this.m_ActiveItems[i];
						int index = t.index;
						bool flag7 = index < 0;
						if (flag7)
						{
							break;
						}
						float expectedItemHeight = this.GetExpectedItemHeight(index);
						bool flag8 = this.m_ActiveItems[i].rootElement.style.display == DisplayStyle.Flex;
						if (flag8)
						{
							bool flag9 = num5 + expectedItemHeight < this.m_ScrollView.scrollOffset.y;
							if (flag9)
							{
								t.rootElement.BringToFront();
								this.HideItem(i);
								scrollInsertionList.Add(t);
								num6++;
								int firstVisibleIndex2 = this.firstVisibleIndex;
								this.firstVisibleIndex = firstVisibleIndex2 + 1;
							}
							else
							{
								bool flag10 = num5 > this.viewportMaxOffset;
								if (flag10)
								{
									this.HideItem(i);
								}
							}
						}
						num5 += this.GetExpectedItemHeight(index);
					}
					this.m_ActiveItems.RemoveRange(0, num6);
					this.m_ActiveItems.AddRange(scrollInsertionList);
					this.m_ScrollInsertionList.Clear();
					bool flag11 = this.firstVisibleIndex != firstVisibleIndex;
					if (flag11)
					{
						this.contentPadding = this.GetContentHeightForIndex(this.firstVisibleIndex - 1);
						this.UpdateAnchor();
					}
					this.ScheduleScrollDirectionReset();
					this.m_CollectionView.SaveViewData();
				}
				base.ScheduleDeferredScrollToItem();
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00013325 File Offset: 0x00011525
		private void UpdateAnchor()
		{
			this.anchoredIndex = this.firstVisibleIndex;
			this.anchorOffset = this.m_ScrollView.scrollOffset.y - this.contentPadding;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00013354 File Offset: 0x00011554
		private void ScheduleFill()
		{
			bool flag = this.m_ScheduledItem == null;
			if (flag)
			{
				this.m_ScheduledItem = this.m_CollectionView.schedule.Execute(this.m_FillCallback);
			}
			else
			{
				this.m_ScheduledItem.Pause();
				this.m_ScheduledItem.Resume();
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000133A8 File Offset: 0x000115A8
		private void ScheduleScrollDirectionReset()
		{
			bool flag = this.m_ScrollResetScheduledItem == null;
			if (flag)
			{
				this.m_ScrollResetScheduledItem = this.m_CollectionView.schedule.Execute(this.m_ScrollResetCallback);
			}
			else
			{
				this.m_ScrollResetScheduledItem.Pause();
				this.m_ScrollResetScheduledItem.Resume();
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000133FA File Offset: 0x000115FA
		private void ResetScroll()
		{
			this.m_ScrollDirection = DynamicHeightVirtualizationController<T>.ScrollDirection.Idle;
			this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.None;
			this.m_ScrollView.UpdateContentViewTransform();
			this.UpdateAnchor();
			this.m_CollectionView.SaveViewData();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001342C File Offset: 0x0001162C
		public override int GetIndexFromPosition(Vector2 position)
		{
			int num = 0;
			for (float num2 = 0f; num2 < position.y; num2 += this.GetExpectedItemHeight(num++))
			{
			}
			return num - 1;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013468 File Offset: 0x00011668
		public override float GetExpectedItemHeight(int index)
		{
			int draggedIndex = base.GetDraggedIndex();
			bool flag = draggedIndex >= 0 && index == draggedIndex;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float num2;
				num = (this.m_ItemHeightCache.TryGetValue(index, out num2) ? num2 : this.defaultExpectedHeight);
			}
			return num;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000134B4 File Offset: 0x000116B4
		private int GetFirstVisibleItem(float offset)
		{
			bool flag = offset <= 0f;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int num2 = -1;
				while (offset > 0f)
				{
					num2++;
					float expectedItemHeight = this.GetExpectedItemHeight(num2);
					offset -= expectedItemHeight;
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013500 File Offset: 0x00011700
		public override float GetExpectedContentHeight()
		{
			return this.m_AccumulatedHeight + (float)(base.itemsCount - this.m_ItemHeightCache.Count) * this.defaultExpectedHeight;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013534 File Offset: 0x00011734
		private float GetContentHeightForIndex(int lastIndex)
		{
			DynamicHeightVirtualizationController<T>.<>c__DisplayClass67_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			bool flag = lastIndex < 0;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				CS$<>8__locals1.draggedIndex = base.GetDraggedIndex();
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo;
				bool flag2 = this.m_HighestCachedIndex <= lastIndex && this.m_ContentHeightCache.TryGetValue(this.m_HighestCachedIndex, out contentHeightCacheInfo);
				if (flag2)
				{
					num = this.<GetContentHeightForIndex>g__GetContentHeightFromCachedHeight|67_0(lastIndex, in contentHeightCacheInfo, ref CS$<>8__locals1);
				}
				else
				{
					float num2 = 0f;
					for (int i = lastIndex; i >= 0; i--)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo2;
						bool flag3 = this.m_ContentHeightCache.TryGetValue(i, out contentHeightCacheInfo2);
						if (flag3)
						{
							return num2 + this.<GetContentHeightForIndex>g__GetContentHeightFromCachedHeight|67_0(i, in contentHeightCacheInfo2, ref CS$<>8__locals1);
						}
						num2 += ((CS$<>8__locals1.draggedIndex == i) ? 0f : this.defaultExpectedHeight);
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001360C File Offset: 0x0001180C
		private DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo GetCachedContentHeight(int index)
		{
			while (index >= 0)
			{
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo;
				bool flag = this.m_ContentHeightCache.TryGetValue(index, out contentHeightCacheInfo);
				if (flag)
				{
					return contentHeightCacheInfo;
				}
				index--;
			}
			return default(DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013654 File Offset: 0x00011854
		private void RegisterItemHeight(int index, float height)
		{
			bool flag = height <= 0f;
			if (!flag)
			{
				float num = this.m_CollectionView.ResolveItemHeight(height);
				float num2;
				bool flag2 = this.m_ItemHeightCache.TryGetValue(index, out num2);
				if (flag2)
				{
					this.m_AccumulatedHeight -= num2;
				}
				this.m_AccumulatedHeight += num;
				this.m_ItemHeightCache[index] = num;
				bool flag3 = index > this.m_HighestCachedIndex;
				if (flag3)
				{
					this.m_HighestCachedIndex = index;
				}
				bool flag4 = num2 == 0f;
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo cachedContentHeight = this.GetCachedContentHeight(index - 1);
				this.m_ContentHeightCache[index] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(cachedContentHeight.sum + num, cachedContentHeight.count + 1);
				foreach (KeyValuePair<int, float> keyValuePair in this.m_ItemHeightCache)
				{
					bool flag5 = keyValuePair.Key > index;
					if (flag5)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo = this.m_ContentHeightCache[keyValuePair.Key];
						this.m_ContentHeightCache[keyValuePair.Key] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(contentHeightCacheInfo.sum - num2 + num, flag4 ? (contentHeightCacheInfo.count + 1) : contentHeightCacheInfo.count);
					}
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000137B4 File Offset: 0x000119B4
		private void UnregisterItemHeight(int index)
		{
			float num;
			bool flag = !this.m_ItemHeightCache.TryGetValue(index, out num);
			if (!flag)
			{
				this.m_AccumulatedHeight -= num;
				this.m_ItemHeightCache.Remove(index);
				this.m_ContentHeightCache.Remove(index);
				int num2 = -1;
				foreach (KeyValuePair<int, float> keyValuePair in this.m_ItemHeightCache)
				{
					bool flag2 = keyValuePair.Key > index;
					if (flag2)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo = this.m_ContentHeightCache[keyValuePair.Key];
						this.m_ContentHeightCache[keyValuePair.Key] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(contentHeightCacheInfo.sum - num, contentHeightCacheInfo.count - 1);
					}
					bool flag3 = keyValuePair.Key > num2;
					if (flag3)
					{
						num2 = keyValuePair.Key;
					}
				}
				this.m_HighestCachedIndex = num2;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000138BC File Offset: 0x00011ABC
		private void CleanItemHeightCache()
		{
			bool flag = !this.IsIndexOutOfBounds(this.m_HighestCachedIndex);
			if (!flag)
			{
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					foreach (int num in this.m_ItemHeightCache.Keys)
					{
						bool flag2 = this.IsIndexOutOfBounds(num);
						if (flag2)
						{
							list.Add(num);
						}
					}
					foreach (int num2 in list)
					{
						this.UnregisterItemHeight(num2);
					}
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
				this.m_MinimumItemHeight = -1f;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000139B4 File Offset: 0x00011BB4
		private void OnRecycledItemGeometryChanged(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || item.isDragGhost || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			if (!flag)
			{
				bool flag2 = this.UpdateRegisteredHeight(item);
				if (flag2)
				{
					this.ApplyScrollViewUpdate(false);
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013A28 File Offset: 0x00011C28
		private bool UpdateRegisteredHeight(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || item.isDragGhost || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = item.rootElement.layout.height < this.defaultExpectedHeight;
				if (flag3)
				{
					this.m_MinimumItemHeight = item.rootElement.layout.height;
					this.Resize(this.m_ScrollView.layout.size);
				}
				float num = item.rootElement.layout.height - item.rootElement.resolvedStyle.paddingTop;
				float num2;
				bool flag4 = this.m_ItemHeightCache.TryGetValue(item.index, out num2);
				float num3 = (flag4 ? this.GetExpectedItemHeight(item.index) : this.defaultExpectedHeight);
				bool flag5 = this.m_WaitingCache.Count == 0;
				if (flag5)
				{
					bool flag6 = num > num3;
					if (flag6)
					{
						this.m_StickToBottom = false;
					}
					else
					{
						float num4 = num - num3;
						float num5 = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
						this.m_StickToBottom = num5 > 0f && this.m_ScrollView.scrollOffset.y >= this.m_ScrollView.verticalScroller.highValue + num4;
					}
				}
				bool flag7 = !flag4 || !Mathf.Approximately(num, num2);
				if (flag7)
				{
					this.RegisterItemHeight(item.index, num);
					this.UpdateScrollViewContainer(num3, num);
					bool flag8 = this.m_WaitingCache.Count == 0;
					if (flag8)
					{
						return true;
					}
				}
				flag2 = this.m_WaitingCache.Remove(item.index) && this.m_WaitingCache.Count == 0;
			}
			return flag2;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013C48 File Offset: 0x00011E48
		internal override T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T orMakeItemAtIndex = base.GetOrMakeItemAtIndex(activeItemIndex, scrollViewIndex);
			orMakeItemAtIndex.onGeometryChanged += this.m_GeometryChangedCallback;
			return orMakeItemAtIndex;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013C78 File Offset: 0x00011E78
		internal override void ReleaseItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			t.onGeometryChanged -= this.m_GeometryChangedCallback;
			int index = t.index;
			this.UnregisterItemHeight(index);
			base.ReleaseItem(activeItemsIndex);
			this.m_WaitingCache.Remove(index);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013CCE File Offset: 0x00011ECE
		internal override void StartDragItem(ReusableCollectionItem item)
		{
			this.m_WaitingCache.Remove(item.index);
			base.StartDragItem(item);
			this.m_DraggedItem.onGeometryChanged -= this.m_GeometryChangedCallback;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013D04 File Offset: 0x00011F04
		internal override void EndDrag(int dropIndex)
		{
			bool flag = this.m_DraggedItem.index < dropIndex;
			int index = this.m_DraggedItem.index;
			int num = (flag ? 1 : (-1));
			float expectedItemHeight = this.GetExpectedItemHeight(index);
			for (int num2 = index; num2 != dropIndex; num2 += num)
			{
				float expectedItemHeight2 = this.GetExpectedItemHeight(num2);
				float expectedItemHeight3 = this.GetExpectedItemHeight(num2 + num);
				bool flag2 = Mathf.Approximately(expectedItemHeight2, expectedItemHeight3);
				if (!flag2)
				{
					this.RegisterItemHeight(num2, expectedItemHeight3);
				}
			}
			this.RegisterItemHeight(flag ? (dropIndex - 1) : dropIndex, expectedItemHeight);
			bool flag3 = this.firstVisibleIndex > this.m_DraggedItem.index;
			if (flag3)
			{
				this.firstVisibleIndex = this.GetFirstVisibleItem(this.m_ScrollView.scrollOffset.y);
				this.UpdateAnchor();
			}
			this.m_DraggedItem.onGeometryChanged += this.m_GeometryChangedCallback;
			base.EndDrag(dropIndex);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013E08 File Offset: 0x00012008
		private void HideItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			t.rootElement.style.display = DisplayStyle.None;
			this.m_WaitingCache.Remove(t.index);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013E58 File Offset: 0x00012058
		private void MarkWaitingForLayout(T item)
		{
			bool isDragGhost = item.isDragGhost;
			if (!isDragGhost)
			{
				this.m_WaitingCache.Add(item.index);
				item.rootElement.lastLayout = Rect.zero;
				item.rootElement.MarkDirtyRepaint();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013EB4 File Offset: 0x000120B4
		private bool IsIndexOutOfBounds(int i)
		{
			return this.m_CollectionView.itemsSource == null || i >= base.itemsCount;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013EE4 File Offset: 0x000120E4
		private void RefreshScrollOffset()
		{
			this.contentHeight = this.GetExpectedContentHeight();
			float num = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
			this.m_ScrollView.scrollOffset = base.serializedData.scrollOffset;
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00013F78 File Offset: 0x00012178
		[CompilerGenerated]
		private float <GetContentHeightForIndex>g__GetContentHeightFromCachedHeight|67_0(int index, in DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo heightInfo, ref DynamicHeightVirtualizationController<T>.<>c__DisplayClass67_0 A_3)
		{
			bool flag = A_3.draggedIndex >= 0 && index >= A_3.draggedIndex;
			float num;
			if (flag)
			{
				num = heightInfo.sum + (float)(index - heightInfo.count + 1) * this.defaultExpectedHeight - this.m_DraggedItem.rootElement.layout.height;
			}
			else
			{
				num = heightInfo.sum + (float)(index - heightInfo.count + 1) * this.defaultExpectedHeight;
			}
			return num;
		}

		// Token: 0x040001FF RID: 511
		private int m_HighestCachedIndex = -1;

		// Token: 0x04000200 RID: 512
		private readonly Dictionary<int, float> m_ItemHeightCache = new Dictionary<int, float>(32);

		// Token: 0x04000201 RID: 513
		private readonly Dictionary<int, DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo> m_ContentHeightCache = new Dictionary<int, DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo>(32);

		// Token: 0x04000202 RID: 514
		private readonly HashSet<int> m_WaitingCache = new HashSet<int>(32);

		// Token: 0x04000203 RID: 515
		private int? m_ScrolledToItemIndex;

		// Token: 0x04000204 RID: 516
		private int m_ForcedFirstVisibleItem = -1;

		// Token: 0x04000205 RID: 517
		private int m_ForcedLastVisibleItem = -1;

		// Token: 0x04000206 RID: 518
		private bool m_StickToBottom;

		// Token: 0x04000207 RID: 519
		private DynamicHeightVirtualizationController<T>.VirtualizationChange m_LastChange;

		// Token: 0x04000208 RID: 520
		private DynamicHeightVirtualizationController<T>.ScrollDirection m_ScrollDirection;

		// Token: 0x04000209 RID: 521
		private Vector2 m_DelayedScrollOffset = Vector2.negativeInfinity;

		// Token: 0x0400020A RID: 522
		private float m_AccumulatedHeight;

		// Token: 0x0400020B RID: 523
		private float m_MinimumItemHeight = -1f;

		// Token: 0x0400020C RID: 524
		private Action m_FillCallback;

		// Token: 0x0400020D RID: 525
		private Action m_ScrollResetCallback;

		// Token: 0x0400020E RID: 526
		private Action<ReusableCollectionItem> m_GeometryChangedCallback;

		// Token: 0x0400020F RID: 527
		private IVisualElementScheduledItem m_ScheduledItem;

		// Token: 0x04000210 RID: 528
		private IVisualElementScheduledItem m_ScrollResetScheduledItem;

		// Token: 0x04000211 RID: 529
		private IVisualElementScheduledItem m_RefreshScrollOffsetScheduledItem;

		// Token: 0x04000212 RID: 530
		private Predicate<int> m_IndexOutOfBoundsPredicate;

		// Token: 0x04000213 RID: 531
		private bool m_FillExecuted;

		// Token: 0x04000214 RID: 532
		private long m_TimeSinceFillScheduledMs;

		// Token: 0x04000215 RID: 533
		private const float k_ForceRefreshIntervalInMilliseconds = 100f;

		// Token: 0x02000071 RID: 113
		private readonly struct ContentHeightCacheInfo
		{
			// Token: 0x06000446 RID: 1094 RVA: 0x00013FFA File Offset: 0x000121FA
			public ContentHeightCacheInfo(float sum, int count)
			{
				this.sum = sum;
				this.count = count;
			}

			// Token: 0x04000216 RID: 534
			public readonly float sum;

			// Token: 0x04000217 RID: 535
			public readonly int count;
		}

		// Token: 0x02000072 RID: 114
		private enum VirtualizationChange
		{
			// Token: 0x04000219 RID: 537
			None,
			// Token: 0x0400021A RID: 538
			Resize,
			// Token: 0x0400021B RID: 539
			Scroll,
			// Token: 0x0400021C RID: 540
			ForcedScroll
		}

		// Token: 0x02000073 RID: 115
		private enum ScrollDirection
		{
			// Token: 0x0400021E RID: 542
			Idle,
			// Token: 0x0400021F RID: 543
			Up,
			// Token: 0x04000220 RID: 544
			Down
		}
	}
}
