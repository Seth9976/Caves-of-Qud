using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000679 RID: 1657
	internal class DigestClient : IAuthenticationModule
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000B59A8 File Offset: 0x000B3BA8
		private static Hashtable Cache
		{
			get
			{
				object syncRoot = DigestClient.cache.SyncRoot;
				lock (syncRoot)
				{
					DigestClient.CheckExpired(DigestClient.cache.Count);
				}
				return DigestClient.cache;
			}
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000B59FC File Offset: 0x000B3BFC
		private static void CheckExpired(int count)
		{
			if (count < 10)
			{
				return;
			}
			DateTime dateTime = DateTime.MaxValue;
			DateTime utcNow = DateTime.UtcNow;
			ArrayList arrayList = null;
			foreach (object obj in DigestClient.cache.Keys)
			{
				int num = (int)obj;
				DigestSession digestSession = (DigestSession)DigestClient.cache[num];
				if (digestSession.LastUse < dateTime && (digestSession.LastUse - utcNow).Ticks > 6000000000L)
				{
					dateTime = digestSession.LastUse;
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(num);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					int num2 = (int)obj2;
					DigestClient.cache.Remove(num2);
				}
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000B5B28 File Offset: 0x000B3D28
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("digest") == -1)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			DigestSession digestSession = new DigestSession();
			if (!digestSession.Parse(challenge))
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode() ^ digestSession.Nonce.GetHashCode();
			DigestSession digestSession2 = (DigestSession)DigestClient.Cache[num];
			bool flag = digestSession2 == null;
			if (flag)
			{
				digestSession2 = digestSession;
			}
			else if (!digestSession2.Parse(challenge))
			{
				return null;
			}
			if (flag)
			{
				DigestClient.Cache.Add(num, digestSession2);
			}
			return digestSession2.Authenticate(webRequest, credentials);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000B5BE0 File Offset: 0x000B3DE0
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			if (credentials == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			if (digestSession == null)
			{
				return null;
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000B5C34 File Offset: 0x000B3E34
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001EA4 RID: 7844
		private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
	}
}
