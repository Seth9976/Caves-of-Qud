using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000050 RID: 80
	[ExecuteAlways]
	[RequireComponent(typeof(CanvasRenderer))]
	public class TMP_SubMeshUI : MaskableGraphic
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00014A07 File Offset: 0x00012C07
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00014A0F File Offset: 0x00012C0F
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				this.m_fontAsset = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00014A18 File Offset: 0x00012C18
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00014A20 File Offset: 0x00012C20
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00014A29 File Offset: 0x00012C29
		public override Texture mainTexture
		{
			get
			{
				if (this.sharedMaterial != null)
				{
					return this.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex);
				}
				return null;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00014A4B File Offset: 0x00012C4B
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00014A5C File Offset: 0x00012C5C
		public override Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_material = value;
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00014AB3 File Offset: 0x00012CB3
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00014ABB File Offset: 0x00012CBB
		public Material sharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				this.SetSharedMaterial(value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00014AC4 File Offset: 0x00012CC4
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00014ACC File Offset: 0x00012CCC
		public Material fallbackMaterial
		{
			get
			{
				return this.m_fallbackMaterial;
			}
			set
			{
				if (this.m_fallbackMaterial == value)
				{
					return;
				}
				if (this.m_fallbackMaterial != null && this.m_fallbackMaterial != value)
				{
					TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				}
				this.m_fallbackMaterial = value;
				TMP_MaterialManager.AddFallbackMaterialReference(this.m_fallbackMaterial);
				this.SetSharedMaterial(this.m_fallbackMaterial);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00014B2D File Offset: 0x00012D2D
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00014B35 File Offset: 0x00012D35
		public Material fallbackSourceMaterial
		{
			get
			{
				return this.m_fallbackSourceMaterial;
			}
			set
			{
				this.m_fallbackSourceMaterial = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00014B3E File Offset: 0x00012D3E
		public override Material materialForRendering
		{
			get
			{
				return TMP_MaterialManager.GetMaterialForRendering(this, this.m_sharedMaterial);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00014B4C File Offset: 0x00012D4C
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00014B54 File Offset: 0x00012D54
		public bool isDefaultMaterial
		{
			get
			{
				return this.m_isDefaultMaterial;
			}
			set
			{
				this.m_isDefaultMaterial = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00014B5D File Offset: 0x00012D5D
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00014B65 File Offset: 0x00012D65
		public float padding
		{
			get
			{
				return this.m_padding;
			}
			set
			{
				this.m_padding = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00014B6E File Offset: 0x00012D6E
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00014B9C File Offset: 0x00012D9C
		public Mesh mesh
		{
			get
			{
				if (this.m_mesh == null)
				{
					this.m_mesh = new Mesh();
					this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_mesh;
			}
			set
			{
				this.m_mesh = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00014BA5 File Offset: 0x00012DA5
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshProUGUI>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public static TMP_SubMeshUI AddSubTextObject(TextMeshProUGUI textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject();
			gameObject.hideFlags = (TMP_Settings.hideSubTextObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.transform.SetAsFirstSibling();
			gameObject.layer = textComponent.gameObject.layer;
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.pivot = textComponent.rectTransform.pivot;
			gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
			TMP_SubMeshUI tmp_SubMeshUI = gameObject.AddComponent<TMP_SubMeshUI>();
			tmp_SubMeshUI.m_TextComponent = textComponent;
			tmp_SubMeshUI.m_materialReferenceIndex = materialReference.index;
			tmp_SubMeshUI.m_fontAsset = materialReference.fontAsset;
			tmp_SubMeshUI.m_spriteAsset = materialReference.spriteAsset;
			tmp_SubMeshUI.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tmp_SubMeshUI.maskable = textComponent.maskable;
			tmp_SubMeshUI.SetSharedMaterial(materialReference.material);
			return tmp_SubMeshUI;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00014CB2 File Offset: 0x00012EB2
		protected override void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != HideFlags.DontSave)
			{
				base.hideFlags = HideFlags.DontSave;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00014CE8 File Offset: 0x00012EE8
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00014D10 File Offset: 0x00012F10
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(this.m_MaskMaterial);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			this.RecalculateClipping();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetAllDirty();
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014DA1 File Offset: 0x00012FA1
		protected override void OnTransformParentChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00014DC0 File Offset: 0x00012FC0
		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			Material material = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				Transform transform = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
				this.m_StencilValue = (base.maskable ? MaskUtilities.GetStencilDepth(base.transform, transform) : 0);
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0)
			{
				Material material2 = StencilMaterial.Add(material, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = material2;
				material = this.m_MaskMaterial;
			}
			return material;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00014E50 File Offset: 0x00013050
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014E73 File Offset: 0x00013073
		public float GetPaddingForMaterial(Material mat)
		{
			return ShaderUtilities.GetPadding(mat, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00014E91 File Offset: 0x00013091
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014EA6 File Offset: 0x000130A6
		public override void SetAllDirty()
		{
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00014EA8 File Offset: 0x000130A8
		public override void SetVerticesDirty()
		{
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00014EAA File Offset: 0x000130AA
		public override void SetLayoutDirty()
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00014EAC File Offset: 0x000130AC
		public override void SetMaterialDirty()
		{
			this.m_materialDirty = true;
			this.UpdateMaterial();
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00014ECE File Offset: 0x000130CE
		public void SetPivotDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			base.rectTransform.pivot = this.m_TextComponent.rectTransform.pivot;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00014EF4 File Offset: 0x000130F4
		private Transform GetRootCanvasTransform()
		{
			if (this.m_RootCanvasTransform == null)
			{
				this.m_RootCanvasTransform = this.m_TextComponent.canvas.rootCanvas.transform;
			}
			return this.m_RootCanvasTransform;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014F25 File Offset: 0x00013125
		public override void Cull(Rect clipRect, bool validRect)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00014F27 File Offset: 0x00013127
		protected override void UpdateGeometry()
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00014F29 File Offset: 0x00013129
		public override void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				if (!this.m_materialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_materialDirty = false;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00014F45 File Offset: 0x00013145
		public void RefreshMaterial()
		{
			this.UpdateMaterial();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00014F50 File Offset: 0x00013150
		protected override void UpdateMaterial()
		{
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ShaderTag_CullMode) && this.textComponent.fontSharedMaterial != null)
			{
				float @float = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, @float);
			}
			base.canvasRenderer.materialCount = 1;
			base.canvasRenderer.SetMaterial(this.materialForRendering, 0);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00014FD6 File Offset: 0x000131D6
		public override void RecalculateClipping()
		{
			base.RecalculateClipping();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00014FDE File Offset: 0x000131DE
		private Material GetMaterial()
		{
			return this.m_sharedMaterial;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00014FE8 File Offset: 0x000131E8
		private Material GetMaterial(Material mat)
		{
			if (this.m_material == null || this.m_material.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_material = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_material;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001504D File Offset: 0x0001324D
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00015077 File Offset: 0x00013277
		private Material GetSharedMaterial()
		{
			return base.canvasRenderer.GetMaterial();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00015084 File Offset: 0x00013284
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_Material = this.m_sharedMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x04000327 RID: 807
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x04000328 RID: 808
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x04000329 RID: 809
		[SerializeField]
		private Material m_material;

		// Token: 0x0400032A RID: 810
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x0400032B RID: 811
		private Material m_fallbackMaterial;

		// Token: 0x0400032C RID: 812
		private Material m_fallbackSourceMaterial;

		// Token: 0x0400032D RID: 813
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x0400032E RID: 814
		[SerializeField]
		private float m_padding;

		// Token: 0x0400032F RID: 815
		private Mesh m_mesh;

		// Token: 0x04000330 RID: 816
		[SerializeField]
		private TextMeshProUGUI m_TextComponent;

		// Token: 0x04000331 RID: 817
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x04000332 RID: 818
		private bool m_materialDirty;

		// Token: 0x04000333 RID: 819
		[SerializeField]
		private int m_materialReferenceIndex;

		// Token: 0x04000334 RID: 820
		private Transform m_RootCanvasTransform;
	}
}
