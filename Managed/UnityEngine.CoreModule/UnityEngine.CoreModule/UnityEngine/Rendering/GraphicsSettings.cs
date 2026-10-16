using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004C9 RID: 1225
	[NativeHeader("Runtime/Camera/GraphicsSettings.h")]
	[StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	public sealed class GraphicsSettings : Object
	{
		// Token: 0x06002DD0 RID: 11728 RVA: 0x00008C6F File Offset: 0x00006E6F
		private GraphicsSettings()
		{
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002DD1 RID: 11729
		// (set) Token: 0x06002DD2 RID: 11730
		public static extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x00056FF8 File Offset: 0x000551F8
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x00057010 File Offset: 0x00055210
		public static Vector3 transparencySortAxis
		{
			get
			{
				Vector3 vector;
				GraphicsSettings.get_transparencySortAxis_Injected(out vector);
				return vector;
			}
			set
			{
				GraphicsSettings.set_transparencySortAxis_Injected(ref value);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002DD5 RID: 11733
		// (set) Token: 0x06002DD6 RID: 11734
		public static extern bool realtimeDirectRectangularAreaLights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002DD7 RID: 11735
		// (set) Token: 0x06002DD8 RID: 11736
		public static extern bool lightsUseLinearIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002DD9 RID: 11737
		// (set) Token: 0x06002DDA RID: 11738
		public static extern bool lightsUseColorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002DDB RID: 11739
		// (set) Token: 0x06002DDC RID: 11740
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)")]
		public static extern uint defaultRenderingLayerMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002DDD RID: 11741
		// (set) Token: 0x06002DDE RID: 11742
		public static extern Camera.GateFitMode defaultGateFitMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002DDF RID: 11743
		// (set) Token: 0x06002DE0 RID: 11744
		public static extern bool useScriptableRenderPipelineBatching
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002DE1 RID: 11745
		// (set) Token: 0x06002DE2 RID: 11746
		public static extern bool logWhenShaderIsCompiled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002DE3 RID: 11747
		// (set) Token: 0x06002DE4 RID: 11748
		public static extern bool disableBuiltinCustomRenderTextureUpdate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002DE5 RID: 11749
		public static extern VideoShadersIncludeMode videoShadersIncludeMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002DE6 RID: 11750
		// (set) Token: 0x06002DE7 RID: 11751
		public static extern LightProbeOutsideHullStrategy lightProbeOutsideHullStrategy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06002DE8 RID: 11752
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		// Token: 0x06002DE9 RID: 11753 RVA: 0x00057024 File Offset: 0x00055224
		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return GraphicsSettings.HasShaderDefine(Graphics.activeTier, defineHash);
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x00057044 File Offset: 0x00055244
		[NativeName("CurrentRenderPipeline")]
		private static ScriptableObject INTERNAL_currentRenderPipeline
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<ScriptableObject>(GraphicsSettings.get_INTERNAL_currentRenderPipeline_Injected());
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x0005705C File Offset: 0x0005525C
		public static RenderPipelineAsset currentRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline as RenderPipelineAsset;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x00057078 File Offset: 0x00055278
		public static bool isScriptableRenderPipelineEnabled
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline != null;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x00057085 File Offset: 0x00055285
		public static Type currentRenderPipelineAssetType
		{
			get
			{
				return GraphicsSettings.isScriptableRenderPipelineEnabled ? GraphicsSettings.INTERNAL_currentRenderPipeline.GetType() : null;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x0005709C File Offset: 0x0005529C
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x000570B3 File Offset: 0x000552B3
		[Obsolete("renderPipelineAsset has been deprecated. Use defaultRenderPipeline instead (UnityUpgradable) -> defaultRenderPipeline", false)]
		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return GraphicsSettings.defaultRenderPipeline;
			}
			set
			{
				GraphicsSettings.defaultRenderPipeline = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000570C0 File Offset: 0x000552C0
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x000570D8 File Offset: 0x000552D8
		[NativeName("DefaultRenderPipeline")]
		private static ScriptableObject INTERNAL_defaultRenderPipeline
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<ScriptableObject>(GraphicsSettings.get_INTERNAL_defaultRenderPipeline_Injected());
			}
			set
			{
				GraphicsSettings.set_INTERNAL_defaultRenderPipeline_Injected(Object.MarshalledUnityObject.Marshal<ScriptableObject>(value));
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000570F0 File Offset: 0x000552F0
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x0005710C File Offset: 0x0005530C
		public static RenderPipelineAsset defaultRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_defaultRenderPipeline as RenderPipelineAsset;
			}
			set
			{
				GraphicsSettings.ValidateSetRenderPipelineAsset(value);
				GraphicsSettings.INTERNAL_defaultRenderPipeline = value;
			}
		}

		// Token: 0x06002DF4 RID: 11764
		[NativeName("GetAllConfiguredRenderPipelinesForScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] GetAllConfiguredRenderPipelines();

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x00057120 File Offset: 0x00055320
		public static RenderPipelineAsset[] allConfiguredRenderPipelines
		{
			get
			{
				return GraphicsSettings.GetAllConfiguredRenderPipelines().Cast<RenderPipelineAsset>().ToArray<RenderPipelineAsset>();
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x00057144 File Offset: 0x00055344
		[FreeFunction]
		public static Object GetGraphicsSettings()
		{
			return Unmarshal.UnmarshalUnityObject<Object>(GraphicsSettings.GetGraphicsSettings_Injected());
		}

		// Token: 0x06002DF7 RID: 11767
		[NativeName("SetShaderModeScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShaderMode(BuiltinShaderType type, BuiltinShaderMode mode);

		// Token: 0x06002DF8 RID: 11768
		[NativeName("GetShaderModeScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern BuiltinShaderMode GetShaderMode(BuiltinShaderType type);

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0005715C File Offset: 0x0005535C
		[NativeName("SetCustomShaderScript")]
		public static void SetCustomShader(BuiltinShaderType type, Shader shader)
		{
			GraphicsSettings.SetCustomShader_Injected(type, Object.MarshalledUnityObject.Marshal<Shader>(shader));
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x00057178 File Offset: 0x00055378
		[NativeName("GetCustomShaderScript")]
		public static Shader GetCustomShader(BuiltinShaderType type)
		{
			return Unmarshal.UnmarshalUnityObject<Shader>(GraphicsSettings.GetCustomShader_Injected(type));
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002DFB RID: 11771
		// (set) Token: 0x06002DFC RID: 11772
		public static extern bool cameraRelativeLightCulling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002DFD RID: 11773
		// (set) Token: 0x06002DFE RID: 11774
		public static extern bool cameraRelativeShadowCulling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x00057190 File Offset: 0x00055390
		[RequiredByNativeCode]
		[VisibleToOtherModules]
		internal static Shader GetDefaultShader(DefaultShaderType type)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			bool flag = GraphicsSettings.currentRenderPipeline == null;
			Shader shader;
			if (flag)
			{
				shader = null;
			}
			else
			{
				if (!true)
				{
				}
				Shader shader2;
				switch (type)
				{
				case DefaultShaderType.Default:
					shader2 = currentRenderPipeline.defaultShader;
					break;
				case DefaultShaderType.AutodeskInteractive:
					shader2 = currentRenderPipeline.autodeskInteractiveShader;
					break;
				case DefaultShaderType.AutodeskInteractiveTransparent:
					shader2 = currentRenderPipeline.autodeskInteractiveTransparentShader;
					break;
				case DefaultShaderType.AutodeskInteractiveMasked:
					shader2 = currentRenderPipeline.autodeskInteractiveMaskedShader;
					break;
				case DefaultShaderType.TerrainDetailLit:
					shader2 = currentRenderPipeline.terrainDetailLitShader;
					break;
				case DefaultShaderType.TerrainDetailGrass:
					shader2 = currentRenderPipeline.terrainDetailGrassShader;
					break;
				case DefaultShaderType.TerrainDetailGrassBillboard:
					shader2 = currentRenderPipeline.terrainDetailGrassBillboardShader;
					break;
				case DefaultShaderType.SpeedTree7:
					shader2 = currentRenderPipeline.defaultSpeedTree7Shader;
					break;
				case DefaultShaderType.SpeedTree8:
					shader2 = currentRenderPipeline.defaultSpeedTree8Shader;
					break;
				case DefaultShaderType.SpeedTree9:
					shader2 = currentRenderPipeline.defaultSpeedTree9Shader;
					break;
				default:
					throw new NotImplementedException(string.Format("DefaultShaderType {0} not implemented", type));
				}
				if (!true)
				{
				}
				shader = shader2;
			}
			return shader;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00057268 File Offset: 0x00055468
		[RequiredByNativeCode]
		[VisibleToOtherModules]
		internal static Material GetDefaultMaterial(DefaultMaterialType type)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			bool flag = GraphicsSettings.currentRenderPipeline == null;
			Material material;
			if (flag)
			{
				material = null;
			}
			else
			{
				if (!true)
				{
				}
				Material material2;
				switch (type)
				{
				case DefaultMaterialType.Default:
					material2 = currentRenderPipeline.defaultMaterial;
					break;
				case DefaultMaterialType.Particle:
					material2 = currentRenderPipeline.defaultParticleMaterial;
					break;
				case DefaultMaterialType.Line:
					material2 = currentRenderPipeline.defaultLineMaterial;
					break;
				case DefaultMaterialType.Terrain:
					material2 = currentRenderPipeline.defaultTerrainMaterial;
					break;
				case DefaultMaterialType.Sprite:
					material2 = currentRenderPipeline.default2DMaterial;
					break;
				case DefaultMaterialType.SpriteMask:
					material2 = currentRenderPipeline.default2DMaskMaterial;
					break;
				case DefaultMaterialType.UGUI:
					material2 = currentRenderPipeline.defaultUIMaterial;
					break;
				case DefaultMaterialType.UGUI_Overdraw:
					material2 = currentRenderPipeline.defaultUIOverdrawMaterial;
					break;
				case DefaultMaterialType.UGUI_ETC1Supported:
					material2 = currentRenderPipeline.defaultUIETC1SupportedMaterial;
					break;
				default:
					throw new NotImplementedException(string.Format("DefaultMaterialType {0} not implemented", type));
				}
				if (!true)
				{
				}
				material = material2;
			}
			return material;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x00057334 File Offset: 0x00055534
		[NativeName("RegisterRenderPipelineSettings")]
		private unsafe static void Internal_RegisterRenderPipeline(string renderpipelineName, Object settings)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(renderpipelineName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = renderpipelineName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GraphicsSettings.Internal_RegisterRenderPipeline_Injected(ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Object>(settings));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x00057390 File Offset: 0x00055590
		[NativeName("UnregisterRenderPipelineSettings")]
		private unsafe static void Internal_UnregisterRenderPipeline(string renderpipelineName)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(renderpipelineName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = renderpipelineName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GraphicsSettings.Internal_UnregisterRenderPipeline_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000573E4 File Offset: 0x000555E4
		[NativeName("GetSettingsForRenderPipeline")]
		private unsafe static Object Internal_GetSettingsForRenderPipeline(string renderpipelineName)
		{
			Object @object;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(renderpipelineName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = renderpipelineName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = GraphicsSettings.Internal_GetSettingsForRenderPipeline_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				@object = Unmarshal.UnmarshalUnityObject<Object>(intPtr);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x00057444 File Offset: 0x00055644
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x0005745C File Offset: 0x0005565C
		[NativeName("CurrentRenderPipelineGlobalSettings")]
		private static Object INTERNAL_currentRenderPipelineGlobalSettings
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Object>(GraphicsSettings.get_INTERNAL_currentRenderPipelineGlobalSettings_Injected());
			}
			set
			{
				GraphicsSettings.set_INTERNAL_currentRenderPipelineGlobalSettings_Injected(Object.MarshalledUnityObject.Marshal<Object>(value));
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x00057474 File Offset: 0x00055674
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x00057480 File Offset: 0x00055680
		internal static RenderPipelineGlobalSettings currentRenderPipelineGlobalSettings
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipelineGlobalSettings as RenderPipelineGlobalSettings;
			}
			set
			{
				GraphicsSettings.INTERNAL_currentRenderPipelineGlobalSettings = value;
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0005748C File Offset: 0x0005568C
		private static void CheckRenderPipelineType(Type renderPipelineType)
		{
			bool flag = renderPipelineType == null;
			if (flag)
			{
				throw new ArgumentNullException("renderPipelineType");
			}
			bool flag2 = !typeof(RenderPipeline).IsAssignableFrom(renderPipelineType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} must be a valid {1}", renderPipelineType, "RenderPipeline"));
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000574E0 File Offset: 0x000556E0
		[Obsolete("Please use EditorGraphicsSettings.SetRenderPipelineGlobalSettingsAsset(renderPipelineType, newSettings). #from(23.2)", false)]
		public static void UpdateGraphicsSettings(RenderPipelineGlobalSettings newSettings, Type renderPipelineType)
		{
			GraphicsSettings.CheckRenderPipelineType(renderPipelineType);
			bool flag = newSettings != null;
			if (flag)
			{
				GraphicsSettings.Internal_RegisterRenderPipeline(renderPipelineType.FullName, newSettings);
			}
			else
			{
				GraphicsSettings.Internal_UnregisterRenderPipeline(renderPipelineType.FullName);
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0005751B File Offset: 0x0005571B
		[Obsolete("Please use EditorGraphicsSettings.SetRenderPipelineGlobalSettingsAsset(renderPipelineType, settings). #from(23.2)", false)]
		public static void RegisterRenderPipelineSettings(Type renderPipelineType, RenderPipelineGlobalSettings settings)
		{
			GraphicsSettings.CheckRenderPipelineType(renderPipelineType);
			GraphicsSettings.Internal_RegisterRenderPipeline(renderPipelineType.FullName, settings);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00057532 File Offset: 0x00055732
		[Obsolete("Please use EditorGraphicsSettings.SetRenderPipelineGlobalSettingsAsset<TRenderPipelineType>(settings). #from(23.2)", false)]
		public static void RegisterRenderPipelineSettings<T>(RenderPipelineGlobalSettings settings) where T : RenderPipeline
		{
			GraphicsSettings.Internal_RegisterRenderPipeline(typeof(T).FullName, settings);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0005754B File Offset: 0x0005574B
		[Obsolete("Please use EditorGraphicsSettings.SetRenderPipelineGlobalSettingsAsset<TRenderPipelineType>(null). #from(23.2)", false)]
		public static void UnregisterRenderPipelineSettings<T>() where T : RenderPipeline
		{
			GraphicsSettings.Internal_UnregisterRenderPipeline(typeof(T).FullName);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00057563 File Offset: 0x00055763
		[Obsolete("Please use EditorGraphicsSettings.SetRenderPipelineGlobalSettingsAsset(renderPipelineType, null). #from(23.2)", false)]
		public static void UnregisterRenderPipelineSettings(Type renderPipelineType)
		{
			GraphicsSettings.CheckRenderPipelineType(renderPipelineType);
			GraphicsSettings.Internal_UnregisterRenderPipeline(renderPipelineType.FullName);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0005757C File Offset: 0x0005577C
		public static RenderPipelineGlobalSettings GetSettingsForRenderPipeline<T>() where T : RenderPipeline
		{
			return GraphicsSettings.Internal_GetSettingsForRenderPipeline(typeof(T).FullName) as RenderPipelineGlobalSettings;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000575A8 File Offset: 0x000557A8
		public static RenderPipelineGlobalSettings GetSettingsForRenderPipeline(Type renderPipelineType)
		{
			GraphicsSettings.CheckRenderPipelineType(renderPipelineType);
			return GraphicsSettings.Internal_GetSettingsForRenderPipeline(renderPipelineType.FullName) as RenderPipelineGlobalSettings;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000575D4 File Offset: 0x000557D4
		private static RenderPipelineGlobalSettings Internal_GetCurrentRenderPipelineGlobalSettings()
		{
			RenderPipelineGlobalSettings renderPipelineGlobalSettings = null;
			bool flag = GraphicsSettings.currentRenderPipeline != null;
			if (flag)
			{
				renderPipelineGlobalSettings = GraphicsSettings.Internal_GetSettingsForRenderPipeline(GraphicsSettings.currentRenderPipeline.pipelineTypeFullName) as RenderPipelineGlobalSettings;
			}
			return renderPipelineGlobalSettings;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00057610 File Offset: 0x00055810
		internal static void ValidateSetRenderPipelineAsset(RenderPipelineAsset newRenderPipelineAsset)
		{
			bool flag = newRenderPipelineAsset == null;
			if (!flag)
			{
				bool flag2 = !newRenderPipelineAsset.requiresCompatibleRenderPipelineGlobalSettings;
				if (!flag2)
				{
					RenderPipelineGlobalSettings renderPipelineGlobalSettings;
					bool flag3 = !GraphicsSettings.TryGetCurrentRenderPipelineGlobalSettings(out renderPipelineGlobalSettings);
					if (flag3)
					{
						throw new InvalidOperationException("Cannot set " + newRenderPipelineAsset.name + " Render Pipeline Asset when there is no current RenderPipelineGlobalSettings set in GraphicsSettings. Make sure your project has one when you build or use TrySetCurrentRenderPipelineGlobalSettings to set it in Player before setting Render Pipeline asset.");
					}
					Type type = newRenderPipelineAsset.GetType();
					bool flag4 = !SupportedOnRenderPipelineAttribute.IsTypeSupportedOnRenderPipeline(renderPipelineGlobalSettings.GetType(), type);
					if (flag4)
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"Cannot set ",
							newRenderPipelineAsset.name,
							" Render Pipeline Asset of type ",
							type.Name,
							" when the current RenderPipelineGlobalSettings is of type ",
							renderPipelineGlobalSettings.GetType().Name,
							". Make sure your project has Render Pipeline Global Settings for ",
							newRenderPipelineAsset.name,
							" render pipeline when you build or use TrySetCurrentRenderPipelineGlobalSettings to set it in Player before setting Render Pipeline asset."
						}));
					}
				}
			}
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000576EC File Offset: 0x000558EC
		public static bool TrySetCurrentRenderPipelineGlobalSettings(RenderPipelineGlobalSettings asset)
		{
			bool flag = asset == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				GraphicsSettings.currentRenderPipelineGlobalSettings = asset;
				GraphicsSettings.s_CurrentRenderPipelineGlobalSettings = new Lazy<RenderPipelineGlobalSettings>(GraphicsSettings.currentRenderPipelineGlobalSettings);
				RenderPipelineManager.CleanupRenderPipeline();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0005772C File Offset: 0x0005592C
		public static bool TryGetCurrentRenderPipelineGlobalSettings(out RenderPipelineGlobalSettings asset)
		{
			asset = GraphicsSettings.s_CurrentRenderPipelineGlobalSettings.Value;
			return asset != null;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00057754 File Offset: 0x00055954
		public static T GetRenderPipelineSettings<T>() where T : class, IRenderPipelineGraphicsSettings
		{
			T t;
			GraphicsSettings.TryGetRenderPipelineSettings<T>(out t);
			return t;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00057770 File Offset: 0x00055970
		public static bool TryGetRenderPipelineSettings<T>(out T settings) where T : class, IRenderPipelineGraphicsSettings
		{
			settings = default(T);
			RenderPipelineGlobalSettings renderPipelineGlobalSettings;
			bool flag = !GraphicsSettings.TryGetCurrentRenderPipelineGlobalSettings(out renderPipelineGlobalSettings);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				IRenderPipelineGraphicsSettings renderPipelineGraphicsSettings;
				bool flag3 = renderPipelineGlobalSettings.TryGet(typeof(T), out renderPipelineGraphicsSettings);
				if (flag3)
				{
					settings = renderPipelineGraphicsSettings as T;
				}
				flag2 = settings != null;
			}
			return flag2;
		}

		// Token: 0x06002E17 RID: 11799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_transparencySortAxis_Injected(out Vector3 ret);

		// Token: 0x06002E18 RID: 11800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortAxis_Injected([In] ref Vector3 value);

		// Token: 0x06002E19 RID: 11801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_INTERNAL_currentRenderPipeline_Injected();

		// Token: 0x06002E1A RID: 11802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_INTERNAL_defaultRenderPipeline_Injected();

		// Token: 0x06002E1B RID: 11803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_INTERNAL_defaultRenderPipeline_Injected(IntPtr value);

		// Token: 0x06002E1C RID: 11804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetGraphicsSettings_Injected();

		// Token: 0x06002E1D RID: 11805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomShader_Injected(BuiltinShaderType type, IntPtr shader);

		// Token: 0x06002E1E RID: 11806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCustomShader_Injected(BuiltinShaderType type);

		// Token: 0x06002E1F RID: 11807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RegisterRenderPipeline_Injected(ref ManagedSpanWrapper renderpipelineName, IntPtr settings);

		// Token: 0x06002E20 RID: 11808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_UnregisterRenderPipeline_Injected(ref ManagedSpanWrapper renderpipelineName);

		// Token: 0x06002E21 RID: 11809
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetSettingsForRenderPipeline_Injected(ref ManagedSpanWrapper renderpipelineName);

		// Token: 0x06002E22 RID: 11810
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_INTERNAL_currentRenderPipelineGlobalSettings_Injected();

		// Token: 0x06002E23 RID: 11811
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_INTERNAL_currentRenderPipelineGlobalSettings_Injected(IntPtr value);

		// Token: 0x04000EB6 RID: 3766
		private static Lazy<RenderPipelineGlobalSettings> s_CurrentRenderPipelineGlobalSettings = new Lazy<RenderPipelineGlobalSettings>(new Func<RenderPipelineGlobalSettings>(GraphicsSettings.Internal_GetCurrentRenderPipelineGlobalSettings));
	}
}
