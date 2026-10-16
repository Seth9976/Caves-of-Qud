using System;
using System.Collections;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000F0 RID: 240
	internal class MailAddressCollection : IEnumerable
	{
		// Token: 0x17000489 RID: 1161
		public MailAddress this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002296B File Offset: 0x00020B6B
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00022978 File Offset: 0x00020B78
		public void Add(MailAddress addr)
		{
			this.data.Add(addr);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00022987 File Offset: 0x00020B87
		public MailAddress Get(int index)
		{
			return (MailAddress)this.data[index];
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002299A File Offset: 0x00020B9A
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000229A8 File Offset: 0x00020BA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.data.Count; i++)
			{
				MailAddress mailAddress = this.Get(i);
				stringBuilder.Append(mailAddress);
				if (i != this.data.Count - 1)
				{
					stringBuilder.Append(",\r\n  ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00022A04 File Offset: 0x00020C04
		public static MailAddressCollection Parse(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("Null is not allowed as an address string");
			}
			MailAddressCollection mailAddressCollection = new MailAddressCollection();
			string[] array = str.Split(new char[] { ',', ';' });
			for (int i = 0; i < array.Length; i++)
			{
				MailAddress mailAddress = MailAddress.Parse(array[i]);
				if (mailAddress != null)
				{
					mailAddressCollection.Add(mailAddress);
				}
			}
			return mailAddressCollection;
		}

		// Token: 0x04001125 RID: 4389
		protected ArrayList data = new ArrayList();
	}
}
