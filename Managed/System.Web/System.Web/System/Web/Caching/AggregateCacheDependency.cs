using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace System.Web.Caching
{
	/// <summary>Combines multiple dependencies between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and an array of <see cref="T:System.Web.Caching.CacheDependency" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000675 RID: 1653
	public sealed class AggregateCacheDependency : CacheDependency
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> class.</summary>
		// Token: 0x060046A4 RID: 18084 RVA: 0x000C428F File Offset: 0x000C248F
		public AggregateCacheDependency()
		{
			base.FinishInit();
		}

		/// <summary>Adds an array of <see cref="T:System.Web.Caching.CacheDependency" /> objects to the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object.</summary>
		/// <param name="dependencies">The array of <see cref="T:System.Web.Caching.CacheDependency" />  objects to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dependencies" /> is null.- or -A <see cref="T:System.Web.Caching.CacheDependency" /> object in <paramref name="dependencies" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.Caching.CacheDependency" /> object is referenced from more than one <see cref="T:System.Web.Caching.Cache" /> entry.</exception>
		// Token: 0x060046A5 RID: 18085 RVA: 0x000C42A8 File Offset: 0x000C24A8
		public void Add(params CacheDependency[] dependencies)
		{
			if (dependencies == null)
			{
				throw new ArgumentNullException("dependencies");
			}
			if (dependencies.Length == 0)
			{
				return;
			}
			bool flag = false;
			foreach (CacheDependency cacheDependency in dependencies)
			{
				if (cacheDependency == null || cacheDependency.IsUsed)
				{
					throw new InvalidOperationException("Cache dependency already in use");
				}
				if (!flag && cacheDependency != null && cacheDependency.HasChanged)
				{
					flag = true;
				}
			}
			object obj = this.dependenciesLock;
			lock (obj)
			{
				if (this.dependencies == null)
				{
					this.dependencies = new List<CacheDependency>(dependencies.Length);
				}
				foreach (CacheDependency cacheDependency2 in dependencies)
				{
					if (cacheDependency2 != null)
					{
						cacheDependency2.DependencyChanged += this.OnAnyChanged;
					}
				}
				this.dependencies.AddRange(dependencies);
				base.Start = DateTime.UtcNow;
			}
			if (flag)
			{
				base.NotifyDependencyChanged(this, null);
			}
		}

		/// <summary>Retrieves a unique identifier for the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object.</summary>
		/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object. If one of the associated dependency objects does not have a unique identifier, the <see cref="M:System.Web.Caching.AggregateCacheDependency.GetUniqueId" /> method returns null.</returns>
		// Token: 0x060046A6 RID: 18086 RVA: 0x000C439C File Offset: 0x000C259C
		public override string GetUniqueID()
		{
			if (this.dependencies == null || this.dependencies.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			object obj = this.dependenciesLock;
			lock (obj)
			{
				foreach (CacheDependency cacheDependency in this.dependencies)
				{
					string uniqueID = cacheDependency.GetUniqueID();
					if (string.IsNullOrEmpty(uniqueID))
					{
						return null;
					}
					stringBuilder.Append(uniqueID);
					stringBuilder.Append(';');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000C445C File Offset: 0x000C265C
		protected override void DependencyDispose()
		{
			base.DependencyDispose();
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000C4464 File Offset: 0x000C2664
		internal override void DependencyDisposeInternal()
		{
			if (this.dependencies != null && this.dependencies.Count > 0)
			{
				foreach (CacheDependency cacheDependency in this.dependencies)
				{
					cacheDependency.DependencyChanged -= this.OnAnyChanged;
				}
			}
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000C44D8 File Offset: 0x000C26D8
		private void OnAnyChanged(object sender, EventArgs args)
		{
			base.NotifyDependencyChanged(sender, args);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string[] GetFileDependencies()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0400254B RID: 9547
		private object dependenciesLock = new object();

		// Token: 0x0400254C RID: 9548
		private List<CacheDependency> dependencies;
	}
}
