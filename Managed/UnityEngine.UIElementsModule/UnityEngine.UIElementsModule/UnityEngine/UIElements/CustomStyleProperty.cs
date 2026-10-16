using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000351 RID: 849
	public struct CustomStyleProperty<T> : IEquatable<CustomStyleProperty<T>>
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00069A0F File Offset: 0x00067C0F
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x00069A17 File Offset: 0x00067C17
		public string name { readonly get; private set; }

		// Token: 0x0600192A RID: 6442 RVA: 0x00069A20 File Offset: 0x00067C20
		public CustomStyleProperty(string propertyName)
		{
			bool flag = !string.IsNullOrEmpty(propertyName) && !propertyName.StartsWith("--");
			if (flag)
			{
				throw new ArgumentException("Custom style property \"" + propertyName + "\" must start with \"--\" prefix.");
			}
			this.name = propertyName;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00069A6C File Offset: 0x00067C6C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is CustomStyleProperty<T>);
			return !flag && this.Equals((CustomStyleProperty<T>)obj);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00069AA0 File Offset: 0x00067CA0
		public bool Equals(CustomStyleProperty<T> other)
		{
			return this.name == other.name;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00069AC4 File Offset: 0x00067CC4
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00069AE4 File Offset: 0x00067CE4
		public static bool operator ==(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00069B00 File Offset: 0x00067D00
		public static bool operator !=(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return !(a == b);
		}
	}
}
