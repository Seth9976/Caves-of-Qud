using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000418 RID: 1048
	public abstract class ControlledSelectable : Selectable, IControlledSelectable
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000CDF43 File Offset: 0x000CC143
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x000CDF50 File Offset: 0x000CC150
		public bool selected
		{
			get
			{
				return this.wasSelected.GetValueOrDefault();
			}
			set
			{
				this.Deselect();
				if (value)
				{
					this.Select();
				}
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000CDF64 File Offset: 0x000CC164
		public void Deselect()
		{
			if (EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
				ExecuteEvents.Execute<IPointerExitHandler>(EventSystem.current.currentSelectedGameObject, pointerEventData, ExecuteEvents.pointerExitHandler);
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000CDFB4 File Offset: 0x000CC1B4
		public bool IsSelected()
		{
			return this.selected;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000CDFBC File Offset: 0x000CC1BC
		public override void OnSelect(BaseEventData eventData)
		{
			if (this.controller != null)
			{
				this.controller.SetSelected(this, this.controller.lastEventSource);
			}
			this.wasSelected = new bool?(true);
			this.SelectChanged(true);
			base.OnSelect(eventData);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000CE008 File Offset: 0x000CC208
		public virtual void Update()
		{
			this._debouceClick -= Time.deltaTime;
			bool flag = base.IsPressed() || base.currentSelectionState == Selectable.SelectionState.Selected;
			bool? flag2 = this.wasSelected;
			bool flag3 = flag;
			if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
			{
				this.wasSelected = new bool?(flag);
				this.SelectChanged(flag);
			}
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000CE06C File Offset: 0x000CC26C
		protected ControlledSelectable.ScrollViewCalcs GetScrollViewCalcs(ControlledSelectable.ScrollViewCalcs reuse)
		{
			if (this._rt == null)
			{
				this._rt = base.gameObject.GetComponent<RectTransform>();
			}
			ControlledSelectable.ScrollViewCalcs scrollViewCalcs = reuse ?? new ControlledSelectable.ScrollViewCalcs();
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

		// Token: 0x060027D2 RID: 10194 RVA: 0x000CE1DC File Offset: 0x000CC3DC
		public bool IsInFullView()
		{
			this.GetScrollViewCalcs(this._sv);
			bool flag = this._sv.scrollTop > this._sv.contentTop;
			bool flag2 = this._sv.scrollBottom < this._sv.contentBottom;
			return !flag && !flag2;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000CE22C File Offset: 0x000CC42C
		public bool IsInView()
		{
			this.GetScrollViewCalcs(this._sv);
			bool flag = this._sv.scrollTop > this._sv.contentBottom;
			bool flag2 = this._sv.scrollBottom < this._sv.contentTop;
			return !flag && !flag2;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000CE27C File Offset: 0x000CC47C
		public virtual void SelectChanged(bool newState)
		{
			if (this._rt == null)
			{
				this._rt = base.GetComponent<RectTransform>();
			}
			if (newState)
			{
				ScrollRect componentInParent = base.gameObject.GetComponentInParent<ScrollRect>();
				if (componentInParent != null && componentInParent.vertical)
				{
					Vector3[] array = new Vector3[4];
					Vector3[] array2 = new Vector3[4];
					componentInParent.content.GetWorldCorners(array);
					this._rt.GetWorldCorners(array2);
					float num = componentInParent.content.rect.height * (float)Options.StageScale;
					float num2 = componentInParent.viewport.rect.height * (float)Options.StageScale;
					if (num2 < num)
					{
						float num3 = array[2].y - array2[2].y;
						float num4 = array[2].y - array2[0].y;
						float verticalNormalizedPosition = componentInParent.verticalNormalizedPosition;
						float num5 = (1f - verticalNormalizedPosition) * (num - num2);
						float num6 = num5 + num2;
						bool flag = num5 > num3;
						bool flag2 = num6 < num4;
						if (flag)
						{
							componentInParent.verticalNormalizedPosition = 1f - num3 / (num - num2);
							return;
						}
						if (flag2)
						{
							componentInParent.verticalNormalizedPosition = 1f - (num4 - num2) / (num - num2);
						}
					}
				}
			}
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000CE3C0 File Offset: 0x000CC5C0
		public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			if (eventData.IsPointerMoving())
			{
				this.Select();
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000CE3D7 File Offset: 0x000CC5D7
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (this._debouceClick > 0f)
			{
				return;
			}
			base.OnPointerUp(eventData);
			if (this.controller)
			{
				this.controller.Activate(this, QudBaseMenuController.MenuEventPipeline.Pointer);
			}
			this._debouceClick = 0.5f;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000CE413 File Offset: 0x000CC613
		public virtual void UpdateData()
		{
			this.SelectChanged(this.selected);
		}

		// Token: 0x040013A1 RID: 5025
		public QudBaseMenuController controller;

		// Token: 0x040013A2 RID: 5026
		public object data;

		// Token: 0x040013A3 RID: 5027
		private RectTransform _rt;

		// Token: 0x040013A4 RID: 5028
		private bool? wasSelected;

		// Token: 0x040013A5 RID: 5029
		private ControlledSelectable.ScrollViewCalcs _sv = new ControlledSelectable.ScrollViewCalcs();

		// Token: 0x040013A6 RID: 5030
		private float _debouceClick;

		// Token: 0x020017DB RID: 6107
		protected class ScrollViewCalcs
		{
			// Token: 0x0400600B RID: 24587
			public ScrollRect scroller;

			// Token: 0x0400600C RID: 24588
			public float contentHeight;

			// Token: 0x0400600D RID: 24589
			public float scrollHeight;

			// Token: 0x0400600E RID: 24590
			public float contentTop;

			// Token: 0x0400600F RID: 24591
			public float contentBottom;

			// Token: 0x04006010 RID: 24592
			public float scrollTop;

			// Token: 0x04006011 RID: 24593
			public float scrollBottom;

			// Token: 0x04006012 RID: 24594
			public float scrollPercent;
		}
	}
}
