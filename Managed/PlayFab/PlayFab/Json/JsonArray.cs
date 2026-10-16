using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlayFab.Json
{
	// Token: 0x02000037 RID: 55
	[GeneratedCode("simple-json", "1.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class JsonArray : List<object>
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00010553 File Offset: 0x0000E753
		public JsonArray()
		{
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001055B File Offset: 0x0000E75B
		public JsonArray(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00010564 File Offset: 0x0000E764
		public override string ToString()
		{
			return PlayFabSimpleJson.SerializeObject(this, null) ?? string.Empty;
		}
	}
}
