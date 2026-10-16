using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x0200002A RID: 42
	[AddComponentMenu("UI/Mask", 13)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class Mask : UIBehaviour, ICanvasRaycastFilter, IMaterialModifier
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000F590 File Offset: 0x0000D790
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this.m_RectTransform) == null)
				{
					rectTransform = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000F5BE File Offset: 0x0000D7BE
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public Graphic graphic
		{
			get
			{
				Graphic graphic;
				if ((graphic = this.m_Graphic) == null)
				{
					graphic = (this.m_Graphic = base.GetComponent<Graphic>());
				}
				return graphic;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000F612 File Offset: 0x0000D812
		protected Mask()
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000F621 File Offset: 0x0000D821
		public virtual bool MaskEnabled()
		{
			return this.IsActive() && this.graphic != null;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000F639 File Offset: 0x0000D839
		[Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000F63C File Offset: 0x0000D83C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.hasPopInstruction = true;
				this.graphic.SetMaterialDirty();
				if (this.graphic is MaskableGraphic)
				{
					(this.graphic as MaskableGraphic).isMaskingGraphic = true;
				}
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
				this.graphic.canvasRenderer.hasPopInstruction = false;
				this.graphic.canvasRenderer.popMaterialCount = 0;
				if (this.graphic is MaskableGraphic)
				{
					(this.graphic as MaskableGraphic).isMaskingGraphic = false;
				}
			}
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = null;
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F736 File Offset: 0x0000D936
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000F750 File Offset: 0x0000D950
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!this.MaskEnabled())
			{
				return baseMaterial;
			}
			Transform transform = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = MaskUtilities.GetStencilDepth(base.transform, transform);
			if (stencilDepth >= 8)
			{
				Debug.LogWarning("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int num = 1 << stencilDepth;
			if (num == 1)
			{
				Material material = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0));
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = material;
				Material material2 = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, (ColorWriteMask)0);
				StencilMaterial.Remove(this.m_UnmaskMaterial);
				this.m_UnmaskMaterial = material2;
				this.graphic.canvasRenderer.popMaterialCount = 1;
				this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
				return this.m_MaskMaterial;
			}
			Material material3 = StencilMaterial.Add(baseMaterial, num | (num - 1), StencilOp.Replace, CompareFunction.Equal, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0), num - 1, num | (num - 1));
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = material3;
			this.graphic.canvasRenderer.hasPopInstruction = true;
			Material material4 = StencilMaterial.Add(baseMaterial, num - 1, StencilOp.Replace, CompareFunction.Equal, (ColorWriteMask)0, num - 1, num | (num - 1));
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = material4;
			this.graphic.canvasRenderer.popMaterialCount = 1;
			this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
			return this.m_MaskMaterial;
		}

		// Token: 0x040000F6 RID: 246
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x040000F8 RID: 248
		[NonSerialized]
		private Graphic m_Graphic;

		// Token: 0x040000F9 RID: 249
		[NonSerialized]
		private Material m_MaskMaterial;

		// Token: 0x040000FA RID: 250
		[NonSerialized]
		private Material m_UnmaskMaterial;
	}
}
