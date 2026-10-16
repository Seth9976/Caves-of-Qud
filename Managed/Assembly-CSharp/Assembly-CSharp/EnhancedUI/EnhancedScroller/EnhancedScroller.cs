using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EnhancedUI.EnhancedScroller
{
	// Token: 0x020001DC RID: 476
	[RequireComponent(typeof(ScrollRect))]
	public class EnhancedScroller : MonoBehaviour
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00066424 File Offset: 0x00064624
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x0006642C File Offset: 0x0006462C
		public IEnhancedScrollerDelegate Delegate
		{
			get
			{
				return this._delegate;
			}
			set
			{
				this._delegate = value;
				this._reloadData = true;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0006643C File Offset: 0x0006463C
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x00066444 File Offset: 0x00064644
		public float ScrollPosition
		{
			get
			{
				return this._scrollPosition;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, this.GetScrollPositionForCellViewIndex(this._cellViewSizeArray.Count - 1, EnhancedScroller.CellViewPositionEnum.Before));
				if (this._scrollPosition != value)
				{
					this._scrollPosition = value;
					if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
					{
						this._scrollRect.verticalNormalizedPosition = 1f - this._scrollPosition / this._ScrollSize;
					}
					else
					{
						this._scrollRect.horizontalNormalizedPosition = this._scrollPosition / this._ScrollSize;
					}
					this._refreshActive = true;
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000664C9 File Offset: 0x000646C9
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000664D4 File Offset: 0x000646D4
		public bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				if (this.loop != value)
				{
					float scrollPosition = this._scrollPosition;
					this.loop = value;
					this._Resize(false);
					if (this.loop)
					{
						this.ScrollPosition = this._loopFirstScrollPosition + scrollPosition;
					}
					else
					{
						this.ScrollPosition = scrollPosition - this._loopFirstScrollPosition;
					}
					this.ScrollbarVisibility = this.scrollbarVisibility;
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00066531 File Offset: 0x00064731
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x0006653C File Offset: 0x0006473C
		public EnhancedScroller.ScrollbarVisibilityEnum ScrollbarVisibility
		{
			get
			{
				return this.scrollbarVisibility;
			}
			set
			{
				this.scrollbarVisibility = value;
				if (this._scrollbar != null && this._cellViewOffsetArray != null && this._cellViewOffsetArray.Count > 0)
				{
					if (this._cellViewOffsetArray.Last() < this.ScrollRectSize || this.loop)
					{
						this._scrollbar.gameObject.SetActive(this.scrollbarVisibility == EnhancedScroller.ScrollbarVisibilityEnum.Always);
						return;
					}
					this._scrollbar.gameObject.SetActive(this.scrollbarVisibility != EnhancedScroller.ScrollbarVisibilityEnum.Never);
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000665C5 File Offset: 0x000647C5
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x000665D2 File Offset: 0x000647D2
		public Vector2 Velocity
		{
			get
			{
				return this._scrollRect.velocity;
			}
			set
			{
				this._scrollRect.velocity = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x000665E0 File Offset: 0x000647E0
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x0006660B File Offset: 0x0006480B
		public float LinearVelocity
		{
			get
			{
				if (this.scrollDirection != EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					return this._scrollRect.velocity.x;
				}
				return this._scrollRect.velocity.y;
			}
			set
			{
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					this._scrollRect.velocity = new Vector2(0f, value);
					return;
				}
				this._scrollRect.velocity = new Vector2(value, 0f);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00066642 File Offset: 0x00064842
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x0006664A File Offset: 0x0006484A
		public bool IsScrolling { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00066653 File Offset: 0x00064853
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0006665B File Offset: 0x0006485B
		public bool IsTweening { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00066664 File Offset: 0x00064864
		public int StartCellViewIndex
		{
			get
			{
				return this._activeCellViewsStartIndex;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0006666C File Offset: 0x0006486C
		public int EndCellViewIndex
		{
			get
			{
				return this._activeCellViewsEndIndex;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00066674 File Offset: 0x00064874
		public int StartDataIndex
		{
			get
			{
				return this._activeCellViewsStartIndex % this.NumberOfCells;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00066683 File Offset: 0x00064883
		public int EndDataIndex
		{
			get
			{
				return this._activeCellViewsEndIndex % this.NumberOfCells;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00066692 File Offset: 0x00064892
		public int NumberOfCells
		{
			get
			{
				if (this._delegate == null)
				{
					return 0;
				}
				return this._delegate.GetNumberOfCells(this);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x000666AA File Offset: 0x000648AA
		public ScrollRect ScrollRect
		{
			get
			{
				return this._scrollRect;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000666B4 File Offset: 0x000648B4
		public float ScrollRectSize
		{
			get
			{
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					return this._scrollRectTransform.rect.height;
				}
				return this._scrollRectTransform.rect.width;
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000666F0 File Offset: 0x000648F0
		public EnhancedScrollerCellView GetCellView(EnhancedScrollerCellView cellPrefab)
		{
			EnhancedScrollerCellView enhancedScrollerCellView = this._GetRecycledCellView(cellPrefab);
			if (enhancedScrollerCellView == null)
			{
				enhancedScrollerCellView = global::UnityEngine.Object.Instantiate<GameObject>(cellPrefab.gameObject).GetComponent<EnhancedScrollerCellView>();
				enhancedScrollerCellView.transform.SetParent(this._container);
				enhancedScrollerCellView.transform.localPosition = Vector3.zero;
				enhancedScrollerCellView.transform.localRotation = Quaternion.identity;
			}
			return enhancedScrollerCellView;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00066754 File Offset: 0x00064954
		public void ReloadData(float scrollPositionFactor = 0f)
		{
			this._reloadData = false;
			this._RecycleAllCells();
			if (this._delegate != null)
			{
				this._Resize(true);
			}
			if (scrollPositionFactor != 0f)
			{
				this._scrollPosition = scrollPositionFactor * this._ScrollSize;
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					this._scrollRect.verticalNormalizedPosition = 1f - scrollPositionFactor;
					return;
				}
				this._scrollRect.horizontalNormalizedPosition = scrollPositionFactor;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000667BC File Offset: 0x000649BC
		public void RefreshActiveCellViews()
		{
			for (int i = 0; i < this._activeCellViews.Count; i++)
			{
				this._activeCellViews[i].RefreshCellView();
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000667F0 File Offset: 0x000649F0
		public void ClearAll()
		{
			this.ClearActive();
			this.ClearRecycled();
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00066800 File Offset: 0x00064A00
		public void ClearActive()
		{
			for (int i = 0; i < this._activeCellViews.Count; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(this._activeCellViews[i].gameObject);
			}
			this._activeCellViews.Clear();
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00066844 File Offset: 0x00064A44
		public void ClearRecycled()
		{
			for (int i = 0; i < this._recycledCellViews.Count; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(this._recycledCellViews[i].gameObject);
			}
			this._recycledCellViews.Clear();
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00066888 File Offset: 0x00064A88
		public void ToggleLoop()
		{
			this.Loop = !this.loop;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0006689C File Offset: 0x00064A9C
		public void JumpToDataIndex(int dataIndex, float scrollerOffset = 0f, float cellOffset = 0f, bool useSpacing = true, EnhancedScroller.TweenType tweenType = EnhancedScroller.TweenType.immediate, float tweenTime = 0f, Action jumpComplete = null)
		{
			float num = 0f;
			if (cellOffset != 0f)
			{
				float num2 = ((this._delegate != null) ? this._delegate.GetCellViewSize(this, dataIndex) : 0f);
				if (useSpacing)
				{
					num2 += this.spacing;
					if (dataIndex > 0 && dataIndex < this.NumberOfCells - 1)
					{
						num2 += this.spacing;
					}
				}
				num = num2 * cellOffset;
			}
			float num3 = -(scrollerOffset * this.ScrollRectSize) + num;
			float num10;
			if (this.loop)
			{
				float num4 = this.GetScrollPositionForCellViewIndex(dataIndex, EnhancedScroller.CellViewPositionEnum.Before) + num3;
				float num5 = this.GetScrollPositionForCellViewIndex(dataIndex + this.NumberOfCells, EnhancedScroller.CellViewPositionEnum.Before) + num3;
				float num6 = this.GetScrollPositionForCellViewIndex(dataIndex + this.NumberOfCells * 2, EnhancedScroller.CellViewPositionEnum.Before) + num3;
				float num7 = Mathf.Abs(this._scrollPosition - num4);
				float num8 = Mathf.Abs(this._scrollPosition - num5);
				float num9 = Mathf.Abs(this._scrollPosition - num6);
				if (num7 < num8)
				{
					if (num7 < num9)
					{
						num10 = num4;
					}
					else
					{
						num10 = num6;
					}
				}
				else if (num8 < num9)
				{
					num10 = num5;
				}
				else
				{
					num10 = num6;
				}
			}
			else
			{
				num10 = this.GetScrollPositionForDataIndex(dataIndex, EnhancedScroller.CellViewPositionEnum.Before) + num3;
			}
			num10 = Mathf.Clamp(num10, 0f, this.GetScrollPositionForCellViewIndex(this._cellViewSizeArray.Count - 1, EnhancedScroller.CellViewPositionEnum.Before));
			if (useSpacing)
			{
				num10 = Mathf.Clamp(num10 - this.spacing, 0f, this.GetScrollPositionForCellViewIndex(this._cellViewSizeArray.Count - 1, EnhancedScroller.CellViewPositionEnum.Before));
			}
			base.StartCoroutine(this.TweenPosition(tweenType, tweenTime, this.ScrollPosition, num10, jumpComplete));
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00066A14 File Offset: 0x00064C14
		[Obsolete("This is an obsolete method, please use the version of this function with a cell offset.")]
		public void JumpToDataIndex(int dataIndex, EnhancedScroller.CellViewPositionEnum position = EnhancedScroller.CellViewPositionEnum.Before, bool useSpacing = true)
		{
			this.ScrollPosition = this.GetScrollPositionForDataIndex(dataIndex, position);
			if (useSpacing)
			{
				if (position == EnhancedScroller.CellViewPositionEnum.Before)
				{
					this.ScrollPosition = this._scrollPosition - this.spacing;
					return;
				}
				this.ScrollPosition = this._scrollPosition + this.spacing;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00066A54 File Offset: 0x00064C54
		public void Snap()
		{
			if (this.NumberOfCells == 0)
			{
				return;
			}
			this._snapJumping = true;
			this.LinearVelocity = 0f;
			this._snapInertia = this._scrollRect.inertia;
			this._scrollRect.inertia = false;
			float num = this.ScrollPosition + this.ScrollRectSize * Mathf.Clamp01(this.snapWatchOffset);
			this._snapCellViewIndex = this.GetCellViewIndexAtPosition(num);
			this._snapDataIndex = this._snapCellViewIndex % this.NumberOfCells;
			this.JumpToDataIndex(this._snapDataIndex, this.snapJumpToOffset, this.snapCellCenterOffset, this.snapUseCellSpacing, this.snapTweenType, this.snapTweenTime, new Action(this.SnapJumpComplete));
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00066B0C File Offset: 0x00064D0C
		public float GetScrollPositionForCellViewIndex(int cellViewIndex, EnhancedScroller.CellViewPositionEnum insertPosition)
		{
			if (this.NumberOfCells == 0)
			{
				return 0f;
			}
			if (cellViewIndex == 0 && insertPosition == EnhancedScroller.CellViewPositionEnum.Before)
			{
				return 0f;
			}
			if (cellViewIndex >= this._cellViewOffsetArray.Count)
			{
				return this._cellViewOffsetArray[this._cellViewOffsetArray.Count - 2];
			}
			if (insertPosition == EnhancedScroller.CellViewPositionEnum.Before)
			{
				return this._cellViewOffsetArray[cellViewIndex - 1] + this.spacing + (float)((this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical) ? this.padding.top : this.padding.left);
			}
			return this._cellViewOffsetArray[cellViewIndex] + (float)((this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical) ? this.padding.top : this.padding.left);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00066BC3 File Offset: 0x00064DC3
		public float GetScrollPositionForDataIndex(int dataIndex, EnhancedScroller.CellViewPositionEnum insertPosition)
		{
			return this.GetScrollPositionForCellViewIndex(this.loop ? (this._delegate.GetNumberOfCells(this) + dataIndex) : dataIndex, insertPosition);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00066BE5 File Offset: 0x00064DE5
		public int GetCellViewIndexAtPosition(float position)
		{
			return this._GetCellIndexAtPosition(position, 0, this._cellViewOffsetArray.Count - 1);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00066BFC File Offset: 0x00064DFC
		private float _ScrollSize
		{
			get
			{
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					return this._container.rect.height - this._scrollRectTransform.rect.height;
				}
				return this._container.rect.width - this._scrollRectTransform.rect.width;
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00066C60 File Offset: 0x00064E60
		private void _Resize(bool keepPosition)
		{
			float scrollPosition = this._scrollPosition;
			this._cellViewSizeArray.Clear();
			float num = this._AddCellViewSizes();
			if (this.loop)
			{
				if (num < this.ScrollRectSize)
				{
					int num2 = Mathf.CeilToInt(this.ScrollRectSize / num);
					this._DuplicateCellViewSizes(num2, this._cellViewSizeArray.Count);
				}
				this._loopFirstCellIndex = this._cellViewSizeArray.Count;
				this._loopLastCellIndex = this._loopFirstCellIndex + this._cellViewSizeArray.Count - 1;
				this._DuplicateCellViewSizes(2, this._cellViewSizeArray.Count);
			}
			this._CalculateCellViewOffsets();
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._container.sizeDelta = new Vector2(this._container.sizeDelta.x, this._cellViewOffsetArray.Last() + (float)this.padding.top + (float)this.padding.bottom);
			}
			else
			{
				this._container.sizeDelta = new Vector2(this._cellViewOffsetArray.Last() + (float)this.padding.left + (float)this.padding.right, this._container.sizeDelta.y);
			}
			if (this.loop)
			{
				this._loopFirstScrollPosition = this.GetScrollPositionForCellViewIndex(this._loopFirstCellIndex, EnhancedScroller.CellViewPositionEnum.Before) + this.spacing * 0.5f;
				this._loopLastScrollPosition = this.GetScrollPositionForCellViewIndex(this._loopLastCellIndex, EnhancedScroller.CellViewPositionEnum.After) - this.ScrollRectSize + this.spacing * 0.5f;
				this._loopFirstJumpTrigger = this._loopFirstScrollPosition - this.ScrollRectSize;
				this._loopLastJumpTrigger = this._loopLastScrollPosition + this.ScrollRectSize;
			}
			this._ResetVisibleCellViews();
			if (keepPosition)
			{
				this.ScrollPosition = scrollPosition;
			}
			else if (this.loop)
			{
				this.ScrollPosition = this._loopFirstScrollPosition;
			}
			else
			{
				this.ScrollPosition = 0f;
			}
			this.ScrollbarVisibility = this.scrollbarVisibility;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00066E44 File Offset: 0x00065044
		private float _AddCellViewSizes()
		{
			float num = 0f;
			for (int i = 0; i < this.NumberOfCells; i++)
			{
				this._cellViewSizeArray.Add(this._delegate.GetCellViewSize(this, i) + ((i == 0) ? 0f : this._layoutGroup.spacing));
				num += this._cellViewSizeArray[this._cellViewSizeArray.Count - 1];
			}
			return num;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00066EB4 File Offset: 0x000650B4
		private void _DuplicateCellViewSizes(int numberOfTimes, int cellCount)
		{
			for (int i = 0; i < numberOfTimes; i++)
			{
				for (int j = 0; j < cellCount; j++)
				{
					this._cellViewSizeArray.Add(this._cellViewSizeArray[j] + ((j == 0) ? this._layoutGroup.spacing : 0f));
				}
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00066F08 File Offset: 0x00065108
		private void _CalculateCellViewOffsets()
		{
			this._cellViewOffsetArray.Clear();
			float num = 0f;
			for (int i = 0; i < this._cellViewSizeArray.Count; i++)
			{
				num += this._cellViewSizeArray[i];
				this._cellViewOffsetArray.Add(num);
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00066F58 File Offset: 0x00065158
		private EnhancedScrollerCellView _GetRecycledCellView(EnhancedScrollerCellView cellPrefab)
		{
			for (int i = 0; i < this._recycledCellViews.Count; i++)
			{
				if (this._recycledCellViews[i].cellIdentifier == cellPrefab.cellIdentifier)
				{
					return this._recycledCellViews.RemoveAt(i);
				}
			}
			return null;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00066FA8 File Offset: 0x000651A8
		private void _ResetVisibleCellViews()
		{
			int num;
			int num2;
			this._CalculateCurrentActiveCellRange(out num, out num2);
			int i = 0;
			SmallList<int> smallList = new SmallList<int>();
			while (i < this._activeCellViews.Count)
			{
				if (this._activeCellViews[i].cellIndex < num || this._activeCellViews[i].cellIndex > num2)
				{
					this._RecycleCell(this._activeCellViews[i]);
				}
				else
				{
					smallList.Add(this._activeCellViews[i].cellIndex);
					i++;
				}
			}
			if (smallList.Count == 0)
			{
				for (i = num; i <= num2; i++)
				{
					this._AddCellView(i, EnhancedScroller.ListPositionEnum.Last);
				}
			}
			else
			{
				for (i = num2; i >= num; i--)
				{
					if (i < smallList.First())
					{
						this._AddCellView(i, EnhancedScroller.ListPositionEnum.First);
					}
				}
				for (i = num; i <= num2; i++)
				{
					if (i > smallList.Last())
					{
						this._AddCellView(i, EnhancedScroller.ListPositionEnum.Last);
					}
				}
			}
			this._activeCellViewsStartIndex = num;
			this._activeCellViewsEndIndex = num2;
			this._SetPadders();
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0006709A File Offset: 0x0006529A
		private void _RecycleAllCells()
		{
			while (this._activeCellViews.Count > 0)
			{
				this._RecycleCell(this._activeCellViews[0]);
			}
			this._activeCellViewsStartIndex = 0;
			this._activeCellViewsEndIndex = 0;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000670CC File Offset: 0x000652CC
		private void _RecycleCell(EnhancedScrollerCellView cellView)
		{
			if (this.cellViewWillRecycle != null)
			{
				this.cellViewWillRecycle(cellView);
			}
			this._activeCellViews.Remove(cellView);
			this._recycledCellViews.Add(cellView);
			cellView.transform.SetParent(this._recycledCellViewContainer);
			cellView.dataIndex = 0;
			cellView.cellIndex = 0;
			cellView.active = false;
			if (this.cellViewVisibilityChanged != null)
			{
				this.cellViewVisibilityChanged(cellView);
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00067140 File Offset: 0x00065340
		private void _AddCellView(int cellIndex, EnhancedScroller.ListPositionEnum listPosition)
		{
			if (this.NumberOfCells == 0)
			{
				return;
			}
			int num = cellIndex % this.NumberOfCells;
			EnhancedScrollerCellView cellView = this._delegate.GetCellView(this, num, cellIndex);
			cellView.cellIndex = cellIndex;
			cellView.dataIndex = num;
			cellView.active = true;
			cellView.transform.SetParent(this._container, false);
			cellView.transform.localScale = Vector3.one;
			LayoutElement layoutElement = cellView.GetComponent<LayoutElement>();
			if (layoutElement == null)
			{
				layoutElement = cellView.gameObject.AddComponent<LayoutElement>();
			}
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				layoutElement.minHeight = this._cellViewSizeArray[cellIndex] - ((cellIndex > 0) ? this._layoutGroup.spacing : 0f);
			}
			else
			{
				layoutElement.minWidth = this._cellViewSizeArray[cellIndex] - ((cellIndex > 0) ? this._layoutGroup.spacing : 0f);
			}
			if (listPosition == EnhancedScroller.ListPositionEnum.First)
			{
				this._activeCellViews.AddStart(cellView);
			}
			else
			{
				this._activeCellViews.Add(cellView);
			}
			if (listPosition == EnhancedScroller.ListPositionEnum.Last)
			{
				cellView.transform.SetSiblingIndex(this._container.childCount - 2);
			}
			else if (listPosition == EnhancedScroller.ListPositionEnum.First)
			{
				cellView.transform.SetSiblingIndex(1);
			}
			if (this.cellViewVisibilityChanged != null)
			{
				this.cellViewVisibilityChanged(cellView);
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0006727C File Offset: 0x0006547C
		private void _SetPadders()
		{
			if (this.NumberOfCells == 0)
			{
				return;
			}
			float num = this._cellViewOffsetArray[this._activeCellViewsStartIndex] - this._cellViewSizeArray[this._activeCellViewsStartIndex];
			float num2 = this._cellViewOffsetArray.Last() - this._cellViewOffsetArray[this._activeCellViewsEndIndex];
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._firstPadder.minHeight = num;
				this._firstPadder.gameObject.SetActive(this._firstPadder.minHeight > 0f);
				this._lastPadder.minHeight = num2;
				this._lastPadder.gameObject.SetActive(this._lastPadder.minHeight > 0f);
				return;
			}
			this._firstPadder.minWidth = num;
			this._firstPadder.gameObject.SetActive(this._firstPadder.minWidth > 0f);
			this._lastPadder.minWidth = num2;
			this._lastPadder.gameObject.SetActive(this._lastPadder.minWidth > 0f);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00067398 File Offset: 0x00065598
		private void _RefreshActive()
		{
			this._refreshActive = false;
			Vector2 vector = Vector2.zero;
			if (this.loop)
			{
				if (this._scrollPosition < this._loopFirstJumpTrigger)
				{
					vector = this._scrollRect.velocity;
					this.ScrollPosition = this._loopLastScrollPosition - (this._loopFirstJumpTrigger - this._scrollPosition);
					this._scrollRect.velocity = vector;
				}
				else if (this._scrollPosition > this._loopLastJumpTrigger)
				{
					vector = this._scrollRect.velocity;
					this.ScrollPosition = this._loopFirstScrollPosition + (this._scrollPosition - this._loopLastJumpTrigger);
					this._scrollRect.velocity = vector;
				}
			}
			int num;
			int num2;
			this._CalculateCurrentActiveCellRange(out num, out num2);
			if (num == this._activeCellViewsStartIndex && num2 == this._activeCellViewsEndIndex)
			{
				return;
			}
			this._ResetVisibleCellViews();
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00067464 File Offset: 0x00065664
		private void _CalculateCurrentActiveCellRange(out int startIndex, out int endIndex)
		{
			startIndex = 0;
			endIndex = 0;
			float scrollPosition = this._scrollPosition;
			float num = this._scrollPosition + ((this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical) ? this._scrollRectTransform.rect.height : this._scrollRectTransform.rect.width);
			startIndex = this.GetCellViewIndexAtPosition(scrollPosition);
			endIndex = this.GetCellViewIndexAtPosition(num);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000674C8 File Offset: 0x000656C8
		private int _GetCellIndexAtPosition(float position, int startIndex, int endIndex)
		{
			if (startIndex >= endIndex)
			{
				return startIndex;
			}
			int num = (startIndex + endIndex) / 2;
			if (this._cellViewOffsetArray[num] + (float)((this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical) ? this.padding.top : this.padding.left) >= position)
			{
				return this._GetCellIndexAtPosition(position, startIndex, num);
			}
			return this._GetCellIndexAtPosition(position, num + 1, endIndex);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00067528 File Offset: 0x00065728
		private void Awake()
		{
			this._scrollRect = base.GetComponent<ScrollRect>();
			this._scrollRectTransform = this._scrollRect.GetComponent<RectTransform>();
			if (this._scrollRect.content != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this._scrollRect.content.gameObject);
			}
			GameObject gameObject = new GameObject("Container", new Type[] { typeof(RectTransform) });
			gameObject.transform.SetParent(this._scrollRectTransform, false);
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				gameObject.AddComponent<VerticalLayoutGroup>();
			}
			else
			{
				gameObject.AddComponent<HorizontalLayoutGroup>();
			}
			this._container = gameObject.GetComponent<RectTransform>();
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._container.anchorMin = new Vector2(0f, 1f);
				this._container.anchorMax = Vector2.one;
				this._container.pivot = new Vector2(0f, 1f);
			}
			else
			{
				this._container.anchorMin = Vector2.zero;
				this._container.anchorMax = new Vector2(0f, 1f);
				this._container.pivot = new Vector2(0f, 0.5f);
			}
			this._container.anchoredPosition = Vector2.zero;
			this._container.offsetMax = Vector2.zero;
			this._container.offsetMin = Vector2.zero;
			this._container.localPosition = Vector3.zero;
			this._container.localRotation = Quaternion.identity;
			this._container.localScale = Vector3.one;
			this._scrollRect.content = this._container;
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._scrollbar = this._scrollRect.verticalScrollbar;
			}
			else
			{
				this._scrollbar = this._scrollRect.horizontalScrollbar;
			}
			this._layoutGroup = this._container.GetComponent<HorizontalOrVerticalLayoutGroup>();
			this._layoutGroup.spacing = this.spacing;
			this._layoutGroup.padding = this.padding;
			this._layoutGroup.childAlignment = TextAnchor.UpperLeft;
			this._layoutGroup.childForceExpandHeight = true;
			this._layoutGroup.childForceExpandWidth = true;
			this._scrollRect.horizontal = this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Horizontal;
			this._scrollRect.vertical = this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical;
			gameObject = new GameObject("First Padder", new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			gameObject.transform.SetParent(this._container, false);
			this._firstPadder = gameObject.GetComponent<LayoutElement>();
			gameObject = new GameObject("Last Padder", new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			gameObject.transform.SetParent(this._container, false);
			this._lastPadder = gameObject.GetComponent<LayoutElement>();
			gameObject = new GameObject("Recycled Cells", new Type[] { typeof(RectTransform) });
			gameObject.transform.SetParent(this._scrollRect.transform, false);
			this._recycledCellViewContainer = gameObject.GetComponent<RectTransform>();
			this._recycledCellViewContainer.gameObject.SetActive(false);
			this._lastScrollRectSize = this.ScrollRectSize;
			this._lastLoop = this.loop;
			this._lastScrollbarVisibility = this.scrollbarVisibility;
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._container.anchorMin = new Vector2(0f, 1f);
				this._container.anchorMax = Vector2.one;
				this._container.pivot = new Vector2(0f, 1f);
			}
			else
			{
				this._container.anchorMin = Vector2.zero;
				this._container.anchorMax = new Vector2(0f, 1f);
				this._container.pivot = new Vector2(0f, 0.5f);
			}
			this._container.offsetMax = Vector2.zero;
			this._container.offsetMin = Vector2.zero;
			this._container.localPosition = Vector3.zero;
			this._container.localRotation = Quaternion.identity;
			this._container.localScale = Vector3.one;
			this._container.anchoredPosition = Vector2.zero;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00067978 File Offset: 0x00065B78
		private void Update()
		{
			if (this._reloadData)
			{
				this.ReloadData(0f);
			}
			if ((this.loop && this._lastScrollRectSize != this.ScrollRectSize) || this.loop != this._lastLoop)
			{
				this._Resize(true);
				this._lastScrollRectSize = this.ScrollRectSize;
				this._lastLoop = this.loop;
			}
			if (this._lastScrollbarVisibility != this.scrollbarVisibility)
			{
				this.ScrollbarVisibility = this.scrollbarVisibility;
				this._lastScrollbarVisibility = this.scrollbarVisibility;
			}
			if (this.LinearVelocity != 0f && !this.IsScrolling)
			{
				this.IsScrolling = true;
				if (this.scrollerScrollingChanged != null)
				{
					this.scrollerScrollingChanged(this, true);
					return;
				}
			}
			else if (this.LinearVelocity == 0f && this.IsScrolling)
			{
				this.IsScrolling = false;
				if (this.scrollerScrollingChanged != null)
				{
					this.scrollerScrollingChanged(this, false);
				}
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00067A64 File Offset: 0x00065C64
		private void LateUpdate()
		{
			if (this._refreshActive)
			{
				this._RefreshActive();
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00067A74 File Offset: 0x00065C74
		private void OnEnable()
		{
			this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this._ScrollRect_OnValueChanged));
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00067A92 File Offset: 0x00065C92
		private void OnDisable()
		{
			this._scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this._ScrollRect_OnValueChanged));
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00067AB0 File Offset: 0x00065CB0
		private void _ScrollRect_OnValueChanged(Vector2 val)
		{
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._scrollPosition = (1f - val.y) * this._ScrollSize;
			}
			else
			{
				this._scrollPosition = val.x * this._ScrollSize;
			}
			this._refreshActive = true;
			if (this.scrollerScrolled != null)
			{
				this.scrollerScrolled(this, val, this._scrollPosition);
			}
			if (this.snapping && !this._snapJumping && Mathf.Abs(this.LinearVelocity) <= this.snapVelocityThreshold)
			{
				this.Snap();
			}
			this._RefreshActive();
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00067B44 File Offset: 0x00065D44
		private void SnapJumpComplete()
		{
			this._snapJumping = false;
			this._scrollRect.inertia = this._snapInertia;
			if (this.scrollerSnapped != null)
			{
				this.scrollerSnapped(this, this._snapCellViewIndex, this._snapDataIndex);
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00067B7E File Offset: 0x00065D7E
		private IEnumerator TweenPosition(EnhancedScroller.TweenType tweenType, float time, float start, float end, Action tweenComplete)
		{
			if (tweenType == EnhancedScroller.TweenType.immediate || time == 0f)
			{
				this.ScrollPosition = end;
			}
			else
			{
				this._scrollRect.velocity = Vector2.zero;
				this.IsTweening = true;
				if (this.scrollerTweeningChanged != null)
				{
					this.scrollerTweeningChanged(this, true);
				}
				this._tweenTimeLeft = 0f;
				float newPosition = 0f;
				while (this._tweenTimeLeft < time)
				{
					switch (tweenType)
					{
					case EnhancedScroller.TweenType.linear:
						newPosition = this.linear(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.spring:
						newPosition = EnhancedScroller.spring(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInQuad:
						newPosition = EnhancedScroller.easeInQuad(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutQuad:
						newPosition = EnhancedScroller.easeOutQuad(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutQuad:
						newPosition = EnhancedScroller.easeInOutQuad(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInCubic:
						newPosition = EnhancedScroller.easeInCubic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutCubic:
						newPosition = EnhancedScroller.easeOutCubic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutCubic:
						newPosition = EnhancedScroller.easeInOutCubic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInQuart:
						newPosition = EnhancedScroller.easeInQuart(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutQuart:
						newPosition = EnhancedScroller.easeOutQuart(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutQuart:
						newPosition = EnhancedScroller.easeInOutQuart(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInQuint:
						newPosition = EnhancedScroller.easeInQuint(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutQuint:
						newPosition = EnhancedScroller.easeOutQuint(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutQuint:
						newPosition = EnhancedScroller.easeInOutQuint(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInSine:
						newPosition = EnhancedScroller.easeInSine(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutSine:
						newPosition = EnhancedScroller.easeOutSine(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutSine:
						newPosition = EnhancedScroller.easeInOutSine(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInExpo:
						newPosition = EnhancedScroller.easeInExpo(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutExpo:
						newPosition = EnhancedScroller.easeOutExpo(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutExpo:
						newPosition = EnhancedScroller.easeInOutExpo(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInCirc:
						newPosition = EnhancedScroller.easeInCirc(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutCirc:
						newPosition = EnhancedScroller.easeOutCirc(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutCirc:
						newPosition = EnhancedScroller.easeInOutCirc(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInBounce:
						newPosition = EnhancedScroller.easeInBounce(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutBounce:
						newPosition = EnhancedScroller.easeOutBounce(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutBounce:
						newPosition = EnhancedScroller.easeInOutBounce(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInBack:
						newPosition = EnhancedScroller.easeInBack(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutBack:
						newPosition = EnhancedScroller.easeOutBack(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutBack:
						newPosition = EnhancedScroller.easeInOutBack(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInElastic:
						newPosition = EnhancedScroller.easeInElastic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutElastic:
						newPosition = EnhancedScroller.easeOutElastic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutElastic:
						newPosition = EnhancedScroller.easeInOutElastic(start, end, this._tweenTimeLeft / time);
						break;
					}
					if (this.loop)
					{
						if (end > start && newPosition > this._loopLastJumpTrigger)
						{
							newPosition = this._loopFirstScrollPosition + (newPosition - this._loopLastJumpTrigger);
						}
						else if (start > end && newPosition < this._loopFirstJumpTrigger)
						{
							newPosition = this._loopLastScrollPosition - (this._loopFirstJumpTrigger - newPosition);
						}
					}
					this.ScrollPosition = newPosition;
					this._tweenTimeLeft += Time.unscaledDeltaTime;
					yield return null;
				}
				this.ScrollPosition = end;
			}
			if (tweenComplete != null)
			{
				tweenComplete();
			}
			this.IsTweening = false;
			if (this.scrollerTweeningChanged != null)
			{
				this.scrollerTweeningChanged(this, false);
			}
			yield break;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00067BB2 File Offset: 0x00065DB2
		private float linear(float start, float end, float val)
		{
			return Mathf.Lerp(start, end, val);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00067BBC File Offset: 0x00065DBC
		private static float spring(float start, float end, float val)
		{
			val = Mathf.Clamp01(val);
			val = (Mathf.Sin(val * 3.1415927f * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
			return start + (end - start) * val;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00067C20 File Offset: 0x00065E20
		private static float easeInQuad(float start, float end, float val)
		{
			end -= start;
			return end * val * val + start;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00067C2E File Offset: 0x00065E2E
		private static float easeOutQuad(float start, float end, float val)
		{
			end -= start;
			return -end * val * (val - 2f) + start;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00067C44 File Offset: 0x00065E44
		private static float easeInOutQuad(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val + start;
			}
			val -= 1f;
			return -end / 2f * (val * (val - 2f) - 1f) + start;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00067C98 File Offset: 0x00065E98
		private static float easeInCubic(float start, float end, float val)
		{
			end -= start;
			return end * val * val * val + start;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00067CA8 File Offset: 0x00065EA8
		private static float easeOutCubic(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return end * (val * val * val + 1f) + start;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00067CC8 File Offset: 0x00065EC8
		private static float easeInOutCubic(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val * val + start;
			}
			val -= 2f;
			return end / 2f * (val * val * val + 2f) + start;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00067D19 File Offset: 0x00065F19
		private static float easeInQuart(float start, float end, float val)
		{
			end -= start;
			return end * val * val * val * val + start;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00067D2B File Offset: 0x00065F2B
		private static float easeOutQuart(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return -end * (val * val * val * val - 1f) + start;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00067D50 File Offset: 0x00065F50
		private static float easeInOutQuart(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val * val * val + start;
			}
			val -= 2f;
			return -end / 2f * (val * val * val * val - 2f) + start;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00067DA6 File Offset: 0x00065FA6
		private static float easeInQuint(float start, float end, float val)
		{
			end -= start;
			return end * val * val * val * val * val + start;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00067DBA File Offset: 0x00065FBA
		private static float easeOutQuint(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return end * (val * val * val * val * val + 1f) + start;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00067DE0 File Offset: 0x00065FE0
		private static float easeInOutQuint(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val * val * val * val + start;
			}
			val -= 2f;
			return end / 2f * (val * val * val * val * val + 2f) + start;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00067E39 File Offset: 0x00066039
		private static float easeInSine(float start, float end, float val)
		{
			end -= start;
			return -end * Mathf.Cos(val / 1f * 1.5707964f) + end + start;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00067E59 File Offset: 0x00066059
		private static float easeOutSine(float start, float end, float val)
		{
			end -= start;
			return end * Mathf.Sin(val / 1f * 1.5707964f) + start;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00067E76 File Offset: 0x00066076
		private static float easeInOutSine(float start, float end, float val)
		{
			end -= start;
			return -end / 2f * (Mathf.Cos(3.1415927f * val / 1f) - 1f) + start;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00067EA0 File Offset: 0x000660A0
		private static float easeInExpo(float start, float end, float val)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (val / 1f - 1f)) + start;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00067EC8 File Offset: 0x000660C8
		private static float easeOutExpo(float start, float end, float val)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * val / 1f) + 1f) + start;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00067EF4 File Offset: 0x000660F4
		private static float easeInOutExpo(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * Mathf.Pow(2f, 10f * (val - 1f)) + start;
			}
			val -= 1f;
			return end / 2f * (-Mathf.Pow(2f, -10f * val) + 2f) + start;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00067F64 File Offset: 0x00066164
		private static float easeInCirc(float start, float end, float val)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - val * val) - 1f) + start;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00067F84 File Offset: 0x00066184
		private static float easeOutCirc(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - val * val) + start;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00067FA8 File Offset: 0x000661A8
		private static float easeInOutCirc(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return -end / 2f * (Mathf.Sqrt(1f - val * val) - 1f) + start;
			}
			val -= 2f;
			return end / 2f * (Mathf.Sqrt(1f - val * val) + 1f) + start;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00068014 File Offset: 0x00066214
		private static float easeInBounce(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			return end - EnhancedScroller.easeOutBounce(0f, end, num - val) + start;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00068040 File Offset: 0x00066240
		private static float easeOutBounce(float start, float end, float val)
		{
			val /= 1f;
			end -= start;
			if (val < 0.36363637f)
			{
				return end * (7.5625f * val * val) + start;
			}
			if (val < 0.72727275f)
			{
				val -= 0.54545456f;
				return end * (7.5625f * val * val + 0.75f) + start;
			}
			if ((double)val < 0.9090909090909091)
			{
				val -= 0.8181818f;
				return end * (7.5625f * val * val + 0.9375f) + start;
			}
			val -= 0.95454544f;
			return end * (7.5625f * val * val + 0.984375f) + start;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000680DC File Offset: 0x000662DC
		private static float easeInOutBounce(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			if (val < num / 2f)
			{
				return EnhancedScroller.easeInBounce(0f, end, val * 2f) * 0.5f + start;
			}
			return EnhancedScroller.easeOutBounce(0f, end, val * 2f - num) * 0.5f + end * 0.5f + start;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00068140 File Offset: 0x00066340
		private static float easeInBack(float start, float end, float val)
		{
			end -= start;
			val /= 1f;
			float num = 1.70158f;
			return end * val * val * ((num + 1f) * val - num) + start;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00068174 File Offset: 0x00066374
		private static float easeOutBack(float start, float end, float val)
		{
			float num = 1.70158f;
			end -= start;
			val = val / 1f - 1f;
			return end * (val * val * ((num + 1f) * val + num) + 1f) + start;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000681B4 File Offset: 0x000663B4
		private static float easeInOutBack(float start, float end, float val)
		{
			float num = 1.70158f;
			end -= start;
			val /= 0.5f;
			if (val < 1f)
			{
				num *= 1.525f;
				return end / 2f * (val * val * ((num + 1f) * val - num)) + start;
			}
			val -= 2f;
			num *= 1.525f;
			return end / 2f * (val * val * ((num + 1f) * val + num) + 2f) + start;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00068230 File Offset: 0x00066430
		private static float easeInElastic(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (val == 0f)
			{
				return start;
			}
			val /= num;
			if (val == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			val -= 1f;
			return -(num3 * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val * num - num4) * 6.2831855f / num2)) + start;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000682D8 File Offset: 0x000664D8
		private static float easeOutElastic(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (val == 0f)
			{
				return start;
			}
			val /= num;
			if (val == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val * num - num4) * 6.2831855f / num2) + end + start;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00068378 File Offset: 0x00066578
		private static float easeInOutElastic(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (val == 0f)
			{
				return start;
			}
			val /= num / 2f;
			if (val == 2f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			if (val < 1f)
			{
				val -= 1f;
				return -0.5f * (num3 * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val * num - num4) * 6.2831855f / num2)) + start;
			}
			val -= 1f;
			return num3 * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val * num - num4) * 6.2831855f / num2) * 0.5f + end + start;
		}

		// Token: 0x04000B22 RID: 2850
		public EnhancedScroller.ScrollDirectionEnum scrollDirection;

		// Token: 0x04000B23 RID: 2851
		public float spacing;

		// Token: 0x04000B24 RID: 2852
		public RectOffset padding;

		// Token: 0x04000B25 RID: 2853
		[SerializeField]
		private bool loop;

		// Token: 0x04000B26 RID: 2854
		[SerializeField]
		private EnhancedScroller.ScrollbarVisibilityEnum scrollbarVisibility;

		// Token: 0x04000B27 RID: 2855
		public bool snapping;

		// Token: 0x04000B28 RID: 2856
		public float snapVelocityThreshold;

		// Token: 0x04000B29 RID: 2857
		public float snapWatchOffset;

		// Token: 0x04000B2A RID: 2858
		public float snapJumpToOffset;

		// Token: 0x04000B2B RID: 2859
		public float snapCellCenterOffset;

		// Token: 0x04000B2C RID: 2860
		public bool snapUseCellSpacing;

		// Token: 0x04000B2D RID: 2861
		public EnhancedScroller.TweenType snapTweenType;

		// Token: 0x04000B2E RID: 2862
		public float snapTweenTime;

		// Token: 0x04000B2F RID: 2863
		public CellViewVisibilityChangedDelegate cellViewVisibilityChanged;

		// Token: 0x04000B30 RID: 2864
		public CellViewWillRecycleDelegate cellViewWillRecycle;

		// Token: 0x04000B31 RID: 2865
		public ScrollerScrolledDelegate scrollerScrolled;

		// Token: 0x04000B32 RID: 2866
		public ScrollerSnappedDelegate scrollerSnapped;

		// Token: 0x04000B33 RID: 2867
		public ScrollerScrollingChangedDelegate scrollerScrollingChanged;

		// Token: 0x04000B34 RID: 2868
		public ScrollerTweeningChangedDelegate scrollerTweeningChanged;

		// Token: 0x04000B37 RID: 2871
		private ScrollRect _scrollRect;

		// Token: 0x04000B38 RID: 2872
		private RectTransform _scrollRectTransform;

		// Token: 0x04000B39 RID: 2873
		private Scrollbar _scrollbar;

		// Token: 0x04000B3A RID: 2874
		private RectTransform _container;

		// Token: 0x04000B3B RID: 2875
		private HorizontalOrVerticalLayoutGroup _layoutGroup;

		// Token: 0x04000B3C RID: 2876
		private IEnhancedScrollerDelegate _delegate;

		// Token: 0x04000B3D RID: 2877
		private bool _reloadData;

		// Token: 0x04000B3E RID: 2878
		private bool _refreshActive;

		// Token: 0x04000B3F RID: 2879
		private SmallList<EnhancedScrollerCellView> _recycledCellViews = new SmallList<EnhancedScrollerCellView>();

		// Token: 0x04000B40 RID: 2880
		private LayoutElement _firstPadder;

		// Token: 0x04000B41 RID: 2881
		private LayoutElement _lastPadder;

		// Token: 0x04000B42 RID: 2882
		private RectTransform _recycledCellViewContainer;

		// Token: 0x04000B43 RID: 2883
		private SmallList<float> _cellViewSizeArray = new SmallList<float>();

		// Token: 0x04000B44 RID: 2884
		private SmallList<float> _cellViewOffsetArray = new SmallList<float>();

		// Token: 0x04000B45 RID: 2885
		private float _scrollPosition;

		// Token: 0x04000B46 RID: 2886
		private SmallList<EnhancedScrollerCellView> _activeCellViews = new SmallList<EnhancedScrollerCellView>();

		// Token: 0x04000B47 RID: 2887
		private int _activeCellViewsStartIndex;

		// Token: 0x04000B48 RID: 2888
		private int _activeCellViewsEndIndex;

		// Token: 0x04000B49 RID: 2889
		private int _loopFirstCellIndex;

		// Token: 0x04000B4A RID: 2890
		private int _loopLastCellIndex;

		// Token: 0x04000B4B RID: 2891
		private float _loopFirstScrollPosition;

		// Token: 0x04000B4C RID: 2892
		private float _loopLastScrollPosition;

		// Token: 0x04000B4D RID: 2893
		private float _loopFirstJumpTrigger;

		// Token: 0x04000B4E RID: 2894
		private float _loopLastJumpTrigger;

		// Token: 0x04000B4F RID: 2895
		private float _lastScrollRectSize;

		// Token: 0x04000B50 RID: 2896
		private bool _lastLoop;

		// Token: 0x04000B51 RID: 2897
		private int _snapCellViewIndex;

		// Token: 0x04000B52 RID: 2898
		private int _snapDataIndex;

		// Token: 0x04000B53 RID: 2899
		private bool _snapJumping;

		// Token: 0x04000B54 RID: 2900
		private bool _snapInertia;

		// Token: 0x04000B55 RID: 2901
		private EnhancedScroller.ScrollbarVisibilityEnum _lastScrollbarVisibility;

		// Token: 0x04000B56 RID: 2902
		private float _tweenTimeLeft;

		// Token: 0x02001634 RID: 5684
		public enum ScrollDirectionEnum
		{
			// Token: 0x04005996 RID: 22934
			Vertical,
			// Token: 0x04005997 RID: 22935
			Horizontal
		}

		// Token: 0x02001635 RID: 5685
		public enum CellViewPositionEnum
		{
			// Token: 0x04005999 RID: 22937
			Before,
			// Token: 0x0400599A RID: 22938
			After
		}

		// Token: 0x02001636 RID: 5686
		public enum ScrollbarVisibilityEnum
		{
			// Token: 0x0400599C RID: 22940
			OnlyIfNeeded,
			// Token: 0x0400599D RID: 22941
			Always,
			// Token: 0x0400599E RID: 22942
			Never
		}

		// Token: 0x02001637 RID: 5687
		private enum ListPositionEnum
		{
			// Token: 0x040059A0 RID: 22944
			First,
			// Token: 0x040059A1 RID: 22945
			Last
		}

		// Token: 0x02001638 RID: 5688
		public enum TweenType
		{
			// Token: 0x040059A3 RID: 22947
			immediate,
			// Token: 0x040059A4 RID: 22948
			linear,
			// Token: 0x040059A5 RID: 22949
			spring,
			// Token: 0x040059A6 RID: 22950
			easeInQuad,
			// Token: 0x040059A7 RID: 22951
			easeOutQuad,
			// Token: 0x040059A8 RID: 22952
			easeInOutQuad,
			// Token: 0x040059A9 RID: 22953
			easeInCubic,
			// Token: 0x040059AA RID: 22954
			easeOutCubic,
			// Token: 0x040059AB RID: 22955
			easeInOutCubic,
			// Token: 0x040059AC RID: 22956
			easeInQuart,
			// Token: 0x040059AD RID: 22957
			easeOutQuart,
			// Token: 0x040059AE RID: 22958
			easeInOutQuart,
			// Token: 0x040059AF RID: 22959
			easeInQuint,
			// Token: 0x040059B0 RID: 22960
			easeOutQuint,
			// Token: 0x040059B1 RID: 22961
			easeInOutQuint,
			// Token: 0x040059B2 RID: 22962
			easeInSine,
			// Token: 0x040059B3 RID: 22963
			easeOutSine,
			// Token: 0x040059B4 RID: 22964
			easeInOutSine,
			// Token: 0x040059B5 RID: 22965
			easeInExpo,
			// Token: 0x040059B6 RID: 22966
			easeOutExpo,
			// Token: 0x040059B7 RID: 22967
			easeInOutExpo,
			// Token: 0x040059B8 RID: 22968
			easeInCirc,
			// Token: 0x040059B9 RID: 22969
			easeOutCirc,
			// Token: 0x040059BA RID: 22970
			easeInOutCirc,
			// Token: 0x040059BB RID: 22971
			easeInBounce,
			// Token: 0x040059BC RID: 22972
			easeOutBounce,
			// Token: 0x040059BD RID: 22973
			easeInOutBounce,
			// Token: 0x040059BE RID: 22974
			easeInBack,
			// Token: 0x040059BF RID: 22975
			easeOutBack,
			// Token: 0x040059C0 RID: 22976
			easeInOutBack,
			// Token: 0x040059C1 RID: 22977
			easeInElastic,
			// Token: 0x040059C2 RID: 22978
			easeOutElastic,
			// Token: 0x040059C3 RID: 22979
			easeInOutElastic
		}
	}
}
