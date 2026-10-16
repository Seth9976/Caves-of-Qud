using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200041A RID: 1050
	public class CursorManager : MonoBehaviour
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000CE7C3 File Offset: 0x000CC9C3
		public int cursorThemeIndex
		{
			get
			{
				return Options.MouseCursor;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000CE7CC File Offset: 0x000CC9CC
		public bool cursorAutoHideEnabled
		{
			get
			{
				bool flag = this._cursorAutoHideEnabled.GetValueOrDefault();
				if (this._cursorAutoHideEnabled == null)
				{
					flag = Options.GetOptionBool("OptionMouseAutoHideCursorEnabled");
					this._cursorAutoHideEnabled = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000CE80C File Offset: 0x000CCA0C
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x000CE814 File Offset: 0x000CCA14
		public CursorManager.Style currentStyle
		{
			get
			{
				return this._currentStyle;
			}
			set
			{
				if (this._currentStyle != value)
				{
					this.syncCursor = true;
					this._currentStyle = value;
				}
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000CE82D File Offset: 0x000CCA2D
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x000CE835 File Offset: 0x000CCA35
		public bool cursorHidden
		{
			get
			{
				return this._cursorHidden;
			}
			set
			{
				if (this._cursorHidden != value)
				{
					this.syncCursor = true;
					this._cursorHidden = value;
				}
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000CE84E File Offset: 0x000CCA4E
		public void Sync()
		{
			this.syncCursor = true;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000CE857 File Offset: 0x000CCA57
		public void Start()
		{
			CursorManager.instance = this;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000CE85F File Offset: 0x000CCA5F
		public static void setStyle(CursorManager.Style style)
		{
			if (CursorManager.instance != null)
			{
				CursorManager.instance.currentStyle = style;
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000CE879 File Offset: 0x000CCA79
		private bool inWindow(Vector2 v)
		{
			return v.x >= 0f && v.y >= 0f && v.x <= (float)Screen.width && v.y <= (float)Screen.height;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000CE8B8 File Offset: 0x000CCAB8
		public void Update()
		{
			if (this.inWindow(Input.mousePosition) && !this.inWindow(this.lastMousePosition))
			{
				this.syncCursor = true;
			}
			if (Vector3.Distance(this.lastMousePosition, Input.mousePosition) > 1f && !this.forceCursorHidden)
			{
				this.cursorHideTime = Time.realtimeSinceStartup + this.cursorHideDelay;
				this.cursorHidden = false;
				this.lastMousePosition = Input.mousePosition;
			}
			if (this.cursorAutoHideEnabled && !this.cursorHidden && Application.isFocused && this.cursorHideTime < Time.realtimeSinceStartup)
			{
				this.cursorHidden = true;
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000CE960 File Offset: 0x000CCB60
		public void LateUpdate()
		{
			if (this.syncCursor)
			{
				this.syncCursor = false;
				Texture2D texture2D;
				if (this.cursorHidden)
				{
					texture2D = this.hiddenCursor[this.cursorThemeIndex];
				}
				else
				{
					Texture2D texture2D2;
					switch (this.currentStyle)
					{
					case CursorManager.Style.Pointer:
						texture2D2 = this.normalCursor[this.cursorThemeIndex];
						break;
					case CursorManager.Style.ResizeNorthSouth:
						texture2D2 = this.resizeNorthSouthCursor[this.cursorThemeIndex];
						break;
					case CursorManager.Style.ResizeWestEast:
						texture2D2 = this.resizeWestEastCursor[this.cursorThemeIndex];
						break;
					default:
						texture2D2 = null;
						break;
					}
					texture2D = texture2D2;
				}
				Texture2D texture2D3 = texture2D;
				CursorManager.Style currentStyle = this.currentStyle;
				Vector2 zero;
				if (currentStyle != CursorManager.Style.ResizeNorthSouth)
				{
					if (currentStyle != CursorManager.Style.ResizeWestEast)
					{
						zero = Vector2.zero;
					}
					else
					{
						zero = this.resizeHotSpot;
					}
				}
				else
				{
					zero = this.resizeHotSpot;
				}
				Vector2 vector = zero;
				Cursor.SetCursor(texture2D3, vector, CursorMode.Auto);
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000CEA34 File Offset: 0x000CCC34
		public void UpdateOptions()
		{
			this._cursorAutoHideEnabled = null;
			this.cursorHidden = false;
			this.syncCursor = true;
		}

		// Token: 0x040013AE RID: 5038
		public static CursorManager instance;

		// Token: 0x040013AF RID: 5039
		public List<Texture2D> hiddenCursor;

		// Token: 0x040013B0 RID: 5040
		public List<Texture2D> normalCursor;

		// Token: 0x040013B1 RID: 5041
		public Vector2 resizeHotSpot;

		// Token: 0x040013B2 RID: 5042
		public List<Texture2D> resizeNorthSouthCursor;

		// Token: 0x040013B3 RID: 5043
		public List<Texture2D> resizeWestEastCursor;

		// Token: 0x040013B4 RID: 5044
		public float cursorHideDelay = 3f;

		// Token: 0x040013B5 RID: 5045
		private bool? _cursorAutoHideEnabled;

		// Token: 0x040013B6 RID: 5046
		private CursorManager.Style _currentStyle;

		// Token: 0x040013B7 RID: 5047
		private bool _cursorHidden;

		// Token: 0x040013B8 RID: 5048
		public bool forceCursorHidden;

		// Token: 0x040013B9 RID: 5049
		private Vector3 lastMousePosition;

		// Token: 0x040013BA RID: 5050
		private bool syncCursor = true;

		// Token: 0x040013BB RID: 5051
		private float cursorHideTime;

		// Token: 0x020017DD RID: 6109
		public enum Style
		{
			// Token: 0x0400601C RID: 24604
			Pointer,
			// Token: 0x0400601D RID: 24605
			ResizeNorthSouth,
			// Token: 0x0400601E RID: 24606
			ResizeWestEast
		}
	}
}
