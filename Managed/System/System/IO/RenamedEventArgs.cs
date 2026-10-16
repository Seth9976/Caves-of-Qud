using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004FB RID: 1275
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values. </param>
		/// <param name="directory">The name of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		/// <param name="oldName">The old name of the affected file or directory. </param>
		// Token: 0x060029AB RID: 10667 RVA: 0x0008F1A4 File Offset: 0x0008D3A4
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName)
			: base(changeType, directory, name)
		{
			this._oldName = oldName;
			this._oldFullPath = FileSystemEventArgs.Combine(directory, oldName);
		}

		/// <summary>Gets the previous fully qualified path of the affected file or directory.</summary>
		/// <returns>The previous fully qualified path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x0008F1C5 File Offset: 0x0008D3C5
		public string OldFullPath
		{
			get
			{
				return this._oldFullPath;
			}
		}

		/// <summary>Gets the old name of the affected file or directory.</summary>
		/// <returns>The previous name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x0008F1CD File Offset: 0x0008D3CD
		public string OldName
		{
			get
			{
				return this._oldName;
			}
		}

		// Token: 0x0400161A RID: 5658
		private readonly string _oldName;

		// Token: 0x0400161B RID: 5659
		private readonly string _oldFullPath;
	}
}
