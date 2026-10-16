using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that provides access to files that were uploaded by a client.</summary>
	// Token: 0x02000095 RID: 149
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpFileCollectionWrapper : HttpFileCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpFileCollectionWrapper" /> class. </summary>
		/// <param name="httpFileCollection">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpApplicationState" /> is null.</exception>
		// Token: 0x06000746 RID: 1862 RVA: 0x000110A6 File Offset: 0x0000F2A6
		public HttpFileCollectionWrapper(HttpFileCollection httpFileCollection)
		{
			if (httpFileCollection == null)
			{
				throw new ArgumentNullException("httpFileCollection");
			}
			this.w = httpFileCollection;
		}

		/// <summary>Gets an array that contains the keys (names) of all posted file objects in the collection.</summary>
		/// <returns>An array of file names.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000110C3 File Offset: 0x0000F2C3
		public override string[] AllKeys
		{
			get
			{
				return this.w.AllKeys;
			}
		}

		/// <summary>Gets the number of objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x000110D0 File Offset: 0x0000F2D0
		public override int Count
		{
			get
			{
				return this.w.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is thread-safe.</summary>
		/// <returns>true if access is synchronized (thread-safe); otherwise, false. The default is false.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000110DD File Offset: 0x0000F2DD
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.w).IsSynchronized;
			}
		}

		/// <summary>Gets the posted file object at the specified index.</summary>
		/// <returns>The posted file object specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the item to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170002C5 RID: 709
		public override HttpPostedFileBase this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets the posted file object that has the specified name from the collection.</summary>
		/// <returns>The posted file object specified by <paramref name="name" />, if found; otherwise, null.</returns>
		/// <param name="name">The name of the object to get.</param>
		// Token: 0x170002C6 RID: 710
		public override HttpPostedFileBase this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Web.HttpApplicationStateWrapper" /> instance.</summary>
		/// <returns>A collection that contains all the keys in the collection.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x000110FC File Offset: 0x0000F2FC
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.w.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00011109 File Offset: 0x0000F309
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.w).SyncRoot;
			}
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index in the array.</summary>
		/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Web.HttpFileCollectionWrapper" /> object is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Web.HttpFileCollectionWrapper" /> object cannot be cast automatically to the type of the destination array.</exception>
		// Token: 0x0600074E RID: 1870 RVA: 0x00011116 File Offset: 0x0000F316
		public override void CopyTo(Array dest, int index)
		{
			this.w.CopyTo(dest, index);
		}

		/// <summary>Returns the posted file object at the specified index.</summary>
		/// <returns>The posted file object specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the item to return.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x0600074F RID: 1871 RVA: 0x00011128 File Offset: 0x0000F328
		public override HttpPostedFileBase Get(int index)
		{
			HttpPostedFile httpPostedFile = this.w.Get(index);
			if (httpPostedFile == null)
			{
				return null;
			}
			return new HttpPostedFileWrapper(httpPostedFile);
		}

		/// <summary>Returns the posted file object that has the specified name from the collection.</summary>
		/// <returns>The posted file object specified by <paramref name="name" />, if found; otherwise, null.</returns>
		/// <param name="name">The name of the object to return.</param>
		// Token: 0x06000750 RID: 1872 RVA: 0x00011150 File Offset: 0x0000F350
		public override HttpPostedFileBase Get(string name)
		{
			HttpPostedFile httpPostedFile = this.w.Get(name);
			if (httpPostedFile == null)
			{
				return null;
			}
			return new HttpPostedFileWrapper(httpPostedFile);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		// Token: 0x06000751 RID: 1873 RVA: 0x00011175 File Offset: 0x0000F375
		public override IEnumerator GetEnumerator()
		{
			return this.w.GetEnumerator();
		}

		/// <summary>Returns the name of the posted file object at the specified index.</summary>
		/// <returns>The name of the posted file object that is specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object name to return.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06000752 RID: 1874 RVA: 0x00011182 File Offset: 0x0000F382
		public override string GetKey(int index)
		{
			return this.w.GetKey(index);
		}

		/// <summary>Returns the data that is required in order to serialize the <see cref="T:System.Web.HttpFileCollectionWrapper" /> object.</summary>
		/// <param name="info">The information that is required in order to serialize the <see cref="T:System.Web.HttpFileCollectionWrapper" /> object.</param>
		/// <param name="context">The source and destination of the serialized stream that is associated with the <see cref="T:System.Web.HttpFileCollectionWrapper" /> object.</param>
		// Token: 0x06000753 RID: 1875 RVA: 0x00011190 File Offset: 0x0000F390
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.w.GetObjectData(info, context);
		}

		/// <summary>Raises the deserialization event when deserialization is finished.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that is associated with the current <see cref="T:System.Web.HttpFileCollectionWrapper" /> instance is invalid.</exception>
		// Token: 0x06000754 RID: 1876 RVA: 0x0001119F File Offset: 0x0000F39F
		public override void OnDeserialization(object sender)
		{
			this.w.OnDeserialization(sender);
		}

		// Token: 0x04000F65 RID: 3941
		private HttpFileCollection w;
	}
}
