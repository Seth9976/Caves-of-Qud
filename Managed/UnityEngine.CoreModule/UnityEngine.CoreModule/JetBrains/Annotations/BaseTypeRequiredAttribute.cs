using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D6 RID: 214
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[BaseTypeRequired(typeof(Attribute))]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x000085D5 File Offset: 0x000067D5
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000085E6 File Offset: 0x000067E6
		[NotNull]
		public Type BaseType { get; }
	}
}
