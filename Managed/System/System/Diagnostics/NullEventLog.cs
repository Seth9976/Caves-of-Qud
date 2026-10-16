using System;

namespace System.Diagnostics
{
	// Token: 0x02000271 RID: 625
	internal class NullEventLog : EventLogImpl
	{
		// Token: 0x060013AB RID: 5035 RVA: 0x000518B0 File Offset: 0x0004FAB0
		public NullEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00003917 File Offset: 0x00001B17
		public override void BeginInit()
		{
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Clear()
		{
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Close()
		{
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00003917 File Offset: 0x00001B17
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Delete(string logName, string machineName)
		{
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00003917 File Offset: 0x00001B17
		public override void DeleteEventSource(string source, string machineName)
		{
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Dispose(bool disposing)
		{
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00003917 File Offset: 0x00001B17
		public override void DisableNotification()
		{
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EnableNotification()
		{
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EndInit()
		{
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool Exists(string logName, string machineName)
		{
			return true;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00051AD2 File Offset: 0x0004FCD2
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00003062 File Offset: 0x00001262
		protected override int GetEntryCount()
		{
			return 0;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00002F6A File Offset: 0x0000116A
		protected override EventLogEntry GetEntry(int index)
		{
			return null;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00051CEC File Offset: 0x0004FEEC
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x000522EC File Offset: 0x000504EC
		protected override string[] GetLogNames(string machineName)
		{
			return new string[0];
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00002F6A File Offset: 0x0000116A
		public override string LogNameFromSourceName(string source, string machineName)
		{
			return null;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00003062 File Offset: 0x00001262
		public override bool SourceExists(string source, string machineName)
		{
			return false;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00003917 File Offset: 0x00001B17
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000521D0 File Offset: 0x000503D0
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x000521D3 File Offset: 0x000503D3
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000521DA File Offset: 0x000503DA
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x000521E5 File Offset: 0x000503E5
		public override long MaximumKilobytes
		{
			get
			{
				return long.MaxValue;
			}
			set
			{
				throw new NotSupportedException("This EventLog implementation does not support setting max kilobytes policy");
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000521F1 File Offset: 0x000503F1
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000521FD File Offset: 0x000503FD
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}
	}
}
