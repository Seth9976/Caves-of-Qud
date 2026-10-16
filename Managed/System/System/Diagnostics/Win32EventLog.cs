using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000289 RID: 649
	internal class Win32EventLog : EventLogImpl
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x00053303 File Offset: 0x00051503
		public Win32EventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00003917 File Offset: 0x00001B17
		public override void BeginInit()
		{
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00053317 File Offset: 0x00051517
		public override void Clear()
		{
			if (Win32EventLog.PInvoke.ClearEventLog(this.ReadHandle, null) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00053334 File Offset: 0x00051534
		public override void Close()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._readHandle != IntPtr.Zero)
				{
					this.CloseEventLog(this._readHandle);
					this._readHandle = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00053398 File Offset: 0x00051598
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(sourceData.MachineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("EventLog registry key is missing.");
				}
				bool flag = false;
				RegistryKey registryKey = null;
				try
				{
					registryKey = eventLogKey.OpenSubKey(sourceData.LogName, true);
					if (registryKey == null)
					{
						base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
						registryKey = eventLogKey.CreateSubKey(sourceData.LogName);
						registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
						Win32EventLog.UpdateLogRegistry(registryKey);
						using (RegistryKey registryKey2 = registryKey.CreateSubKey(sourceData.LogName))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey2, sourceData);
						}
						flag = true;
					}
					if (sourceData.LogName != sourceData.Source)
					{
						if (!flag)
						{
							string[] array = (string[])registryKey.GetValue("Sources");
							if (array == null)
							{
								registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
							}
							else
							{
								bool flag2 = false;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == sourceData.Source)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									string[] array2 = new string[array.Length + 1];
									Array.Copy(array, 0, array2, 0, array.Length);
									array2[array.Length] = sourceData.Source;
									registryKey.SetValue("Sources", array2);
								}
							}
						}
						using (RegistryKey registryKey3 = registryKey.CreateSubKey(sourceData.Source))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey3, sourceData);
						}
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0005359C File Offset: 0x0005179C
		public override void Delete(string logName, string machineName)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("The event log key does not exist.");
				}
				using (RegistryKey registryKey = eventLogKey.OpenSubKey(logName, false))
				{
					if (registryKey == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", logName, machineName));
					}
					base.CoreEventLog.Clear();
					string text = (string)registryKey.GetValue("File");
					if (text != null)
					{
						try
						{
							File.Delete(text);
						}
						catch (Exception)
						{
						}
					}
				}
				eventLogKey.DeleteSubKeyTree(logName);
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00053654 File Offset: 0x00051854
		public override void DeleteEventSource(string source, string machineName)
		{
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, true))
			{
				if (registryKey == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
				}
				registryKey.DeleteSubKeyTree(source);
				string[] array = (string[])registryKey.GetValue("Sources");
				if (array != null)
				{
					List<string> list = new List<string>();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != source)
						{
							list.Add(array[i]);
						}
					}
					string[] array2 = list.ToArray();
					registryKey.SetValue("Sources", array2);
				}
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00051A27 File Offset: 0x0004FC27
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EndInit()
		{
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000536FC File Offset: 0x000518FC
		public override bool Exists(string logName, string machineName)
		{
			bool flag;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyByName(logName, machineName, false))
			{
				flag = registryKey != null;
			}
			return flag;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00053734 File Offset: 0x00051934
		[MonoTODO]
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "EventMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], messageID, replacementStrings);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return string.Join(", ", replacementStrings);
			}
			return text;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0005377C File Offset: 0x0005197C
		private string FormatCategory(string source, int category)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "CategoryMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], (uint)category, new string[0]);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return "(" + category.ToString(CultureInfo.InvariantCulture) + ")";
			}
			return text;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000537DC File Offset: 0x000519DC
		protected override int GetEntryCount()
		{
			int num = 0;
			if (Win32EventLog.PInvoke.GetNumberOfEventLogRecords(this.ReadHandle, ref num) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return num;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00053808 File Offset: 0x00051A08
		protected override EventLogEntry GetEntry(int index)
		{
			index += this.OldestEventLogEntry;
			int num = 0;
			int num2 = 0;
			byte[] array = new byte[524287];
			this.ReadEventLog(index, array, ref num, ref num2);
			MemoryStream memoryStream = new MemoryStream(array);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryReader.ReadBytes(8);
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			int num5 = binaryReader.ReadInt32();
			uint num6 = binaryReader.ReadUInt32();
			int eventID = EventLog.GetEventID((long)((ulong)num6));
			short num7 = binaryReader.ReadInt16();
			short num8 = binaryReader.ReadInt16();
			short num9 = binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt32();
			int num10 = binaryReader.ReadInt32();
			int num11 = binaryReader.ReadInt32();
			int num12 = binaryReader.ReadInt32();
			int num13 = binaryReader.ReadInt32();
			int num14 = binaryReader.ReadInt32();
			DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds((double)num4);
			DateTime dateTime2 = new DateTime(1970, 1, 1).AddSeconds((double)num5);
			StringBuilder stringBuilder = new StringBuilder();
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text2 = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text3 = null;
			if (num11 != 0)
			{
				memoryStream.Position = (long)num12;
				byte[] array2 = binaryReader.ReadBytes(num11);
				text3 = Win32EventLog.LookupAccountSid(text2, array2);
			}
			memoryStream.Position = (long)num10;
			string[] array3 = new string[(int)num8];
			for (int i = 0; i < (int)num8; i++)
			{
				stringBuilder.Length = 0;
				while (binaryReader.PeekChar() != 0)
				{
					stringBuilder.Append(binaryReader.ReadChar());
				}
				binaryReader.ReadChar();
				array3[i] = stringBuilder.ToString();
			}
			byte[] array4 = new byte[num13];
			memoryStream.Position = (long)num14;
			binaryReader.Read(array4, 0, num13);
			string text4 = this.FormatMessage(text, num6, array3);
			return new EventLogEntry(this.FormatCategory(text, (int)num9), num9, num3, eventID, text, text4, text3, text2, (EventLogEntryType)num7, dateTime, dateTime2, array4, array3, (long)((ulong)num6));
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00051CEC File Offset: 0x0004FEEC
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00053A74 File Offset: 0x00051C74
		protected override string[] GetLogNames(string machineName)
		{
			string[] array;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					array = new string[0];
				}
				else
				{
					array = eventLogKey.GetSubKeyNames();
				}
			}
			return array;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00053ABC File Offset: 0x00051CBC
		public override string LogNameFromSourceName(string source, string machineName)
		{
			string text;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false))
			{
				if (registryKey == null)
				{
					text = string.Empty;
				}
				else
				{
					text = Win32EventLog.GetLogName(registryKey);
				}
			}
			return text;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00053B04 File Offset: 0x00051D04
		public override bool SourceExists(string source, string machineName)
		{
			RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false);
			if (registryKey != null)
			{
				registryKey.Close();
				return true;
			}
			return false;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00053B28 File Offset: 0x00051D28
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			IntPtr intPtr = this.RegisterEventSource();
			try
			{
				if (Win32EventLog.PInvoke.ReportEvent(intPtr, (ushort)type, (ushort)category, instanceID, IntPtr.Zero, (ushort)replacementStrings.Length, (uint)rawData.Length, replacementStrings, rawData) != 1)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				this.DeregisterEventSource(intPtr);
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00053B80 File Offset: 0x00051D80
		private static void UpdateLogRegistry(RegistryKey logKey)
		{
			if (logKey.GetValue("File") == null)
			{
				string logName = Win32EventLog.GetLogName(logKey);
				string text;
				if (logName.Length > 8)
				{
					text = logName.Substring(0, 8) + ".evt";
				}
				else
				{
					text = logName + ".evt";
				}
				string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "config");
				logKey.SetValue("File", Path.Combine(text2, text));
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00053BF0 File Offset: 0x00051DF0
		private static void UpdateSourceRegistry(RegistryKey sourceKey, EventSourceCreationData data)
		{
			if (data.CategoryCount > 0)
			{
				sourceKey.SetValue("CategoryCount", data.CategoryCount);
			}
			if (data.CategoryResourceFile != null && data.CategoryResourceFile.Length > 0)
			{
				sourceKey.SetValue("CategoryMessageFile", data.CategoryResourceFile);
			}
			if (data.MessageResourceFile != null && data.MessageResourceFile.Length > 0)
			{
				sourceKey.SetValue("EventMessageFile", data.MessageResourceFile);
			}
			if (data.ParameterResourceFile != null && data.ParameterResourceFile.Length > 0)
			{
				sourceKey.SetValue("ParameterMessageFile", data.ParameterResourceFile);
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00053C91 File Offset: 0x00051E91
		private static string GetLogName(RegistryKey logKey)
		{
			string name = logKey.Name;
			return name.Substring(name.LastIndexOf("\\") + 1);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00053CAC File Offset: 0x00051EAC
		private void ReadEventLog(int index, byte[] buffer, ref int bytesRead, ref int minBufferNeeded)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Win32EventLog.PInvoke.ReadEventLog(this.ReadHandle, (Win32EventLog.ReadFlags)6, index, buffer, buffer.Length, ref bytesRead, ref minBufferNeeded) != 1)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (i >= 2)
					{
						throw new Win32Exception(lastWin32Error);
					}
					base.CoreEventLog.Reset();
				}
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00053CFA File Offset: 0x00051EFA
		[MonoTODO("Support remote machines")]
		private static RegistryKey GetEventLogKey(string machineName, bool writable)
		{
			return Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00053D0C File Offset: 0x00051F0C
		private static RegistryKey FindSourceKeyByName(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						using (RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], writable))
						{
							if (registryKey3 == null)
							{
								break;
							}
							RegistryKey registryKey4 = registryKey3.OpenSubKey(source, writable);
							if (registryKey4 != null)
							{
								return registryKey4;
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00053DAC File Offset: 0x00051FAC
		private static RegistryKey FindLogKeyByName(string logName, string machineName, bool writable)
		{
			RegistryKey registryKey;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, writable))
			{
				if (eventLogKey == null)
				{
					registryKey = null;
				}
				else
				{
					registryKey = eventLogKey.OpenSubKey(logName, writable);
				}
			}
			return registryKey;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00053DF0 File Offset: 0x00051FF0
		private static RegistryKey FindLogKeyBySource(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey3 = null;
						try
						{
							RegistryKey registryKey4 = registryKey.OpenSubKey(subKeyNames[i], writable);
							if (registryKey4 != null)
							{
								registryKey3 = registryKey4.OpenSubKey(source, writable);
								if (registryKey3 != null)
								{
									return registryKey4;
								}
							}
						}
						finally
						{
							if (registryKey3 != null)
							{
								registryKey3.Close();
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00053E90 File Offset: 0x00052090
		private int OldestEventLogEntry
		{
			get
			{
				int num = 0;
				if (Win32EventLog.PInvoke.GetOldestEventLogRecord(this.ReadHandle, ref num) != 1)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				return num;
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00053EBB File Offset: 0x000520BB
		private void CloseEventLog(IntPtr hEventLog)
		{
			if (Win32EventLog.PInvoke.CloseEventLog(hEventLog) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00053ED1 File Offset: 0x000520D1
		private void DeregisterEventSource(IntPtr hEventLog)
		{
			if (Win32EventLog.PInvoke.DeregisterEventSource(hEventLog) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00053EE8 File Offset: 0x000520E8
		private static string LookupAccountSid(string machineName, byte[] sid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder();
			uint capacity2 = (uint)stringBuilder2.Capacity;
			string text = null;
			while (text == null)
			{
				Win32EventLog.SidNameUse sidNameUse;
				if (!Win32EventLog.PInvoke.LookupAccountSid(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sidNameUse))
				{
					if (Marshal.GetLastWin32Error() == 122)
					{
						stringBuilder.EnsureCapacity((int)capacity);
						stringBuilder2.EnsureCapacity((int)capacity2);
					}
					else
					{
						text = string.Empty;
					}
				}
				else
				{
					text = string.Format("{0}\\{1}", stringBuilder2.ToString(), stringBuilder.ToString());
				}
			}
			return text;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00053F68 File Offset: 0x00052168
		private static string FetchMessage(string msgDll, uint messageID, string[] replacementStrings)
		{
			IntPtr intPtr = Win32EventLog.PInvoke.LoadLibraryEx(msgDll, IntPtr.Zero, Win32EventLog.LoadFlags.LibraryAsDataFile);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr[] array = new IntPtr[replacementStrings.Length];
			try
			{
				for (int i = 0; i < replacementStrings.Length; i++)
				{
					array[i] = Marshal.StringToHGlobalAuto(replacementStrings[i]);
				}
				if (Win32EventLog.PInvoke.FormatMessage(Win32EventLog.FormatMessageFlags.AllocateBuffer | Win32EventLog.FormatMessageFlags.FromHModule | Win32EventLog.FormatMessageFlags.ArgumentArray, intPtr, messageID, 0, ref intPtr2, 0, array) != 0)
				{
					string text = Marshal.PtrToStringAuto(intPtr2);
					intPtr2 = Win32EventLog.PInvoke.LocalFree(intPtr2);
					return text.TrimEnd(null);
				}
				Marshal.GetLastWin32Error();
			}
			finally
			{
				foreach (IntPtr intPtr3 in array)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
				}
				Win32EventLog.PInvoke.FreeLibrary(intPtr);
			}
			return null;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005403C File Offset: 0x0005223C
		private string[] GetMessageResourceDlls(string source, string valueName)
		{
			RegistryKey registryKey = Win32EventLog.FindSourceKeyByName(source, base.CoreEventLog.MachineName, false);
			if (registryKey != null)
			{
				string text = registryKey.GetValue(valueName) as string;
				if (text != null)
				{
					return text.Split(';', StringSplitOptions.None);
				}
			}
			return new string[0];
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00054080 File Offset: 0x00052280
		private IntPtr ReadHandle
		{
			get
			{
				if (this._readHandle != IntPtr.Zero)
				{
					return this._readHandle;
				}
				string logName = base.CoreEventLog.GetLogName();
				this._readHandle = Win32EventLog.PInvoke.OpenEventLog(base.CoreEventLog.MachineName, logName);
				if (this._readHandle == IntPtr.Zero)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' on computer '{1}' cannot be opened.", logName, base.CoreEventLog.MachineName), new Win32Exception());
				}
				return this._readHandle;
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00054108 File Offset: 0x00052308
		private IntPtr RegisterEventSource()
		{
			IntPtr intPtr = Win32EventLog.PInvoke.RegisterEventSource(base.CoreEventLog.MachineName, base.CoreEventLog.Source);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event source '{0}' on computer '{1}' cannot be opened.", base.CoreEventLog.Source, base.CoreEventLog.MachineName), new Win32Exception());
			}
			return intPtr;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00054170 File Offset: 0x00052370
		public override void DisableNotification()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._notifyResetEvent != null)
				{
					this._notifyResetEvent.Close();
					this._notifyResetEvent = null;
				}
				this._notifyThread = null;
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000541CC File Offset: 0x000523CC
		public override void EnableNotification()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._notifyResetEvent == null)
				{
					this._notifyResetEvent = new ManualResetEvent(false);
					this._lastEntryWritten = this.OldestEventLogEntry + base.EntryCount;
					if (Win32EventLog.PInvoke.NotifyChangeEventLog(this.ReadHandle, this._notifyResetEvent.SafeWaitHandle.DangerousGetHandle()) == 0)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to receive notifications for log '{0}' on computer '{1}'.", base.CoreEventLog.GetLogName(), base.CoreEventLog.MachineName), new Win32Exception());
					}
					this._notifyThread = new Thread(delegate
					{
						this.NotifyEventThread(this._notifyResetEvent);
					});
					this._notifyThread.IsBackground = true;
					this._notifyThread.Start();
				}
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000542B0 File Offset: 0x000524B0
		private void NotifyEventThread(ManualResetEvent resetEvent)
		{
			if (resetEvent == null)
			{
				return;
			}
			for (;;)
			{
				try
				{
					resetEvent.WaitOne();
				}
				catch (ObjectDisposedException)
				{
					break;
				}
				object eventLock = this._eventLock;
				lock (eventLock)
				{
					if (resetEvent == this._notifyResetEvent)
					{
						if (!(this._readHandle == IntPtr.Zero))
						{
							int oldestEventLogEntry = this.OldestEventLogEntry;
							if (this._lastEntryWritten < oldestEventLogEntry)
							{
								this._lastEntryWritten = oldestEventLogEntry;
							}
							int num = this._lastEntryWritten - oldestEventLogEntry;
							int num2 = base.EntryCount + oldestEventLogEntry;
							for (int i = num; i < num2 - 1; i++)
							{
								EventLogEntry entry = this.GetEntry(i);
								base.CoreEventLog.OnEntryWritten(entry);
							}
							this._lastEntryWritten = num2;
							continue;
						}
					}
				}
				break;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0000829A File Offset: 0x0000649A
		public override OverflowAction OverflowAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0000829A File Offset: 0x0000649A
		public override int MinimumRetentionDays
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0000829A File Offset: 0x0000649A
		public override long MaximumKilobytes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0000829A File Offset: 0x0000649A
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0000829A File Offset: 0x0000649A
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000BB0 RID: 2992
		private const int MESSAGE_NOT_FOUND = 317;

		// Token: 0x04000BB1 RID: 2993
		private ManualResetEvent _notifyResetEvent;

		// Token: 0x04000BB2 RID: 2994
		private IntPtr _readHandle;

		// Token: 0x04000BB3 RID: 2995
		private Thread _notifyThread;

		// Token: 0x04000BB4 RID: 2996
		private int _lastEntryWritten;

		// Token: 0x04000BB5 RID: 2997
		private object _eventLock = new object();

		// Token: 0x0200028A RID: 650
		private class PInvoke
		{
			// Token: 0x060014A1 RID: 5281
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ClearEventLog(IntPtr hEventLog, string lpBackupFileName);

			// Token: 0x060014A2 RID: 5282
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int CloseEventLog(IntPtr hEventLog);

			// Token: 0x060014A3 RID: 5283
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int DeregisterEventSource(IntPtr hEventLog);

			// Token: 0x060014A4 RID: 5284
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int FormatMessage(Win32EventLog.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, ref IntPtr lpBuffer, int nSize, IntPtr[] arguments);

			// Token: 0x060014A5 RID: 5285
			[DllImport("kernel32", SetLastError = true)]
			public static extern bool FreeLibrary(IntPtr hModule);

			// Token: 0x060014A6 RID: 5286
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetNumberOfEventLogRecords(IntPtr hEventLog, ref int NumberOfRecords);

			// Token: 0x060014A7 RID: 5287
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetOldestEventLogRecord(IntPtr hEventLog, ref int OldestRecord);

			// Token: 0x060014A8 RID: 5288
			[DllImport("kernel32", SetLastError = true)]
			public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, Win32EventLog.LoadFlags dwFlags);

			// Token: 0x060014A9 RID: 5289
			[DllImport("kernel32", SetLastError = true)]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x060014AA RID: 5290
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out Win32EventLog.SidNameUse peUse);

			// Token: 0x060014AB RID: 5291
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int NotifyChangeEventLog(IntPtr hEventLog, IntPtr hEvent);

			// Token: 0x060014AC RID: 5292
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr OpenEventLog(string machineName, string logName);

			// Token: 0x060014AD RID: 5293
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr RegisterEventSource(string machineName, string sourceName);

			// Token: 0x060014AE RID: 5294
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReportEvent(IntPtr hHandle, ushort wType, ushort wCategory, uint dwEventID, IntPtr sid, ushort wNumStrings, uint dwDataSize, string[] lpStrings, byte[] lpRawData);

			// Token: 0x060014AF RID: 5295
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReadEventLog(IntPtr hEventLog, Win32EventLog.ReadFlags dwReadFlags, int dwRecordOffset, byte[] buffer, int nNumberOfBytesToRead, ref int pnBytesRead, ref int pnMinNumberOfBytesNeeded);

			// Token: 0x04000BB6 RID: 2998
			public const int ERROR_INSUFFICIENT_BUFFER = 122;

			// Token: 0x04000BB7 RID: 2999
			public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;
		}

		// Token: 0x0200028B RID: 651
		private enum ReadFlags
		{
			// Token: 0x04000BB9 RID: 3001
			Sequential = 1,
			// Token: 0x04000BBA RID: 3002
			Seek,
			// Token: 0x04000BBB RID: 3003
			ForwardsRead = 4,
			// Token: 0x04000BBC RID: 3004
			BackwardsRead = 8
		}

		// Token: 0x0200028C RID: 652
		private enum LoadFlags : uint
		{
			// Token: 0x04000BBE RID: 3006
			LibraryAsDataFile = 2U
		}

		// Token: 0x0200028D RID: 653
		[Flags]
		private enum FormatMessageFlags
		{
			// Token: 0x04000BC0 RID: 3008
			AllocateBuffer = 256,
			// Token: 0x04000BC1 RID: 3009
			IgnoreInserts = 512,
			// Token: 0x04000BC2 RID: 3010
			FromHModule = 2048,
			// Token: 0x04000BC3 RID: 3011
			FromSystem = 4096,
			// Token: 0x04000BC4 RID: 3012
			ArgumentArray = 8192
		}

		// Token: 0x0200028E RID: 654
		private enum SidNameUse
		{
			// Token: 0x04000BC6 RID: 3014
			User = 1,
			// Token: 0x04000BC7 RID: 3015
			Group,
			// Token: 0x04000BC8 RID: 3016
			Domain,
			// Token: 0x04000BC9 RID: 3017
			lias,
			// Token: 0x04000BCA RID: 3018
			WellKnownGroup,
			// Token: 0x04000BCB RID: 3019
			DeletedAccount,
			// Token: 0x04000BCC RID: 3020
			Invalid,
			// Token: 0x04000BCD RID: 3021
			Unknown,
			// Token: 0x04000BCE RID: 3022
			Computer
		}
	}
}
