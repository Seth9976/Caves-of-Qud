using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core.Telemetry.Internal;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000053 RID: 83
	internal class CoreDiagnostics
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003AC1 File Offset: 0x00001CC1
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public static CoreDiagnostics Instance { get; internal set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public IDictionary<string, string> CoreTags { get; } = new Dictionary<string, string>();

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003AD8 File Offset: 0x00001CD8
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00003AE0 File Offset: 0x00001CE0
		internal IDiagnosticsComponentProvider DiagnosticsComponentProvider { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003AE9 File Offset: 0x00001CE9
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00003AF1 File Offset: 0x00001CF1
		internal IDiagnostics Diagnostics { get; set; }

		// Token: 0x06000167 RID: 359 RVA: 0x00003AFA File Offset: 0x00001CFA
		public void SetProjectConfiguration(string serializedProjectConfig)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003AFC File Offset: 0x00001CFC
		public void SendCircularDependencyDiagnostics(Exception exception)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003AFE File Offset: 0x00001CFE
		public void SendCorePackageInitDiagnostics(Exception exception)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003B00 File Offset: 0x00001D00
		public void SendOperateServicesInitDiagnostics(Exception exception)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003B04 File Offset: 0x00001D04
		internal async Task SendCoreDiagnosticsAsync(string diagnosticName, Exception exception)
		{
			await Task.CompletedTask;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003B3F File Offset: 0x00001D3F
		private static void OnSendFailed(Task failedSendTask)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003B44 File Offset: 0x00001D44
		internal async Task<IDiagnostics> GetOrCreateDiagnosticsAsync()
		{
			if (this.Diagnostics == null)
			{
				IDiagnosticsFactory diagnosticsFactory = await this.DiagnosticsComponentProvider.CreateDiagnosticsComponents();
				this.Diagnostics = diagnosticsFactory.Create("com.unity.services.core");
			}
			return this.Diagnostics;
		}

		// Token: 0x0400005B RID: 91
		internal const string CorePackageName = "com.unity.services.core";

		// Token: 0x0400005C RID: 92
		internal const string CircularDependencyDiagnosticName = "circular_dependency";

		// Token: 0x0400005D RID: 93
		internal const string CorePackageInitDiagnosticName = "core_package_init";

		// Token: 0x0400005E RID: 94
		internal const string OperateServicesInitDiagnosticName = "operate_services_init";

		// Token: 0x0400005F RID: 95
		internal const string ProjectConfigTagName = "project_config";
	}
}
