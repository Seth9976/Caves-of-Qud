using System;
using System.Collections.Generic;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Telemetry.Internal;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000054 RID: 84
	internal class CoreMetrics
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003B9A File Offset: 0x00001D9A
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public static CoreMetrics Instance { get; internal set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003BA9 File Offset: 0x00001DA9
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00003BB1 File Offset: 0x00001DB1
		internal IMetrics Metrics { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003BBA File Offset: 0x00001DBA
		internal IDictionary<Type, IMetrics> AllPackageMetrics { get; } = new Dictionary<Type, IMetrics>();

		// Token: 0x06000174 RID: 372 RVA: 0x00003BC2 File Offset: 0x00001DC2
		public void SendAllPackagesInitSuccessMetric()
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public void SendAllPackagesInitTimeMetric(double initTimeSeconds)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003BC6 File Offset: 0x00001DC6
		public void SendInitTimeMetricForPackage(Type packageType, double initTimeSeconds)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public void Initialize(IProjectConfiguration configuration, IMetricsFactory factory, Type corePackageType)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003BCA File Offset: 0x00001DCA
		internal void FindAndCacheAllPackageMetrics(IProjectConfiguration configuration, IMetricsFactory factory)
		{
		}

		// Token: 0x04000064 RID: 100
		internal const string PackageInitTimeMetricName = "package_init_time";

		// Token: 0x04000065 RID: 101
		internal const string AllPackagesInitSuccessMetricName = "all_packages_init_success";

		// Token: 0x04000066 RID: 102
		internal const string AllPackagesInitTimeMetricName = "all_packages_init_time";

		// Token: 0x04000067 RID: 103
		internal const string PackageInitializerNamesKeyFormat = "{0}.initializer-assembly-qualified-names";

		// Token: 0x04000068 RID: 104
		internal const char PackageInitializerNamesSeparator = ';';

		// Token: 0x04000069 RID: 105
		internal const string AllPackageNamesKey = "com.unity.services.core.all-package-names";

		// Token: 0x0400006A RID: 106
		internal const char AllPackageNamesSeparator = ';';
	}
}
