using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000515 RID: 1301
	[IODescription("")]
	[DefaultEvent("Changed")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x06002A13 RID: 10771 RVA: 0x00090B7C File Offset: 0x0008ED7C
		public FileSystemWatcher()
		{
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.enableRaisingEvents = false;
			this.filter = "*";
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.path = "";
			this.InitWatcher();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06002A14 RID: 10772 RVA: 0x00090BCC File Offset: 0x0008EDCC
		public FileSystemWatcher(string path)
			: this(path, "*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory and type of files to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.-or- The <paramref name="filter" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06002A15 RID: 10773 RVA: 0x00090BDC File Offset: 0x0008EDDC
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Empty path", "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException("Directory does not exist", "path");
			}
			this.inited = false;
			this.start_requested = false;
			this.enableRaisingEvents = false;
			this.filter = filter;
			if (this.filter == "*.*")
			{
				this.filter = "*";
			}
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.path = path;
			this.synchronizingObject = null;
			this.InitWatcher();
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00090CA8 File Offset: 0x0008EEA8
		[EnvironmentPermission(SecurityAction.Assert, Read = "MONO_MANAGED_WATCHER")]
		private void InitWatcher()
		{
			object obj = FileSystemWatcher.lockobj;
			lock (obj)
			{
				if (this.watcher_handle == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("MONO_MANAGED_WATCHER");
					int num = 0;
					bool flag2 = false;
					if (environmentVariable == null)
					{
						num = FileSystemWatcher.InternalSupportsFSW();
					}
					switch (num)
					{
					case 1:
						flag2 = DefaultWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 2:
						flag2 = FAMWatcher.GetInstance(out this.watcher, false);
						this.watcher_handle = this;
						break;
					case 3:
						flag2 = KeventWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 4:
						flag2 = FAMWatcher.GetInstance(out this.watcher, true);
						this.watcher_handle = this;
						break;
					case 6:
						flag2 = CoreFXFileSystemWatcherProxy.GetInstance(out this.watcher);
						this.watcher_handle = (this.watcher as CoreFXFileSystemWatcherProxy).NewWatcher(this);
						break;
					}
					if (num == 0 || !flag2)
					{
						if (string.Compare(environmentVariable, "disabled", true) == 0)
						{
							NullFileWatcher.GetInstance(out this.watcher);
						}
						else
						{
							DefaultWatcher.GetInstance(out this.watcher);
							this.watcher_handle = this;
						}
					}
					this.inited = true;
				}
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00090DF0 File Offset: 0x0008EFF0
		[Conditional("DEBUG")]
		[Conditional("TRACE")]
		private void ShowWatcherInfo()
		{
			Console.WriteLine("Watcher implementation: {0}", (this.watcher != null) ? this.watcher.GetType().ToString() : "<none>");
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x00090E1B File Offset: 0x0008F01B
		// (set) Token: 0x06002A19 RID: 10777 RVA: 0x00090E23 File Offset: 0x0008F023
		internal bool Waiting
		{
			get
			{
				return this.waiting;
			}
			set
			{
				this.waiting = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x00090E2C File Offset: 0x0008F02C
		internal string MangledFilter
		{
			get
			{
				if (this.filter != "*.*")
				{
					return this.filter;
				}
				if (this.mangledFilter != null)
				{
					return this.mangledFilter;
				}
				return "*.*";
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00090E5C File Offset: 0x0008F05C
		internal SearchPattern2 Pattern
		{
			get
			{
				if (this.pattern == null)
				{
					IFileWatcher fileWatcher = this.watcher;
					if (((fileWatcher != null) ? fileWatcher.GetType() : null) == typeof(KeventWatcher))
					{
						this.pattern = new SearchPattern2(this.MangledFilter, true);
					}
					else
					{
						this.pattern = new SearchPattern2(this.MangledFilter);
					}
				}
				return this.pattern;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x00090EC0 File Offset: 0x0008F0C0
		internal string FullPath
		{
			get
			{
				if (this.fullpath == null)
				{
					if (this.path == null || this.path == "")
					{
						this.fullpath = Environment.CurrentDirectory;
					}
					else
					{
						this.fullpath = global::System.IO.Path.GetFullPath(this.path);
					}
				}
				return this.fullpath;
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>true if the component is enabled; otherwise, false. The default is false. If you are using the component on a designer in Visual Studio 2005, the default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x00090F13 File Offset: 0x0008F113
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x00090F1C File Offset: 0x0008F11C
		[IODescription("Flag to indicate if this instance is active")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.start_requested = true;
				if (!this.inited)
				{
					return;
				}
				if (value == this.enableRaisingEvents)
				{
					return;
				}
				this.enableRaisingEvents = value;
				if (value)
				{
					this.Start();
					return;
				}
				this.Stop();
				this.start_requested = false;
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.) </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x00090F7A File Offset: 0x0008F17A
		// (set) Token: 0x06002A20 RID: 10784 RVA: 0x00090F84 File Offset: 0x0008F184
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[IODescription("File name filter pattern")]
		[DefaultValue("*.*")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null || value == "")
				{
					value = "*";
				}
				if (!string.Equals(this.filter, value, PathInternal.StringComparison))
				{
					this.filter = ((value == "*.*") ? "*" : value);
					this.pattern = null;
					this.mangledFilter = null;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>true if you want to monitor subdirectories; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x00090FE4 File Offset: 0x0008F1E4
		// (set) Token: 0x06002A22 RID: 10786 RVA: 0x00090FEC File Offset: 0x0008F1EC
		[IODescription("Flag to indicate we want to watch subdirectories")]
		[DefaultValue(false)]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories == value)
				{
					return;
				}
				this.includeSubdirectories = value;
				if (value && this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the size (in bytes) of the internal buffer.</summary>
		/// <returns>The internal buffer size in bytes. The default is 8192 (8 KB).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x00091016 File Offset: 0x0008F216
		// (set) Token: 0x06002A24 RID: 10788 RVA: 0x0009101E File Offset: 0x0008F21E
		[Browsable(false)]
		[DefaultValue(8192)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize == value)
				{
					return;
				}
				if (value < 4096)
				{
					value = 4096;
				}
				this.internalBufferSize = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of LastWrite, FileName, and DirectoryName.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x00091054 File Offset: 0x0008F254
		// (set) Token: 0x06002A26 RID: 10790 RVA: 0x0009105C File Offset: 0x0008F25C
		[DefaultValue(NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite)]
		[IODescription("Flag to indicate which change event we want to monitor")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilter;
			}
			set
			{
				if (this.notifyFilter == value)
				{
					return;
				}
				this.notifyFilter = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.-or- The specified path contains wildcard characters.-or- The specified path contains invalid path characters.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x00091083 File Offset: 0x0008F283
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x0009108C File Offset: 0x0008F28C
		[IODescription("The directory to monitor")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				value = ((value == null) ? string.Empty : value);
				if (string.Equals(this.path, value, PathInternal.StringComparison))
				{
					return;
				}
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = Directory.Exists(value);
				}
				catch (Exception ex)
				{
				}
				if (ex != null)
				{
					throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
				}
				if (!flag)
				{
					throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
				}
				this.path = value;
				this.fullpath = null;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x00091148 File Offset: 0x0008F348
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (this.Site != null && this.Site.DesignMode)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of a directory change.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> that represents the object used to marshal the event handler calls issued as a result of a directory change. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x0009116D File Offset: 0x0008F36D
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x00091175 File Offset: 0x0008F375
		[Browsable(false)]
		[IODescription("The object used to marshal the event handler calls resulting from a directory change")]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A2D RID: 10797 RVA: 0x0009117E File Offset: 0x0008F37E
		public void BeginInit()
		{
			this.inited = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002A2E RID: 10798 RVA: 0x00091188 File Offset: 0x0008F388
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			try
			{
				IFileWatcher fileWatcher = this.watcher;
				if (fileWatcher != null)
				{
					fileWatcher.StopDispatching(this.watcher_handle);
				}
				IFileWatcher fileWatcher2 = this.watcher;
				if (fileWatcher2 != null)
				{
					fileWatcher2.Dispose(this.watcher_handle);
				}
			}
			catch (Exception)
			{
			}
			this.watcher_handle = null;
			this.watcher = null;
			this.disposed = true;
			base.Dispose(disposing);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00091204 File Offset: 0x0008F404
		~FileSystemWatcher()
		{
			if (!this.disposed)
			{
				this.Dispose(false);
			}
		}

		/// <summary>Ends the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A30 RID: 10800 RVA: 0x0009123C File Offset: 0x0008F43C
		public void EndInit()
		{
			this.inited = true;
			if (this.start_requested)
			{
				this.EnableRaisingEvents = true;
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00091254 File Offset: 0x0008F454
		private void RaiseEvent(Delegate ev, EventArgs arg, FileSystemWatcher.EventType evtype)
		{
			if (this.disposed)
			{
				return;
			}
			if (ev == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				foreach (Delegate @delegate in ev.GetInvocationList())
				{
					switch (evtype)
					{
					case FileSystemWatcher.EventType.FileSystemEvent:
						((FileSystemEventHandler)@delegate)(this, (FileSystemEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.ErrorEvent:
						((ErrorEventHandler)@delegate)(this, (ErrorEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.RenameEvent:
						((RenamedEventHandler)@delegate)(this, (RenamedEventArgs)arg);
						break;
					}
				}
				return;
			}
			this.synchronizingObject.BeginInvoke(ev, new object[] { this, arg });
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06002A32 RID: 10802 RVA: 0x000912F9 File Offset: 0x0008F4F9
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Changed, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06002A33 RID: 10803 RVA: 0x00091309 File Offset: 0x0008F509
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Created, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06002A34 RID: 10804 RVA: 0x00091319 File Offset: 0x0008F519
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Deleted, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x06002A35 RID: 10805 RVA: 0x00091329 File Offset: 0x0008F529
		protected void OnError(ErrorEventArgs e)
		{
			this.RaiseEvent(this.Error, e, FileSystemWatcher.EventType.ErrorEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002A36 RID: 10806 RVA: 0x00091339 File Offset: 0x0008F539
		protected void OnRenamed(RenamedEventArgs e)
		{
			this.RaiseEvent(this.Renamed, e, FileSystemWatcher.EventType.RenameEvent);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A37 RID: 10807 RVA: 0x00091349 File Offset: 0x0008F549
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <param name="timeout">The time (in milliseconds) to wait before timing out. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A38 RID: 10808 RVA: 0x00091354 File Offset: 0x0008F554
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			WaitForChangedResult waitForChangedResult = default(WaitForChangedResult);
			bool flag = this.EnableRaisingEvents;
			if (!flag)
			{
				this.EnableRaisingEvents = true;
			}
			bool flag3;
			lock (this)
			{
				this.waiting = true;
				flag3 = Monitor.Wait(this, timeout);
				if (flag3)
				{
					waitForChangedResult = this.lastData;
				}
			}
			this.EnableRaisingEvents = flag;
			if (!flag3)
			{
				waitForChangedResult.TimedOut = true;
			}
			return waitForChangedResult;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000913D0 File Offset: 0x0008F5D0
		internal void DispatchErrorEvents(ErrorEventArgs args)
		{
			if (this.disposed)
			{
				return;
			}
			this.OnError(args);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000913E4 File Offset: 0x0008F5E4
		internal void DispatchEvents(FileAction act, string filename, ref RenamedEventArgs renamed)
		{
			FileSystemWatcher.<>c__DisplayClass70_0 CS$<>8__locals1 = new FileSystemWatcher.<>c__DisplayClass70_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.filename = filename;
			if (this.disposed)
			{
				return;
			}
			if (this.waiting)
			{
				this.lastData = default(WaitForChangedResult);
			}
			switch (act)
			{
			case FileAction.Added:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Created;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Removed:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Deleted;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Modified:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Changed;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.RenamedOldName:
				if (renamed != null)
				{
					this.OnRenamed(renamed);
				}
				this.lastData.OldName = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, CS$<>8__locals1.filename, "");
				return;
			case FileAction.RenamedNewName:
			{
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				if (renamed == null)
				{
					renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, "", CS$<>8__locals1.filename);
				}
				RenamedEventArgs renamed_ref = renamed;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnRenamed(renamed_ref);
				});
				renamed = null;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000915A8 File Offset: 0x0008F7A8
		private void Start()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StartDispatching(this.watcher_handle);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000915D2 File Offset: 0x0008F7D2
		private void Stop()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StopDispatching(this.watcher_handle);
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002A3D RID: 10813 RVA: 0x000915FC File Offset: 0x0008F7FC
		// (remove) Token: 0x06002A3E RID: 10814 RVA: 0x00091634 File Offset: 0x0008F834
		[IODescription("Occurs when a file/directory change matches the filter")]
		public event FileSystemEventHandler Changed;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002A3F RID: 10815 RVA: 0x0009166C File Offset: 0x0008F86C
		// (remove) Token: 0x06002A40 RID: 10816 RVA: 0x000916A4 File Offset: 0x0008F8A4
		[IODescription("Occurs when a file/directory creation matches the filter")]
		public event FileSystemEventHandler Created;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002A41 RID: 10817 RVA: 0x000916DC File Offset: 0x0008F8DC
		// (remove) Token: 0x06002A42 RID: 10818 RVA: 0x00091714 File Offset: 0x0008F914
		[IODescription("Occurs when a file/directory deletion matches the filter")]
		public event FileSystemEventHandler Deleted;

		/// <summary>Occurs when the instance of <see cref="T:System.IO.FileSystemWatcher" /> is unable to continue monitoring changes or when the internal buffer overflows.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002A43 RID: 10819 RVA: 0x0009174C File Offset: 0x0008F94C
		// (remove) Token: 0x06002A44 RID: 10820 RVA: 0x00091784 File Offset: 0x0008F984
		[Browsable(false)]
		public event ErrorEventHandler Error;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is renamed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002A45 RID: 10821 RVA: 0x000917BC File Offset: 0x0008F9BC
		// (remove) Token: 0x06002A46 RID: 10822 RVA: 0x000917F4 File Offset: 0x0008F9F4
		[IODescription("Occurs when a file/directory rename matches the filter")]
		public event RenamedEventHandler Renamed;

		// Token: 0x06002A47 RID: 10823
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalSupportsFSW();

		// Token: 0x0400167D RID: 5757
		private bool inited;

		// Token: 0x0400167E RID: 5758
		private bool start_requested;

		// Token: 0x0400167F RID: 5759
		private bool enableRaisingEvents;

		// Token: 0x04001680 RID: 5760
		private string filter;

		// Token: 0x04001681 RID: 5761
		private bool includeSubdirectories;

		// Token: 0x04001682 RID: 5762
		private int internalBufferSize;

		// Token: 0x04001683 RID: 5763
		private NotifyFilters notifyFilter;

		// Token: 0x04001684 RID: 5764
		private string path;

		// Token: 0x04001685 RID: 5765
		private string fullpath;

		// Token: 0x04001686 RID: 5766
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04001687 RID: 5767
		private WaitForChangedResult lastData;

		// Token: 0x04001688 RID: 5768
		private bool waiting;

		// Token: 0x04001689 RID: 5769
		private SearchPattern2 pattern;

		// Token: 0x0400168A RID: 5770
		private bool disposed;

		// Token: 0x0400168B RID: 5771
		private string mangledFilter;

		// Token: 0x0400168C RID: 5772
		private IFileWatcher watcher;

		// Token: 0x0400168D RID: 5773
		private object watcher_handle;

		// Token: 0x0400168E RID: 5774
		private static object lockobj = new object();

		// Token: 0x02000516 RID: 1302
		private enum EventType
		{
			// Token: 0x04001695 RID: 5781
			FileSystemEvent,
			// Token: 0x04001696 RID: 5782
			ErrorEvent,
			// Token: 0x04001697 RID: 5783
			RenameEvent
		}
	}
}
