using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003C2 RID: 962
	[RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
	public class ItemContainer : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600239F RID: 9119 RVA: 0x000B4CD8 File Offset: 0x000B2ED8
		// (remove) Token: 0x060023A0 RID: 9120 RVA: 0x000B4D0C File Offset: 0x000B2F0C
		public static event EventHandler Selected;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060023A1 RID: 9121 RVA: 0x000B4D40 File Offset: 0x000B2F40
		// (remove) Token: 0x060023A2 RID: 9122 RVA: 0x000B4D74 File Offset: 0x000B2F74
		public static event EventHandler Unselected;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060023A3 RID: 9123 RVA: 0x000B4DA8 File Offset: 0x000B2FA8
		// (remove) Token: 0x060023A4 RID: 9124 RVA: 0x000B4DDC File Offset: 0x000B2FDC
		public static event ItemEventHandler PointerDown;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060023A5 RID: 9125 RVA: 0x000B4E10 File Offset: 0x000B3010
		// (remove) Token: 0x060023A6 RID: 9126 RVA: 0x000B4E44 File Offset: 0x000B3044
		public static event ItemEventHandler PointerUp;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060023A7 RID: 9127 RVA: 0x000B4E78 File Offset: 0x000B3078
		// (remove) Token: 0x060023A8 RID: 9128 RVA: 0x000B4EAC File Offset: 0x000B30AC
		public static event ItemEventHandler DoubleClick;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060023A9 RID: 9129 RVA: 0x000B4EE0 File Offset: 0x000B30E0
		// (remove) Token: 0x060023AA RID: 9130 RVA: 0x000B4F14 File Offset: 0x000B3114
		public static event ItemEventHandler PointerEnter;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060023AB RID: 9131 RVA: 0x000B4F48 File Offset: 0x000B3148
		// (remove) Token: 0x060023AC RID: 9132 RVA: 0x000B4F7C File Offset: 0x000B317C
		public static event ItemEventHandler PointerExit;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060023AD RID: 9133 RVA: 0x000B4FB0 File Offset: 0x000B31B0
		// (remove) Token: 0x060023AE RID: 9134 RVA: 0x000B4FE4 File Offset: 0x000B31E4
		public static event ItemEventHandler BeginDrag;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060023AF RID: 9135 RVA: 0x000B5018 File Offset: 0x000B3218
		// (remove) Token: 0x060023B0 RID: 9136 RVA: 0x000B504C File Offset: 0x000B324C
		public static event ItemEventHandler Drag;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060023B1 RID: 9137 RVA: 0x000B5080 File Offset: 0x000B3280
		// (remove) Token: 0x060023B2 RID: 9138 RVA: 0x000B50B4 File Offset: 0x000B32B4
		public static event ItemEventHandler Drop;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060023B3 RID: 9139 RVA: 0x000B50E8 File Offset: 0x000B32E8
		// (remove) Token: 0x060023B4 RID: 9140 RVA: 0x000B511C File Offset: 0x000B331C
		public static event ItemEventHandler EndDrag;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060023B5 RID: 9141 RVA: 0x000B5150 File Offset: 0x000B3350
		// (remove) Token: 0x060023B6 RID: 9142 RVA: 0x000B5184 File Offset: 0x000B3384
		public static event EventHandler BeginEdit;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060023B7 RID: 9143 RVA: 0x000B51B8 File Offset: 0x000B33B8
		// (remove) Token: 0x060023B8 RID: 9144 RVA: 0x000B51EC File Offset: 0x000B33EC
		public static event EventHandler EndEdit;

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000B521F File Offset: 0x000B341F
		public LayoutElement LayoutElement
		{
			get
			{
				return this.m_layoutElement;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000B5227 File Offset: 0x000B3427
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000B522F File Offset: 0x000B342F
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x000B5238 File Offset: 0x000B3438
		public virtual bool IsSelected
		{
			get
			{
				return this.m_isSelected;
			}
			set
			{
				if (this.m_isSelected != value)
				{
					this.m_isSelected = value;
					if (this.m_isSelected)
					{
						if (ItemContainer.Selected != null)
						{
							ItemContainer.Selected(this, EventArgs.Empty);
							return;
						}
					}
					else if (ItemContainer.Unselected != null)
					{
						ItemContainer.Unselected(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000B528C File Offset: 0x000B348C
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x000B5294 File Offset: 0x000B3494
		public bool IsEditing
		{
			get
			{
				return this.m_isEditing;
			}
			set
			{
				if (this.m_isEditing != value && this.m_isSelected)
				{
					this.m_isEditing = value && this.m_isSelected;
					if (this.EditorPresenter != this.ItemPresenter)
					{
						if (this.EditorPresenter != null)
						{
							this.EditorPresenter.SetActive(this.m_isEditing);
						}
						if (this.ItemPresenter != null)
						{
							this.ItemPresenter.SetActive(!this.m_isEditing);
						}
					}
					if (this.m_isEditing)
					{
						if (ItemContainer.BeginEdit != null)
						{
							ItemContainer.BeginEdit(this, EventArgs.Empty);
							return;
						}
					}
					else if (ItemContainer.EndEdit != null)
					{
						ItemContainer.EndEdit(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000B5355 File Offset: 0x000B3555
		private ItemsControl ItemsControl
		{
			get
			{
				if (this.m_itemsControl == null)
				{
					this.m_itemsControl = base.GetComponentInParent<ItemsControl>();
				}
				return this.m_itemsControl;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x000B5377 File Offset: 0x000B3577
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x000B537F File Offset: 0x000B357F
		public object Item { get; set; }

		// Token: 0x060023C2 RID: 9154 RVA: 0x000B5388 File Offset: 0x000B3588
		private void Awake()
		{
			this.m_rectTransform = base.GetComponent<RectTransform>();
			this.m_layoutElement = base.GetComponent<LayoutElement>();
			if (this.ItemPresenter == null)
			{
				this.ItemPresenter = base.gameObject;
			}
			if (this.EditorPresenter == null)
			{
				this.EditorPresenter = base.gameObject;
			}
			this.AwakeOverride();
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000B53E7 File Offset: 0x000B35E7
		private void Start()
		{
			this.StartOverride();
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000B53EF File Offset: 0x000B35EF
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			this.m_coBeginEdit = null;
			this.OnDestroyOverride();
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000B5404 File Offset: 0x000B3604
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000B5406 File Offset: 0x000B3606
		protected virtual void StartOverride()
		{
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000B5408 File Offset: 0x000B3608
		protected virtual void OnDestroyOverride()
		{
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000B540C File Offset: 0x000B360C
		public virtual void Clear()
		{
			this.m_isEditing = false;
			if (this.EditorPresenter != this.ItemPresenter)
			{
				if (this.EditorPresenter != null)
				{
					this.EditorPresenter.SetActive(this.m_isEditing);
				}
				if (this.ItemPresenter != null)
				{
					this.ItemPresenter.SetActive(!this.m_isEditing);
				}
			}
			this.m_isSelected = false;
			this.Item = null;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000B5482 File Offset: 0x000B3682
		private IEnumerator CoBeginEdit()
		{
			yield return new WaitForSeconds(0.5f);
			this.m_coBeginEdit = null;
			this.IsEditing = this.CanEdit;
			yield break;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000B5494 File Offset: 0x000B3694
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			this.m_canBeginEdit = this.m_isSelected && this.ItemsControl != null && this.ItemsControl.SelectedItemsCount == 1 && this.ItemsControl.CanEdit;
			if (ItemContainer.PointerDown != null)
			{
				ItemContainer.PointerDown(this, eventData);
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000B54EC File Offset: 0x000B36EC
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				if (ItemContainer.DoubleClick != null)
				{
					ItemContainer.DoubleClick(this, eventData);
				}
				if (this.CanEdit && this.m_coBeginEdit != null)
				{
					base.StopCoroutine(this.m_coBeginEdit);
					this.m_coBeginEdit = null;
					return;
				}
			}
			else
			{
				if (this.m_canBeginEdit && this.m_coBeginEdit == null)
				{
					this.m_coBeginEdit = this.CoBeginEdit();
					base.StartCoroutine(this.m_coBeginEdit);
				}
				if (ItemContainer.PointerUp != null)
				{
					ItemContainer.PointerUp(this, eventData);
				}
			}
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000B5578 File Offset: 0x000B3778
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IBeginDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
				return;
			}
			this.m_canBeginEdit = false;
			if (ItemContainer.BeginDrag != null)
			{
				ItemContainer.BeginDrag(this, eventData);
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000B55C4 File Offset: 0x000B37C4
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (ItemContainer.Drop != null)
			{
				ItemContainer.Drop(this, eventData);
			}
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000B55D9 File Offset: 0x000B37D9
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.dragHandler);
				return;
			}
			if (ItemContainer.Drag != null)
			{
				ItemContainer.Drag(this, eventData);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000B5613 File Offset: 0x000B3813
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IEndDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.endDragHandler);
				return;
			}
			if (ItemContainer.EndDrag != null)
			{
				ItemContainer.EndDrag(this, eventData);
			}
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000B564D File Offset: 0x000B384D
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (ItemContainer.PointerEnter != null)
			{
				ItemContainer.PointerEnter(this, eventData);
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000B5662 File Offset: 0x000B3862
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (ItemContainer.PointerExit != null)
			{
				ItemContainer.PointerExit(this, eventData);
			}
		}

		// Token: 0x040011E5 RID: 4581
		public bool CanDrag = true;

		// Token: 0x040011E6 RID: 4582
		public bool CanEdit = true;

		// Token: 0x040011E7 RID: 4583
		public bool CanDrop = true;

		// Token: 0x040011F5 RID: 4597
		public GameObject ItemPresenter;

		// Token: 0x040011F6 RID: 4598
		public GameObject EditorPresenter;

		// Token: 0x040011F7 RID: 4599
		private LayoutElement m_layoutElement;

		// Token: 0x040011F8 RID: 4600
		private RectTransform m_rectTransform;

		// Token: 0x040011F9 RID: 4601
		protected bool m_isSelected;

		// Token: 0x040011FA RID: 4602
		private bool m_isEditing;

		// Token: 0x040011FB RID: 4603
		private ItemsControl m_itemsControl;

		// Token: 0x040011FD RID: 4605
		private bool m_canBeginEdit;

		// Token: 0x040011FE RID: 4606
		private IEnumerator m_coBeginEdit;
	}
}
