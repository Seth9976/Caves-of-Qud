using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000071 RID: 113
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x06000677 RID: 1655
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000678 RID: 1656
		public abstract Camera eventCamera { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001B84D File Offset: 0x00019A4D
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001B850 File Offset: 0x00019A50
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001B857 File Offset: 0x00019A57
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001B860 File Offset: 0x00019A60
		public BaseRaycaster rootRaycaster
		{
			get
			{
				if (this.m_RootRaycaster == null)
				{
					BaseRaycaster[] componentsInParent = base.GetComponentsInParent<BaseRaycaster>();
					if (componentsInParent.Length != 0)
					{
						this.m_RootRaycaster = componentsInParent[componentsInParent.Length - 1];
					}
				}
				return this.m_RootRaycaster;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001B89C File Offset: 0x00019A9C
		public override string ToString()
		{
			string[] array = new string[8];
			array[0] = "Name: ";
			int num = 1;
			GameObject gameObject = base.gameObject;
			array[num] = ((gameObject != null) ? gameObject.ToString() : null);
			array[2] = "\neventCamera: ";
			int num2 = 3;
			Camera eventCamera = this.eventCamera;
			array[num2] = ((eventCamera != null) ? eventCamera.ToString() : null);
			array[4] = "\nsortOrderPriority: ";
			array[5] = this.sortOrderPriority.ToString();
			array[6] = "\nrenderOrderPriority: ";
			array[7] = this.renderOrderPriority.ToString();
			return string.Concat(array);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B920 File Offset: 0x00019B20
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001B92E File Offset: 0x00019B2E
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001B93C File Offset: 0x00019B3C
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001B94B File Offset: 0x00019B4B
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x0400023D RID: 573
		private BaseRaycaster m_RootRaycaster;
	}
}
