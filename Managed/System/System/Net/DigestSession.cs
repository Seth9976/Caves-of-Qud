using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x02000678 RID: 1656
	internal class DigestSession
	{
		// Token: 0x06003408 RID: 13320 RVA: 0x000B5569 File Offset: 0x000B3769
		public DigestSession()
		{
			this._nc = 1;
			this.lastUse = DateTime.UtcNow;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000B5583 File Offset: 0x000B3783
		public string Algorithm
		{
			get
			{
				return this.parser.Algorithm;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000B5590 File Offset: 0x000B3790
		public string Realm
		{
			get
			{
				return this.parser.Realm;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000B559D File Offset: 0x000B379D
		public string Nonce
		{
			get
			{
				return this.parser.Nonce;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000B55AA File Offset: 0x000B37AA
		public string Opaque
		{
			get
			{
				return this.parser.Opaque;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000B55B7 File Offset: 0x000B37B7
		public string QOP
		{
			get
			{
				return this.parser.QOP;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000B55C4 File Offset: 0x000B37C4
		public string CNonce
		{
			get
			{
				if (this._cnonce == null)
				{
					byte[] array = new byte[15];
					DigestSession.rng.GetBytes(array);
					this._cnonce = Convert.ToBase64String(array);
					Array.Clear(array, 0, array.Length);
				}
				return this._cnonce;
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000B5608 File Offset: 0x000B3808
		public bool Parse(string challenge)
		{
			this.parser = new DigestHeaderParser(challenge);
			if (!this.parser.Parse())
			{
				return false;
			}
			if (this.parser.Algorithm == null || this.parser.Algorithm.ToUpper().StartsWith("MD5"))
			{
				this.hash = MD5.Create();
			}
			return true;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000B5668 File Offset: 0x000B3868
		private string HashToHexString(string toBeHashed)
		{
			if (this.hash == null)
			{
				return null;
			}
			this.hash.Initialize();
			byte[] array = this.hash.ComputeHash(Encoding.ASCII.GetBytes(toBeHashed));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000B56D4 File Offset: 0x000B38D4
		private string HA1(string username, string password)
		{
			string text = string.Format("{0}:{1}:{2}", username, this.Realm, password);
			if (this.Algorithm != null && this.Algorithm.ToLower() == "md5-sess")
			{
				text = string.Format("{0}:{1}:{2}", this.HashToHexString(text), this.Nonce, this.CNonce);
			}
			return this.HashToHexString(text);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000B5738 File Offset: 0x000B3938
		private string HA2(HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}", webRequest.Method, webRequest.RequestUri.PathAndQuery);
			this.QOP == "auth-int";
			return this.HashToHexString(text);
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000B577C File Offset: 0x000B397C
		private string Response(string username, string password, HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}:", this.HA1(username, password), this.Nonce);
			if (this.QOP != null)
			{
				text += string.Format("{0}:{1}:{2}:", this._nc.ToString("X8"), this.CNonce, this.QOP);
			}
			text += this.HA2(webRequest);
			return this.HashToHexString(text);
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000B57EC File Offset: 0x000B39EC
		public Authorization Authenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (this.parser == null)
			{
				throw new InvalidOperationException();
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			this.lastUse = DateTime.UtcNow;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "digest");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == "")
			{
				return null;
			}
			string password = credential.Password;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Digest username=\"{0}\", ", userName);
			stringBuilder.AppendFormat("realm=\"{0}\", ", this.Realm);
			stringBuilder.AppendFormat("nonce=\"{0}\", ", this.Nonce);
			stringBuilder.AppendFormat("uri=\"{0}\", ", httpWebRequest.Address.PathAndQuery);
			if (this.Algorithm != null)
			{
				stringBuilder.AppendFormat("algorithm=\"{0}\", ", this.Algorithm);
			}
			stringBuilder.AppendFormat("response=\"{0}\", ", this.Response(userName, password, httpWebRequest));
			if (this.QOP != null)
			{
				stringBuilder.AppendFormat("qop=\"{0}\", ", this.QOP);
			}
			lock (this)
			{
				if (this.QOP != null)
				{
					stringBuilder.AppendFormat("nc={0:X8}, ", this._nc);
					this._nc++;
				}
			}
			if (this.CNonce != null)
			{
				stringBuilder.AppendFormat("cnonce=\"{0}\", ", this.CNonce);
			}
			if (this.Opaque != null)
			{
				stringBuilder.AppendFormat("opaque=\"{0}\", ", this.Opaque);
			}
			stringBuilder.Length -= 2;
			return new Authorization(stringBuilder.ToString());
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000B59A0 File Offset: 0x000B3BA0
		public DateTime LastUse
		{
			get
			{
				return this.lastUse;
			}
		}

		// Token: 0x04001E9E RID: 7838
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		// Token: 0x04001E9F RID: 7839
		private DateTime lastUse;

		// Token: 0x04001EA0 RID: 7840
		private int _nc;

		// Token: 0x04001EA1 RID: 7841
		private HashAlgorithm hash;

		// Token: 0x04001EA2 RID: 7842
		private DigestHeaderParser parser;

		// Token: 0x04001EA3 RID: 7843
		private string _cnonce;
	}
}
