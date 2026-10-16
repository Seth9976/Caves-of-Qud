using System;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000B3 RID: 179
	internal class GooglePurchaseStateEnumProvider : IGooglePurchaseStateEnumProvider
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000C764 File Offset: 0x0000A964
		public int Purchased()
		{
			return GooglePurchaseStateEnum.Purchased();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000C76B File Offset: 0x0000A96B
		public int Pending()
		{
			return GooglePurchaseStateEnum.Pending();
		}
	}
}
