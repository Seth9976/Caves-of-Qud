using System;
using Uniject;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000066 RID: 102
	internal class NativeStoreProvider : INativeStoreProvider
	{
		// Token: 0x06000258 RID: 600 RVA: 0x000079B0 File Offset: 0x00005BB0
		public INativeStore GetAndroidStore(IUnityCallback callback, AppStore store, IPurchasingBinder binder, IUtil util)
		{
			INativeStore androidStoreHelper;
			try
			{
				androidStoreHelper = this.GetAndroidStoreHelper(callback, store, binder, util);
			}
			catch (Exception ex)
			{
				throw new NotSupportedException("Failed to bind to native store: " + ex.ToString());
			}
			if (androidStoreHelper != null)
			{
				return androidStoreHelper;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000079FC File Offset: 0x00005BFC
		private INativeStore GetAndroidStoreHelper(IUnityCallback callback, AppStore store, IPurchasingBinder binder, IUtil util)
		{
			if (store != AppStore.AmazonAppStore)
			{
				if (store != AppStore.UDP)
				{
					throw new NotImplementedException();
				}
			}
			else
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity.purchasing.amazon.AmazonPurchasing"))
				{
					JavaBridge javaBridge = new JavaBridge(new ScriptingUnityCallback(callback, util));
					AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[] { javaBridge });
					AmazonAppStoreStoreExtensions amazonAppStoreStoreExtensions = new AmazonAppStoreStoreExtensions(androidJavaObject);
					binder.RegisterExtension<IAmazonExtensions>(amazonAppStoreStoreExtensions);
					binder.RegisterConfiguration<IAmazonConfiguration>(amazonAppStoreStoreExtensions);
					return new AndroidJavaStore(androidJavaObject);
				}
			}
			if (UdpIapBridgeInterface.GetClassType() != null)
			{
				UDPImpl udpimpl = new UDPImpl();
				UDPBindings udpbindings = new UDPBindings();
				udpimpl.SetNativeStore(udpbindings);
				binder.RegisterExtension<IUDPExtensions>(udpimpl);
				return udpbindings;
			}
			Debug.LogError("Cannot set Android target to UDP. Make sure you have installed UDP in your project");
			throw new NotImplementedException();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public INativeAppleStore GetStorekit(IUnityCallback callback)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS)
			{
				return new iOSStoreBindings();
			}
			return new OSXStoreBindings();
		}
	}
}
