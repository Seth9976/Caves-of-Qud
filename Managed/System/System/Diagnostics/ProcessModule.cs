using System;
using System.ComponentModel;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Represents a.dll or .exe file that is loaded into a particular process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027F RID: 639
	[Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessModule : Component
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x0005309B File Offset: 0x0005129B
		internal ProcessModule(IntPtr baseaddr, IntPtr entryaddr, string filename, FileVersionInfo version_info, int memory_size, string modulename)
		{
			this.baseaddr = baseaddr;
			this.entryaddr = entryaddr;
			this.filename = filename;
			this.version_info = version_info;
			this.memory_size = memory_size;
			this.modulename = modulename;
		}

		/// <summary>Gets the memory address where the module was loaded.</summary>
		/// <returns>The load address of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000530D0 File Offset: 0x000512D0
		[MonitoringDescription("The base memory address of this module")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.baseaddr;
			}
		}

		/// <summary>Gets the memory address for the function that runs when the system loads and runs the module.</summary>
		/// <returns>The entry point of the module.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x000530D8 File Offset: 0x000512D8
		[MonitoringDescription("The base memory address of the entry point of this module")]
		public IntPtr EntryPointAddress
		{
			get
			{
				return this.entryaddr;
			}
		}

		/// <summary>Gets the full path to the module.</summary>
		/// <returns>The fully qualified path that defines the location of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x000530E0 File Offset: 0x000512E0
		[MonitoringDescription("The file name of this module")]
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets version information about the module.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> that contains the module's version information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x000530E8 File Offset: 0x000512E8
		[Browsable(false)]
		public FileVersionInfo FileVersionInfo
		{
			get
			{
				return this.version_info;
			}
		}

		/// <summary>Gets the amount of memory that is required to load the module.</summary>
		/// <returns>The size, in bytes, of the memory that the module occupies.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x000530F0 File Offset: 0x000512F0
		[MonitoringDescription("The memory needed by this module")]
		public int ModuleMemorySize
		{
			get
			{
				return this.memory_size;
			}
		}

		/// <summary>Gets the name of the process module.</summary>
		/// <returns>The name of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x000530F8 File Offset: 0x000512F8
		[MonitoringDescription("The name of this module")]
		public string ModuleName
		{
			get
			{
				return this.modulename;
			}
		}

		/// <summary>Converts the name of the module to a string.</summary>
		/// <returns>The value of the <see cref="P:System.Diagnostics.ProcessModule.ModuleName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600144E RID: 5198 RVA: 0x00053100 File Offset: 0x00051300
		public override string ToString()
		{
			return this.ModuleName;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal ProcessModule()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B72 RID: 2930
		private IntPtr baseaddr;

		// Token: 0x04000B73 RID: 2931
		private IntPtr entryaddr;

		// Token: 0x04000B74 RID: 2932
		private string filename;

		// Token: 0x04000B75 RID: 2933
		private FileVersionInfo version_info;

		// Token: 0x04000B76 RID: 2934
		private int memory_size;

		// Token: 0x04000B77 RID: 2935
		private string modulename;
	}
}
