using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007D6 RID: 2006
	internal class HeaderCollection : NameValueCollection
	{
		// Token: 0x0600404A RID: 16458 RVA: 0x000DB974 File Offset: 0x000D9B74
		internal HeaderCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000DB984 File Offset: 0x000D9B84
		public override void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType = null;
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition = null;
			}
			base.Remove(name);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x000DBA10 File Offset: 0x000D9C10
		public override string Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.Get(name);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000DBAA8 File Offset: 0x000D9CA8
		public override string[] GetValues(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.GetValues(name);
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000DBB40 File Offset: 0x000D9D40
		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000DBB49 File Offset: 0x000D9D49
		internal void InternalSet(string name, string value)
		{
			base.Set(name, value);
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000DBB53 File Offset: 0x000D9D53
		internal void InternalAdd(string name, string value)
		{
			if (MailHeaderInfo.IsSingleton(name))
			{
				base.Set(name, value);
				return;
			}
			base.Add(name, value);
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x000DBB70 File Offset: 0x000D9D70
		public override void Set(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "value"), "value");
			}
			if (!MimeBasePart.IsAscii(name, false))
			{
				throw new FormatException(SR.Format("An invalid character was found in header name.", Array.Empty<object>()));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			base.Set(name, value);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000DBC84 File Offset: 0x000D9E84
		public override void Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "value"), "value");
			}
			MailBnfHelper.ValidateHeaderName(name);
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			this.InternalAdd(name, value);
		}

		// Token: 0x04002698 RID: 9880
		private MimeBasePart _part;
	}
}
