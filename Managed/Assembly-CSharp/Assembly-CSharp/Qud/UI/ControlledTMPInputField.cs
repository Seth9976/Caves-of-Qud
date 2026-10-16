using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000419 RID: 1049
	public class ControlledTMPInputField : TMP_InputField, IControlledSelectable, IControlledInputField
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000CE434 File Offset: 0x000CC634
		// (set) Token: 0x060027DA RID: 10202 RVA: 0x000CE444 File Offset: 0x000CC644
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

		// Token: 0x060027DB RID: 10203 RVA: 0x000CE482 File Offset: 0x000CC682
		public bool IsSelected()
		{
			return this.selected;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000CE48C File Offset: 0x000CC68C
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

		// Token: 0x060027DD RID: 10205 RVA: 0x000CE4D8 File Offset: 0x000CC6D8
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

		// Token: 0x060027DE RID: 10206 RVA: 0x000CE52E File Offset: 0x000CC72E
		public virtual void SelectChanged(bool newState)
		{
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000CE530 File Offset: 0x000CC730
		public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			this.Select();
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000CE53F File Offset: 0x000CC73F
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

		// Token: 0x060027E1 RID: 10209 RVA: 0x000CE57B File Offset: 0x000CC77B
		public void Init()
		{
			this.wasSelected = new bool?(false);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000CE589 File Offset: 0x000CC789
		public void UpdateValue(string value)
		{
			this.controller.ActivateInput(this, QudBaseMenuController.MenuEventPipeline.KeyboardOrJoystick);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000CE598 File Offset: 0x000CC798
		protected ControlledTMPInputField.ScrollViewCalcs GetScrollViewCalcs(ControlledTMPInputField.ScrollViewCalcs reuse)
		{
			if (this._rt == null)
			{
				this._rt = base.gameObject.GetComponent<RectTransform>();
			}
			ControlledTMPInputField.ScrollViewCalcs scrollViewCalcs = reuse ?? new ControlledTMPInputField.ScrollViewCalcs();
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

		// Token: 0x060027E4 RID: 10212 RVA: 0x000CE708 File Offset: 0x000CC908
		public bool IsInFullView()
		{
			this.GetScrollViewCalcs(this._sv);
			bool flag = this._sv.scrollTop > this._sv.contentTop;
			bool flag2 = this._sv.scrollBottom < this._sv.contentBottom;
			return !flag && !flag2;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000CE758 File Offset: 0x000CC958
		public bool IsInView()
		{
			this.GetScrollViewCalcs(this._sv);
			bool flag = this._sv.scrollTop > this._sv.contentBottom;
			bool flag2 = this._sv.scrollBottom < this._sv.contentTop;
			return !flag && !flag2;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000CE7BB File Offset: 0x000CC9BB
		bool IControlledInputField.get_isFocused()
		{
			return base.isFocused;
		}

		// Token: 0x040013A7 RID: 5031
		public QudBaseMenuController controller;

		// Token: 0x040013A8 RID: 5032
		public object data;

		// Token: 0x040013A9 RID: 5033
		private RectTransform _rt;

		// Token: 0x040013AA RID: 5034
		private bool? wasSelected;

		// Token: 0x040013AB RID: 5035
		private string oldItemText;

		// Token: 0x040013AC RID: 5036
		private float _debouceClick;

		// Token: 0x040013AD RID: 5037
		private ControlledTMPInputField.ScrollViewCalcs _sv = new ControlledTMPInputField.ScrollViewCalcs();

		// Token: 0x020017DC RID: 6108
		protected class ScrollViewCalcs
		{
			// Token: 0x04006013 RID: 24595
			public ScrollRect scroller;

			// Token: 0x04006014 RID: 24596
			public float contentHeight;

			// Token: 0x04006015 RID: 24597
			public float scrollHeight;

			// Token: 0x04006016 RID: 24598
			public float contentTop;

			// Token: 0x04006017 RID: 24599
			public float contentBottom;

			// Token: 0x04006018 RID: 24600
			public float scrollTop;

			// Token: 0x04006019 RID: 24601
			public float scrollBottom;

			// Token: 0x0400601A RID: 24602
			public float scrollPercent;
		}
	}
}
