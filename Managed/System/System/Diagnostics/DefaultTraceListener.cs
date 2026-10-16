using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides the default output methods and behavior for tracing.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000255 RID: 597
	public class DefaultTraceListener : TraceListener
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x0004EA50 File Offset: 0x0004CC50
		static DefaultTraceListener()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_TRACE_LISTENER");
				if (environmentVariable != null)
				{
					string text;
					string text2;
					if (environmentVariable.StartsWith("Console.Out"))
					{
						text = "Console.Out";
						text2 = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Out");
					}
					else if (environmentVariable.StartsWith("Console.Error"))
					{
						text = "Console.Error";
						text2 = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Error");
					}
					else
					{
						text = environmentVariable;
						text2 = "";
					}
					DefaultTraceListener.MonoTraceFile = text;
					DefaultTraceListener.MonoTracePrefix = text2;
				}
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004EADA File Offset: 0x0004CCDA
		private static string GetPrefix(string var, string target)
		{
			if (var.Length > target.Length)
			{
				return var.Substring(target.Length + 1);
			}
			return "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DefaultTraceListener" /> class with "Default" as its <see cref="P:System.Diagnostics.TraceListener.Name" /> property value.</summary>
		// Token: 0x06001250 RID: 4688 RVA: 0x0004EAFE File Offset: 0x0004CCFE
		public DefaultTraceListener()
			: base("Default")
		{
		}

		/// <summary>Gets or sets a value indicating whether the application is running in user-interface mode.</summary>
		/// <returns>true if user-interface mode is enabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004EB0B File Offset: 0x0004CD0B
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x0004EB13 File Offset: 0x0004CD13
		[MonoTODO("AssertUiEnabled defaults to False; should follow Environment.UserInteractive.")]
		public bool AssertUiEnabled
		{
			get
			{
				return this.assertUiEnabled;
			}
			set
			{
				this.assertUiEnabled = value;
			}
		}

		/// <summary>Gets or sets the name of a log file to write trace or debug messages to.</summary>
		/// <returns>The name of a log file to write trace or debug messages to.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0004EB1C File Offset: 0x0004CD1C
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x0004EB24 File Offset: 0x0004CD24
		[MonoTODO]
		public string LogFileName
		{
			get
			{
				return this.logFileName;
			}
			set
			{
				this.logFileName = value;
			}
		}

		/// <summary>Emits or displays a message and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001255 RID: 4693 RVA: 0x0004EB2D File Offset: 0x0004CD2D
		public override void Fail(string message)
		{
			base.Fail(message);
		}

		/// <summary>Emits or displays detailed messages and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <param name="detailMessage">The detailed message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001256 RID: 4694 RVA: 0x0004EB36 File Offset: 0x0004CD36
		public override void Fail(string message, string detailMessage)
		{
			base.Fail(message, detailMessage);
			if (this.ProcessUI(message, detailMessage) == DefaultTraceListener.DialogResult.Abort)
			{
				Thread.CurrentThread.Abort();
			}
			this.WriteLine(new StackTrace().ToString());
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004EB68 File Offset: 0x0004CD68
		private DefaultTraceListener.DialogResult ProcessUI(string message, string detailMessage)
		{
			if (!this.AssertUiEnabled)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			object obj;
			MethodInfo method;
			try
			{
				Assembly assembly = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				if (assembly == null)
				{
					return DefaultTraceListener.DialogResult.None;
				}
				Type type = assembly.GetType("System.Windows.Forms.MessageBoxButtons");
				obj = Enum.Parse(type, "AbortRetryIgnore");
				method = assembly.GetType("System.Windows.Forms.MessageBox").GetMethod("Show", new Type[]
				{
					typeof(string),
					typeof(string),
					type
				});
			}
			catch
			{
				return DefaultTraceListener.DialogResult.None;
			}
			if (method == null || obj == null)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			string text = string.Format("Assertion Failed: {0} to quit, {1} to debug, {2} to continue", "Abort", "Retry", "Ignore");
			string text2 = string.Format("{0}{1}{2}{1}{1}{3}", new object[]
			{
				message,
				Environment.NewLine,
				detailMessage,
				new StackTrace()
			});
			string text3 = method.Invoke(null, new object[] { text2, text, obj }).ToString();
			if (text3 == "Ignore")
			{
				return DefaultTraceListener.DialogResult.Ignore;
			}
			if (!(text3 == "Abort"))
			{
				return DefaultTraceListener.DialogResult.Retry;
			}
			return DefaultTraceListener.DialogResult.Abort;
		}

		// Token: 0x06001258 RID: 4696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteWindowsDebugString(char* message);

		// Token: 0x06001259 RID: 4697 RVA: 0x0004ECA8 File Offset: 0x0004CEA8
		private unsafe void WriteDebugString(string message)
		{
			if (DefaultTraceListener.OnWin32)
			{
				fixed (string text = message)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					DefaultTraceListener.WriteWindowsDebugString(ptr);
				}
				return;
			}
			this.WriteMonoTrace(message);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004ECDC File Offset: 0x0004CEDC
		private void WriteMonoTrace(string message)
		{
			string monoTraceFile = DefaultTraceListener.MonoTraceFile;
			if (monoTraceFile == "Console.Out")
			{
				Console.Out.Write(message);
				return;
			}
			if (!(monoTraceFile == "Console.Error"))
			{
				this.WriteLogFile(message, DefaultTraceListener.MonoTraceFile);
				return;
			}
			Console.Error.Write(message);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004ED2F File Offset: 0x0004CF2F
		private void WritePrefix()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				this.WriteMonoTrace(DefaultTraceListener.MonoTracePrefix);
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004ED43 File Offset: 0x0004CF43
		private void WriteImpl(string message)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
				this.WritePrefix();
			}
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
			}
			else
			{
				this.WriteDebugString(message);
			}
			this.WriteLogFile(message, this.LogFileName);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004ED80 File Offset: 0x0004CF80
		private void WriteLogFile(string message, string logFile)
		{
			if (logFile != null && logFile.Length != 0)
			{
				FileInfo fileInfo = new FileInfo(logFile);
				StreamWriter streamWriter = null;
				try
				{
					if (fileInfo.Exists)
					{
						streamWriter = fileInfo.AppendText();
					}
					else
					{
						streamWriter = fileInfo.CreateText();
					}
				}
				catch
				{
					return;
				}
				using (streamWriter)
				{
					streamWriter.Write(message);
					streamWriter.Flush();
				}
			}
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method.</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600125E RID: 4702 RVA: 0x0004EDF8 File Offset: 0x0004CFF8
		public override void Write(string message)
		{
			this.WriteImpl(message);
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method, followed by a carriage return and line feed (\r\n).</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600125F RID: 4703 RVA: 0x0004EE04 File Offset: 0x0004D004
		public override void WriteLine(string message)
		{
			string text = message + Environment.NewLine;
			this.WriteImpl(text);
			base.NeedIndent = true;
		}

		// Token: 0x04000ABB RID: 2747
		private static readonly bool OnWin32 = Path.DirectorySeparatorChar == '\\';

		// Token: 0x04000ABC RID: 2748
		private const string ConsoleOutTrace = "Console.Out";

		// Token: 0x04000ABD RID: 2749
		private const string ConsoleErrorTrace = "Console.Error";

		// Token: 0x04000ABE RID: 2750
		private static readonly string MonoTracePrefix;

		// Token: 0x04000ABF RID: 2751
		private static readonly string MonoTraceFile;

		// Token: 0x04000AC0 RID: 2752
		private string logFileName;

		// Token: 0x04000AC1 RID: 2753
		private bool assertUiEnabled;

		// Token: 0x02000256 RID: 598
		private enum DialogResult
		{
			// Token: 0x04000AC3 RID: 2755
			None,
			// Token: 0x04000AC4 RID: 2756
			Retry,
			// Token: 0x04000AC5 RID: 2757
			Ignore,
			// Token: 0x04000AC6 RID: 2758
			Abort
		}
	}
}
