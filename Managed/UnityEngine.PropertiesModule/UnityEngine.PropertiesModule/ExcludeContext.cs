using System;

namespace Unity.Properties
{
	// Token: 0x02000064 RID: 100
	public readonly struct ExcludeContext<TContainer, TValue>
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x000065DC File Offset: 0x000047DC
		internal static ExcludeContext<TContainer, TValue> FromProperty(PropertyVisitor visitor, Property<TContainer, TValue> property)
		{
			return new ExcludeContext<TContainer, TValue>(visitor, property);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000065F5 File Offset: 0x000047F5
		public Property<TContainer, TValue> Property { get; }

		// Token: 0x060001C5 RID: 453 RVA: 0x000065FD File Offset: 0x000047FD
		private ExcludeContext(PropertyVisitor visitor, Property<TContainer, TValue> property)
		{
			this.m_Visitor = visitor;
			this.Property = property;
		}

		// Token: 0x0400008F RID: 143
		private readonly PropertyVisitor m_Visitor;
	}
}
