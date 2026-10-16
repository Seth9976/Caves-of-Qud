using System;

namespace Unity.Properties
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class GeneratePropertyBagsForTypeAttribute : Attribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E4B File Offset: 0x0000104B
		public Type Type { get; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002E54 File Offset: 0x00001054
		public GeneratePropertyBagsForTypeAttribute(Type type)
		{
			bool flag = !TypeTraits.IsContainer(type);
			if (flag)
			{
				throw new ArgumentException(type.Name + " is not a valid container type.");
			}
			this.Type = type;
		}
	}
}
