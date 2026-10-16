using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000025 RID: 37
	[AddComponentMenu("Layout/Layout Element", 140)]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000E68C File Offset: 0x0000C88C
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000E694 File Offset: 0x0000C894
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E6AA File Offset: 0x0000C8AA
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E6AE File Offset: 0x0000C8AE
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000E6B6 File Offset: 0x0000C8B6
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000E6EA File Offset: 0x0000C8EA
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000E6F2 File Offset: 0x0000C8F2
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000E708 File Offset: 0x0000C908
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000E710 File Offset: 0x0000C910
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000E726 File Offset: 0x0000C926
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000E72E File Offset: 0x0000C92E
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000E744 File Offset: 0x0000C944
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000E74C File Offset: 0x0000C94C
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000E762 File Offset: 0x0000C962
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000E76A File Offset: 0x0000C96A
		public virtual int layoutPriority
		{
			get
			{
				return this.m_LayoutPriority;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_LayoutPriority, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E780 File Offset: 0x0000C980
		protected LayoutElement()
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E7EA File Offset: 0x0000C9EA
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E7F2 File Offset: 0x0000C9F2
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E800 File Offset: 0x0000CA00
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E808 File Offset: 0x0000CA08
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E810 File Offset: 0x0000CA10
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x040000E4 RID: 228
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x040000E7 RID: 231
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		private float m_FlexibleHeight = -1f;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		private int m_LayoutPriority = 1;
	}
}
