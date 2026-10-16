using System;
using System.Runtime.Versioning;

namespace System.Web.Util
{
	// Token: 0x0200010C RID: 268
	internal sealed class BinaryCompatibility
	{
		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002520C File Offset: 0x0002340C
		static BinaryCompatibility()
		{
			TelemetryLogger.LogTargetFramework(BinaryCompatibility.Current.TargetFramework);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002523C File Offset: 0x0002343C
		public BinaryCompatibility(FrameworkName frameworkName)
		{
			Version version = VersionUtil.FrameworkDefault;
			if (frameworkName != null && frameworkName.Identifier == ".NETFramework")
			{
				version = frameworkName.Version;
			}
			this.TargetFramework = version;
			this.TargetsAtLeastFramework45 = version >= VersionUtil.Framework45;
			this.TargetsAtLeastFramework451 = version >= VersionUtil.Framework451;
			this.TargetsAtLeastFramework452 = version >= VersionUtil.Framework452;
			this.TargetsAtLeastFramework46 = version >= VersionUtil.Framework46;
			this.TargetsAtLeastFramework461 = version >= VersionUtil.Framework461;
			this.TargetsAtLeastFramework463 = version >= VersionUtil.Framework463;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000252E4 File Offset: 0x000234E4
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x000252EC File Offset: 0x000234EC
		public bool TargetsAtLeastFramework45 { get; private set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000252F5 File Offset: 0x000234F5
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x000252FD File Offset: 0x000234FD
		public bool TargetsAtLeastFramework451 { get; private set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00025306 File Offset: 0x00023506
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0002530E File Offset: 0x0002350E
		public bool TargetsAtLeastFramework452 { get; private set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00025317 File Offset: 0x00023517
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0002531F File Offset: 0x0002351F
		public bool TargetsAtLeastFramework46 { get; private set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00025328 File Offset: 0x00023528
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x00025330 File Offset: 0x00023530
		public bool TargetsAtLeastFramework461 { get; private set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00025339 File Offset: 0x00023539
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00025341 File Offset: 0x00023541
		public bool TargetsAtLeastFramework463 { get; private set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002534A File Offset: 0x0002354A
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00025352 File Offset: 0x00023552
		public Version TargetFramework { get; private set; }

		// Token: 0x0400118E RID: 4494
		internal const string TargetFrameworkKey = "ASPNET_TARGETFRAMEWORK";

		// Token: 0x0400118F RID: 4495
		public static readonly BinaryCompatibility Current = new BinaryCompatibility(AppDomain.CurrentDomain.GetData("ASPNET_TARGETFRAMEWORK") as FrameworkName);
	}
}
