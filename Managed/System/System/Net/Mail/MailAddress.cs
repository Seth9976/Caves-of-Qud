using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the address of an electronic mail sender or recipient.</summary>
	// Token: 0x020007F5 RID: 2037
	public class MailAddress
	{
		// Token: 0x06004114 RID: 16660 RVA: 0x000DF00C File Offset: 0x000DD20C
		internal MailAddress(string displayName, string userName, string domain)
		{
			this._host = domain;
			this._userName = userName;
			this._displayName = displayName;
			this._displayNameEncoding = Encoding.GetEncoding("utf-8");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address. </summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.</exception>
		// Token: 0x06004115 RID: 16661 RVA: 0x000DF039 File Offset: 0x000DD239
		public MailAddress(string address)
			: this(address, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address and display name.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />. This parameter can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06004116 RID: 16662 RVA: 0x000DF044 File Offset: 0x000DD244
		public MailAddress(string address, string displayName)
			: this(address, displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address, display name, and encoding.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />.</param>
		/// <param name="displayNameEncoding">The <see cref="T:System.Text.Encoding" /> that defines the character set used for <paramref name="displayName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="displayName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="displayName" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06004117 RID: 16663 RVA: 0x000DF050 File Offset: 0x000DD250
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "address"), "address");
			}
			this._displayNameEncoding = displayNameEncoding ?? Encoding.GetEncoding("utf-8");
			this._displayName = displayName ?? string.Empty;
			if (!string.IsNullOrEmpty(this._displayName))
			{
				this._displayName = MailAddressParser.NormalizeOrThrow(this._displayName);
				if (this._displayName.Length >= 2 && this._displayName[0] == '"' && this._displayName[this._displayName.Length - 1] == '"')
				{
					this._displayName = this._displayName.Substring(1, this._displayName.Length - 2);
				}
			}
			MailAddress mailAddress = MailAddressParser.ParseAddress(address);
			this._host = mailAddress._host;
			this._userName = mailAddress._userName;
			if (string.IsNullOrEmpty(this._displayName))
			{
				this._displayName = mailAddress._displayName;
			}
		}

		/// <summary>Gets the display name composed from the display name and address information specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display name; otherwise, <see cref="F:System.String.Empty" /> ("") if no display name information was specified when this instance was created.</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x000DF16C File Offset: 0x000DD36C
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		/// <summary>Gets the user information from the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user name portion of the <see cref="P:System.Net.Mail.MailAddress.Address" />.</returns>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x000DF174 File Offset: 0x000DD374
		public string User
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000DF17C File Offset: 0x000DD37C
		private string GetUser(bool allowUnicode)
		{
			if (!allowUnicode && !MimeBasePart.IsAscii(this._userName, true))
			{
				throw new SmtpException(SR.Format("The client or server is only configured for E-mail addresses with ASCII local-parts: {0}.", this.Address));
			}
			return this._userName;
		}

		/// <summary>Gets the host portion of the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the host computer that accepts e-mail for the <see cref="P:System.Net.Mail.MailAddress.User" /> property.</returns>
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000DF1AB File Offset: 0x000DD3AB
		public string Host
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000DF1B4 File Offset: 0x000DD3B4
		private string GetHost(bool allowUnicode)
		{
			string text = this._host;
			if (!allowUnicode && !MimeBasePart.IsAscii(text, true))
			{
				IdnMapping idnMapping = new IdnMapping();
				try
				{
					text = idnMapping.GetAscii(text);
				}
				catch (ArgumentException ex)
				{
					throw new SmtpException(SR.Format("The address has an invalid host name: {0}.", this.Address), ex);
				}
			}
			return text;
		}

		/// <summary>Gets the e-mail address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the e-mail address.</returns>
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x000DF210 File Offset: 0x000DD410
		public string Address
		{
			get
			{
				return this._userName + "@" + this._host;
			}
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x000DF228 File Offset: 0x000DD428
		private string GetAddress(bool allowUnicode)
		{
			return this.GetUser(allowUnicode) + "@" + this.GetHost(allowUnicode);
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000DF242 File Offset: 0x000DD442
		private string SmtpAddress
		{
			get
			{
				return "<" + this.Address + ">";
			}
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x000DF259 File Offset: 0x000DD459
		internal string GetSmtpAddress(bool allowUnicode)
		{
			return "<" + this.GetAddress(allowUnicode) + ">";
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the contents of this <see cref="T:System.Net.Mail.MailAddress" />.</returns>
		// Token: 0x06004121 RID: 16673 RVA: 0x000DF271 File Offset: 0x000DD471
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				return this.Address;
			}
			return "\"" + this.DisplayName + "\" " + this.SmtpAddress;
		}

		/// <summary>Compares two mail addresses.</summary>
		/// <returns>true if the two mail addresses are equal; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Net.Mail.MailAddress" /> instance to compare to the current instance.</param>
		// Token: 0x06004122 RID: 16674 RVA: 0x000DF2A2 File Offset: 0x000DD4A2
		public override bool Equals(object value)
		{
			return value != null && this.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>Returns a hash value for a mail address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06004123 RID: 16675 RVA: 0x000B73EA File Offset: 0x000B55EA
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000DF2BC File Offset: 0x000DD4BC
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this._displayName))
			{
				if (MimeBasePart.IsAscii(this._displayName, false) || allowUnicode)
				{
					text = "\"" + this._displayName + "\"";
				}
				else
				{
					IEncodableStream encoderForHeader = MailAddress.s_encoderFactory.GetEncoderForHeader(this._displayNameEncoding, false, charsConsumed);
					byte[] bytes = this._displayNameEncoding.GetBytes(this._displayName);
					encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
					text = encoderForHeader.GetEncodedString();
				}
				text = text + " " + this.GetSmtpAddress(allowUnicode);
			}
			else
			{
				text = this.GetAddress(allowUnicode);
			}
			return text;
		}

		// Token: 0x0400270E RID: 9998
		private readonly Encoding _displayNameEncoding;

		// Token: 0x0400270F RID: 9999
		private readonly string _displayName;

		// Token: 0x04002710 RID: 10000
		private readonly string _userName;

		// Token: 0x04002711 RID: 10001
		private readonly string _host;

		// Token: 0x04002712 RID: 10002
		private static readonly EncodedStreamFactory s_encoderFactory = new EncodedStreamFactory();
	}
}
