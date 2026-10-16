using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Enumeration;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO.CoreFX
{
	// Token: 0x02000541 RID: 1345
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x000945E8 File Offset: 0x000927E8
		private void StartRaisingEvents()
		{
			if (this.IsSuspended())
			{
				this._enabled = true;
				return;
			}
			if (!FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				return;
			}
			this._directoryHandle = global::Interop.Kernel32.CreateFile(this._directory, 1, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, 1107296256);
			if (FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				this._directoryHandle = null;
				throw new FileNotFoundException(SR.Format("Error reading the {0} directory.", this._directory));
			}
			FileSystemWatcher.AsyncReadState asyncReadState;
			try
			{
				int num = Interlocked.Increment(ref this._currentSession);
				byte[] array = this.AllocateBuffer();
				asyncReadState = new FileSystemWatcher.AsyncReadState(num, array, this._directoryHandle, ThreadPoolBoundHandle.BindHandle(this._directoryHandle));
				asyncReadState.PreAllocatedOverlapped = new PreAllocatedOverlapped(new IOCompletionCallback(this.ReadDirectoryChangesCallback), asyncReadState, array);
			}
			catch
			{
				this._directoryHandle.Dispose();
				this._directoryHandle = null;
				throw;
			}
			this._enabled = true;
			this.Monitor(asyncReadState);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000946D0 File Offset: 0x000928D0
		private void StopRaisingEvents()
		{
			this._enabled = false;
			if (this.IsSuspended())
			{
				return;
			}
			if (FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				return;
			}
			Interlocked.Increment(ref this._currentSession);
			this._directoryHandle.Dispose();
			this._directoryHandle = null;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0009470E File Offset: 0x0009290E
		private void FinalizeDispose()
		{
			if (!FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				this._directoryHandle.Dispose();
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x00094728 File Offset: 0x00092928
		private static bool IsHandleInvalid(SafeFileHandle handle)
		{
			return handle == null || handle.IsInvalid || handle.IsClosed;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x00094740 File Offset: 0x00092940
		private unsafe void Monitor(FileSystemWatcher.AsyncReadState state)
		{
			NativeOverlapped* ptr = null;
			bool flag = false;
			try
			{
				if (this._enabled && !FileSystemWatcher.IsHandleInvalid(state.DirectoryHandle))
				{
					ptr = state.ThreadPoolBinding.AllocateNativeOverlapped(state.PreAllocatedOverlapped);
					int num;
					flag = global::Interop.Kernel32.ReadDirectoryChangesW(state.DirectoryHandle, state.Buffer, this._internalBufferSize, this._includeSubdirectories, (int)this._notifyFilters, out num, ptr, IntPtr.Zero);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (ArgumentNullException)
			{
			}
			finally
			{
				if (!flag)
				{
					if (ptr != null)
					{
						state.ThreadPoolBinding.FreeNativeOverlapped(ptr);
					}
					state.PreAllocatedOverlapped.Dispose();
					state.ThreadPoolBinding.Dispose();
					if (!FileSystemWatcher.IsHandleInvalid(state.DirectoryHandle))
					{
						this.OnError(new ErrorEventArgs(new Win32Exception()));
					}
				}
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x00094824 File Offset: 0x00092A24
		private unsafe void ReadDirectoryChangesCallback(uint errorCode, uint numBytes, NativeOverlapped* overlappedPointer)
		{
			FileSystemWatcher.AsyncReadState asyncReadState = (FileSystemWatcher.AsyncReadState)ThreadPoolBoundHandle.GetNativeOverlappedState(overlappedPointer);
			try
			{
				if (!FileSystemWatcher.IsHandleInvalid(asyncReadState.DirectoryHandle))
				{
					if (errorCode != 0U)
					{
						if (errorCode != 995U)
						{
							this.OnError(new ErrorEventArgs(new Win32Exception((int)errorCode)));
							this.EnableRaisingEvents = false;
						}
					}
					else if (asyncReadState.Session == Volatile.Read(ref this._currentSession))
					{
						if (numBytes == 0U)
						{
							this.NotifyInternalBufferOverflowEvent();
						}
						else
						{
							this.ParseEventBufferAndNotifyForEach(asyncReadState.Buffer);
						}
					}
				}
			}
			finally
			{
				asyncReadState.ThreadPoolBinding.FreeNativeOverlapped(overlappedPointer);
				this.Monitor(asyncReadState);
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000948C4 File Offset: 0x00092AC4
		private unsafe void ParseEventBufferAndNotifyForEach(byte[] buffer)
		{
			int num = 0;
			string text = null;
			int num2;
			do
			{
				int num3;
				string text2;
				fixed (byte* ptr = &buffer[0])
				{
					byte* ptr2 = ptr;
					num2 = *(int*)(ptr2 + num);
					num3 = *(int*)(ptr2 + num + 4);
					int num4 = *(int*)(ptr2 + num + 8);
					text2 = new string((char*)(ptr2 + num + 12), 0, num4 / 2);
				}
				if (num3 == 4)
				{
					text = text2;
				}
				else if (num3 == 5)
				{
					this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
					text = null;
				}
				else
				{
					if (text != null)
					{
						this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
						text = null;
					}
					switch (num3)
					{
					case 1:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Created, text2);
						break;
					case 2:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Deleted, text2);
						break;
					case 3:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Changed, text2);
						break;
					}
				}
				num += num2;
			}
			while (num2 != 0);
			if (text != null)
			{
				this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
			}
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000949A9 File Offset: 0x00092BA9
		public FileSystemWatcher()
		{
			this._directory = string.Empty;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000949DA File Offset: 0x00092BDA
		public FileSystemWatcher(string path)
		{
			FileSystemWatcher.CheckPathValidity(path);
			this._directory = path;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x00094A10 File Offset: 0x00092C10
		public FileSystemWatcher(string path, string filter)
		{
			FileSystemWatcher.CheckPathValidity(path);
			this._directory = path;
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			this.Filter = filter;
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x00094A64 File Offset: 0x00092C64
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x00094A6C File Offset: 0x00092C6C
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this._notifyFilters;
			}
			set
			{
				if ((value & ~(NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size)) != (NotifyFilters)0)
				{
					throw new ArgumentException(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", "value", (int)value, "NotifyFilters"));
				}
				if (this._notifyFilters != value)
				{
					this._notifyFilters = value;
					this.Restart();
				}
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x00094AB8 File Offset: 0x00092CB8
		public Collection<string> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x00094AC0 File Offset: 0x00092CC0
		// (set) Token: 0x06002B76 RID: 11126 RVA: 0x00094AC8 File Offset: 0x00092CC8
		public bool EnableRaisingEvents
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled == value)
				{
					return;
				}
				if (this.IsSuspended())
				{
					this._enabled = value;
					return;
				}
				if (value)
				{
					this.StartRaisingEventsIfNotDisposed();
					return;
				}
				this.StopRaisingEvents();
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x00094AF4 File Offset: 0x00092CF4
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x00094B15 File Offset: 0x00092D15
		public string Filter
		{
			get
			{
				if (this.Filters.Count != 0)
				{
					return this.Filters[0];
				}
				return "*";
			}
			set
			{
				this.Filters.Clear();
				this.Filters.Add(value);
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x00094B2E File Offset: 0x00092D2E
		// (set) Token: 0x06002B7A RID: 11130 RVA: 0x00094B36 File Offset: 0x00092D36
		public bool IncludeSubdirectories
		{
			get
			{
				return this._includeSubdirectories;
			}
			set
			{
				if (this._includeSubdirectories != value)
				{
					this._includeSubdirectories = value;
					this.Restart();
				}
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x00094B4E File Offset: 0x00092D4E
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x00094B56 File Offset: 0x00092D56
		public int InternalBufferSize
		{
			get
			{
				return (int)this._internalBufferSize;
			}
			set
			{
				if ((ulong)this._internalBufferSize != (ulong)((long)value))
				{
					if (value < 4096)
					{
						this._internalBufferSize = 4096U;
					}
					else
					{
						this._internalBufferSize = (uint)value;
					}
					this.Restart();
				}
			}
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00094B88 File Offset: 0x00092D88
		private byte[] AllocateBuffer()
		{
			byte[] array;
			try
			{
				array = new byte[this._internalBufferSize];
			}
			catch (OutOfMemoryException)
			{
				throw new OutOfMemoryException(SR.Format("The specified buffer size is too large. FileSystemWatcher cannot allocate {0} bytes for the internal buffer.", this._internalBufferSize));
			}
			return array;
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x00094BD0 File Offset: 0x00092DD0
		// (set) Token: 0x06002B7F RID: 11135 RVA: 0x00094BD8 File Offset: 0x00092DD8
		public string Path
		{
			get
			{
				return this._directory;
			}
			set
			{
				value = ((value == null) ? string.Empty : value);
				if (!string.Equals(this._directory, value, PathInternal.StringComparison))
				{
					if (value.Length == 0)
					{
						throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
					}
					if (!Directory.Exists(value))
					{
						throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
					}
					this._directory = value;
					this.Restart();
				}
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06002B80 RID: 11136 RVA: 0x00094C4E File Offset: 0x00092E4E
		// (remove) Token: 0x06002B81 RID: 11137 RVA: 0x00094C67 File Offset: 0x00092E67
		public event FileSystemEventHandler Changed
		{
			add
			{
				this._onChangedHandler = (FileSystemEventHandler)Delegate.Combine(this._onChangedHandler, value);
			}
			remove
			{
				this._onChangedHandler = (FileSystemEventHandler)Delegate.Remove(this._onChangedHandler, value);
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06002B82 RID: 11138 RVA: 0x00094C80 File Offset: 0x00092E80
		// (remove) Token: 0x06002B83 RID: 11139 RVA: 0x00094C99 File Offset: 0x00092E99
		public event FileSystemEventHandler Created
		{
			add
			{
				this._onCreatedHandler = (FileSystemEventHandler)Delegate.Combine(this._onCreatedHandler, value);
			}
			remove
			{
				this._onCreatedHandler = (FileSystemEventHandler)Delegate.Remove(this._onCreatedHandler, value);
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06002B84 RID: 11140 RVA: 0x00094CB2 File Offset: 0x00092EB2
		// (remove) Token: 0x06002B85 RID: 11141 RVA: 0x00094CCB File Offset: 0x00092ECB
		public event FileSystemEventHandler Deleted
		{
			add
			{
				this._onDeletedHandler = (FileSystemEventHandler)Delegate.Combine(this._onDeletedHandler, value);
			}
			remove
			{
				this._onDeletedHandler = (FileSystemEventHandler)Delegate.Remove(this._onDeletedHandler, value);
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06002B86 RID: 11142 RVA: 0x00094CE4 File Offset: 0x00092EE4
		// (remove) Token: 0x06002B87 RID: 11143 RVA: 0x00094CFD File Offset: 0x00092EFD
		public event ErrorEventHandler Error
		{
			add
			{
				this._onErrorHandler = (ErrorEventHandler)Delegate.Combine(this._onErrorHandler, value);
			}
			remove
			{
				this._onErrorHandler = (ErrorEventHandler)Delegate.Remove(this._onErrorHandler, value);
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06002B88 RID: 11144 RVA: 0x00094D16 File Offset: 0x00092F16
		// (remove) Token: 0x06002B89 RID: 11145 RVA: 0x00094D2F File Offset: 0x00092F2F
		public event RenamedEventHandler Renamed
		{
			add
			{
				this._onRenamedHandler = (RenamedEventHandler)Delegate.Combine(this._onRenamedHandler, value);
			}
			remove
			{
				this._onRenamedHandler = (RenamedEventHandler)Delegate.Remove(this._onRenamedHandler, value);
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00094D48 File Offset: 0x00092F48
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.StopRaisingEvents();
					this._onChangedHandler = null;
					this._onCreatedHandler = null;
					this._onDeletedHandler = null;
					this._onRenamedHandler = null;
					this._onErrorHandler = null;
				}
				else
				{
					this.FinalizeDispose();
				}
			}
			finally
			{
				this._disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00094DAC File Offset: 0x00092FAC
		private static void CheckPathValidity(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(SR.Format("The directory name {0} is invalid.", path), "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", path), "path");
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00094E04 File Offset: 0x00093004
		private bool MatchPattern(ReadOnlySpan<char> relativePath)
		{
			if (relativePath.IsWhiteSpace())
			{
				return false;
			}
			ReadOnlySpan<char> fileName = global::System.IO.Path.GetFileName(relativePath);
			if (fileName.Length == 0)
			{
				return false;
			}
			string[] filters = this._filters.GetFilters();
			if (filters.Length == 0)
			{
				return true;
			}
			string[] array = filters;
			for (int i = 0; i < array.Length; i++)
			{
				if (FileSystemName.MatchesSimpleExpression(array[i], fileName, !PathInternal.IsCaseSensitive))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x00094E6A File Offset: 0x0009306A
		private void NotifyInternalBufferOverflowEvent()
		{
			ErrorEventHandler onErrorHandler = this._onErrorHandler;
			if (onErrorHandler == null)
			{
				return;
			}
			onErrorHandler(this, new ErrorEventArgs(new InternalBufferOverflowException(SR.Format("Too many changes at once in directory:{0}.", this._directory))));
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x00094E98 File Offset: 0x00093098
		private void NotifyRenameEventArgs(WatcherChangeTypes action, ReadOnlySpan<char> name, ReadOnlySpan<char> oldName)
		{
			RenamedEventHandler onRenamedHandler = this._onRenamedHandler;
			if (onRenamedHandler != null && (this.MatchPattern(name) || this.MatchPattern(oldName)))
			{
				onRenamedHandler(this, new RenamedEventArgs(action, this._directory, name.IsEmpty ? null : name.ToString(), oldName.IsEmpty ? null : oldName.ToString()));
			}
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x00094F06 File Offset: 0x00093106
		private FileSystemEventHandler GetHandler(WatcherChangeTypes changeType)
		{
			switch (changeType)
			{
			case WatcherChangeTypes.Created:
				return this._onCreatedHandler;
			case WatcherChangeTypes.Deleted:
				return this._onDeletedHandler;
			case WatcherChangeTypes.Changed:
				return this._onChangedHandler;
			}
			return null;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00094F38 File Offset: 0x00093138
		private void NotifyFileSystemEventArgs(WatcherChangeTypes changeType, ReadOnlySpan<char> name)
		{
			FileSystemEventHandler handler = this.GetHandler(changeType);
			if (handler != null && this.MatchPattern(name.IsEmpty ? this._directory : name))
			{
				handler(this, new FileSystemEventArgs(changeType, this._directory, name.IsEmpty ? null : name.ToString()));
			}
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00094F9C File Offset: 0x0009319C
		private void NotifyFileSystemEventArgs(WatcherChangeTypes changeType, string name)
		{
			FileSystemEventHandler handler = this.GetHandler(changeType);
			if (handler != null && this.MatchPattern(string.IsNullOrEmpty(name) ? this._directory : name))
			{
				handler(this, new FileSystemEventArgs(changeType, this._directory, name));
			}
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00094FE6 File Offset: 0x000931E6
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.InvokeOn(e, this._onChangedHandler);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x00094FF5 File Offset: 0x000931F5
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.InvokeOn(e, this._onCreatedHandler);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00095004 File Offset: 0x00093204
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.InvokeOn(e, this._onDeletedHandler);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x00095014 File Offset: 0x00093214
		private void InvokeOn(FileSystemEventArgs e, FileSystemEventHandler handler)
		{
			if (handler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(handler, new object[] { this, e });
					return;
				}
				handler(this, e);
			}
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00095058 File Offset: 0x00093258
		protected void OnError(ErrorEventArgs e)
		{
			ErrorEventHandler onErrorHandler = this._onErrorHandler;
			if (onErrorHandler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(onErrorHandler, new object[] { this, e });
					return;
				}
				onErrorHandler(this, e);
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000950A0 File Offset: 0x000932A0
		protected void OnRenamed(RenamedEventArgs e)
		{
			RenamedEventHandler onRenamedHandler = this._onRenamedHandler;
			if (onRenamedHandler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(onRenamedHandler, new object[] { this, e });
					return;
				}
				onRenamedHandler(this, e);
			}
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000950E8 File Offset: 0x000932E8
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000950F4 File Offset: 0x000932F4
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			TaskCompletionSource<WaitForChangedResult> tcs = new TaskCompletionSource<WaitForChangedResult>();
			FileSystemEventHandler fileSystemEventHandler = null;
			RenamedEventHandler renamedEventHandler = null;
			if ((changeType & (WatcherChangeTypes.Changed | WatcherChangeTypes.Created | WatcherChangeTypes.Deleted)) != (WatcherChangeTypes)0)
			{
				fileSystemEventHandler = delegate(object s, FileSystemEventArgs e)
				{
					if ((e.ChangeType & changeType) != (WatcherChangeTypes)0)
					{
						tcs.TrySetResult(new WaitForChangedResult(e.ChangeType, e.Name, null, false));
					}
				};
				if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
				{
					this.Created += fileSystemEventHandler;
				}
				if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
				{
					this.Deleted += fileSystemEventHandler;
				}
				if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
				{
					this.Changed += fileSystemEventHandler;
				}
			}
			if ((changeType & WatcherChangeTypes.Renamed) != (WatcherChangeTypes)0)
			{
				renamedEventHandler = delegate(object s, RenamedEventArgs e)
				{
					if ((e.ChangeType & changeType) != (WatcherChangeTypes)0)
					{
						tcs.TrySetResult(new WaitForChangedResult(e.ChangeType, e.Name, e.OldName, false));
					}
				};
				this.Renamed += renamedEventHandler;
			}
			try
			{
				bool enableRaisingEvents = this.EnableRaisingEvents;
				if (!enableRaisingEvents)
				{
					this.EnableRaisingEvents = true;
				}
				tcs.Task.Wait(timeout);
				this.EnableRaisingEvents = enableRaisingEvents;
			}
			finally
			{
				if (renamedEventHandler != null)
				{
					this.Renamed -= renamedEventHandler;
				}
				if (fileSystemEventHandler != null)
				{
					if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
					{
						this.Changed -= fileSystemEventHandler;
					}
					if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
					{
						this.Deleted -= fileSystemEventHandler;
					}
					if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
					{
						this.Created -= fileSystemEventHandler;
					}
				}
			}
			if (tcs.Task.Status != TaskStatus.RanToCompletion)
			{
				return WaitForChangedResult.TimedOutResult;
			}
			return tcs.Task.Result;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0009522C File Offset: 0x0009342C
		private void Restart()
		{
			if (!this.IsSuspended() && this._enabled)
			{
				this.StopRaisingEvents();
				this.StartRaisingEventsIfNotDisposed();
			}
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0009524A File Offset: 0x0009344A
		private void StartRaisingEventsIfNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			this.StartRaisingEvents();
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x0009526B File Offset: 0x0009346B
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

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x00095290 File Offset: 0x00093490
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x00095298 File Offset: 0x00093498
		public ISynchronizeInvoke SynchronizingObject { get; set; }

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000952A4 File Offset: 0x000934A4
		public void BeginInit()
		{
			bool enabled = this._enabled;
			this.StopRaisingEvents();
			this._enabled = enabled;
			this._initializing = true;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000952CC File Offset: 0x000934CC
		public void EndInit()
		{
			this._initializing = false;
			if (this._directory.Length != 0 && this._enabled)
			{
				this.StartRaisingEvents();
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000952F0 File Offset: 0x000934F0
		private bool IsSuspended()
		{
			return this._initializing || base.DesignMode;
		}

		// Token: 0x040017AB RID: 6059
		private int _currentSession;

		// Token: 0x040017AC RID: 6060
		private SafeFileHandle _directoryHandle;

		// Token: 0x040017AD RID: 6061
		private readonly FileSystemWatcher.NormalizedFilterCollection _filters = new FileSystemWatcher.NormalizedFilterCollection();

		// Token: 0x040017AE RID: 6062
		private string _directory;

		// Token: 0x040017AF RID: 6063
		private const NotifyFilters c_defaultNotifyFilters = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

		// Token: 0x040017B0 RID: 6064
		private NotifyFilters _notifyFilters = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

		// Token: 0x040017B1 RID: 6065
		private bool _includeSubdirectories;

		// Token: 0x040017B2 RID: 6066
		private bool _enabled;

		// Token: 0x040017B3 RID: 6067
		private bool _initializing;

		// Token: 0x040017B4 RID: 6068
		private uint _internalBufferSize = 8192U;

		// Token: 0x040017B5 RID: 6069
		private bool _disposed;

		// Token: 0x040017B6 RID: 6070
		private FileSystemEventHandler _onChangedHandler;

		// Token: 0x040017B7 RID: 6071
		private FileSystemEventHandler _onCreatedHandler;

		// Token: 0x040017B8 RID: 6072
		private FileSystemEventHandler _onDeletedHandler;

		// Token: 0x040017B9 RID: 6073
		private RenamedEventHandler _onRenamedHandler;

		// Token: 0x040017BA RID: 6074
		private ErrorEventHandler _onErrorHandler;

		// Token: 0x040017BB RID: 6075
		private static readonly char[] s_wildcards = new char[] { '?', '*' };

		// Token: 0x040017BC RID: 6076
		private const int c_notifyFiltersValidMask = 383;

		// Token: 0x02000542 RID: 1346
		private sealed class AsyncReadState
		{
			// Token: 0x06002BA4 RID: 11172 RVA: 0x00095319 File Offset: 0x00093519
			internal AsyncReadState(int session, byte[] buffer, SafeFileHandle handle, ThreadPoolBoundHandle binding)
			{
				this.Session = session;
				this.Buffer = buffer;
				this.DirectoryHandle = handle;
				this.ThreadPoolBinding = binding;
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x0009533E File Offset: 0x0009353E
			// (set) Token: 0x06002BA6 RID: 11174 RVA: 0x00095346 File Offset: 0x00093546
			internal int Session { get; private set; }

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x0009534F File Offset: 0x0009354F
			// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x00095357 File Offset: 0x00093557
			internal byte[] Buffer { get; private set; }

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x00095360 File Offset: 0x00093560
			// (set) Token: 0x06002BAA RID: 11178 RVA: 0x00095368 File Offset: 0x00093568
			internal SafeFileHandle DirectoryHandle { get; private set; }

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06002BAB RID: 11179 RVA: 0x00095371 File Offset: 0x00093571
			// (set) Token: 0x06002BAC RID: 11180 RVA: 0x00095379 File Offset: 0x00093579
			internal ThreadPoolBoundHandle ThreadPoolBinding { get; private set; }

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06002BAD RID: 11181 RVA: 0x00095382 File Offset: 0x00093582
			// (set) Token: 0x06002BAE RID: 11182 RVA: 0x0009538A File Offset: 0x0009358A
			internal PreAllocatedOverlapped PreAllocatedOverlapped { get; set; }
		}

		// Token: 0x02000543 RID: 1347
		private sealed class NormalizedFilterCollection : Collection<string>
		{
			// Token: 0x06002BAF RID: 11183 RVA: 0x00095393 File Offset: 0x00093593
			internal NormalizedFilterCollection()
				: base(new FileSystemWatcher.NormalizedFilterCollection.ImmutableStringList())
			{
			}

			// Token: 0x06002BB0 RID: 11184 RVA: 0x000953A0 File Offset: 0x000935A0
			protected override void InsertItem(int index, string item)
			{
				base.InsertItem(index, (string.IsNullOrEmpty(item) || item == "*.*") ? "*" : item);
			}

			// Token: 0x06002BB1 RID: 11185 RVA: 0x000953C6 File Offset: 0x000935C6
			protected override void SetItem(int index, string item)
			{
				base.SetItem(index, (string.IsNullOrEmpty(item) || item == "*.*") ? "*" : item);
			}

			// Token: 0x06002BB2 RID: 11186 RVA: 0x000953EC File Offset: 0x000935EC
			internal string[] GetFilters()
			{
				return ((FileSystemWatcher.NormalizedFilterCollection.ImmutableStringList)base.Items).Items;
			}

			// Token: 0x02000544 RID: 1348
			private sealed class ImmutableStringList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
			{
				// Token: 0x170008EC RID: 2284
				public string this[int index]
				{
					get
					{
						string[] items = this.Items;
						if (index >= items.Length)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						return items[index];
					}
					set
					{
						string[] array = (string[])this.Items.Clone();
						array[index] = value;
						this.Items = array;
					}
				}

				// Token: 0x170008ED RID: 2285
				// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x00095451 File Offset: 0x00093651
				public int Count
				{
					get
					{
						return this.Items.Length;
					}
				}

				// Token: 0x170008EE RID: 2286
				// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x00003062 File Offset: 0x00001262
				public bool IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06002BB7 RID: 11191 RVA: 0x000044FA File Offset: 0x000026FA
				public void Add(string item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06002BB8 RID: 11192 RVA: 0x0009545B File Offset: 0x0009365B
				public void Clear()
				{
					this.Items = Array.Empty<string>();
				}

				// Token: 0x06002BB9 RID: 11193 RVA: 0x00095468 File Offset: 0x00093668
				public bool Contains(string item)
				{
					return Array.IndexOf<string>(this.Items, item) != -1;
				}

				// Token: 0x06002BBA RID: 11194 RVA: 0x0009547C File Offset: 0x0009367C
				public void CopyTo(string[] array, int arrayIndex)
				{
					this.Items.CopyTo(array, arrayIndex);
				}

				// Token: 0x06002BBB RID: 11195 RVA: 0x0009548B File Offset: 0x0009368B
				public IEnumerator<string> GetEnumerator()
				{
					return ((IEnumerable<string>)this.Items).GetEnumerator();
				}

				// Token: 0x06002BBC RID: 11196 RVA: 0x00095498 File Offset: 0x00093698
				public int IndexOf(string item)
				{
					return Array.IndexOf<string>(this.Items, item);
				}

				// Token: 0x06002BBD RID: 11197 RVA: 0x000954A8 File Offset: 0x000936A8
				public void Insert(int index, string item)
				{
					string[] items = this.Items;
					string[] array = new string[items.Length + 1];
					items.AsSpan<string>(0, index).CopyTo(array);
					items.AsSpan<string>(index).CopyTo(array.AsSpan<string>(index + 1));
					array[index] = item;
					this.Items = array;
				}

				// Token: 0x06002BBE RID: 11198 RVA: 0x000044FA File Offset: 0x000026FA
				public bool Remove(string item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06002BBF RID: 11199 RVA: 0x00095500 File Offset: 0x00093700
				public void RemoveAt(int index)
				{
					string[] items = this.Items;
					string[] array = new string[items.Length - 1];
					items.AsSpan<string>(0, index).CopyTo(array);
					items.AsSpan<string>(index + 1).CopyTo(array.AsSpan<string>(index));
					this.Items = array;
				}

				// Token: 0x06002BC0 RID: 11200 RVA: 0x00095552 File Offset: 0x00093752
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x040017C3 RID: 6083
				public string[] Items = Array.Empty<string>();
			}
		}
	}
}
