using System;

namespace Unity.Properties
{
	// Token: 0x0200006E RID: 110
	public interface IDictionaryPropertyAccept<TDictionary>
	{
		// Token: 0x060001D1 RID: 465
		void Accept<TContainer>(IDictionaryPropertyVisitor visitor, Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary dictionary);
	}
}
