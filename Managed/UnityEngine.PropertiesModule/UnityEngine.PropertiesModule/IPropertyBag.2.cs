using System;

namespace Unity.Properties
{
	// Token: 0x02000043 RID: 67
	public interface IPropertyBag<TContainer> : IPropertyBag
	{
		// Token: 0x06000133 RID: 307
		PropertyCollection<TContainer> GetProperties();

		// Token: 0x06000134 RID: 308
		PropertyCollection<TContainer> GetProperties(ref TContainer container);

		// Token: 0x06000135 RID: 309
		TContainer CreateInstance();

		// Token: 0x06000136 RID: 310
		bool TryCreateInstance(out TContainer instance);

		// Token: 0x06000137 RID: 311
		void Accept(IPropertyBagVisitor visitor, ref TContainer container);
	}
}
