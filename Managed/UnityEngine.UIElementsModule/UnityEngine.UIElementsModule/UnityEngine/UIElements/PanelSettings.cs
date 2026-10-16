using System;
using System.Diagnostics;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C3 RID: 707
	[HelpURL("UIE-Runtime-Panel-Settings")]
	public class PanelSettings : ScriptableObject
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00050DA8 File Offset: 0x0004EFA8
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00050DC0 File Offset: 0x0004EFC0
		public ThemeStyleSheet themeStyleSheet
		{
			get
			{
				return this.themeUss;
			}
			set
			{
				this.themeUss = value;
				this.ApplyThemeStyleSheet(null);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00050DD2 File Offset: 0x0004EFD2
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00050DDA File Offset: 0x0004EFDA
		public RenderTexture targetTexture
		{
			get
			{
				return this.m_TargetTexture;
			}
			set
			{
				this.m_TargetTexture = value;
				this.m_PanelAccess.SetTargetTexture();
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00050DF0 File Offset: 0x0004EFF0
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		public PanelRenderMode renderMode
		{
			get
			{
				return this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00050E01 File Offset: 0x0004F001
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00050E09 File Offset: 0x0004F009
		internal int worldSpaceLayer
		{
			get
			{
				return this.m_WorldSpaceLayer;
			}
			set
			{
				this.m_WorldSpaceLayer = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00050E12 File Offset: 0x0004F012
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00050E1A File Offset: 0x0004F01A
		public PanelScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleMode = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00050E24 File Offset: 0x0004F024
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x00050E3C File Offset: 0x0004F03C
		public float referenceSpritePixelsPerUnit
		{
			get
			{
				return this.m_ReferenceSpritePixelsPerUnit;
			}
			set
			{
				this.m_ReferenceSpritePixelsPerUnit = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00050E48 File Offset: 0x0004F048
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x00050E60 File Offset: 0x0004F060
		internal float pixelsPerUnit
		{
			get
			{
				return this.m_PixelsPerUnit;
			}
			set
			{
				this.m_PixelsPerUnit = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00050E6A File Offset: 0x0004F06A
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00050E72 File Offset: 0x0004F072
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00050E7B File Offset: 0x0004F07B
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x00050E83 File Offset: 0x0004F083
		public float referenceDpi
		{
			get
			{
				return this.m_ReferenceDpi;
			}
			set
			{
				this.m_ReferenceDpi = ((value >= 1f) ? value : 96f);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00050E9B File Offset: 0x0004F09B
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x00050EA3 File Offset: 0x0004F0A3
		public float fallbackDpi
		{
			get
			{
				return this.m_FallbackDpi;
			}
			set
			{
				this.m_FallbackDpi = ((value >= 1f) ? value : 96f);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00050EBB File Offset: 0x0004F0BB
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00050EC3 File Offset: 0x0004F0C3
		public Vector2Int referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00050ECC File Offset: 0x0004F0CC
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00050ED4 File Offset: 0x0004F0D4
		public PanelScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00050EDD File Offset: 0x0004F0DD
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x00050EE5 File Offset: 0x0004F0E5
		public float match
		{
			get
			{
				return this.m_Match;
			}
			set
			{
				this.m_Match = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00050EEE File Offset: 0x0004F0EE
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x00050EF6 File Offset: 0x0004F0F6
		public float sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				this.m_SortingOrder = value;
				this.ApplySortingOrder();
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00050F07 File Offset: 0x0004F107
		internal void ApplySortingOrder()
		{
			this.m_PanelAccess.SetSortingPriority();
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00050F16 File Offset: 0x0004F116
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00050F1E File Offset: 0x0004F11E
		public int targetDisplay
		{
			get
			{
				return this.m_TargetDisplay;
			}
			set
			{
				this.m_TargetDisplay = value;
				this.m_PanelAccess.SetTargetDisplay();
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00050F34 File Offset: 0x0004F134
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00050F3C File Offset: 0x0004F13C
		public BindingLogLevel bindingLogLevel
		{
			get
			{
				return this.m_BindingLogLevel;
			}
			set
			{
				bool flag = this.m_BindingLogLevel == value;
				if (!flag)
				{
					this.m_BindingLogLevel = value;
					Binding.SetPanelLogLevel(this.panel, value);
				}
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00050F6D File Offset: 0x0004F16D
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00050F75 File Offset: 0x0004F175
		public bool clearDepthStencil
		{
			get
			{
				return this.m_ClearDepthStencil;
			}
			set
			{
				this.m_ClearDepthStencil = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00050F7E File Offset: 0x0004F17E
		public float depthClearValue
		{
			get
			{
				return 0.99f;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00050F85 File Offset: 0x0004F185
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x00050F8D File Offset: 0x0004F18D
		public bool clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00050F96 File Offset: 0x0004F196
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x00050F9E File Offset: 0x0004F19E
		public Color colorClearValue
		{
			get
			{
				return this.m_ColorClearValue;
			}
			set
			{
				this.m_ColorClearValue = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00050FA7 File Offset: 0x0004F1A7
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00050FAF File Offset: 0x0004F1AF
		public uint vertexBudget
		{
			get
			{
				return this.m_VertexBudget;
			}
			set
			{
				this.m_VertexBudget = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		internal BaseRuntimePanel panel
		{
			get
			{
				return this.m_PanelAccess.panel;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00050FC5 File Offset: 0x0004F1C5
		internal bool isInitialized
		{
			get
			{
				PanelSettings.RuntimePanelAccess panelAccess = this.m_PanelAccess;
				return panelAccess != null && panelAccess.isInitialized;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00050FD9 File Offset: 0x0004F1D9
		internal VisualElement visualTree
		{
			get
			{
				return this.m_PanelAccess.panel.visualTree;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00050FEB File Offset: 0x0004F1EB
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x00050FF3 File Offset: 0x0004F1F3
		public DynamicAtlasSettings dynamicAtlasSettings
		{
			get
			{
				return this.m_DynamicAtlasSettings;
			}
			set
			{
				this.m_DynamicAtlasSettings = value;
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00050FFC File Offset: 0x0004F1FC
		private PanelSettings()
		{
			this.m_PanelAccess = new PanelSettings.RuntimePanelAccess(this);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00002152 File Offset: 0x00000352
		private void Reset()
		{
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000510D4 File Offset: 0x0004F2D4
		private void OnEnable()
		{
			bool flag = !this.m_DisableNoThemeWarning && this.themeUss == null;
			if (flag)
			{
				Debug.LogWarning("No Theme Style Sheet set to PanelSettings " + base.name + ", UI will not render properly", this);
			}
			this.UpdateScreenDPI();
			this.InitializeShaders();
			this.AssignICUData();
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00051132 File Offset: 0x0004F332
		private void OnDisable()
		{
			this.m_PanelAccess.DisposePanel();
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00051132 File Offset: 0x0004F332
		internal void DisposePanel()
		{
			this.m_PanelAccess.DisposePanel();
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00051141 File Offset: 0x0004F341
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x00051149 File Offset: 0x0004F349
		private float ScreenDPI { get; set; }

		// Token: 0x06001459 RID: 5209 RVA: 0x00051152 File Offset: 0x0004F352
		[Conditional("ENABLE_PROFILER")]
		public void SetPanelChangeReceiver(IDebugPanelChangeReceiver value)
		{
			this.m_PanelChangeReceiver = value;
			this.m_PanelAccess.SetPanelChangeReceiver();
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00051168 File Offset: 0x0004F368
		internal IDebugPanelChangeReceiver GetPanelChangeReceiver()
		{
			return this.m_PanelChangeReceiver;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00051180 File Offset: 0x0004F380
		internal void UpdateScreenDPI()
		{
			this.ScreenDPI = Screen.dpi;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00051190 File Offset: 0x0004F390
		private void ApplyThemeStyleSheet(VisualElement root = null)
		{
			bool flag = !this.m_PanelAccess.isInitialized;
			if (!flag)
			{
				bool flag2 = root == null;
				if (flag2)
				{
					root = this.visualTree;
				}
				bool flag3 = this.m_OldThemeUss != this.themeUss && this.m_OldThemeUss != null;
				if (flag3)
				{
					if (root != null)
					{
						root.styleSheets.Remove(this.m_OldThemeUss);
					}
				}
				bool flag4 = this.themeUss != null;
				if (flag4)
				{
					this.themeUss.isDefaultStyleSheet = true;
					if (root != null)
					{
						root.styleSheets.Add(this.themeUss);
					}
				}
				this.m_OldThemeUss = this.themeUss;
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00051254 File Offset: 0x0004F454
		internal bool AssignICUData()
		{
			return false;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00051268 File Offset: 0x0004F468
		private void InitializeShaders()
		{
			bool flag = this.m_AtlasBlitShader == null;
			if (flag)
			{
				this.m_AtlasBlitShader = Shader.Find(Shaders.k_AtlasBlit);
			}
			bool flag2 = this.m_RuntimeShader == null;
			if (flag2)
			{
				this.m_RuntimeShader = Shader.Find(Shaders.k_Runtime);
			}
			bool flag3 = this.m_RuntimeWorldShader == null;
			if (flag3)
			{
				this.m_RuntimeWorldShader = Shader.Find(Shaders.k_RuntimeWorld);
			}
			bool flag4 = this.m_SDFShader == null;
			if (flag4)
			{
				this.m_SDFShader = Shader.Find(TextShaderUtilities.k_SDFText);
			}
			bool flag5 = this.m_BitmapShader == null;
			if (flag5)
			{
				this.m_BitmapShader = Shader.Find(TextShaderUtilities.k_BitmapText);
			}
			bool flag6 = this.m_SpriteShader == null;
			if (flag6)
			{
				this.m_SpriteShader = Shader.Find(TextShaderUtilities.k_SpriteText);
			}
			this.m_PanelAccess.SetTargetTexture();
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00051354 File Offset: 0x0004F554
		internal void ApplyPanelSettings()
		{
			Rect targetRect = this.m_TargetRect;
			float resolvedScale = this.m_ResolvedScale;
			this.UpdateScreenDPI();
			this.m_TargetRect = this.GetDisplayRect();
			bool flag = this.renderMode == PanelRenderMode.WorldSpace;
			if (flag)
			{
				this.m_ResolvedScale = 1f;
			}
			else
			{
				this.m_ResolvedScale = this.ResolveScale(this.m_TargetRect, this.ScreenDPI);
			}
			BaseRuntimePanel panel = this.panel;
			bool flag2 = this.renderMode != PanelRenderMode.WorldSpace;
			if (flag2)
			{
				bool flag3 = this.visualTree.style.width.value == 0f || this.m_ResolvedScale != resolvedScale || this.m_TargetRect.width != targetRect.width || this.m_TargetRect.height != targetRect.height;
				if (flag3)
				{
					panel.scale = ((this.m_ResolvedScale == 0f) ? 0f : (1f / this.m_ResolvedScale));
					this.visualTree.style.left = 0f;
					this.visualTree.style.top = 0f;
					this.visualTree.style.width = this.m_TargetRect.width * this.m_ResolvedScale;
					this.visualTree.style.height = this.m_TargetRect.height * this.m_ResolvedScale;
				}
				panel.panelRenderer.forceGammaRendering = this.targetTexture != null && this.forceGammaRendering;
			}
			panel.targetTexture = this.targetTexture;
			panel.targetDisplay = this.targetDisplay;
			panel.drawsInCameras = this.renderMode == PanelRenderMode.WorldSpace;
			panel.pixelsPerUnit = this.pixelsPerUnit;
			panel.isFlat = this.renderMode != PanelRenderMode.WorldSpace;
			panel.worldSpaceLayer = this.worldSpaceLayer;
			panel.clearSettings = new PanelClearSettings
			{
				clearColor = this.m_ClearColor,
				clearDepthStencil = this.m_ClearDepthStencil,
				color = this.m_ColorClearValue
			};
			panel.referenceSpritePixelsPerUnit = this.referenceSpritePixelsPerUnit;
			panel.panelRenderer.vertexBudget = this.m_VertexBudget;
			panel.dataBindingManager.logLevel = this.m_BindingLogLevel;
			DynamicAtlas dynamicAtlas = panel.atlas as DynamicAtlas;
			bool flag4 = dynamicAtlas != null;
			if (flag4)
			{
				dynamicAtlas.minAtlasSize = this.dynamicAtlasSettings.minAtlasSize;
				dynamicAtlas.maxAtlasSize = this.dynamicAtlasSettings.maxAtlasSize;
				dynamicAtlas.maxSubTextureSize = this.dynamicAtlasSettings.maxSubTextureSize;
				dynamicAtlas.activeFilters = this.dynamicAtlasSettings.activeFilters;
				dynamicAtlas.customFilter = this.dynamicAtlasSettings.customFilter;
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00051641 File Offset: 0x0004F841
		public void SetScreenToPanelSpaceFunction(Func<Vector2, Vector2> screentoPanelSpaceFunction)
		{
			this.m_AssignedScreenToPanel = screentoPanelSpaceFunction;
			this.panel.screenToPanelSpace = this.m_AssignedScreenToPanel;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00051660 File Offset: 0x0004F860
		internal float ResolveScale(Rect targetRect, float screenDpi)
		{
			float num = 1f;
			switch (this.scaleMode)
			{
			case PanelScaleMode.ConstantPhysicalSize:
			{
				float num2 = ((screenDpi == 0f) ? this.fallbackDpi : screenDpi);
				bool flag = num2 != 0f;
				if (flag)
				{
					num = this.referenceDpi / num2;
				}
				break;
			}
			case PanelScaleMode.ScaleWithScreenSize:
			{
				bool flag2 = this.referenceResolution.x * this.referenceResolution.y != 0;
				if (flag2)
				{
					Vector2 vector = this.referenceResolution;
					Vector2 vector2 = new Vector2(targetRect.width / vector.x, targetRect.height / vector.y);
					PanelScreenMatchMode screenMatchMode = this.screenMatchMode;
					PanelScreenMatchMode panelScreenMatchMode = screenMatchMode;
					float num4;
					if (panelScreenMatchMode != PanelScreenMatchMode.Shrink)
					{
						if (panelScreenMatchMode != PanelScreenMatchMode.Expand)
						{
							float num3 = Mathf.Clamp01(this.match);
							num4 = Mathf.Lerp(vector2.x, vector2.y, num3);
						}
						else
						{
							num4 = Mathf.Min(vector2.x, vector2.y);
						}
					}
					else
					{
						num4 = Mathf.Max(vector2.x, vector2.y);
					}
					bool flag3 = num4 != 0f;
					if (flag3)
					{
						num = 1f / num4;
					}
				}
				break;
			}
			}
			bool flag4 = this.scale > 0f;
			if (flag4)
			{
				num /= this.scale;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000517EC File Offset: 0x0004F9EC
		internal Rect GetDisplayRect()
		{
			bool flag = this.m_TargetTexture != null;
			Rect rect;
			if (flag)
			{
				rect = new Rect(0f, 0f, (float)this.m_TargetTexture.width, (float)this.m_TargetTexture.height);
			}
			else
			{
				rect = new Rect(0f, 0f, (float)BaseRuntimePanel.getScreenRenderingWidth(this.targetDisplay), (float)BaseRuntimePanel.getScreenRenderingHeight(this.targetDisplay));
			}
			return rect;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00051860 File Offset: 0x0004FA60
		internal void AttachAndInsertUIDocumentToVisualTree(UIDocument uiDocument)
		{
			bool flag = this.m_AttachedUIDocumentsList == null;
			if (flag)
			{
				this.m_AttachedUIDocumentsList = new UIDocumentList();
			}
			else
			{
				this.m_AttachedUIDocumentsList.RemoveFromListAndFromVisualTree(uiDocument);
			}
			this.m_AttachedUIDocumentsList.AddToListAndToVisualTree(uiDocument, this.visualTree, 0);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000518B0 File Offset: 0x0004FAB0
		internal void DetachUIDocument(UIDocument uiDocument)
		{
			bool flag = this.m_AttachedUIDocumentsList == null;
			if (!flag)
			{
				this.m_AttachedUIDocumentsList.RemoveFromListAndFromVisualTree(uiDocument);
				bool flag2 = this.m_AttachedUIDocumentsList.m_AttachedUIDocuments.Count == 0;
				if (flag2)
				{
					this.m_PanelAccess.MarkPotentiallyEmpty();
				}
			}
		}

		// Token: 0x04000AF1 RID: 2801
		private const int k_DefaultSortingOrder = 0;

		// Token: 0x04000AF2 RID: 2802
		private const float k_DefaultScaleValue = 1f;

		// Token: 0x04000AF3 RID: 2803
		internal const string k_DefaultStyleSheetPath = "Packages/com.unity.ui/PackageResources/StyleSheets/Generated/Default.tss.asset";

		// Token: 0x04000AF4 RID: 2804
		[SerializeField]
		private ThemeStyleSheet themeUss;

		// Token: 0x04000AF5 RID: 2805
		[SerializeField]
		private bool m_DisableNoThemeWarning = false;

		// Token: 0x04000AF6 RID: 2806
		[SerializeField]
		private RenderTexture m_TargetTexture;

		// Token: 0x04000AF7 RID: 2807
		[SerializeField]
		private PanelRenderMode m_RenderMode = PanelRenderMode.ScreenSpaceOverlay;

		// Token: 0x04000AF8 RID: 2808
		[SerializeField]
		private int m_WorldSpaceLayer = 0;

		// Token: 0x04000AF9 RID: 2809
		[SerializeField]
		private PanelScaleMode m_ScaleMode = PanelScaleMode.ConstantPhysicalSize;

		// Token: 0x04000AFA RID: 2810
		[SerializeField]
		private float m_ReferenceSpritePixelsPerUnit = 100f;

		// Token: 0x04000AFB RID: 2811
		[SerializeField]
		private float m_PixelsPerUnit = 100f;

		// Token: 0x04000AFC RID: 2812
		[SerializeField]
		private float m_Scale = 1f;

		// Token: 0x04000AFD RID: 2813
		private const float DefaultDpi = 96f;

		// Token: 0x04000AFE RID: 2814
		[SerializeField]
		private float m_ReferenceDpi = 96f;

		// Token: 0x04000AFF RID: 2815
		[SerializeField]
		private float m_FallbackDpi = 96f;

		// Token: 0x04000B00 RID: 2816
		[SerializeField]
		private Vector2Int m_ReferenceResolution = new Vector2Int(1200, 800);

		// Token: 0x04000B01 RID: 2817
		[SerializeField]
		private PanelScreenMatchMode m_ScreenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;

		// Token: 0x04000B02 RID: 2818
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Match = 0f;

		// Token: 0x04000B03 RID: 2819
		[SerializeField]
		private float m_SortingOrder = 0f;

		// Token: 0x04000B04 RID: 2820
		[SerializeField]
		private int m_TargetDisplay = 0;

		// Token: 0x04000B05 RID: 2821
		[SerializeField]
		private BindingLogLevel m_BindingLogLevel;

		// Token: 0x04000B06 RID: 2822
		[SerializeField]
		private bool m_ClearDepthStencil = true;

		// Token: 0x04000B07 RID: 2823
		[SerializeField]
		private bool m_ClearColor;

		// Token: 0x04000B08 RID: 2824
		[SerializeField]
		private Color m_ColorClearValue = Color.clear;

		// Token: 0x04000B09 RID: 2825
		[SerializeField]
		private uint m_VertexBudget = 0U;

		// Token: 0x04000B0A RID: 2826
		private PanelSettings.RuntimePanelAccess m_PanelAccess;

		// Token: 0x04000B0B RID: 2827
		internal UIDocumentList m_AttachedUIDocumentsList;

		// Token: 0x04000B0C RID: 2828
		[HideInInspector]
		[SerializeField]
		private DynamicAtlasSettings m_DynamicAtlasSettings = DynamicAtlasSettings.defaults;

		// Token: 0x04000B0D RID: 2829
		[SerializeField]
		[HideInInspector]
		private Shader m_AtlasBlitShader;

		// Token: 0x04000B0E RID: 2830
		[SerializeField]
		[HideInInspector]
		private Shader m_RuntimeShader;

		// Token: 0x04000B0F RID: 2831
		[HideInInspector]
		[SerializeField]
		private Shader m_RuntimeWorldShader;

		// Token: 0x04000B10 RID: 2832
		[SerializeField]
		[HideInInspector]
		private Shader m_SDFShader;

		// Token: 0x04000B11 RID: 2833
		[SerializeField]
		[HideInInspector]
		private Shader m_BitmapShader;

		// Token: 0x04000B12 RID: 2834
		[SerializeField]
		[HideInInspector]
		private Shader m_SpriteShader;

		// Token: 0x04000B13 RID: 2835
		[SerializeField]
		[HideInInspector]
		internal TextAsset m_ICUDataAsset;

		// Token: 0x04000B14 RID: 2836
		[SerializeField]
		public bool forceGammaRendering;

		// Token: 0x04000B15 RID: 2837
		[SerializeField]
		public PanelTextSettings textSettings;

		// Token: 0x04000B16 RID: 2838
		private Rect m_TargetRect;

		// Token: 0x04000B17 RID: 2839
		private float m_ResolvedScale;

		// Token: 0x04000B18 RID: 2840
		private StyleSheet m_OldThemeUss;

		// Token: 0x04000B1A RID: 2842
		private IDebugPanelChangeReceiver m_PanelChangeReceiver = null;

		// Token: 0x04000B1B RID: 2843
		private Func<Vector2, Vector2> m_AssignedScreenToPanel;

		// Token: 0x020002C4 RID: 708
		private class RuntimePanelAccess
		{
			// Token: 0x06001465 RID: 5221 RVA: 0x000518FE File Offset: 0x0004FAFE
			internal RuntimePanelAccess(PanelSettings settings)
			{
				this.m_Settings = settings;
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x06001466 RID: 5222 RVA: 0x0005190F File Offset: 0x0004FB0F
			internal bool isInitialized
			{
				get
				{
					return this.m_RuntimePanel != null;
				}
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005191C File Offset: 0x0004FB1C
			internal BaseRuntimePanel panel
			{
				get
				{
					bool flag = this.m_RuntimePanel == null;
					if (flag)
					{
						this.m_RuntimePanel = this.CreateRelatedRuntimePanel();
						this.m_RuntimePanel.sortingPriority = this.m_Settings.m_SortingOrder;
						this.m_RuntimePanel.targetDisplay = this.m_Settings.m_TargetDisplay;
						this.m_RuntimePanel.panelChangeReceiver = this.m_Settings.GetPanelChangeReceiver();
						VisualElement visualTree = this.m_RuntimePanel.visualTree;
						visualTree.name = this.m_Settings.name;
						this.m_Settings.ApplyPanelSettings();
						this.m_Settings.ApplyThemeStyleSheet(visualTree);
						bool flag2 = this.m_Settings.m_TargetTexture != null;
						if (flag2)
						{
							this.m_RuntimePanel.targetTexture = this.m_Settings.m_TargetTexture;
						}
						bool flag3 = this.m_Settings.m_AssignedScreenToPanel != null;
						if (flag3)
						{
							this.m_Settings.SetScreenToPanelSpaceFunction(this.m_Settings.m_AssignedScreenToPanel);
						}
					}
					return this.m_RuntimePanel;
				}
			}

			// Token: 0x06001468 RID: 5224 RVA: 0x00051A28 File Offset: 0x0004FC28
			internal void DisposePanel()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.DisposeRelatedPanel();
					this.m_RuntimePanel = null;
				}
			}

			// Token: 0x06001469 RID: 5225 RVA: 0x00051A54 File Offset: 0x0004FC54
			internal void SetTargetTexture()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.targetTexture = this.m_Settings.targetTexture;
				}
			}

			// Token: 0x0600146A RID: 5226 RVA: 0x00051A88 File Offset: 0x0004FC88
			internal void SetSortingPriority()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.sortingPriority = this.m_Settings.m_SortingOrder;
				}
			}

			// Token: 0x0600146B RID: 5227 RVA: 0x00051ABC File Offset: 0x0004FCBC
			internal void SetTargetDisplay()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.targetDisplay = this.m_Settings.m_TargetDisplay;
				}
			}

			// Token: 0x0600146C RID: 5228 RVA: 0x00051AF0 File Offset: 0x0004FCF0
			internal void SetPanelChangeReceiver()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.panelChangeReceiver = this.m_Settings.m_PanelChangeReceiver;
				}
			}

			// Token: 0x0600146D RID: 5229 RVA: 0x00051B24 File Offset: 0x0004FD24
			private BaseRuntimePanel CreateRelatedRuntimePanel()
			{
				return (RuntimePanel)UIElementsRuntimeUtility.FindOrCreateRuntimePanel(this.m_Settings, new UIElementsRuntimeUtility.CreateRuntimePanelDelegate(RuntimePanel.Create));
			}

			// Token: 0x0600146E RID: 5230 RVA: 0x00051B54 File Offset: 0x0004FD54
			private void DisposeRelatedPanel()
			{
				UIElementsRuntimeUtility.DisposeRuntimePanel(this.m_Settings);
			}

			// Token: 0x0600146F RID: 5231 RVA: 0x00051B63 File Offset: 0x0004FD63
			internal void MarkPotentiallyEmpty()
			{
				UIElementsRuntimeUtility.MarkPotentiallyEmpty(this.m_Settings);
			}

			// Token: 0x04000B1C RID: 2844
			private readonly PanelSettings m_Settings;

			// Token: 0x04000B1D RID: 2845
			private BaseRuntimePanel m_RuntimePanel;
		}
	}
}
