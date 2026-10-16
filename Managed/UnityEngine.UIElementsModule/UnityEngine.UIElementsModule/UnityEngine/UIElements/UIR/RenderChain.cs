using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200065F RID: 1631
	internal class RenderChain : IDisposable
	{
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000B1690 File Offset: 0x000AF890
		internal RenderChainCommand firstCommand
		{
			get
			{
				return this.m_FirstCommand;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000B16A8 File Offset: 0x000AF8A8
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x000B16B0 File Offset: 0x000AF8B0
		public OpacityIdAccelerator opacityIdAccelerator { get; private set; }

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000B16B9 File Offset: 0x000AF8B9
		public RenderChain(BaseVisualElementPanel panel)
			: this(panel, new UIRenderDevice(panel.panelRenderer.vertexBudget, 0U), panel.atlas, new VectorImageManager(panel.atlas))
		{
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000B16E8 File Offset: 0x000AF8E8
		protected RenderChain(BaseVisualElementPanel panel, UIRenderDevice device, AtlasBase atlas, VectorImageManager vectorImageManager)
		{
			this.m_DirtyTracker.heads = new List<VisualElement>(8);
			this.m_DirtyTracker.tails = new List<VisualElement>(8);
			this.m_DirtyTracker.minDepths = new int[5];
			this.m_DirtyTracker.maxDepths = new int[5];
			this.m_DirtyTracker.Reset();
			this.panel = panel;
			this.device = device;
			this.atlas = atlas;
			this.vectorImageManager = vectorImageManager;
			this.tempMeshAllocator = new TempMeshAllocatorImpl();
			this.jobManager = new JobManager();
			this.opacityIdAccelerator = new OpacityIdAccelerator();
			this.meshGenerationNodeManager = new MeshGenerationNodeManager(this.entryRecorder);
			this.m_VisualChangesProcessor = new RenderChain.VisualChangesProcessor(this);
			ColorSpace activeColorSpace = QualitySettings.activeColorSpace;
			bool flag = panel.contextType == ContextType.Player;
			if (flag)
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				device.drawsInCameras = (this.drawInCameras = baseRuntimePanel.drawsInCameras);
				bool drawInCameras = this.drawInCameras;
				if (drawInCameras)
				{
					this.m_DefaultMat = Shaders.runtimeWorldMaterial;
				}
				else
				{
					this.m_DefaultMat = Shaders.runtimeMaterial;
					bool flag2 = activeColorSpace == ColorSpace.Linear;
					if (flag2)
					{
						this.forceGammaRendering = panel.panelRenderer.forceGammaRendering;
					}
				}
			}
			else
			{
				bool flag3 = activeColorSpace == ColorSpace.Linear;
				if (flag3)
				{
					this.forceGammaRendering = true;
				}
				this.m_DefaultMat = Shaders.editorMaterial;
			}
			Shaders.Acquire();
			this.shaderInfoAllocator = new UIRVEShaderInfoAllocator(this.forceGammaRendering ? ColorSpace.Gamma : activeColorSpace);
			device.isFlat = (this.isFlat = panel.isFlat);
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000B1964 File Offset: 0x000AFB64
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x000B196C File Offset: 0x000AFB6C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000B1975 File Offset: 0x000AFB75
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000B1988 File Offset: 0x000AFB88
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Shaders.Release();
					RenderChainCommand firstCommand = this.m_FirstCommand;
					for (VisualElement visualElement = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); visualElement != null; visualElement = visualElement.renderChainData.next)
					{
						this.ResetTextures(visualElement);
					}
					this.tempMeshAllocator.Dispose();
					this.tempMeshAllocator = null;
					this.jobManager.Dispose();
					this.jobManager = null;
					VectorImageManager vectorImageManager = this.vectorImageManager;
					if (vectorImageManager != null)
					{
						vectorImageManager.Dispose();
					}
					this.vectorImageManager = null;
					this.shaderInfoAllocator.Dispose();
					this.shaderInfoAllocator = null;
					UIRenderDevice device = this.device;
					if (device != null)
					{
						device.Dispose();
					}
					this.device = null;
					OpacityIdAccelerator opacityIdAccelerator = this.opacityIdAccelerator;
					if (opacityIdAccelerator != null)
					{
						opacityIdAccelerator.Dispose();
					}
					this.opacityIdAccelerator = null;
					RenderChain.VisualChangesProcessor visualChangesProcessor = this.m_VisualChangesProcessor;
					if (visualChangesProcessor != null)
					{
						visualChangesProcessor.Dispose();
					}
					this.m_VisualChangesProcessor = null;
					MeshGenerationDeferrer meshGenerationDeferrer = this.m_MeshGenerationDeferrer;
					if (meshGenerationDeferrer != null)
					{
						meshGenerationDeferrer.Dispose();
					}
					this.m_MeshGenerationDeferrer = null;
					this.meshGenerationNodeManager.Dispose();
					this.meshGenerationNodeManager = null;
					this.atlas = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000B1ACE File Offset: 0x000AFCCE
		internal ChainBuilderStats stats
		{
			get
			{
				return this.m_Stats;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000B1AD6 File Offset: 0x000AFCD6
		internal ref ChainBuilderStats statsByRef
		{
			get
			{
				return ref this.m_Stats;
			}
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
		public void ProcessChanges()
		{
			this.m_Stats = default(ChainBuilderStats);
			this.m_Stats.elementsAdded = this.m_Stats.elementsAdded + this.m_StatsElementsAdded;
			this.m_Stats.elementsRemoved = this.m_Stats.elementsRemoved + this.m_StatsElementsRemoved;
			this.m_StatsElementsAdded = (this.m_StatsElementsRemoved = 0U);
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			int num = 0;
			RenderDataDirtyTypes renderDataDirtyTypes = RenderDataDirtyTypes.Clipping | RenderDataDirtyTypes.ClippingHierarchy;
			RenderDataDirtyTypes renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int i = this.m_DirtyTracker.minDepths[num]; i <= this.m_DirtyTracker.maxDepths[num]; i++)
			{
				VisualElement visualElement = this.m_DirtyTracker.heads[i];
				while (visualElement != null)
				{
					VisualElement nextDirty = visualElement.renderChainData.nextDirty;
					bool flag = (visualElement.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag)
					{
						bool flag2 = visualElement.renderChainData.isInChain && visualElement.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag2)
						{
							RenderEvents.ProcessOnClippingChanged(this, visualElement, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement, renderDataDirtyTypes2);
					}
					visualElement = nextDirty;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 1;
			renderDataDirtyTypes = RenderDataDirtyTypes.Opacity | RenderDataDirtyTypes.OpacityHierarchy;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int j = this.m_DirtyTracker.minDepths[num]; j <= this.m_DirtyTracker.maxDepths[num]; j++)
			{
				VisualElement visualElement2 = this.m_DirtyTracker.heads[j];
				while (visualElement2 != null)
				{
					VisualElement nextDirty2 = visualElement2.renderChainData.nextDirty;
					bool flag3 = (visualElement2.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag3)
					{
						bool flag4 = visualElement2.renderChainData.isInChain && visualElement2.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag4)
						{
							RenderEvents.ProcessOnOpacityChanged(this, visualElement2, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement2, renderDataDirtyTypes2);
					}
					visualElement2 = nextDirty2;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 2;
			renderDataDirtyTypes = RenderDataDirtyTypes.Color;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int k = this.m_DirtyTracker.minDepths[num]; k <= this.m_DirtyTracker.maxDepths[num]; k++)
			{
				VisualElement visualElement3 = this.m_DirtyTracker.heads[k];
				while (visualElement3 != null)
				{
					VisualElement nextDirty3 = visualElement3.renderChainData.nextDirty;
					bool flag5 = (visualElement3.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag5)
					{
						bool flag6 = visualElement3.renderChainData.isInChain && visualElement3.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag6)
						{
							RenderEvents.ProcessOnColorChanged(this, visualElement3, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement3, renderDataDirtyTypes2);
					}
					visualElement3 = nextDirty3;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 3;
			renderDataDirtyTypes = RenderDataDirtyTypes.Transform | RenderDataDirtyTypes.ClipRectSize;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int l = this.m_DirtyTracker.minDepths[num]; l <= this.m_DirtyTracker.maxDepths[num]; l++)
			{
				VisualElement visualElement4 = this.m_DirtyTracker.heads[l];
				while (visualElement4 != null)
				{
					VisualElement nextDirty4 = visualElement4.renderChainData.nextDirty;
					bool flag7 = (visualElement4.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag7)
					{
						bool flag8 = visualElement4.renderChainData.isInChain && visualElement4.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag8)
						{
							RenderEvents.ProcessOnTransformOrSizeChanged(this, visualElement4, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement4, renderDataDirtyTypes2);
					}
					visualElement4 = nextDirty4;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.jobManager.CompleteNudgeJobs();
			this.m_BlockDirtyRegistration = true;
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 4;
			renderDataDirtyTypes = RenderDataDirtyTypes.AllVisuals;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int m = this.m_DirtyTracker.minDepths[num]; m <= this.m_DirtyTracker.maxDepths[num]; m++)
			{
				VisualElement visualElement5 = this.m_DirtyTracker.heads[m];
				while (visualElement5 != null)
				{
					VisualElement nextDirty5 = visualElement5.renderChainData.nextDirty;
					bool flag9 = (visualElement5.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag9)
					{
						bool flag10 = visualElement5.renderChainData.isInChain && visualElement5.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag10)
						{
							this.m_VisualChangesProcessor.ProcessOnVisualsChanged(visualElement5, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement5, renderDataDirtyTypes2);
					}
					visualElement5 = nextDirty5;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_MeshGenerationDeferrer.ProcessDeferredWork(this.m_VisualChangesProcessor.meshGenerationContext);
			this.m_VisualChangesProcessor.ScheduleMeshGenerationJobs();
			this.m_MeshGenerationDeferrer.ProcessDeferredWork(this.m_VisualChangesProcessor.meshGenerationContext);
			this.m_VisualChangesProcessor.ConvertEntriesToCommands(ref this.m_Stats);
			this.jobManager.CompleteConvertMeshJobs();
			this.jobManager.CompleteCopyMeshJobs();
			this.opacityIdAccelerator.CompleteJobs();
			this.m_BlockDirtyRegistration = false;
			this.meshGenerationNodeManager.ResetAll();
			this.tempMeshAllocator.Clear();
			this.meshWriteDataPool.ReturnAll();
			this.entryPool.ReturnAll();
			this.m_DirtyTracker.Reset();
			AtlasBase atlas = this.atlas;
			if (atlas != null)
			{
				atlas.InvokeUpdateDynamicTextures(this.panel);
			}
			VectorImageManager vectorImageManager = this.vectorImageManager;
			if (vectorImageManager != null)
			{
				vectorImageManager.Commit();
			}
			this.shaderInfoAllocator.IssuePendingStorageChanges();
			UIRenderDevice device = this.device;
			if (device != null)
			{
				device.OnFrameRenderingBegin();
			}
			bool drawInCameras = this.drawInCameras;
			if (drawInCameras)
			{
				this.SerializeCommandsForCameras();
			}
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000B21B4 File Offset: 0x000B03B4
		private void SerializeCommandsForCameras()
		{
			using (RenderChain.k_MarkerSerialize.Auto())
			{
				bool flag = this.m_FirstCommand == null;
				if (!flag)
				{
					Exception ex = null;
					UIRenderDevice device = this.device;
					RenderChainCommand firstCommand = this.m_FirstCommand;
					Material defaultMat = this.m_DefaultMat;
					Material defaultMat2 = this.m_DefaultMat;
					VectorImageManager vectorImageManager = this.vectorImageManager;
					device.EvaluateChain(firstCommand, defaultMat, defaultMat2, (vectorImageManager != null) ? vectorImageManager.atlas : null, this.shaderInfoAllocator.atlas, this.panel.scaledPixelsPerPoint, ref ex);
					UIRenderDevice device2 = this.device;
					List<CommandList> list = ((device2 != null) ? device2.currentFrameCommandLists : null);
					bool flag2 = this.drawInCameras && list != null;
					if (flag2)
					{
						for (int i = 0; i < this.device.currentFrameCommandListCount; i++)
						{
							CommandList commandList = list[i];
							VisualElement owner = commandList.m_Owner;
							UIRenderer uirenderer = ((owner != null) ? owner.uiRenderer : null);
							bool flag3 = uirenderer != null;
							if (flag3)
							{
								List<CommandList>[] commandLists = this.device.commandLists;
								uirenderer.commandLists = commandLists;
								int num = (int)(this.device.frameIndex % (uint)commandLists.Length);
								uirenderer.SetNativeData(num, i, this.m_DefaultMat);
							}
						}
					}
					Debug.Assert(ex == null);
				}
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000B2324 File Offset: 0x000B0524
		public void Render()
		{
			Debug.Assert(!this.drawInCameras);
			PanelClearSettings clearSettings = this.panel.clearSettings;
			bool flag = clearSettings.clearColor || clearSettings.clearDepthStencil;
			if (flag)
			{
				Color color = clearSettings.color;
				color = color.RGBMultiplied(color.a);
				GL.Clear(clearSettings.clearDepthStencil, clearSettings.clearColor, color, 0.99f);
			}
			Exception ex = null;
			bool flag2 = this.m_FirstCommand != null;
			if (flag2)
			{
				bool invertCulling = GL.invertCulling;
				bool flag3 = invertCulling;
				if (flag3)
				{
					GL.invertCulling = false;
				}
				Rect layout = this.panel.visualTree.layout;
				bool forceGammaRendering = this.forceGammaRendering;
				if (forceGammaRendering)
				{
					this.m_DefaultMat.EnableKeyword(Shaders.k_ForceGammaKeyword);
				}
				else
				{
					this.m_DefaultMat.DisableKeyword(Shaders.k_ForceGammaKeyword);
				}
				this.m_DefaultMat.SetPass(0);
				Matrix4x4 matrix4x = ProjectionUtils.Ortho(layout.xMin, layout.xMax, layout.yMax, layout.yMin, -0.001f, 1.001f);
				GL.LoadProjectionMatrix(matrix4x);
				GL.modelview = Matrix4x4.identity;
				UIRenderDevice device = this.device;
				RenderChainCommand firstCommand = this.m_FirstCommand;
				Material defaultMat = this.m_DefaultMat;
				Material defaultMat2 = this.m_DefaultMat;
				VectorImageManager vectorImageManager = this.vectorImageManager;
				device.EvaluateChain(firstCommand, defaultMat, defaultMat2, (vectorImageManager != null) ? vectorImageManager.atlas : null, this.shaderInfoAllocator.atlas, this.panel.scaledPixelsPerPoint, ref ex);
				bool flag4 = invertCulling;
				if (flag4)
				{
					GL.invertCulling = true;
				}
			}
			bool flag5 = ex != null;
			if (!flag5)
			{
				bool drawStats = this.drawStats;
				if (drawStats)
				{
					this.DrawStats();
				}
				return;
			}
			bool flag6 = GUIUtility.IsExitGUIException(ex);
			if (flag6)
			{
				throw ex;
			}
			throw new ImmediateModeException(ex);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000B24D8 File Offset: 0x000B06D8
		public void UIEOnChildAdded(VisualElement ve)
		{
			VisualElement parent = ve.hierarchy.parent;
			int num = ((parent != null) ? parent.hierarchy.IndexOf(ve) : 0);
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be added to an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			bool flag = parent != null && !parent.renderChainData.isInChain;
			if (!flag)
			{
				uint num2 = RenderEvents.DepthFirstOnChildAdded(this, parent, ve, num, true);
				Debug.Assert(ve.renderChainData.isInChain);
				Debug.Assert(ve.panel == this.panel);
				this.UIEOnClippingChanged(ve, true);
				this.UIEOnOpacityChanged(ve, false);
				this.UIEOnVisualsChanged(ve, true);
				this.m_StatsElementsAdded += num2;
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000B2598 File Offset: 0x000B0798
		public void UIEOnChildrenReordered(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be moved under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				RenderEvents.DepthFirstOnChildRemoving(this, ve.hierarchy[i]);
			}
			for (int j = 0; j < childCount; j++)
			{
				RenderEvents.DepthFirstOnChildAdded(this, ve, ve.hierarchy[j], j, false);
			}
			this.UIEOnClippingChanged(ve, true);
			this.UIEOnOpacityChanged(ve, true);
			this.UIEOnVisualsChanged(ve, true);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000B2640 File Offset: 0x000B0840
		public void UIEOnChildRemoving(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be removed from an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			this.m_StatsElementsRemoved += RenderEvents.DepthFirstOnChildRemoving(this, ve);
			Debug.Assert(!ve.renderChainData.isInChain);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000B268C File Offset: 0x000B088C
		public void UIEOnRenderHintsChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("Render Hints cannot change under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				bool flag = (ve.renderHints & RenderHints.DirtyAll) == RenderHints.DirtyDynamicColor;
				bool flag2 = flag;
				if (flag2)
				{
					this.UIEOnVisualsChanged(ve, false);
				}
				else
				{
					this.UIEOnChildRemoving(ve);
					this.UIEOnChildAdded(ve);
				}
				ve.MarkRenderHintsClean();
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000B2700 File Offset: 0x000B0900
		public void UIEOnClippingChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change clipping state under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Clipping | (hierarchical ? RenderDataDirtyTypes.ClippingHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Clipping);
			}
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000B274C File Offset: 0x000B094C
		public void UIEOnOpacityChanged(VisualElement ve, bool hierarchical = false)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change opacity under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Opacity | (hierarchical ? RenderDataDirtyTypes.OpacityHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Opacity);
			}
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B27A0 File Offset: 0x000B09A0
		public void UIEOnColorChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change background color under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Color, RenderDataDirtyTypeClasses.Color);
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000B27E8 File Offset: 0x000B09E8
		public void UIEOnTransformOrSizeChanged(VisualElement ve, bool transformChanged, bool clipRectSizeChanged)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change size or transform under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				RenderDataDirtyTypes renderDataDirtyTypes = (transformChanged ? RenderDataDirtyTypes.Transform : RenderDataDirtyTypes.None) | (clipRectSizeChanged ? RenderDataDirtyTypes.ClipRectSize : RenderDataDirtyTypes.None);
				this.m_DirtyTracker.RegisterDirty(ve, renderDataDirtyTypes, RenderDataDirtyTypeClasses.TransformSize);
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B283C File Offset: 0x000B0A3C
		public void UIEOnVisualsChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot be marked for dirty repaint under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Visuals | (hierarchical ? RenderDataDirtyTypes.VisualsHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Visuals);
			}
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B288C File Offset: 0x000B0A8C
		public void UIEOnOpacityIdChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot for opacity id change under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.VisualsOpacityId, RenderDataDirtyTypeClasses.Visuals);
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000B28D0 File Offset: 0x000B0AD0
		public void UIEOnDisableRenderingChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change their display style during generateVisualContent callback execution nor during visual tree rendering");
				}
				CommandManipulator.DisableElementRendering(this, ve, ve.disableRendering);
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000B2912 File Offset: 0x000B0B12
		// (set) Token: 0x06002FB8 RID: 12216 RVA: 0x000B291A File Offset: 0x000B0B1A
		internal BaseVisualElementPanel panel { get; private set; }

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000B2923 File Offset: 0x000B0B23
		// (set) Token: 0x06002FBA RID: 12218 RVA: 0x000B292B File Offset: 0x000B0B2B
		internal UIRenderDevice device { get; private set; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000B2934 File Offset: 0x000B0B34
		public BaseElementBuilder elementBuilder
		{
			get
			{
				return this.m_VisualChangesProcessor.elementBuilder;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000B2941 File Offset: 0x000B0B41
		// (set) Token: 0x06002FBD RID: 12221 RVA: 0x000B2949 File Offset: 0x000B0B49
		internal AtlasBase atlas { get; private set; }

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000B2952 File Offset: 0x000B0B52
		// (set) Token: 0x06002FBF RID: 12223 RVA: 0x000B295A File Offset: 0x000B0B5A
		internal VectorImageManager vectorImageManager { get; private set; }

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000B2963 File Offset: 0x000B0B63
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x000B296B File Offset: 0x000B0B6B
		internal TempMeshAllocatorImpl tempMeshAllocator { get; private set; }

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000B2974 File Offset: 0x000B0B74
		internal MeshWriteDataPool meshWriteDataPool { get; } = new MeshWriteDataPool();

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000B297C File Offset: 0x000B0B7C
		internal EntryPool entryPool
		{
			get
			{
				return RenderChain.s_SharedEntryPool;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000B2983 File Offset: 0x000B0B83
		public MeshGenerationDeferrer meshGenerationDeferrer
		{
			get
			{
				return this.m_MeshGenerationDeferrer;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000B298B File Offset: 0x000B0B8B
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x000B2993 File Offset: 0x000B0B93
		public MeshGenerationNodeManager meshGenerationNodeManager { get; private set; }

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000B299C File Offset: 0x000B0B9C
		// (set) Token: 0x06002FC8 RID: 12232 RVA: 0x000B29A4 File Offset: 0x000B0BA4
		internal JobManager jobManager { get; private set; }

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000B29AD File Offset: 0x000B0BAD
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x000B29B5 File Offset: 0x000B0BB5
		internal bool drawStats { get; set; }

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000B29BE File Offset: 0x000B0BBE
		internal bool drawInCameras { get; }

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000B29C6 File Offset: 0x000B0BC6
		internal bool isFlat { get; }

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000B29CE File Offset: 0x000B0BCE
		public bool forceGammaRendering { get; }

		// Token: 0x06002FCE RID: 12238 RVA: 0x000B29D6 File Offset: 0x000B0BD6
		internal void EnsureFitsDepth(int depth)
		{
			this.m_DirtyTracker.EnsureFits(depth);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000B29E8 File Offset: 0x000B0BE8
		internal void ChildWillBeRemoved(VisualElement ve)
		{
			bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
			if (flag)
			{
				this.m_DirtyTracker.ClearDirty(ve, ~ve.renderChainData.dirtiedValues);
			}
			Debug.Assert(ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None);
			Debug.Assert(ve.renderChainData.prevDirty == null);
			Debug.Assert(ve.renderChainData.nextDirty == null);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000B2A60 File Offset: 0x000B0C60
		internal RenderChainCommand AllocCommand()
		{
			return this.m_CommandPool.Get();
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000B2A7F File Offset: 0x000B0C7F
		internal void FreeCommand(RenderChainCommand cmd)
		{
			cmd.Reset();
			this.m_CommandPool.Return(cmd);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000B2A98 File Offset: 0x000B0C98
		internal void OnRenderCommandAdded(RenderChainCommand command)
		{
			bool flag = command.prev == null;
			if (flag)
			{
				this.m_FirstCommand = command;
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000B2ABC File Offset: 0x000B0CBC
		internal void OnRenderCommandsRemoved(RenderChainCommand firstCommand, RenderChainCommand lastCommand)
		{
			bool flag = firstCommand.prev == null;
			if (flag)
			{
				this.m_FirstCommand = lastCommand.next;
			}
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000B2AE4 File Offset: 0x000B0CE4
		internal void RepaintTexturedElements()
		{
			RenderChainCommand firstCommand = this.m_FirstCommand;
			for (VisualElement visualElement = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); visualElement != null; visualElement = visualElement.renderChainData.next)
			{
				bool flag = visualElement.renderChainData.textures != null;
				if (flag)
				{
					this.UIEOnVisualsChanged(visualElement, false);
				}
			}
			this.UIEOnOpacityChanged(this.panel.visualTree, false);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000B2B50 File Offset: 0x000B0D50
		public ExtraRenderChainVEData GetOrAddExtraData(VisualElement ve)
		{
			ExtraRenderChainVEData extraRenderChainVEData;
			bool flag = !this.m_ExtraData.TryGetValue(ve, out extraRenderChainVEData);
			if (flag)
			{
				extraRenderChainVEData = this.m_ExtraDataPool.Get();
				this.m_ExtraData.Add(ve, extraRenderChainVEData);
				ve.renderChainData.flags = ve.renderChainData.flags | RenderDataFlags.HasExtraData;
			}
			return extraRenderChainVEData;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000B2BA4 File Offset: 0x000B0DA4
		public void FreeExtraData(VisualElement ve)
		{
			Debug.Assert(ve.renderChainData.hasExtraData);
			Debug.Assert(!ve.renderChainData.hasExtraMeshes);
			ExtraRenderChainVEData extraRenderChainVEData;
			this.m_ExtraData.Remove(ve, out extraRenderChainVEData);
			this.m_ExtraDataPool.Return(extraRenderChainVEData);
			ve.renderChainData.flags = ve.renderChainData.flags & ~RenderDataFlags.HasExtraData;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000B2C04 File Offset: 0x000B0E04
		public void InsertExtraMesh(VisualElement ve, MeshHandle mesh)
		{
			ExtraRenderChainVEData orAddExtraData = this.GetOrAddExtraData(ve);
			BasicNode<MeshHandle> basicNode = this.m_MeshHandleNodePool.Get();
			basicNode.data = mesh;
			basicNode.InsertFirst(ref orAddExtraData.extraMesh);
			ve.renderChainData.flags = ve.renderChainData.flags | RenderDataFlags.HasExtraMeshes;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000B2C4C File Offset: 0x000B0E4C
		public void FreeExtraMeshes(VisualElement ve)
		{
			bool flag = !ve.renderChainData.hasExtraMeshes;
			if (!flag)
			{
				ExtraRenderChainVEData extraRenderChainVEData = this.m_ExtraData[ve];
				BasicNode<MeshHandle> basicNode = extraRenderChainVEData.extraMesh;
				extraRenderChainVEData.extraMesh = null;
				while (basicNode != null)
				{
					this.device.Free(basicNode.data);
					BasicNode<MeshHandle> next = basicNode.next;
					basicNode.data = null;
					basicNode.next = null;
					this.m_MeshHandleNodePool.Return(basicNode);
					basicNode = next;
				}
				ve.renderChainData.flags = ve.renderChainData.flags & ~RenderDataFlags.HasExtraMeshes;
			}
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000B2CE0 File Offset: 0x000B0EE0
		public void InsertTexture(VisualElement ve, Texture src, TextureId id, bool isAtlas)
		{
			BasicNode<TextureEntry> basicNode = this.m_TexturePool.Get();
			basicNode.data.source = src;
			basicNode.data.actual = id;
			basicNode.data.replaced = isAtlas;
			basicNode.InsertFirst(ref ve.renderChainData.textures);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000B2D34 File Offset: 0x000B0F34
		public void ResetTextures(VisualElement ve)
		{
			AtlasBase atlas = this.atlas;
			TextureRegistry textureRegistry = this.m_TextureRegistry;
			BasicNodePool<TextureEntry> texturePool = this.m_TexturePool;
			BasicNode<TextureEntry> basicNode = ve.renderChainData.textures;
			ve.renderChainData.textures = null;
			while (basicNode != null)
			{
				BasicNode<TextureEntry> next = basicNode.next;
				bool replaced = basicNode.data.replaced;
				if (replaced)
				{
					atlas.ReturnAtlas(ve, basicNode.data.source as Texture2D, basicNode.data.actual);
				}
				else
				{
					textureRegistry.Release(basicNode.data.actual);
				}
				texturePool.Return(basicNode);
				basicNode = next;
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000B2DD8 File Offset: 0x000B0FD8
		private void DrawStats()
		{
			bool flag = this.device != null;
			float num = 12f;
			Rect rect = new Rect(30f, 60f, 1000f, 100f);
			GUI.Box(new Rect(20f, 40f, 200f, (float)(flag ? 380 : 256)), "UI Toolkit Draw Stats");
			GUI.Label(rect, "Elements added\t: " + this.m_Stats.elementsAdded.ToString());
			rect.y += num;
			GUI.Label(rect, "Elements removed\t: " + this.m_Stats.elementsRemoved.ToString());
			rect.y += num;
			GUI.Label(rect, "Mesh allocs allocated\t: " + this.m_Stats.newMeshAllocations.ToString());
			rect.y += num;
			GUI.Label(rect, "Mesh allocs updated\t: " + this.m_Stats.updatedMeshAllocations.ToString());
			rect.y += num;
			GUI.Label(rect, "Clip update roots\t: " + this.m_Stats.recursiveClipUpdates.ToString());
			rect.y += num;
			GUI.Label(rect, "Clip update total\t: " + this.m_Stats.recursiveClipUpdatesExpanded.ToString());
			rect.y += num;
			GUI.Label(rect, "Opacity update roots\t: " + this.m_Stats.recursiveOpacityUpdates.ToString());
			rect.y += num;
			GUI.Label(rect, "Opacity update total\t: " + this.m_Stats.recursiveOpacityUpdatesExpanded.ToString());
			rect.y += num;
			GUI.Label(rect, "Opacity ID update\t: " + this.m_Stats.opacityIdUpdates.ToString());
			rect.y += num;
			GUI.Label(rect, "Xform update roots\t: " + this.m_Stats.recursiveTransformUpdates.ToString());
			rect.y += num;
			GUI.Label(rect, "Xform update total\t: " + this.m_Stats.recursiveTransformUpdatesExpanded.ToString());
			rect.y += num;
			GUI.Label(rect, "Xformed by bone\t: " + this.m_Stats.boneTransformed.ToString());
			rect.y += num;
			GUI.Label(rect, "Xformed by skipping\t: " + this.m_Stats.skipTransformed.ToString());
			rect.y += num;
			GUI.Label(rect, "Xformed by nudging\t: " + this.m_Stats.nudgeTransformed.ToString());
			rect.y += num;
			GUI.Label(rect, "Xformed by repaint\t: " + this.m_Stats.visualUpdateTransformed.ToString());
			rect.y += num;
			GUI.Label(rect, "Visual update roots\t: " + this.m_Stats.recursiveVisualUpdates.ToString());
			rect.y += num;
			GUI.Label(rect, "Visual update total\t: " + this.m_Stats.recursiveVisualUpdatesExpanded.ToString());
			rect.y += num;
			GUI.Label(rect, "Visual update flats\t: " + this.m_Stats.nonRecursiveVisualUpdates.ToString());
			rect.y += num;
			GUI.Label(rect, "Dirty processed\t: " + this.m_Stats.dirtyProcessed.ToString());
			rect.y += num;
			GUI.Label(rect, "Group-xform updates\t: " + this.m_Stats.groupTransformElementsChanged.ToString());
			rect.y += num;
			bool flag2 = !flag;
			if (!flag2)
			{
				rect.y += num;
				UIRenderDevice.DrawStatistics drawStatistics = this.device.GatherDrawStatistics();
				GUI.Label(rect, "Frame index\t: " + drawStatistics.currentFrameIndex.ToString());
				rect.y += num;
				GUI.Label(rect, "Command count\t: " + drawStatistics.commandCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Skip cmd counts\t: " + drawStatistics.skippedCommandCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Draw commands\t: " + drawStatistics.drawCommandCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Disable commands\t: " + drawStatistics.disableCommandCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Draw ranges\t: " + drawStatistics.drawRangeCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Draw range calls\t: " + drawStatistics.drawRangeCallCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Material sets\t: " + drawStatistics.materialSetCount.ToString());
				rect.y += num;
				GUI.Label(rect, "Stencil changes\t: " + drawStatistics.stencilRefChanges.ToString());
				rect.y += num;
				GUI.Label(rect, "Immediate draws\t: " + drawStatistics.immediateDraws.ToString());
				rect.y += num;
				GUI.Label(rect, "Total triangles\t: " + (drawStatistics.totalIndices / 3U).ToString());
				rect.y += num;
			}
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000B3434 File Offset: 0x000B1634
		private static VisualElement GetFirstElementInPanel(VisualElement ve)
		{
			for (;;)
			{
				bool flag;
				if (ve != null)
				{
					VisualElement prev = ve.renderChainData.prev;
					flag = prev != null && prev.renderChainData.isInChain;
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					break;
				}
				ve = ve.renderChainData.prev;
			}
			return ve;
		}

		// Token: 0x040016EF RID: 5871
		private RenderChainCommand m_FirstCommand;

		// Token: 0x040016F0 RID: 5872
		private RenderChain.DepthOrderedDirtyTracking m_DirtyTracker;

		// Token: 0x040016F1 RID: 5873
		private RenderChain.VisualChangesProcessor m_VisualChangesProcessor;

		// Token: 0x040016F2 RID: 5874
		private LinkedPool<RenderChainCommand> m_CommandPool = new LinkedPool<RenderChainCommand>(() => new RenderChainCommand(), delegate(RenderChainCommand r)
		{
			r.Reset();
		}, 10000);

		// Token: 0x040016F3 RID: 5875
		private LinkedPool<ExtraRenderChainVEData> m_ExtraDataPool = new LinkedPool<ExtraRenderChainVEData>(() => new ExtraRenderChainVEData(), delegate(ExtraRenderChainVEData e)
		{
			e.Reset();
		}, 10000);

		// Token: 0x040016F4 RID: 5876
		private BasicNodePool<MeshHandle> m_MeshHandleNodePool = new BasicNodePool<MeshHandle>();

		// Token: 0x040016F5 RID: 5877
		private BasicNodePool<TextureEntry> m_TexturePool = new BasicNodePool<TextureEntry>();

		// Token: 0x040016F6 RID: 5878
		private Dictionary<VisualElement, ExtraRenderChainVEData> m_ExtraData = new Dictionary<VisualElement, ExtraRenderChainVEData>();

		// Token: 0x040016F7 RID: 5879
		private MeshGenerationDeferrer m_MeshGenerationDeferrer = new MeshGenerationDeferrer();

		// Token: 0x040016F8 RID: 5880
		private Material m_DefaultMat;

		// Token: 0x040016F9 RID: 5881
		private bool m_BlockDirtyRegistration;

		// Token: 0x040016FA RID: 5882
		private ChainBuilderStats m_Stats;

		// Token: 0x040016FB RID: 5883
		private uint m_StatsElementsAdded;

		// Token: 0x040016FC RID: 5884
		private uint m_StatsElementsRemoved;

		// Token: 0x040016FD RID: 5885
		private TextureRegistry m_TextureRegistry = TextureRegistry.instance;

		// Token: 0x040016FF RID: 5887
		private static EntryPool s_SharedEntryPool = new EntryPool(10000);

		// Token: 0x04001700 RID: 5888
		private static readonly ProfilerMarker k_MarkerProcess = new ProfilerMarker("RenderChain.Process");

		// Token: 0x04001701 RID: 5889
		private static readonly ProfilerMarker k_MarkerClipProcessing = new ProfilerMarker("RenderChain.UpdateClips");

		// Token: 0x04001702 RID: 5890
		private static readonly ProfilerMarker k_MarkerOpacityProcessing = new ProfilerMarker("RenderChain.UpdateOpacity");

		// Token: 0x04001703 RID: 5891
		private static readonly ProfilerMarker k_MarkerColorsProcessing = new ProfilerMarker("RenderChain.UpdateColors");

		// Token: 0x04001704 RID: 5892
		private static readonly ProfilerMarker k_MarkerTransformProcessing = new ProfilerMarker("RenderChain.UpdateTransforms");

		// Token: 0x04001705 RID: 5893
		private static readonly ProfilerMarker k_MarkerVisualsProcessing = new ProfilerMarker("RenderChain.UpdateVisuals");

		// Token: 0x04001706 RID: 5894
		private static readonly ProfilerMarker k_MarkerSerialize = new ProfilerMarker("RenderChain.Serialize");

		// Token: 0x0400170E RID: 5902
		public EntryRecorder entryRecorder = new EntryRecorder(RenderChain.s_SharedEntryPool);

		// Token: 0x04001711 RID: 5905
		internal UIRVEShaderInfoAllocator shaderInfoAllocator;

		// Token: 0x02000660 RID: 1632
		private struct DepthOrderedDirtyTracking
		{
			// Token: 0x06002FDE RID: 12254 RVA: 0x000B3504 File Offset: 0x000B1704
			public void EnsureFits(int maxDepth)
			{
				while (this.heads.Count <= maxDepth)
				{
					this.heads.Add(null);
					this.tails.Add(null);
				}
			}

			// Token: 0x06002FDF RID: 12255 RVA: 0x000B3548 File Offset: 0x000B1748
			public void RegisterDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypes, RenderDataDirtyTypeClasses dirtyTypeClass)
			{
				Debug.Assert(dirtyTypes > RenderDataDirtyTypes.None);
				int hierarchyDepth = ve.renderChainData.hierarchyDepth;
				this.minDepths[(int)dirtyTypeClass] = ((hierarchyDepth < this.minDepths[(int)dirtyTypeClass]) ? hierarchyDepth : this.minDepths[(int)dirtyTypeClass]);
				this.maxDepths[(int)dirtyTypeClass] = ((hierarchyDepth > this.maxDepths[(int)dirtyTypeClass]) ? hierarchyDepth : this.maxDepths[(int)dirtyTypeClass]);
				bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
				if (flag)
				{
					ve.renderChainData.dirtiedValues = ve.renderChainData.dirtiedValues | dirtyTypes;
				}
				else
				{
					ve.renderChainData.dirtiedValues = dirtyTypes;
					bool flag2 = this.tails[hierarchyDepth] != null;
					if (flag2)
					{
						this.tails[hierarchyDepth].renderChainData.nextDirty = ve;
						ve.renderChainData.prevDirty = this.tails[hierarchyDepth];
						this.tails[hierarchyDepth] = ve;
					}
					else
					{
						List<VisualElement> list = this.heads;
						int num = hierarchyDepth;
						this.tails[hierarchyDepth] = ve;
						list[num] = ve;
					}
				}
			}

			// Token: 0x06002FE0 RID: 12256 RVA: 0x000B3650 File Offset: 0x000B1850
			public void ClearDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypesInverse)
			{
				Debug.Assert(ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None);
				ve.renderChainData.dirtiedValues = ve.renderChainData.dirtiedValues & dirtyTypesInverse;
				bool flag = ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None;
				if (flag)
				{
					bool flag2 = ve.renderChainData.prevDirty != null;
					if (flag2)
					{
						ve.renderChainData.prevDirty.renderChainData.nextDirty = ve.renderChainData.nextDirty;
					}
					bool flag3 = ve.renderChainData.nextDirty != null;
					if (flag3)
					{
						ve.renderChainData.nextDirty.renderChainData.prevDirty = ve.renderChainData.prevDirty;
					}
					bool flag4 = this.tails[ve.renderChainData.hierarchyDepth] == ve;
					if (flag4)
					{
						Debug.Assert(ve.renderChainData.nextDirty == null);
						this.tails[ve.renderChainData.hierarchyDepth] = ve.renderChainData.prevDirty;
					}
					bool flag5 = this.heads[ve.renderChainData.hierarchyDepth] == ve;
					if (flag5)
					{
						Debug.Assert(ve.renderChainData.prevDirty == null);
						this.heads[ve.renderChainData.hierarchyDepth] = ve.renderChainData.nextDirty;
					}
					ve.renderChainData.prevDirty = (ve.renderChainData.nextDirty = null);
				}
			}

			// Token: 0x06002FE1 RID: 12257 RVA: 0x000B37C8 File Offset: 0x000B19C8
			public void Reset()
			{
				for (int i = 0; i < this.minDepths.Length; i++)
				{
					this.minDepths[i] = int.MaxValue;
					this.maxDepths[i] = int.MinValue;
				}
			}

			// Token: 0x04001716 RID: 5910
			public List<VisualElement> heads;

			// Token: 0x04001717 RID: 5911
			public List<VisualElement> tails;

			// Token: 0x04001718 RID: 5912
			public int[] minDepths;

			// Token: 0x04001719 RID: 5913
			public int[] maxDepths;

			// Token: 0x0400171A RID: 5914
			public uint dirtyID;
		}

		// Token: 0x02000661 RID: 1633
		private class VisualChangesProcessor : IDisposable
		{
			// Token: 0x17000B7F RID: 2943
			// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000B3809 File Offset: 0x000B1A09
			public BaseElementBuilder elementBuilder
			{
				get
				{
					return this.m_ElementBuilder;
				}
			}

			// Token: 0x17000B80 RID: 2944
			// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000B3811 File Offset: 0x000B1A11
			public MeshGenerationContext meshGenerationContext
			{
				get
				{
					return this.m_MeshGenerationContext;
				}
			}

			// Token: 0x06002FE4 RID: 12260 RVA: 0x000B381C File Offset: 0x000B1A1C
			public VisualChangesProcessor(RenderChain renderChain)
			{
				this.m_RenderChain = renderChain;
				this.m_MeshGenerationContext = new MeshGenerationContext(this.m_RenderChain.meshWriteDataPool, this.m_RenderChain.entryRecorder, this.m_RenderChain.tempMeshAllocator, this.m_RenderChain.meshGenerationDeferrer, this.m_RenderChain.meshGenerationNodeManager);
				this.m_ElementBuilder = new DefaultElementBuilder(this.m_RenderChain);
				this.m_EntryProcessingList = new List<RenderChain.VisualChangesProcessor.EntryProcessingInfo>();
				this.m_Processors = new List<EntryProcessor>(4);
			}

			// Token: 0x06002FE5 RID: 12261 RVA: 0x000B38A2 File Offset: 0x000B1AA2
			public void ScheduleMeshGenerationJobs()
			{
				this.m_ElementBuilder.ScheduleMeshGenerationJobs(this.m_MeshGenerationContext);
			}

			// Token: 0x06002FE6 RID: 12262 RVA: 0x000B38B8 File Offset: 0x000B1AB8
			public void ProcessOnVisualsChanged(VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
			{
				bool flag = ve.renderChainData.pendingHierarchicalRepaint || (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None;
				bool flag2 = flag;
				if (flag2)
				{
					stats.recursiveVisualUpdates += 1U;
				}
				else
				{
					stats.nonRecursiveVisualUpdates += 1U;
				}
				this.DepthFirstOnVisualsChanged(ve, dirtyID, flag, ref stats);
			}

			// Token: 0x06002FE7 RID: 12263 RVA: 0x000B3910 File Offset: 0x000B1B10
			private void DepthFirstOnVisualsChanged(VisualElement ve, uint dirtyID, bool hierarchical, ref ChainBuilderStats stats)
			{
				bool flag = dirtyID == ve.renderChainData.dirtyID;
				if (!flag)
				{
					ve.renderChainData.dirtyID = dirtyID;
					if (hierarchical)
					{
						stats.recursiveVisualUpdatesExpanded += 1U;
					}
					bool flag2 = !ve.areAncestorsAndSelfDisplayed;
					if (flag2)
					{
						if (hierarchical)
						{
							ve.renderChainData.pendingHierarchicalRepaint = true;
						}
						else
						{
							ve.renderChainData.pendingRepaint = true;
						}
					}
					else
					{
						ve.renderChainData.pendingHierarchicalRepaint = false;
						ve.renderChainData.pendingRepaint = false;
						bool flag3 = !hierarchical && (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.AllVisuals) == RenderDataDirtyTypes.VisualsOpacityId;
						if (flag3)
						{
							stats.opacityIdUpdates += 1U;
							RenderChain.VisualChangesProcessor.UpdateOpacityId(ve, this.m_RenderChain);
						}
						else
						{
							RenderChain.VisualChangesProcessor.UpdateWorldFlipsWinding(ve);
							Debug.Assert(ve.renderChainData.clipMethod > ClipMethod.Undetermined);
							Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.hierarchy.parent == null || ve.renderChainData.transformID.Equals(ve.hierarchy.parent.renderChainData.transformID) || ve.renderChainData.isGroupTransform);
							bool flag4 = ve is TextElement;
							if (flag4)
							{
								RenderEvents.UpdateTextCoreSettings(this.m_RenderChain, ve);
							}
							bool flag5 = (ve.renderHints & RenderHints.DynamicColor) == RenderHints.DynamicColor;
							if (flag5)
							{
								RenderEvents.SetColorValues(this.m_RenderChain, ve);
							}
							Entry entry = this.m_RenderChain.entryPool.Get();
							entry.type = EntryType.DedicatedPlaceholder;
							this.m_EntryProcessingList.Add(new RenderChain.VisualChangesProcessor.EntryProcessingInfo
							{
								type = RenderChain.VisualChangesProcessor.VisualsProcessingType.Head,
								visualElement = ve,
								rootEntry = entry
							});
							this.m_MeshGenerationContext.Begin(entry, ve);
							this.m_ElementBuilder.Build(this.m_MeshGenerationContext);
							this.m_MeshGenerationContext.End();
							if (hierarchical)
							{
								int childCount = ve.hierarchy.childCount;
								for (int i = 0; i < childCount; i++)
								{
									this.DepthFirstOnVisualsChanged(ve.hierarchy[i], dirtyID, true, ref stats);
								}
							}
							this.m_EntryProcessingList.Add(new RenderChain.VisualChangesProcessor.EntryProcessingInfo
							{
								type = RenderChain.VisualChangesProcessor.VisualsProcessingType.Tail,
								visualElement = ve,
								rootEntry = entry
							});
						}
					}
				}
			}

			// Token: 0x06002FE8 RID: 12264 RVA: 0x000B3B84 File Offset: 0x000B1D84
			private static void UpdateWorldFlipsWinding(VisualElement ve)
			{
				bool localFlipsWinding = ve.renderChainData.localFlipsWinding;
				bool flag = false;
				VisualElement parent = ve.hierarchy.parent;
				bool flag2 = parent != null;
				if (flag2)
				{
					flag = parent.renderChainData.worldFlipsWinding;
				}
				ve.renderChainData.worldFlipsWinding = flag ^ localFlipsWinding;
			}

			// Token: 0x06002FE9 RID: 12265 RVA: 0x000B3BD4 File Offset: 0x000B1DD4
			public void ConvertEntriesToCommands(ref ChainBuilderStats stats)
			{
				int num = 0;
				for (int i = 0; i < this.m_EntryProcessingList.Count; i++)
				{
					RenderChain.VisualChangesProcessor.EntryProcessingInfo entryProcessingInfo = this.m_EntryProcessingList[i];
					bool flag = entryProcessingInfo.type == RenderChain.VisualChangesProcessor.VisualsProcessingType.Head;
					if (flag)
					{
						bool flag2 = num < this.m_Processors.Count;
						EntryProcessor entryProcessor;
						if (flag2)
						{
							entryProcessor = this.m_Processors[num];
						}
						else
						{
							entryProcessor = new EntryProcessor();
							this.m_Processors.Add(entryProcessor);
						}
						num++;
						entryProcessor.Init(entryProcessingInfo.rootEntry, this.m_RenderChain, entryProcessingInfo.visualElement);
						entryProcessor.ProcessHead();
					}
					else
					{
						num--;
						EntryProcessor entryProcessor2 = this.m_Processors[num];
						entryProcessor2.ProcessTail();
						bool flag3 = entryProcessor2.firstHeadCommand != null || entryProcessor2.firstTailCommand != null;
						bool flag4 = flag3;
						if (flag4)
						{
						}
						CommandManipulator.ReplaceCommands(this.m_RenderChain, entryProcessingInfo.visualElement, entryProcessor2);
					}
				}
				this.m_EntryProcessingList.Clear();
				for (int j = 0; j < this.m_Processors.Count; j++)
				{
					this.m_Processors[j].ClearReferences();
				}
			}

			// Token: 0x06002FEA RID: 12266 RVA: 0x000B3D18 File Offset: 0x000B1F18
			public static void UpdateOpacityId(VisualElement ve, RenderChain renderChain)
			{
				bool flag = ve.renderChainData.headMesh != null;
				if (flag)
				{
					RenderChain.VisualChangesProcessor.DoUpdateOpacityId(ve, renderChain, ve.renderChainData.headMesh);
				}
				bool flag2 = ve.renderChainData.tailMesh != null;
				if (flag2)
				{
					RenderChain.VisualChangesProcessor.DoUpdateOpacityId(ve, renderChain, ve.renderChainData.tailMesh);
				}
				bool hasExtraMeshes = ve.renderChainData.hasExtraMeshes;
				if (hasExtraMeshes)
				{
					ExtraRenderChainVEData orAddExtraData = renderChain.GetOrAddExtraData(ve);
					for (BasicNode<MeshHandle> basicNode = orAddExtraData.extraMesh; basicNode != null; basicNode = basicNode.next)
					{
						RenderChain.VisualChangesProcessor.DoUpdateOpacityId(ve, renderChain, basicNode.data);
					}
				}
			}

			// Token: 0x06002FEB RID: 12267 RVA: 0x000B3DB8 File Offset: 0x000B1FB8
			private static void DoUpdateOpacityId(VisualElement ve, RenderChain renderChain, MeshHandle mesh)
			{
				int size = (int)mesh.allocVerts.size;
				NativeSlice<Vertex> nativeSlice = mesh.allocPage.vertices.cpuData.Slice<Vertex>((int)mesh.allocVerts.start, size);
				NativeSlice<Vertex> nativeSlice2;
				renderChain.device.Update(mesh, (uint)size, out nativeSlice2);
				Color32 color = renderChain.shaderInfoAllocator.OpacityAllocToVertexData(ve.renderChainData.opacityID);
				renderChain.opacityIdAccelerator.CreateJob(nativeSlice, nativeSlice2, color, size);
			}

			// Token: 0x17000B81 RID: 2945
			// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000B3E2B File Offset: 0x000B202B
			// (set) Token: 0x06002FED RID: 12269 RVA: 0x000B3E33 File Offset: 0x000B2033
			private protected bool disposed { protected get; private set; }

			// Token: 0x06002FEE RID: 12270 RVA: 0x000B3E3C File Offset: 0x000B203C
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06002FEF RID: 12271 RVA: 0x000B3E50 File Offset: 0x000B2050
			protected void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						this.m_MeshGenerationContext.Dispose();
						this.m_MeshGenerationContext = null;
					}
					this.disposed = true;
				}
			}

			// Token: 0x0400171B RID: 5915
			private static readonly ProfilerMarker k_GenerateEntriesMarker = new ProfilerMarker("UIR.GenerateEntries");

			// Token: 0x0400171C RID: 5916
			private static readonly ProfilerMarker k_ConvertEntriesToCommandsMarker = new ProfilerMarker("UIR.ConvertEntriesToCommands");

			// Token: 0x0400171D RID: 5917
			private static readonly ProfilerMarker k_UpdateOpacityIdMarker = new ProfilerMarker("UIR.UpdateOpacityId");

			// Token: 0x0400171E RID: 5918
			private RenderChain m_RenderChain;

			// Token: 0x0400171F RID: 5919
			private MeshGenerationContext m_MeshGenerationContext;

			// Token: 0x04001720 RID: 5920
			private BaseElementBuilder m_ElementBuilder;

			// Token: 0x04001721 RID: 5921
			private List<RenderChain.VisualChangesProcessor.EntryProcessingInfo> m_EntryProcessingList;

			// Token: 0x04001722 RID: 5922
			private List<EntryProcessor> m_Processors;

			// Token: 0x02000662 RID: 1634
			private enum VisualsProcessingType
			{
				// Token: 0x04001725 RID: 5925
				Head,
				// Token: 0x04001726 RID: 5926
				Tail
			}

			// Token: 0x02000663 RID: 1635
			private struct EntryProcessingInfo
			{
				// Token: 0x04001727 RID: 5927
				public VisualElement visualElement;

				// Token: 0x04001728 RID: 5928
				public RenderChain.VisualChangesProcessor.VisualsProcessingType type;

				// Token: 0x04001729 RID: 5929
				public Entry rootEntry;
			}
		}
	}
}
