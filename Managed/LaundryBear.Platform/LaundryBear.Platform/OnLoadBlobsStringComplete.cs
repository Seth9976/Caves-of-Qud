using System;
using System.Runtime.CompilerServices;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000039 RID: 57
	// (Invoke) Token: 0x060000B1 RID: 177
	public delegate void OnLoadBlobsStringComplete(StorageResult result, [TupleElementNames(new string[] { "path", "contents" })] ValueTuple<string, string>[] contents);
}
