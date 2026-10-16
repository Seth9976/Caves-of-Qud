using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005DE RID: 1502
	[Obsolete("EventSystem no longer supports input override for legacy input. Install Input System package for full input binding functionality.")]
	public class InputWrapper : MonoBehaviour
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x0009FDE8 File Offset: 0x0009DFE8
		public virtual string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x0009FE00 File Offset: 0x0009E000
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x0009FE17 File Offset: 0x0009E017
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

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x0009FE24 File Offset: 0x0009E024
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x0009FE3B File Offset: 0x0009E03B
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

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x0009FE48 File Offset: 0x0009E048
		public virtual bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0009FE60 File Offset: 0x0009E060
		public virtual bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0009FE78 File Offset: 0x0009E078
		public virtual bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0009FE90 File Offset: 0x0009E090
		public virtual bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x0009FEA8 File Offset: 0x0009E0A8
		public virtual Vector2 mousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x0009FEC4 File Offset: 0x0009E0C4
		public virtual Vector2 mouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x0009FEDC File Offset: 0x0009E0DC
		public virtual bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x0009FEF4 File Offset: 0x0009E0F4
		public virtual int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0009FF0C File Offset: 0x0009E10C
		public virtual Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x0009FF24 File Offset: 0x0009E124
		public virtual float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x0009FF3C File Offset: 0x0009E13C
		public virtual bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}
	}
}
