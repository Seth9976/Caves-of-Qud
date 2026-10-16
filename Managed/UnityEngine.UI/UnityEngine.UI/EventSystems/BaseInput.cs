using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000069 RID: 105
	public class BaseInput : UIBehaviour
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x000197FF File Offset: 0x000179FF
		public virtual string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00019806 File Offset: 0x00017A06
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0001980D File Offset: 0x00017A0D
		public virtual IMECompositionMode imeCompositionMode
		{
			get
			{
				return Input.imeCompositionMode;
			}
			set
			{
				Input.imeCompositionMode = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00019815 File Offset: 0x00017A15
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0001981C File Offset: 0x00017A1C
		public virtual Vector2 compositionCursorPos
		{
			get
			{
				return Input.compositionCursorPos;
			}
			set
			{
				Input.compositionCursorPos = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00019824 File Offset: 0x00017A24
		public virtual bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001982B File Offset: 0x00017A2B
		public virtual bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00019833 File Offset: 0x00017A33
		public virtual bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001983B File Offset: 0x00017A3B
		public virtual bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00019843 File Offset: 0x00017A43
		public virtual Vector2 mousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001984F File Offset: 0x00017A4F
		public virtual Vector2 mouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00019856 File Offset: 0x00017A56
		public virtual float mouseScrollDeltaPerTick
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001985D File Offset: 0x00017A5D
		public virtual bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00019864 File Offset: 0x00017A64
		public virtual int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001986B File Offset: 0x00017A6B
		public virtual Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019873 File Offset: 0x00017A73
		public virtual float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001987B File Offset: 0x00017A7B
		public virtual bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}
	}
}
