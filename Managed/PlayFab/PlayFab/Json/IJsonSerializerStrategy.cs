using System;
using System.CodeDom.Compiler;

namespace PlayFab.Json
{
	// Token: 0x0200003A RID: 58
	[GeneratedCode("simple-json", "1.0.0")]
	public interface IJsonSerializerStrategy
	{
		// Token: 0x06000359 RID: 857
		bool TrySerializeNonPrimitiveObject(object input, out object output);

		// Token: 0x0600035A RID: 858
		object DeserializeObject(object value, Type type);
	}
}
