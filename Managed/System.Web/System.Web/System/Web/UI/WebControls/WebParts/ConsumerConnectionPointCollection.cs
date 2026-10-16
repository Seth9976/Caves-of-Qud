using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a collection of connection points for a control in a Web Parts zone acting as a consumer. This class cannot be inherited.</summary>
	// Token: 0x020006D4 RID: 1748
	public sealed class ConsumerConnectionPointCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> class. </summary>
		// Token: 0x06004A12 RID: 18962 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ConsumerConnectionPointCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> class using the specified collection of connection points. </summary>
		/// <param name="connectionPoints">A collection of consumer connection points.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="connectionPoints" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The collection contains an invalid connection point.- or -The collection contains an item with a duplicate ID.</exception>
		// Token: 0x06004A13 RID: 18963 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ConsumerConnectionPointCollection(ICollection connectionPoints)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default consumer connection point.</summary>
		/// <returns>The default <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />.</returns>
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ConsumerConnectionPoint Default
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ConsumerConnectionPoint get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the connection point with the specified <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionPoint.ID" /> property. </summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> in the collection whose ID matches the value of <paramref name="id" />.</returns>
		/// <param name="id">A string value representing the connection point ID of the connection point to be retrieved.</param>
		// Token: 0x170016F2 RID: 5874
		public ConsumerConnectionPoint this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> object is in the collection.</summary>
		/// <returns>true if the consumer connection point is contained in the collection; otherwise, false.</returns>
		/// <param name="connectionPoint">The connection point to search for.</param>
		// Token: 0x06004A17 RID: 18967 RVA: 0x000C7D5C File Offset: 0x000C5F5C
		public bool Contains(ConsumerConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> objects that receives the copied items from the current <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x06004A18 RID: 18968 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(ConsumerConnectionPoint[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="connectionPoint" /> within the current <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" />, if found; otherwise, -1.</returns>
		/// <param name="connectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> to locate.</param>
		// Token: 0x06004A19 RID: 18969 RVA: 0x000C7D78 File Offset: 0x000C5F78
		public int IndexOf(ConsumerConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
