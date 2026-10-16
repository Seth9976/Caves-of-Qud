using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a configuration element that contains a collection of protocols.</summary>
	// Token: 0x02000778 RID: 1912
	[ConfigurationCollection(typeof(ProtocolElement))]
	public sealed class ProtocolCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProtocolCollection" /> class. </summary>
		// Token: 0x06004DCB RID: 19915 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ProtocolCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets all the keys of the <see cref="T:System.Web.Configuration.ProtocolCollection" /> instance.</summary>
		/// <returns>The array that contains the collection keys.</returns>
		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string[] AllKeys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ProtocolElement" /> object at the specified index of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProtocolElement" /> object.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProtocolElement" /> object in the <see cref="T:System.Web.Configuration.ProtocolCollection" /> instance.</param>
		// Token: 0x170017C4 RID: 6084
		public ProtocolElement this[int index]
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

		/// <summary>Adds a configuration element to the <see cref="T:System.Web.Configuration.ProtocolCollection" /> instance.</summary>
		/// <param name="protocolElement">The element to add.</param>
		// Token: 0x06004DCF RID: 19919 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(ProtocolElement protocolElement)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all configuration element objects from the collection.</summary>
		// Token: 0x06004DD0 RID: 19920 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ProtocolElement" /> object that has the specified key from the collection.</summary>
		/// <param name="name">The key of the <see cref="T:System.Web.Configuration.ProtocolElement" /> object to remove.</param>
		// Token: 0x06004DD3 RID: 19923 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.ProtocolElement" /> object from the collection.</summary>
		/// <param name="protocolElement">The element to remove.</param>
		// Token: 0x06004DD4 RID: 19924 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(ProtocolElement protocolElement)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ProtocolElement" /> object at the specified index location.</summary>
		/// <param name="index">The index location of the element to remove. </param>
		// Token: 0x06004DD5 RID: 19925 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
