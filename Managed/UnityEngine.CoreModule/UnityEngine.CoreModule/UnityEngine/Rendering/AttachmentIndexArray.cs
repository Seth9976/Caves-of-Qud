using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000528 RID: 1320
	public struct AttachmentIndexArray
	{
		// Token: 0x060032A0 RID: 12960 RVA: 0x0006220C File Offset: 0x0006040C
		public AttachmentIndexArray(int numAttachments)
		{
			bool flag = numAttachments < 0 || numAttachments > 8;
			if (flag)
			{
				throw new ArgumentException(string.Format("AttachmentIndexArray - numAttachments must be in range of [0, {0}[", 8));
			}
			this.a0 = (this.a1 = (this.a2 = (this.a3 = (this.a4 = (this.a5 = (this.a6 = (this.a7 = -1)))))));
			this.activeAttachments = numAttachments;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x00062290 File Offset: 0x00060490
		public AttachmentIndexArray(int[] attachments)
		{
			this = new AttachmentIndexArray(attachments.Length);
			for (int i = 0; i < this.activeAttachments; i++)
			{
				this[i] = attachments[i];
			}
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000622CC File Offset: 0x000604CC
		public AttachmentIndexArray(NativeArray<int> attachments)
		{
			this = new AttachmentIndexArray(attachments.Length);
			for (int i = 0; i < this.activeAttachments; i++)
			{
				this[i] = attachments[i];
			}
		}

		// Token: 0x17000802 RID: 2050
		public unsafe int this[int index]
		{
			get
			{
				bool flag = index >= 8;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", 8));
				}
				bool flag2 = (ulong)index >= (ulong)((long)this.activeAttachments);
				if (flag2)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", this.activeAttachments));
				}
				fixed (AttachmentIndexArray* ptr = &this)
				{
					AttachmentIndexArray* ptr2 = ptr;
					int* ptr3 = (int*)ptr2;
					return ptr3[index];
				}
			}
			set
			{
				bool flag = index >= 8;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", 8));
				}
				bool flag2 = (ulong)index >= (ulong)((long)this.activeAttachments);
				if (flag2)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", this.activeAttachments));
				}
				fixed (AttachmentIndexArray* ptr = &this)
				{
					AttachmentIndexArray* ptr2 = ptr;
					int* ptr3 = (int*)ptr2;
					ptr3[index] = value;
				}
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000623F8 File Offset: 0x000605F8
		public int Length
		{
			get
			{
				return this.activeAttachments;
			}
		}

		// Token: 0x040010A2 RID: 4258
		public static AttachmentIndexArray Emtpy = new AttachmentIndexArray(0);

		// Token: 0x040010A3 RID: 4259
		public const int MaxAttachments = 8;

		// Token: 0x040010A4 RID: 4260
		private int a0;

		// Token: 0x040010A5 RID: 4261
		private int a1;

		// Token: 0x040010A6 RID: 4262
		private int a2;

		// Token: 0x040010A7 RID: 4263
		private int a3;

		// Token: 0x040010A8 RID: 4264
		private int a4;

		// Token: 0x040010A9 RID: 4265
		private int a5;

		// Token: 0x040010AA RID: 4266
		private int a6;

		// Token: 0x040010AB RID: 4267
		private int a7;

		// Token: 0x040010AC RID: 4268
		private int activeAttachments;
	}
}
