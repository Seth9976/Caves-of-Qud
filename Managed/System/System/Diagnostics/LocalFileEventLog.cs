using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200026F RID: 623
	internal class LocalFileEventLog : EventLogImpl
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x000518B0 File Offset: 0x0004FAB0
		public LocalFileEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00003917 File Offset: 0x00001B17
		public override void BeginInit()
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000518BC File Offset: 0x0004FABC
		public override void Clear()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return;
			}
			string[] files = Directory.GetFiles(text, "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00051906 File Offset: 0x0004FB06
		public override void Close()
		{
			if (this.file_watcher != null)
			{
				this.file_watcher.EnableRaisingEvents = false;
				this.file_watcher = null;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00051924 File Offset: 0x0004FB24
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			string text = this.FindLogStore(sourceData.LogName);
			if (!Directory.Exists(text))
			{
				base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
				Directory.CreateDirectory(text);
				Directory.CreateDirectory(Path.Combine(text, sourceData.LogName));
				if (this.RunningOnUnix)
				{
					LocalFileEventLog.ModifyAccessPermissions(text, "777");
					LocalFileEventLog.ModifyAccessPermissions(text, "+t");
				}
			}
			Directory.CreateDirectory(Path.Combine(text, sourceData.Source));
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000519A1 File Offset: 0x0004FBA1
		public override void Delete(string logName, string machineName)
		{
			string text = this.FindLogStore(logName);
			if (!Directory.Exists(text))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", logName, machineName));
			}
			Directory.Delete(text, true);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000519D0 File Offset: 0x0004FBD0
		public override void DeleteEventSource(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
			}
			Directory.Delete(text);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00051A27 File Offset: 0x0004FC27
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00051A2F File Offset: 0x0004FC2F
		public override void DisableNotification()
		{
			if (this.file_watcher == null)
			{
				return;
			}
			this.file_watcher.EnableRaisingEvents = false;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00051A48 File Offset: 0x0004FC48
		public override void EnableNotification()
		{
			if (this.file_watcher == null)
			{
				string text = this.FindLogStore(base.CoreEventLog.Log);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				this.file_watcher = new FileSystemWatcher();
				this.file_watcher.Path = text;
				this.file_watcher.Created += delegate(object o, FileSystemEventArgs e)
				{
					LocalFileEventLog localFileEventLog = this;
					lock (localFileEventLog)
					{
						if (this._notifying)
						{
							return;
						}
						this._notifying = true;
					}
					Thread.Sleep(100);
					try
					{
						while (this.GetLatestIndex() > this.last_notification_index)
						{
							try
							{
								EventLog coreEventLog = base.CoreEventLog;
								int num = this.last_notification_index;
								this.last_notification_index = num + 1;
								coreEventLog.OnEntryWritten(this.GetEntry(num));
							}
							catch (Exception)
							{
							}
						}
					}
					finally
					{
						localFileEventLog = this;
						lock (localFileEventLog)
						{
							this._notifying = false;
						}
					}
				};
			}
			this.last_notification_index = this.GetLatestIndex();
			this.file_watcher.EnableRaisingEvents = true;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EndInit()
		{
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00051AC4 File Offset: 0x0004FCC4
		public override bool Exists(string logName, string machineName)
		{
			return Directory.Exists(this.FindLogStore(logName));
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00051AD2 File Offset: 0x0004FCD2
		[MonoTODO("Use MessageTable from PE for lookup")]
		protected override string FormatMessage(string source, uint eventID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00051AE0 File Offset: 0x0004FCE0
		protected override int GetEntryCount()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return 0;
			}
			return Directory.GetFiles(text, "*.log").Length;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00051B18 File Offset: 0x0004FD18
		protected override EventLogEntry GetEntry(int index)
		{
			string text = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (index + 1).ToString(CultureInfo.InvariantCulture) + ".log");
			EventLogEntry eventLogEntry;
			using (TextReader textReader = File.OpenText(text))
			{
				int num = int.Parse(Path.GetFileNameWithoutExtension(text), CultureInfo.InvariantCulture);
				uint num2 = uint.Parse(textReader.ReadLine().Substring(12), CultureInfo.InvariantCulture);
				EventLogEntryType eventLogEntryType = (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType), textReader.ReadLine().Substring(11));
				string text2 = textReader.ReadLine().Substring(8);
				string text3 = textReader.ReadLine().Substring(10);
				short num3 = short.Parse(text3, CultureInfo.InvariantCulture);
				string text4 = "(" + text3 + ")";
				DateTime dateTime = DateTime.ParseExact(textReader.ReadLine().Substring(15), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
				DateTime lastWriteTime = File.GetLastWriteTime(text);
				int num4 = int.Parse(textReader.ReadLine().Substring(20));
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				while (list.Count < num4)
				{
					char c = (char)textReader.Read();
					if (c == '\0')
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string[] array = list.ToArray();
				string text5 = this.FormatMessage(text2, num2, array);
				int eventID = EventLog.GetEventID((long)((ulong)num2));
				byte[] array2 = Convert.FromBase64String(textReader.ReadToEnd());
				eventLogEntry = new EventLogEntry(text4, num3, num, eventID, text2, text5, null, Environment.MachineName, eventLogEntryType, dateTime, lastWriteTime, array2, array, (long)((ulong)num2));
			}
			return eventLogEntry;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00051CEC File Offset: 0x0004FEEC
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00051CFC File Offset: 0x0004FEFC
		protected override string[] GetLogNames(string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return new string[0];
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				array[i] = Path.GetFileName(directories[i]);
			}
			return array;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00051D54 File Offset: 0x0004FF54
		public override string LogNameFromSourceName(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return string.Empty;
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				return string.Empty;
			}
			return new DirectoryInfo(text).Parent.Name;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00051D95 File Offset: 0x0004FF95
		public override bool SourceExists(string source, string machineName)
		{
			return Directory.Exists(this.EventLogStore) && this.FindSourceDirectory(source) != null;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00051DB0 File Offset: 0x0004FFB0
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			object obj = LocalFileEventLog.lockObject;
			lock (obj)
			{
				string text = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (this.GetLatestIndex() + 1).ToString(CultureInfo.InvariantCulture) + ".log");
				try
				{
					using (TextWriter textWriter = File.CreateText(text))
					{
						textWriter.WriteLine("InstanceID: {0}", instanceID.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("EntryType: {0}", (int)type);
						textWriter.WriteLine("Source: {0}", base.CoreEventLog.Source);
						textWriter.WriteLine("Category: {0}", category.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("TimeGenerated: {0}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
						textWriter.WriteLine("ReplacementStrings: {0}", replacementStrings.Length.ToString(CultureInfo.InvariantCulture));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string text2 in replacementStrings)
						{
							stringBuilder.Append(text2);
							stringBuilder.Append('\0');
						}
						textWriter.Write(stringBuilder.ToString());
						textWriter.Write(Convert.ToBase64String(rawData));
					}
				}
				catch (IOException)
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00051F64 File Offset: 0x00050164
		private string FindSourceDirectory(string source)
		{
			string text = null;
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string[] directories2 = Directory.GetDirectories(directories[i], "*");
				for (int j = 0; j < directories2.Length; j++)
				{
					if (string.Compare(Path.GetFileName(directories2[j]), source, true, CultureInfo.InvariantCulture) == 0)
					{
						text = directories2[j];
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00051FD4 File Offset: 0x000501D4
		private bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00052000 File Offset: 0x00050200
		private string FindLogStore(string logName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return Path.Combine(this.EventLogStore, logName);
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				if (string.Compare(Path.GetFileName(directories[i]), logName, true, CultureInfo.InvariantCulture) == 0)
				{
					return directories[i];
				}
			}
			return Path.Combine(this.EventLogStore, logName);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0005206C File Offset: 0x0005026C
		private string EventLogStore
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (environmentVariable != null && environmentVariable.Length > "local".Length + 1)
				{
					return environmentVariable.Substring("local".Length + 1);
				}
				if (this.RunningOnUnix)
				{
					return "/var/lib/mono/eventlog";
				}
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mono\\eventlog");
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000520D0 File Offset: 0x000502D0
		private int GetLatestIndex()
		{
			int num = 0;
			string[] files = Directory.GetFiles(this.FindLogStore(base.CoreEventLog.Log), "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					int num2 = int.Parse(Path.GetFileNameWithoutExtension(files[i]), CultureInfo.InvariantCulture);
					if (num2 > num)
					{
						num = num2;
					}
				}
				catch
				{
				}
			}
			return num;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0005213C File Offset: 0x0005033C
		private static void ModifyAccessPermissions(string path, string permissions)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "chmod";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = string.Format("{0} \"{1}\"", permissions, path);
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				throw new SecurityException("Access permissions could not be modified.", ex);
			}
			process.WaitForExit();
			if (process.ExitCode != 0)
			{
				process.Close();
				throw new SecurityException("Access permissions could not be modified.");
			}
			process.Close();
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x000521D0 File Offset: 0x000503D0
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000521D3 File Offset: 0x000503D3
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x000521DA File Offset: 0x000503DA
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x000521E5 File Offset: 0x000503E5
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

		// Token: 0x060013A5 RID: 5029 RVA: 0x000521F1 File Offset: 0x000503F1
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000521FD File Offset: 0x000503FD
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}

		// Token: 0x04000B25 RID: 2853
		private const string DateFormat = "yyyyMMddHHmmssfff";

		// Token: 0x04000B26 RID: 2854
		private static readonly object lockObject = new object();

		// Token: 0x04000B27 RID: 2855
		private FileSystemWatcher file_watcher;

		// Token: 0x04000B28 RID: 2856
		private int last_notification_index;

		// Token: 0x04000B29 RID: 2857
		private bool _notifying;
	}
}
