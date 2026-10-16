using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.Purchasing.Security;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200007F RID: 127
	public class SubscriptionInfo
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00009EF8 File Offset: 0x000080F8
		public SubscriptionInfo(AppleInAppPurchaseReceipt r, string intro_json)
		{
			AppleStoreProductType appleStoreProductType = (AppleStoreProductType)Enum.Parse(typeof(AppleStoreProductType), r.productType.ToString());
			if (appleStoreProductType == AppleStoreProductType.Consumable || appleStoreProductType == AppleStoreProductType.NonConsumable)
			{
				throw new InvalidProductTypeException();
			}
			if (!string.IsNullOrEmpty(intro_json))
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(intro_json);
				int num = -1;
				SubscriptionPeriodUnit subscriptionPeriodUnit = SubscriptionPeriodUnit.NotAvailable;
				this.introductory_price = dictionary.TryGetString("introductoryPrice") + dictionary.TryGetString("introductoryPriceLocale");
				if (string.IsNullOrEmpty(this.introductory_price))
				{
					this.introductory_price = "not available";
				}
				else
				{
					try
					{
						this.introductory_price_cycles = Convert.ToInt64(dictionary.TryGetString("introductoryPriceNumberOfPeriods"));
						num = Convert.ToInt32(dictionary.TryGetString("numberOfUnits"));
						subscriptionPeriodUnit = (SubscriptionPeriodUnit)Convert.ToInt32(dictionary.TryGetString("unit"));
					}
					catch (Exception ex)
					{
						Debug.unityLogger.Log("Unable to parse introductory period cycles and duration, this product does not have configuration of introductory price period", ex);
						subscriptionPeriodUnit = SubscriptionPeriodUnit.NotAvailable;
					}
				}
				DateTime now = DateTime.Now;
				switch (subscriptionPeriodUnit)
				{
				case SubscriptionPeriodUnit.Day:
					this.introductory_price_period = TimeSpan.FromTicks(TimeSpan.FromDays(1.0).Ticks * (long)num);
					break;
				case SubscriptionPeriodUnit.Month:
					this.introductory_price_period = TimeSpan.FromTicks((now.AddMonths(1) - now).Ticks * (long)num);
					break;
				case SubscriptionPeriodUnit.Week:
					this.introductory_price_period = TimeSpan.FromTicks(TimeSpan.FromDays(7.0).Ticks * (long)num);
					break;
				case SubscriptionPeriodUnit.Year:
					this.introductory_price_period = TimeSpan.FromTicks((now.AddYears(1) - now).Ticks * (long)num);
					break;
				case SubscriptionPeriodUnit.NotAvailable:
					this.introductory_price_period = TimeSpan.Zero;
					this.introductory_price_cycles = 0L;
					break;
				}
			}
			else
			{
				this.introductory_price = "not available";
				this.introductory_price_period = TimeSpan.Zero;
				this.introductory_price_cycles = 0L;
			}
			DateTime utcNow = DateTime.UtcNow;
			this.purchaseDate = r.purchaseDate;
			this.productId = r.productID;
			this.subscriptionExpireDate = r.subscriptionExpirationDate;
			this.subscriptionCancelDate = r.cancellationDate;
			if (appleStoreProductType == AppleStoreProductType.NonRenewingSubscription)
			{
				this.is_subscribed = Result.Unsupported;
				this.is_expired = Result.Unsupported;
				this.is_cancelled = Result.Unsupported;
				this.is_free_trial = Result.Unsupported;
				this.is_auto_renewing = Result.Unsupported;
				this.is_introductory_price_period = Result.Unsupported;
			}
			else
			{
				this.is_cancelled = ((r.cancellationDate.Ticks > 0L && r.cancellationDate.Ticks < utcNow.Ticks) ? Result.True : Result.False);
				this.is_subscribed = ((r.subscriptionExpirationDate.Ticks >= utcNow.Ticks) ? Result.True : Result.False);
				this.is_expired = ((r.subscriptionExpirationDate.Ticks > 0L && r.subscriptionExpirationDate.Ticks < utcNow.Ticks) ? Result.True : Result.False);
				this.is_free_trial = ((r.isFreeTrial == 1) ? Result.True : Result.False);
				this.is_auto_renewing = ((appleStoreProductType == AppleStoreProductType.AutoRenewingSubscription && this.is_cancelled == Result.False && this.is_expired == Result.False) ? Result.True : Result.False);
				this.is_introductory_price_period = ((r.isIntroductoryPricePeriod == 1) ? Result.True : Result.False);
			}
			this.remainedTime = ((this.is_subscribed == Result.True) ? r.subscriptionExpirationDate.Subtract(utcNow) : TimeSpan.Zero);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000A25C File Offset: 0x0000845C
		public SubscriptionInfo(string skuDetails, bool isAutoRenewing, DateTime purchaseDate, bool isFreeTrial, bool hasIntroductoryPriceTrial, bool purchaseHistorySupported, string updateMetadata)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(skuDetails);
			object obj;
			if (!dictionary.TryGetValue("type", out obj) || (string)obj == "inapp")
			{
				throw new InvalidProductTypeException();
			}
			object obj2;
			bool flag = dictionary.TryGetValue("productId", out obj2);
			this.productId = null;
			if (flag)
			{
				this.productId = obj2 as string;
			}
			this.purchaseDate = purchaseDate;
			this.is_subscribed = Result.True;
			this.is_auto_renewing = (isAutoRenewing ? Result.True : Result.False);
			this.is_expired = Result.False;
			this.is_cancelled = (isAutoRenewing ? Result.False : Result.True);
			this.is_free_trial = Result.False;
			string text = null;
			if (dictionary.ContainsKey("subscriptionPeriod"))
			{
				text = (string)dictionary["subscriptionPeriod"];
			}
			string text2 = null;
			if (dictionary.ContainsKey("freeTrialPeriod"))
			{
				text2 = (string)dictionary["freeTrialPeriod"];
			}
			string text3 = null;
			if (dictionary.ContainsKey("introductoryPrice"))
			{
				text3 = (string)dictionary["introductoryPrice"];
			}
			string text4 = null;
			if (dictionary.ContainsKey("introductoryPricePeriod"))
			{
				text4 = (string)dictionary["introductoryPricePeriod"];
			}
			long num = 0L;
			if (dictionary.ContainsKey("introductoryPriceCycles"))
			{
				num = (long)dictionary["introductoryPriceCycles"];
			}
			this.free_trial_period_string = text2;
			this.subscriptionPeriod = this.computePeriodTimeSpan(this.parsePeriodTimeSpanUnits(text));
			this.freeTrialPeriod = TimeSpan.Zero;
			if (isFreeTrial)
			{
				this.freeTrialPeriod = this.parseTimeSpan(text2);
			}
			this.introductory_price = text3;
			this.introductory_price_cycles = num;
			this.introductory_price_period = TimeSpan.Zero;
			this.is_introductory_price_period = Result.False;
			TimeSpan timeSpan = TimeSpan.Zero;
			if (hasIntroductoryPriceTrial)
			{
				this.introductory_price_period = ((text4 != null && text4.Equals(text)) ? this.subscriptionPeriod : this.parseTimeSpan(text4));
				timeSpan = this.accumulateIntroductoryDuration(this.parsePeriodTimeSpanUnits(text4), this.introductory_price_cycles);
			}
			TimeSpan timeSpan2 = TimeSpan.FromSeconds((updateMetadata == null) ? 0.0 : this.computeExtraTime(updateMetadata, this.subscriptionPeriod.TotalSeconds));
			TimeSpan timeSpan3 = DateTime.UtcNow.Subtract(purchaseDate);
			if (timeSpan3 <= timeSpan2)
			{
				this.subscriptionExpireDate = purchaseDate.Add(timeSpan2);
			}
			else if (timeSpan3 <= this.freeTrialPeriod.Add(timeSpan2))
			{
				this.is_free_trial = Result.True;
				this.subscriptionExpireDate = purchaseDate.Add(this.freeTrialPeriod.Add(timeSpan2));
			}
			else if (timeSpan3 < this.freeTrialPeriod.Add(timeSpan2).Add(timeSpan))
			{
				this.is_introductory_price_period = Result.True;
				DateTime dateTime = this.purchaseDate.Add(this.freeTrialPeriod.Add(timeSpan2));
				this.subscriptionExpireDate = this.nextBillingDate(dateTime, this.parsePeriodTimeSpanUnits(text4));
			}
			else
			{
				DateTime dateTime2 = this.purchaseDate.Add(this.freeTrialPeriod.Add(timeSpan2).Add(timeSpan));
				this.subscriptionExpireDate = this.nextBillingDate(dateTime2, this.parsePeriodTimeSpanUnits(text));
			}
			this.remainedTime = this.subscriptionExpireDate.Subtract(DateTime.UtcNow);
			this.sku_details = skuDetails;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000A588 File Offset: 0x00008788
		public SubscriptionInfo(string productId)
		{
			this.productId = productId;
			this.is_subscribed = Result.True;
			this.is_expired = Result.False;
			this.is_cancelled = Result.Unsupported;
			this.is_free_trial = Result.Unsupported;
			this.is_auto_renewing = Result.Unsupported;
			this.remainedTime = TimeSpan.MaxValue;
			this.is_introductory_price_period = Result.Unsupported;
			this.introductory_price_period = TimeSpan.MaxValue;
			this.introductory_price = null;
			this.introductory_price_cycles = 0L;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A5F1 File Offset: 0x000087F1
		public string getProductId()
		{
			return this.productId;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000A5F9 File Offset: 0x000087F9
		public DateTime getPurchaseDate()
		{
			return this.purchaseDate;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A601 File Offset: 0x00008801
		public Result isSubscribed()
		{
			return this.is_subscribed;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A609 File Offset: 0x00008809
		public Result isExpired()
		{
			return this.is_expired;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A611 File Offset: 0x00008811
		public Result isCancelled()
		{
			return this.is_cancelled;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A619 File Offset: 0x00008819
		public Result isFreeTrial()
		{
			return this.is_free_trial;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000A621 File Offset: 0x00008821
		public Result isAutoRenewing()
		{
			return this.is_auto_renewing;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000A629 File Offset: 0x00008829
		public TimeSpan getRemainingTime()
		{
			return this.remainedTime;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A631 File Offset: 0x00008831
		public Result isIntroductoryPricePeriod()
		{
			return this.is_introductory_price_period;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A639 File Offset: 0x00008839
		public TimeSpan getIntroductoryPricePeriod()
		{
			return this.introductory_price_period;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A641 File Offset: 0x00008841
		public string getIntroductoryPrice()
		{
			if (!string.IsNullOrEmpty(this.introductory_price))
			{
				return this.introductory_price;
			}
			return "not available";
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A65C File Offset: 0x0000885C
		public long getIntroductoryPricePeriodCycles()
		{
			return this.introductory_price_cycles;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A664 File Offset: 0x00008864
		public DateTime getExpireDate()
		{
			return this.subscriptionExpireDate;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A66C File Offset: 0x0000886C
		public DateTime getCancelDate()
		{
			return this.subscriptionCancelDate;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A674 File Offset: 0x00008874
		public TimeSpan getFreeTrialPeriod()
		{
			return this.freeTrialPeriod;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A67C File Offset: 0x0000887C
		public TimeSpan getSubscriptionPeriod()
		{
			return this.subscriptionPeriod;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000A684 File Offset: 0x00008884
		public string getFreeTrialPeriodString()
		{
			return this.free_trial_period_string;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A68C File Offset: 0x0000888C
		public string getSkuDetails()
		{
			return this.sku_details;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A694 File Offset: 0x00008894
		public string getSubscriptionInfoJsonString()
		{
			return MiniJson.JsonEncode(new Dictionary<string, object>
			{
				{ "productId", this.productId },
				{ "is_free_trial", this.is_free_trial },
				{
					"is_introductory_price_period",
					this.is_introductory_price_period == Result.True
				},
				{
					"remaining_time_in_seconds",
					this.remainedTime.TotalSeconds
				}
			});
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A70C File Offset: 0x0000890C
		private DateTime nextBillingDate(DateTime billing_begin_date, TimeSpanUnits units)
		{
			if (units.days == 0.0 && units.months == 0 && units.years == 0)
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			}
			DateTime dateTime = billing_begin_date;
			while (DateTime.Compare(dateTime, DateTime.UtcNow) <= 0)
			{
				dateTime = dateTime.AddDays(units.days).AddMonths(units.months).AddYears(units.years);
			}
			return dateTime;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A78C File Offset: 0x0000898C
		private TimeSpan accumulateIntroductoryDuration(TimeSpanUnits units, long cycles)
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			for (long num = 0L; num < cycles; num += 1L)
			{
				timeSpan = timeSpan.Add(this.computePeriodTimeSpan(units));
			}
			return timeSpan;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A7C0 File Offset: 0x000089C0
		private TimeSpan computePeriodTimeSpan(TimeSpanUnits units)
		{
			DateTime now = DateTime.Now;
			return now.AddDays(units.days).AddMonths(units.months).AddYears(units.years)
				.Subtract(now);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A808 File Offset: 0x00008A08
		private double computeExtraTime(string metadata, double new_sku_period_in_seconds)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(metadata);
			double num = (double)((long)dictionary["old_sku_remaining_seconds"]);
			long num2 = (long)dictionary["old_sku_price_in_micros"];
			double totalSeconds = this.parseTimeSpan((string)dictionary["old_sku_period_string"]).TotalSeconds;
			long num3 = (long)dictionary["new_sku_price_in_micros"];
			return num / totalSeconds * (double)num2 / (double)num3 * new_sku_period_in_seconds;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A880 File Offset: 0x00008A80
		private TimeSpan parseTimeSpan(string period_string)
		{
			TimeSpan timeSpan;
			try
			{
				timeSpan = XmlConvert.ToTimeSpan(period_string);
			}
			catch (Exception)
			{
				if (period_string == null || period_string.Length == 0)
				{
					timeSpan = TimeSpan.Zero;
				}
				else
				{
					timeSpan = new TimeSpan(7, 0, 0, 0);
				}
			}
			return timeSpan;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		private TimeSpanUnits parsePeriodTimeSpanUnits(string time_span)
		{
			if (time_span == "P1W")
			{
				return new TimeSpanUnits(7.0, 0, 0);
			}
			if (time_span == "P1M")
			{
				return new TimeSpanUnits(0.0, 1, 0);
			}
			if (time_span == "P3M")
			{
				return new TimeSpanUnits(0.0, 3, 0);
			}
			if (time_span == "P6M")
			{
				return new TimeSpanUnits(0.0, 6, 0);
			}
			if (!(time_span == "P1Y"))
			{
				return new TimeSpanUnits((double)this.parseTimeSpan(time_span).Days, 0, 0);
			}
			return new TimeSpanUnits(0.0, 0, 1);
		}

		// Token: 0x040001B0 RID: 432
		private readonly Result is_subscribed;

		// Token: 0x040001B1 RID: 433
		private readonly Result is_expired;

		// Token: 0x040001B2 RID: 434
		private readonly Result is_cancelled;

		// Token: 0x040001B3 RID: 435
		private readonly Result is_free_trial;

		// Token: 0x040001B4 RID: 436
		private readonly Result is_auto_renewing;

		// Token: 0x040001B5 RID: 437
		private readonly Result is_introductory_price_period;

		// Token: 0x040001B6 RID: 438
		private readonly string productId;

		// Token: 0x040001B7 RID: 439
		private readonly DateTime purchaseDate;

		// Token: 0x040001B8 RID: 440
		private readonly DateTime subscriptionExpireDate;

		// Token: 0x040001B9 RID: 441
		private readonly DateTime subscriptionCancelDate;

		// Token: 0x040001BA RID: 442
		private readonly TimeSpan remainedTime;

		// Token: 0x040001BB RID: 443
		private readonly string introductory_price;

		// Token: 0x040001BC RID: 444
		private readonly TimeSpan introductory_price_period;

		// Token: 0x040001BD RID: 445
		private readonly long introductory_price_cycles;

		// Token: 0x040001BE RID: 446
		private readonly TimeSpan freeTrialPeriod;

		// Token: 0x040001BF RID: 447
		private readonly TimeSpan subscriptionPeriod;

		// Token: 0x040001C0 RID: 448
		private readonly string free_trial_period_string;

		// Token: 0x040001C1 RID: 449
		private readonly string sku_details;
	}
}
