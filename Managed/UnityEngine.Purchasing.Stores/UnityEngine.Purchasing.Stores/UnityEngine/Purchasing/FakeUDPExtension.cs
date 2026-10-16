using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000045 RID: 69
	[Obsolete("UDP support will be removed in the next major update of In-App Purchasing. Right now, the UDP SDK will still function normally in tandem with IAP.")]
	public class FakeUDPExtension : IUDPExtensions, IStoreExtension
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000057F4 File Offset: 0x000039F4
		public object GetUserInfo()
		{
			Type classType = UserInfoInterface.GetClassType();
			if (classType == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(classType);
			UserInfoInterface.GetChannelProp().SetValue(obj, "Fake_Channel", null);
			UserInfoInterface.GetIdProp().SetValue(obj, "Fake_User_Id_123456", null);
			UserInfoInterface.GetIdProp().SetValue(obj, "Fake_Login_Token", null);
			return obj;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000584D File Offset: 0x00003A4D
		public string GetLastInitializationError()
		{
			return "Fake Initialization Error";
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005854 File Offset: 0x00003A54
		public string GetLastPurchaseError()
		{
			return "Fake Purchase Error";
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000585B File Offset: 0x00003A5B
		public void EnableDebugLog(bool enable)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000585D File Offset: 0x00003A5D
		public void RegisterPurchaseDeferredListener(Action<Product> action)
		{
		}
	}
}
