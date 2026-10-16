using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;

namespace UnityEngine.AMD
{
	// Token: 0x0200000C RID: 12
	public class GraphicsDevice
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002337 File Offset: 0x00000537
		private GraphicsDevice()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000234C File Offset: 0x0000054C
		private bool Initialize()
		{
			return GraphicsDevice.AMDUP_InitApi();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002363 File Offset: 0x00000563
		private void Shutdown()
		{
			GraphicsDevice.AMDUP_ShutdownApi();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000236C File Offset: 0x0000056C
		~GraphicsDevice()
		{
			this.Shutdown();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000239C File Offset: 0x0000059C
		private void InsertEventCall(CommandBuffer cmd, PluginEvent pluginEvent, IntPtr ptr)
		{
			cmd.IssuePluginEventAndData(GraphicsDevice.AMDUP_GetRenderEventCallback(), (int)(pluginEvent + GraphicsDevice.AMDUP_GetBaseEventId()), ptr);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023B4 File Offset: 0x000005B4
		private static GraphicsDevice InternalCreate()
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
				GraphicsDevice graphicsDevice2 = new GraphicsDevice();
				bool flag2 = graphicsDevice2.Initialize();
				if (flag2)
				{
					GraphicsDevice.sGraphicsDeviceInstance = graphicsDevice2;
					graphicsDevice = graphicsDevice2;
				}
				else
				{
					Debug.LogWarning("Unity has an invalid api for dvice. Init failed[");
					graphicsDevice = null;
				}
			}
			return graphicsDevice;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002418 File Offset: 0x00000618
		private static int CreateSetTextureUserData(int featureId, int textureSlot, bool clearTextureTable)
		{
			int num = featureId & 65535;
			int num2 = textureSlot & 32767;
			int num3 = (clearTextureTable ? 1 : 0);
			return (num << 16) | (num2 << 1) | num3;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000244C File Offset: 0x0000064C
		private void SetTexture(CommandBuffer cmd, FSR2Context fsr2Context, FSR2CommandExecutionData.Textures textureSlot, Texture texture, bool clearTextureTable = false)
		{
			bool flag = texture == null;
			if (!flag)
			{
				uint num = (uint)GraphicsDevice.CreateSetTextureUserData((int)fsr2Context.featureSlot, (int)textureSlot, clearTextureTable);
				cmd.IssuePluginCustomTextureUpdateV2(GraphicsDevice.AMDUP_GetSetTextureEventCallback(), texture, num);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002488 File Offset: 0x00000688
		public static GraphicsDevice CreateGraphicsDevice()
		{
			return GraphicsDevice.InternalCreate();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024A0 File Offset: 0x000006A0
		public static GraphicsDevice device
		{
			get
			{
				return GraphicsDevice.sGraphicsDeviceInstance;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000024B8 File Offset: 0x000006B8
		public static uint version
		{
			get
			{
				return GraphicsDevice.AMDUP_GetDeviceVersion();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024D0 File Offset: 0x000006D0
		public FSR2Context CreateFeature(CommandBuffer cmd, in FSR2CommandInitializationData initSettings)
		{
			bool flag = this.s_ContextObjectPool.Count == 0;
			FSR2Context fsr2Context;
			if (flag)
			{
				fsr2Context = new FSR2Context();
			}
			else
			{
				fsr2Context = this.s_ContextObjectPool.Pop();
			}
			fsr2Context.Init(initSettings, GraphicsDevice.AMDUP_CreateFeatureSlot());
			this.InsertEventCall(cmd, PluginEvent.FSR2Init, fsr2Context.GetInitCmdPtr());
			return fsr2Context;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002530 File Offset: 0x00000730
		public bool GetRenderResolutionFromQualityMode(FSR2Quality qualityMode, uint displayWidth, uint displayHeight, out uint renderWidth, out uint renderHeight)
		{
			return GraphicsDevice.AMDUP_GetRenderResolutionFromQualityMode(qualityMode, displayWidth, displayHeight, out renderWidth, out renderHeight);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002550 File Offset: 0x00000750
		public float GetUpscaleRatioFromQualityMode(FSR2Quality qualityMode)
		{
			return GraphicsDevice.AMDUP_GetUpscaleRatioFromQualityMode(qualityMode);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002568 File Offset: 0x00000768
		public void DestroyFeature(CommandBuffer cmd, FSR2Context fsrContext)
		{
			this.InsertEventCall(cmd, PluginEvent.DestroyFeature, new IntPtr((long)((ulong)fsrContext.featureSlot)));
			fsrContext.Reset();
			this.s_ContextObjectPool.Push(fsrContext);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002594 File Offset: 0x00000794
		public void ExecuteFSR2(CommandBuffer cmd, FSR2Context fsr2Context, in FSR2TextureTable textures)
		{
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.ColorInput, textures.colorInput, true);
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.ColorOutput, textures.colorOutput, false);
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.Depth, textures.depth, false);
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.MotionVectors, textures.motionVectors, false);
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.TransparencyMask, textures.transparencyMask, false);
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.ExposureTexture, textures.exposureTexture, false);
			this.SetTexture(cmd, fsr2Context, FSR2CommandExecutionData.Textures.BiasColorMask, textures.biasColorMask, false);
			this.InsertEventCall(cmd, PluginEvent.FSR2Execute, fsr2Context.GetExecuteCmdPtr());
			this.InsertEventCall(cmd, PluginEvent.FSR2PostExecute, fsr2Context.GetExecuteCmdPtr());
		}

		// Token: 0x06000032 RID: 50
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern bool AMDUP_InitApi();

		// Token: 0x06000033 RID: 51
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern void AMDUP_ShutdownApi();

		// Token: 0x06000034 RID: 52
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern uint AMDUP_GetDeviceVersion();

		// Token: 0x06000035 RID: 53
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern IntPtr AMDUP_GetRenderEventCallback();

		// Token: 0x06000036 RID: 54
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern IntPtr AMDUP_GetSetTextureEventCallback();

		// Token: 0x06000037 RID: 55
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern uint AMDUP_CreateFeatureSlot();

		// Token: 0x06000038 RID: 56
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern bool AMDUP_GetRenderResolutionFromQualityMode(FSR2Quality qualityMode, uint displayWidth, uint displayHeight, out uint renderWidth, out uint renderHeight);

		// Token: 0x06000039 RID: 57
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern float AMDUP_GetUpscaleRatioFromQualityMode(FSR2Quality qualityMode);

		// Token: 0x0600003A RID: 58
		[DllImport("AMDUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		private static extern int AMDUP_GetBaseEventId();

		// Token: 0x0400003E RID: 62
		private static GraphicsDevice sGraphicsDeviceInstance;

		// Token: 0x0400003F RID: 63
		private Stack<FSR2Context> s_ContextObjectPool = new Stack<FSR2Context>();
	}
}
