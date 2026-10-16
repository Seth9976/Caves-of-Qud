using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004F5 RID: 1269
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system. </param>
		/// <param name="directory">The root directory of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		// Token: 0x0600296E RID: 10606 RVA: 0x0008E8C8 File Offset: 0x0008CAC8
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this._changeType = changeType;
			this._name = name;
			this._fullPath = Path.GetFullPath(FileSystemEventArgs.Combine(directory, name));
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0008E8F0 File Offset: 0x0008CAF0
		internal static string Combine(string directoryPath, string name)
		{
			bool flag = false;
			if (directoryPath.Length > 0)
			{
				char c = directoryPath[directoryPath.Length - 1];
				flag = c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
			}
			if (!flag)
			{
				return directoryPath + Path.DirectorySeparatorChar.ToString() + name;
			}
			return directoryPath + name;
		}

		/// <summary>Gets the type of directory event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values that represents the kind of change detected in the file system.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x0008E947 File Offset: 0x0008CB47
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this._changeType;
			}
		}

		/// <summary>Gets the fully qualifed path of the affected file or directory.</summary>
		/// <returns>The path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x0008E94F File Offset: 0x0008CB4F
		public string FullPath
		{
			get
			{
				return this._fullPath;
			}
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x0008E957 File Offset: 0x0008CB57
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04001605 RID: 5637
		private readonly WatcherChangeTypes _changeType;

		// Token: 0x04001606 RID: 5638
		private readonly string _name;

		// Token: 0x04001607 RID: 5639
		private readonly string _fullPath;
	}
}
