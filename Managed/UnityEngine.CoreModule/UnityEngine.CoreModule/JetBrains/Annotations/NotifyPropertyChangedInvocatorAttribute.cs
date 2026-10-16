using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D2 RID: 210
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00002082 File Offset: 0x00000282
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00008564 File Offset: 0x00006764
		public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00008575 File Offset: 0x00006775
		[CanBeNull]
		public string ParameterName { get; }
	}
}
