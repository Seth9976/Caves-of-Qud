using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200058C RID: 1420
	public abstract class UxmlTypeRestriction : IEquatable<UxmlTypeRestriction>
	{
		// Token: 0x06002A82 RID: 10882 RVA: 0x00098DF4 File Offset: 0x00096FF4
		public virtual bool Equals(UxmlTypeRestriction other)
		{
			return this == other;
		}
	}
}
