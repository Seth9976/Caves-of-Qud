using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Unity;

namespace System.Web.Caching
{
	/// <summary>Establishes a dependency relationship between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and a file, cache key, an array of either, or another <see cref="T:System.Web.Caching.CacheDependency" /> object. The <see cref="T:System.Web.Caching.CacheDependency" /> class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.</summary>
	// Token: 0x02000677 RID: 1655
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CacheDependency : IDisposable
	{
		// Token: 0x14000115 RID: 277
		// (add) Token: 0x060046CE RID: 18126 RVA: 0x000C518F File Offset: 0x000C338F
		// (remove) Token: 0x060046CF RID: 18127 RVA: 0x000C51A2 File Offset: 0x000C33A2
		internal event EventHandler DependencyChanged
		{
			add
			{
				this.events.AddHandler(CacheDependency.dependencyChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(CacheDependency.dependencyChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class.</summary>
		// Token: 0x060046D0 RID: 18128 RVA: 0x000C51B5 File Offset: 0x000C33B5
		protected CacheDependency()
			: this(null, null, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors a file or directory for changes.</summary>
		/// <param name="filename">The path to a file or directory that the cached object is dependent upon. When this resource changes, the cached object becomes obsolete and is removed from the cache. </param>
		// Token: 0x060046D1 RID: 18129 RVA: 0x000C51C5 File Offset: 0x000C33C5
		public CacheDependency(string filename)
			: this(new string[] { filename }, null, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories) for changes.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		// Token: 0x060046D2 RID: 18130 RVA: 0x000C51DE File Offset: 0x000C33DE
		public CacheDependency(string[] filenames)
			: this(filenames, null, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors a file or directory for changes.</summary>
		/// <param name="filename">The path to a file or directory that the cached object is dependent upon. When this resource changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="start">The time against which to check the last modified date of the directory or file. </param>
		// Token: 0x060046D3 RID: 18131 RVA: 0x000C51EE File Offset: 0x000C33EE
		public CacheDependency(string filename, DateTime start)
			: this(new string[] { filename }, null, null, start)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories) for changes and specifies a time when change monitoring begins.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="start">The time against which to check the last modified date of the objects in the array. </param>
		// Token: 0x060046D4 RID: 18132 RVA: 0x000C5203 File Offset: 0x000C3403
		public CacheDependency(string[] filenames, DateTime start)
			: this(filenames, null, null, start)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		// Token: 0x060046D5 RID: 18133 RVA: 0x000C520F File Offset: 0x000C340F
		public CacheDependency(string[] filenames, string[] cachekeys)
			: this(filenames, cachekeys, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes. It also makes itself dependent upon a separate instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		/// <param name="dependency">Another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that this instance is dependent upon. </param>
		// Token: 0x060046D6 RID: 18134 RVA: 0x000C521F File Offset: 0x000C341F
		public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency)
			: this(filenames, cachekeys, dependency, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		/// <param name="start">The date and time against which to check the last modified date of the objects passed in the <paramref name="filenames" /> and <paramref name="cachekeys" /> arrays. </param>
		// Token: 0x060046D7 RID: 18135 RVA: 0x000C522F File Offset: 0x000C342F
		public CacheDependency(string[] filenames, string[] cachekeys, DateTime start)
			: this(filenames, cachekeys, null, start)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes. It also makes itself dependent upon another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class and a time when the change monitoring begins.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		/// <param name="dependency">Another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that this instance is dependent upon. </param>
		/// <param name="start">The time against which to check the last modified date of the objects in the arrays and the <see cref="T:System.Web.Caching.CacheDependency" /> object. </param>
		// Token: 0x060046D8 RID: 18136 RVA: 0x000C523C File Offset: 0x000C343C
		public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency, DateTime start)
		{
			int num = ((filenames != null) ? filenames.Length : 0);
			if (num > 0)
			{
				this.watchers = new FileSystemWatcher[num];
				for (int i = 0; i < num; i++)
				{
					string text = filenames[i];
					if (!string.IsNullOrEmpty(text))
					{
						string filter = null;
						string text2;
						if (Directory.Exists(text))
						{
							text2 = text;
						}
						else
						{
							string directoryName = Path.GetDirectoryName(text);
							if (directoryName == null || !Directory.Exists(directoryName))
							{
								goto IL_01E9;
							}
							text2 = directoryName;
							filter = Path.GetFileName(text);
						}
						object obj = CacheDependency.locker;
						lock (obj)
						{
							if (CacheDependency.useSharedWatchers)
							{
								FileSystemWatcher fileSystemWatcher;
								if (!CacheDependency.sharedWatchers.TryGetValue(text2, out fileSystemWatcher))
								{
									fileSystemWatcher = new FileSystemWatcher();
									fileSystemWatcher.Path = text2;
									fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
									fileSystemWatcher.Created += delegate(object s, FileSystemEventArgs e)
									{
										if (filter == null || e.Name == filter)
										{
											this.OnChanged(s, e);
										}
									};
									fileSystemWatcher.Changed += delegate(object s, FileSystemEventArgs e)
									{
										if (filter == null || e.Name == filter)
										{
											this.OnChanged(s, e);
										}
									};
									fileSystemWatcher.Deleted += delegate(object s, FileSystemEventArgs e)
									{
										if (filter == null || e.Name == filter)
										{
											this.OnChanged(s, e);
										}
									};
									fileSystemWatcher.Renamed += delegate(object s, RenamedEventArgs e)
									{
										if (filter == null || e.OldName == filter)
										{
											this.OnChanged(s, e);
										}
									};
									fileSystemWatcher.EnableRaisingEvents = true;
									CacheDependency.sharedWatchers[text2] = fileSystemWatcher;
								}
							}
							else
							{
								FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
								fileSystemWatcher.Path = text2;
								if (filter != null)
								{
									fileSystemWatcher.Filter = filter;
								}
								fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
								fileSystemWatcher.Created += this.OnChanged;
								fileSystemWatcher.Changed += this.OnChanged;
								fileSystemWatcher.Deleted += this.OnChanged;
								fileSystemWatcher.Renamed += new RenamedEventHandler(this.OnChanged);
								fileSystemWatcher.EnableRaisingEvents = true;
								this.watchers[i] = fileSystemWatcher;
							}
						}
					}
					IL_01E9:;
				}
			}
			this.cachekeys = cachekeys;
			this.dependency = dependency;
			if (dependency != null)
			{
				dependency.DependencyChanged += this.OnChildDependencyChanged;
			}
			this.start = start;
			this.FinishInit();
		}

		/// <summary>Retrieves a unique identifier for a <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
		/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.CacheDependency" /> object.</returns>
		// Token: 0x060046D9 RID: 18137 RVA: 0x000C548C File Offset: 0x000C368C
		public virtual string GetUniqueID()
		{
			StringBuilder stringBuilder = new StringBuilder();
			object obj = CacheDependency.locker;
			lock (obj)
			{
				FileSystemWatcher[] array;
				if (CacheDependency.useSharedWatchers)
				{
					array = new FileSystemWatcher[CacheDependency.sharedWatchers.Count];
					CacheDependency.sharedWatchers.Values.CopyTo(array, 0);
				}
				else
				{
					array = this.watchers;
				}
				if (array != null)
				{
					foreach (FileSystemWatcher fileSystemWatcher in array)
					{
						if (fileSystemWatcher != null && fileSystemWatcher.Path != null && fileSystemWatcher.Path.Length != 0)
						{
							stringBuilder.Append("_" + fileSystemWatcher.Path);
						}
					}
				}
			}
			if (this.cachekeys != null)
			{
				foreach (string text in this.cachekeys)
				{
					stringBuilder.AppendFormat("_" + text, Array.Empty<object>());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x000C5598 File Offset: 0x000C3798
		private void OnChanged(object sender, FileSystemEventArgs args)
		{
			this.OnDependencyChanged(sender, args);
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x000C55A4 File Offset: 0x000C37A4
		private bool DoOnChanged()
		{
			DateTime now = DateTime.Now;
			if (now < this.start)
			{
				return false;
			}
			this.hasChanged = true;
			this.utcLastModified = now.ToUniversalTime();
			this.DisposeWatchers();
			if (this.cache != null)
			{
				this.cache.CheckDependencies();
			}
			return true;
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x000C55F8 File Offset: 0x000C37F8
		private void DisposeWatchers()
		{
			if (CacheDependency.useSharedWatchers)
			{
				return;
			}
			object obj = CacheDependency.locker;
			lock (obj)
			{
				if (this.watchers != null)
				{
					foreach (FileSystemWatcher fileSystemWatcher in this.watchers)
					{
						if (fileSystemWatcher != null)
						{
							fileSystemWatcher.Dispose();
						}
					}
				}
				this.watchers = null;
			}
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
		// Token: 0x060046DD RID: 18141 RVA: 0x000C5670 File Offset: 0x000C3870
		public void Dispose()
		{
			this.DependencyDispose();
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00003946 File Offset: 0x00001B46
		internal virtual void DependencyDisposeInternal()
		{
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Web.Caching.CacheDependency" /> class and any classes that derive from <see cref="T:System.Web.Caching.CacheDependency" />.</summary>
		// Token: 0x060046DF RID: 18143 RVA: 0x000C5678 File Offset: 0x000C3878
		protected virtual void DependencyDispose()
		{
			this.DependencyDisposeInternal();
			this.DisposeWatchers();
			if (this.dependency != null)
			{
				this.dependency.DependencyChanged -= this.OnChildDependencyChanged;
				this.dependency.Dispose();
			}
			this.cache = null;
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x000C56B7 File Offset: 0x000C38B7
		internal void SetCache(Cache c)
		{
			this.cache = c;
			this.used = c != null;
		}

		/// <summary>Completes initialization of the <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
		// Token: 0x060046E1 RID: 18145 RVA: 0x000C56CA File Offset: 0x000C38CA
		protected internal void FinishInit()
		{
			this.utcLastModified = DateTime.UtcNow;
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x000C56D7 File Offset: 0x000C38D7
		internal bool IsUsed
		{
			get
			{
				return this.used;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x000C56DF File Offset: 0x000C38DF
		// (set) Token: 0x060046E4 RID: 18148 RVA: 0x000C56E7 File Offset: 0x000C38E7
		internal DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		/// <summary>Gets the time when the dependency was last changed.</summary>
		/// <returns>The time when the dependency was last changed.</returns>
		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x000C56F0 File Offset: 0x000C38F0
		public DateTime UtcLastModified
		{
			get
			{
				return this.utcLastModified;
			}
		}

		/// <summary>Marks the time when a dependency last changed.</summary>
		/// <param name="utcLastModified">The time when the dependency last changed. </param>
		// Token: 0x060046E6 RID: 18150 RVA: 0x000C56F8 File Offset: 0x000C38F8
		protected void SetUtcLastModified(DateTime utcLastModified)
		{
			this.utcLastModified = utcLastModified;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.Caching.CacheDependency" /> object has changed.</summary>
		/// <returns>true if the <see cref="T:System.Web.Caching.CacheDependency" /> object has changed; otherwise, false. The default is false.</returns>
		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x000C5704 File Offset: 0x000C3904
		public bool HasChanged
		{
			get
			{
				if (this.hasChanged)
				{
					return true;
				}
				if (DateTime.Now < this.start)
				{
					return false;
				}
				if (this.cache != null && this.cachekeys != null)
				{
					foreach (string text in this.cachekeys)
					{
						if (this.cache.GetKeyLastChange(text) > this.start)
						{
							this.hasChanged = true;
							break;
						}
					}
				}
				if (this.hasChanged)
				{
					this.DisposeWatchers();
				}
				return this.hasChanged;
			}
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x000C578E File Offset: 0x000C398E
		private void OnChildDependencyChanged(object o, EventArgs e)
		{
			this.hasChanged = true;
			this.OnDependencyChanged(o, e);
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x000C57A0 File Offset: 0x000C39A0
		private void OnDependencyChanged(object sender, EventArgs e)
		{
			if (!this.DoOnChanged())
			{
				return;
			}
			EventHandler eventHandler = this.events[CacheDependency.dependencyChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		/// <summary>Notifies the base <see cref="T:System.Web.Caching.CacheDependency" /> object that the dependency represented by a derived <see cref="T:System.Web.Caching.CacheDependency" /> class has changed.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060046EA RID: 18154 RVA: 0x000C5598 File Offset: 0x000C3798
		protected void NotifyDependencyChanged(object sender, EventArgs e)
		{
			this.OnDependencyChanged(sender, e);
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual string[] GetFileDependencies()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void ItemRemoved()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void KeepDependenciesAlive()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void SetCacheDependencyChanged(Action<object, EventArgs> dependencyChangedAction)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x000C580C File Offset: 0x000C3A0C
		public bool TakeOwnership()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x0400255A RID: 9562
		private static readonly object dependencyChangedEvent = new object();

		// Token: 0x0400255B RID: 9563
		private string[] cachekeys;

		// Token: 0x0400255C RID: 9564
		private CacheDependency dependency;

		// Token: 0x0400255D RID: 9565
		private DateTime start;

		// Token: 0x0400255E RID: 9566
		private Cache cache;

		// Token: 0x0400255F RID: 9567
		private FileSystemWatcher[] watchers;

		// Token: 0x04002560 RID: 9568
		private static readonly bool useSharedWatchers = Environment.GetEnvironmentVariable("MONO_SYSTEMWEB_CACHEDEPENDENCY_SHARED_FSW") != null;

		// Token: 0x04002561 RID: 9569
		private static readonly Dictionary<string, FileSystemWatcher> sharedWatchers = new Dictionary<string, FileSystemWatcher>();

		// Token: 0x04002562 RID: 9570
		private bool hasChanged;

		// Token: 0x04002563 RID: 9571
		private bool used;

		// Token: 0x04002564 RID: 9572
		private DateTime utcLastModified;

		// Token: 0x04002565 RID: 9573
		private static readonly object locker = new object();

		// Token: 0x04002566 RID: 9574
		private EventHandlerList events = new EventHandlerList();
	}
}
