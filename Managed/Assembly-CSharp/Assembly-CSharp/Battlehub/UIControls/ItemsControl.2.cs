using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003CD RID: 973
	public abstract class ItemsControl : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDropHandler
	{
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06002412 RID: 9234 RVA: 0x000B5D84 File Offset: 0x000B3F84
		// (remove) Token: 0x06002413 RID: 9235 RVA: 0x000B5DBC File Offset: 0x000B3FBC
		public event EventHandler<ItemArgs> ItemBeginDrag;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06002414 RID: 9236 RVA: 0x000B5DF4 File Offset: 0x000B3FF4
		// (remove) Token: 0x06002415 RID: 9237 RVA: 0x000B5E2C File Offset: 0x000B402C
		public event EventHandler<ItemDropCancelArgs> ItemBeginDrop;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06002416 RID: 9238 RVA: 0x000B5E64 File Offset: 0x000B4064
		// (remove) Token: 0x06002417 RID: 9239 RVA: 0x000B5E9C File Offset: 0x000B409C
		public event EventHandler<ItemDropArgs> ItemDrop;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06002418 RID: 9240 RVA: 0x000B5ED4 File Offset: 0x000B40D4
		// (remove) Token: 0x06002419 RID: 9241 RVA: 0x000B5F0C File Offset: 0x000B410C
		public event EventHandler<ItemArgs> ItemEndDrag;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600241A RID: 9242 RVA: 0x000B5F44 File Offset: 0x000B4144
		// (remove) Token: 0x0600241B RID: 9243 RVA: 0x000B5F7C File Offset: 0x000B417C
		public event EventHandler<SelectionChangedArgs> SelectionChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600241C RID: 9244 RVA: 0x000B5FB4 File Offset: 0x000B41B4
		// (remove) Token: 0x0600241D RID: 9245 RVA: 0x000B5FEC File Offset: 0x000B41EC
		public event EventHandler<ItemArgs> ItemDoubleClick;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600241E RID: 9246 RVA: 0x000B6024 File Offset: 0x000B4224
		// (remove) Token: 0x0600241F RID: 9247 RVA: 0x000B605C File Offset: 0x000B425C
		public event EventHandler<ItemsCancelArgs> ItemsRemoving;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06002420 RID: 9248 RVA: 0x000B6094 File Offset: 0x000B4294
		// (remove) Token: 0x06002421 RID: 9249 RVA: 0x000B60CC File Offset: 0x000B42CC
		public event EventHandler<ItemsRemovedArgs> ItemsRemoved;

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000B6101 File Offset: 0x000B4301
		protected virtual bool CanScroll
		{
			get
			{
				return this.CanReorder;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000B6109 File Offset: 0x000B4309
		protected bool IsDropInProgress
		{
			get
			{
				return this.m_isDropInProgress;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x000B6111 File Offset: 0x000B4311
		public object DropTarget
		{
			get
			{
				if (this.m_dropTarget == null)
				{
					return null;
				}
				return this.m_dropTarget.Item;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x000B612E File Offset: 0x000B432E
		public ItemDropAction DropAction
		{
			get
			{
				if (this.m_dropMarker == null)
				{
					return ItemDropAction.None;
				}
				return this.m_dropMarker.Action;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x000B614B File Offset: 0x000B434B
		protected ItemDropMarker DropMarker
		{
			get
			{
				return this.m_dropMarker;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000B6153 File Offset: 0x000B4353
		// (set) Token: 0x06002428 RID: 9256 RVA: 0x000B615C File Offset: 0x000B435C
		public IEnumerable Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				if (value == null)
				{
					this.m_items = null;
					this.m_scrollRect.verticalNormalizedPosition = 1f;
					this.m_scrollRect.horizontalNormalizedPosition = 0f;
				}
				else
				{
					this.m_items = value.OfType<object>().ToList<object>();
				}
				this.DataBind();
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000B61AC File Offset: 0x000B43AC
		public int ItemsCount
		{
			get
			{
				if (this.m_items == null)
				{
					return 0;
				}
				return this.m_items.Count;
			}
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000B61C3 File Offset: 0x000B43C3
		protected void RemoveItemAt(int index)
		{
			this.m_items.RemoveAt(index);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000B61D1 File Offset: 0x000B43D1
		protected void RemoveItemContainerAt(int index)
		{
			this.m_itemContainers.RemoveAt(index);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000B61DF File Offset: 0x000B43DF
		protected void InsertItem(int index, object value)
		{
			this.m_items.Insert(index, value);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000B61EE File Offset: 0x000B43EE
		protected void InsertItemContainerAt(int index, ItemContainer container)
		{
			this.m_itemContainers.Insert(index, container);
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x000B61FD File Offset: 0x000B43FD
		public int SelectedItemsCount
		{
			get
			{
				if (this.m_selectedItems == null)
				{
					return 0;
				}
				return this.m_selectedItems.Count;
			}
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000B6214 File Offset: 0x000B4414
		public bool IsItemSelected(object obj)
		{
			return this.m_selectedItemsHS != null && this.m_selectedItemsHS.Contains(obj);
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000B622C File Offset: 0x000B442C
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x000B6234 File Offset: 0x000B4434
		public virtual IEnumerable SelectedItems
		{
			get
			{
				return this.m_selectedItems;
			}
			set
			{
				if (this.m_selectionLocked)
				{
					return;
				}
				this.m_selectionLocked = true;
				IList selectedItems = this.m_selectedItems;
				if (value != null)
				{
					this.m_selectedItems = value.OfType<object>().ToList<object>();
					this.m_selectedItemsHS = new HashSet<object>(this.m_selectedItems);
					for (int i = this.m_selectedItems.Count - 1; i >= 0; i--)
					{
						object obj = this.m_selectedItems[i];
						ItemContainer itemContainer = this.GetItemContainer(obj);
						if (itemContainer != null)
						{
							itemContainer.IsSelected = true;
						}
					}
					if (this.m_selectedItems.Count == 0)
					{
						this.m_selectedItemContainer = null;
						this.m_selectedIndex = -1;
					}
					else
					{
						this.m_selectedItemContainer = this.GetItemContainer(this.m_selectedItems[0]);
						this.m_selectedIndex = this.IndexOf(this.m_selectedItems[0]);
					}
				}
				else
				{
					this.m_selectedItems = null;
					this.m_selectedItemsHS = null;
					this.m_selectedItemContainer = null;
					this.m_selectedIndex = -1;
				}
				List<object> list = new List<object>();
				if (selectedItems != null)
				{
					for (int j = 0; j < selectedItems.Count; j++)
					{
						object obj2 = selectedItems[j];
						if (this.m_selectedItemsHS == null || !this.m_selectedItemsHS.Contains(obj2))
						{
							list.Add(obj2);
							ItemContainer itemContainer2 = this.GetItemContainer(obj2);
							if (itemContainer2 != null)
							{
								itemContainer2.IsSelected = false;
							}
						}
					}
				}
				if (this.SelectionChanged != null)
				{
					object[] array = ((this.m_selectedItems == null) ? new object[0] : this.m_selectedItems.ToArray());
					this.SelectionChanged(this, new SelectionChangedArgs(list.ToArray(), array));
				}
				this.m_selectionLocked = false;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x000B63D3 File Offset: 0x000B45D3
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x000B63F8 File Offset: 0x000B45F8
		public object SelectedItem
		{
			get
			{
				if (this.m_selectedItems == null || this.m_selectedItems.Count == 0)
				{
					return null;
				}
				return this.m_selectedItems[0];
			}
			set
			{
				this.SelectedIndex = this.IndexOf(value);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x000B6407 File Offset: 0x000B4607
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x000B641C File Offset: 0x000B461C
		public int SelectedIndex
		{
			get
			{
				if (this.SelectedItem == null)
				{
					return -1;
				}
				return this.m_selectedIndex;
			}
			set
			{
				if (this.m_selectedIndex == value)
				{
					return;
				}
				if (this.m_selectionLocked)
				{
					return;
				}
				this.m_selectionLocked = true;
				ItemContainer selectedItemContainer = this.m_selectedItemContainer;
				if (selectedItemContainer != null)
				{
					selectedItemContainer.IsSelected = false;
				}
				this.m_selectedIndex = value;
				object obj = null;
				if (this.m_selectedIndex >= 0 && this.m_selectedIndex < this.m_items.Count)
				{
					obj = this.m_items[this.m_selectedIndex];
					this.m_selectedItemContainer = this.GetItemContainer(obj);
					if (this.m_selectedItemContainer != null)
					{
						this.m_selectedItemContainer.IsSelected = true;
					}
				}
				object[] array;
				if (obj == null)
				{
					array = new object[0];
				}
				else
				{
					(array = new object[1])[0] = obj;
				}
				object[] array2 = array;
				foreach (object obj2 in (this.m_selectedItems == null) ? new object[0] : this.m_selectedItems.Except<object>(array2).ToArray<object>())
				{
					ItemContainer itemContainer = this.GetItemContainer(obj2);
					if (itemContainer != null)
					{
						itemContainer.IsSelected = false;
					}
				}
				this.m_selectedItems = array2.ToList<object>();
				this.m_selectedItemsHS = new HashSet<object>(this.m_selectedItems);
				if (this.SelectionChanged != null)
				{
					object[] array3;
					this.SelectionChanged(this, new SelectionChangedArgs(array3, array2));
				}
				this.m_selectionLocked = false;
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000B6563 File Offset: 0x000B4763
		public int IndexOf(object obj)
		{
			if (this.m_items == null)
			{
				return -1;
			}
			if (obj == null)
			{
				return -1;
			}
			return this.m_items.IndexOf(obj);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000B6580 File Offset: 0x000B4780
		public ItemContainer GetItemContainer(object obj)
		{
			return this.m_itemContainers.Where<ItemContainer>((ItemContainer ic) => ic.Item == obj).FirstOrDefault<ItemContainer>();
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000B65B6 File Offset: 0x000B47B6
		public ItemContainer LastItemContainer()
		{
			if (this.m_itemContainers == null || this.m_itemContainers.Count == 0)
			{
				return null;
			}
			return this.m_itemContainers[this.m_itemContainers.Count - 1];
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000B65E7 File Offset: 0x000B47E7
		public ItemContainer GetItemContainer(int siblingIndex)
		{
			if (siblingIndex < 0 || siblingIndex >= this.m_itemContainers.Count)
			{
				return null;
			}
			return this.m_itemContainers[siblingIndex];
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000B660C File Offset: 0x000B480C
		public void ExternalBeginDrag(Vector3 position)
		{
			if (!this.CanDrag)
			{
				return;
			}
			this.m_externalDragOperation = true;
			if (this.m_dropTarget == null)
			{
				return;
			}
			if ((this.m_dragItems != null || this.m_externalDragOperation) && this.m_scrollDir == ItemsControl.ScrollDir.None)
			{
				this.m_dropMarker.SetTraget(this.m_dropTarget);
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000B6661 File Offset: 0x000B4861
		public void ExternalItemDrag(Vector3 position)
		{
			if (!this.CanDrag)
			{
				return;
			}
			if (this.m_dropTarget != null)
			{
				this.m_dropMarker.SetPosition(position);
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000B668B File Offset: 0x000B488B
		public void ExternalItemDrop()
		{
			if (!this.CanDrag)
			{
				return;
			}
			this.m_externalDragOperation = false;
			this.m_dropMarker.SetTraget(null);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000B66A9 File Offset: 0x000B48A9
		public ItemContainer Add(object item)
		{
			if (this.m_items == null)
			{
				this.m_items = new List<object>();
				this.m_itemContainers = new List<ItemContainer>();
			}
			return this.Insert(this.m_items.Count, item);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000B66DC File Offset: 0x000B48DC
		public virtual ItemContainer Insert(int index, object item)
		{
			if (this.m_items == null)
			{
				this.m_items = new List<object>();
				this.m_itemContainers = new List<ItemContainer>();
			}
			object obj = this.m_items.ElementAtOrDefault<object>(index);
			ItemContainer itemContainer = this.GetItemContainer(obj);
			int num;
			if (itemContainer != null)
			{
				num = this.m_itemContainers.IndexOf(itemContainer);
			}
			else
			{
				num = this.m_itemContainers.Count;
			}
			this.m_items.Insert(index, item);
			itemContainer = this.InstantiateItemContainer(num);
			if (itemContainer != null)
			{
				itemContainer.Item = item;
				this.DataBindItem(item, itemContainer);
			}
			return itemContainer;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000B6770 File Offset: 0x000B4970
		public void SetNextSibling(object sibling, object nextSibling)
		{
			ItemContainer itemContainer = this.GetItemContainer(sibling);
			if (itemContainer == null)
			{
				return;
			}
			ItemContainer itemContainer2 = this.GetItemContainer(nextSibling);
			if (itemContainer2 == null)
			{
				return;
			}
			this.Drop(new ItemContainer[] { itemContainer2 }, itemContainer, ItemDropAction.SetNextSibling);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000B67B4 File Offset: 0x000B49B4
		public void SetPrevSibling(object sibling, object prevSibling)
		{
			ItemContainer itemContainer = this.GetItemContainer(sibling);
			if (itemContainer == null)
			{
				return;
			}
			ItemContainer itemContainer2 = this.GetItemContainer(prevSibling);
			if (itemContainer2 == null)
			{
				return;
			}
			this.Drop(new ItemContainer[] { itemContainer2 }, itemContainer, ItemDropAction.SetPrevSibling);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000B67F7 File Offset: 0x000B49F7
		public virtual void Remove(object item)
		{
			if (item == null)
			{
				return;
			}
			if (this.m_items == null)
			{
				return;
			}
			if (!this.m_items.Contains(item))
			{
				return;
			}
			this.DestroyItem(item);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000B681C File Offset: 0x000B4A1C
		public void RemoveAt(int index)
		{
			if (this.m_items == null)
			{
				return;
			}
			if (index >= this.m_items.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.Remove(this.m_items[index]);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000B6858 File Offset: 0x000B4A58
		private void Awake()
		{
			if (this.Panel == null)
			{
				this.Panel = base.transform;
			}
			this.m_itemContainers = base.GetComponentsInChildren<ItemContainer>().ToList<ItemContainer>();
			this.m_rtcListener = base.GetComponentInChildren<RectTransformChangeListener>();
			if (this.m_rtcListener != null)
			{
				this.m_rtcListener.RectTransformChanged += this.OnViewportRectTransformChanged;
			}
			this.m_dropMarker = base.GetComponentInChildren<ItemDropMarker>(true);
			this.m_scrollRect = base.GetComponent<ScrollRect>();
			if (this.Camera == null)
			{
				this.Camera = Camera.main;
			}
			this.m_prevCanDrag = this.CanDrag;
			this.OnCanDragChanged();
			this.AwakeOverride();
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000B690B File Offset: 0x000B4B0B
		private void Start()
		{
			this.m_canvas = base.GetComponentInParent<Canvas>();
			this.StartOverride();
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000B6920 File Offset: 0x000B4B20
		private void Update()
		{
			if (this.m_scrollDir != ItemsControl.ScrollDir.None)
			{
				float num = this.m_scrollRect.content.rect.height - this.m_scrollRect.viewport.rect.height;
				float num2 = 0f;
				if (num > 0f)
				{
					num2 = this.ScrollSpeed / 10f * (1f / num);
				}
				float num3 = this.m_scrollRect.content.rect.width - this.m_scrollRect.viewport.rect.width;
				float num4 = 0f;
				if (num3 > 0f)
				{
					num4 = this.ScrollSpeed / 10f * (1f / num3);
				}
				if (this.m_scrollDir == ItemsControl.ScrollDir.Up)
				{
					this.m_scrollRect.verticalNormalizedPosition += num2;
					if (this.m_scrollRect.verticalNormalizedPosition > 1f)
					{
						this.m_scrollRect.verticalNormalizedPosition = 1f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
				else if (this.m_scrollDir == ItemsControl.ScrollDir.Down)
				{
					this.m_scrollRect.verticalNormalizedPosition -= num2;
					if (this.m_scrollRect.verticalNormalizedPosition < 0f)
					{
						this.m_scrollRect.verticalNormalizedPosition = 0f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
				else if (this.m_scrollDir == ItemsControl.ScrollDir.Left)
				{
					this.m_scrollRect.horizontalNormalizedPosition -= num4;
					if (this.m_scrollRect.horizontalNormalizedPosition < 0f)
					{
						this.m_scrollRect.horizontalNormalizedPosition = 0f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
				if (this.m_scrollDir == ItemsControl.ScrollDir.Right)
				{
					this.m_scrollRect.horizontalNormalizedPosition += num4;
					if (this.m_scrollRect.horizontalNormalizedPosition > 1f)
					{
						this.m_scrollRect.horizontalNormalizedPosition = 1f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
			}
			if (Input.GetKeyDown(this.RemoveKey))
			{
				this.RemoveSelectedItems();
			}
			if (Input.GetKeyDown(this.SelectAllKey) && Input.GetKey(this.RangeselectKey))
			{
				this.SelectedItems = this.m_items;
			}
			if (this.m_prevCanDrag != this.CanDrag)
			{
				this.OnCanDragChanged();
				this.m_prevCanDrag = this.CanDrag;
			}
			this.UpdateOverride();
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000B6B64 File Offset: 0x000B4D64
		private void OnEnable()
		{
			ItemContainer.Selected += this.OnItemSelected;
			ItemContainer.Unselected += this.OnItemUnselected;
			ItemContainer.PointerUp += this.OnItemPointerUp;
			ItemContainer.PointerDown += this.OnItemPointerDown;
			ItemContainer.PointerEnter += this.OnItemPointerEnter;
			ItemContainer.PointerExit += this.OnItemPointerExit;
			ItemContainer.DoubleClick += this.OnItemDoubleClick;
			ItemContainer.BeginEdit += this.OnItemBeginEdit;
			ItemContainer.EndEdit += this.OnItemEndEdit;
			ItemContainer.BeginDrag += this.OnItemBeginDrag;
			ItemContainer.Drag += this.OnItemDrag;
			ItemContainer.Drop += this.OnItemDrop;
			ItemContainer.EndDrag += this.OnItemEndDrag;
			this.OnEnableOverride();
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000B6C58 File Offset: 0x000B4E58
		private void OnDisable()
		{
			ItemContainer.Selected -= this.OnItemSelected;
			ItemContainer.Unselected -= this.OnItemUnselected;
			ItemContainer.PointerUp -= this.OnItemPointerUp;
			ItemContainer.PointerDown -= this.OnItemPointerDown;
			ItemContainer.PointerEnter -= this.OnItemPointerEnter;
			ItemContainer.PointerExit -= this.OnItemPointerExit;
			ItemContainer.DoubleClick -= this.OnItemDoubleClick;
			ItemContainer.BeginEdit -= this.OnItemBeginEdit;
			ItemContainer.EndEdit -= this.OnItemEndEdit;
			ItemContainer.BeginDrag -= this.OnItemBeginDrag;
			ItemContainer.Drag -= this.OnItemDrag;
			ItemContainer.Drop -= this.OnItemDrop;
			ItemContainer.EndDrag -= this.OnItemEndDrag;
			this.OnDisableOverride();
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000B6D4A File Offset: 0x000B4F4A
		private void OnDestroy()
		{
			if (this.m_rtcListener != null)
			{
				this.m_rtcListener.RectTransformChanged -= this.OnViewportRectTransformChanged;
			}
			this.OnDestroyOverride();
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000B6D77 File Offset: 0x000B4F77
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000B6D79 File Offset: 0x000B4F79
		protected virtual void StartOverride()
		{
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000B6D7B File Offset: 0x000B4F7B
		protected virtual void UpdateOverride()
		{
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000B6D7D File Offset: 0x000B4F7D
		protected virtual void OnEnableOverride()
		{
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000B6D7F File Offset: 0x000B4F7F
		protected virtual void OnDisableOverride()
		{
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000B6D81 File Offset: 0x000B4F81
		protected virtual void OnDestroyOverride()
		{
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000B6D84 File Offset: 0x000B4F84
		private void OnViewportRectTransformChanged()
		{
			if (this.ExpandChildrenHeight || this.ExpandChildrenWidth)
			{
				Rect rect = this.m_scrollRect.viewport.rect;
				if (rect.width != this.m_width || rect.height != this.m_height)
				{
					this.m_width = rect.width;
					this.m_height = rect.height;
					if (this.m_itemContainers != null)
					{
						for (int i = 0; i < this.m_itemContainers.Count; i++)
						{
							ItemContainer itemContainer = this.m_itemContainers[i];
							if (itemContainer != null)
							{
								if (this.ExpandChildrenWidth)
								{
									itemContainer.LayoutElement.minWidth = this.m_width;
								}
								if (this.ExpandChildrenHeight)
								{
									itemContainer.LayoutElement.minHeight = this.m_height;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000B6E58 File Offset: 0x000B5058
		private void OnCanDragChanged()
		{
			for (int i = 0; i < this.m_itemContainers.Count; i++)
			{
				ItemContainer itemContainer = this.m_itemContainers[i];
				if (itemContainer != null)
				{
					itemContainer.CanDrag = this.CanDrag;
				}
			}
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000B6EA0 File Offset: 0x000B50A0
		protected bool CanHandleEvent(object sender)
		{
			ItemContainer itemContainer = sender as ItemContainer;
			return itemContainer && itemContainer.transform.IsChildOf(this.Panel);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000B6ED0 File Offset: 0x000B50D0
		private void OnItemSelected(object sender, EventArgs e)
		{
			if (this.m_selectionLocked)
			{
				return;
			}
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			ItemContainer.Unselected -= this.OnItemUnselected;
			if (Input.GetKey(this.MultiselectKey))
			{
				IList list = ((this.m_selectedItems != null) ? this.m_selectedItems.ToList<object>() : new List<object>());
				list.Add(((ItemContainer)sender).Item);
				this.SelectedItems = list;
			}
			else if (Input.GetKey(this.RangeselectKey))
			{
				this.SelectRange((ItemContainer)sender);
			}
			else
			{
				this.SelectedIndex = this.IndexOf(((ItemContainer)sender).Item);
			}
			ItemContainer.Unselected += this.OnItemUnselected;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000B6F88 File Offset: 0x000B5188
		private void SelectRange(ItemContainer itemContainer)
		{
			if (this.m_selectedItems != null && this.m_selectedItems.Count > 0)
			{
				List<object> list = new List<object>();
				int num = this.IndexOf(this.m_selectedItems[0]);
				object item = itemContainer.Item;
				int num2 = this.IndexOf(item);
				int num3 = Mathf.Min(num, num2);
				int num4 = Math.Max(num, num2);
				list.Add(this.m_selectedItems[0]);
				for (int i = num3; i < num; i++)
				{
					list.Add(this.m_items[i]);
				}
				for (int j = num + 1; j <= num4; j++)
				{
					list.Add(this.m_items[j]);
				}
				this.SelectedItems = list;
				return;
			}
			this.SelectedIndex = this.IndexOf(itemContainer.Item);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000B7060 File Offset: 0x000B5260
		private void OnItemUnselected(object sender, EventArgs e)
		{
			if (this.m_selectionLocked)
			{
				return;
			}
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			IList list = ((this.m_selectedItems != null) ? this.m_selectedItems.ToList<object>() : new List<object>());
			list.Remove(((ItemContainer)sender).Item);
			this.SelectedItems = list;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000B70B4 File Offset: 0x000B52B4
		private void OnItemPointerDown(ItemContainer sender, PointerEventData e)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_externalDragOperation)
			{
				return;
			}
			this.m_dropMarker.SetTraget(null);
			this.m_dragItems = null;
			this.m_isDropInProgress = false;
			if (!this.SelectOnPointerUp)
			{
				if (Input.GetKey(this.RangeselectKey))
				{
					this.SelectRange(sender);
					return;
				}
				if (Input.GetKey(this.MultiselectKey))
				{
					sender.IsSelected = !sender.IsSelected;
					return;
				}
				sender.IsSelected = true;
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000B7130 File Offset: 0x000B5330
		private void OnItemPointerUp(ItemContainer sender, PointerEventData e)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_externalDragOperation)
			{
				return;
			}
			if (this.m_dragItems != null)
			{
				return;
			}
			if (this.SelectOnPointerUp)
			{
				if (!this.m_isDropInProgress)
				{
					if (Input.GetKey(this.RangeselectKey))
					{
						this.SelectRange(sender);
						return;
					}
					if (Input.GetKey(this.MultiselectKey))
					{
						sender.IsSelected = !sender.IsSelected;
						return;
					}
					sender.IsSelected = true;
					return;
				}
			}
			else if (!Input.GetKey(this.MultiselectKey) && !Input.GetKey(this.RangeselectKey) && this.m_selectedItems != null && this.m_selectedItems.Count > 1)
			{
				if (this.SelectedItem == sender.Item)
				{
					this.SelectedItem = null;
				}
				this.SelectedItem = sender.Item;
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000B71F7 File Offset: 0x000B53F7
		private void OnItemPointerEnter(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_dropTarget = sender;
			if ((this.m_dragItems != null || this.m_externalDragOperation) && this.m_scrollDir == ItemsControl.ScrollDir.None)
			{
				this.m_dropMarker.SetTraget(this.m_dropTarget);
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000B7233 File Offset: 0x000B5433
		private void OnItemPointerExit(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_dropTarget = null;
			if (this.m_dragItems != null || this.m_externalDragOperation)
			{
				this.m_dropMarker.SetTraget(null);
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000B7262 File Offset: 0x000B5462
		private void OnItemDoubleClick(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.ItemDoubleClick != null)
			{
				this.ItemDoubleClick(this, new ItemArgs(new object[] { sender.Item }));
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000B7296 File Offset: 0x000B5496
		protected virtual void OnItemBeginEdit(object sender, EventArgs e)
		{
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000B7298 File Offset: 0x000B5498
		protected virtual void OnItemEndEdit(object sender, EventArgs e)
		{
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000B729C File Offset: 0x000B549C
		private void OnItemBeginDrag(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_dropTarget != null)
			{
				this.m_dropMarker.SetTraget(this.m_dropTarget);
				this.m_dropMarker.SetPosition(eventData.position);
			}
			if (this.m_selectedItems != null && this.m_selectedItems.Contains(sender.Item))
			{
				this.m_dragItems = this.GetDragItems();
			}
			else
			{
				this.m_dragItems = new ItemContainer[] { sender };
			}
			if (this.ItemBeginDrag != null)
			{
				this.ItemBeginDrag(this, new ItemArgs(this.m_dragItems.Select<ItemContainer, object>((ItemContainer di) => di.Item).ToArray<object>()));
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000B7364 File Offset: 0x000B5564
		private void OnItemDrag(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.ExternalItemDrag(eventData.position);
			float height = this.m_scrollRect.viewport.rect.height;
			float width = this.m_scrollRect.viewport.rect.width;
			Camera camera = null;
			if (this.m_canvas.renderMode == RenderMode.WorldSpace || this.m_canvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				camera = this.Camera;
			}
			if (this.CanScroll)
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_scrollRect.viewport, eventData.position, camera, out vector))
				{
					if (vector.y >= 0f)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Up;
						this.m_dropMarker.SetTraget(null);
						return;
					}
					if (vector.y < -height)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Down;
						this.m_dropMarker.SetTraget(null);
						return;
					}
					if (vector.x <= 0f)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Left;
						return;
					}
					if (vector.x >= width)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Right;
						return;
					}
					this.m_scrollDir = ItemsControl.ScrollDir.None;
					return;
				}
			}
			else
			{
				this.m_scrollDir = ItemsControl.ScrollDir.None;
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000B7484 File Offset: 0x000B5684
		private void OnItemDrop(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_isDropInProgress = true;
			try
			{
				if (this.CanDrop(this.m_dragItems, this.m_dropTarget))
				{
					bool flag = false;
					if (this.ItemBeginDrop != null)
					{
						ItemDropCancelArgs itemDropCancelArgs = new ItemDropCancelArgs(this.m_dragItems.Select<ItemContainer, object>((ItemContainer di) => di.Item).ToArray<object>(), this.m_dropTarget.Item, this.m_dropMarker.Action, false);
						if (this.ItemBeginDrop != null)
						{
							this.ItemBeginDrop(this, itemDropCancelArgs);
							flag = itemDropCancelArgs.Cancel;
						}
					}
					if (!flag)
					{
						this.Drop(this.m_dragItems, this.m_dropTarget, this.m_dropMarker.Action);
						if (this.ItemDrop != null)
						{
							if (this.m_dragItems == null)
							{
								Debug.LogWarning("m_dragItems");
							}
							if (this.m_dropTarget == null)
							{
								Debug.LogWarning("m_dropTarget");
							}
							if (this.m_dropMarker == null)
							{
								Debug.LogWarning("m_dropMarker");
							}
							if (this.m_dragItems != null && this.m_dropTarget != null && this.m_dropMarker != null)
							{
								this.ItemDrop(this, new ItemDropArgs(this.m_dragItems.Select<ItemContainer, object>((ItemContainer di) => di.Item).ToArray<object>(), this.m_dropTarget.Item, this.m_dropMarker.Action, false));
							}
						}
					}
				}
				this.RaiseEndDrag();
			}
			finally
			{
				this.m_isDropInProgress = false;
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000B7644 File Offset: 0x000B5844
		private void OnItemEndDrag(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.RaiseEndDrag();
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000B7658 File Offset: 0x000B5858
		private void RaiseEndDrag()
		{
			if (this.m_dragItems != null)
			{
				if (this.ItemEndDrag != null)
				{
					this.ItemEndDrag(this, new ItemArgs(this.m_dragItems.Select<ItemContainer, object>((ItemContainer di) => di.Item).ToArray<object>()));
				}
				this.m_dropMarker.SetTraget(null);
				this.m_dragItems = null;
				this.m_scrollDir = ItemsControl.ScrollDir.None;
			}
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000B76D0 File Offset: 0x000B58D0
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (!this.CanReorder)
			{
				return;
			}
			if (this.m_dragItems == null)
			{
				GameObject pointerDrag = eventData.pointerDrag;
				if (pointerDrag != null)
				{
					ItemContainer component = pointerDrag.GetComponent<ItemContainer>();
					if (component != null && component.Item != null)
					{
						object item = component.Item;
						if (this.ItemDrop != null)
						{
							this.ItemDrop(this, new ItemDropArgs(new object[] { item }, null, ItemDropAction.SetLastChild, true));
						}
					}
				}
				return;
			}
			if (this.m_itemContainers != null && this.m_itemContainers.Count > 0)
			{
				this.m_dropTarget = this.m_itemContainers.Last<ItemContainer>();
				this.m_dropMarker.Action = ItemDropAction.SetNextSibling;
			}
			this.m_isDropInProgress = true;
			try
			{
				if (this.CanDrop(this.m_dragItems, this.m_dropTarget))
				{
					this.Drop(this.m_dragItems, this.m_dropTarget, this.m_dropMarker.Action);
					if (this.ItemDrop != null)
					{
						this.ItemDrop(this, new ItemDropArgs(this.m_dragItems.Select<ItemContainer, object>((ItemContainer di) => di.Item).ToArray<object>(), this.m_dropTarget.Item, this.m_dropMarker.Action, false));
					}
				}
				this.m_dropMarker.SetTraget(null);
				this.m_dragItems = null;
			}
			finally
			{
				this.m_isDropInProgress = false;
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000B783C File Offset: 0x000B5A3C
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.CanUnselectAll)
			{
				this.SelectedIndex = -1;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000B7850 File Offset: 0x000B5A50
		protected virtual bool CanDrop(ItemContainer[] dragItems, ItemContainer dropTarget)
		{
			return dropTarget == null || (dragItems != null && !dragItems.Contains(dropTarget.Item));
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000B7880 File Offset: 0x000B5A80
		protected ItemContainer[] GetDragItems()
		{
			ItemContainer[] array = new ItemContainer[this.m_selectedItems.Count];
			if (this.m_selectedItems != null)
			{
				for (int i = 0; i < this.m_selectedItems.Count; i++)
				{
					array[i] = this.GetItemContainer(this.m_selectedItems[i]);
				}
			}
			return array.OrderBy<ItemContainer, int>((ItemContainer di) => di.transform.GetSiblingIndex()).ToArray<ItemContainer>();
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000B78FC File Offset: 0x000B5AFC
		protected virtual void SetNextSibling(ItemContainer sibling, ItemContainer nextSibling)
		{
			int num = sibling.transform.GetSiblingIndex();
			int siblingIndex = nextSibling.transform.GetSiblingIndex();
			this.RemoveItemContainerAt(siblingIndex);
			this.RemoveItemAt(siblingIndex);
			if (siblingIndex > num)
			{
				num++;
			}
			nextSibling.transform.SetSiblingIndex(num);
			this.InsertItemContainerAt(num, nextSibling);
			this.InsertItem(num, nextSibling.Item);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000B7958 File Offset: 0x000B5B58
		protected virtual void SetPrevSibling(ItemContainer sibling, ItemContainer prevSibling)
		{
			int num = sibling.transform.GetSiblingIndex();
			int siblingIndex = prevSibling.transform.GetSiblingIndex();
			this.RemoveItemContainerAt(siblingIndex);
			this.RemoveItemAt(siblingIndex);
			if (siblingIndex < num)
			{
				num--;
			}
			prevSibling.transform.SetSiblingIndex(num);
			this.InsertItemContainerAt(num, prevSibling);
			this.InsertItem(num, prevSibling.Item);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000B79B4 File Offset: 0x000B5BB4
		protected virtual void Drop(ItemContainer[] dragItems, ItemContainer dropTarget, ItemDropAction action)
		{
			if (action == ItemDropAction.SetPrevSibling)
			{
				foreach (ItemContainer itemContainer in dragItems)
				{
					this.SetPrevSibling(dropTarget, itemContainer);
				}
			}
			else if (action == ItemDropAction.SetNextSibling)
			{
				foreach (ItemContainer itemContainer2 in dragItems)
				{
					this.SetNextSibling(dropTarget, itemContainer2);
				}
			}
			this.UpdateSelectedItemIndex();
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000B7A05 File Offset: 0x000B5C05
		protected void UpdateSelectedItemIndex()
		{
			this.m_selectedIndex = this.IndexOf(this.SelectedItem);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000B7A1C File Offset: 0x000B5C1C
		protected virtual void DataBind()
		{
			this.m_itemContainers = base.GetComponentsInChildren<ItemContainer>().ToList<ItemContainer>();
			if (this.m_items == null)
			{
				for (int i = 0; i < this.m_itemContainers.Count; i++)
				{
					global::UnityEngine.Object.DestroyImmediate(this.m_itemContainers[i].gameObject);
				}
			}
			else
			{
				int num = this.m_items.Count - this.m_itemContainers.Count;
				if (num > 0)
				{
					for (int j = 0; j < num; j++)
					{
						this.InstantiateItemContainer(this.m_itemContainers.Count);
					}
				}
				else
				{
					int num2 = this.m_itemContainers.Count + num;
					for (int k = this.m_itemContainers.Count - 1; k >= num2; k--)
					{
						this.DestroyItemContainer(k);
					}
				}
			}
			for (int l = 0; l < this.m_itemContainers.Count; l++)
			{
				ItemContainer itemContainer = this.m_itemContainers[l];
				if (itemContainer != null)
				{
					itemContainer.Clear();
				}
			}
			if (this.m_items != null)
			{
				for (int m = 0; m < this.m_items.Count; m++)
				{
					object obj = this.m_items[m];
					ItemContainer itemContainer2 = this.m_itemContainers[m];
					itemContainer2.CanDrag = this.CanDrag;
					if (itemContainer2 != null)
					{
						itemContainer2.Item = obj;
						this.DataBindItem(obj, itemContainer2);
					}
				}
			}
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000B7B82 File Offset: 0x000B5D82
		public virtual void DataBindItem(object item, ItemContainer itemContainer)
		{
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000B7B84 File Offset: 0x000B5D84
		protected ItemContainer InstantiateItemContainer(int siblingIndex)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.ItemContainerPrefab);
			gameObject.name = "ItemContainer";
			gameObject.transform.SetParent(this.Panel, false);
			gameObject.transform.SetSiblingIndex(siblingIndex);
			ItemContainer itemContainer = this.InstantiateItemContainerOverride(gameObject);
			itemContainer.CanDrag = this.CanDrag;
			if (this.ExpandChildrenWidth)
			{
				itemContainer.LayoutElement.minWidth = this.m_width;
			}
			if (this.ExpandChildrenHeight)
			{
				itemContainer.LayoutElement.minHeight = this.m_height;
			}
			this.m_itemContainers.Insert(siblingIndex, itemContainer);
			return itemContainer;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000B7C1A File Offset: 0x000B5E1A
		protected void DestroyItemContainer(int siblingIndex)
		{
			if (this.m_itemContainers == null)
			{
				return;
			}
			if (siblingIndex >= 0 && siblingIndex < this.m_itemContainers.Count)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_itemContainers[siblingIndex].gameObject);
				this.m_itemContainers.RemoveAt(siblingIndex);
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000B7C5C File Offset: 0x000B5E5C
		protected virtual ItemContainer InstantiateItemContainerOverride(GameObject container)
		{
			ItemContainer itemContainer = container.GetComponent<ItemContainer>();
			if (itemContainer == null)
			{
				itemContainer = container.AddComponent<ItemContainer>();
			}
			return itemContainer;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000B7C84 File Offset: 0x000B5E84
		public void RemoveSelectedItems()
		{
			if (this.m_selectedItems == null)
			{
				return;
			}
			object[] array;
			if (this.ItemsRemoving != null)
			{
				ItemsCancelArgs itemsCancelArgs = new ItemsCancelArgs(this.m_selectedItems.ToList<object>());
				this.ItemsRemoving(this, itemsCancelArgs);
				if (itemsCancelArgs.Items == null)
				{
					array = new object[0];
				}
				else
				{
					array = itemsCancelArgs.Items.ToArray();
				}
			}
			else
			{
				array = this.m_selectedItems.ToArray();
			}
			if (array.Length == 0)
			{
				return;
			}
			this.SelectedItems = null;
			foreach (object obj in array)
			{
				this.DestroyItem(obj);
			}
			if (this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, new ItemsRemovedArgs(array));
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000B7D2C File Offset: 0x000B5F2C
		protected virtual void DestroyItem(object item)
		{
			if (this.m_selectedItems != null && this.m_selectedItems.Contains(item))
			{
				this.m_selectedItems.Remove(item);
				this.m_selectedItemsHS.Remove(item);
				if (this.m_selectedItems.Count == 0)
				{
					this.m_selectedItemContainer = null;
					this.m_selectedIndex = -1;
				}
				else
				{
					this.m_selectedItemContainer = this.GetItemContainer(this.m_selectedItems[0]);
					this.m_selectedIndex = this.IndexOf(this.m_selectedItemContainer.Item);
				}
			}
			ItemContainer itemContainer = this.GetItemContainer(item);
			if (itemContainer != null)
			{
				int siblingIndex = itemContainer.transform.GetSiblingIndex();
				this.DestroyItemContainer(siblingIndex);
				this.m_items.Remove(item);
			}
		}

		// Token: 0x04001225 RID: 4645
		public KeyCode MultiselectKey = KeyCode.LeftControl;

		// Token: 0x04001226 RID: 4646
		public KeyCode RangeselectKey = KeyCode.LeftShift;

		// Token: 0x04001227 RID: 4647
		public KeyCode SelectAllKey = KeyCode.A;

		// Token: 0x04001228 RID: 4648
		public KeyCode RemoveKey = KeyCode.Delete;

		// Token: 0x04001229 RID: 4649
		public bool SelectOnPointerUp;

		// Token: 0x0400122A RID: 4650
		public bool CanUnselectAll = true;

		// Token: 0x0400122B RID: 4651
		public bool CanEdit = true;

		// Token: 0x0400122C RID: 4652
		private bool m_prevCanDrag;

		// Token: 0x0400122D RID: 4653
		public bool CanDrag = true;

		// Token: 0x0400122E RID: 4654
		public bool CanReorder = true;

		// Token: 0x0400122F RID: 4655
		public bool ExpandChildrenWidth = true;

		// Token: 0x04001230 RID: 4656
		public bool ExpandChildrenHeight;

		// Token: 0x04001231 RID: 4657
		private bool m_isDropInProgress;

		// Token: 0x04001232 RID: 4658
		[SerializeField]
		private GameObject ItemContainerPrefab;

		// Token: 0x04001233 RID: 4659
		[SerializeField]
		protected Transform Panel;

		// Token: 0x04001234 RID: 4660
		private Canvas m_canvas;

		// Token: 0x04001235 RID: 4661
		public Camera Camera;

		// Token: 0x04001236 RID: 4662
		public float ScrollSpeed = 100f;

		// Token: 0x04001237 RID: 4663
		private ItemsControl.ScrollDir m_scrollDir;

		// Token: 0x04001238 RID: 4664
		private ScrollRect m_scrollRect;

		// Token: 0x04001239 RID: 4665
		private RectTransformChangeListener m_rtcListener;

		// Token: 0x0400123A RID: 4666
		private float m_width;

		// Token: 0x0400123B RID: 4667
		private float m_height;

		// Token: 0x0400123C RID: 4668
		private List<ItemContainer> m_itemContainers;

		// Token: 0x0400123D RID: 4669
		private ItemDropMarker m_dropMarker;

		// Token: 0x0400123E RID: 4670
		private bool m_externalDragOperation;

		// Token: 0x0400123F RID: 4671
		private ItemContainer m_dropTarget;

		// Token: 0x04001240 RID: 4672
		private ItemContainer[] m_dragItems;

		// Token: 0x04001241 RID: 4673
		private IList<object> m_items;

		// Token: 0x04001242 RID: 4674
		private bool m_selectionLocked;

		// Token: 0x04001243 RID: 4675
		private List<object> m_selectedItems;

		// Token: 0x04001244 RID: 4676
		private HashSet<object> m_selectedItemsHS;

		// Token: 0x04001245 RID: 4677
		private ItemContainer m_selectedItemContainer;

		// Token: 0x04001246 RID: 4678
		private int m_selectedIndex = -1;

		// Token: 0x02001795 RID: 6037
		private enum ScrollDir
		{
			// Token: 0x04005F03 RID: 24323
			None,
			// Token: 0x04005F04 RID: 24324
			Up,
			// Token: 0x04005F05 RID: 24325
			Down,
			// Token: 0x04005F06 RID: 24326
			Left,
			// Token: 0x04005F07 RID: 24327
			Right
		}
	}
}
