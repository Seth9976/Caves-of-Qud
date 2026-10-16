using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006F RID: 111
	public struct RaycastResult
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001B5F0 File Offset: 0x000197F0
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001B5F8 File Offset: 0x000197F8
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001B601 File Offset: 0x00019801
		public bool isValid
		{
			get
			{
				return this.module != null && this.gameObject != null;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001B620 File Offset: 0x00019820
		public void Clear()
		{
			this.gameObject = null;
			this.module = null;
			this.distance = 0f;
			this.index = 0f;
			this.depth = 0;
			this.sortingLayer = 0;
			this.sortingOrder = 0;
			this.worldNormal = Vector3.up;
			this.worldPosition = Vector3.zero;
			this.screenPosition = Vector3.zero;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001B68C File Offset: 0x0001988C
		public override string ToString()
		{
			if (!this.isValid)
			{
				return "";
			}
			string[] array = new string[24];
			array[0] = "Name: ";
			int num = 1;
			GameObject gameObject = this.gameObject;
			array[num] = ((gameObject != null) ? gameObject.ToString() : null);
			array[2] = "\nmodule: ";
			int num2 = 3;
			BaseRaycaster baseRaycaster = this.module;
			array[num2] = ((baseRaycaster != null) ? baseRaycaster.ToString() : null);
			array[4] = "\ndistance: ";
			array[5] = this.distance.ToString();
			array[6] = "\nindex: ";
			array[7] = this.index.ToString();
			array[8] = "\ndepth: ";
			array[9] = this.depth.ToString();
			array[10] = "\nworldNormal: ";
			int num3 = 11;
			Vector3 vector = this.worldNormal;
			array[num3] = vector.ToString();
			array[12] = "\nworldPosition: ";
			int num4 = 13;
			vector = this.worldPosition;
			array[num4] = vector.ToString();
			array[14] = "\nscreenPosition: ";
			int num5 = 15;
			Vector2 vector2 = this.screenPosition;
			array[num5] = vector2.ToString();
			array[16] = "\nmodule.sortOrderPriority: ";
			array[17] = this.module.sortOrderPriority.ToString();
			array[18] = "\nmodule.renderOrderPriority: ";
			array[19] = this.module.renderOrderPriority.ToString();
			array[20] = "\nsortingLayer: ";
			array[21] = this.sortingLayer.ToString();
			array[22] = "\nsortingOrder: ";
			array[23] = this.sortingOrder.ToString();
			return string.Concat(array);
		}

		// Token: 0x0400022F RID: 559
		private GameObject m_GameObject;

		// Token: 0x04000230 RID: 560
		public BaseRaycaster module;

		// Token: 0x04000231 RID: 561
		public float distance;

		// Token: 0x04000232 RID: 562
		public float index;

		// Token: 0x04000233 RID: 563
		public int depth;

		// Token: 0x04000234 RID: 564
		public int sortingGroupID;

		// Token: 0x04000235 RID: 565
		public int sortingGroupOrder;

		// Token: 0x04000236 RID: 566
		public int sortingLayer;

		// Token: 0x04000237 RID: 567
		public int sortingOrder;

		// Token: 0x04000238 RID: 568
		public Vector3 worldPosition;

		// Token: 0x04000239 RID: 569
		public Vector3 worldNormal;

		// Token: 0x0400023A RID: 570
		public Vector2 screenPosition;

		// Token: 0x0400023B RID: 571
		public int displayIndex;
	}
}
