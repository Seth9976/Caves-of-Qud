using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.CoreFX;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020004FE RID: 1278
	internal class CoreFXFileSystemWatcherProxy : IFileWatcher
	{
		// Token: 0x060029BC RID: 10684 RVA: 0x0008F248 File Offset: 0x0008D448
		protected void Operation(Action<IDictionary<object, FileSystemWatcher>, ConditionalWeakTable<object, FileSystemWatcher>, IDictionary<object, object>, object> map_op = null, Action<FileSystemWatcher, FileSystemWatcher> object_op = null, object handle = null, Action<FileSystemWatcher, FileSystemWatcher> cancel_op = null)
		{
			FileSystemWatcher internal_fsw = null;
			FileSystemWatcher fsw = null;
			bool flag2;
			if (cancel_op != null)
			{
				bool flag = Monitor.TryEnter(CoreFXFileSystemWatcherProxy.instance, 300);
				flag2 = handle != null && (CoreFXFileSystemWatcherProxy.internal_map.TryGetValue(handle, out internal_fsw) || CoreFXFileSystemWatcherProxy.external_map.TryGetValue(handle, out fsw));
				if (flag2 && flag)
				{
					try
					{
						cancel_op(internal_fsw, fsw);
					}
					catch (Exception)
					{
					}
				}
				if (flag)
				{
					Monitor.Exit(CoreFXFileSystemWatcherProxy.instance);
				}
				if (flag2 && !flag)
				{
					try
					{
						Task.Run<bool>(delegate
						{
							cancel_op(internal_fsw, fsw);
							return true;
						}).Wait(300);
					}
					catch (Exception)
					{
					}
				}
				return;
			}
			IFileWatcher fileWatcher;
			if (map_op != null && handle == null)
			{
				fileWatcher = CoreFXFileSystemWatcherProxy.instance;
				lock (fileWatcher)
				{
					try
					{
						map_op(CoreFXFileSystemWatcherProxy.internal_map, CoreFXFileSystemWatcherProxy.external_map, CoreFXFileSystemWatcherProxy.event_map, null);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException("map_op", ex);
					}
				}
				return;
			}
			if (handle == null)
			{
				return;
			}
			fileWatcher = CoreFXFileSystemWatcherProxy.instance;
			lock (fileWatcher)
			{
				flag2 = CoreFXFileSystemWatcherProxy.internal_map.TryGetValue(handle, out internal_fsw) && CoreFXFileSystemWatcherProxy.external_map.TryGetValue(handle, out fsw);
				if (flag2 && map_op != null)
				{
					try
					{
						map_op(CoreFXFileSystemWatcherProxy.internal_map, CoreFXFileSystemWatcherProxy.external_map, CoreFXFileSystemWatcherProxy.event_map, handle);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException("map_op", ex2);
					}
				}
			}
			if (!flag2 || object_op == null)
			{
				return;
			}
			try
			{
				object_op(internal_fsw, fsw);
			}
			catch (Exception ex3)
			{
				throw new InvalidOperationException("object_op", ex3);
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0008F454 File Offset: 0x0008D654
		protected void ProxyDispatch(object sender, FileAction action, FileSystemEventArgs args)
		{
			RenamedEventArgs renamed = ((action == FileAction.RenamedNewName) ? ((RenamedEventArgs)args) : null);
			object handle = null;
			this.Operation(delegate(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object h)
			{
				event_map.TryGetValue(sender, out handle);
			}, null, null, null);
			this.Operation(null, delegate(FileSystemWatcher _, FileSystemWatcher fsw)
			{
				if (!fsw.EnableRaisingEvents)
				{
					return;
				}
				fsw.DispatchEvents(action, args.Name, ref renamed);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					Monitor.PulseAll(fsw);
				}
			}, handle, null);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0008F4D0 File Offset: 0x0008D6D0
		protected void ProxyDispatchError(object sender, ErrorEventArgs args)
		{
			object handle = null;
			this.Operation(delegate(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object _)
			{
				event_map.TryGetValue(sender, out handle);
			}, null, null, null);
			this.Operation(null, delegate(FileSystemWatcher _, FileSystemWatcher fsw)
			{
				fsw.DispatchErrorEvents(args);
			}, handle, null);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0008F528 File Offset: 0x0008D728
		public object NewWatcher(FileSystemWatcher fsw)
		{
			object handle = new object();
			FileSystemWatcher result = new FileSystemWatcher();
			result.Changed += delegate(object o, FileSystemEventArgs args)
			{
				Task.Run(delegate
				{
					this.ProxyDispatch(o, FileAction.Modified, args);
				});
			};
			result.Created += delegate(object o, FileSystemEventArgs args)
			{
				Task.Run(delegate
				{
					this.ProxyDispatch(o, FileAction.Added, args);
				});
			};
			result.Deleted += delegate(object o, FileSystemEventArgs args)
			{
				Task.Run(delegate
				{
					this.ProxyDispatch(o, FileAction.Removed, args);
				});
			};
			result.Renamed += delegate(object o, RenamedEventArgs args)
			{
				Task.Run(delegate
				{
					this.ProxyDispatch(o, FileAction.RenamedNewName, args);
				});
			};
			result.Error += delegate(object o, ErrorEventArgs args)
			{
				Task.Run(delegate
				{
					this.ProxyDispatchError(handle, args);
				});
			};
			this.Operation(delegate(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object _)
			{
				in_map.Add(handle, result);
				out_map.Add(handle, fsw);
				event_map.Add(result, handle);
			}, null, null, null);
			return handle;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0008F5ED File Offset: 0x0008D7ED
		public void StartDispatching(object handle)
		{
			if (handle == null)
			{
				return;
			}
			this.Operation(null, delegate(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				internal_fsw.Path = fsw.Path;
				internal_fsw.Filter = fsw.Filter;
				internal_fsw.IncludeSubdirectories = fsw.IncludeSubdirectories;
				internal_fsw.InternalBufferSize = fsw.InternalBufferSize;
				internal_fsw.NotifyFilter = fsw.NotifyFilter;
				internal_fsw.Site = fsw.Site;
				internal_fsw.EnableRaisingEvents = true;
			}, handle, null);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0008F61B File Offset: 0x0008D81B
		public void StopDispatching(object handle)
		{
			if (handle == null)
			{
				return;
			}
			this.Operation(null, null, handle, delegate(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				if (internal_fsw != null)
				{
					internal_fsw.EnableRaisingEvents = false;
				}
			});
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0008F64C File Offset: 0x0008D84C
		public void Dispose(object handle)
		{
			if (handle == null)
			{
				return;
			}
			this.Operation(null, null, handle, delegate(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				if (internal_fsw != null)
				{
					internal_fsw.Dispose();
				}
				FileSystemWatcher fileSystemWatcher = CoreFXFileSystemWatcherProxy.internal_map[handle];
				CoreFXFileSystemWatcherProxy.internal_map.Remove(handle);
				CoreFXFileSystemWatcherProxy.external_map.Remove(handle);
				CoreFXFileSystemWatcherProxy.event_map.Remove(fileSystemWatcher);
				handle = null;
			});
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x0008F68C File Offset: 0x0008D88C
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (CoreFXFileSystemWatcherProxy.instance != null)
			{
				watcher = CoreFXFileSystemWatcherProxy.instance;
				return true;
			}
			CoreFXFileSystemWatcherProxy.internal_map = new ConcurrentDictionary<object, FileSystemWatcher>();
			CoreFXFileSystemWatcherProxy.external_map = new ConditionalWeakTable<object, FileSystemWatcher>();
			CoreFXFileSystemWatcherProxy.event_map = new ConcurrentDictionary<object, object>();
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new CoreFXFileSystemWatcherProxy());
			CoreFXFileSystemWatcherProxy.instance = fileWatcher;
			return true;
		}

		// Token: 0x04001621 RID: 5665
		private static IFileWatcher instance;

		// Token: 0x04001622 RID: 5666
		private static IDictionary<object, FileSystemWatcher> internal_map;

		// Token: 0x04001623 RID: 5667
		private static ConditionalWeakTable<object, FileSystemWatcher> external_map;

		// Token: 0x04001624 RID: 5668
		private static IDictionary<object, object> event_map;

		// Token: 0x04001625 RID: 5669
		private const int INTERRUPT_MS = 300;
	}
}
