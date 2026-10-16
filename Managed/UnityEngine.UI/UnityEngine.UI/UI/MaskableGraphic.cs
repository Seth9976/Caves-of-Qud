using System;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x0200002C RID: 44
	public abstract class MaskableGraphic : Graphic, IClippable, IMaskable, IMaterialModifier
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000FC8A File Offset: 0x0000DE8A
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000FC92 File Offset: 0x0000DE92
		public MaskableGraphic.CullStateChangedEvent onCullStateChanged
		{
			get
			{
				return this.m_OnCullStateChanged;
			}
			set
			{
				this.m_OnCullStateChanged = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000FC9B File Offset: 0x0000DE9B
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000FCA3 File Offset: 0x0000DEA3
		public bool maskable
		{
			get
			{
				return this.m_Maskable;
			}
			set
			{
				if (value == this.m_Maskable)
				{
					return;
				}
				this.m_Maskable = value;
				this.m_ShouldRecalculateStencil = true;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000FCC3 File Offset: 0x0000DEC3
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000FCCB File Offset: 0x0000DECB
		public bool isMaskingGraphic
		{
			get
			{
				return this.m_IsMaskingGraphic;
			}
			set
			{
				if (value == this.m_IsMaskingGraphic)
				{
					return;
				}
				this.m_IsMaskingGraphic = value;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			Material material = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				if (this.maskable)
				{
					Transform transform = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
					this.m_StencilValue = MaskUtilities.GetStencilDepth(base.transform, transform);
				}
				else
				{
					this.m_StencilValue = 0;
				}
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0 && !this.isMaskingGraphic)
			{
				Material material2 = StencilMaterial.Add(material, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = material2;
				material = this.m_MaskMaterial;
			}
			return material;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000FD80 File Offset: 0x0000DF80
		public virtual void Cull(Rect clipRect, bool validRect)
		{
			bool flag = !validRect || !clipRect.Overlaps(this.rootCanvasRect, true);
			this.UpdateCull(flag);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		private void UpdateCull(bool cull)
		{
			if (base.canvasRenderer.cull != cull)
			{
				base.canvasRenderer.cull = cull;
				UISystemProfilerApi.AddMarker("MaskableGraphic.cullingChanged", this);
				this.m_OnCullStateChanged.Invoke(cull);
				this.OnCullingChanged();
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000FDE5 File Offset: 0x0000DFE5
		public virtual void SetClipRect(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.EnableRectClipping(clipRect);
				return;
			}
			base.canvasRenderer.DisableRectClipping();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000FE02 File Offset: 0x0000E002
		public virtual void SetClipSoftness(Vector2 clipSoftness)
		{
			base.canvasRenderer.clippingSoftness = clipSoftness;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000FE10 File Offset: 0x0000E010
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
			if (this.isMaskingGraphic)
			{
				MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000FE39 File Offset: 0x0000E039
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
			this.UpdateClipParent();
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			if (this.isMaskingGraphic)
			{
				MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000FE74 File Offset: 0x0000E074
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000FE98 File Offset: 0x0000E098
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore.", true)]
		public virtual void ParentMaskStateChanged()
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000FE9A File Offset: 0x0000E09A
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		private Rect rootCanvasRect
		{
			get
			{
				base.rectTransform.GetWorldCorners(this.m_Corners);
				if (base.canvas)
				{
					Matrix4x4 worldToLocalMatrix = base.canvas.rootCanvas.transform.worldToLocalMatrix;
					for (int i = 0; i < 4; i++)
					{
						this.m_Corners[i] = worldToLocalMatrix.MultiplyPoint(this.m_Corners[i]);
					}
				}
				Vector2 vector = this.m_Corners[0];
				Vector2 vector2 = this.m_Corners[0];
				for (int j = 1; j < 4; j++)
				{
					vector.x = Mathf.Min(this.m_Corners[j].x, vector.x);
					vector.y = Mathf.Min(this.m_Corners[j].y, vector.y);
					vector2.x = Mathf.Max(this.m_Corners[j].x, vector2.x);
					vector2.y = Mathf.Max(this.m_Corners[j].y, vector2.y);
				}
				return new Rect(vector, vector2 - vector);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010004 File Offset: 0x0000E204
		private void UpdateClipParent()
		{
			RectMask2D rectMask2D = ((this.maskable && this.IsActive()) ? MaskUtilities.GetRectMaskForClippable(this) : null);
			if (this.m_ParentMask != null && (rectMask2D != this.m_ParentMask || !rectMask2D.IsActive()))
			{
				this.m_ParentMask.RemoveClippable(this);
				this.UpdateCull(false);
			}
			if (rectMask2D != null && rectMask2D.IsActive())
			{
				rectMask2D.AddClippable(this);
			}
			this.m_ParentMask = rectMask2D;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010081 File Offset: 0x0000E281
		public virtual void RecalculateClipping()
		{
			this.UpdateClipParent();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010089 File Offset: 0x0000E289
		public virtual void RecalculateMasking()
		{
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000100DE File Offset: 0x0000E2DE
		GameObject IClippable.get_gameObject()
		{
			return base.gameObject;
		}

		// Token: 0x040000FB RID: 251
		[NonSerialized]
		protected bool m_ShouldRecalculateStencil = true;

		// Token: 0x040000FC RID: 252
		[NonSerialized]
		protected Material m_MaskMaterial;

		// Token: 0x040000FD RID: 253
		[NonSerialized]
		private RectMask2D m_ParentMask;

		// Token: 0x040000FE RID: 254
		[SerializeField]
		private bool m_Maskable = true;

		// Token: 0x040000FF RID: 255
		private bool m_IsMaskingGraphic;

		// Token: 0x04000100 RID: 256
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore.", true)]
		[NonSerialized]
		protected bool m_IncludeForMasking;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		private MaskableGraphic.CullStateChangedEvent m_OnCullStateChanged = new MaskableGraphic.CullStateChangedEvent();

		// Token: 0x04000102 RID: 258
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore", true)]
		[NonSerialized]
		protected bool m_ShouldRecalculate = true;

		// Token: 0x04000103 RID: 259
		[NonSerialized]
		protected int m_StencilValue;

		// Token: 0x04000104 RID: 260
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x020000A1 RID: 161
		[Serializable]
		public class CullStateChangedEvent : UnityEvent<bool>
		{
		}
	}
}
