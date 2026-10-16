using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000416 RID: 1046
	public class ControlledInputField : InputField, IControlledInputField, IControlledSelectable
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060027B8 RID: 10168 RVA: 0x000CDBB5 File Offset: 0x000CBDB5
		// (set) Token: 0x060027B9 RID: 10169 RVA: 0x000CDBC4 File Offset: 0x000CBDC4
		public bool selected
		{
			get
			{
				return this.wasSelected.GetValueOrDefault();
			}
			set
			{
				bool? flag = this.wasSelected;
				if (!((value == flag.GetValueOrDefault()) & (flag != null)))
				{
					if (value)
					{
						this.Select();
						base.ActivateInputField();
						return;
					}
					this.InstantClearState();
				}
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000CDC02 File Offset: 0x000CBE02
		public bool IsSelected()
		{
			return this.selected;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000CDC0C File Offset: 0x000CBE0C
		public override void OnSelect(BaseEventData eventData)
		{
			if (this.controller != null)
			{
				this.controller.SetSelectedInput(this, this.controller.lastEventSource);
			}
			this.wasSelected = new bool?(true);
			this.SelectChanged(true);
			base.OnSelect(eventData);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000CDC58 File Offset: 0x000CBE58
		public virtual void Update()
		{
			this._debouceClick -= Time.deltaTime;
			bool isFocused = base.isFocused;
			bool? flag = this.wasSelected;
			bool flag2 = isFocused;
			if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
			{
				this.wasSelected = new bool?(isFocused);
				this.SelectChanged(isFocused);
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000CDCAE File Offset: 0x000CBEAE
		public virtual void SelectChanged(bool newState)
		{
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000CDCB0 File Offset: 0x000CBEB0
		public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			this.Select();
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000CDCBF File Offset: 0x000CBEBF
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (this._debouceClick > 0f)
			{
				return;
			}
			base.OnPointerUp(eventData);
			if (this.controller)
			{
				this.controller.ActivateInput(this, QudBaseMenuController.MenuEventPipeline.Pointer);
			}
			this._debouceClick = 0.5f;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000CDCFB File Offset: 0x000CBEFB
		public void Init()
		{
			this.wasSelected = new bool?(false);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000CDD09 File Offset: 0x000CBF09
		public void UpdateValue(string value)
		{
			this.controller.ActivateInput(this, QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick);
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000CDD18 File Offset: 0x000CBF18
		protected ControlledInputField.ScrollViewCalcs GetScrollViewCalcs(ControlledInputField.ScrollViewCalcs reuse)
		{
			if (this._rt == null)
			{
				this._rt = base.gameObject.GetComponent<RectTransform>();
			}
			ControlledInputField.ScrollViewCalcs scrollViewCalcs = reuse ?? new ControlledInputField.ScrollViewCalcs();
			if (scrollViewCalcs.scroller == null)
			{
				scrollViewCalcs.scroller = base.gameObject.GetComponentInParent<ScrollRect>();
			}
			if (scrollViewCalcs.scroller != null && scrollViewCalcs.scroller.vertical)
			{
				Vector3[] array = new Vector3[4];
				Vector3[] array2 = new Vector3[4];
				scrollViewCalcs.scroller.content.GetWorldCorners(array);
				this._rt.GetWorldCorners(array2);
				scrollViewCalcs.contentHeight = scrollViewCalcs.scroller.content.rect.height * (float)Options.StageScale;
				scrollViewCalcs.scrollHeight = scrollViewCalcs.scroller.viewport.rect.height * (float)Options.StageScale;
				scrollViewCalcs.contentTop = array[2].y - array2[2].y;
				scrollViewCalcs.contentBottom = array[2].y - array2[0].y;
				scrollViewCalcs.scrollPercent = scrollViewCalcs.scroller.verticalNormalizedPosition;
				scrollViewCalcs.scrollTop = (1f - scrollViewCalcs.scrollPercent) * (scrollViewCalcs.contentHeight - scrollViewCalcs.scrollHeight);
				scrollViewCalcs.scrollBottom = scrollViewCalcs.scrollTop + scrollViewCalcs.scrollHeight;
			}
			return scrollViewCalcs;
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000CDE88 File Offset: 0x000CC088
		public bool IsInFullView()
		{
			this.GetScrollViewCalcs(this._sv);
			bool flag = this._sv.scrollTop > this._sv.contentTop;
			bool flag2 = this._sv.scrollBottom < this._sv.contentBottom;
			return !flag && !flag2;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000CDED8 File Offset: 0x000CC0D8
		public bool IsInView()
		{
			this.GetScrollViewCalcs(this._sv);
			bool flag = this._sv.scrollTop > this._sv.contentBottom;
			bool flag2 = this._sv.scrollBottom < this._sv.contentTop;
			return !flag && !flag2;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000CDF3B File Offset: 0x000CC13B
		bool IControlledInputField.get_isFocused()
		{
			return base.isFocused;
		}

		// Token: 0x0400139A RID: 5018
		public QudBaseMenuController controller;

		// Token: 0x0400139B RID: 5019
		public object data;

		// Token: 0x0400139C RID: 5020
		private RectTransform _rt;

		// Token: 0x0400139D RID: 5021
		private bool? wasSelected;

		// Token: 0x0400139E RID: 5022
		private string oldItemText;

		// Token: 0x0400139F RID: 5023
		private float _debouceClick;

		// Token: 0x040013A0 RID: 5024
		private ControlledInputField.ScrollViewCalcs _sv = new ControlledInputField.ScrollViewCalcs();

		// Token: 0x020017DA RID: 6106
		protected class ScrollViewCalcs
		{
			// Token: 0x04006003 RID: 24579
			public ScrollRect scroller;

			// Token: 0x04006004 RID: 24580
			public float contentHeight;

			// Token: 0x04006005 RID: 24581
			public float scrollHeight;

			// Token: 0x04006006 RID: 24582
			public float contentTop;

			// Token: 0x04006007 RID: 24583
			public float contentBottom;

			// Token: 0x04006008 RID: 24584
			public float scrollTop;

			// Token: 0x04006009 RID: 24585
			public float scrollBottom;

			// Token: 0x0400600A RID: 24586
			public float scrollPercent;
		}
	}
}
