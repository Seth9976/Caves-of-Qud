using System;

namespace Unity.Properties
{
	// Token: 0x02000065 RID: 101
	public readonly struct ExcludeContext<TContainer>
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00006610 File Offset: 0x00004810
		internal static ExcludeContext<TContainer> FromProperty<TValue>(PropertyVisitor visitor, Property<TContainer, TValue> property)
		{
			return new ExcludeContext<TContainer>(visitor, property);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006629 File Offset: 0x00004829
		public IProperty<TContainer> Property { get; }

		// Token: 0x060001C8 RID: 456 RVA: 0x00006631 File Offset: 0x00004831
		private ExcludeContext(PropertyVisitor visitor, IProperty<TContainer> property)
		{
			this.m_Visitor = visitor;
			this.Property = property;
		}

		// Token: 0x04000091 RID: 145
		private readonly PropertyVisitor m_Visitor;
	}
}
