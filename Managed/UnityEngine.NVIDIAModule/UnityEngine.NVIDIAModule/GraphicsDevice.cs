using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000014 RID: 20
	public class GraphicsDevice
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000029E9 File Offset: 0x00000BE9
		private GraphicsDevice(string projectId, string engineVersion, string appDir)
		{
			this.m_InitDeviceContext = new InitDeviceContext(projectId, engineVersion, appDir);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A14 File Offset: 0x00000C14
		private bool Initialize()
		{
			return GraphicsDevice.NVUP_InitApi(this.m_InitDeviceContext.GetInitCmdPtr());
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002A36 File Offset: 0x00000C36
		private void Shutdown()
		{
			GraphicsDevice.NVUP_ShutdownApi();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002A40 File Offset: 0x00000C40
		~GraphicsDevice()
		{
			this.Shutdown();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002A70 File Offset: 0x00000C70
		private void InsertEventCall(CommandBuffer cmd, PluginEvent pluginEvent, IntPtr ptr)
		{
			cmd.IssuePluginEventAndData(GraphicsDevice.NVUP_GetRenderEventCallback(), (int)(pluginEvent + GraphicsDevice.NVUP_GetBaseEventId()), ptr);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002A88 File Offset: 0x00000C88
		private static GraphicsDevice InternalCreate(string appIdOrProjectId, string engineVersion, string appDir)
		{
			bool flag = GraphicsDevice.sGraphicsDeviceInstance != null;
			GraphicsDevice graphicsDevice;
			if (flag)
			{
				GraphicsDevice.sGraphicsDeviceInstance.Shutdown();
				GraphicsDevice.sGraphicsDeviceInstance.Initialize();
				graphicsDevice = GraphicsDevice.sGraphicsDeviceInstance;
			}
			else
			{
				GraphicsDevice graphicsDevice2 = new GraphicsDevice(appIdOrProjectId, engineVersion, appDir);
				bool flag2 = graphicsDevice2.Initialize();
				if (flag2)
				{
					GraphicsDevice.sGraphicsDeviceInstance = graphicsDevice2;
					graphicsDevice = graphicsDevice2;
				}
				else
				{
					graphicsDevice = null;
				}
			}
			return graphicsDevice;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002AE4 File Offset: 0x00000CE4
		private static int CreateSetTextureUserData(int featureId, int textureSlot, bool clearTextureTable)
		{
			int num = featureId & 65535;
			int num2 = textureSlot & 32767;
			int num3 = (clearTextureTable ? 1 : 0);
			return (num << 16) | (num2 << 1) | num3;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002B18 File Offset: 0x00000D18
		private void SetTexture(CommandBuffer cmd, DLSSContext dlssContext, DLSSCommandExecutionData.Textures textureSlot, Texture texture, bool clearTextureTable = false)
		{
			bool flag = texture == null;
			if (!flag)
			{
				uint num = (uint)GraphicsDevice.CreateSetTextureUserData((int)dlssContext.featureSlot, (int)textureSlot, clearTextureTable);
				cmd.IssuePluginCustomTextureUpdateV2(GraphicsDevice.NVUP_GetSetTextureEventCallback(), texture, num);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002B54 File Offset: 0x00000D54
		internal GraphicsDeviceDebugInfo GetDebugInfo(uint debugViewId)
		{
			GraphicsDeviceDebugInfo graphicsDeviceDebugInfo = default(GraphicsDeviceDebugInfo);
			GraphicsDevice.NVUP_GetGraphicsDeviceDebugInfo(debugViewId, out graphicsDeviceDebugInfo);
			return graphicsDeviceDebugInfo;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B78 File Offset: 0x00000D78
		internal uint CreateDebugViewId()
		{
			return GraphicsDevice.NVUP_CreateDebugView();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B8F File Offset: 0x00000D8F
		internal void DeleteDebugViewId(uint debugViewId)
		{
			GraphicsDevice.NVUP_DeleteDebugView(debugViewId);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B9C File Offset: 0x00000D9C
		public static GraphicsDevice CreateGraphicsDevice()
		{
			return GraphicsDevice.InternalCreate(GraphicsDevice.s_DefaultProjectID, Application.unityVersion, GraphicsDevice.s_DefaultAppDir);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static GraphicsDevice CreateGraphicsDevice(string projectID)
		{
			return GraphicsDevice.InternalCreate(projectID, Application.unityVersion, GraphicsDevice.s_DefaultAppDir);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public static GraphicsDevice CreateGraphicsDevice(string projectID, string appDir)
		{
			return GraphicsDevice.InternalCreate(projectID, Application.unityVersion, appDir);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002C08 File Offset: 0x00000E08
		public static GraphicsDevice device
		{
			get
			{
				return GraphicsDevice.sGraphicsDeviceInstance;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002C20 File Offset: 0x00000E20
		public static uint version
		{
			get
			{
				return GraphicsDevice.NVUP_GetDeviceVersion();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C38 File Offset: 0x00000E38
		public bool IsFeatureAvailable(GraphicsDeviceFeature featureID)
		{
			return GraphicsDevice.NVUP_IsFeatureAvailable(featureID);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002C50 File Offset: 0x00000E50
		public DLSSContext CreateFeature(CommandBuffer cmd, in DLSSCommandInitializationData initSettings)
		{
			bool flag = !this.IsFeatureAvailable(GraphicsDeviceFeature.DLSS);
			DLSSContext dlsscontext;
			if (flag)
			{
				dlsscontext = null;
			}
			else
			{
				bool flag2 = this.s_ContextObjectPool.Count == 0;
				DLSSContext dlsscontext2;
				if (flag2)
				{
					dlsscontext2 = new DLSSContext();
				}
				else
				{
					dlsscontext2 = this.s_ContextObjectPool.Pop();
				}
				dlsscontext2.Init(initSettings, GraphicsDevice.NVUP_CreateFeatureSlot());
				this.InsertEventCall(cmd, PluginEvent.DLSSInit, dlsscontext2.GetInitCmdPtr());
				dlsscontext = dlsscontext2;
			}
			return dlsscontext;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CC2 File Offset: 0x00000EC2
		public void DestroyFeature(CommandBuffer cmd, DLSSContext dlssContext)
		{
			this.InsertEventCall(cmd, PluginEvent.DestroyFeature, new IntPtr((long)((ulong)dlssContext.featureSlot)));
			dlssContext.Reset();
			this.s_ContextObjectPool.Push(dlssContext);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public void ExecuteDLSS(CommandBuffer cmd, DLSSContext dlssContext, in DLSSTextureTable textures)
		{
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.ColorInput, textures.colorInput, true);
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.ColorOutput, textures.colorOutput, false);
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.Depth, textures.depth, false);
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.MotionVectors, textures.motionVectors, false);
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.TransparencyMask, textures.transparencyMask, false);
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.ExposureTexture, textures.exposureTexture, false);
			this.SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.BiasColorMask, textures.biasColorMask, false);
			this.InsertEventCall(cmd, PluginEvent.DLSSExecute, dlssContext.GetExecuteCmdPtr());
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002D84 File Offset: 0x00000F84
		public bool GetOptimalSettings(uint targetWidth, uint targetHeight, DLSSQuality quality, out OptimalDLSSSettingsData optimalSettings)
		{
			return GraphicsDevice.NVUP_GetOptimalSettings(targetWidth, targetHeight, quality, out optimalSettings);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public GraphicsDeviceDebugView CreateDebugView()
		{
			return new GraphicsDeviceDebugView(this.CreateDebugViewId());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public unsafe void UpdateDebugView(GraphicsDeviceDebugView debugView)
		{
			bool flag = debugView == null;
			if (!flag)
			{
				GraphicsDeviceDebugInfo debugInfo = this.GetDebugInfo(debugView.m_ViewId);
				debugView.m_DeviceVersion = debugInfo.NVDeviceVersion;
				debugView.m_NgxVersion = debugInfo.NGXVersion;
				bool flag2 = debugView.m_DlssDebugFeatures == null || (ulong)debugInfo.dlssInfosCount != (ulong)((long)debugView.m_DlssDebugFeatures.Length);
				if (flag2)
				{
					debugView.m_DlssDebugFeatures = new DLSSDebugFeatureInfos[debugInfo.dlssInfosCount];
				}
				int num = 0;
				while ((long)num < (long)((ulong)debugInfo.dlssInfosCount))
				{
					debugView.m_DlssDebugFeatures[num] = debugInfo.dlssInfos[num];
					num++;
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E74 File Offset: 0x00001074
		public void DeleteDebugView(GraphicsDeviceDebugView debugView)
		{
			bool flag = debugView == null;
			if (!flag)
			{
				this.DeleteDebugViewId(debugView.m_ViewId);
			}
		}

		// Token: 0x0600007F RID: 127
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern bool NVUP_InitApi(IntPtr initData);

		// Token: 0x06000080 RID: 128
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern void NVUP_ShutdownApi();

		// Token: 0x06000081 RID: 129
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern bool NVUP_IsFeatureAvailable(GraphicsDeviceFeature featureID);

		// Token: 0x06000082 RID: 130
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern bool NVUP_GetOptimalSettings(uint inTargetWidth, uint inTargetHeight, DLSSQuality inPerfVQuality, out OptimalDLSSSettingsData data);

		// Token: 0x06000083 RID: 131
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern IntPtr NVUP_GetRenderEventCallback();

		// Token: 0x06000084 RID: 132
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern IntPtr NVUP_GetSetTextureEventCallback();

		// Token: 0x06000085 RID: 133
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern uint NVUP_CreateFeatureSlot();

		// Token: 0x06000086 RID: 134
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern uint NVUP_GetDeviceVersion();

		// Token: 0x06000087 RID: 135
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern uint NVUP_CreateDebugView();

		// Token: 0x06000088 RID: 136
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern void NVUP_GetGraphicsDeviceDebugInfo(uint debugViewId, out GraphicsDeviceDebugInfo data);

		// Token: 0x06000089 RID: 137
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern void NVUP_DeleteDebugView(uint debugViewId);

		// Token: 0x0600008A RID: 138
		[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern int NVUP_GetBaseEventId();

		// Token: 0x04000057 RID: 87
		private static string s_DefaultProjectID = "231313132";

		// Token: 0x04000058 RID: 88
		private static string s_DefaultAppDir = ".\\";

		// Token: 0x04000059 RID: 89
		private static GraphicsDevice sGraphicsDeviceInstance = null;

		// Token: 0x0400005A RID: 90
		private InitDeviceContext m_InitDeviceContext = null;

		// Token: 0x0400005B RID: 91
		private Stack<DLSSContext> s_ContextObjectPool = new Stack<DLSSContext>();
	}
}
