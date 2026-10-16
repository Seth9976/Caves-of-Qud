using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Specifies a set of values that are used when you start a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000248 RID: 584
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, SelfAffectingProcessMgmt = true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProcessStartInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class without specifying a file name with which to start the process.</summary>
		// Token: 0x060011DB RID: 4571 RVA: 0x0004DD90 File Offset: 0x0004BF90
		public ProcessStartInfo()
		{
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004DD9F File Offset: 0x0004BF9F
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class and specifies a file name such as an application or document with which to start the process.</summary>
		/// <param name="fileName">An application or document with which to start a process. </param>
		// Token: 0x060011DD RID: 4573 RVA: 0x0004DDBA File Offset: 0x0004BFBA
		public ProcessStartInfo(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.</summary>
		/// <param name="fileName">An application with which to start a process. </param>
		/// <param name="arguments">Command-line arguments to pass to the application when the process starts. </param>
		// Token: 0x060011DE RID: 4574 RVA: 0x0004DDD0 File Offset: 0x0004BFD0
		public ProcessStartInfo(string fileName, string arguments)
		{
			this.fileName = fileName;
			this.arguments = arguments;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004DDED File Offset: 0x0004BFED
		public Collection<string> ArgumentList
		{
			get
			{
				if (this._argumentList == null)
				{
					this._argumentList = new Collection<string>();
				}
				return this._argumentList;
			}
		}

		/// <summary>Gets or sets the verb to use when opening the application or document specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The action to take with the file that the process opens. The default is an empty string (""), which signifies no action.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004DE08 File Offset: 0x0004C008
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004DE1E File Offset: 0x0004C01E
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The verb to apply to the document specified by the FileName property.")]
		[NotifyParentProperty(true)]
		public string Verb
		{
			get
			{
				if (this.verb == null)
				{
					return string.Empty;
				}
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		/// <summary>Gets or sets the set of command-line arguments to use when starting the application.</summary>
		/// <returns>File type–specific arguments that the system can associate with the application specified in the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property. The default is an empty string (""). On Windows Vista and earlier versions of the Windows operating system, the length of the arguments added to the length of the full path to the process must be less than 2080. On Windows 7 and later versions, the length must be less than 32699.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004DE27 File Offset: 0x0004C027
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x0004DE3D File Offset: 0x0004C03D
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[SettingsBindable(true)]
		[DefaultValue("")]
		[MonitoringDescription("Command line arguments that will be passed to the application specified by the FileName property.")]
		public string Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					return string.Empty;
				}
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to start the process in a new window.</summary>
		/// <returns>true if the process should be started without creating a new window to contain it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004DE46 File Offset: 0x0004C046
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x0004DE4E File Offset: 0x0004C04E
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("Whether to start the process without creating a new window to contain it.")]
		public bool CreateNoWindow
		{
			get
			{
				return this.createNoWindow;
			}
			set
			{
				this.createNoWindow = value;
			}
		}

		/// <summary>Gets search paths for files, directories for temporary files, application-specific options, and other similar information.</summary>
		/// <returns>A string dictionary that provides environment variables that apply to this process and child processes. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004DE58 File Offset: 0x0004C058
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[MonitoringDescription("Set of environment variables that apply to this process and child processes.")]
		[NotifyParentProperty(true)]
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new CaseSensitiveStringDictionary();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in global::System.Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0004DF30 File Offset: 0x0004C130
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDictionary<string, string> Environment
		{
			get
			{
				if (this.environment == null)
				{
					this.environment = this.EnvironmentVariables.AsGenericDictionary();
				}
				return this.environment;
			}
		}

		/// <summary>Gets or sets a value indicating whether the input for an application is read from the <see cref="P:System.Diagnostics.Process.StandardInput" /> stream.</summary>
		/// <returns>true if input should be read from <see cref="P:System.Diagnostics.Process.StandardInput" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004DF51 File Offset: 0x0004C151
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0004DF59 File Offset: 0x0004C159
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("Whether the process command input is read from the Process instance's StandardInput member.")]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirectStandardInput;
			}
			set
			{
				this.redirectStandardInput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <returns>true if output should be written to <see cref="P:System.Diagnostics.Process.StandardOutput" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004DF62 File Offset: 0x0004C162
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0004DF6A File Offset: 0x0004C16A
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether the process output is written to the Process instance's StandardOutput member.")]
		[DefaultValue(false)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirectStandardOutput;
			}
			set
			{
				this.redirectStandardOutput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <returns>true if error output should be written to <see cref="P:System.Diagnostics.Process.StandardError" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004DF73 File Offset: 0x0004C173
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0004DF7B File Offset: 0x0004C17B
		[MonitoringDescription("Whether the process's error output is written to the Process instance's StandardError member.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirectStandardError;
			}
			set
			{
				this.redirectStandardError = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for error output.</summary>
		/// <returns>An object that represents the preferred encoding for error output. The default is null.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004DF84 File Offset: 0x0004C184
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0004DF8C File Offset: 0x0004C18C
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.standardErrorEncoding;
			}
			set
			{
				this.standardErrorEncoding = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for standard output.</summary>
		/// <returns>An object that represents the preferred encoding for standard output. The default is null.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004DF95 File Offset: 0x0004C195
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004DF9D File Offset: 0x0004C19D
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.standardOutputEncoding;
			}
			set
			{
				this.standardOutputEncoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the operating system shell to start the process.</summary>
		/// <returns>true if the shell should be used when starting the process; false if the process should be created directly from the executable file. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004DFA6 File Offset: 0x0004C1A6
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0004DFAE File Offset: 0x0004C1AE
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether to use the operating system shell to start the process.")]
		[DefaultValue(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.useShellExecute;
			}
			set
			{
				this.useShellExecute = value;
			}
		}

		/// <summary>Gets or sets the user name to be used when starting the process.</summary>
		/// <returns>The user name to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004DFB7 File Offset: 0x0004C1B7
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0004DFCD File Offset: 0x0004C1CD
		[NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					return string.Empty;
				}
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		/// <summary>Gets or sets a secure string that contains the user password to use when starting the process.</summary>
		/// <returns>The user password to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004DFD6 File Offset: 0x0004C1D6
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x0004DFDE File Offset: 0x0004C1DE
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004DFE7 File Offset: 0x0004C1E7
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0004DFEF File Offset: 0x0004C1EF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PasswordInClearText
		{
			get
			{
				return this.passwordInClearText;
			}
			set
			{
				this.passwordInClearText = value;
			}
		}

		/// <summary>Gets or sets a value that identifies the domain to use when starting the process. </summary>
		/// <returns>The Active Directory domain to use when starting the process. The domain property is primarily of interest to users within enterprise environments that use Active Directory.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004DFF8 File Offset: 0x0004C1F8
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0004E00E File Offset: 0x0004C20E
		[NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				if (this.domain == null)
				{
					return string.Empty;
				}
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the Windows user profile is to be loaded from the registry. </summary>
		/// <returns>true if the Windows user profile should be loaded; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004E017 File Offset: 0x0004C217
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x0004E01F File Offset: 0x0004C21F
		[NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.loadUserProfile;
			}
			set
			{
				this.loadUserProfile = value;
			}
		}

		/// <summary>Gets or sets the application or document to start.</summary>
		/// <returns>The name of the application to start, or the name of a document of a file type that is associated with an application and that has a default open action available to it. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004E028 File Offset: 0x0004C228
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0004E03E File Offset: 0x0004C23E
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The name of the application, document or URL to start.")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is false, gets or sets the working directory for the process to be started. When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is true, gets or sets the directory that contains the process to be started.</summary>
		/// <returns>When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is true, the fully qualified name of the directory that contains the process to be started. When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is false, the working directory for the process to be started. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004E047 File Offset: 0x0004C247
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x0004E05D File Offset: 0x0004C25D
		[MonitoringDescription("The initial working directory for the process.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string WorkingDirectory
		{
			get
			{
				if (this.directory == null)
				{
					return string.Empty;
				}
				return this.directory;
			}
			set
			{
				this.directory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an error dialog box is displayed to the user if the process cannot be started.</summary>
		/// <returns>true if an error dialog box should be displayed on the screen if the process cannot be started; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004E066 File Offset: 0x0004C266
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0004E06E File Offset: 0x0004C26E
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether to show an error dialog to the user if there is an error.")]
		public bool ErrorDialog
		{
			get
			{
				return this.errorDialog;
			}
			set
			{
				this.errorDialog = value;
			}
		}

		/// <summary>Gets or sets the window handle to use when an error dialog box is shown for a process that cannot be started.</summary>
		/// <returns>A pointer to the handle of the error dialog box that results from a process start failure.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0004E077 File Offset: 0x0004C277
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0004E07F File Offset: 0x0004C27F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr ErrorDialogParentHandle
		{
			get
			{
				return this.errorDialogParentHandle;
			}
			set
			{
				this.errorDialogParentHandle = value;
			}
		}

		/// <summary>Gets or sets the window state to use when the process is started.</summary>
		/// <returns>One of the enumeration values that indicates whether the process is started in a window that is maximized, minimized, normal (neither maximized nor minimized), or not visible. The default is Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The window style is not one of the <see cref="T:System.Diagnostics.ProcessWindowStyle" /> enumeration members. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0004E088 File Offset: 0x0004C288
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x0004E090 File Offset: 0x0004C290
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("How the main window should be created when the process starts.")]
		[NotifyParentProperty(true)]
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				return this.windowStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessWindowStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessWindowStyle));
				}
				this.windowStyle = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0004E0C6 File Offset: 0x0004C2C6
		internal bool HaveEnvVars
		{
			get
			{
				return this.environmentVariables != null;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0004E0D1 File Offset: 0x0004C2D1
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x0004E0D9 File Offset: 0x0004C2D9
		public Encoding StandardInputEncoding { get; set; }

		/// <summary>Gets the set of verbs associated with the type of file specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The actions that the system can apply to the file indicated by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0004E0E4 File Offset: 0x0004C2E4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] Verbs
		{
			get
			{
				PlatformID platform = global::System.Environment.OSVersion.Platform;
				if (platform == PlatformID.Unix || platform == PlatformID.MacOSX || platform == (PlatformID)128)
				{
					return ProcessStartInfo.empty;
				}
				string text = (string.IsNullOrEmpty(this.fileName) ? null : Path.GetExtension(this.fileName));
				if (text == null)
				{
					return ProcessStartInfo.empty;
				}
				RegistryKey registryKey = null;
				RegistryKey registryKey2 = null;
				RegistryKey registryKey3 = null;
				string[] array;
				try
				{
					registryKey = Registry.ClassesRoot.OpenSubKey(text);
					string text2 = ((registryKey != null) ? (registryKey.GetValue(null) as string) : null);
					registryKey2 = ((text2 != null) ? Registry.ClassesRoot.OpenSubKey(text2) : null);
					registryKey3 = ((registryKey2 != null) ? registryKey2.OpenSubKey("shell") : null);
					array = ((registryKey3 != null) ? registryKey3.GetSubKeyNames() : null);
				}
				finally
				{
					if (registryKey3 != null)
					{
						registryKey3.Close();
					}
					if (registryKey2 != null)
					{
						registryKey2.Close();
					}
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
				return array;
			}
		}

		// Token: 0x04000A94 RID: 2708
		private string fileName;

		// Token: 0x04000A95 RID: 2709
		private string arguments;

		// Token: 0x04000A96 RID: 2710
		private string directory;

		// Token: 0x04000A97 RID: 2711
		private string verb;

		// Token: 0x04000A98 RID: 2712
		private ProcessWindowStyle windowStyle;

		// Token: 0x04000A99 RID: 2713
		private bool errorDialog;

		// Token: 0x04000A9A RID: 2714
		private IntPtr errorDialogParentHandle;

		// Token: 0x04000A9B RID: 2715
		private bool useShellExecute = true;

		// Token: 0x04000A9C RID: 2716
		private string userName;

		// Token: 0x04000A9D RID: 2717
		private string domain;

		// Token: 0x04000A9E RID: 2718
		private SecureString password;

		// Token: 0x04000A9F RID: 2719
		private string passwordInClearText;

		// Token: 0x04000AA0 RID: 2720
		private bool loadUserProfile;

		// Token: 0x04000AA1 RID: 2721
		private bool redirectStandardInput;

		// Token: 0x04000AA2 RID: 2722
		private bool redirectStandardOutput;

		// Token: 0x04000AA3 RID: 2723
		private bool redirectStandardError;

		// Token: 0x04000AA4 RID: 2724
		private Encoding standardOutputEncoding;

		// Token: 0x04000AA5 RID: 2725
		private Encoding standardErrorEncoding;

		// Token: 0x04000AA6 RID: 2726
		private bool createNoWindow;

		// Token: 0x04000AA7 RID: 2727
		private WeakReference weakParentProcess;

		// Token: 0x04000AA8 RID: 2728
		internal StringDictionary environmentVariables;

		// Token: 0x04000AA9 RID: 2729
		private static readonly string[] empty = new string[0];

		// Token: 0x04000AAA RID: 2730
		private Collection<string> _argumentList;

		// Token: 0x04000AAB RID: 2731
		private IDictionary<string, string> environment;
	}
}
