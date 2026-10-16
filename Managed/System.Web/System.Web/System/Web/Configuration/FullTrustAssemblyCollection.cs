using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FullTrustAssembly" /> objects. This class cannot be inherited. </summary>
	// Token: 0x020006A5 RID: 1701
	[ConfigurationCollection(typeof(string))]
	public sealed class FullTrustAssemblyCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FullTrustAssemblyCollection" /> class.</summary>
		// Token: 0x060047DC RID: 18396 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public FullTrustAssemblyCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object from the collection at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The collection element index.</param>
		// Token: 0x1700164D RID: 5709
		public FullTrustAssembly this[int index]
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

		/// <summary>Adds a <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object to the collection.</summary>
		/// <param name="fullTrustAssembly">The object to add to the collection.</param>
		// Token: 0x060047DF RID: 18399 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(FullTrustAssembly fullTrustAssembly)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.FullTrustAssembly" /> objects from the collection.</summary>
		// Token: 0x060047E0 RID: 18400 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object from the collection.</summary>
		/// <param name="key">The key of the element to remove from the collection.</param>
		// Token: 0x060047E3 RID: 18403 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.FullTrustAssembly" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index of the element to remove from the collection.</param>
		// Token: 0x060047E4 RID: 18404 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
