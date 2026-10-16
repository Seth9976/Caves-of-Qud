using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200004F RID: 79
	[RequireComponent(typeof(MeshRenderer))]
	[ExecuteAlways]
	public class TMP_SubMesh : MonoBehaviour
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001443D File Offset: 0x0001263D
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00014445 File Offset: 0x00012645
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0001444E File Offset: 0x0001264E
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00014456 File Offset: 0x00012656
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0001445F File Offset: 0x0001265F
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00014470 File Offset: 0x00012670
		public Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000144B9 File Offset: 0x000126B9
		// (set) Token: 0x06000377 RID: 887 RVA: 0x000144C1 File Offset: 0x000126C1
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000144CA File Offset: 0x000126CA
		// (set) Token: 0x06000379 RID: 889 RVA: 0x000144D4 File Offset: 0x000126D4
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00014535 File Offset: 0x00012735
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0001453D File Offset: 0x0001273D
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00014546 File Offset: 0x00012746
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0001454E File Offset: 0x0001274E
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00014557 File Offset: 0x00012757
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0001455F File Offset: 0x0001275F
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00014568 File Offset: 0x00012768
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001458C File Offset: 0x0001278C
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000145E5 File Offset: 0x000127E5
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00014613 File Offset: 0x00012813
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001461C File Offset: 0x0001281C
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshPro>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00014640 File Offset: 0x00012840
		public static TMP_SubMesh AddSubTextObject(TextMeshPro textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject();
			gameObject.hideFlags = (TMP_Settings.hideSubTextObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = textComponent.gameObject.layer;
			TMP_SubMesh tmp_SubMesh = gameObject.AddComponent<TMP_SubMesh>();
			tmp_SubMesh.m_TextComponent = textComponent;
			tmp_SubMesh.m_fontAsset = materialReference.fontAsset;
			tmp_SubMesh.m_spriteAsset = materialReference.spriteAsset;
			tmp_SubMesh.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tmp_SubMesh.SetSharedMaterial(materialReference.material);
			tmp_SubMesh.renderer.sortingLayerID = textComponent.renderer.sortingLayerID;
			tmp_SubMesh.renderer.sortingOrder = textComponent.renderer.sortingOrder;
			return tmp_SubMesh;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00014720 File Offset: 0x00012920
		private void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != HideFlags.DontSave)
			{
				base.hideFlags = HideFlags.DontSave;
			}
			this.meshFilter.sharedMesh = this.mesh;
			if (this.m_sharedMaterial != null)
			{
				this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, new Vector4(-32767f, -32767f, 32767f, 32767f));
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00014796 File Offset: 0x00012996
		private void OnDisable()
		{
			this.m_meshFilter.sharedMesh = null;
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000147C4 File Offset: 0x000129C4
		private void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetAllDirty();
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00014836 File Offset: 0x00012A36
		public void DestroySelf()
		{
			global::UnityEngine.Object.Destroy(base.gameObject, 1f);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00014848 File Offset: 0x00012A48
		private Material GetMaterial(Material mat)
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
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

		// Token: 0x0600038B RID: 907 RVA: 0x000148C7 File Offset: 0x00012AC7
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000148F1 File Offset: 0x00012AF1
		private Material GetSharedMaterial()
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer.sharedMaterial;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014918 File Offset: 0x00012B18
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00014933 File Offset: 0x00012B33
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00014956 File Offset: 0x00012B56
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001496B File Offset: 0x00012B6B
		public void SetVerticesDirty()
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001496D File Offset: 0x00012B6D
		public void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00014978 File Offset: 0x00012B78
		protected void UpdateMaterial()
		{
			if (this.renderer == null || this.m_sharedMaterial == null)
			{
				return;
			}
			this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ShaderTag_CullMode) && this.textComponent.fontSharedMaterial != null)
			{
				float @float = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, @float);
			}
		}

		// Token: 0x0400031A RID: 794
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x0400031B RID: 795
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x0400031C RID: 796
		[SerializeField]
		private Material m_material;

		// Token: 0x0400031D RID: 797
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x0400031E RID: 798
		private Material m_fallbackMaterial;

		// Token: 0x0400031F RID: 799
		private Material m_fallbackSourceMaterial;

		// Token: 0x04000320 RID: 800
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x04000321 RID: 801
		[SerializeField]
		private float m_padding;

		// Token: 0x04000322 RID: 802
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x04000323 RID: 803
		private MeshFilter m_meshFilter;

		// Token: 0x04000324 RID: 804
		private Mesh m_mesh;

		// Token: 0x04000325 RID: 805
		[SerializeField]
		private TextMeshPro m_TextComponent;

		// Token: 0x04000326 RID: 806
		[NonSerialized]
		private bool m_isRegisteredForEvents;
	}
}
