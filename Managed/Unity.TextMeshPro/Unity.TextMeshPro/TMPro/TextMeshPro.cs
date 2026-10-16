using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000082 RID: 130
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MeshRenderer))]
	[AddComponentMenu("Mesh/TextMeshPro - Text")]
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html")]
	public class TextMeshPro : TMP_Text, ILayoutElement
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0002ED31 File Offset: 0x0002CF31
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0002ED4E File Offset: 0x0002CF4E
		public int sortingLayerID
		{
			get
			{
				if (this.renderer == null)
				{
					return 0;
				}
				return this.m_renderer.sortingLayerID;
			}
			set
			{
				if (this.renderer == null)
				{
					return;
				}
				this.m_renderer.sortingLayerID = value;
				this._SortingLayerID = value;
				this.UpdateSubMeshSortingLayerID(value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0002ED79 File Offset: 0x0002CF79
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0002ED96 File Offset: 0x0002CF96
		public int sortingOrder
		{
			get
			{
				if (this.renderer == null)
				{
					return 0;
				}
				return this.m_renderer.sortingOrder;
			}
			set
			{
				if (this.renderer == null)
				{
					return;
				}
				this.m_renderer.sortingOrder = value;
				this._SortingOrder = value;
				this.UpdateSubMeshSortingOrder(value);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0002EDC1 File Offset: 0x0002CFC1
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0002EDC9 File Offset: 0x0002CFC9
		public override bool autoSizeTextContainer
		{
			get
			{
				return this.m_autoSizeTextContainer;
			}
			set
			{
				if (this.m_autoSizeTextContainer == value)
				{
					return;
				}
				this.m_autoSizeTextContainer = value;
				if (this.m_autoSizeTextContainer)
				{
					TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0002EDF0 File Offset: 0x0002CFF0
		[Obsolete("The TextContainer is now obsolete. Use the RectTransform instead.")]
		public TextContainer textContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0002EDF3 File Offset: 0x0002CFF3
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0002EE15 File Offset: 0x0002D015
		public Renderer renderer
		{
			get
			{
				if (this.m_renderer == null)
				{
					this.m_renderer = base.GetComponent<Renderer>();
				}
				return this.m_renderer;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0002EE37 File Offset: 0x0002D037
		public override Mesh mesh
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
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0002EE68 File Offset: 0x0002D068
		public MeshFilter meshFilter
		{
			get
			{
				if (this.m_meshFilter == null)
				{
					this.m_meshFilter = base.GetComponent<MeshFilter>();
					if (this.m_meshFilter == null)
					{
						this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
						this.m_meshFilter.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
					}
				}
				return this.m_meshFilter;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0002EEC1 File Offset: 0x0002D0C1
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0002EEC9 File Offset: 0x0002D0C9
		public MaskingTypes maskType
		{
			get
			{
				return this.m_maskType;
			}
			set
			{
				this.m_maskType = value;
				this.SetMask(this.m_maskType);
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002EEDE File Offset: 0x0002D0DE
		public void SetMask(MaskingTypes type, Vector4 maskCoords)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0002EEEE File Offset: 0x0002D0EE
		public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords, softnessX, softnessY);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0002EF01 File Offset: 0x0002D101
		public override void SetVerticesDirty()
		{
			if (this == null || !this.IsActive())
			{
				return;
			}
			TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002EF1B File Offset: 0x0002D11B
		public override void SetLayoutDirty()
		{
			this.m_isPreferredWidthDirty = true;
			this.m_isPreferredHeightDirty = true;
			if (this == null || !this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.rectTransform);
			this.m_isLayoutDirty = true;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0002EF4F File Offset: 0x0002D14F
		public override void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0002EF57 File Offset: 0x0002D157
		public override void SetAllDirty()
		{
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0002EF6C File Offset: 0x0002D16C
		public override void Rebuild(CanvasUpdate update)
		{
			if (this == null)
			{
				return;
			}
			if (update == CanvasUpdate.Prelayout)
			{
				if (this.m_autoSizeTextContainer)
				{
					this.m_rectTransform.sizeDelta = base.GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
					return;
				}
			}
			else if (update == CanvasUpdate.PreRender)
			{
				this.OnPreRenderObject();
				if (!this.m_isMaterialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_isMaterialDirty = false;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002EFCC File Offset: 0x0002D1CC
		protected override void UpdateMaterial()
		{
			if (this.renderer == null || this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_renderer.sharedMaterial == null || this.m_renderer.sharedMaterial.GetInstanceID() != this.m_sharedMaterial.GetInstanceID())
			{
				this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0002F038 File Offset: 0x0002D238
		public override void UpdateMeshPadding()
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_havePropertiesChanged = true;
			this.checkPaddingRequired = false;
			if (this.m_textInfo == null)
			{
				return;
			}
			for (int i = 1; i < this.m_textInfo.materialCount; i++)
			{
				this.m_subTextObjects[i].UpdateMeshPadding(this.m_enableExtraPadding, this.m_isUsingBold);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002F0B9 File Offset: 0x0002D2B9
		public override void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
			this.m_havePropertiesChanged = true;
			this.m_ignoreActiveState = ignoreActiveState;
			this.OnPreRenderObject();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002F0CF File Offset: 0x0002D2CF
		public override TMP_TextInfo GetTextInfo(string text)
		{
			base.SetText(text);
			this.SetArraySizes(this.m_TextProcessingArray);
			this.m_renderMode = TextRenderFlags.DontRender;
			this.ComputeMarginSize();
			this.GenerateTextMesh();
			this.m_renderMode = TextRenderFlags.Render;
			return base.textInfo;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002F10C File Offset: 0x0002D30C
		public override void ClearMesh(bool updateMesh)
		{
			if (this.m_textInfo.meshInfo[0].mesh == null)
			{
				this.m_textInfo.meshInfo[0].mesh = this.m_mesh;
			}
			this.m_textInfo.ClearMeshInfo(updateMesh);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000609 RID: 1545 RVA: 0x0002F160 File Offset: 0x0002D360
		// (remove) Token: 0x0600060A RID: 1546 RVA: 0x0002F198 File Offset: 0x0002D398
		public override event Action<TMP_TextInfo> OnPreRenderText;

		// Token: 0x0600060B RID: 1547 RVA: 0x0002F1CD File Offset: 0x0002D3CD
		public override void UpdateGeometry(Mesh mesh, int index)
		{
			mesh.RecalculateBounds();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
		public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					mesh = this.m_subTextObjects[i].mesh;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
				{
					mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
				{
					mesh.SetUVs(0, this.m_textInfo.meshInfo[i].uvs0);
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
				{
					mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
				{
					mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				}
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		public override void UpdateVertexData()
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					this.m_textInfo.meshInfo[i].ClearUnusedVertices();
					mesh = this.m_subTextObjects[i].mesh;
				}
				mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				mesh.SetUVs(0, this.m_textInfo.meshInfo[i].uvs0);
				mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002F382 File Offset: 0x0002D582
		public void UpdateFontAsset()
		{
			this.LoadFontAsset();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002F38A File Offset: 0x0002D58A
		public void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002F38C File Offset: 0x0002D58C
		public void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002F390 File Offset: 0x0002D590
		protected override void Awake()
		{
			this.m_renderer = base.GetComponent<Renderer>();
			if (this.m_renderer == null)
			{
				this.m_renderer = base.gameObject.AddComponent<Renderer>();
			}
			this.m_rectTransform = base.rectTransform;
			this.m_transform = this.transform;
			this.m_meshFilter = base.GetComponent<MeshFilter>();
			if (this.m_meshFilter == null)
			{
				this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.m_mesh == null)
			{
				this.m_mesh = new Mesh();
				this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				this.m_meshFilter.sharedMesh = this.m_mesh;
				this.m_textInfo = new TMP_TextInfo(this);
			}
			this.m_meshFilter.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
			base.LoadDefaultSettings();
			this.LoadFontAsset();
			if (this.m_TextProcessingArray == null)
			{
				this.m_TextProcessingArray = new TMP_Text.TextProcessingElement[this.m_max_characters];
			}
			this.m_cached_TextElement = new TMP_Character();
			this.m_isFirstAllocation = true;
			this.m_havePropertiesChanged = true;
			this.m_isAwake = true;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002F4A0 File Offset: 0x0002D6A0
		protected override void OnEnable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (!this.m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
			this.meshFilter.sharedMesh = this.mesh;
			this.SetActiveSubMeshes(true);
			this.ComputeMarginSize();
			this.SetAllDirty();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002F4F7 File Offset: 0x0002D6F7
		protected override void OnDisable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			this.meshFilter.sharedMesh = null;
			this.SetActiveSubMeshes(false);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002F521 File Offset: 0x0002D721
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			this.m_isRegisteredForEvents = false;
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002F550 File Offset: 0x0002D750
		protected override void LoadFontAsset()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_fontAsset == null)
			{
				if (TMP_Settings.defaultFontAsset != null)
				{
					this.m_fontAsset = TMP_Settings.defaultFontAsset;
				}
				if (this.m_fontAsset == null)
				{
					Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
					return;
				}
				if (this.m_fontAsset.characterLookupTable == null)
				{
					Debug.Log("Dictionary is Null!");
				}
				this.m_sharedMaterial = this.m_fontAsset.material;
				this.m_sharedMaterial.SetFloat("_CullMode", 0f);
				this.m_renderer.receiveShadows = false;
				this.m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
			else
			{
				if (this.m_fontAsset.characterLookupTable == null)
				{
					this.m_fontAsset.ReadFontAssetDefinition();
				}
				if (this.m_sharedMaterial == null || this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || this.m_fontAsset.atlasTexture.GetInstanceID() != this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					if (this.m_fontAsset.material == null)
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"The Font Atlas Texture of the Font Asset ",
							this.m_fontAsset.name,
							" assigned to ",
							base.gameObject.name,
							" is missing."
						}), this);
					}
					else
					{
						this.m_sharedMaterial = this.m_fontAsset.material;
					}
				}
			}
			this.ValidateEnvMapProperty();
			this.m_padding = this.GetPaddingForMaterial();
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			base.GetSpecialCharacters(this.m_fontAsset);
			this.SetMaterialDirty();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002F71C File Offset: 0x0002D91C
		private void ValidateEnvMapProperty()
		{
			if (this.m_sharedMaterial != null)
			{
				this.m_hasEnvMapProperty = this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) && this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) != null;
				return;
			}
			this.m_hasEnvMapProperty = false;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002F770 File Offset: 0x0002D970
		private void UpdateEnvMapMatrix()
		{
			if (!this.m_hasEnvMapProperty)
			{
				return;
			}
			Vector3 vector = this.m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
			if (this.m_currentEnvMapRotation == vector)
			{
				return;
			}
			this.m_currentEnvMapRotation = vector;
			this.m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(vector), Vector3.one);
			this.m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, this.m_EnvMapMatrix);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
		private void SetMask(MaskingTypes maskType)
		{
			switch (maskType)
			{
			case MaskingTypes.MaskOff:
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskHard:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskSoft:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002F896 File Offset: 0x0002DA96
		private void SetMaskCoordinates(Vector4 coords)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002F8A9 File Offset: 0x0002DAA9
		private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002F8E0 File Offset: 0x0002DAE0
		private void EnableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = true;
				this.UpdateMask();
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002F93C File Offset: 0x0002DB3C
		private void DisableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = false;
				this.UpdateMask();
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002F998 File Offset: 0x0002DB98
		private void UpdateMask()
		{
			if (!this.m_isMaskingEnabled)
			{
				return;
			}
			if (this.m_isMaskingEnabled && this.m_fontMaterial == null)
			{
				this.CreateMaterialInstance();
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		protected override Material GetMaterial(Material mat)
		{
			if (this.m_fontMaterial == null || this.m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_fontMaterial = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002FA28 File Offset: 0x0002DC28
		protected override Material[] GetMaterials(Material[] mats)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontMaterials == null)
			{
				this.m_fontMaterials = new Material[materialCount];
			}
			else if (this.m_fontMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontMaterials[i] = base.fontMaterial;
				}
				else
				{
					this.m_fontMaterials[i] = this.m_subTextObjects[i].material;
				}
			}
			this.m_fontSharedMaterials = this.m_fontMaterials;
			return this.m_fontMaterials;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002FAB5 File Offset: 0x0002DCB5
		protected override void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002FAD0 File Offset: 0x0002DCD0
		protected override Material[] GetSharedMaterials()
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontSharedMaterials[i] = this.m_sharedMaterial;
				}
				else
				{
					this.m_fontSharedMaterials[i] = this.m_subTextObjects[i].sharedMaterial;
				}
			}
			return this.m_fontSharedMaterials;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002FB54 File Offset: 0x0002DD54
		protected override void SetSharedMaterials(Material[] materials)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				Texture texture = materials[i].GetTexture(ShaderUtilities.ID_MainTex);
				if (i == 0)
				{
					if (!(texture == null) && texture.GetInstanceID() == this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						this.m_sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
						this.m_padding = this.GetPaddingForMaterial(this.m_sharedMaterial);
					}
				}
				else if (!(texture == null) && texture.GetInstanceID() == this.m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && this.m_subTextObjects[i].isDefaultMaterial)
				{
					this.m_subTextObjects[i].sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002FC68 File Offset: 0x0002DE68
		protected override void SetOutlineThickness(float thickness)
		{
			thickness = Mathf.Clamp01(thickness);
			this.m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_fontMaterial = this.m_renderer.material;
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002FCDC File Offset: 0x0002DEDC
		protected override void SetFaceColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002FD30 File Offset: 0x0002DF30
		protected override void SetOutlineColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002FD84 File Offset: 0x0002DF84
		private void CreateMaterialInstance()
		{
			Material material = new Material(this.m_sharedMaterial);
			material.shaderKeywords = this.m_sharedMaterial.shaderKeywords;
			Material material2 = material;
			material2.name += " Instance";
			this.m_fontMaterial = material;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002FDCC File Offset: 0x0002DFCC
		protected override void SetShaderDepth()
		{
			if (this.m_isOverlay)
			{
				Material material = this.m_renderer.material;
				this.m_sharedMaterial = material;
				return;
			}
			Material material2 = this.m_renderer.material;
			this.m_sharedMaterial = material2;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002FE08 File Offset: 0x0002E008
		protected override void SetCulling()
		{
			if (this.m_isCullingEnabled)
			{
				this.m_renderer.material.SetFloat("_CullMode", 2f);
				for (int i = 1; i < this.m_subTextObjects.Length; i++)
				{
					if (!(this.m_subTextObjects[i] != null))
					{
						return;
					}
					Renderer renderer = this.m_subTextObjects[i].renderer;
					if (renderer != null)
					{
						renderer.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
					}
				}
			}
			else
			{
				this.m_renderer.material.SetFloat("_CullMode", 0f);
				int num = 1;
				while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
				{
					Renderer renderer2 = this.m_subTextObjects[num].renderer;
					if (renderer2 != null)
					{
						renderer2.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
					}
					num++;
				}
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		private void SetPerspectiveCorrection()
		{
			if (this.m_isOrthographic)
			{
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
				return;
			}
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002FF28 File Offset: 0x0002E128
		internal override int SetArraySizes(TMP_Text.TextProcessingElement[] textProcessingArray)
		{
			int num = 0;
			this.m_totalCharacterCount = 0;
			this.m_isUsingBold = false;
			this.m_isTextLayoutPhase = false;
			this.tag_NoParsing = false;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_fontStyleStack.Clear();
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			TMP_Text.m_materialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
			if (this.m_textInfo == null)
			{
				this.m_textInfo = new TMP_TextInfo(this.m_InternalTextProcessingArraySize);
			}
			else if (this.m_textInfo.characterInfo.Length < this.m_InternalTextProcessingArraySize)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_InternalTextProcessingArraySize, false);
			}
			this.m_textElementType = TMP_TextElementType.Character;
			if (this.m_overflowMode == TextOverflowModes.Ellipsis)
			{
				base.GetEllipsisSpecialCharacter(this.m_currentFontAsset);
				if (this.m_Ellipsis.character != null)
				{
					if (this.m_Ellipsis.fontAsset.GetInstanceID() != this.m_currentFontAsset.GetInstanceID())
					{
						if (TMP_Settings.matchMaterialPreset && this.m_currentMaterial.GetInstanceID() != this.m_Ellipsis.fontAsset.material.GetInstanceID())
						{
							this.m_Ellipsis.material = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_Ellipsis.fontAsset.material);
						}
						else
						{
							this.m_Ellipsis.material = this.m_Ellipsis.fontAsset.material;
						}
						this.m_Ellipsis.materialIndex = MaterialReference.AddMaterialReference(this.m_Ellipsis.material, this.m_Ellipsis.fontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferences[this.m_Ellipsis.materialIndex].referenceCount = 0;
					}
				}
				else
				{
					this.m_overflowMode = TextOverflowModes.Truncate;
					if (!TMP_Settings.warningsDisabled)
					{
						Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + this.m_currentFontAsset.name + "] or any potential fallbacks. Switching Text Overflow mode to Truncate.", this);
					}
				}
			}
			bool flag = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.liga);
			if (this.m_overflowMode == TextOverflowModes.Linked && this.m_linkedTextComponent != null && !this.m_isCalculatingPreferredValues)
			{
				this.m_linkedTextComponent.text = string.Empty;
			}
			int num2 = 0;
			while (num2 < textProcessingArray.Length && textProcessingArray[num2].unicode != 0U)
			{
				if (this.m_textInfo.characterInfo == null || this.m_totalCharacterCount >= this.m_textInfo.characterInfo.Length)
				{
					TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_totalCharacterCount + 1, true);
				}
				uint num3 = textProcessingArray[num2].unicode;
				if (!this.m_isRichText || num3 != 60U)
				{
					goto IL_047F;
				}
				int currentMaterialIndex = this.m_currentMaterialIndex;
				int num4;
				if (!base.ValidateHtmlTag(textProcessingArray, num2 + 1, out num4))
				{
					goto IL_047F;
				}
				int stringIndex = textProcessingArray[num2].stringIndex;
				num2 = num4;
				if ((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					this.m_isUsingBold = true;
				}
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					MaterialReference[] materialReferences = TMP_Text.m_materialReferences;
					int currentMaterialIndex2 = this.m_currentMaterialIndex;
					materialReferences[currentMaterialIndex2].referenceCount = materialReferences[currentMaterialIndex2].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)(57344 + this.m_spriteIndex);
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = this.m_textElementType;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = stringIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = textProcessingArray[num2].stringIndex - stringIndex + 1;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = currentMaterialIndex;
					num++;
					this.m_totalCharacterCount++;
				}
				IL_0DAE:
				num2++;
				continue;
				IL_047F:
				bool flag2 = false;
				bool flag3 = false;
				TMP_FontAsset currentFontAsset = this.m_currentFontAsset;
				Material currentMaterial = this.m_currentMaterial;
				int currentMaterialIndex3 = this.m_currentMaterialIndex;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num3))
						{
							num3 = (uint)char.ToUpper((char)num3);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num3))
						{
							num3 = (uint)char.ToLower((char)num3);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num3))
					{
						num3 = (uint)char.ToUpper((char)num3);
					}
				}
				TMP_TextElement tmp_TextElement = null;
				uint num5 = ((num2 + 1 < textProcessingArray.Length) ? textProcessingArray[num2 + 1].unicode : 0U);
				if (base.emojiFallbackSupport && ((TMP_TextParsingUtilities.IsEmojiPresentationForm(num3) && num5 != 65038U) || (TMP_TextParsingUtilities.IsEmoji(num3) && num5 == 65039U)) && TMP_Settings.emojiFallbackTextAssets != null && TMP_Settings.emojiFallbackTextAssets.Count > 0)
				{
					tmp_TextElement = TMP_FontAssetUtilities.GetTextElementFromTextAssets(num3, this.m_currentFontAsset, TMP_Settings.emojiFallbackTextAssets, true, base.fontStyle, base.fontWeight, out flag2);
				}
				if (tmp_TextElement == null)
				{
					tmp_TextElement = base.GetTextElement(num3, this.m_currentFontAsset, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag2);
				}
				if (tmp_TextElement == null)
				{
					base.DoMissingGlyphCallback((int)num3, textProcessingArray[num2].stringIndex, this.m_currentFontAsset);
					uint num6 = num3;
					num3 = (textProcessingArray[num2].unicode = (uint)((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
					tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset(num3, this.m_currentFontAsset, true, FontStyles.Normal, FontWeight.Regular, out flag2);
					if (tmp_TextElement == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
					{
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAssets(num3, this.m_currentFontAsset, TMP_Settings.fallbackFontAssets, true, FontStyles.Normal, FontWeight.Regular, out flag2);
					}
					if (tmp_TextElement == null && TMP_Settings.defaultFontAsset != null)
					{
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset(num3, TMP_Settings.defaultFontAsset, true, FontStyles.Normal, FontWeight.Regular, out flag2);
					}
					if (tmp_TextElement == null)
					{
						num3 = (textProcessingArray[num2].unicode = 32U);
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset(num3, this.m_currentFontAsset, true, FontStyles.Normal, FontWeight.Regular, out flag2);
					}
					if (tmp_TextElement == null)
					{
						num3 = (textProcessingArray[num2].unicode = 3U);
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset(num3, this.m_currentFontAsset, true, FontStyles.Normal, FontWeight.Regular, out flag2);
					}
					if (!TMP_Settings.warningsDisabled)
					{
						Debug.LogWarning((num6 > 65535U) ? string.Format("The character with Unicode value \\U{0:X8} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4} in text object [{3}].", new object[]
						{
							num6,
							this.m_fontAsset.name,
							tmp_TextElement.unicode,
							base.name
						}) : string.Format("The character with Unicode value \\u{0:X4} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4} in text object [{3}].", new object[]
						{
							num6,
							this.m_fontAsset.name,
							tmp_TextElement.unicode,
							base.name
						}), this);
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].alternativeGlyph = null;
				if (tmp_TextElement.elementType == TextElementType.Character)
				{
					if (tmp_TextElement.textAsset.instanceID != this.m_currentFontAsset.instanceID)
					{
						flag3 = true;
						this.m_currentFontAsset = tmp_TextElement.textAsset as TMP_FontAsset;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentFontAsset.material, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					}
					if ((num5 >= 65024U && num5 <= 65039U) || (num5 >= 917760U && num5 <= 917999U))
					{
						uint glyphVariantIndex = this.m_currentFontAsset.GetGlyphVariantIndex(num3, num5);
						Glyph glyph;
						if (glyphVariantIndex != 0U && this.m_currentFontAsset.TryAddGlyphInternal(glyphVariantIndex, out glyph))
						{
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].alternativeGlyph = glyph;
						}
						textProcessingArray[num2 + 1].unicode = 26U;
						num2++;
					}
					List<LigatureSubstitutionRecord> list;
					if (flag && this.m_currentFontAsset.fontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(tmp_TextElement.glyphIndex, out list))
					{
						if (list == null)
						{
							break;
						}
						for (int i = 0; i < list.Count; i++)
						{
							LigatureSubstitutionRecord ligatureSubstitutionRecord = list[i];
							int num7 = ligatureSubstitutionRecord.componentGlyphIDs.Length;
							uint num8 = ligatureSubstitutionRecord.ligatureGlyphID;
							for (int j = 1; j < num7; j++)
							{
								uint unicode = textProcessingArray[num2 + j].unicode;
								if (this.m_currentFontAsset.GetGlyphIndex(unicode) != ligatureSubstitutionRecord.componentGlyphIDs[j])
								{
									num8 = 0U;
									break;
								}
							}
							Glyph glyph2;
							if (num8 != 0U && this.m_currentFontAsset.TryAddGlyphInternal(num8, out glyph2))
							{
								this.m_textInfo.characterInfo[this.m_totalCharacterCount].alternativeGlyph = glyph2;
								for (int k = 0; k < num7; k++)
								{
									if (k == 0)
									{
										textProcessingArray[num2 + k].length = num7;
									}
									else
									{
										textProcessingArray[num2 + k].unicode = 26U;
									}
								}
								num2 += num7 - 1;
								break;
							}
						}
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Character;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = tmp_TextElement;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].isUsingAlternateTypeface = flag2;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)num3;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = textProcessingArray[num2].stringIndex;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = textProcessingArray[num2].length;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
				if (tmp_TextElement.elementType == TextElementType.Sprite)
				{
					TMP_SpriteAsset tmp_SpriteAsset = tmp_TextElement.textAsset as TMP_SpriteAsset;
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(tmp_SpriteAsset.material, tmp_SpriteAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					MaterialReference[] materialReferences2 = TMP_Text.m_materialReferences;
					int currentMaterialIndex4 = this.m_currentMaterialIndex;
					materialReferences2[currentMaterialIndex4].referenceCount = materialReferences2[currentMaterialIndex4].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Sprite;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = currentMaterialIndex3;
					num++;
					this.m_totalCharacterCount++;
					goto IL_0DAE;
				}
				if (flag3 && this.m_currentFontAsset.instanceID != this.m_fontAsset.instanceID)
				{
					if (TMP_Settings.matchMaterialPreset)
					{
						this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_currentFontAsset.material);
					}
					else
					{
						this.m_currentMaterial = this.m_currentFontAsset.material;
					}
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
				}
				if (tmp_TextElement != null && tmp_TextElement.glyph.atlasIndex > 0)
				{
					this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentFontAsset, this.m_currentMaterial, tmp_TextElement.glyph.atlasIndex);
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					flag3 = true;
				}
				if (!char.IsWhiteSpace((char)num3) && num3 != 8203U)
				{
					if (TMP_Text.m_materialReferences[this.m_currentMaterialIndex].referenceCount < 16383)
					{
						MaterialReference[] materialReferences3 = TMP_Text.m_materialReferences;
						int currentMaterialIndex5 = this.m_currentMaterialIndex;
						materialReferences3[currentMaterialIndex5].referenceCount = materialReferences3[currentMaterialIndex5].referenceCount + 1;
					}
					else if (flag3)
					{
						int num9;
						if (this.materialIndexPairs.TryGetValue(this.m_currentMaterialIndex, out num9) && TMP_Text.m_materialReferences[num9].referenceCount < 16383)
						{
							this.m_currentMaterialIndex = num9;
						}
						else
						{
							int num10 = MaterialReference.AddMaterialReference(new Material(this.m_currentMaterial), this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
							this.materialIndexPairs[this.m_currentMaterialIndex] = num10;
							this.m_currentMaterialIndex = num10;
						}
						MaterialReference[] materialReferences4 = TMP_Text.m_materialReferences;
						int currentMaterialIndex6 = this.m_currentMaterialIndex;
						materialReferences4[currentMaterialIndex6].referenceCount = materialReferences4[currentMaterialIndex6].referenceCount + 1;
					}
					else
					{
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_currentMaterial), this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						MaterialReference[] materialReferences5 = TMP_Text.m_materialReferences;
						int currentMaterialIndex7 = this.m_currentMaterialIndex;
						materialReferences5[currentMaterialIndex7].referenceCount = materialReferences5[currentMaterialIndex7].referenceCount + 1;
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].material = this.m_currentMaterial;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
				TMP_Text.m_materialReferences[this.m_currentMaterialIndex].isFallbackMaterial = flag3;
				if (flag3)
				{
					TMP_Text.m_materialReferences[this.m_currentMaterialIndex].fallbackMaterial = currentMaterial;
					this.m_currentFontAsset = currentFontAsset;
					this.m_currentMaterial = currentMaterial;
					this.m_currentMaterialIndex = currentMaterialIndex3;
				}
				this.m_totalCharacterCount++;
				goto IL_0DAE;
			}
			if (this.m_isCalculatingPreferredValues)
			{
				this.m_isCalculatingPreferredValues = false;
				return this.m_totalCharacterCount;
			}
			this.m_textInfo.spriteCount = num;
			int num11 = (this.m_textInfo.materialCount = TMP_Text.m_materialReferenceIndexLookup.Count);
			if (num11 > this.m_textInfo.meshInfo.Length)
			{
				TMP_TextInfo.Resize<TMP_MeshInfo>(ref this.m_textInfo.meshInfo, num11, false);
			}
			if (num11 > this.m_subTextObjects.Length)
			{
				TMP_TextInfo.Resize<TMP_SubMesh>(ref this.m_subTextObjects, Mathf.NextPowerOfTwo(num11 + 1));
			}
			if (this.m_VertexBufferAutoSizeReduction && this.m_textInfo.characterInfo.Length - this.m_totalCharacterCount > 256)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, Mathf.Max(this.m_totalCharacterCount + 1, 256), true);
			}
			for (int l = 0; l < num11; l++)
			{
				if (l > 0)
				{
					if (this.m_subTextObjects[l] == null)
					{
						this.m_subTextObjects[l] = TMP_SubMesh.AddSubTextObject(this, TMP_Text.m_materialReferences[l]);
						this.m_textInfo.meshInfo[l].vertices = null;
					}
					if (this.m_subTextObjects[l].sharedMaterial == null || this.m_subTextObjects[l].sharedMaterial.GetInstanceID() != TMP_Text.m_materialReferences[l].material.GetInstanceID())
					{
						this.m_subTextObjects[l].sharedMaterial = TMP_Text.m_materialReferences[l].material;
						this.m_subTextObjects[l].fontAsset = TMP_Text.m_materialReferences[l].fontAsset;
						this.m_subTextObjects[l].spriteAsset = TMP_Text.m_materialReferences[l].spriteAsset;
					}
					if (TMP_Text.m_materialReferences[l].isFallbackMaterial)
					{
						this.m_subTextObjects[l].fallbackMaterial = TMP_Text.m_materialReferences[l].material;
						this.m_subTextObjects[l].fallbackSourceMaterial = TMP_Text.m_materialReferences[l].fallbackMaterial;
					}
				}
				int referenceCount = TMP_Text.m_materialReferences[l].referenceCount;
				if (this.m_textInfo.meshInfo[l].vertices == null || this.m_textInfo.meshInfo[l].vertices.Length < referenceCount * 4)
				{
					if (this.m_textInfo.meshInfo[l].vertices == null)
					{
						if (l == 0)
						{
							this.m_textInfo.meshInfo[l] = new TMP_MeshInfo(this.m_mesh, referenceCount + 1);
						}
						else
						{
							this.m_textInfo.meshInfo[l] = new TMP_MeshInfo(this.m_subTextObjects[l].mesh, referenceCount + 1);
						}
					}
					else
					{
						this.m_textInfo.meshInfo[l].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
					}
				}
				else if (this.m_VertexBufferAutoSizeReduction && referenceCount > 0 && this.m_textInfo.meshInfo[l].vertices.Length / 4 - referenceCount > 256)
				{
					this.m_textInfo.meshInfo[l].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
				}
				this.m_textInfo.meshInfo[l].material = TMP_Text.m_materialReferences[l].material;
			}
			int num12 = num11;
			while (num12 < this.m_subTextObjects.Length && this.m_subTextObjects[num12] != null)
			{
				if (num12 < this.m_textInfo.meshInfo.Length)
				{
					this.m_textInfo.meshInfo[num12].ClearUnusedVertices(0, true);
				}
				num12++;
			}
			return this.m_totalCharacterCount;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000310EC File Offset: 0x0002F2EC
		public override void ComputeMarginSize()
		{
			if (base.rectTransform != null)
			{
				Rect rect = this.m_rectTransform.rect;
				this.m_marginWidth = rect.width - this.m_margin.x - this.m_margin.z;
				this.m_marginHeight = rect.height - this.m_margin.y - this.m_margin.w;
				this.m_PreviousRectTransformSize = rect.size;
				this.m_PreviousPivotPosition = this.m_rectTransform.pivot;
				this.m_RectTransformCorners = this.GetTextContainerLocalCorners();
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0003118A File Offset: 0x0002F38A
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_havePropertiesChanged = true;
			this.isMaskUpdateRequired = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000311A0 File Offset: 0x0002F3A0
		protected override void OnTransformParentChanged()
		{
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000311B0 File Offset: 0x0002F3B0
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.rectTransform != null && Mathf.Abs(this.m_rectTransform.rect.width - this.m_PreviousRectTransformSize.x) < 0.0001f && Mathf.Abs(this.m_rectTransform.rect.height - this.m_PreviousRectTransformSize.y) < 0.0001f && Mathf.Abs(this.m_rectTransform.pivot.x - this.m_PreviousPivotPosition.x) < 0.0001f && Mathf.Abs(this.m_rectTransform.pivot.y - this.m_PreviousPivotPosition.y) < 0.0001f)
			{
				return;
			}
			this.ComputeMarginSize();
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00031288 File Offset: 0x0002F488
		internal override void InternalUpdate()
		{
			if (!this.m_havePropertiesChanged)
			{
				float y = this.m_rectTransform.lossyScale.y;
				if (y != this.m_previousLossyScaleY && this.m_TextProcessingArray[0].unicode != 0U)
				{
					float num = y / this.m_previousLossyScaleY;
					if (num < 0.8f || num > 1.25f)
					{
						this.UpdateSDFScale(num);
						this.m_previousLossyScaleY = y;
					}
				}
			}
			if (this.m_isUsingLegacyAnimationComponent)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
			}
			this.UpdateEnvMapMatrix();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0003130C File Offset: 0x0002F50C
		private void OnPreRenderObject()
		{
			if (!this.m_isAwake || (!this.IsActive() && !this.m_ignoreActiveState))
			{
				return;
			}
			if (this.m_fontAsset == null)
			{
				Debug.LogWarning("Please assign a Font Asset to this " + this.transform.name + " gameobject.", this);
				return;
			}
			if (this.m_havePropertiesChanged || this.m_isLayoutDirty)
			{
				if (this.isMaskUpdateRequired)
				{
					this.UpdateMask();
					this.isMaskUpdateRequired = false;
				}
				if (this.checkPaddingRequired)
				{
					this.UpdateMeshPadding();
				}
				base.ParseInputText();
				TMP_FontAsset.UpdateFontAssetsInUpdateQueue();
				if (this.m_enableAutoSizing)
				{
					this.m_fontSize = Mathf.Clamp(this.m_fontSizeBase, this.m_fontSizeMin, this.m_fontSizeMax);
				}
				this.m_maxFontSize = this.m_fontSizeMax;
				this.m_minFontSize = this.m_fontSizeMin;
				this.m_lineSpacingDelta = 0f;
				this.m_charWidthAdjDelta = 0f;
				this.m_isTextTruncated = false;
				this.m_havePropertiesChanged = false;
				this.m_isLayoutDirty = false;
				this.m_ignoreActiveState = false;
				this.m_IsAutoSizePointSizeSet = false;
				this.m_AutoSizeIterationCount = 0;
				this.SetActiveSubTextObjectRenderers(this.m_renderer.enabled);
				while (!this.m_IsAutoSizePointSizeSet)
				{
					this.GenerateTextMesh();
					this.m_AutoSizeIterationCount++;
				}
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00031450 File Offset: 0x0002F650
		protected virtual void GenerateTextMesh()
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID().ToString());
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			if (this.m_textInfo != null)
			{
				this.m_textInfo.Clear();
			}
			if (this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0U)
			{
				this.ClearMesh(true);
				this.m_preferredWidth = 0f;
				this.m_preferredHeight = 0f;
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			this.m_currentSpriteAsset = this.m_spriteAsset;
			if (this.m_spriteAnimator != null)
			{
				this.m_spriteAnimator.StopAllAnimations();
			}
			int totalCharacterCount = this.m_totalCharacterCount;
			float num = this.m_fontSize / this.m_fontAsset.m_FaceInfo.pointSize * this.m_fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
			float num2 = num;
			float num3 = this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = this.m_fontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			uint num4 = 0U;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_fontStyleStack.Clear();
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			float num5 = 0f;
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			bool flag = false;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			bool flag2 = false;
			Vector3 zero3 = Vector3.zero;
			Vector3 zero4 = Vector3.zero;
			bool flag3 = false;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			this.m_fontColor32 = this.m_fontColor;
			this.m_htmlColor = this.m_fontColor32;
			this.m_underlineColor = this.m_htmlColor;
			this.m_strikethroughColor = this.m_htmlColor;
			this.m_colorStack.SetDefault(this.m_htmlColor);
			this.m_underlineColorStack.SetDefault(this.m_htmlColor);
			this.m_strikethroughColorStack.SetDefault(this.m_htmlColor);
			this.m_HighlightStateStack.SetDefault(new HighlightState(this.m_htmlColor, TMP_Offset.zero));
			this.m_colorGradientPreset = null;
			this.m_colorGradientStack.SetDefault(null);
			this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
			this.m_ItalicAngleStack.SetDefault(this.m_ItalicAngle);
			this.m_actionStack.Clear();
			this.m_FXScale = Vector3.one;
			this.m_FXRotation = Quaternion.identity;
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float num6 = this.m_currentFontAsset.m_FaceInfo.lineHeight - (this.m_currentFontAsset.m_FaceInfo.ascentLine - this.m_currentFontAsset.m_FaceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = this.m_firstVisibleCharacter;
			this.m_lastCharacterOfLine = 0;
			this.m_firstVisibleCharacterOfLine = 0;
			this.m_lastVisibleCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			this.m_startOfLineDescender = 0f;
			this.m_lineVisibleCharacterCount = 0;
			this.m_lineVisibleSpaceCount = 0;
			bool flag4 = true;
			this.m_IsDrivenLineSpacing = false;
			this.m_firstOverflowCharacterIndex = -1;
			this.m_LastBaseGlyphIndex = int.MinValue;
			bool flag5 = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.kern);
			bool flag6 = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.mark);
			bool flag7 = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.mkmk);
			this.m_pageNumber = 0;
			int num7 = Mathf.Clamp(this.m_pageToDisplay - 1, 0, this.m_textInfo.pageInfo.Length - 1);
			this.m_textInfo.ClearPageInfo();
			Vector4 margin = this.m_margin;
			float num8 = ((this.m_marginWidth > 0f) ? this.m_marginWidth : 0f);
			float num9 = ((this.m_marginHeight > 0f) ? this.m_marginHeight : 0f);
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			this.m_width = -1f;
			float num10 = num8 + 0.0001f - this.m_marginLeft - this.m_marginRight;
			this.m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
			this.m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
			this.m_textInfo.ClearLineInfo();
			this.m_maxCapHeight = 0f;
			this.m_maxTextAscender = 0f;
			this.m_ElementDescender = 0f;
			this.m_PageAscender = 0f;
			float num11 = 0f;
			bool flag8 = false;
			this.m_isNewPage = false;
			bool flag9 = true;
			this.m_isNonBreakingSpace = false;
			bool flag10 = false;
			int num12 = 0;
			TMP_Text.CharacterSubstitution characterSubstitution = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool flag11 = false;
			base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedEllipsisState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, -1, -1);
			TMP_Text.m_EllipsisInsertionCandidateStack.Clear();
			int num13 = 0;
			int num14 = 0;
			while (num14 < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[num14].unicode != 0U)
			{
				num4 = this.m_TextProcessingArray[num14].unicode;
				if (num13 > 5)
				{
					Debug.LogError("Line breaking recursion max threshold hit... Character [" + num4.ToString() + "] index: " + num14.ToString());
					characterSubstitution.index = this.m_characterCount;
					characterSubstitution.unicode = 3U;
				}
				if (num4 != 26U)
				{
					if (this.m_isRichText && num4 == 60U)
					{
						this.m_isTextLayoutPhase = true;
						this.m_textElementType = TMP_TextElementType.Character;
						int num15;
						if (base.ValidateHtmlTag(this.m_TextProcessingArray, num14 + 1, out num15))
						{
							num14 = num15;
							if (this.m_textElementType == TMP_TextElementType.Character)
							{
								goto IL_413D;
							}
						}
					}
					else
					{
						this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
						this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
						this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					}
					int currentMaterialIndex = this.m_currentMaterialIndex;
					bool isUsingAlternateTypeface = this.m_textInfo.characterInfo[this.m_characterCount].isUsingAlternateTypeface;
					this.m_isTextLayoutPhase = false;
					bool flag12 = false;
					if (characterSubstitution.index == this.m_characterCount)
					{
						num4 = characterSubstitution.unicode;
						this.m_textElementType = TMP_TextElementType.Character;
						flag12 = true;
						if (num4 != 3U)
						{
							if (num4 != 45U)
							{
								if (num4 == 8230U)
								{
									this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_Ellipsis.character;
									this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
									this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_Ellipsis.fontAsset;
									this.m_textInfo.characterInfo[this.m_characterCount].material = this.m_Ellipsis.material;
									this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
									MaterialReference[] materialReferences = TMP_Text.m_materialReferences;
									int materialIndex = this.m_Underline.materialIndex;
									materialReferences[materialIndex].referenceCount = materialReferences[materialIndex].referenceCount + 1;
									this.m_isTextTruncated = true;
									characterSubstitution.index = this.m_characterCount + 1;
									characterSubstitution.unicode = 3U;
								}
							}
						}
						else
						{
							this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_currentFontAsset.characterLookupTable[3U];
							this.m_isTextTruncated = true;
						}
					}
					if (this.m_characterCount < this.m_firstVisibleCharacter && num4 != 3U)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
						this.m_textInfo.characterInfo[this.m_characterCount].character = '\u200b';
						this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = 0;
						this.m_characterCount++;
					}
					else
					{
						float num16 = 1f;
						if (this.m_textElementType == TMP_TextElementType.Character)
						{
							if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
							{
								if (char.IsLower((char)num4))
								{
									num4 = (uint)char.ToUpper((char)num4);
								}
							}
							else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
							{
								if (char.IsUpper((char)num4))
								{
									num4 = (uint)char.ToLower((char)num4);
								}
							}
							else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num4))
							{
								num16 = 0.8f;
								num4 = (uint)char.ToUpper((char)num4);
							}
						}
						float num17 = 0f;
						float num18 = 0f;
						float num19 = 0f;
						if (this.m_textElementType == TMP_TextElementType.Sprite)
						{
							TMP_SpriteCharacter tmp_SpriteCharacter = (TMP_SpriteCharacter)base.textInfo.characterInfo[this.m_characterCount].textElement;
							this.m_currentSpriteAsset = tmp_SpriteCharacter.textAsset as TMP_SpriteAsset;
							this.m_spriteIndex = (int)tmp_SpriteCharacter.glyphIndex;
							if (tmp_SpriteCharacter == null)
							{
								goto IL_413D;
							}
							if (num4 == 60U)
							{
								num4 = (uint)(57344 + this.m_spriteIndex);
							}
							else
							{
								this.m_spriteColor = TMP_Text.s_colorWhite;
							}
							float num20 = this.m_currentFontSize / this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							if (this.m_currentSpriteAsset.m_FaceInfo.pointSize > 0f)
							{
								float num21 = this.m_currentFontSize / this.m_currentSpriteAsset.m_FaceInfo.pointSize * this.m_currentSpriteAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								num2 = tmp_SpriteCharacter.m_Scale * tmp_SpriteCharacter.m_Glyph.scale * num21;
								num18 = this.m_currentSpriteAsset.m_FaceInfo.ascentLine;
								num17 = this.m_currentSpriteAsset.m_FaceInfo.baseline * num20 * this.m_fontScaleMultiplier * this.m_currentSpriteAsset.m_FaceInfo.scale;
								num19 = this.m_currentSpriteAsset.m_FaceInfo.descentLine;
							}
							else
							{
								float num22 = this.m_currentFontSize / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								num2 = this.m_currentFontAsset.m_FaceInfo.ascentLine / tmp_SpriteCharacter.m_Glyph.metrics.height * tmp_SpriteCharacter.m_Scale * tmp_SpriteCharacter.m_Glyph.scale * num22;
								float num23 = num22 / num2;
								num18 = this.m_currentFontAsset.m_FaceInfo.ascentLine * num23;
								num17 = this.m_currentFontAsset.m_FaceInfo.baseline * num20 * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
								num19 = this.m_currentFontAsset.m_FaceInfo.descentLine * num23;
							}
							this.m_cached_TextElement = tmp_SpriteCharacter;
							this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
							this.m_textInfo.characterInfo[this.m_characterCount].scale = num2;
							this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_currentFontAsset;
							this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_currentMaterialIndex;
							this.m_currentMaterialIndex = currentMaterialIndex;
							num5 = 0f;
						}
						else if (this.m_textElementType == TMP_TextElementType.Character)
						{
							this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
							if (this.m_cached_TextElement == null)
							{
								goto IL_413D;
							}
							this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
							this.m_currentMaterial = this.m_textInfo.characterInfo[this.m_characterCount].material;
							this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
							float num24;
							if (flag12 && this.m_TextProcessingArray[num14].unicode == 10U && this.m_characterCount != this.m_firstCharacterOfLine)
							{
								num24 = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize * num16 / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							}
							else
							{
								num24 = this.m_currentFontSize * num16 / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							}
							if (flag12 && num4 == 8230U)
							{
								num18 = 0f;
								num19 = 0f;
							}
							else
							{
								num18 = this.m_currentFontAsset.m_FaceInfo.ascentLine;
								num19 = this.m_currentFontAsset.m_FaceInfo.descentLine;
							}
							num2 = num24 * this.m_fontScaleMultiplier * this.m_cached_TextElement.m_Scale * this.m_cached_TextElement.m_Glyph.scale;
							num17 = this.m_currentFontAsset.m_FaceInfo.baseline * num24 * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
							this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
							this.m_textInfo.characterInfo[this.m_characterCount].scale = num2;
							num5 = ((this.m_currentMaterialIndex == 0) ? this.m_padding : this.m_subTextObjects[this.m_currentMaterialIndex].padding);
						}
						float num25 = num2;
						if (num4 == 173U || num4 == 3U)
						{
							num2 = 0f;
						}
						this.m_textInfo.characterInfo[this.m_characterCount].character = (char)num4;
						this.m_textInfo.characterInfo[this.m_characterCount].pointSize = this.m_currentFontSize;
						this.m_textInfo.characterInfo[this.m_characterCount].color = this.m_htmlColor;
						this.m_textInfo.characterInfo[this.m_characterCount].underlineColor = this.m_underlineColor;
						this.m_textInfo.characterInfo[this.m_characterCount].strikethroughColor = this.m_strikethroughColor;
						this.m_textInfo.characterInfo[this.m_characterCount].highlightState = this.m_HighlightState;
						this.m_textInfo.characterInfo[this.m_characterCount].style = this.m_FontStyleInternal;
						Glyph alternativeGlyph = this.m_textInfo.characterInfo[this.m_characterCount].alternativeGlyph;
						GlyphMetrics glyphMetrics = ((alternativeGlyph == null) ? this.m_cached_TextElement.m_Glyph.metrics : alternativeGlyph.metrics);
						bool flag13 = num4 <= 65535U && char.IsWhiteSpace((char)num4);
						GlyphValueRecord glyphValueRecord = default(GlyphValueRecord);
						float num26 = this.m_characterSpacing;
						if (flag5 && this.m_textElementType == TMP_TextElementType.Character)
						{
							uint glyphIndex = this.m_cached_TextElement.m_GlyphIndex;
							if (this.m_characterCount < totalCharacterCount - 1 && this.m_textInfo.characterInfo[this.m_characterCount + 1].elementType == TMP_TextElementType.Character)
							{
								uint num27 = (this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.m_GlyphIndex << 16) | glyphIndex;
								GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
								if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(num27, out glyphPairAdjustmentRecord))
								{
									glyphValueRecord = glyphPairAdjustmentRecord.firstAdjustmentRecord.glyphValueRecord;
									num26 = (((glyphPairAdjustmentRecord.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num26);
								}
							}
							if (this.m_characterCount >= 1)
							{
								uint glyphIndex2 = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.m_GlyphIndex;
								uint num28 = (glyphIndex << 16) | glyphIndex2;
								GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
								if (base.textInfo.characterInfo[this.m_characterCount - 1].elementType == TMP_TextElementType.Character && this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(num28, out glyphPairAdjustmentRecord))
								{
									glyphValueRecord += glyphPairAdjustmentRecord.secondAdjustmentRecord.glyphValueRecord;
									num26 = (((glyphPairAdjustmentRecord.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num26);
								}
							}
						}
						this.m_textInfo.characterInfo[this.m_characterCount].adjustedHorizontalAdvance = glyphValueRecord.xAdvance;
						bool flag14 = TMP_TextParsingUtilities.IsBaseGlyph(num4);
						if (flag14)
						{
							this.m_LastBaseGlyphIndex = this.m_characterCount;
						}
						if (this.m_characterCount > 0 && !flag14)
						{
							if (flag6 && this.m_LastBaseGlyphIndex != -2147483648 && this.m_LastBaseGlyphIndex == this.m_characterCount - 1)
							{
								uint index = this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].textElement.glyph.index;
								uint num29 = (this.m_cached_TextElement.glyphIndex << 16) | index;
								MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord;
								if (this.m_currentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(num29, out markToBaseAdjustmentRecord))
								{
									float num30 = (this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].origin - this.m_xAdvance) / num2;
									glyphValueRecord.xPlacement = num30 + markToBaseAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate - markToBaseAdjustmentRecord.markPositionAdjustment.xPositionAdjustment;
									glyphValueRecord.yPlacement = markToBaseAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate - markToBaseAdjustmentRecord.markPositionAdjustment.yPositionAdjustment;
									num26 = 0f;
								}
							}
							else
							{
								bool flag15 = false;
								if (flag7)
								{
									int num31 = this.m_characterCount - 1;
									while (num31 >= 0 && num31 != this.m_LastBaseGlyphIndex)
									{
										uint index2 = this.m_textInfo.characterInfo[num31].textElement.glyph.index;
										uint num32 = (this.m_cached_TextElement.glyphIndex << 16) | index2;
										MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord;
										if (this.m_currentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(num32, out markToMarkAdjustmentRecord))
										{
											float num33 = (this.m_textInfo.characterInfo[num31].origin - this.m_xAdvance) / num2;
											float num34 = num17 - this.m_lineOffset + this.m_baselineOffset;
											float num35 = (this.m_textInfo.characterInfo[num31].baseLine - num34) / num2;
											glyphValueRecord.xPlacement = num33 + markToMarkAdjustmentRecord.baseMarkGlyphAnchorPoint.xCoordinate - markToMarkAdjustmentRecord.combiningMarkPositionAdjustment.xPositionAdjustment;
											glyphValueRecord.yPlacement = num35 + markToMarkAdjustmentRecord.baseMarkGlyphAnchorPoint.yCoordinate - markToMarkAdjustmentRecord.combiningMarkPositionAdjustment.yPositionAdjustment;
											num26 = 0f;
											flag15 = true;
											break;
										}
										num31--;
									}
								}
								if (flag6 && this.m_LastBaseGlyphIndex != -2147483648 && !flag15)
								{
									uint index3 = this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].textElement.glyph.index;
									uint num36 = (this.m_cached_TextElement.glyphIndex << 16) | index3;
									MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord2;
									if (this.m_currentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(num36, out markToBaseAdjustmentRecord2))
									{
										float num37 = (this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].origin - this.m_xAdvance) / num2;
										glyphValueRecord.xPlacement = num37 + markToBaseAdjustmentRecord2.baseGlyphAnchorPoint.xCoordinate - markToBaseAdjustmentRecord2.markPositionAdjustment.xPositionAdjustment;
										glyphValueRecord.yPlacement = markToBaseAdjustmentRecord2.baseGlyphAnchorPoint.yCoordinate - markToBaseAdjustmentRecord2.markPositionAdjustment.yPositionAdjustment;
										num26 = 0f;
									}
								}
							}
						}
						num18 += glyphValueRecord.yPlacement;
						num19 += glyphValueRecord.yPlacement;
						if (this.m_isRightToLeft)
						{
							this.m_xAdvance -= glyphMetrics.horizontalAdvance * (1f - this.m_charWidthAdjDelta) * num2;
							if (flag13 || num4 == 8203U)
							{
								this.m_xAdvance -= this.m_wordSpacing * num3;
							}
						}
						float num38 = 0f;
						if (this.m_monoSpacing != 0f)
						{
							if (this.m_duoSpace && (num4 == 46U || num4 == 58U || num4 == 44U))
							{
								num38 = (this.m_monoSpacing / 4f - (glyphMetrics.width / 2f + glyphMetrics.horizontalBearingX) * num2) * (1f - this.m_charWidthAdjDelta);
							}
							else
							{
								num38 = (this.m_monoSpacing / 2f - (glyphMetrics.width / 2f + glyphMetrics.horizontalBearingX) * num2) * (1f - this.m_charWidthAdjDelta);
							}
							this.m_xAdvance += num38;
						}
						float num39;
						float num40;
						if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
						{
							if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
							{
								float @float = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
								num39 = this.m_currentFontAsset.boldStyle / 4f * @float * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
								if (num39 + num5 > @float)
								{
									num5 = @float - num39;
								}
							}
							else
							{
								num39 = 0f;
							}
							num40 = this.m_currentFontAsset.boldSpacing;
						}
						else
						{
							if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
							{
								float float2 = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
								num39 = this.m_currentFontAsset.normalStyle / 4f * float2 * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
								if (num39 + num5 > float2)
								{
									num5 = float2 - num39;
								}
							}
							else
							{
								num39 = 0f;
							}
							num40 = 0f;
						}
						Vector3 vector3;
						vector3.x = this.m_xAdvance + (glyphMetrics.horizontalBearingX * this.m_FXScale.x - num5 - num39 + glyphValueRecord.xPlacement) * num2 * (1f - this.m_charWidthAdjDelta);
						vector3.y = num17 + (glyphMetrics.horizontalBearingY + num5 + glyphValueRecord.yPlacement) * num2 - this.m_lineOffset + this.m_baselineOffset;
						vector3.z = 0f;
						Vector3 vector4;
						vector4.x = vector3.x;
						vector4.y = vector3.y - (glyphMetrics.height + num5 * 2f) * num2;
						vector4.z = 0f;
						Vector3 vector5;
						vector5.x = vector4.x + (glyphMetrics.width * this.m_FXScale.x + num5 * 2f + num39 * 2f) * num2 * (1f - this.m_charWidthAdjDelta);
						vector5.y = vector3.y;
						vector5.z = 0f;
						Vector3 vector6;
						vector6.x = vector5.x;
						vector6.y = vector4.y;
						vector6.z = 0f;
						if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
						{
							float num41 = (float)this.m_ItalicAngle * 0.01f;
							float num42 = (this.m_currentFontAsset.m_FaceInfo.capLine - (this.m_currentFontAsset.m_FaceInfo.baseline + this.m_baselineOffset)) / 2f * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
							Vector3 vector7 = new Vector3(num41 * ((glyphMetrics.horizontalBearingY + num5 + num39 - num42) * num2), 0f, 0f);
							Vector3 vector8 = new Vector3(num41 * ((glyphMetrics.horizontalBearingY - glyphMetrics.height - num5 - num39 - num42) * num2), 0f, 0f);
							vector3 += vector7;
							vector4 += vector8;
							vector5 += vector7;
							vector6 += vector8;
						}
						if (this.m_FXRotation != Quaternion.identity)
						{
							Matrix4x4 matrix4x = Matrix4x4.Rotate(this.m_FXRotation);
							Vector3 vector9 = (vector5 + vector4) / 2f;
							vector3 = matrix4x.MultiplyPoint3x4(vector3 - vector9) + vector9;
							vector4 = matrix4x.MultiplyPoint3x4(vector4 - vector9) + vector9;
							vector5 = matrix4x.MultiplyPoint3x4(vector5 - vector9) + vector9;
							vector6 = matrix4x.MultiplyPoint3x4(vector6 - vector9) + vector9;
						}
						this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft = vector4;
						this.m_textInfo.characterInfo[this.m_characterCount].topLeft = vector3;
						this.m_textInfo.characterInfo[this.m_characterCount].topRight = vector5;
						this.m_textInfo.characterInfo[this.m_characterCount].bottomRight = vector6;
						this.m_textInfo.characterInfo[this.m_characterCount].origin = this.m_xAdvance + glyphValueRecord.xPlacement * num2;
						this.m_textInfo.characterInfo[this.m_characterCount].baseLine = num17 - this.m_lineOffset + this.m_baselineOffset + glyphValueRecord.yPlacement * num2;
						this.m_textInfo.characterInfo[this.m_characterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
						float num43 = ((this.m_textElementType == TMP_TextElementType.Character) ? (num18 * num2 / num16 + this.m_baselineOffset) : (num18 * num2 + this.m_baselineOffset));
						float num44 = ((this.m_textElementType == TMP_TextElementType.Character) ? (num19 * num2 / num16 + this.m_baselineOffset) : (num19 * num2 + this.m_baselineOffset));
						float num45 = num43;
						float num46 = num44;
						bool flag16 = this.m_characterCount == this.m_firstCharacterOfLine;
						if (flag16 || !flag13)
						{
							if (this.m_baselineOffset != 0f)
							{
								num45 = Mathf.Max((num43 - this.m_baselineOffset) / this.m_fontScaleMultiplier, num45);
								num46 = Mathf.Min((num44 - this.m_baselineOffset) / this.m_fontScaleMultiplier, num46);
							}
							this.m_maxLineAscender = Mathf.Max(num45, this.m_maxLineAscender);
							this.m_maxLineDescender = Mathf.Min(num46, this.m_maxLineDescender);
						}
						if (flag16 || !flag13)
						{
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender = num45;
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender = num46;
							this.m_ElementAscender = (this.m_textInfo.characterInfo[this.m_characterCount].ascender = num43 - this.m_lineOffset);
							this.m_ElementDescender = (this.m_textInfo.characterInfo[this.m_characterCount].descender = num44 - this.m_lineOffset);
						}
						else
						{
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender = this.m_maxLineAscender;
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender = this.m_maxLineDescender;
							this.m_ElementAscender = (this.m_textInfo.characterInfo[this.m_characterCount].ascender = this.m_maxLineAscender - this.m_lineOffset);
							this.m_ElementDescender = (this.m_textInfo.characterInfo[this.m_characterCount].descender = this.m_maxLineDescender - this.m_lineOffset);
						}
						if ((this.m_lineNumber == 0 || this.m_isNewPage) && (flag16 || !flag13))
						{
							this.m_maxTextAscender = this.m_maxLineAscender;
							this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * num2 / num16);
						}
						if (this.m_lineOffset == 0f && (flag16 || !flag13))
						{
							this.m_PageAscender = ((this.m_PageAscender > num43) ? this.m_PageAscender : num43);
						}
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
						bool flag17 = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
						if (num4 == 9U || ((this.m_TextWrappingMode == TextWrappingModes.PreserveWhitespace || this.m_TextWrappingMode == TextWrappingModes.PreserveWhitespaceNoWrap) && (flag13 || num4 == 8203U)) || (!flag13 && num4 != 8203U && num4 != 173U && num4 != 3U) || (num4 == 173U && !flag11) || this.m_textElementType == TMP_TextElementType.Sprite)
						{
							this.m_textInfo.characterInfo[this.m_characterCount].isVisible = true;
							float num47 = this.m_marginLeft;
							float num48 = this.m_marginRight;
							if (flag12)
							{
								num47 = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
								num48 = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
							}
							num10 = ((this.m_width != -1f) ? Mathf.Min(num8 + 0.0001f - num47 - num48, this.m_width) : (num8 + 0.0001f - num47 - num48));
							float num49 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? glyphMetrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * ((num4 == 173U) ? num25 : num2);
							float num50 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
							int characterCount = this.m_characterCount;
							if (num50 > num9 + 0.0001f)
							{
								if (this.m_firstOverflowCharacterIndex == -1)
								{
									this.m_firstOverflowCharacterIndex = this.m_characterCount;
								}
								if (this.m_enableAutoSizing)
								{
									if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_lineOffset > 0f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										float num51 = (num9 - num50) / (float)this.m_lineNumber;
										this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + num51 / num, this.m_lineSpacingMax);
										return;
									}
									if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										this.m_maxFontSize = this.m_fontSize;
										float num52 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
										this.m_fontSize -= num52;
										this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
										return;
									}
								}
								switch (this.m_overflowMode)
								{
								case TextOverflowModes.Ellipsis:
								{
									if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
									{
										num14 = -1;
										this.m_characterCount = 0;
										characterSubstitution.index = 0;
										characterSubstitution.unicode = 3U;
										this.m_firstCharacterOfLine = 0;
										goto IL_413D;
									}
									WordWrapState wordWrapState = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
									num14 = base.RestoreWordWrappingState(ref wordWrapState);
									num14--;
									this.m_characterCount--;
									characterSubstitution.index = this.m_characterCount;
									characterSubstitution.unicode = 8230U;
									num13++;
									goto IL_413D;
								}
								case TextOverflowModes.Truncate:
									num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
									characterSubstitution.index = characterCount;
									characterSubstitution.unicode = 3U;
									goto IL_413D;
								case TextOverflowModes.Page:
									if (num14 < 0 || characterCount == 0)
									{
										num14 = -1;
										this.m_characterCount = 0;
										characterSubstitution.index = 0;
										characterSubstitution.unicode = 3U;
										goto IL_413D;
									}
									if (this.m_maxLineAscender - this.m_maxLineDescender > num9 + 0.0001f)
									{
										num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLineState);
										characterSubstitution.index = characterCount;
										characterSubstitution.unicode = 3U;
										goto IL_413D;
									}
									num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLineState);
									this.m_isNewPage = true;
									this.m_firstCharacterOfLine = this.m_characterCount;
									this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
									this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
									this.m_startOfLineAscender = 0f;
									this.m_xAdvance = 0f + this.tag_Indent;
									this.m_lineOffset = 0f;
									this.m_maxTextAscender = 0f;
									this.m_PageAscender = 0f;
									this.m_lineNumber++;
									this.m_pageNumber++;
									goto IL_413D;
								case TextOverflowModes.Linked:
									num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
									if (this.m_linkedTextComponent != null)
									{
										this.m_linkedTextComponent.text = this.text;
										this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
										this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
										this.m_linkedTextComponent.ForceMeshUpdate(false, false);
										this.m_isTextTruncated = true;
									}
									characterSubstitution.index = characterCount;
									characterSubstitution.unicode = 3U;
									goto IL_413D;
								}
							}
							if (flag14 && num49 > num10 * (flag17 ? 1.05f : 1f))
							{
								if (this.m_TextWrappingMode != TextWrappingModes.NoWrap && this.m_TextWrappingMode != TextWrappingModes.PreserveWhitespaceNoWrap && this.m_characterCount != this.m_firstCharacterOfLine)
								{
									num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
									float num53;
									if (this.m_lineHeight == -32767f)
									{
										float adjustedAscender = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
										num53 = ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f) - this.m_maxLineDescender + adjustedAscender + (num6 + this.m_lineSpacingDelta) * num + this.m_lineSpacing * num3;
									}
									else
									{
										num53 = this.m_lineHeight + this.m_lineSpacing * num3;
										this.m_IsDrivenLineSpacing = true;
									}
									float num54 = this.m_maxTextAscender + num53 + this.m_lineOffset - this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender;
									if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '\u00ad' && !flag11 && (this.m_overflowMode == TextOverflowModes.Overflow || num54 < num9 + 0.0001f))
									{
										characterSubstitution.index = this.m_characterCount - 1;
										characterSubstitution.unicode = 45U;
										num14--;
										this.m_characterCount--;
										goto IL_413D;
									}
									flag11 = false;
									if (this.m_textInfo.characterInfo[this.m_characterCount].character == '\u00ad')
									{
										flag11 = true;
										goto IL_413D;
									}
									if (this.m_enableAutoSizing && flag9)
									{
										if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											float num55 = num49;
											if (this.m_charWidthAdjDelta > 0f)
											{
												num55 /= 1f - this.m_charWidthAdjDelta;
											}
											float num56 = num49 - (num10 - 0.0001f) * (flag17 ? 1.05f : 1f);
											this.m_charWidthAdjDelta += num56 / num55;
											this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
											return;
										}
										if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											this.m_maxFontSize = this.m_fontSize;
											float num57 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
											this.m_fontSize -= num57;
											this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
											return;
										}
									}
									int previous_WordBreak = TMP_Text.m_SavedSoftLineBreakState.previous_WordBreak;
									if (flag9 && previous_WordBreak != -1 && previous_WordBreak != num12)
									{
										num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState);
										num12 = previous_WordBreak;
										if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '\u00ad')
										{
											characterSubstitution.index = this.m_characterCount - 1;
											characterSubstitution.unicode = 45U;
											num14--;
											this.m_characterCount--;
											goto IL_413D;
										}
									}
									if (num54 <= num9 + 0.0001f)
									{
										base.InsertNewLine(num14, num, num2, num3, num40, num26, num10, num6, ref flag8, ref num11);
										flag4 = true;
										flag9 = true;
										goto IL_413D;
									}
									if (this.m_firstOverflowCharacterIndex == -1)
									{
										this.m_firstOverflowCharacterIndex = this.m_characterCount;
									}
									if (this.m_enableAutoSizing)
									{
										if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											float num58 = (num9 - num54) / (float)(this.m_lineNumber + 1);
											this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + num58 / num, this.m_lineSpacingMax);
											return;
										}
										if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											float num59 = num49;
											if (this.m_charWidthAdjDelta > 0f)
											{
												num59 /= 1f - this.m_charWidthAdjDelta;
											}
											float num60 = num49 - (num10 - 0.0001f) * (flag17 ? 1.05f : 1f);
											this.m_charWidthAdjDelta += num60 / num59;
											this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
											return;
										}
										if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											this.m_maxFontSize = this.m_fontSize;
											float num61 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
											this.m_fontSize -= num61;
											this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
											return;
										}
									}
									switch (this.m_overflowMode)
									{
									case TextOverflowModes.Overflow:
									case TextOverflowModes.Masking:
									case TextOverflowModes.ScrollRect:
										base.InsertNewLine(num14, num, num2, num3, num40, num26, num10, num6, ref flag8, ref num11);
										flag4 = true;
										flag9 = true;
										goto IL_413D;
									case TextOverflowModes.Ellipsis:
									{
										if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
										{
											num14 = -1;
											this.m_characterCount = 0;
											characterSubstitution.index = 0;
											characterSubstitution.unicode = 3U;
											this.m_firstCharacterOfLine = 0;
											goto IL_413D;
										}
										WordWrapState wordWrapState2 = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
										num14 = base.RestoreWordWrappingState(ref wordWrapState2);
										num14--;
										this.m_characterCount--;
										characterSubstitution.index = this.m_characterCount;
										characterSubstitution.unicode = 8230U;
										num13++;
										goto IL_413D;
									}
									case TextOverflowModes.Truncate:
										num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
										characterSubstitution.index = characterCount;
										characterSubstitution.unicode = 3U;
										goto IL_413D;
									case TextOverflowModes.Page:
										this.m_isNewPage = true;
										base.InsertNewLine(num14, num, num2, num3, num40, num26, num10, num6, ref flag8, ref num11);
										this.m_startOfLineAscender = 0f;
										this.m_lineOffset = 0f;
										this.m_maxTextAscender = 0f;
										this.m_PageAscender = 0f;
										this.m_pageNumber++;
										flag4 = true;
										flag9 = true;
										goto IL_413D;
									case TextOverflowModes.Linked:
										if (this.m_linkedTextComponent != null)
										{
											this.m_linkedTextComponent.text = this.text;
											this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
											this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
											this.m_linkedTextComponent.ForceMeshUpdate(false, false);
											this.m_isTextTruncated = true;
										}
										characterSubstitution.index = this.m_characterCount;
										characterSubstitution.unicode = 3U;
										goto IL_413D;
									}
								}
								else
								{
									if (this.m_enableAutoSizing && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
										{
											float num62 = num49;
											if (this.m_charWidthAdjDelta > 0f)
											{
												num62 /= 1f - this.m_charWidthAdjDelta;
											}
											float num63 = num49 - (num10 - 0.0001f) * (flag17 ? 1.05f : 1f);
											this.m_charWidthAdjDelta += num63 / num62;
											this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
											return;
										}
										if (this.m_fontSize > this.m_fontSizeMin)
										{
											this.m_maxFontSize = this.m_fontSize;
											float num64 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
											this.m_fontSize -= num64;
											this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
											return;
										}
									}
									switch (this.m_overflowMode)
									{
									case TextOverflowModes.Ellipsis:
									{
										if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
										{
											num14 = -1;
											this.m_characterCount = 0;
											characterSubstitution.index = 0;
											characterSubstitution.unicode = 3U;
											this.m_firstCharacterOfLine = 0;
											goto IL_413D;
										}
										WordWrapState wordWrapState3 = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
										num14 = base.RestoreWordWrappingState(ref wordWrapState3);
										num14--;
										this.m_characterCount--;
										characterSubstitution.index = this.m_characterCount;
										characterSubstitution.unicode = 8230U;
										num13++;
										goto IL_413D;
									}
									case TextOverflowModes.Truncate:
										num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
										characterSubstitution.index = characterCount;
										characterSubstitution.unicode = 3U;
										goto IL_413D;
									case TextOverflowModes.Linked:
										num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
										if (this.m_linkedTextComponent != null)
										{
											this.m_linkedTextComponent.text = this.text;
											this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
											this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
											this.m_linkedTextComponent.ForceMeshUpdate(false, false);
											this.m_isTextTruncated = true;
										}
										characterSubstitution.index = this.m_characterCount;
										characterSubstitution.unicode = 3U;
										goto IL_413D;
									}
								}
							}
							if (flag13)
							{
								this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
								this.m_lastVisibleCharacterOfLine = this.m_characterCount;
								TMP_LineInfo[] lineInfo = this.m_textInfo.lineInfo;
								int lineNumber = this.m_lineNumber;
								this.m_lineVisibleSpaceCount = (lineInfo[lineNumber].spaceCount = lineInfo[lineNumber].spaceCount + 1);
								this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft = num47;
								this.m_textInfo.lineInfo[this.m_lineNumber].marginRight = num48;
								this.m_textInfo.spaceCount++;
								if (num4 == 160U)
								{
									TMP_LineInfo[] lineInfo2 = this.m_textInfo.lineInfo;
									int lineNumber2 = this.m_lineNumber;
									lineInfo2[lineNumber2].controlCharacterCount = lineInfo2[lineNumber2].controlCharacterCount + 1;
								}
							}
							else if (num4 == 173U)
							{
								this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
							}
							else
							{
								Color32 color;
								if (this.m_overrideHtmlColors)
								{
									color = this.m_fontColor32;
								}
								else
								{
									color = this.m_htmlColor;
								}
								if (this.m_textElementType == TMP_TextElementType.Character)
								{
									this.SaveGlyphVertexInfo(num5, num39, color);
								}
								else if (this.m_textElementType == TMP_TextElementType.Sprite)
								{
									this.SaveSpriteVertexInfo(color);
								}
								if (flag4)
								{
									flag4 = false;
									this.m_firstVisibleCharacterOfLine = this.m_characterCount;
								}
								this.m_lineVisibleCharacterCount++;
								this.m_lastVisibleCharacterOfLine = this.m_characterCount;
								this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft = num47;
								this.m_textInfo.lineInfo[this.m_lineNumber].marginRight = num48;
							}
						}
						else
						{
							if (this.m_overflowMode == TextOverflowModes.Linked && (num4 == 10U || num4 == 11U))
							{
								float num65 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
								int characterCount2 = this.m_characterCount;
								if (num65 > num9 + 0.0001f)
								{
									if (this.m_firstOverflowCharacterIndex == -1)
									{
										this.m_firstOverflowCharacterIndex = this.m_characterCount;
									}
									num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
									if (this.m_linkedTextComponent != null)
									{
										this.m_linkedTextComponent.text = this.text;
										this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
										this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
										this.m_linkedTextComponent.ForceMeshUpdate(false, false);
										this.m_isTextTruncated = true;
									}
									characterSubstitution.index = characterCount2;
									characterSubstitution.unicode = 3U;
									goto IL_413D;
								}
							}
							if ((num4 == 10U || num4 == 11U || num4 == 160U || num4 == 8199U || num4 == 8232U || num4 == 8233U || char.IsSeparator((char)num4)) && num4 != 173U && num4 != 8203U && num4 != 8288U)
							{
								TMP_LineInfo[] lineInfo3 = this.m_textInfo.lineInfo;
								int lineNumber3 = this.m_lineNumber;
								lineInfo3[lineNumber3].spaceCount = lineInfo3[lineNumber3].spaceCount + 1;
								this.m_textInfo.spaceCount++;
							}
							if (num4 == 160U)
							{
								TMP_LineInfo[] lineInfo4 = this.m_textInfo.lineInfo;
								int lineNumber4 = this.m_lineNumber;
								lineInfo4[lineNumber4].controlCharacterCount = lineInfo4[lineNumber4].controlCharacterCount + 1;
							}
						}
						if (this.m_overflowMode == TextOverflowModes.Ellipsis && (!flag12 || num4 == 45U))
						{
							float num66 = this.m_currentFontSize / this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
							float num67 = this.m_marginLeft;
							float num68 = this.m_marginRight;
							if (num4 == 10U && this.m_characterCount != this.m_firstCharacterOfLine)
							{
								num66 = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize / this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
								num67 = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
								num68 = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
							}
							float num69 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
							float num70 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? this.m_Ellipsis.character.m_Glyph.metrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * num66;
							float num71 = ((this.m_width != -1f) ? Mathf.Min(num8 + 0.0001f - num67 - num68, this.m_width) : (num8 + 0.0001f - num67 - num68));
							if (num70 < num71 * (flag17 ? 1.05f : 1f) && num69 < num9 + 0.0001f)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedEllipsisState, num14, this.m_characterCount);
								TMP_Text.m_EllipsisInsertionCandidateStack.Push(TMP_Text.m_SavedEllipsisState);
							}
						}
						this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = this.m_lineNumber;
						this.m_textInfo.characterInfo[this.m_characterCount].pageNumber = this.m_pageNumber;
						if ((num4 != 10U && num4 != 11U && num4 != 13U && !flag12) || this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
						{
							this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
						}
						if (num4 == 9U)
						{
							float num72 = this.m_currentFontAsset.m_FaceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * num2;
							if (this.m_isRightToLeft)
							{
								float num73 = Mathf.Floor(this.m_xAdvance / num72) * num72;
								this.m_xAdvance = ((num73 < this.m_xAdvance) ? num73 : (this.m_xAdvance - num72));
							}
							else
							{
								float num74 = Mathf.Ceil(this.m_xAdvance / num72) * num72;
								this.m_xAdvance = ((num74 > this.m_xAdvance) ? num74 : (this.m_xAdvance + num72));
							}
						}
						else if (this.m_monoSpacing != 0f)
						{
							float num75;
							if (this.m_duoSpace && (num4 == 46U || num4 == 58U || num4 == 44U))
							{
								num75 = this.m_monoSpacing / 2f - num38;
							}
							else
							{
								num75 = this.m_monoSpacing - num38;
							}
							this.m_xAdvance += (num75 + (this.m_currentFontAsset.normalSpacingOffset + num26) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (flag13 || num4 == 8203U)
							{
								this.m_xAdvance += this.m_wordSpacing * num3;
							}
						}
						else if (this.m_isRightToLeft)
						{
							this.m_xAdvance -= (glyphValueRecord.xAdvance * num2 + (this.m_currentFontAsset.normalSpacingOffset + num26 + num40) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (flag13 || num4 == 8203U)
							{
								this.m_xAdvance -= this.m_wordSpacing * num3;
							}
						}
						else
						{
							this.m_xAdvance += ((glyphMetrics.horizontalAdvance * this.m_FXScale.x + glyphValueRecord.xAdvance) * num2 + (this.m_currentFontAsset.normalSpacingOffset + num26 + num40) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (flag13 || num4 == 8203U)
							{
								this.m_xAdvance += this.m_wordSpacing * num3;
							}
						}
						this.m_textInfo.characterInfo[this.m_characterCount].xAdvance = this.m_xAdvance;
						if (num4 == 13U)
						{
							this.m_xAdvance = 0f + this.tag_Indent;
						}
						if (this.m_overflowMode == TextOverflowModes.Page && num4 != 10U && num4 != 11U && num4 != 13U && num4 != 8232U && num4 != 8233U)
						{
							if (this.m_pageNumber + 1 > this.m_textInfo.pageInfo.Length)
							{
								TMP_TextInfo.Resize<TMP_PageInfo>(ref this.m_textInfo.pageInfo, this.m_pageNumber + 1, true);
							}
							this.m_textInfo.pageInfo[this.m_pageNumber].ascender = this.m_PageAscender;
							this.m_textInfo.pageInfo[this.m_pageNumber].descender = ((this.m_ElementDescender < this.m_textInfo.pageInfo[this.m_pageNumber].descender) ? this.m_ElementDescender : this.m_textInfo.pageInfo[this.m_pageNumber].descender);
							if (this.m_isNewPage)
							{
								this.m_isNewPage = false;
								this.m_textInfo.pageInfo[this.m_pageNumber].firstCharacterIndex = this.m_characterCount;
							}
							this.m_textInfo.pageInfo[this.m_pageNumber].lastCharacterIndex = this.m_characterCount;
						}
						if (num4 == 10U || num4 == 11U || num4 == 3U || num4 == 8232U || num4 == 8233U || (num4 == 45U && flag12) || this.m_characterCount == totalCharacterCount - 1)
						{
							float num76 = this.m_maxLineAscender - this.m_startOfLineAscender;
							if (this.m_lineOffset > 0f && Math.Abs(num76) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
							{
								base.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, num76);
								this.m_ElementDescender -= num76;
								this.m_lineOffset += num76;
								if (TMP_Text.m_SavedEllipsisState.lineNumber == this.m_lineNumber)
								{
									TMP_Text.m_SavedEllipsisState = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
									TMP_Text.m_SavedEllipsisState.startOfLineAscender = TMP_Text.m_SavedEllipsisState.startOfLineAscender + num76;
									TMP_Text.m_SavedEllipsisState.lineOffset = TMP_Text.m_SavedEllipsisState.lineOffset + num76;
									TMP_Text.m_EllipsisInsertionCandidateStack.Push(TMP_Text.m_SavedEllipsisState);
								}
							}
							this.m_isNewPage = false;
							float num77 = this.m_maxLineAscender - this.m_lineOffset;
							float num78 = this.m_maxLineDescender - this.m_lineOffset;
							this.m_ElementDescender = ((this.m_ElementDescender < num78) ? this.m_ElementDescender : num78);
							if (!flag8)
							{
								num11 = this.m_ElementDescender;
							}
							if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
							{
								flag8 = true;
							}
							this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
							this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
							this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = this.m_characterCount);
							this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
							this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
							this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
							this.m_textInfo.lineInfo[this.m_lineNumber].visibleSpaceCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex + 1 - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex - this.m_lineVisibleCharacterCount;
							this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, num78);
							this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, num77);
							this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x - num5 * num2;
							this.m_textInfo.lineInfo[this.m_lineNumber].width = num10;
							if (this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
							{
								this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
							}
							float num79 = ((this.m_currentFontAsset.normalSpacingOffset + num26 + num40) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].isVisible)
							{
								this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance + (this.m_isRightToLeft ? num79 : (-num79));
							}
							else
							{
								this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastCharacterOfLine].xAdvance + (this.m_isRightToLeft ? num79 : (-num79));
							}
							this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
							this.m_textInfo.lineInfo[this.m_lineNumber].ascender = num77;
							this.m_textInfo.lineInfo[this.m_lineNumber].descender = num78;
							this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = num77 - num78 + num6 * num;
							if (num4 == 10U || num4 == 11U || (num4 == 45U && flag12) || num4 == 8232U || num4 == 8233U)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, num14, this.m_characterCount);
								this.m_lineNumber++;
								flag4 = true;
								flag10 = false;
								flag9 = true;
								this.m_firstCharacterOfLine = this.m_characterCount + 1;
								this.m_lineVisibleCharacterCount = 0;
								this.m_lineVisibleSpaceCount = 0;
								if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
								{
									base.ResizeLineExtents(this.m_lineNumber);
								}
								float adjustedAscender2 = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
								if (this.m_lineHeight == -32767f)
								{
									float num80 = 0f - this.m_maxLineDescender + adjustedAscender2 + (num6 + this.m_lineSpacingDelta) * num + (this.m_lineSpacing + ((num4 == 10U || num4 == 8233U) ? this.m_paragraphSpacing : 0f)) * num3;
									this.m_lineOffset += num80;
									this.m_IsDrivenLineSpacing = false;
								}
								else
								{
									this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((num4 == 10U || num4 == 8233U) ? this.m_paragraphSpacing : 0f)) * num3;
									this.m_IsDrivenLineSpacing = true;
								}
								this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
								this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
								this.m_startOfLineAscender = adjustedAscender2;
								this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
								base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
								base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, num14, this.m_characterCount);
								this.m_characterCount++;
								goto IL_413D;
							}
							if (num4 == 3U)
							{
								num14 = this.m_TextProcessingArray.Length;
							}
						}
						if (this.m_textInfo.characterInfo[this.m_characterCount].isVisible)
						{
							this.m_meshExtents.min.x = Mathf.Min(this.m_meshExtents.min.x, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.x);
							this.m_meshExtents.min.y = Mathf.Min(this.m_meshExtents.min.y, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.y);
							this.m_meshExtents.max.x = Mathf.Max(this.m_meshExtents.max.x, this.m_textInfo.characterInfo[this.m_characterCount].topRight.x);
							this.m_meshExtents.max.y = Mathf.Max(this.m_meshExtents.max.y, this.m_textInfo.characterInfo[this.m_characterCount].topRight.y);
						}
						if ((this.m_TextWrappingMode != TextWrappingModes.NoWrap && this.m_TextWrappingMode != TextWrappingModes.PreserveWhitespaceNoWrap) || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis || this.m_overflowMode == TextOverflowModes.Linked)
						{
							bool flag18 = false;
							bool flag19 = false;
							if ((flag13 || num4 == 8203U || num4 == 45U || num4 == 173U) && (!this.m_isNonBreakingSpace || flag10) && num4 != 160U && num4 != 8199U && num4 != 8209U && num4 != 8239U && num4 != 8288U)
							{
								if (num4 != 45U || this.m_characterCount <= 0 || !char.IsWhiteSpace(this.m_textInfo.characterInfo[this.m_characterCount - 1].character) || this.m_textInfo.characterInfo[this.m_characterCount - 1].lineNumber != this.m_lineNumber)
								{
									flag9 = false;
									flag18 = true;
									TMP_Text.m_SavedSoftLineBreakState.previous_WordBreak = -1;
								}
							}
							else if (!this.m_isNonBreakingSpace && ((TMP_TextParsingUtilities.IsHangul(num4) && !TMP_Settings.useModernHangulLineBreakingRules) || TMP_TextParsingUtilities.IsCJK(num4)))
							{
								bool flag20 = TMP_Settings.linebreakingRules.leadingCharacters.Contains(num4);
								bool flag21 = this.m_characterCount < totalCharacterCount - 1 && TMP_Settings.linebreakingRules.followingCharacters.Contains((uint)this.m_textInfo.characterInfo[this.m_characterCount + 1].character);
								if (!flag20)
								{
									if (!flag21)
									{
										flag9 = false;
										flag18 = true;
									}
									if (flag9)
									{
										if (flag13)
										{
											flag19 = true;
										}
										flag18 = true;
									}
								}
								else if (flag9 && flag16)
								{
									if (flag13)
									{
										flag19 = true;
									}
									flag18 = true;
								}
							}
							else if (!this.m_isNonBreakingSpace && this.m_characterCount + 1 < totalCharacterCount && TMP_TextParsingUtilities.IsCJK((uint)this.m_textInfo.characterInfo[this.m_characterCount + 1].character))
							{
								flag18 = true;
							}
							else if (flag9)
							{
								if ((flag13 && num4 != 160U) || (num4 == 173U && !flag11))
								{
									flag19 = true;
								}
								flag18 = true;
							}
							if (flag18)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
							}
							if (flag19)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, num14, this.m_characterCount);
							}
						}
						base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, num14, this.m_characterCount);
						this.m_characterCount++;
					}
				}
				IL_413D:
				num14++;
			}
			float num81 = this.m_maxFontSize - this.m_minFontSize;
			if (this.m_enableAutoSizing && num81 > 0.051f && this.m_fontSize < this.m_fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
			{
				if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
				{
					this.m_charWidthAdjDelta = 0f;
				}
				this.m_minFontSize = this.m_fontSize;
				float num82 = Mathf.Max((this.m_maxFontSize - this.m_fontSize) / 2f, 0.05f);
				this.m_fontSize += num82;
				this.m_fontSize = Mathf.Min((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMax);
				return;
			}
			this.m_IsAutoSizePointSizeSet = true;
			if (this.m_AutoSizeIterationCount >= this.m_AutoSizeMaxIterationCount)
			{
				Debug.Log("Auto Size Iteration Count: " + this.m_AutoSizeIterationCount.ToString() + ". Final Point Size: " + this.m_fontSize.ToString());
			}
			if (this.m_characterCount == 0 || (this.m_characterCount == 1 && num4 == 3U))
			{
				this.ClearMesh(true);
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				return;
			}
			int num83 = TMP_Text.m_materialReferences[this.m_Underline.materialIndex].referenceCount * 4;
			this.m_textInfo.meshInfo[0].Clear(false);
			Vector3 vector10 = Vector3.zero;
			Vector3[] rectTransformCorners = this.m_RectTransformCorners;
			VerticalAlignmentOptions verticalAlignment = this.m_VerticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							if (this.m_overflowMode != TextOverflowModes.Page)
							{
								vector10 = rectTransformCorners[0] + new Vector3(0f + margin.x, 0f - num11 + margin.w, 0f);
							}
							else
							{
								vector10 = rectTransformCorners[0] + new Vector3(0f + margin.x, 0f - this.m_textInfo.pageInfo[num7].descender + margin.w, 0f);
							}
						}
					}
					else if (this.m_overflowMode != TextOverflowModes.Page)
					{
						vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_maxTextAscender + margin.y + num11 - margin.w) / 2f, 0f);
					}
					else
					{
						vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_textInfo.pageInfo[num7].ascender + margin.y + this.m_textInfo.pageInfo[num7].descender - margin.w) / 2f, 0f);
					}
				}
				else if (this.m_overflowMode != TextOverflowModes.Page)
				{
					vector10 = rectTransformCorners[1] + new Vector3(0f + margin.x, 0f - this.m_maxTextAscender - margin.y, 0f);
				}
				else
				{
					vector10 = rectTransformCorners[1] + new Vector3(0f + margin.x, 0f - this.m_textInfo.pageInfo[num7].ascender - margin.y, 0f);
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_maxCapHeight - margin.y - margin.w) / 2f, 0f);
					}
				}
				else
				{
					vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_meshExtents.max.y + margin.y + this.m_meshExtents.min.y - margin.w) / 2f, 0f);
				}
			}
			else
			{
				vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f, 0f);
			}
			Vector3 vector11 = Vector3.zero;
			Vector3 vector12 = Vector3.zero;
			int num84 = 0;
			int num85 = 0;
			int num86 = 0;
			bool flag22 = false;
			bool flag23 = false;
			int num87 = 0;
			float num88 = (this.m_previousLossyScaleY = this.transform.lossyScale.y);
			Color32 color2 = Color.white;
			Color32 color3 = Color.white;
			HighlightState highlightState = new HighlightState(new Color32(byte.MaxValue, byte.MaxValue, 0, 64), TMP_Offset.zero);
			float num89 = 0f;
			float num90 = 0f;
			float num91 = 0f;
			float num92 = 0f;
			float num93 = TMP_Text.k_LargePositiveFloat;
			int num94 = 0;
			float num95 = 0f;
			float num96 = 0f;
			float num97 = 0f;
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			int i = 0;
			while (i < this.m_characterCount)
			{
				TMP_FontAsset fontAsset = characterInfo[i].fontAsset;
				char character = characterInfo[i].character;
				bool flag24 = char.IsWhiteSpace(character);
				int lineNumber5 = characterInfo[i].lineNumber;
				TMP_LineInfo tmp_LineInfo = this.m_textInfo.lineInfo[lineNumber5];
				num85 = lineNumber5 + 1;
				HorizontalAlignmentOptions alignment = tmp_LineInfo.alignment;
				if (alignment <= HorizontalAlignmentOptions.Justified)
				{
					switch (alignment)
					{
					case HorizontalAlignmentOptions.Left:
						if (!this.m_isRightToLeft)
						{
							vector11 = new Vector3(0f + tmp_LineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							vector11 = new Vector3(0f - tmp_LineInfo.maxAdvance, 0f, 0f);
						}
						break;
					case HorizontalAlignmentOptions.Center:
						vector11 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width / 2f - tmp_LineInfo.maxAdvance / 2f, 0f, 0f);
						break;
					case (HorizontalAlignmentOptions)3:
						break;
					case HorizontalAlignmentOptions.Right:
						if (!this.m_isRightToLeft)
						{
							vector11 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width - tmp_LineInfo.maxAdvance, 0f, 0f);
						}
						else
						{
							vector11 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
						}
						break;
					default:
						if (alignment == HorizontalAlignmentOptions.Justified)
						{
							goto IL_4926;
						}
						break;
					}
				}
				else
				{
					if (alignment == HorizontalAlignmentOptions.Flush)
					{
						goto IL_4926;
					}
					if (alignment == HorizontalAlignmentOptions.Geometry)
					{
						vector11 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width / 2f - (tmp_LineInfo.lineExtents.min.x + tmp_LineInfo.lineExtents.max.x) / 2f, 0f, 0f);
					}
				}
				IL_4BA4:
				vector12 = vector10 + vector11;
				bool isVisible = characterInfo[i].isVisible;
				if (isVisible)
				{
					TMP_TextElementType elementType = characterInfo[i].elementType;
					if (elementType != TMP_TextElementType.Character)
					{
						if (elementType != TMP_TextElementType.Sprite)
						{
						}
					}
					else
					{
						Extents lineExtents = tmp_LineInfo.lineExtents;
						float num98 = this.m_uvLineOffset * (float)lineNumber5 % 1f;
						switch (this.m_horizontalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[i].vertex_BL.uv2.x = 0f;
							characterInfo[i].vertex_TL.uv2.x = 0f;
							characterInfo[i].vertex_TR.uv2.x = 1f;
							characterInfo[i].vertex_BR.uv2.x = 1f;
							break;
						case TextureMappingOptions.Line:
							if (this.m_textAlignment != TextAlignmentOptions.Justified)
							{
								characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num98;
								characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num98;
								characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num98;
								characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num98;
							}
							else
							{
								characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
								characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
								characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
								characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
							characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
							characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
							characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x + vector11.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num98;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							switch (this.m_verticalMapping)
							{
							case TextureMappingOptions.Character:
								characterInfo[i].vertex_BL.uv2.y = 0f;
								characterInfo[i].vertex_TL.uv2.y = 1f;
								characterInfo[i].vertex_TR.uv2.y = 0f;
								characterInfo[i].vertex_BR.uv2.y = 1f;
								break;
							case TextureMappingOptions.Line:
								characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num98;
								characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num98;
								characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
								characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + num98;
								characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + num98;
								characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
								characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float num99 = (1f - (characterInfo[i].vertex_BL.uv2.y + characterInfo[i].vertex_TL.uv2.y) * characterInfo[i].aspectRatio) / 2f;
							characterInfo[i].vertex_BL.uv2.x = characterInfo[i].vertex_BL.uv2.y * characterInfo[i].aspectRatio + num99 + num98;
							characterInfo[i].vertex_TL.uv2.x = characterInfo[i].vertex_BL.uv2.x;
							characterInfo[i].vertex_TR.uv2.x = characterInfo[i].vertex_TL.uv2.y * characterInfo[i].aspectRatio + num99 + num98;
							characterInfo[i].vertex_BR.uv2.x = characterInfo[i].vertex_TR.uv2.x;
							break;
						}
						}
						switch (this.m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[i].vertex_BL.uv2.y = 0f;
							characterInfo[i].vertex_TL.uv2.y = 1f;
							characterInfo[i].vertex_TR.uv2.y = 1f;
							characterInfo[i].vertex_BR.uv2.y = 0f;
							break;
						case TextureMappingOptions.Line:
							characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - tmp_LineInfo.descender) / (tmp_LineInfo.ascender - tmp_LineInfo.descender);
							characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - tmp_LineInfo.descender) / (tmp_LineInfo.ascender - tmp_LineInfo.descender);
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float num100 = (1f - (characterInfo[i].vertex_BL.uv2.x + characterInfo[i].vertex_TR.uv2.x) / characterInfo[i].aspectRatio) / 2f;
							characterInfo[i].vertex_BL.uv2.y = num100 + characterInfo[i].vertex_BL.uv2.x / characterInfo[i].aspectRatio;
							characterInfo[i].vertex_TL.uv2.y = num100 + characterInfo[i].vertex_TR.uv2.x / characterInfo[i].aspectRatio;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							break;
						}
						}
						num89 = characterInfo[i].scale * Mathf.Abs(num88) * (1f - this.m_charWidthAdjDelta);
						if (!characterInfo[i].isUsingAlternateTypeface && (characterInfo[i].style & FontStyles.Bold) == FontStyles.Bold)
						{
							num89 *= -1f;
						}
						characterInfo[i].vertex_BL.uv.w = num89;
						characterInfo[i].vertex_TL.uv.w = num89;
						characterInfo[i].vertex_TR.uv.w = num89;
						characterInfo[i].vertex_BR.uv.w = num89;
					}
					if (i < this.m_maxVisibleCharacters && num84 < this.m_maxVisibleWords && lineNumber5 < this.m_maxVisibleLines && this.m_overflowMode != TextOverflowModes.Page)
					{
						TMP_CharacterInfo[] array = characterInfo;
						int num101 = i;
						array[num101].vertex_BL.position = array[num101].vertex_BL.position + vector12;
						TMP_CharacterInfo[] array2 = characterInfo;
						int num102 = i;
						array2[num102].vertex_TL.position = array2[num102].vertex_TL.position + vector12;
						TMP_CharacterInfo[] array3 = characterInfo;
						int num103 = i;
						array3[num103].vertex_TR.position = array3[num103].vertex_TR.position + vector12;
						TMP_CharacterInfo[] array4 = characterInfo;
						int num104 = i;
						array4[num104].vertex_BR.position = array4[num104].vertex_BR.position + vector12;
					}
					else if (i < this.m_maxVisibleCharacters && num84 < this.m_maxVisibleWords && lineNumber5 < this.m_maxVisibleLines && this.m_overflowMode == TextOverflowModes.Page && characterInfo[i].pageNumber == num7)
					{
						TMP_CharacterInfo[] array5 = characterInfo;
						int num105 = i;
						array5[num105].vertex_BL.position = array5[num105].vertex_BL.position + vector12;
						TMP_CharacterInfo[] array6 = characterInfo;
						int num106 = i;
						array6[num106].vertex_TL.position = array6[num106].vertex_TL.position + vector12;
						TMP_CharacterInfo[] array7 = characterInfo;
						int num107 = i;
						array7[num107].vertex_TR.position = array7[num107].vertex_TR.position + vector12;
						TMP_CharacterInfo[] array8 = characterInfo;
						int num108 = i;
						array8[num108].vertex_BR.position = array8[num108].vertex_BR.position + vector12;
					}
					else
					{
						characterInfo[i].vertex_BL.position = Vector3.zero;
						characterInfo[i].vertex_TL.position = Vector3.zero;
						characterInfo[i].vertex_TR.position = Vector3.zero;
						characterInfo[i].vertex_BR.position = Vector3.zero;
						characterInfo[i].isVisible = false;
					}
					if (QualitySettings.activeColorSpace == ColorSpace.Linear)
					{
						this.m_ConvertToLinearSpace = true;
					}
					else
					{
						this.m_ConvertToLinearSpace = false;
					}
					if (elementType == TMP_TextElementType.Character)
					{
						this.FillCharacterVertexBuffers(i);
					}
					else if (elementType == TMP_TextElementType.Sprite)
					{
						this.FillSpriteVertexBuffers(i);
					}
				}
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num109 = i;
				characterInfo2[num109].bottomLeft = characterInfo2[num109].bottomLeft + vector12;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num110 = i;
				characterInfo3[num110].topLeft = characterInfo3[num110].topLeft + vector12;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num111 = i;
				characterInfo4[num111].topRight = characterInfo4[num111].topRight + vector12;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num112 = i;
				characterInfo5[num112].bottomRight = characterInfo5[num112].bottomRight + vector12;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num113 = i;
				characterInfo6[num113].origin = characterInfo6[num113].origin + vector12.x;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num114 = i;
				characterInfo7[num114].xAdvance = characterInfo7[num114].xAdvance + vector12.x;
				TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
				int num115 = i;
				characterInfo8[num115].ascender = characterInfo8[num115].ascender + vector12.y;
				TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
				int num116 = i;
				characterInfo9[num116].descender = characterInfo9[num116].descender + vector12.y;
				TMP_CharacterInfo[] characterInfo10 = this.m_textInfo.characterInfo;
				int num117 = i;
				characterInfo10[num117].baseLine = characterInfo10[num117].baseLine + vector12.y;
				if (lineNumber5 != num86 || i == this.m_characterCount - 1)
				{
					if (lineNumber5 != num86)
					{
						TMP_LineInfo[] lineInfo5 = this.m_textInfo.lineInfo;
						int num118 = num86;
						lineInfo5[num118].baseline = lineInfo5[num118].baseline + vector12.y;
						TMP_LineInfo[] lineInfo6 = this.m_textInfo.lineInfo;
						int num119 = num86;
						lineInfo6[num119].ascender = lineInfo6[num119].ascender + vector12.y;
						TMP_LineInfo[] lineInfo7 = this.m_textInfo.lineInfo;
						int num120 = num86;
						lineInfo7[num120].descender = lineInfo7[num120].descender + vector12.y;
						TMP_LineInfo[] lineInfo8 = this.m_textInfo.lineInfo;
						int num121 = num86;
						lineInfo8[num121].maxAdvance = lineInfo8[num121].maxAdvance + vector12.x;
						this.m_textInfo.lineInfo[num86].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[num86].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[num86].descender);
						this.m_textInfo.lineInfo[num86].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[num86].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[num86].ascender);
					}
					if (i == this.m_characterCount - 1)
					{
						TMP_LineInfo[] lineInfo9 = this.m_textInfo.lineInfo;
						int num122 = lineNumber5;
						lineInfo9[num122].baseline = lineInfo9[num122].baseline + vector12.y;
						TMP_LineInfo[] lineInfo10 = this.m_textInfo.lineInfo;
						int num123 = lineNumber5;
						lineInfo10[num123].ascender = lineInfo10[num123].ascender + vector12.y;
						TMP_LineInfo[] lineInfo11 = this.m_textInfo.lineInfo;
						int num124 = lineNumber5;
						lineInfo11[num124].descender = lineInfo11[num124].descender + vector12.y;
						TMP_LineInfo[] lineInfo12 = this.m_textInfo.lineInfo;
						int num125 = lineNumber5;
						lineInfo12[num125].maxAdvance = lineInfo12[num125].maxAdvance + vector12.x;
						this.m_textInfo.lineInfo[lineNumber5].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lineNumber5].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[lineNumber5].descender);
						this.m_textInfo.lineInfo[lineNumber5].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lineNumber5].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[lineNumber5].ascender);
					}
				}
				if (char.IsLetterOrDigit(character) || character == '-' || character == '\u00ad' || character == '‐' || character == '‑')
				{
					if (!flag23)
					{
						flag23 = true;
						num87 = i;
					}
					if (flag23 && i == this.m_characterCount - 1)
					{
						int num126 = this.m_textInfo.wordInfo.Length;
						int wordCount = this.m_textInfo.wordCount;
						if (this.m_textInfo.wordCount + 1 > num126)
						{
							TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, num126 + 1);
						}
						int num127 = i;
						this.m_textInfo.wordInfo[wordCount].firstCharacterIndex = num87;
						this.m_textInfo.wordInfo[wordCount].lastCharacterIndex = num127;
						this.m_textInfo.wordInfo[wordCount].characterCount = num127 - num87 + 1;
						this.m_textInfo.wordInfo[wordCount].textComponent = this;
						num84++;
						this.m_textInfo.wordCount++;
						TMP_LineInfo[] lineInfo13 = this.m_textInfo.lineInfo;
						int num128 = lineNumber5;
						lineInfo13[num128].wordCount = lineInfo13[num128].wordCount + 1;
					}
				}
				else if ((flag23 || (i == 0 && (!char.IsPunctuation(character) || flag24 || character == '\u200b' || i == this.m_characterCount - 1))) && (i <= 0 || i >= characterInfo.Length - 1 || i >= this.m_characterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(characterInfo[i - 1].character) || !char.IsLetterOrDigit(characterInfo[i + 1].character)))
				{
					int num127 = ((i == this.m_characterCount - 1 && char.IsLetterOrDigit(character)) ? i : (i - 1));
					flag23 = false;
					int num129 = this.m_textInfo.wordInfo.Length;
					int wordCount2 = this.m_textInfo.wordCount;
					if (this.m_textInfo.wordCount + 1 > num129)
					{
						TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, num129 + 1);
					}
					this.m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num87;
					this.m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num127;
					this.m_textInfo.wordInfo[wordCount2].characterCount = num127 - num87 + 1;
					this.m_textInfo.wordInfo[wordCount2].textComponent = this;
					num84++;
					this.m_textInfo.wordCount++;
					TMP_LineInfo[] lineInfo14 = this.m_textInfo.lineInfo;
					int num130 = lineNumber5;
					lineInfo14[num130].wordCount = lineInfo14[num130].wordCount + 1;
				}
				if ((this.m_textInfo.characterInfo[i].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool flag25 = true;
					int pageNumber = this.m_textInfo.characterInfo[i].pageNumber;
					this.m_textInfo.characterInfo[i].underlineVertexIndex = num83;
					if (i > this.m_maxVisibleCharacters || lineNumber5 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != this.m_pageToDisplay))
					{
						flag25 = false;
					}
					if (!flag24 && character != '\u200b')
					{
						num92 = Mathf.Max(num92, this.m_textInfo.characterInfo[i].scale);
						num90 = Mathf.Max(num90, Mathf.Abs(num89));
						num93 = Mathf.Min((pageNumber == num94) ? num93 : TMP_Text.k_LargePositiveFloat, this.m_textInfo.characterInfo[i].baseLine + base.font.m_FaceInfo.underlineOffset * num92);
						num94 = pageNumber;
					}
					if (!flag && flag25 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag = true;
						num91 = this.m_textInfo.characterInfo[i].scale;
						if (num92 == 0f)
						{
							num92 = num91;
							num90 = num89;
						}
						zero = new Vector3(this.m_textInfo.characterInfo[i].bottomLeft.x, num93, 0f);
						color2 = this.m_textInfo.characterInfo[i].underlineColor;
					}
					if (flag && this.m_characterCount == 1)
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num93, 0f);
						float num131 = this.m_textInfo.characterInfo[i].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num83, num91, num131, num92, num90, color2);
						num92 = 0f;
						num90 = 0f;
						num93 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && (i == tmp_LineInfo.lastCharacterIndex || i >= tmp_LineInfo.lastVisibleCharacterIndex))
					{
						float num131;
						if (flag24 || character == '\u200b')
						{
							int lastVisibleCharacterIndex = tmp_LineInfo.lastVisibleCharacterIndex;
							zero2 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num93, 0f);
							num131 = this.m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num93, 0f);
							num131 = this.m_textInfo.characterInfo[i].scale;
						}
						flag = false;
						this.DrawUnderlineMesh(zero, zero2, ref num83, num91, num131, num92, num90, color2);
						num92 = 0f;
						num90 = 0f;
						num93 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && !flag25)
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, num93, 0f);
						float num131 = this.m_textInfo.characterInfo[i - 1].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num83, num91, num131, num92, num90, color2);
						num92 = 0f;
						num90 = 0f;
						num93 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && i < this.m_characterCount - 1 && !color2.Compare(this.m_textInfo.characterInfo[i + 1].underlineColor))
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num93, 0f);
						float num131 = this.m_textInfo.characterInfo[i].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num83, num91, num131, num92, num90, color2);
						num92 = 0f;
						num90 = 0f;
						num93 = TMP_Text.k_LargePositiveFloat;
					}
				}
				else if (flag)
				{
					flag = false;
					zero2 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, num93, 0f);
					float num131 = this.m_textInfo.characterInfo[i - 1].scale;
					this.DrawUnderlineMesh(zero, zero2, ref num83, num91, num131, num92, num90, color2);
					num92 = 0f;
					num90 = 0f;
					num93 = TMP_Text.k_LargePositiveFloat;
				}
				bool flag26 = (this.m_textInfo.characterInfo[i].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
				float strikethroughOffset = fontAsset.m_FaceInfo.strikethroughOffset;
				if (flag26)
				{
					bool flag27 = true;
					this.m_textInfo.characterInfo[i].strikethroughVertexIndex = num83;
					if (i > this.m_maxVisibleCharacters || lineNumber5 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && this.m_textInfo.characterInfo[i].pageNumber + 1 != this.m_pageToDisplay))
					{
						flag27 = false;
					}
					if (!flag2 && flag27 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag2 = true;
						num95 = this.m_textInfo.characterInfo[i].pointSize;
						num96 = this.m_textInfo.characterInfo[i].scale;
						zero3 = new Vector3(this.m_textInfo.characterInfo[i].bottomLeft.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num96, 0f);
						color3 = this.m_textInfo.characterInfo[i].strikethroughColor;
						num97 = this.m_textInfo.characterInfo[i].baseLine;
					}
					if (flag2 && this.m_characterCount == 1)
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num96, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num83, num96, num96, num96, num89, color3);
					}
					else if (flag2 && i == tmp_LineInfo.lastCharacterIndex)
					{
						if (flag24 || character == '\u200b')
						{
							int lastVisibleCharacterIndex2 = tmp_LineInfo.lastVisibleCharacterIndex;
							zero4 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num96, 0f);
						}
						else
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num96, 0f);
						}
						flag2 = false;
						this.DrawUnderlineMesh(zero3, zero4, ref num83, num96, num96, num96, num89, color3);
					}
					else if (flag2 && i < this.m_characterCount && (this.m_textInfo.characterInfo[i + 1].pointSize != num95 || !TMP_Math.Approximately(this.m_textInfo.characterInfo[i + 1].baseLine + vector12.y, num97)))
					{
						flag2 = false;
						int lastVisibleCharacterIndex3 = tmp_LineInfo.lastVisibleCharacterIndex;
						if (i > lastVisibleCharacterIndex3)
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num96, 0f);
						}
						else
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num96, 0f);
						}
						this.DrawUnderlineMesh(zero3, zero4, ref num83, num96, num96, num96, num89, color3);
					}
					else if (flag2 && i < this.m_characterCount && fontAsset.GetInstanceID() != characterInfo[i + 1].fontAsset.GetInstanceID())
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num96, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num83, num96, num96, num96, num89, color3);
					}
					else if (flag2 && !flag27)
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, this.m_textInfo.characterInfo[i - 1].baseLine + strikethroughOffset * num96, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num83, num96, num96, num96, num89, color3);
					}
				}
				else if (flag2)
				{
					flag2 = false;
					zero4 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, this.m_textInfo.characterInfo[i - 1].baseLine + strikethroughOffset * num96, 0f);
					this.DrawUnderlineMesh(zero3, zero4, ref num83, num96, num96, num96, num89, color3);
				}
				if ((this.m_textInfo.characterInfo[i].style & FontStyles.Highlight) == FontStyles.Highlight)
				{
					bool flag28 = true;
					int pageNumber2 = this.m_textInfo.characterInfo[i].pageNumber;
					if (i > this.m_maxVisibleCharacters || lineNumber5 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && pageNumber2 + 1 != this.m_pageToDisplay))
					{
						flag28 = false;
					}
					if (!flag3 && flag28 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag3 = true;
						vector = TMP_Text.k_LargePositiveVector2;
						vector2 = TMP_Text.k_LargeNegativeVector2;
						highlightState = this.m_textInfo.characterInfo[i].highlightState;
					}
					if (flag3)
					{
						TMP_CharacterInfo tmp_CharacterInfo = this.m_textInfo.characterInfo[i];
						HighlightState highlightState2 = tmp_CharacterInfo.highlightState;
						bool flag29 = false;
						if (highlightState != highlightState2)
						{
							if (flag24)
							{
								vector2.x = (vector2.x - highlightState.padding.right + tmp_CharacterInfo.origin) / 2f;
							}
							else
							{
								vector2.x = (vector2.x - highlightState.padding.right + tmp_CharacterInfo.bottomLeft.x) / 2f;
							}
							vector.y = Mathf.Min(vector.y, tmp_CharacterInfo.descender);
							vector2.y = Mathf.Max(vector2.y, tmp_CharacterInfo.ascender);
							this.DrawTextHighlight(vector, vector2, ref num83, highlightState.color);
							flag3 = true;
							vector = new Vector2(vector2.x, tmp_CharacterInfo.descender - highlightState2.padding.bottom);
							if (flag24)
							{
								vector2 = new Vector2(tmp_CharacterInfo.xAdvance + highlightState2.padding.right, tmp_CharacterInfo.ascender + highlightState2.padding.top);
							}
							else
							{
								vector2 = new Vector2(tmp_CharacterInfo.topRight.x + highlightState2.padding.right, tmp_CharacterInfo.ascender + highlightState2.padding.top);
							}
							highlightState = highlightState2;
							flag29 = true;
						}
						if (!flag29)
						{
							if (flag24)
							{
								vector.x = Mathf.Min(vector.x, tmp_CharacterInfo.origin - highlightState.padding.left);
								vector2.x = Mathf.Max(vector2.x, tmp_CharacterInfo.xAdvance + highlightState.padding.right);
							}
							else
							{
								vector.x = Mathf.Min(vector.x, tmp_CharacterInfo.bottomLeft.x - highlightState.padding.left);
								vector2.x = Mathf.Max(vector2.x, tmp_CharacterInfo.topRight.x + highlightState.padding.right);
							}
							vector.y = Mathf.Min(vector.y, tmp_CharacterInfo.descender - highlightState.padding.bottom);
							vector2.y = Mathf.Max(vector2.y, tmp_CharacterInfo.ascender + highlightState.padding.top);
						}
					}
					if (flag3 && this.m_characterCount == 1)
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num83, highlightState.color);
					}
					else if (flag3 && (i == tmp_LineInfo.lastCharacterIndex || i >= tmp_LineInfo.lastVisibleCharacterIndex))
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num83, highlightState.color);
					}
					else if (flag3 && !flag28)
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num83, highlightState.color);
					}
				}
				else if (flag3)
				{
					flag3 = false;
					this.DrawTextHighlight(vector, vector2, ref num83, highlightState.color);
				}
				num86 = lineNumber5;
				i++;
				continue;
				IL_4926:
				if (i > tmp_LineInfo.lastVisibleCharacterIndex || character == '\n' || character == '\u00ad' || character == '\u200b' || character == '\u2060' || character == '\u0003')
				{
					goto IL_4BA4;
				}
				char character2 = characterInfo[tmp_LineInfo.lastCharacterIndex].character;
				bool flag30 = (alignment & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush;
				if ((!char.IsControl(character2) && lineNumber5 < this.m_lineNumber) || flag30 || tmp_LineInfo.maxAdvance > tmp_LineInfo.width)
				{
					if (lineNumber5 != num86 || i == 0 || i == this.m_firstVisibleCharacter)
					{
						if (!this.m_isRightToLeft)
						{
							vector11 = new Vector3(tmp_LineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							vector11 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
						}
						flag22 = char.IsSeparator(character);
						goto IL_4BA4;
					}
					float num132 = ((!this.m_isRightToLeft) ? (tmp_LineInfo.width - tmp_LineInfo.maxAdvance) : (tmp_LineInfo.width + tmp_LineInfo.maxAdvance));
					int num133 = tmp_LineInfo.visibleCharacterCount - 1 + tmp_LineInfo.controlCharacterCount;
					int num134 = tmp_LineInfo.spaceCount - tmp_LineInfo.controlCharacterCount;
					if (flag22)
					{
						num134--;
						num133++;
					}
					float num135 = ((num134 > 0) ? this.m_wordWrappingRatios : 1f);
					if (num134 < 1)
					{
						num134 = 1;
					}
					if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
					{
						if (!this.m_isRightToLeft)
						{
							vector11 += new Vector3(num132 * (1f - num135) / (float)num134, 0f, 0f);
							goto IL_4BA4;
						}
						vector11 -= new Vector3(num132 * (1f - num135) / (float)num134, 0f, 0f);
						goto IL_4BA4;
					}
					else
					{
						if (!this.m_isRightToLeft)
						{
							vector11 += new Vector3(num132 * num135 / (float)num133, 0f, 0f);
							goto IL_4BA4;
						}
						vector11 -= new Vector3(num132 * num135 / (float)num133, 0f, 0f);
						goto IL_4BA4;
					}
				}
				else
				{
					if (!this.m_isRightToLeft)
					{
						vector11 = new Vector3(tmp_LineInfo.marginLeft, 0f, 0f);
						goto IL_4BA4;
					}
					vector11 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
					goto IL_4BA4;
				}
			}
			this.m_textInfo.meshInfo[this.m_Underline.materialIndex].vertexCount = num83;
			this.m_textInfo.characterCount = this.m_characterCount;
			this.m_textInfo.spriteCount = this.m_spriteCount;
			this.m_textInfo.lineCount = num85;
			this.m_textInfo.wordCount = ((num84 != 0 && this.m_characterCount > 0) ? num84 : 1);
			this.m_textInfo.pageCount = this.m_pageNumber + 1;
			if (this.m_renderMode == TextRenderFlags.Render && this.IsActive())
			{
				Action<TMP_TextInfo> onPreRenderText = this.OnPreRenderText;
				if (onPreRenderText != null)
				{
					onPreRenderText(this.m_textInfo);
				}
				if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
				{
					this.m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
				}
				this.m_mesh.MarkDynamic();
				this.m_mesh.vertices = this.m_textInfo.meshInfo[0].vertices;
				this.m_mesh.SetUVs(0, this.m_textInfo.meshInfo[0].uvs0);
				this.m_mesh.uv2 = this.m_textInfo.meshInfo[0].uvs2;
				this.m_mesh.colors32 = this.m_textInfo.meshInfo[0].colors32;
				this.m_mesh.RecalculateBounds();
				for (int j = 1; j < this.m_textInfo.materialCount; j++)
				{
					this.m_textInfo.meshInfo[j].ClearUnusedVertices();
					if (!(this.m_subTextObjects[j] == null))
					{
						if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
						{
							this.m_textInfo.meshInfo[j].SortGeometry(VertexSortingOrder.Reverse);
						}
						this.m_subTextObjects[j].mesh.vertices = this.m_textInfo.meshInfo[j].vertices;
						this.m_subTextObjects[j].mesh.SetUVs(0, this.m_textInfo.meshInfo[j].uvs0);
						this.m_subTextObjects[j].mesh.uv2 = this.m_textInfo.meshInfo[j].uvs2;
						this.m_subTextObjects[j].mesh.colors32 = this.m_textInfo.meshInfo[j].colors32;
						this.m_subTextObjects[j].mesh.RecalculateBounds();
					}
				}
			}
			TMPro_EventManager.ON_TEXT_CHANGED(this);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000388CA File Offset: 0x00036ACA
		protected override Vector3[] GetTextContainerLocalCorners()
		{
			if (this.m_rectTransform == null)
			{
				this.m_rectTransform = base.rectTransform;
			}
			this.m_rectTransform.GetLocalCorners(this.m_RectTransformCorners);
			return this.m_RectTransformCorners;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00038900 File Offset: 0x00036B00
		private void SetMeshFilters(bool state)
		{
			if (this.m_meshFilter != null)
			{
				if (state)
				{
					this.m_meshFilter.sharedMesh = this.m_mesh;
				}
				else
				{
					this.m_meshFilter.sharedMesh = null;
				}
			}
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				if (this.m_subTextObjects[num].meshFilter != null)
				{
					if (state)
					{
						this.m_subTextObjects[num].meshFilter.sharedMesh = this.m_subTextObjects[num].mesh;
					}
					else
					{
						this.m_subTextObjects[num].meshFilter.sharedMesh = null;
					}
				}
				num++;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000389AC File Offset: 0x00036BAC
		protected override void SetActiveSubMeshes(bool state)
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				if (this.m_subTextObjects[num].enabled != state)
				{
					this.m_subTextObjects[num].enabled = state;
				}
				num++;
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000389FC File Offset: 0x00036BFC
		protected void SetActiveSubTextObjectRenderers(bool state)
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				Renderer renderer = this.m_subTextObjects[num].renderer;
				if (renderer != null && renderer.enabled != state)
				{
					renderer.enabled = state;
				}
				num++;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00038A54 File Offset: 0x00036C54
		protected override void DestroySubMeshObjects()
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_subTextObjects[num]);
				num++;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00038A94 File Offset: 0x00036C94
		internal void UpdateSubMeshSortingLayerID(int id)
		{
			for (int i = 1; i < this.m_subTextObjects.Length; i++)
			{
				TMP_SubMesh tmp_SubMesh = this.m_subTextObjects[i];
				if (tmp_SubMesh != null && tmp_SubMesh.renderer != null)
				{
					tmp_SubMesh.renderer.sortingLayerID = id;
				}
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00038AE0 File Offset: 0x00036CE0
		internal void UpdateSubMeshSortingOrder(int order)
		{
			for (int i = 1; i < this.m_subTextObjects.Length; i++)
			{
				TMP_SubMesh tmp_SubMesh = this.m_subTextObjects[i];
				if (tmp_SubMesh != null && tmp_SubMesh.renderer != null)
				{
					tmp_SubMesh.renderer.sortingOrder = order;
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00038B2C File Offset: 0x00036D2C
		protected override Bounds GetCompoundBounds()
		{
			Bounds bounds = this.m_mesh.bounds;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				Bounds bounds2 = this.m_subTextObjects[num].mesh.bounds;
				min.x = ((min.x < bounds2.min.x) ? min.x : bounds2.min.x);
				min.y = ((min.y < bounds2.min.y) ? min.y : bounds2.min.y);
				max.x = ((max.x > bounds2.max.x) ? max.x : bounds2.max.x);
				max.y = ((max.y > bounds2.max.y) ? max.y : bounds2.max.y);
				num++;
			}
			Vector3 vector = (min + max) / 2f;
			Vector2 vector2 = max - min;
			return new Bounds(vector, vector2);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00038C80 File Offset: 0x00036E80
		private void UpdateSDFScale(float scaleDelta)
		{
			if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity || scaleDelta == float.NegativeInfinity)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
				return;
			}
			for (int i = 0; i < this.m_textInfo.materialCount; i++)
			{
				TMP_MeshInfo tmp_MeshInfo = this.m_textInfo.meshInfo[i];
				for (int j = 0; j < tmp_MeshInfo.uvs0.Length; j++)
				{
					Vector4[] uvs = tmp_MeshInfo.uvs0;
					int num = j;
					uvs[num].w = uvs[num].w * Mathf.Abs(scaleDelta);
				}
			}
			for (int k = 0; k < this.m_textInfo.meshInfo.Length; k++)
			{
				if (k == 0)
				{
					this.m_mesh.SetUVs(0, this.m_textInfo.meshInfo[0].uvs0);
				}
				else
				{
					this.m_subTextObjects[k].mesh.SetUVs(0, this.m_textInfo.meshInfo[k].uvs0);
				}
			}
		}

		// Token: 0x04000598 RID: 1432
		[SerializeField]
		internal int _SortingLayer;

		// Token: 0x04000599 RID: 1433
		[SerializeField]
		internal int _SortingLayerID;

		// Token: 0x0400059A RID: 1434
		[SerializeField]
		internal int _SortingOrder;

		// Token: 0x0400059C RID: 1436
		[SerializeField]
		private bool m_hasFontAssetChanged;

		// Token: 0x0400059D RID: 1437
		private float m_previousLossyScaleY = -1f;

		// Token: 0x0400059E RID: 1438
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x0400059F RID: 1439
		private MeshFilter m_meshFilter;

		// Token: 0x040005A0 RID: 1440
		private bool m_isFirstAllocation;

		// Token: 0x040005A1 RID: 1441
		private int m_max_characters = 8;

		// Token: 0x040005A2 RID: 1442
		private int m_max_numberOfLines = 4;

		// Token: 0x040005A3 RID: 1443
		private TMP_SubMesh[] m_subTextObjects = new TMP_SubMesh[8];

		// Token: 0x040005A4 RID: 1444
		[SerializeField]
		private MaskingTypes m_maskType;

		// Token: 0x040005A5 RID: 1445
		private Matrix4x4 m_EnvMapMatrix;

		// Token: 0x040005A6 RID: 1446
		private Vector3[] m_RectTransformCorners = new Vector3[4];

		// Token: 0x040005A7 RID: 1447
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x040005A8 RID: 1448
		private static ProfilerMarker k_GenerateTextMarker = new ProfilerMarker("TMP Layout Text");

		// Token: 0x040005A9 RID: 1449
		private static ProfilerMarker k_SetArraySizesMarker = new ProfilerMarker("TMP.SetArraySizes");

		// Token: 0x040005AA RID: 1450
		private static ProfilerMarker k_GenerateTextPhaseIMarker = new ProfilerMarker("TMP GenerateText - Phase I");

		// Token: 0x040005AB RID: 1451
		private static ProfilerMarker k_ParseMarkupTextMarker = new ProfilerMarker("TMP Parse Markup Text");

		// Token: 0x040005AC RID: 1452
		private static ProfilerMarker k_CharacterLookupMarker = new ProfilerMarker("TMP Lookup Character & Glyph Data");

		// Token: 0x040005AD RID: 1453
		private static ProfilerMarker k_HandleGPOSFeaturesMarker = new ProfilerMarker("TMP Handle GPOS Features");

		// Token: 0x040005AE RID: 1454
		private static ProfilerMarker k_CalculateVerticesPositionMarker = new ProfilerMarker("TMP Calculate Vertices Position");

		// Token: 0x040005AF RID: 1455
		private static ProfilerMarker k_ComputeTextMetricsMarker = new ProfilerMarker("TMP Compute Text Metrics");

		// Token: 0x040005B0 RID: 1456
		private static ProfilerMarker k_HandleVisibleCharacterMarker = new ProfilerMarker("TMP Handle Visible Character");

		// Token: 0x040005B1 RID: 1457
		private static ProfilerMarker k_HandleWhiteSpacesMarker = new ProfilerMarker("TMP Handle White Space & Control Character");

		// Token: 0x040005B2 RID: 1458
		private static ProfilerMarker k_HandleHorizontalLineBreakingMarker = new ProfilerMarker("TMP Handle Horizontal Line Breaking");

		// Token: 0x040005B3 RID: 1459
		private static ProfilerMarker k_HandleVerticalLineBreakingMarker = new ProfilerMarker("TMP Handle Vertical Line Breaking");

		// Token: 0x040005B4 RID: 1460
		private static ProfilerMarker k_SaveGlyphVertexDataMarker = new ProfilerMarker("TMP Save Glyph Vertex Data");

		// Token: 0x040005B5 RID: 1461
		private static ProfilerMarker k_ComputeCharacterAdvanceMarker = new ProfilerMarker("TMP Compute Character Advance");

		// Token: 0x040005B6 RID: 1462
		private static ProfilerMarker k_HandleCarriageReturnMarker = new ProfilerMarker("TMP Handle Carriage Return");

		// Token: 0x040005B7 RID: 1463
		private static ProfilerMarker k_HandleLineTerminationMarker = new ProfilerMarker("TMP Handle Line Termination");

		// Token: 0x040005B8 RID: 1464
		private static ProfilerMarker k_SavePageInfoMarker = new ProfilerMarker("TMP Save Page Info");

		// Token: 0x040005B9 RID: 1465
		private static ProfilerMarker k_SaveTextExtentMarker = new ProfilerMarker("TMP Save Text Extent");

		// Token: 0x040005BA RID: 1466
		private static ProfilerMarker k_SaveProcessingStatesMarker = new ProfilerMarker("TMP Save Processing States");

		// Token: 0x040005BB RID: 1467
		private static ProfilerMarker k_GenerateTextPhaseIIMarker = new ProfilerMarker("TMP GenerateText - Phase II");

		// Token: 0x040005BC RID: 1468
		private static ProfilerMarker k_GenerateTextPhaseIIIMarker = new ProfilerMarker("TMP GenerateText - Phase III");

		// Token: 0x040005BD RID: 1469
		private Dictionary<int, int> materialIndexPairs = new Dictionary<int, int>();
	}
}
