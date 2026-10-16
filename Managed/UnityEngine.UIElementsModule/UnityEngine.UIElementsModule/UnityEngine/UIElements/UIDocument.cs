using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CB RID: 715
	[ExecuteAlways]
	[HelpURL("UIE-get-started-with-runtime-ui")]
	[AddComponentMenu("UI Toolkit/UI Document")]
	[DefaultExecutionOrder(-100)]
	[DisallowMultipleComponent]
	public sealed class UIDocument : MonoBehaviour
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00051FF8 File Offset: 0x000501F8
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00052010 File Offset: 0x00050210
		public PanelSettings panelSettings
		{
			get
			{
				return this.m_PanelSettings;
			}
			set
			{
				bool flag = this.parentUI == null;
				if (flag)
				{
					bool flag2 = this.m_PanelSettings == value;
					if (flag2)
					{
						this.m_PreviousPanelSettings = this.m_PanelSettings;
						return;
					}
					bool flag3 = this.m_PanelSettings != null;
					if (flag3)
					{
						this.m_PanelSettings.DetachUIDocument(this);
					}
					this.m_PanelSettings = value;
					bool flag4 = this.m_PanelSettings != null;
					if (flag4)
					{
						this.m_PanelSettings.AttachAndInsertUIDocumentToVisualTree(this);
					}
				}
				else
				{
					Assert.AreEqual<PanelSettings>(this.parentUI.m_PanelSettings, value);
					this.m_PanelSettings = this.parentUI.m_PanelSettings;
				}
				bool flag5 = this.m_ChildrenContent != null;
				if (flag5)
				{
					foreach (UIDocument uidocument in this.m_ChildrenContent.m_AttachedUIDocuments)
					{
						uidocument.panelSettings = this.m_PanelSettings;
					}
				}
				this.m_PreviousPanelSettings = this.m_PanelSettings;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00052134 File Offset: 0x00050334
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x0005213C File Offset: 0x0005033C
		public UIDocument parentUI
		{
			get
			{
				return this.m_ParentUI;
			}
			private set
			{
				this.m_ParentUI = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00052148 File Offset: 0x00050348
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00052160 File Offset: 0x00050360
		public VisualTreeAsset visualTreeAsset
		{
			get
			{
				return this.sourceAsset;
			}
			set
			{
				this.sourceAsset = value;
				this.RecreateUI();
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00052174 File Offset: 0x00050374
		public VisualElement rootVisualElement
		{
			get
			{
				return this.m_RootVisualElement;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0005218C File Offset: 0x0005038C
		internal int firstChildInserIndex
		{
			get
			{
				return this.m_FirstChildInsertIndex;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00052194 File Offset: 0x00050394
		internal UIDocument.WorldSpaceSizeMode worldSpaceSizeMode
		{
			get
			{
				return this.m_WorldSpaceSizeMode;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0005219C File Offset: 0x0005039C
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x000521A4 File Offset: 0x000503A4
		public float sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				bool flag = this.m_SortingOrder == value;
				if (!flag)
				{
					this.m_SortingOrder = value;
					this.ApplySortingOrder();
				}
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000521D0 File Offset: 0x000503D0
		internal void ApplySortingOrder()
		{
			this.AddRootVisualElementToTree();
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000521DC File Offset: 0x000503DC
		private UIDocument()
		{
			this.m_UIDocumentCreationIndex = UIDocument.s_CurrentUIDocumentCounter++;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00052241 File Offset: 0x00050441
		private void Awake()
		{
			this.SetupFromHierarchy();
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005224C File Offset: 0x0005044C
		private void OnEnable()
		{
			bool flag = this.parentUI != null && this.m_PanelSettings == null;
			if (flag)
			{
				this.m_PanelSettings = this.parentUI.m_PanelSettings;
			}
			bool flag2 = this.m_RootVisualElement == null;
			if (flag2)
			{
				this.RecreateUI();
			}
			else
			{
				this.AddRootVisualElementToTree();
			}
			UIRenderer uirenderer;
			bool flag3 = base.TryGetComponent<UIRenderer>(out uirenderer);
			if (flag3)
			{
				uirenderer.enabled = true;
			}
			this.ResolveRuntimePanel();
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000522CC File Offset: 0x000504CC
		private void ResolveRuntimePanel()
		{
			bool flag = this.m_RuntimePanel == null;
			if (flag)
			{
				VisualElement rootVisualElement = this.rootVisualElement;
				this.m_RuntimePanel = ((rootVisualElement != null) ? rootVisualElement.panel : null) as RuntimePanel;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00052304 File Offset: 0x00050504
		public IRuntimePanel runtimePanel
		{
			get
			{
				this.ResolveRuntimePanel();
				return this.m_RuntimePanel;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00052324 File Offset: 0x00050524
		private void LateUpdate()
		{
			bool flag = this.m_RootVisualElement == null || this.panelSettings == null || this.panelSettings.panel == null;
			if (!flag)
			{
				this.AddOrRemoveRendererComponent();
				bool flag2 = !this.panelSettings.panel.isFlat;
				if (flag2)
				{
					this.ResolveRuntimePanel();
					this.SetTransform();
					this.UpdateRenderer();
				}
				else
				{
					bool rootHasWorldTransform = this.m_RootHasWorldTransform;
					if (rootHasWorldTransform)
					{
						this.ClearTransform();
					}
				}
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000523A8 File Offset: 0x000505A8
		private void UpdateRenderer()
		{
			UIRenderer uirenderer;
			bool flag = !base.TryGetComponent<UIRenderer>(out uirenderer);
			if (flag)
			{
				this.rootVisualElement.uiRenderer = null;
				this.UpdateCutRenderChainFlag();
			}
			else
			{
				this.rootVisualElement.uiRenderer = uirenderer;
				uirenderer.skipRendering = this.parentUI != null || this.pixelsPerUnit < Mathf.Epsilon;
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)this.m_RootVisualElement.panel;
				bool flag2 = baseRuntimePanel == null;
				if (!flag2)
				{
					Debug.Assert(baseRuntimePanel.drawsInCameras);
					Bounds localBounds3D = this.rootVisualElement.localBounds3D;
					VisualElement.TransformAlignedBounds(this.rootVisualElement.worldTransformRef, ref localBounds3D);
					uirenderer.localBounds = this.SanitizeRendererBounds(localBounds3D);
					this.UpdateCutRenderChainFlag();
				}
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00052470 File Offset: 0x00050670
		private Bounds SanitizeRendererBounds(Bounds b)
		{
			bool flag = float.IsNaN(b.size.x) || float.IsNaN(b.size.y) || float.IsNaN(b.size.z);
			Bounds bounds;
			if (flag)
			{
				bounds = new Bounds(Vector3.zero, Vector3.zero);
			}
			else
			{
				bounds = b;
			}
			return bounds;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000524D4 File Offset: 0x000506D4
		private void AddOrRemoveRendererComponent()
		{
			UIRenderer uirenderer;
			base.TryGetComponent<UIRenderer>(out uirenderer);
			bool flag;
			if (this.m_PanelSettings != null)
			{
				BaseRuntimePanel panel = this.m_PanelSettings.panel;
				flag = panel != null && panel.drawsInCameras;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = uirenderer == null;
				if (flag3)
				{
					base.gameObject.AddComponent<UIRenderer>();
				}
			}
			else
			{
				UIRUtility.Destroy(uirenderer);
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0005253C File Offset: 0x0005073C
		private void UpdateCutRenderChainFlag()
		{
			bool flag = this.parentUI == null;
			bool flag2 = this.rootVisualElement.shouldCutRenderChain != flag;
			if (flag2)
			{
				this.rootVisualElement.shouldCutRenderChain = flag;
				this.rootVisualElement.MarkDirtyRepaint();
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00052588 File Offset: 0x00050788
		private void SetTransform()
		{
			Matrix4x4 matrix4x;
			this.ComputeTransform(base.transform, out matrix4x);
			this.m_RootVisualElement.style.transformOrigin = new TransformOrigin(Vector3.zero);
			this.m_RootVisualElement.style.translate = new Translate(matrix4x.GetPosition());
			this.m_RootVisualElement.style.rotate = new Rotate(matrix4x.rotation);
			this.m_RootVisualElement.style.scale = new Scale(matrix4x.lossyScale);
			this.m_RootHasWorldTransform = true;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00052634 File Offset: 0x00050834
		private void ClearTransform()
		{
			this.m_RootVisualElement.style.transformOrigin = StyleKeyword.Null;
			this.m_RootVisualElement.style.translate = StyleKeyword.Null;
			this.m_RootVisualElement.style.rotate = StyleKeyword.Null;
			this.m_RootVisualElement.style.scale = StyleKeyword.Null;
			this.m_RootHasWorldTransform = false;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x000526A5 File Offset: 0x000508A5
		private float pixelsPerUnit
		{
			get
			{
				return (this.m_RuntimePanel == null) ? 1f : this.m_RuntimePanel.pixelsPerUnit;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000526C4 File Offset: 0x000508C4
		private void ComputeTransform(Transform transform, out Matrix4x4 matrix)
		{
			float pixelsPerUnit = this.pixelsPerUnit;
			bool flag = pixelsPerUnit < Mathf.Epsilon;
			if (flag)
			{
				matrix = Matrix4x4.identity;
			}
			else
			{
				float num = 1f / pixelsPerUnit;
				Vector3 vector = Vector3.one * num;
				Quaternion quaternion = Quaternion.AngleAxis(180f, Vector3.right);
				bool flag2 = this.parentUI == null;
				if (flag2)
				{
					matrix = Matrix4x4.TRS(Vector3.zero, quaternion, vector);
				}
				else
				{
					Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, quaternion, vector);
					Matrix4x4 inverse = matrix4x.inverse;
					Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
					Matrix4x4 worldToLocalMatrix = this.parentUI.transform.worldToLocalMatrix;
					matrix = inverse * worldToLocalMatrix * localToWorldMatrix * matrix4x;
				}
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00052794 File Offset: 0x00050994
		private static void SetNoTransform(VisualElement visualElement)
		{
			visualElement.style.translate = Translate.None();
			visualElement.style.rotate = Rotate.None();
			visualElement.style.scale = Scale.None();
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000527E4 File Offset: 0x000509E4
		private void SetupFromHierarchy()
		{
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.parentUI.RemoveChild(this);
			}
			this.parentUI = this.FindUIDocumentParent();
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00052820 File Offset: 0x00050A20
		private UIDocument FindUIDocumentParent()
		{
			Transform transform = base.transform;
			Transform parent = transform.parent;
			bool flag = parent != null;
			if (flag)
			{
				UIDocument[] componentsInParent = parent.GetComponentsInParent<UIDocument>(true);
				bool flag2 = componentsInParent != null && componentsInParent.Length != 0;
				if (flag2)
				{
					return componentsInParent[0];
				}
			}
			return null;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00052874 File Offset: 0x00050A74
		internal void Reset()
		{
			bool flag = this.parentUI == null;
			if (flag)
			{
				PanelSettings previousPanelSettings = this.m_PreviousPanelSettings;
				if (previousPanelSettings != null)
				{
					previousPanelSettings.DetachUIDocument(this);
				}
				this.panelSettings = null;
			}
			this.SetupFromHierarchy();
			bool flag2 = this.parentUI != null;
			if (flag2)
			{
				this.m_PanelSettings = this.parentUI.m_PanelSettings;
				this.AddRootVisualElementToTree();
			}
			else
			{
				bool flag3 = this.m_PanelSettings != null;
				if (flag3)
				{
					this.AddRootVisualElementToTree();
				}
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000528FC File Offset: 0x00050AFC
		private void AddChildAndInsertContentToVisualTree(UIDocument child)
		{
			bool flag = this.m_ChildrenContent == null;
			if (flag)
			{
				this.m_ChildrenContent = new UIDocumentList();
			}
			else
			{
				this.m_ChildrenContent.RemoveFromListAndFromVisualTree(child);
			}
			this.m_ChildrenContent.AddToListAndToVisualTree(child, this.m_RootVisualElement, this.m_FirstChildInsertIndex);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0005294E File Offset: 0x00050B4E
		private void RemoveChild(UIDocument child)
		{
			UIDocumentList childrenContent = this.m_ChildrenContent;
			if (childrenContent != null)
			{
				childrenContent.RemoveFromListAndFromVisualTree(child);
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00052964 File Offset: 0x00050B64
		private void RecreateUI()
		{
			bool flag = this.m_RootVisualElement != null;
			if (flag)
			{
				this.RemoveFromHierarchy();
				this.m_RootVisualElement = null;
			}
			bool flag2 = this.sourceAsset != null;
			if (flag2)
			{
				this.m_RootVisualElement = this.sourceAsset.Instantiate();
				bool flag3 = this.m_RootVisualElement == null;
				if (flag3)
				{
					Debug.LogError("The UXML file set for the UIDocument could not be cloned.");
				}
			}
			bool flag4 = this.m_RootVisualElement == null;
			if (flag4)
			{
				this.m_RootVisualElement = new TemplateContainer
				{
					name = base.gameObject.name + "-container"
				};
			}
			else
			{
				this.m_RootVisualElement.name = base.gameObject.name + "-container";
			}
			this.m_RootVisualElement.pickingMode = PickingMode.Ignore;
			bool isActiveAndEnabled = base.isActiveAndEnabled;
			if (isActiveAndEnabled)
			{
				this.AddRootVisualElementToTree();
			}
			this.m_FirstChildInsertIndex = this.m_RootVisualElement.childCount;
			bool flag5 = this.m_ChildrenContent != null;
			if (flag5)
			{
				bool flag6 = this.m_ChildrenContentCopy == null;
				if (flag6)
				{
					this.m_ChildrenContentCopy = new List<UIDocument>(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				else
				{
					this.m_ChildrenContentCopy.AddRange(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				foreach (UIDocument uidocument in this.m_ChildrenContentCopy)
				{
					bool isActiveAndEnabled2 = uidocument.isActiveAndEnabled;
					if (isActiveAndEnabled2)
					{
						bool flag7 = uidocument.m_RootVisualElement == null;
						if (flag7)
						{
							uidocument.RecreateUI();
						}
						else
						{
							this.AddChildAndInsertContentToVisualTree(uidocument);
						}
					}
				}
				this.m_ChildrenContentCopy.Clear();
			}
			this.SetupRootClassList();
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00052B40 File Offset: 0x00050D40
		private void SetupRootClassList()
		{
			bool flag = this.m_RootVisualElement == null;
			if (!flag)
			{
				bool flag2 = this.panelSettings == null || this.panelSettings.renderMode != PanelRenderMode.WorldSpace;
				if (flag2)
				{
					this.m_RootVisualElement.EnableInClassList("unity-ui-document__root", this.parentUI == null);
				}
				else
				{
					this.UpdateWorldSpaceSize();
				}
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00052BB0 File Offset: 0x00050DB0
		private void UpdateWorldSpaceSize()
		{
			bool flag = this.m_RootVisualElement == null;
			if (!flag)
			{
				bool flag2 = this.m_WorldSpaceSizeMode == UIDocument.WorldSpaceSizeMode.Fixed;
				if (flag2)
				{
					this.m_RootVisualElement.style.position = Position.Absolute;
					this.m_RootVisualElement.style.width = this.m_WorldSpaceWidth;
					this.m_RootVisualElement.style.height = this.m_WorldSpaceHeight;
				}
				else
				{
					this.m_RootVisualElement.style.position = Position.Absolute;
					this.m_RootVisualElement.style.width = StyleKeyword.Null;
					this.m_RootVisualElement.style.height = StyleKeyword.Null;
				}
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00052C78 File Offset: 0x00050E78
		private void AddRootVisualElementToTree()
		{
			bool flag = !base.enabled;
			if (!flag)
			{
				bool flag2 = this.parentUI != null;
				if (flag2)
				{
					this.parentUI.AddChildAndInsertContentToVisualTree(this);
				}
				else
				{
					bool flag3 = this.m_PanelSettings != null;
					if (flag3)
					{
						this.m_PanelSettings.AttachAndInsertUIDocumentToVisualTree(this);
					}
				}
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00052CD8 File Offset: 0x00050ED8
		private void RemoveFromHierarchy()
		{
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.parentUI.RemoveChild(this);
			}
			else
			{
				bool flag2 = this.m_PanelSettings != null;
				if (flag2)
				{
					this.m_PanelSettings.DetachUIDocument(this);
				}
			}
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00052D28 File Offset: 0x00050F28
		private void OnDisable()
		{
			bool flag = this.m_RootVisualElement != null;
			if (flag)
			{
				this.RemoveFromHierarchy();
				this.m_RootVisualElement = null;
			}
			UIRenderer uirenderer;
			bool flag2 = base.TryGetComponent<UIRenderer>(out uirenderer);
			if (flag2)
			{
				uirenderer.enabled = false;
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00052D68 File Offset: 0x00050F68
		private void OnTransformChildrenChanged()
		{
			bool flag = this.m_ChildrenContent != null;
			if (flag)
			{
				bool flag2 = this.m_ChildrenContentCopy == null;
				if (flag2)
				{
					this.m_ChildrenContentCopy = new List<UIDocument>(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				else
				{
					this.m_ChildrenContentCopy.AddRange(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				foreach (UIDocument uidocument in this.m_ChildrenContentCopy)
				{
					uidocument.ReactToHierarchyChanged();
				}
				this.m_ChildrenContentCopy.Clear();
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00052E20 File Offset: 0x00051020
		private void OnTransformParentChanged()
		{
			this.ReactToHierarchyChanged();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00052E2C File Offset: 0x0005102C
		internal void ReactToHierarchyChanged()
		{
			this.SetupFromHierarchy();
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.panelSettings = this.parentUI.m_PanelSettings;
			}
			VisualElement rootVisualElement = this.m_RootVisualElement;
			if (rootVisualElement != null)
			{
				rootVisualElement.RemoveFromHierarchy();
			}
			this.AddRootVisualElementToTree();
			this.SetupRootClassList();
		}

		// Token: 0x04000B23 RID: 2851
		internal const string k_RootStyleClassName = "unity-ui-document__root";

		// Token: 0x04000B24 RID: 2852
		internal const string k_VisualElementNameSuffix = "-container";

		// Token: 0x04000B25 RID: 2853
		private const int k_DefaultSortingOrder = 0;

		// Token: 0x04000B26 RID: 2854
		private static int s_CurrentUIDocumentCounter;

		// Token: 0x04000B27 RID: 2855
		internal readonly int m_UIDocumentCreationIndex;

		// Token: 0x04000B28 RID: 2856
		[SerializeField]
		private PanelSettings m_PanelSettings;

		// Token: 0x04000B29 RID: 2857
		private PanelSettings m_PreviousPanelSettings = null;

		// Token: 0x04000B2A RID: 2858
		[SerializeField]
		private UIDocument m_ParentUI;

		// Token: 0x04000B2B RID: 2859
		private UIDocumentList m_ChildrenContent = null;

		// Token: 0x04000B2C RID: 2860
		private List<UIDocument> m_ChildrenContentCopy = null;

		// Token: 0x04000B2D RID: 2861
		[SerializeField]
		private VisualTreeAsset sourceAsset;

		// Token: 0x04000B2E RID: 2862
		private VisualElement m_RootVisualElement;

		// Token: 0x04000B2F RID: 2863
		private RuntimePanel m_RuntimePanel;

		// Token: 0x04000B30 RID: 2864
		private int m_FirstChildInsertIndex;

		// Token: 0x04000B31 RID: 2865
		[SerializeField]
		private float m_SortingOrder = 0f;

		// Token: 0x04000B32 RID: 2866
		[SerializeField]
		private UIDocument.WorldSpaceSizeMode m_WorldSpaceSizeMode = UIDocument.WorldSpaceSizeMode.Fixed;

		// Token: 0x04000B33 RID: 2867
		[SerializeField]
		private float m_WorldSpaceWidth = 1920f;

		// Token: 0x04000B34 RID: 2868
		[SerializeField]
		private float m_WorldSpaceHeight = 1080f;

		// Token: 0x04000B35 RID: 2869
		private bool m_RootHasWorldTransform;

		// Token: 0x020002CC RID: 716
		internal enum WorldSpaceSizeMode
		{
			// Token: 0x04000B37 RID: 2871
			Dynamic,
			// Token: 0x04000B38 RID: 2872
			Fixed
		}
	}
}
