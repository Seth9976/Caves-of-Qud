using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020005FA RID: 1530
	internal class KnownHttpVerb
	{
		// Token: 0x0600307C RID: 12412 RVA: 0x000A6C34 File Offset: 0x000A4E34
		internal KnownHttpVerb(string name, bool requireContentBody, bool contentBodyNotAllowed, bool connectRequest, bool expectNoContentResponse)
		{
			this.Name = name;
			this.RequireContentBody = requireContentBody;
			this.ContentBodyNotAllowed = contentBodyNotAllowed;
			this.ConnectRequest = connectRequest;
			this.ExpectNoContentResponse = expectNoContentResponse;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000A6C64 File Offset: 0x000A4E64
		static KnownHttpVerb()
		{
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Get.Name] = KnownHttpVerb.Get;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Connect.Name] = KnownHttpVerb.Connect;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Head.Name] = KnownHttpVerb.Head;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Put.Name] = KnownHttpVerb.Put;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Post.Name] = KnownHttpVerb.Post;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.MkCol.Name] = KnownHttpVerb.MkCol;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000A6D88 File Offset: 0x000A4F88
		public bool Equals(KnownHttpVerb verb)
		{
			return this == verb || string.Compare(this.Name, verb.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000A6DA8 File Offset: 0x000A4FA8
		public static KnownHttpVerb Parse(string name)
		{
			KnownHttpVerb knownHttpVerb = KnownHttpVerb.NamedHeaders[name] as KnownHttpVerb;
			if (knownHttpVerb == null)
			{
				knownHttpVerb = new KnownHttpVerb(name, false, false, false, false);
			}
			return knownHttpVerb;
		}

		// Token: 0x04001C29 RID: 7209
		internal string Name;

		// Token: 0x04001C2A RID: 7210
		internal bool RequireContentBody;

		// Token: 0x04001C2B RID: 7211
		internal bool ContentBodyNotAllowed;

		// Token: 0x04001C2C RID: 7212
		internal bool ConnectRequest;

		// Token: 0x04001C2D RID: 7213
		internal bool ExpectNoContentResponse;

		// Token: 0x04001C2E RID: 7214
		private static ListDictionary NamedHeaders = new ListDictionary(CaseInsensitiveAscii.StaticInstance);

		// Token: 0x04001C2F RID: 7215
		internal static KnownHttpVerb Get = new KnownHttpVerb("GET", false, true, false, false);

		// Token: 0x04001C30 RID: 7216
		internal static KnownHttpVerb Connect = new KnownHttpVerb("CONNECT", false, true, true, false);

		// Token: 0x04001C31 RID: 7217
		internal static KnownHttpVerb Head = new KnownHttpVerb("HEAD", false, true, false, true);

		// Token: 0x04001C32 RID: 7218
		internal static KnownHttpVerb Put = new KnownHttpVerb("PUT", true, false, false, false);

		// Token: 0x04001C33 RID: 7219
		internal static KnownHttpVerb Post = new KnownHttpVerb("POST", true, false, false, false);

		// Token: 0x04001C34 RID: 7220
		internal static KnownHttpVerb MkCol = new KnownHttpVerb("MKCOL", false, false, false, false);
	}
}
