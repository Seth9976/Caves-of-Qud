using System;

namespace System.Collections
{
	// Token: 0x02000A39 RID: 2617
	[Serializable]
	internal sealed class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x06005CFF RID: 23807 RVA: 0x00138DC4 File Offset: 0x00136FC4
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x00138DFC File Offset: 0x00136FFC
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}
	}
}
