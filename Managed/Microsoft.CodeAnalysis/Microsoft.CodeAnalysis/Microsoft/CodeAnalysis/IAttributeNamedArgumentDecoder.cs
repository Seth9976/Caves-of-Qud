using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A2 RID: 418
	internal interface IAttributeNamedArgumentDecoder
	{
		// Token: 0x06001089 RID: 4233
		[return: TupleElementNames(new string[] { "nameValuePair", "isProperty", "typeCode", "elementTypeCode" })]
		[return: Nullable(new byte[] { 0, 0, 1 })]
		ValueTuple<KeyValuePair<string, TypedConstant>, bool, SerializationTypeCode, SerializationTypeCode> DecodeCustomAttributeNamedArgumentOrThrow(ref BlobReader argReader);
	}
}
