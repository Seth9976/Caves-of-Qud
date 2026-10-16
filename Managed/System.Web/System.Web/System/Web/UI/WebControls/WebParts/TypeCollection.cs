using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000484 RID: 1156
	public class TypeCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06003450 RID: 13392 RVA: 0x00087FBC File Offset: 0x000861BC
		public TypeCollection()
		{
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00087FC4 File Offset: 0x000861C4
		public TypeCollection(ICollection types)
		{
			base.InnerList.AddRange(types);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000884F7 File Offset: 0x000866F7
		public TypeCollection(TypeCollection existingTypes, ICollection types)
		{
			base.InnerList.AddRange(existingTypes.InnerList);
			base.InnerList.AddRange(types);
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x00087FF8 File Offset: 0x000861F8
		public bool Contains(Type value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x0008851C File Offset: 0x0008671C
		public void CopyTo(Type[] array, int index)
		{
			base.InnerList.CopyTo(0, array, index, this.Count);
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00088015 File Offset: 0x00086215
		public int IndexOf(Type value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x17001080 RID: 4224
		public Type this[int index]
		{
			get
			{
				return (Type)base.InnerList[index];
			}
		}

		// Token: 0x04001D13 RID: 7443
		public static readonly TypeCollection Empty = new TypeCollection();
	}
}
