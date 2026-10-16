using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> objects. This class cannot be inherited. </summary>
	// Token: 0x020006A8 RID: 1704
	[ConfigurationCollection(typeof(string))]
	public sealed class PartialTrustVisibleAssemblyCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssemblyCollection" /> class.</summary>
		// Token: 0x060047EE RID: 18414 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PartialTrustVisibleAssemblyCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object at the specified index.</summary>
		/// <returns>The collection object at the specified index.</returns>
		/// <param name="index">The index of the element to get.</param>
		// Token: 0x17001652 RID: 5714
		public PartialTrustVisibleAssembly this[int index]
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

		/// <summary>Adds a <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object to the collection.</summary>
		/// <param name="partialTrustVisibleAssembly">The object to add to the collection.</param>
		// Token: 0x060047F1 RID: 18417 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(PartialTrustVisibleAssembly partialTrustVisibleAssembly)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> objects from the collection.</summary>
		// Token: 0x060047F2 RID: 18418 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object from the collection </summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x060047F5 RID: 18421 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index of the element to remove.</param>
		// Token: 0x060047F6 RID: 18422 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
