using System;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004C0 RID: 1216
	public struct StylePropertyName : IEquatable<StylePropertyName>
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x000881C7 File Offset: 0x000863C7
		internal readonly StylePropertyId id { get; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000881CF File Offset: 0x000863CF
		private readonly string name { get; }

		// Token: 0x06002575 RID: 9589 RVA: 0x000881D8 File Offset: 0x000863D8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static StylePropertyId StylePropertyIdFromString(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out stylePropertyId);
			StylePropertyId stylePropertyId2;
			if (flag)
			{
				stylePropertyId2 = stylePropertyId;
			}
			else
			{
				stylePropertyId2 = StylePropertyId.Unknown;
			}
			return stylePropertyId2;
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00088204 File Offset: 0x00086404
		internal StylePropertyName(StylePropertyId stylePropertyId)
		{
			this.id = stylePropertyId;
			this.name = null;
			string text;
			bool flag = StylePropertyUtil.s_IdToName.TryGetValue(stylePropertyId, out text);
			if (flag)
			{
				this.name = text;
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0008823C File Offset: 0x0008643C
		public StylePropertyName(string name)
		{
			this.id = StylePropertyName.StylePropertyIdFromString(name);
			this.name = null;
			bool flag = this.id > StylePropertyId.Unknown;
			if (flag)
			{
				this.name = name;
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00088274 File Offset: 0x00086474
		public static bool IsNullOrEmpty(StylePropertyName propertyName)
		{
			return propertyName.id == StylePropertyId.Unknown;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00088290 File Offset: 0x00086490
		public static bool operator ==(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000882B4 File Offset: 0x000864B4
		public static bool operator !=(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000882DC File Offset: 0x000864DC
		public static implicit operator StylePropertyName(string name)
		{
			return new StylePropertyName(name);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000882F4 File Offset: 0x000864F4
		public override int GetHashCode()
		{
			return (int)this.id;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0008830C File Offset: 0x0008650C
		public override bool Equals(object other)
		{
			return other is StylePropertyName && this.Equals((StylePropertyName)other);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00088338 File Offset: 0x00086538
		public bool Equals(StylePropertyName other)
		{
			return this == other;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00088358 File Offset: 0x00086558
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x020004C1 RID: 1217
		internal class PropertyBag : ContainerPropertyBag<StylePropertyName>
		{
			// Token: 0x06002580 RID: 9600 RVA: 0x00088370 File Offset: 0x00086570
			public PropertyBag()
			{
				base.AddProperty<StylePropertyId>(new StylePropertyName.PropertyBag.IdProperty());
				base.AddProperty<string>(new StylePropertyName.PropertyBag.NameProperty());
			}

			// Token: 0x020004C2 RID: 1218
			private class IdProperty : Property<StylePropertyName, StylePropertyId>
			{
				// Token: 0x17000980 RID: 2432
				// (get) Token: 0x06002581 RID: 9601 RVA: 0x00088392 File Offset: 0x00086592
				public override string Name { get; } = "id";

				// Token: 0x17000981 RID: 2433
				// (get) Token: 0x06002582 RID: 9602 RVA: 0x0008839A File Offset: 0x0008659A
				public override bool IsReadOnly { get; } = true;

				// Token: 0x06002583 RID: 9603 RVA: 0x000883A2 File Offset: 0x000865A2
				public override StylePropertyId GetValue(ref StylePropertyName container)
				{
					return container.id;
				}

				// Token: 0x06002584 RID: 9604 RVA: 0x00002152 File Offset: 0x00000352
				public override void SetValue(ref StylePropertyName container, StylePropertyId value)
				{
				}
			}

			// Token: 0x020004C3 RID: 1219
			private class NameProperty : Property<StylePropertyName, string>
			{
				// Token: 0x17000982 RID: 2434
				// (get) Token: 0x06002586 RID: 9606 RVA: 0x000883C5 File Offset: 0x000865C5
				public override string Name { get; } = "name";

				// Token: 0x17000983 RID: 2435
				// (get) Token: 0x06002587 RID: 9607 RVA: 0x000883CD File Offset: 0x000865CD
				public override bool IsReadOnly { get; } = true;

				// Token: 0x06002588 RID: 9608 RVA: 0x000883D5 File Offset: 0x000865D5
				public override string GetValue(ref StylePropertyName container)
				{
					return container.name;
				}

				// Token: 0x06002589 RID: 9609 RVA: 0x00002152 File Offset: 0x00000352
				public override void SetValue(ref StylePropertyName container, string value)
				{
				}
			}
		}
	}
}
