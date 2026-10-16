using System;

namespace UnityEngine
{
	// Token: 0x0200022F RID: 559
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	[Obsolete("Derive collection attributes from 'PropertyAttribute' and set its 'applyToCollection' property to 'true'.", false)]
	public abstract class PropertyCollectionAttribute : PropertyAttribute
	{
		// Token: 0x06001E5D RID: 7773 RVA: 0x0003A7F1 File Offset: 0x000389F1
		protected PropertyCollectionAttribute()
			: base(true)
		{
		}
	}
}
