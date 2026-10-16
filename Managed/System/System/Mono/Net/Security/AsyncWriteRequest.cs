using System;

namespace Mono.Net.Security
{
	// Token: 0x02000095 RID: 149
	internal class AsyncWriteRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00006B02 File Offset: 0x00004D02
		public AsyncWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006B9C File Offset: 0x00004D9C
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			if (base.UserBuffer.Size == 0)
			{
				base.UserResult = base.CurrentSize;
				return AsyncOperationStatus.Complete;
			}
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessWrite(base.UserBuffer);
			int item = valueTuple.Item1;
			bool item2 = valueTuple.Item2;
			if (item < 0)
			{
				base.UserResult = -1;
				return AsyncOperationStatus.Complete;
			}
			base.CurrentSize += item;
			base.UserBuffer.Offset += item;
			base.UserBuffer.Size -= item;
			if (item2)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}
