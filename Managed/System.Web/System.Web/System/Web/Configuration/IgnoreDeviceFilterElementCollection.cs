using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> objects. This class cannot be inherited. </summary>
	// Token: 0x020006A2 RID: 1698
	[ConfigurationCollection(typeof(IgnoreDeviceFilterElement), AddItemName = "filter", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class IgnoreDeviceFilterElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElementCollection" /> class.</summary>
		// Token: 0x060047CE RID: 18382 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IgnoreDeviceFilterElementCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection at the specified index.</summary>
		/// <returns>The specified filter object.</returns>
		/// <param name="index">The index of the filter object to get.</param>
		// Token: 0x1700164A RID: 5706
		public IgnoreDeviceFilterElement this[int index]
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

		/// <summary>Adds a <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object to the collection.</summary>
		/// <param name="deviceFilter">The object to add to the collection.</param>
		// Token: 0x060047D1 RID: 18385 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(IgnoreDeviceFilterElement deviceFilter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> objects from the collection.</summary>
		// Token: 0x060047D2 RID: 18386 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection, by using its name.</summary>
		/// <param name="name">The name of the object to remove.</param>
		// Token: 0x060047D5 RID: 18389 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection.</summary>
		/// <param name="deviceFilter">The object to remove. </param>
		// Token: 0x060047D6 RID: 18390 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(IgnoreDeviceFilterElement deviceFilter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.IgnoreDeviceFilterElement" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index of the object to remove.</param>
		// Token: 0x060047D7 RID: 18391 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
