using System;
using System.Runtime.CompilerServices;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200003A RID: 58
	// (Invoke) Token: 0x060000B5 RID: 181
	public delegate void OnLoadBlobsBytesComplete(StorageResult result, [TupleElementNames(new string[] { "path", "contents" })] ValueTuple<string, byte[]>[] contents);
}
