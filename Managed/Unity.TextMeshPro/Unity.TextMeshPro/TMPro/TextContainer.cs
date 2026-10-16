using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x02000081 RID: 129
	[RequireComponent(typeof(RectTransform))]
	public class TextContainer : UIBehaviour
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0002E659 File Offset: 0x0002C859
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0002E661 File Offset: 0x0002C861
		public bool hasChanged
		{
			get
			{
				return this.m_hasChanged;
			}
			set
			{
				this.m_hasChanged = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0002E66A File Offset: 0x0002C86A
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0002E672 File Offset: 0x0002C872
		public Vector2 pivot
		{
			get
			{
				return this.m_pivot;
			}
			set
			{
				if (this.m_pivot != value)
				{
					this.m_pivot = value;
					this.m_anchorPosition = this.GetAnchorPosition(this.m_pivot);
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		public TextContainerAnchors anchorPosition
		{
			get
			{
				return this.m_anchorPosition;
			}
			set
			{
				if (this.m_anchorPosition != value)
				{
					this.m_anchorPosition = value;
					this.m_pivot = this.GetPivot(this.m_anchorPosition);
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0002E6E1 File Offset: 0x0002C8E1
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0002E6E9 File Offset: 0x0002C8E9
		public Rect rect
		{
			get
			{
				return this.m_rect;
			}
			set
			{
				if (this.m_rect != value)
				{
					this.m_rect = value;
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0002E70D File Offset: 0x0002C90D
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0002E72C File Offset: 0x0002C92C
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_rect.width, this.m_rect.height);
			}
			set
			{
				if (new Vector2(this.m_rect.width, this.m_rect.height) != value)
				{
					this.SetRect(value);
					this.m_hasChanged = true;
					this.m_isDefaultWidth = false;
					this.m_isDefaultHeight = false;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0002E77E File Offset: 0x0002C97E
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0002E78B File Offset: 0x0002C98B
		public float width
		{
			get
			{
				return this.m_rect.width;
			}
			set
			{
				this.SetRect(new Vector2(value, this.m_rect.height));
				this.m_hasChanged = true;
				this.m_isDefaultWidth = false;
				this.OnContainerChanged();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0002E7B8 File Offset: 0x0002C9B8
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0002E7C5 File Offset: 0x0002C9C5
		public float height
		{
			get
			{
				return this.m_rect.height;
			}
			set
			{
				this.SetRect(new Vector2(this.m_rect.width, value));
				this.m_hasChanged = true;
				this.m_isDefaultHeight = false;
				this.OnContainerChanged();
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0002E7F2 File Offset: 0x0002C9F2
		public bool isDefaultWidth
		{
			get
			{
				return this.m_isDefaultWidth;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0002E7FA File Offset: 0x0002C9FA
		public bool isDefaultHeight
		{
			get
			{
				return this.m_isDefaultHeight;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0002E802 File Offset: 0x0002CA02
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0002E80A File Offset: 0x0002CA0A
		public bool isAutoFitting
		{
			get
			{
				return this.m_isAutoFitting;
			}
			set
			{
				this.m_isAutoFitting = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0002E813 File Offset: 0x0002CA13
		public Vector3[] corners
		{
			get
			{
				return this.m_corners;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0002E81B File Offset: 0x0002CA1B
		public Vector3[] worldCorners
		{
			get
			{
				return this.m_worldCorners;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0002E823 File Offset: 0x0002CA23
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0002E82B File Offset: 0x0002CA2B
		public Vector4 margins
		{
			get
			{
				return this.m_margins;
			}
			set
			{
				if (this.m_margins != value)
				{
					this.m_margins = value;
					this.m_hasChanged = true;
					this.OnContainerChanged();
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0002E84F File Offset: 0x0002CA4F
		public RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0002E871 File Offset: 0x0002CA71
		public TextMeshPro textMeshPro
		{
			get
			{
				if (this.m_textMeshPro == null)
				{
					this.m_textMeshPro = base.GetComponent<TextMeshPro>();
				}
				return this.m_textMeshPro;
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002E893 File Offset: 0x0002CA93
		protected override void Awake()
		{
			Debug.LogWarning("The Text Container component is now Obsolete and can safely be removed from [" + base.gameObject.name + "].", this);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002E8B5 File Offset: 0x0002CAB5
		protected override void OnEnable()
		{
			this.OnContainerChanged();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002E8BD File Offset: 0x0002CABD
		protected override void OnDisable()
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		private void OnContainerChanged()
		{
			this.UpdateCorners();
			if (this.m_rectTransform != null)
			{
				this.m_rectTransform.sizeDelta = this.size;
				this.m_rectTransform.hasChanged = true;
			}
			if (this.textMeshPro != null)
			{
				this.m_textMeshPro.SetVerticesDirty();
				this.m_textMeshPro.margin = this.m_margins;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0002E928 File Offset: 0x0002CB28
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.rectTransform == null)
			{
				this.m_rectTransform = base.gameObject.AddComponent<RectTransform>();
			}
			if (this.m_rectTransform.sizeDelta != TextContainer.k_defaultSize)
			{
				this.size = this.m_rectTransform.sizeDelta;
			}
			this.pivot = this.m_rectTransform.pivot;
			this.m_hasChanged = true;
			this.OnContainerChanged();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0002E99A File Offset: 0x0002CB9A
		private void SetRect(Vector2 size)
		{
			this.m_rect = new Rect(this.m_rect.x, this.m_rect.y, size.x, size.y);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0002E9CC File Offset: 0x0002CBCC
		private void UpdateCorners()
		{
			this.m_corners[0] = new Vector3(-this.m_pivot.x * this.m_rect.width, -this.m_pivot.y * this.m_rect.height);
			this.m_corners[1] = new Vector3(-this.m_pivot.x * this.m_rect.width, (1f - this.m_pivot.y) * this.m_rect.height);
			this.m_corners[2] = new Vector3((1f - this.m_pivot.x) * this.m_rect.width, (1f - this.m_pivot.y) * this.m_rect.height);
			this.m_corners[3] = new Vector3((1f - this.m_pivot.x) * this.m_rect.width, -this.m_pivot.y * this.m_rect.height);
			if (this.m_rectTransform != null)
			{
				this.m_rectTransform.pivot = this.m_pivot;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0002EB10 File Offset: 0x0002CD10
		private Vector2 GetPivot(TextContainerAnchors anchor)
		{
			Vector2 zero = Vector2.zero;
			switch (anchor)
			{
			case TextContainerAnchors.TopLeft:
				zero = new Vector2(0f, 1f);
				break;
			case TextContainerAnchors.Top:
				zero = new Vector2(0.5f, 1f);
				break;
			case TextContainerAnchors.TopRight:
				zero = new Vector2(1f, 1f);
				break;
			case TextContainerAnchors.Left:
				zero = new Vector2(0f, 0.5f);
				break;
			case TextContainerAnchors.Middle:
				zero = new Vector2(0.5f, 0.5f);
				break;
			case TextContainerAnchors.Right:
				zero = new Vector2(1f, 0.5f);
				break;
			case TextContainerAnchors.BottomLeft:
				zero = new Vector2(0f, 0f);
				break;
			case TextContainerAnchors.Bottom:
				zero = new Vector2(0.5f, 0f);
				break;
			case TextContainerAnchors.BottomRight:
				zero = new Vector2(1f, 0f);
				break;
			}
			return zero;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002EC04 File Offset: 0x0002CE04
		private TextContainerAnchors GetAnchorPosition(Vector2 pivot)
		{
			if (pivot == new Vector2(0f, 1f))
			{
				return TextContainerAnchors.TopLeft;
			}
			if (pivot == new Vector2(0.5f, 1f))
			{
				return TextContainerAnchors.Top;
			}
			if (pivot == new Vector2(1f, 1f))
			{
				return TextContainerAnchors.TopRight;
			}
			if (pivot == new Vector2(0f, 0.5f))
			{
				return TextContainerAnchors.Left;
			}
			if (pivot == new Vector2(0.5f, 0.5f))
			{
				return TextContainerAnchors.Middle;
			}
			if (pivot == new Vector2(1f, 0.5f))
			{
				return TextContainerAnchors.Right;
			}
			if (pivot == new Vector2(0f, 0f))
			{
				return TextContainerAnchors.BottomLeft;
			}
			if (pivot == new Vector2(0.5f, 0f))
			{
				return TextContainerAnchors.Bottom;
			}
			if (pivot == new Vector2(1f, 0f))
			{
				return TextContainerAnchors.BottomRight;
			}
			return TextContainerAnchors.Custom;
		}

		// Token: 0x0400058B RID: 1419
		private bool m_hasChanged;

		// Token: 0x0400058C RID: 1420
		[SerializeField]
		private Vector2 m_pivot;

		// Token: 0x0400058D RID: 1421
		[SerializeField]
		private TextContainerAnchors m_anchorPosition = TextContainerAnchors.Middle;

		// Token: 0x0400058E RID: 1422
		[SerializeField]
		private Rect m_rect;

		// Token: 0x0400058F RID: 1423
		private bool m_isDefaultWidth;

		// Token: 0x04000590 RID: 1424
		private bool m_isDefaultHeight;

		// Token: 0x04000591 RID: 1425
		private bool m_isAutoFitting;

		// Token: 0x04000592 RID: 1426
		private Vector3[] m_corners = new Vector3[4];

		// Token: 0x04000593 RID: 1427
		private Vector3[] m_worldCorners = new Vector3[4];

		// Token: 0x04000594 RID: 1428
		[SerializeField]
		private Vector4 m_margins;

		// Token: 0x04000595 RID: 1429
		private RectTransform m_rectTransform;

		// Token: 0x04000596 RID: 1430
		private static Vector2 k_defaultSize = new Vector2(100f, 100f);

		// Token: 0x04000597 RID: 1431
		private TextMeshPro m_textMeshPro;
	}
}
