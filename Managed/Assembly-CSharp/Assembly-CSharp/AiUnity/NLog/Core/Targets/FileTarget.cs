using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Internal.FileAppenders;
using AiUnity.NLog.Core.Layouts;
using UnityEngine;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200021A RID: 538
	[Target("File")]
	[Preserve]
	public class FileTarget : TargetWithLayoutHeaderAndFooter, ICreateFileParameters
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0007071C File Offset: 0x0006E91C
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00070724 File Offset: 0x0006E924
		public FileTarget()
		{
			this.ArchiveNumbering = ArchiveNumberingMode.Sequence;
			this._MaxArchiveFilesField = 9;
			this.ConcurrentWriteAttemptDelay = 1;
			this.ArchiveEvery = FileArchivePeriod.None;
			this.ArchiveAboveSize = -1L;
			this.ConcurrentWriteAttempts = 10;
			this.ConcurrentWrites = false;
			this.Encoding = Encoding.Default;
			this.BufferSize = 32768;
			this.AutoFlush = true;
			this.FileAttributes = Win32FileAttributes.Normal;
			this.NewLineChars = Environment.NewLine;
			this.EnableFileDelete = true;
			this.OpenFileCacheTimeout = -1;
			this.OpenFileCacheSize = 5;
			this.CreateDirs = true;
			this.dynamicArchiveFileHandler = new FileTarget.DynamicArchiveFileHandlerClass(this.MaxArchiveFiles);
			this.ForceManaged = false;
			this.ArchiveDateFormat = string.Empty;
			this.FileName = "unity_log.txt";
			this.KeepFileOpen = true;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000707FF File Offset: 0x0006E9FF
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x00070807 File Offset: 0x0006EA07
		[Display("File name", "Name of log file.", false, 0)]
		[DefaultValue("unity_log.txt")]
		[RequiredParameter]
		public Layout FileName { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00070810 File Offset: 0x0006EA10
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x00070818 File Offset: 0x0006EA18
		[DefaultValue(true)]
		public bool CreateDirs { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00070821 File Offset: 0x0006EA21
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x00070829 File Offset: 0x0006EA29
		[Display("Auto clear", "Clear existing log file on startup.  Note log file will also be clear if archiving occurs.", false, 0)]
		[DefaultValue(true)]
		public bool AutoClear { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00070832 File Offset: 0x0006EA32
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x0007083A File Offset: 0x0006EA3A
		[Display("Archive last", "Archive previous log file when new log created", true, 0)]
		[DefaultValue(false)]
		public bool ArchiveOldFileOnStartup { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00070843 File Offset: 0x0006EA43
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0007084B File Offset: 0x0006EA4B
		[DefaultValue(false)]
		public bool ReplaceFileContentsOnEachWrite { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00070854 File Offset: 0x0006EA54
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0007085C File Offset: 0x0006EA5C
		[DefaultValue(true)]
		public bool KeepFileOpen { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00070865 File Offset: 0x0006EA65
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x0007086D File Offset: 0x0006EA6D
		[DefaultValue(true)]
		public bool EnableFileDelete { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00070876 File Offset: 0x0006EA76
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0007087E File Offset: 0x0006EA7E
		[DefaultValue("")]
		public string ArchiveDateFormat { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00070887 File Offset: 0x0006EA87
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0007088F File Offset: 0x0006EA8F
		public Win32FileAttributes FileAttributes { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00070898 File Offset: 0x0006EA98
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x000708A0 File Offset: 0x0006EAA0
		public LineEndingMode LineEnding
		{
			get
			{
				return this.lineEndingMode;
			}
			set
			{
				this.lineEndingMode = value;
				switch (value)
				{
				case LineEndingMode.Default:
					this.NewLineChars = Environment.NewLine;
					return;
				case LineEndingMode.CRLF:
					this.NewLineChars = "\r\n";
					return;
				case LineEndingMode.CR:
					this.NewLineChars = "\r";
					return;
				case LineEndingMode.LF:
					this.NewLineChars = "\n";
					return;
				case LineEndingMode.None:
					this.NewLineChars = string.Empty;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0007090A File Offset: 0x0006EB0A
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x00070912 File Offset: 0x0006EB12
		[Display("Auto flush", "Flush file buffers after each log message", false, 0)]
		[DefaultValue(true)]
		public bool AutoFlush { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0007091B File Offset: 0x0006EB1B
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x00070923 File Offset: 0x0006EB23
		[DefaultValue(5)]
		public int OpenFileCacheSize { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0007092C File Offset: 0x0006EB2C
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00070934 File Offset: 0x0006EB34
		[DefaultValue(-1)]
		public int OpenFileCacheTimeout { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0007093D File Offset: 0x0006EB3D
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x00070945 File Offset: 0x0006EB45
		[DefaultValue(32768)]
		public int BufferSize { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0007094E File Offset: 0x0006EB4E
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x00070956 File Offset: 0x0006EB56
		public Encoding Encoding { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0007095F File Offset: 0x0006EB5F
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x00070967 File Offset: 0x0006EB67
		[DefaultValue(false)]
		public bool ConcurrentWrites { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00070970 File Offset: 0x0006EB70
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x00070978 File Offset: 0x0006EB78
		[DefaultValue(false)]
		public bool NetworkWrites { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00070981 File Offset: 0x0006EB81
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x00070989 File Offset: 0x0006EB89
		[DefaultValue(10)]
		public int ConcurrentWriteAttempts { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00070992 File Offset: 0x0006EB92
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x0007099A File Offset: 0x0006EB9A
		[DefaultValue(1)]
		public int ConcurrentWriteAttemptDelay { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000709A3 File Offset: 0x0006EBA3
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000709AB File Offset: 0x0006EBAB
		[Display("Archive trigger", "Archive file when size exceeds specified size in bytes.  Use -1 to disable functionality.", true, 0)]
		[DefaultValue(-1)]
		public long ArchiveAboveSize { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000709B4 File Offset: 0x0006EBB4
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x000709BC File Offset: 0x0006EBBC
		[Display("Archive interval", "Archive file on specified interval.", true, 0)]
		[DefaultValue(FileArchivePeriod.None)]
		public FileArchivePeriod ArchiveEvery { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x000709C5 File Offset: 0x0006EBC5
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x000709CD File Offset: 0x0006EBCD
		[Display("Archive name", "Name of archive file with placeholder {#} replaced by archive number sequence.", true, 0)]
		[DefaultValue("unity_log_archive_{#}.txt")]
		public Layout ArchiveFileName { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000709D6 File Offset: 0x0006EBD6
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x000709DE File Offset: 0x0006EBDE
		[Display("Max archives", "Maximum number of archive files before overwritting previous archives.", true, 0)]
		[DefaultValue(9)]
		public int MaxArchiveFiles
		{
			get
			{
				return this._MaxArchiveFilesField;
			}
			set
			{
				this._MaxArchiveFilesField = value;
				this.dynamicArchiveFileHandler.MaxArchiveFileToKeep = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000709F3 File Offset: 0x0006EBF3
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x000709FB File Offset: 0x0006EBFB
		[DefaultValue(false)]
		public bool ForceManaged { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00070A04 File Offset: 0x0006EC04
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00070A0C File Offset: 0x0006EC0C
		public ArchiveNumberingMode ArchiveNumbering { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00070A15 File Offset: 0x0006EC15
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00070A1D File Offset: 0x0006EC1D
		protected internal string NewLineChars { get; private set; }

		// Token: 0x060013B8 RID: 5048 RVA: 0x00070A28 File Offset: 0x0006EC28
		public void CleanupInitializedFiles()
		{
			this.CleanupInitializedFiles(DateTime.Now.AddDays(-2.0));
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00070A54 File Offset: 0x0006EC54
		public void CleanupInitializedFiles(DateTime cleanupThreshold)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, DateTime> keyValuePair in this.initializedFiles)
			{
				string key = keyValuePair.Key;
				if (keyValuePair.Value < cleanupThreshold)
				{
					list.Add(key);
				}
			}
			foreach (string text in list)
			{
				this.WriteFooterAndUninitialize(text);
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00070B04 File Offset: 0x0006ED04
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			try
			{
				foreach (BaseFileAppender baseFileAppender in this.recentAppenders)
				{
					if (baseFileAppender == null)
					{
						break;
					}
					baseFileAppender.Flush();
				}
				asyncContinuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				asyncContinuation(ex);
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00070B60 File Offset: 0x0006ED60
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (!this.KeepFileOpen)
			{
				throw new NotImplementedException("Feature removed for Unity");
			}
			if (this.ArchiveAboveSize != -1L || this.ArchiveEvery != FileArchivePeriod.None)
			{
				if (this.NetworkWrites)
				{
					throw new NotImplementedException("Feature removed for Unity");
				}
				if (this.ConcurrentWrites)
				{
					throw new NotImplementedException("Feature removed for Unity");
				}
				this.appenderFactory = CountingSingleProcessFileAppender.TheFactory;
			}
			else
			{
				if (this.NetworkWrites)
				{
					throw new NotImplementedException("Feature removed for Unity");
				}
				if (this.ConcurrentWrites)
				{
					throw new NotImplementedException("Feature removed for Unity");
				}
				this.appenderFactory = SingleProcessFileAppender.TheFactory;
			}
			this.recentAppenders = new BaseFileAppender[this.OpenFileCacheSize];
			if ((this.OpenFileCacheSize > 0 || this.EnableFileDelete) && this.OpenFileCacheTimeout > 0)
			{
				this.autoClosingTimer = new Timer(new TimerCallback(this.AutoClosingTimerCallback), null, this.OpenFileCacheTimeout * 1000, this.OpenFileCacheTimeout * 1000);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00070C58 File Offset: 0x0006EE58
		protected override void CloseTarget()
		{
			base.CloseTarget();
			foreach (string text in new List<string>(this.initializedFiles.Keys))
			{
				this.WriteFooterAndUninitialize(text);
			}
			if (this.autoClosingTimer != null)
			{
				this.autoClosingTimer.Change(-1, -1);
				this.autoClosingTimer.Dispose();
				this.autoClosingTimer = null;
			}
			if (this.recentAppenders != null)
			{
				int num = 0;
				while (num < this.recentAppenders.Length && this.recentAppenders[num] != null)
				{
					this.recentAppenders[num].Close();
					this.recentAppenders[num] = null;
					num++;
				}
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00070D20 File Offset: 0x0006EF20
		protected override void Write(LogEventInfo logEvent)
		{
			string text = Path.Combine(Application.isEditor ? "" : Application.persistentDataPath, FileTarget.CleanupFileName(this.FileName.Render(logEvent)));
			byte[] bytesToWrite = this.GetBytesToWrite(logEvent);
			if (this.ShouldAutoArchive(text, logEvent, bytesToWrite.Length))
			{
				this.InvalidateCacheItem(text);
				this.DoAutoArchive(text, logEvent);
			}
			this.WriteToFile(text, bytesToWrite, false);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00070D84 File Offset: 0x0006EF84
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			Dictionary<string, List<AsyncLogEventInfo>> dictionary = logEvents.BucketSort<AsyncLogEventInfo, string>((AsyncLogEventInfo c) => this.FileName.Render(c.LogEvent));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				List<AsyncContinuation> list = new List<AsyncContinuation>();
				foreach (KeyValuePair<string, List<AsyncLogEventInfo>> keyValuePair in dictionary)
				{
					string text = FileTarget.CleanupFileName(keyValuePair.Key);
					memoryStream.SetLength(0L);
					memoryStream.Position = 0L;
					LogEventInfo logEventInfo = null;
					foreach (AsyncLogEventInfo asyncLogEventInfo in keyValuePair.Value)
					{
						if (logEventInfo == null)
						{
							logEventInfo = asyncLogEventInfo.LogEvent;
						}
						byte[] bytesToWrite = this.GetBytesToWrite(asyncLogEventInfo.LogEvent);
						memoryStream.Write(bytesToWrite, 0, bytesToWrite.Length);
						list.Add(asyncLogEventInfo.Continuation);
					}
					this.FlushCurrentFileWrites(text, logEventInfo, memoryStream, list);
				}
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00070EA8 File Offset: 0x0006F0A8
		protected virtual string GetFormattedMessage(LogEventInfo logEvent)
		{
			return this.Layout.Render(logEvent);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00070EB8 File Offset: 0x0006F0B8
		protected virtual byte[] GetBytesToWrite(LogEventInfo logEvent)
		{
			string text = this.GetFormattedMessage(logEvent) + this.NewLineChars;
			return this.TransformBytes(this.Encoding.GetBytes(text));
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00070EEA File Offset: 0x0006F0EA
		protected virtual byte[] TransformBytes(byte[] value)
		{
			return value;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00070EF0 File Offset: 0x0006F0F0
		private static bool IsContainValidNumberPatternForReplacement(string pattern)
		{
			int num = pattern.IndexOf("{#", StringComparison.Ordinal);
			int num2 = pattern.IndexOf("#}", StringComparison.Ordinal);
			return num != -1 && num2 != -1 && num < num2;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00070F28 File Offset: 0x0006F128
		private static string ReplaceNumber(string pattern, int value)
		{
			int num = pattern.IndexOf("{#", StringComparison.Ordinal);
			int num2 = pattern.IndexOf("#}", StringComparison.Ordinal) + 2;
			int num3 = num2 - num - 2;
			return pattern.Substring(0, num) + Convert.ToString(value, 10).PadLeft(num3, '0') + pattern.Substring(num2);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00070F7C File Offset: 0x0006F17C
		private void FlushCurrentFileWrites(string currentFileName, LogEventInfo firstLogEvent, MemoryStream ms, List<AsyncContinuation> pendingContinuations)
		{
			Exception ex = null;
			try
			{
				if (currentFileName != null)
				{
					if (this.ShouldAutoArchive(currentFileName, firstLogEvent, (int)ms.Length))
					{
						this.WriteFooterAndUninitialize(currentFileName);
						this.InvalidateCacheItem(currentFileName);
						this.DoAutoArchive(currentFileName, firstLogEvent);
					}
					this.WriteToFile(currentFileName, ms.ToArray(), false);
				}
			}
			catch (Exception ex2)
			{
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				ex = ex2;
			}
			foreach (AsyncContinuation asyncContinuation in pendingContinuations)
			{
				asyncContinuation(ex);
			}
			pendingContinuations.Clear();
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00071028 File Offset: 0x0006F228
		private void RecursiveRollingRename(string fileName, string pattern, int archiveNumber)
		{
			if (archiveNumber >= this.MaxArchiveFiles)
			{
				File.Delete(fileName);
				return;
			}
			if (!File.Exists(fileName))
			{
				return;
			}
			string text = FileTarget.ReplaceNumber(pattern, archiveNumber);
			if (File.Exists(fileName))
			{
				this.RecursiveRollingRename(text, pattern, archiveNumber + 1);
			}
			FileTarget.Logger.Trace("Renaming {0} to {1}", new object[] { fileName, text });
			try
			{
				this.MoveFileToArchive(fileName, text);
			}
			catch (IOException)
			{
				string directoryName = Path.GetDirectoryName(text);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				this.MoveFileToArchive(fileName, text);
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000710C4 File Offset: 0x0006F2C4
		private void SequentialArchive(string fileName, string pattern)
		{
			string fileName2 = Path.GetFileName(pattern);
			int num = fileName2.IndexOf("{#", StringComparison.Ordinal);
			int num2 = fileName2.IndexOf("#}", StringComparison.Ordinal) + 2;
			int num3 = fileName2.Length - num2;
			string text = fileName2.Substring(0, num) + "*" + fileName2.Substring(num2);
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			int num4 = -1;
			int num5 = -1;
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			try
			{
				string[] files = Directory.GetFiles(directoryName, text);
				int i = 0;
				while (i < files.Length)
				{
					string text2 = files[i];
					string fileName3 = Path.GetFileName(text2);
					string text3 = fileName3.Substring(num, fileName3.Length - num3 - num);
					int num6;
					try
					{
						num6 = Convert.ToInt32(text3);
					}
					catch (FormatException)
					{
						goto IL_00CE;
					}
					goto IL_00A4;
					IL_00CE:
					i++;
					continue;
					IL_00A4:
					num4 = Math.Max(num4, num6);
					num5 = ((num5 != -1) ? Math.Min(num5, num6) : num6);
					dictionary[num6] = text2;
					goto IL_00CE;
				}
				num4++;
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(directoryName);
				num4 = 0;
			}
			if (num5 != -1)
			{
				int num7 = num4 - this.MaxArchiveFiles + 1;
				for (int j = num5; j < num7; j++)
				{
					string text4;
					if (dictionary.TryGetValue(j, out text4))
					{
						File.Delete(text4);
					}
				}
			}
			string text5 = FileTarget.ReplaceNumber(pattern, num4);
			this.MoveFileToArchive(fileName, text5);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0007122C File Offset: 0x0006F42C
		private void MoveFileToArchive(string existingFileName, string archiveFileName)
		{
			File.Move(existingFileName, archiveFileName);
			string fileName = Path.GetFileName(existingFileName);
			if (fileName == null)
			{
				return;
			}
			if (this.initializedFiles.ContainsKey(fileName))
			{
				this.initializedFiles.Remove(fileName);
				return;
			}
			if (this.initializedFiles.ContainsKey(existingFileName))
			{
				this.initializedFiles.Remove(existingFileName);
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00071284 File Offset: 0x0006F484
		private void DateAndSequentialArchive(string fileName, string pattern, LogEventInfo logEvent)
		{
			string fileName2 = Path.GetFileName(pattern);
			if (string.IsNullOrEmpty(fileName2))
			{
				return;
			}
			int num = fileName2.IndexOf("{#", StringComparison.Ordinal);
			int num2 = fileName2.IndexOf("#}", StringComparison.Ordinal) + 2;
			int num3 = fileName2.Length - num2;
			string text = fileName2.Substring(0, num) + "*" + fileName2.Substring(num2);
			string dateFormatString = this.GetDateFormatString(this.ArchiveDateFormat);
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			if (string.IsNullOrEmpty(directoryName))
			{
				return;
			}
			bool flag = false;
			DateTime dateTime;
			long num4;
			if (this.GetFileInfo(fileName, out dateTime, out num4))
			{
				string dateFormatString2 = this.GetDateFormatString(string.Empty);
				string text2 = dateTime.ToString(dateFormatString2);
				string text3 = logEvent.TimeStamp.ToLocalTime().ToString(dateFormatString2);
				flag = text2 != text3;
			}
			int num5 = -1;
			try
			{
				List<string> list = (from n in new DirectoryInfo(directoryName).GetFiles(text)
					orderby n.CreationTime
					select n.FullName).ToList<string>();
				List<string> list2 = new List<string>();
				for (int i = 0; i < list.Count; i++)
				{
					string fileName3 = Path.GetFileName(list[i]);
					if (!string.IsNullOrEmpty(fileName3))
					{
						string text4 = fileName3.Substring(text.LastIndexOf('*'), dateFormatString.Length);
						string text5 = fileName3.Substring(text.LastIndexOf('*') + dateFormatString.Length + 1, fileName3.Length - num3 - (text.LastIndexOf('*') + dateFormatString.Length + 1));
						int num6;
						try
						{
							num6 = Convert.ToInt32(text5);
						}
						catch (FormatException)
						{
							goto IL_01F5;
						}
						if (text4 == this.GetArchiveDate(flag).ToString(dateFormatString))
						{
							num5 = Math.Max(num5, num6);
						}
						DateTime dateTime2;
						if (DateTime.TryParseExact(text4, dateFormatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime2))
						{
							list2.Add(list[i]);
						}
					}
					IL_01F5:;
				}
				num5++;
				int num7 = 0;
				while (num7 < list2.Count && num7 <= list.Count - this.MaxArchiveFiles)
				{
					File.Delete(list2[num7]);
					num7++;
				}
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(directoryName);
				num5 = 0;
			}
			string text6 = Path.Combine(directoryName, text.Replace("*", string.Format("{0}.{1}", this.GetArchiveDate(flag).ToString(dateFormatString), num5)));
			this.MoveFileToArchive(fileName, text6);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0007155C File Offset: 0x0006F75C
		private void DateArchive(string fileName, string pattern)
		{
			string fileName2 = Path.GetFileName(pattern);
			int num = fileName2.IndexOf("{#", StringComparison.Ordinal);
			int num2 = fileName2.IndexOf("#}", StringComparison.Ordinal) + 2;
			string text = fileName2.Substring(0, num) + "*" + fileName2.Substring(num2);
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			string dateFormatString = this.GetDateFormatString(this.ArchiveDateFormat);
			try
			{
				List<string> list = (from n in new DirectoryInfo(directoryName).GetFiles(text)
					orderby n.CreationTime
					select n.FullName).ToList<string>();
				List<string> list2 = new List<string>();
				for (int i = 0; i < list.Count; i++)
				{
					string text2 = Path.GetFileName(list[i]).Substring(text.LastIndexOf('*'), dateFormatString.Length);
					DateTime minValue = DateTime.MinValue;
					if (DateTime.TryParseExact(text2, dateFormatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue))
					{
						list2.Add(list[i]);
					}
				}
				int num3 = 0;
				while (num3 < list2.Count && num3 <= list.Count - this.MaxArchiveFiles)
				{
					File.Delete(list2[num3]);
					num3++;
				}
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(directoryName);
			}
			string text3 = Path.Combine(directoryName, text.Replace("*", this.GetArchiveDate(true).ToString(dateFormatString)));
			this.MoveFileToArchive(fileName, text3);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00071708 File Offset: 0x0006F908
		private string GetDateFormatString(string defaultFormat)
		{
			string text = defaultFormat;
			if (string.IsNullOrEmpty(text))
			{
				switch (this.ArchiveEvery)
				{
				case FileArchivePeriod.Year:
					return "yyyy";
				case FileArchivePeriod.Month:
					return "yyyyMM";
				case FileArchivePeriod.Hour:
					return "yyyyMMddHH";
				case FileArchivePeriod.Minute:
					return "yyyyMMddHHmm";
				}
				text = "yyyyMMdd";
			}
			return text;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0007176C File Offset: 0x0006F96C
		private DateTime GetArchiveDate(bool isNextCycle)
		{
			DateTime dateTime = DateTime.Now;
			int num = (isNextCycle ? (-1) : 0);
			switch (this.ArchiveEvery)
			{
			case FileArchivePeriod.Year:
				dateTime = dateTime.AddYears(num);
				break;
			case FileArchivePeriod.Month:
				dateTime = dateTime.AddMonths(num);
				break;
			case FileArchivePeriod.Day:
				dateTime = dateTime.AddDays((double)num);
				break;
			case FileArchivePeriod.Hour:
				dateTime = dateTime.AddHours((double)num);
				break;
			case FileArchivePeriod.Minute:
				dateTime = dateTime.AddMinutes((double)num);
				break;
			}
			return dateTime;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000717E8 File Offset: 0x0006F9E8
		private void DoAutoArchive(string fileName, LogEventInfo ev)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (!fileInfo.Exists)
			{
				return;
			}
			string text;
			if (this.ArchiveFileName == null)
			{
				string extension = Path.GetExtension(fileName);
				text = Path.ChangeExtension(fileInfo.FullName, ".{#}" + extension);
			}
			else
			{
				text = this.ArchiveFileName.Render(ev);
			}
			if (!FileTarget.IsContainValidNumberPatternForReplacement(text))
			{
				if (this.dynamicArchiveFileHandler.AddToArchive(text, fileInfo.FullName, this.CreateDirs) && this.initializedFiles.ContainsKey(fileInfo.FullName))
				{
					this.initializedFiles.Remove(fileInfo.FullName);
					return;
				}
			}
			else
			{
				switch (this.ArchiveNumbering)
				{
				case ArchiveNumberingMode.Sequence:
					this.SequentialArchive(fileInfo.FullName, text);
					return;
				case ArchiveNumberingMode.Rolling:
					this.RecursiveRollingRename(fileInfo.FullName, text, 0);
					return;
				case ArchiveNumberingMode.Date:
					this.DateArchive(fileInfo.FullName, text);
					return;
				case ArchiveNumberingMode.DateAndSequence:
					this.DateAndSequentialArchive(fileInfo.FullName, text, ev);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000718DC File Offset: 0x0006FADC
		private bool ShouldAutoArchive(string fileName, LogEventInfo ev, int upcomingWriteSize)
		{
			if (this.ArchiveAboveSize == -1L && this.ArchiveEvery == FileArchivePeriod.None)
			{
				return false;
			}
			DateTime dateTime;
			long num;
			if (!this.GetFileInfo(fileName, out dateTime, out num))
			{
				return false;
			}
			if (this.ArchiveAboveSize != -1L && num + (long)upcomingWriteSize > this.ArchiveAboveSize)
			{
				return true;
			}
			if (this.ArchiveEvery != FileArchivePeriod.None)
			{
				string dateFormatString = this.GetDateFormatString(string.Empty);
				string text = dateTime.ToString(dateFormatString);
				string text2 = ev.TimeStamp.ToLocalTime().ToString(dateFormatString);
				if (text != text2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00071968 File Offset: 0x0006FB68
		private void AutoClosingTimerCallback(object state)
		{
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				if (base.IsInitialized)
				{
					try
					{
						DateTime dateTime = DateTime.Now.AddSeconds((double)(-(double)this.OpenFileCacheTimeout));
						int num = 0;
						while (num < this.recentAppenders.Length && this.recentAppenders[num] != null)
						{
							if (this.recentAppenders[num].OpenTime < dateTime)
							{
								for (int i = num; i < this.recentAppenders.Length; i++)
								{
									if (this.recentAppenders[i] == null)
									{
										break;
									}
									this.recentAppenders[i].Close();
									this.recentAppenders[i] = null;
								}
								break;
							}
							num++;
						}
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						FileTarget.Logger.Warn("Exception in AutoClosingTimerCallback: {0}", new object[] { ex });
					}
				}
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00071A70 File Offset: 0x0006FC70
		private void WriteToFile(string fileName, byte[] bytes, bool justData)
		{
			if (this.ReplaceFileContentsOnEachWrite)
			{
				using (FileStream fileStream = File.Create(fileName))
				{
					byte[] headerBytes = this.GetHeaderBytes();
					byte[] footerBytes = this.GetFooterBytes();
					if (headerBytes != null)
					{
						fileStream.Write(headerBytes, 0, headerBytes.Length);
					}
					fileStream.Write(bytes, 0, bytes.Length);
					if (footerBytes != null)
					{
						fileStream.Write(footerBytes, 0, footerBytes.Length);
					}
				}
				return;
			}
			bool flag = false;
			if (!justData)
			{
				if (!this.initializedFiles.ContainsKey(fileName))
				{
					if (this.ArchiveOldFileOnStartup)
					{
						try
						{
							this.DoAutoArchive(fileName, null);
						}
						catch (Exception ex)
						{
							if (ex.MustBeRethrown())
							{
								throw;
							}
							FileTarget.Logger.Warn("Unable to archive old log file '{0}': {1}", new object[] { fileName, ex });
						}
					}
					if (this.AutoClear)
					{
						try
						{
							File.Delete(fileName);
						}
						catch (Exception ex2)
						{
							if (ex2.MustBeRethrown())
							{
								throw;
							}
							FileTarget.Logger.Warn("Unable to delete old log file '{0}': {1}", new object[] { fileName, ex2 });
						}
					}
					this.initializedFiles[fileName] = DateTime.Now;
					this.initializedFilesCounter++;
					flag = true;
					if (this.initializedFilesCounter >= 100)
					{
						this.initializedFilesCounter = 0;
						this.CleanupInitializedFiles();
					}
				}
				this.initializedFiles[fileName] = DateTime.Now;
			}
			BaseFileAppender baseFileAppender = null;
			int num = this.recentAppenders.Length - 1;
			for (int i = 0; i < this.recentAppenders.Length; i++)
			{
				if (this.recentAppenders[i] == null)
				{
					num = i;
					break;
				}
				if (this.recentAppenders[i].FileName == fileName)
				{
					BaseFileAppender baseFileAppender2 = this.recentAppenders[i];
					for (int j = i; j > 0; j--)
					{
						this.recentAppenders[j] = this.recentAppenders[j - 1];
					}
					this.recentAppenders[0] = baseFileAppender2;
					baseFileAppender = baseFileAppender2;
					break;
				}
			}
			if (baseFileAppender == null)
			{
				BaseFileAppender baseFileAppender3 = this.appenderFactory.Open(fileName, this);
				try
				{
					if (this.recentAppenders[num] != null)
					{
						this.recentAppenders[num].Close();
						this.recentAppenders[num] = null;
					}
				}
				catch (Exception ex3)
				{
					FileTarget.Logger.Error(ex3.ToString(), Array.Empty<object>());
				}
				for (int k = num; k > 0; k--)
				{
					this.recentAppenders[k] = this.recentAppenders[k - 1];
				}
				this.recentAppenders[0] = baseFileAppender3;
				baseFileAppender = baseFileAppender3;
			}
			DateTime dateTime;
			long num2;
			if (flag && (!baseFileAppender.GetFileInfo(out dateTime, out num2) || num2 == 0L))
			{
				byte[] headerBytes2 = this.GetHeaderBytes();
				if (headerBytes2 != null)
				{
					baseFileAppender.Write(headerBytes2);
				}
			}
			baseFileAppender.Write(bytes);
			if (this.AutoFlush)
			{
				baseFileAppender.Flush();
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00071D2C File Offset: 0x0006FF2C
		private byte[] GetHeaderBytes()
		{
			if (base.Header == null)
			{
				return null;
			}
			string text = base.Header.Render(LogEventInfo.CreateNullEvent()) + this.NewLineChars;
			return this.TransformBytes(this.Encoding.GetBytes(text));
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00071D74 File Offset: 0x0006FF74
		private byte[] GetFooterBytes()
		{
			if (base.Footer == null)
			{
				return null;
			}
			string text = base.Footer.Render(LogEventInfo.CreateNullEvent()) + this.NewLineChars;
			return this.TransformBytes(this.Encoding.GetBytes(text));
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00071DBC File Offset: 0x0006FFBC
		private void WriteFooterAndUninitialize(string fileName)
		{
			byte[] footerBytes = this.GetFooterBytes();
			if (footerBytes != null && File.Exists(fileName))
			{
				this.WriteToFile(fileName, footerBytes, true);
			}
			this.initializedFiles.Remove(fileName);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00071DF4 File Offset: 0x0006FFF4
		private bool GetFileInfo(string fileName, out DateTime lastWriteTime, out long fileLength)
		{
			foreach (BaseFileAppender baseFileAppender in this.recentAppenders)
			{
				if (baseFileAppender == null)
				{
					break;
				}
				if (baseFileAppender.FileName == fileName)
				{
					baseFileAppender.GetFileInfo(out lastWriteTime, out fileLength);
					return true;
				}
			}
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.Exists)
			{
				fileLength = fileInfo.Length;
				lastWriteTime = fileInfo.LastWriteTime;
				return true;
			}
			fileLength = -1L;
			lastWriteTime = DateTime.MinValue;
			return false;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00071E6C File Offset: 0x0007006C
		private void InvalidateCacheItem(string fileName)
		{
			int num = 0;
			while (num < this.recentAppenders.Length && this.recentAppenders[num] != null)
			{
				if (this.recentAppenders[num].FileName == fileName)
				{
					this.recentAppenders[num].Close();
					for (int i = num; i < this.recentAppenders.Length - 1; i++)
					{
						this.recentAppenders[i] = this.recentAppenders[i + 1];
					}
					this.recentAppenders[this.recentAppenders.Length - 1] = null;
					return;
				}
				num++;
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00071EF4 File Offset: 0x000700F4
		private static string CleanupFileName(string fileName)
		{
			int num = fileName.LastIndexOfAny(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
			string text = fileName.Substring(num + 1);
			string text2 = ((num > 0) ? fileName.Substring(0, num) : string.Empty);
			text = Path.GetInvalidFileNameChars().Aggregate(text, (string current, char c) => current.Replace(c, '_'));
			return Path.Combine(text2, text);
		}

		// Token: 0x04000CCE RID: 3278
		private readonly Dictionary<string, DateTime> initializedFiles = new Dictionary<string, DateTime>();

		// Token: 0x04000CCF RID: 3279
		private LineEndingMode lineEndingMode;

		// Token: 0x04000CD0 RID: 3280
		private IFileAppenderFactory appenderFactory;

		// Token: 0x04000CD1 RID: 3281
		private BaseFileAppender[] recentAppenders;

		// Token: 0x04000CD2 RID: 3282
		private Timer autoClosingTimer;

		// Token: 0x04000CD3 RID: 3283
		private int initializedFilesCounter;

		// Token: 0x04000CD4 RID: 3284
		private int _MaxArchiveFilesField;

		// Token: 0x04000CD5 RID: 3285
		private readonly FileTarget.DynamicArchiveFileHandlerClass dynamicArchiveFileHandler;

		// Token: 0x0200165E RID: 5726
		private class DynamicArchiveFileHandlerClass
		{
			// Token: 0x0600D7E3 RID: 55267 RVA: 0x004FCBF3 File Offset: 0x004FADF3
			public DynamicArchiveFileHandlerClass(int MaxArchivedFiles)
				: this()
			{
				this.MaxArchiveFileToKeep = MaxArchivedFiles;
			}

			// Token: 0x0600D7E4 RID: 55268 RVA: 0x004FCC02 File Offset: 0x004FAE02
			public DynamicArchiveFileHandlerClass()
			{
				this.MaxArchiveFileToKeep = -1;
				this.archiveFileEntryQueue = new Queue<string>();
			}

			// Token: 0x17000D3C RID: 3388
			// (get) Token: 0x0600D7E5 RID: 55269 RVA: 0x004FCC1C File Offset: 0x004FAE1C
			// (set) Token: 0x0600D7E6 RID: 55270 RVA: 0x004FCC24 File Offset: 0x004FAE24
			public int MaxArchiveFileToKeep { get; set; }

			// Token: 0x0600D7E7 RID: 55271 RVA: 0x004FCC30 File Offset: 0x004FAE30
			public bool AddToArchive(string archiveFileName, string fileName, bool createDirectoryIfNotExists)
			{
				if (this.MaxArchiveFileToKeep < 1)
				{
					FileTarget.Logger.Warn("AddToArchive is called. Even though the MaxArchiveFiles is set to less than 1", Array.Empty<object>());
					return false;
				}
				if (!File.Exists(fileName))
				{
					FileTarget.Logger.Error("Error while trying to archive, Source File : {0} Not found.", new object[] { fileName });
					return false;
				}
				while (this.archiveFileEntryQueue.Count >= this.MaxArchiveFileToKeep)
				{
					string text = this.archiveFileEntryQueue.Dequeue();
					try
					{
						File.Delete(text);
					}
					catch (Exception ex)
					{
						FileTarget.Logger.Warn("Can't Delete Old Archive File : {0} , Exception : {1}", new object[] { text, ex });
					}
				}
				string text2 = archiveFileName;
				if (this.archiveFileEntryQueue.Contains(archiveFileName))
				{
					FileTarget.Logger.Trace("Archive File {0} seems to be already exist. Trying with Different File Name..", new object[] { archiveFileName });
					int num = 1;
					text2 = Path.GetFileNameWithoutExtension(archiveFileName) + ".{#}" + Path.GetExtension(archiveFileName);
					while (File.Exists(FileTarget.ReplaceNumber(text2, num)))
					{
						FileTarget.Logger.Trace("Archive File {0} seems to be already exist, too. Trying with Different File Name..", new object[] { archiveFileName });
						num++;
					}
				}
				try
				{
					File.Move(fileName, text2);
				}
				catch (DirectoryNotFoundException)
				{
					if (createDirectoryIfNotExists)
					{
						FileTarget.Logger.Trace("Directory For Archive File is not created. Creating it..", Array.Empty<object>());
						try
						{
							Directory.CreateDirectory(Path.GetDirectoryName(archiveFileName));
							File.Move(fileName, text2);
							goto IL_014F;
						}
						catch (Exception ex2)
						{
							FileTarget.Logger.Error("Can't create Archive File Directory , Exception : {0}", new object[] { ex2 });
							throw;
						}
						goto IL_014D;
						IL_014F:
						goto IL_0173;
					}
					IL_014D:
					throw;
				}
				catch (Exception ex3)
				{
					FileTarget.Logger.Error("Can't Archive File : {0} , Exception : {1}", new object[] { fileName, ex3 });
					throw;
				}
				IL_0173:
				this.archiveFileEntryQueue.Enqueue(archiveFileName);
				return true;
			}

			// Token: 0x04005A3A RID: 23098
			private readonly Queue<string> archiveFileEntryQueue;
		}
	}
}
