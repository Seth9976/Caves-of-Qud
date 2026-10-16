using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Utils;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000B1 RID: 177
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePurchase : IGooglePurchase
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000C4B7 File Offset: 0x0000A6B7
		public bool isAcknowledged { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000C4BF File Offset: 0x0000A6BF
		public int purchaseState { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000C4C7 File Offset: 0x0000A6C7
		public List<string> skus { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000C4CF File Offset: 0x0000A6CF
		public string orderId { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		public string receipt { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000C4DF File Offset: 0x0000A6DF
		public string signature { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000C4E7 File Offset: 0x0000A6E7
		public string originalJson { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000C4EF File Offset: 0x0000A6EF
		public string purchaseToken { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000C4F7 File Offset: 0x0000A6F7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public string obfuscatedAccountId
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000C4FF File Offset: 0x0000A6FF
		public string obfuscatedProfileId { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000C507 File Offset: 0x0000A707
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public string sku
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.skus.FirstOrDefault<string>();
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000C514 File Offset: 0x0000A714
		internal GooglePurchase(AndroidJavaObject purchase, IEnumerable<AndroidJavaObject> productDetailsEnum)
		{
			using (AndroidJavaObject androidJavaObject = purchase.Call<AndroidJavaObject>("getProducts", Array.Empty<object>()))
			{
				this.isAcknowledged = purchase.Call<bool>("isAcknowledged", Array.Empty<object>());
				this.purchaseState = purchase.Call<int>("getPurchaseState", Array.Empty<object>());
				this.skus = androidJavaObject.Enumerate<string>().ToList<string>();
				this.orderId = purchase.Call<string>("getOrderId", Array.Empty<object>());
				this.originalJson = purchase.Call<string>("getOriginalJson", Array.Empty<object>());
				this.signature = purchase.Call<string>("getSignature", Array.Empty<object>());
				this.purchaseToken = purchase.Call<string>("getPurchaseToken", Array.Empty<object>());
				AndroidJavaObject androidJavaObject2 = purchase.Call<AndroidJavaObject>("getAccountIdentifiers", Array.Empty<object>());
				this.obfuscatedAccountId = androidJavaObject2.Call<string>("getObfuscatedAccountId", Array.Empty<object>());
				this.obfuscatedProfileId = androidJavaObject2.Call<string>("getObfuscatedProfileId", Array.Empty<object>());
				List<string> list = productDetailsEnum.Select<AndroidJavaObject, string>((AndroidJavaObject productDetails) => ProductDetailsConverter.BuildProductDescription(productDetails).metadata.GetGoogleProductMetadata().originalJson).ToList<string>();
				this.receipt = GoogleReceiptEncoder.EncodeReceipt(this.originalJson, this.signature, list);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C678 File Offset: 0x0000A878
		public virtual bool IsAcknowledged()
		{
			return this.isAcknowledged;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000C680 File Offset: 0x0000A880
		public virtual bool IsPurchased()
		{
			return this.purchaseState == GooglePurchaseStateEnum.Purchased();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C68F File Offset: 0x0000A88F
		public virtual bool IsPending()
		{
			return this.purchaseState == GooglePurchaseStateEnum.Pending();
		}
	}
}
