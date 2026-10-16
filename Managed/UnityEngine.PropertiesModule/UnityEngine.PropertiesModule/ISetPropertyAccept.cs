using System;

namespace Unity.Properties
{
	// Token: 0x0200006D RID: 109
	public interface ISetPropertyAccept<TSet>
	{
		// Token: 0x060001D0 RID: 464
		void Accept<TContainer>(ISetPropertyVisitor visitor, Property<TContainer, TSet> property, ref TContainer container, ref TSet set);
	}
}
