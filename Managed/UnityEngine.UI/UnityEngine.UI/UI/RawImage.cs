using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000031 RID: 49
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Raw Image", 12)]
	public class RawImage : MaskableGraphic
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00010524 File Offset: 0x0000E724
		protected RawImage()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00010554 File Offset: 0x0000E754
		public override Texture mainTexture
		{
			get
			{
				if (!(this.m_Texture == null))
				{
					return this.m_Texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000105A8 File Offset: 0x0000E7A8
		// (set) Token: 0x0600031D RID: 797 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				if (this.m_Texture == value)
				{
					return;
				}
				this.m_Texture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000105D4 File Offset: 0x0000E7D4
		// (set) Token: 0x0600031F RID: 799 RVA: 0x000105DC File Offset: 0x0000E7DC
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000105FC File Offset: 0x0000E7FC
		public override void SetNativeSize()
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2((float)num, (float)num2);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001067C File Offset: 0x0000E87C
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			Texture mainTexture = this.mainTexture;
			vh.Clear();
			if (mainTexture != null)
			{
				Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
				Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
				float num = (float)mainTexture.width * mainTexture.texelSize.x;
				float num2 = (float)mainTexture.height * mainTexture.texelSize.y;
				Color color = this.color;
				vh.AddVert(new Vector3(vector.x, vector.y), color, new Vector2(this.m_UVRect.xMin * num, this.m_UVRect.yMin * num2));
				vh.AddVert(new Vector3(vector.x, vector.w), color, new Vector2(this.m_UVRect.xMin * num, this.m_UVRect.yMax * num2));
				vh.AddVert(new Vector3(vector.z, vector.w), color, new Vector2(this.m_UVRect.xMax * num, this.m_UVRect.yMax * num2));
				vh.AddVert(new Vector3(vector.z, vector.y), color, new Vector2(this.m_UVRect.xMax * num, this.m_UVRect.yMin * num2));
				vh.AddTriangle(0, 1, 2);
				vh.AddTriangle(2, 3, 0);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001082B File Offset: 0x0000EA2B
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetMaterialDirty();
			this.SetVerticesDirty();
			base.SetRaycastDirty();
		}

		// Token: 0x0400010B RID: 267
		[FormerlySerializedAs("m_Tex")]
		[SerializeField]
		private Texture m_Texture;

		// Token: 0x0400010C RID: 268
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);
	}
}
