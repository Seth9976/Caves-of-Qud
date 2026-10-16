using System;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace PlayFab
{
	// Token: 0x02000025 RID: 37
	public class PlayFabDataGatherer
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public PlayFabDataGatherer()
		{
			this.ProductName = Application.productName;
			this.Version = Application.version;
			this.Company = Application.companyName;
			this.Platform = Application.platform;
			this.GraphicsMultiThreaded = SystemInfo.graphicsMultiThreaded;
			this.GraphicsType = SystemInfo.graphicsDeviceType;
			this.DataPath = Application.dataPath;
			this.PersistentDataPath = Application.persistentDataPath;
			this.StreamingAssetsPath = Application.streamingAssetsPath;
			this.TargetFrameRate = Application.targetFrameRate;
			this.UnityVersion = Application.unityVersion;
			this.DeviceModel = SystemInfo.deviceModel;
			this.DeviceType = SystemInfo.deviceType;
			this.DeviceUniqueId = PlayFabSettings.DeviceUniqueIdentifier;
			this.OperatingSystem = SystemInfo.operatingSystem;
			this.GraphicsDeviceId = SystemInfo.graphicsDeviceID;
			this.GraphicsDeviceName = SystemInfo.graphicsDeviceName;
			this.GraphicsMemorySize = SystemInfo.graphicsMemorySize;
			this.GraphicsShaderLevel = SystemInfo.graphicsShaderLevel;
			this.SystemMemorySize = SystemInfo.systemMemorySize;
			this.ProcessorCount = SystemInfo.processorCount;
			this.ProcessorFrequency = SystemInfo.processorFrequency;
			this.ProcessorType = SystemInfo.processorType;
			this.SupportsAccelerometer = SystemInfo.supportsAccelerometer;
			this.SupportsGyroscope = SystemInfo.supportsGyroscope;
			this.SupportsLocationService = SystemInfo.supportsLocationService;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000FF14 File Offset: 0x0000E114
		public string GenerateReport()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Logging System Info: ========================================\n");
			foreach (FieldInfo fieldInfo in base.GetType().GetTypeInfo().GetFields())
			{
				string text = fieldInfo.GetValue(this).ToString();
				stringBuilder.AppendFormat("System Info - {0}: {1}\n", fieldInfo.Name, text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000293 RID: 659
		public string ProductName;

		// Token: 0x04000294 RID: 660
		public string ProductBundle;

		// Token: 0x04000295 RID: 661
		public string Version;

		// Token: 0x04000296 RID: 662
		public string Company;

		// Token: 0x04000297 RID: 663
		public RuntimePlatform Platform;

		// Token: 0x04000298 RID: 664
		public bool GraphicsMultiThreaded;

		// Token: 0x04000299 RID: 665
		public GraphicsDeviceType GraphicsType;

		// Token: 0x0400029A RID: 666
		public string DataPath;

		// Token: 0x0400029B RID: 667
		public string PersistentDataPath;

		// Token: 0x0400029C RID: 668
		public string StreamingAssetsPath;

		// Token: 0x0400029D RID: 669
		public int TargetFrameRate;

		// Token: 0x0400029E RID: 670
		public string UnityVersion;

		// Token: 0x0400029F RID: 671
		public bool RunInBackground;

		// Token: 0x040002A0 RID: 672
		public string DeviceModel;

		// Token: 0x040002A1 RID: 673
		public DeviceType DeviceType;

		// Token: 0x040002A2 RID: 674
		public string DeviceUniqueId;

		// Token: 0x040002A3 RID: 675
		public string OperatingSystem;

		// Token: 0x040002A4 RID: 676
		public int GraphicsDeviceId;

		// Token: 0x040002A5 RID: 677
		public string GraphicsDeviceName;

		// Token: 0x040002A6 RID: 678
		public int GraphicsMemorySize;

		// Token: 0x040002A7 RID: 679
		public int GraphicsShaderLevel;

		// Token: 0x040002A8 RID: 680
		public int SystemMemorySize;

		// Token: 0x040002A9 RID: 681
		public int ProcessorCount;

		// Token: 0x040002AA RID: 682
		public int ProcessorFrequency;

		// Token: 0x040002AB RID: 683
		public string ProcessorType;

		// Token: 0x040002AC RID: 684
		public bool SupportsAccelerometer;

		// Token: 0x040002AD RID: 685
		public bool SupportsGyroscope;

		// Token: 0x040002AE RID: 686
		public bool SupportsLocationService;
	}
}
