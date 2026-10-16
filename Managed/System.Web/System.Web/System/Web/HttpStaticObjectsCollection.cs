using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	/// <summary>Provides a collection of application-scoped objects for the <see cref="P:System.Web.HttpApplicationState.StaticObjects" /> property.</summary>
	// Token: 0x020000B6 RID: 182
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpStaticObjectsCollection : ICollection, IEnumerable
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00018556 File Offset: 0x00016756
		private Dictionary<string, object> Objects
		{
			get
			{
				if (this.objects == null)
				{
					this.objects = new Dictionary<string, object>(StringComparer.Ordinal);
				}
				return this.objects;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpStaticObjectsCollection" /> class.</summary>
		// Token: 0x06000A0B RID: 2571 RVA: 0x00002050 File Offset: 0x00000250
		public HttpStaticObjectsCollection()
		{
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00002050 File Offset: 0x00000250
		internal HttpStaticObjectsCollection(HttpApplicationState appstate)
		{
		}

		/// <summary>Returns the object with the specified name from the collection. This property is an alternative to the this accessor.</summary>
		/// <returns>An object from the collection.</returns>
		/// <param name="name">The case-insensitive name of the object to return. </param>
		// Token: 0x06000A0D RID: 2573 RVA: 0x00018576 File Offset: 0x00016776
		public object GetObject(string name)
		{
			return this[name];
		}

		/// <summary>Returns a dictionary enumerator used for iterating through the key-and-value pairs contained in the collection.</summary>
		/// <returns>The enumerator for the collection.</returns>
		// Token: 0x06000A0E RID: 2574 RVA: 0x0001857F File Offset: 0x0001677F
		public IEnumerator GetEnumerator()
		{
			return this.Objects.GetEnumerator();
		}

		/// <summary>Copies members of an <see cref="T:System.Web.HttpStaticObjectsCollection" /> into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Web.HttpStaticObjectsCollection" /> into. </param>
		/// <param name="index">The member of the collection where copying starts. </param>
		// Token: 0x06000A0F RID: 2575 RVA: 0x00018594 File Offset: 0x00016794
		public void CopyTo(Array array, int index)
		{
			if (this.objects == null)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("array is multidimensional");
			}
			if (array.Length > 0 && index >= array.Length)
			{
				throw new ArgumentException("index is equal to or greater than array.Length");
			}
			if (index + this.objects.Count > array.Length)
			{
				throw new ArgumentException("Not enough room from index to end of array for this collection");
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.objects)
			{
				array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), index++);
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001867C File Offset: 0x0001687C
		internal IDictionary GetObjects()
		{
			return this.Objects;
		}

		/// <summary>Gets the object with the specified name from the collection.</summary>
		/// <returns>An object from the collection.</returns>
		/// <param name="name">The case-insensitive name of the object to get. </param>
		// Token: 0x170003B9 RID: 953
		public object this[string name]
		{
			get
			{
				if (this.objects == null)
				{
					return null;
				}
				HttpStaticObjectsCollection.StaticItem staticItem = null;
				object obj;
				if (this.Objects.TryGetValue(name, out obj))
				{
					staticItem = obj as HttpStaticObjectsCollection.StaticItem;
				}
				if (staticItem == null)
				{
					return null;
				}
				return staticItem.Instance;
			}
		}

		/// <summary>Gets the number of objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000186BF File Offset: 0x000168BF
		public int Count
		{
			get
			{
				if (this.objects == null)
				{
					return 0;
				}
				return this.Objects.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>Always returns true.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection is synchronized (that is, thread-safe).</summary>
		/// <returns>In this implementation, this property always returns false.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the collection has been accessed before.</summary>
		/// <returns>true if the <see cref="T:System.Web.HttpStaticObjectsCollection" /> has never been accessed; otherwise, false.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public bool NeverAccessed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The current <see cref="T:System.Web.HttpStaticObjectsCollection" />.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000186D8 File Offset: 0x000168D8
		internal HttpStaticObjectsCollection Clone()
		{
			HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
			if (this.objects == null)
			{
				return httpStaticObjectsCollection;
			}
			Dictionary<string, object> dictionary = httpStaticObjectsCollection.Objects;
			foreach (KeyValuePair<string, object> keyValuePair in this.objects)
			{
				HttpStaticObjectsCollection.StaticItem staticItem = new HttpStaticObjectsCollection.StaticItem((HttpStaticObjectsCollection.StaticItem)keyValuePair.Value);
				dictionary[keyValuePair.Key] = staticItem;
			}
			return httpStaticObjectsCollection;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00018760 File Offset: 0x00016960
		internal void Add(ObjectTagBuilder tag)
		{
			this.Objects.Add(tag.ObjectID, new HttpStaticObjectsCollection.StaticItem(tag.Type));
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001877E File Offset: 0x0001697E
		private void Set(string name, object obj)
		{
			this.Objects[name] = obj;
		}

		/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.IO.BinaryWriter" /> used to write the serialized collection to a stream or encoded string.</param>
		// Token: 0x06000A1A RID: 2586 RVA: 0x00018790 File Offset: 0x00016990
		public void Serialize(BinaryWriter writer)
		{
			if (this.objects == null)
			{
				writer.Write(0);
				return;
			}
			writer.Write(this.objects.Count);
			foreach (KeyValuePair<string, object> keyValuePair in this.objects)
			{
				writer.Write(keyValuePair.Key);
				AltSerialization.Serialize(writer, keyValuePair.Value);
			}
		}

		/// <summary>Creates an <see cref="T:System.Web.HttpStaticObjectsCollection" /> object from a binary file that was written by using the <see cref="M:System.Web.HttpStaticObjectsCollection.Serialize(System.IO.BinaryWriter)" /> method.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> populated with the contents from a binary file written using the <see cref="M:System.Web.HttpStaticObjectsCollection.Serialize(System.IO.BinaryWriter)" /> method.</returns>
		/// <param name="reader">The <see cref="T:System.IO.BinaryReader" /> used to read the serialized collection from a stream or encoded string.</param>
		// Token: 0x06000A1B RID: 2587 RVA: 0x00018818 File Offset: 0x00016A18
		public static HttpStaticObjectsCollection Deserialize(BinaryReader reader)
		{
			HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
			for (int i = reader.ReadInt32(); i > 0; i--)
			{
				httpStaticObjectsCollection.Set(reader.ReadString(), AltSerialization.Deserialize(reader));
			}
			return httpStaticObjectsCollection;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00018850 File Offset: 0x00016A50
		internal byte[] ToByteArray()
		{
			MemoryStream memoryStream = null;
			byte[] buffer;
			try
			{
				memoryStream = new MemoryStream();
				this.Serialize(new BinaryWriter(memoryStream));
				buffer = memoryStream.GetBuffer();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return buffer;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000188A4 File Offset: 0x00016AA4
		internal static HttpStaticObjectsCollection FromByteArray(byte[] data)
		{
			HttpStaticObjectsCollection httpStaticObjectsCollection = null;
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream(data);
				httpStaticObjectsCollection = HttpStaticObjectsCollection.Deserialize(new BinaryReader(memoryStream));
			}
			catch
			{
				throw;
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return httpStaticObjectsCollection;
		}

		// Token: 0x04001019 RID: 4121
		private Dictionary<string, object> objects;

		// Token: 0x020000B7 RID: 183
		private sealed class StaticItem
		{
			// Token: 0x06000A1E RID: 2590 RVA: 0x000188F4 File Offset: 0x00016AF4
			public StaticItem(Type type)
			{
				this.type = type;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x0001890E File Offset: 0x00016B0E
			public StaticItem(HttpStaticObjectsCollection.StaticItem item)
			{
				this.type = item.type;
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00018930 File Offset: 0x00016B30
			public object Instance
			{
				get
				{
					object obj = this.this_lock;
					lock (obj)
					{
						if (this.instance == null)
						{
							this.instance = Activator.CreateInstance(this.type);
						}
					}
					return this.instance;
				}
			}

			// Token: 0x0400101A RID: 4122
			private object this_lock = new object();

			// Token: 0x0400101B RID: 4123
			private Type type;

			// Token: 0x0400101C RID: 4124
			private object instance;
		}
	}
}
