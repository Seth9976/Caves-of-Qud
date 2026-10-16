using System;
using Uniject;
using Unity.Services.Core.Telemetry.Internal;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000037 RID: 55
	internal class TelemetryDiagnosticsInstanceWrapper : ITelemetryDiagnosticsInstanceWrapper
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00003B30 File Offset: 0x00001D30
		public TelemetryDiagnosticsInstanceWrapper(ILogger logger, IUtil util)
		{
			this.m_Logger = logger;
			this.m_Util = util;
			this.m_Queue = new TelemetryQueue<TelemetryDiagnosticParams>(new Action<TelemetryDiagnosticParams>(this.SendDiagnosticOnMainThread));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003B5D File Offset: 0x00001D5D
		public void SetDiagnosticsInstance(IDiagnostics diagnosticsInstance)
		{
			this.m_Instance = diagnosticsInstance;
			this.m_Queue.SendQueuedEvents();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003B74 File Offset: 0x00001D74
		public void SendDiagnostic(string diagnosticName, string diagnosticException)
		{
			TelemetryDiagnosticParams telemetryDiagnosticParams = new TelemetryDiagnosticParams(diagnosticName, diagnosticException);
			if (this.m_Instance != null)
			{
				this.SendDiagnosticOnMainThread(telemetryDiagnosticParams);
				return;
			}
			this.m_Queue.QueueEvent(telemetryDiagnosticParams);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003BA8 File Offset: 0x00001DA8
		private void SendDiagnosticOnMainThread(TelemetryDiagnosticParams diagnosticParams)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.SendDiagnosticAndCatchExceptions(diagnosticParams);
			});
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private void SendDiagnosticAndCatchExceptions(TelemetryDiagnosticParams diagnosticParams)
		{
			try
			{
				this.m_Instance.SendDiagnostic(diagnosticParams.name, diagnosticParams.exception, null);
			}
			catch (Exception ex)
			{
				this.m_Logger.LogIAPError("An exception occurred when sending diagnostic " + diagnosticParams.name + ". Message: " + ex.Message);
			}
		}

		// Token: 0x04000075 RID: 117
		private IDiagnostics m_Instance;

		// Token: 0x04000076 RID: 118
		private ILogger m_Logger;

		// Token: 0x04000077 RID: 119
		private IUtil m_Util;

		// Token: 0x04000078 RID: 120
		private readonly TelemetryQueue<TelemetryDiagnosticParams> m_Queue;
	}
}
