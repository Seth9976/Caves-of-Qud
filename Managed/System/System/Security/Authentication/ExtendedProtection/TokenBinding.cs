using System;
using Unity;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x020002A6 RID: 678
	public class TokenBinding
	{
		// Token: 0x06001530 RID: 5424 RVA: 0x00055A4C File Offset: 0x00053C4C
		internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
		{
			this.BindingType = bindingType;
			this._rawTokenBindingId = rawData;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00055A62 File Offset: 0x00053C62
		public byte[] GetRawTokenBindingId()
		{
			if (this._rawTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._rawTokenBindingId.Clone();
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00055A7E File Offset: 0x00053C7E
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x00055A86 File Offset: 0x00053C86
		public TokenBindingType BindingType { get; private set; }

		// Token: 0x06001534 RID: 5428 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal TokenBinding()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C15 RID: 3093
		private byte[] _rawTokenBindingId;
	}
}
