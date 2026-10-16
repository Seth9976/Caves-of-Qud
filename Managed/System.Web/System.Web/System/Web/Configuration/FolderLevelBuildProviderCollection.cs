using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> objects. </summary>
	// Token: 0x0200069F RID: 1695
	[ConfigurationCollection(typeof(FolderLevelBuildProvider))]
	public sealed class FolderLevelBuildProviderCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> class.</summary>
		// Token: 0x060047C0 RID: 18368 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public FolderLevelBuildProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object at the specified index in the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <returns>The folder-level build provider object that is located at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to get.</param>
		// Token: 0x17001647 RID: 5703
		public FolderLevelBuildProvider this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <param name="buildProvider">The <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to add.</param>
		// Token: 0x060047C3 RID: 18371 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(FolderLevelBuildProvider buildProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Clears all <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> objects from the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		// Token: 0x060047C4 RID: 18372 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object from the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <param name="name">The <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to remove.</param>
		// Token: 0x060047C7 RID: 18375 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object at the specified index in a <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to remove. </param>
		// Token: 0x060047C8 RID: 18376 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
