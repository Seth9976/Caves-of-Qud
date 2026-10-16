using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Store e-mail addresses that are associated with an e-mail message.</summary>
	// Token: 0x020007F6 RID: 2038
	public class MailAddressCollection : Collection<MailAddress>
	{
		/// <summary>Add a list of e-mail addresses to the collection.</summary>
		/// <param name="addresses">The e-mail addresses to add to the <see cref="T:System.Net.Mail.MailAddressCollection" />. Multiple e-mail addresses must be separated with a comma character (","). </param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" addresses" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The<paramref name=" addresses" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.FormatException">The<paramref name=" addresses" /> parameter contains an e-mail address that is invalid or not supported. </exception>
		// Token: 0x06004127 RID: 16679 RVA: 0x000DF36C File Offset: 0x000DD56C
		public void Add(string addresses)
		{
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "addresses"), "addresses");
			}
			this.ParseValue(addresses);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The index of the e-mail address element to be replaced.</param>
		/// <param name="item">An e-mail address that will replace the element in the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" item" /> parameter is null.</exception>
		// Token: 0x06004128 RID: 16680 RVA: 0x000DF3AA File Offset: 0x000DD5AA
		protected override void SetItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		/// <summary>Inserts an e-mail address into the <see cref="T:System.Net.Mail.MailAddressCollection" />, at the specified location.</summary>
		/// <param name="index">The location at which to insert the e-mail address that is specified by <paramref name="item" />.</param>
		/// <param name="item">The e-mail address to be inserted into the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" item" /> parameter is null.</exception>
		// Token: 0x06004129 RID: 16681 RVA: 0x000DF3C2 File Offset: 0x000DD5C2
		protected override void InsertItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000DF3DC File Offset: 0x000DD5DC
		internal void ParseValue(string addresses)
		{
			IList<MailAddress> list = MailAddressParser.ParseMultipleAddresses(addresses);
			for (int i = 0; i < list.Count; i++)
			{
				base.Add(list[i]);
			}
		}

		/// <summary>Returns a string representation of the e-mail addresses in this <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the e-mail addresses in this collection.</returns>
		// Token: 0x0600412B RID: 16683 RVA: 0x000DF410 File Offset: 0x000DD610
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailAddress mailAddress in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mailAddress.ToString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000DF480 File Offset: 0x000DD680
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			foreach (MailAddress mailAddress in this)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = mailAddress.Encode(charsConsumed, allowUnicode);
				}
				else
				{
					text = text + ", " + mailAddress.Encode(1, allowUnicode);
				}
			}
			return text;
		}
	}
}
