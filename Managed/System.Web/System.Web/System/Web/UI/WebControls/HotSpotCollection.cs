using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects inside an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003AB RID: 939
	[Editor("System.Web.UI.Design.WebControls.HotSpotCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HotSpotCollection : StateManagedCollection
	{
		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</returns>
		/// <param name="index">The ordinal index value that specifies the location of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in the collection. </param>
		// Token: 0x17000C51 RID: 3153
		public HotSpot this[int index]
		{
			get
			{
				return (HotSpot)((IList)this)[index];
			}
		}

		/// <summary>Appends a specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to the end of the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
		/// <returns>The index at which the object was added to the collection.</returns>
		/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to append to the collection. </param>
		// Token: 0x0600265C RID: 9820 RVA: 0x000624BA File Offset: 0x000606BA
		public int Add(HotSpot spot)
		{
			return ((IList)this).Add(spot);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000624C3 File Offset: 0x000606C3
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new CircleHotSpot();
			case 1:
				return new PolygonHotSpot();
			case 2:
				return new RectangleHotSpot();
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000624F5 File Offset: 0x000606F5
		protected override Type[] GetKnownTypes()
		{
			return HotSpotCollection._knownTypes;
		}

		/// <summary>Inserts a specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object into the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection at the specified index location.</summary>
		/// <param name="index">The array index at which to add the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object. </param>
		/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to add to the collection. </param>
		// Token: 0x0600265F RID: 9823 RVA: 0x00052B3A File Offset: 0x00050D3A
		public void Insert(int index, HotSpot spot)
		{
			((IList)this).Insert(index, spot);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000624FC File Offset: 0x000606FC
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is HotSpot))
			{
				throw new ArgumentException("o is not a HotSpot");
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object from the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
		/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to remove from the collection. </param>
		// Token: 0x06002661 RID: 9825 RVA: 0x00052B44 File Offset: 0x00050D44
		public void Remove(HotSpot spot)
		{
			((IList)this).Remove(spot);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The array index from which to remove the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object. </param>
		// Token: 0x06002662 RID: 9826 RVA: 0x00052B4D File Offset: 0x00050D4D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00062518 File Offset: 0x00060718
		protected override void SetDirtyObject(object o)
		{
			((HotSpot)o).SetDirty();
		}

		// Token: 0x04001A48 RID: 6728
		private static Type[] _knownTypes = new Type[]
		{
			typeof(CircleHotSpot),
			typeof(PolygonHotSpot),
			typeof(RectangleHotSpot)
		};
	}
}
