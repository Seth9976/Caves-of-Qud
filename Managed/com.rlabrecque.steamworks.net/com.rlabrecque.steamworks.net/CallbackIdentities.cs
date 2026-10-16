using System;

namespace Steamworks
{
	// Token: 0x02000008 RID: 8
	internal class CallbackIdentities
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002BCC File Offset: 0x00000DCC
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), false);
			int num = 0;
			if (num >= customAttributes.Length)
			{
				throw new Exception("Callback number not found for struct " + ((callbackStruct != null) ? callbackStruct.ToString() : null));
			}
			return ((CallbackIdentityAttribute)customAttributes[num]).Identity;
		}
	}
}
