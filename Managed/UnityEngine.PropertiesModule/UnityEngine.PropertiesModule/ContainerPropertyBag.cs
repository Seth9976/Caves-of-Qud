using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000036 RID: 54
	public abstract class ContainerPropertyBag<TContainer> : PropertyBag<TContainer>, INamedProperties<TContainer>
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00005290 File Offset: 0x00003490
		static ContainerPropertyBag()
		{
			bool flag = !TypeTraits.IsContainer(typeof(TContainer));
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Failed to create a property bag for Type=[{0}]. The type is not a valid container type.", typeof(TContainer)));
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000052CF File Offset: 0x000034CF
		protected void AddProperty<TValue>(Property<TContainer, TValue> property)
		{
			this.m_PropertiesList.Add(property);
			this.m_PropertiesHash.Add(property.Name, property);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000052F2 File Offset: 0x000034F2
		public override PropertyCollection<TContainer> GetProperties()
		{
			return new PropertyCollection<TContainer>(this.m_PropertiesList);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000052F2 File Offset: 0x000034F2
		public override PropertyCollection<TContainer> GetProperties(ref TContainer container)
		{
			return new PropertyCollection<TContainer>(this.m_PropertiesList);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000052FF File Offset: 0x000034FF
		public bool TryGetProperty(ref TContainer container, string name, out IProperty<TContainer> property)
		{
			return this.m_PropertiesHash.TryGetValue(name, out property);
		}

		// Token: 0x04000059 RID: 89
		private readonly List<IProperty<TContainer>> m_PropertiesList = new List<IProperty<TContainer>>();

		// Token: 0x0400005A RID: 90
		private readonly Dictionary<string, IProperty<TContainer>> m_PropertiesHash = new Dictionary<string, IProperty<TContainer>>();
	}
}
