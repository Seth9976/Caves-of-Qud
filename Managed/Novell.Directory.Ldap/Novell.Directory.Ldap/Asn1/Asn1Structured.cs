using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DE RID: 222
	public abstract class Asn1Structured : Asn1Object
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x00017678 File Offset: 0x00015878
		protected internal Asn1Structured(Asn1Identifier id)
			: this(id, 10)
		{
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00017683 File Offset: 0x00015883
		protected internal Asn1Structured(Asn1Identifier id, int size)
			: base(id)
		{
			this.content = new Asn1Object[size];
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00017698 File Offset: 0x00015898
		protected internal Asn1Structured(Asn1Identifier id, Asn1Object[] newContent, int size)
			: base(id)
		{
			this.content = newContent;
			this.contentIndex = size;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000176AF File Offset: 0x000158AF
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000176BC File Offset: 0x000158BC
		[CLSCompliant(false)]
		protected internal void decodeStructured(Asn1Decoder dec, Stream in_Renamed, int len)
		{
			int[] array = new int[1];
			while (len > 0)
			{
				this.add(dec.decode(in_Renamed, array));
				len -= array[0];
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000176EC File Offset: 0x000158EC
		public Asn1Object[] toArray()
		{
			Asn1Object[] array = new Asn1Object[this.contentIndex];
			Array.Copy(this.content, 0, array, 0, this.contentIndex);
			return array;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001771C File Offset: 0x0001591C
		public void add(Asn1Object value_Renamed)
		{
			if (this.contentIndex == this.content.Length)
			{
				Asn1Object[] array = new Asn1Object[this.contentIndex + this.contentIndex];
				Array.Copy(this.content, 0, array, 0, this.contentIndex);
				this.content = array;
			}
			Asn1Object[] array2 = this.content;
			int num = this.contentIndex;
			this.contentIndex = num + 1;
			array2[num] = value_Renamed;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017780 File Offset: 0x00015980
		public void set_Renamed(int index, Asn1Object value_Renamed)
		{
			if (index >= this.contentIndex || index < 0)
			{
				throw new IndexOutOfRangeException("Asn1Structured: get: index " + index.ToString() + ", size " + this.contentIndex.ToString());
			}
			this.content[index] = value_Renamed;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000177BF File Offset: 0x000159BF
		public Asn1Object get_Renamed(int index)
		{
			if (index >= this.contentIndex || index < 0)
			{
				throw new IndexOutOfRangeException("Asn1Structured: set: index " + index.ToString() + ", size " + this.contentIndex.ToString());
			}
			return this.content[index];
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000177FD File Offset: 0x000159FD
		public int size()
		{
			return this.contentIndex;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017808 File Offset: 0x00015A08
		[CLSCompliant(false)]
		public virtual string toString(string type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(type);
			for (int i = 0; i < this.contentIndex; i++)
			{
				stringBuilder.Append(this.content[i]);
				if (i != this.contentIndex - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(" }");
			return base.ToString() + stringBuilder.ToString();
		}

		// Token: 0x040004BA RID: 1210
		private Asn1Object[] content;

		// Token: 0x040004BB RID: 1211
		private int contentIndex;
	}
}
