using System;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class VisibilityAttribute : PropertyAttribute
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002AAC File Offset: 0x00000CAC
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public string PropertyName { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002ABD File Offset: 0x00000CBD
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public object Value { get; private set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x00002ACE File Offset: 0x00000CCE
		public VisibilityAttribute(string propertyName, object value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}
	}
}
