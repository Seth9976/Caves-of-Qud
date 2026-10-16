using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000478 RID: 1144
	internal class StyleValuePropertyBag<TContainer, TValue> : ContainerPropertyBag<TContainer> where TContainer : IStyleValue<TValue>
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x00084508 File Offset: 0x00082708
		public StyleValuePropertyBag()
		{
			base.AddProperty<TValue>(new StyleValuePropertyBag<TContainer, TValue>.ValueProperty());
			base.AddProperty<StyleKeyword>(new StyleValuePropertyBag<TContainer, TValue>.KeywordProperty());
		}

		// Token: 0x02000479 RID: 1145
		private class ValueProperty : Property<TContainer, TValue>
		{
			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x0600240A RID: 9226 RVA: 0x0008452A File Offset: 0x0008272A
			public override string Name { get; } = "value";

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x0600240B RID: 9227 RVA: 0x00084532 File Offset: 0x00082732
			public override bool IsReadOnly { get; } = false;

			// Token: 0x0600240C RID: 9228 RVA: 0x0006F56D File Offset: 0x0006D76D
			public override TValue GetValue(ref TContainer container)
			{
				return container.value;
			}

			// Token: 0x0600240D RID: 9229 RVA: 0x0008453A File Offset: 0x0008273A
			public override void SetValue(ref TContainer container, TValue value)
			{
				container.value = value;
			}
		}

		// Token: 0x0200047A RID: 1146
		private class KeywordProperty : Property<TContainer, StyleKeyword>
		{
			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x0600240F RID: 9231 RVA: 0x00084565 File Offset: 0x00082765
			public override string Name { get; } = "keyword";

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06002410 RID: 9232 RVA: 0x0008456D File Offset: 0x0008276D
			public override bool IsReadOnly { get; } = false;

			// Token: 0x06002411 RID: 9233 RVA: 0x0006F5A4 File Offset: 0x0006D7A4
			public override StyleKeyword GetValue(ref TContainer container)
			{
				return container.keyword;
			}

			// Token: 0x06002412 RID: 9234 RVA: 0x00084575 File Offset: 0x00082775
			public override void SetValue(ref TContainer container, StyleKeyword value)
			{
				container.keyword = value;
			}
		}
	}
}
