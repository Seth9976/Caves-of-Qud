using System;
using System.IO;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x02000100 RID: 256
	internal class UUAttachmentEncoder : IAttachmentEncoder
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x00024288 File Offset: 0x00022488
		public UUAttachmentEncoder(int mode, string fileName)
		{
			string text = "\r\n";
			this.beginTag = Encoding.ASCII.GetBytes(string.Concat(new string[]
			{
				"begin ",
				mode.ToString(),
				" ",
				fileName,
				text
			}));
			this.endTag = Encoding.ASCII.GetBytes("`" + text + "end" + text);
			this.endl = Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00024310 File Offset: 0x00022510
		public void EncodeStream(Stream ins, Stream outs)
		{
			outs.Write(this.beginTag, 0, this.beginTag.Length);
			ToUUEncodingTransform toUUEncodingTransform = new ToUUEncodingTransform();
			byte[] array = new byte[toUUEncodingTransform.InputBlockSize];
			byte[] array2 = new byte[toUUEncodingTransform.OutputBlockSize];
			int num;
			for (;;)
			{
				num = ins.Read(array, 0, array.Length);
				if (num < 1)
				{
					goto IL_00A7;
				}
				if (num != toUUEncodingTransform.InputBlockSize)
				{
					break;
				}
				toUUEncodingTransform.TransformBlock(array, 0, num, array2, 0);
				outs.Write(array2, 0, array2.Length);
				outs.Write(this.endl, 0, this.endl.Length);
			}
			byte[] array3 = toUUEncodingTransform.TransformFinalBlock(array, 0, num);
			outs.Write(array3, 0, array3.Length);
			outs.Write(this.endl, 0, this.endl.Length);
			IL_00A7:
			outs.Write(this.endTag, 0, this.endTag.Length);
		}

		// Token: 0x0400115A RID: 4442
		protected byte[] beginTag;

		// Token: 0x0400115B RID: 4443
		protected byte[] endTag;

		// Token: 0x0400115C RID: 4444
		protected byte[] endl;
	}
}
