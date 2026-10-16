using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200024C RID: 588
	[StaticAccessor("GetApiRestrictions()", StaticAccessorType.Arrow)]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Scripting/ApiRestrictions.h")]
	[ExtensionOfNativeClass]
	internal class ApiRestrictions
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x0003BA8C File Offset: 0x00039C8C
		internal static void PushDisableApiInternal(ApiRestrictions.ContextRestrictions contextApi, Object context, ApiRestrictions.GlobalRestrictions globalApi)
		{
			ApiRestrictions.PushDisableApiInternal_Injected(contextApi, Object.MarshalledUnityObject.Marshal<Object>(context), globalApi);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0003BAA8 File Offset: 0x00039CA8
		internal static void PopDisableApiInternal(ApiRestrictions.ContextRestrictions contextApi, Object context, ApiRestrictions.GlobalRestrictions globalApi)
		{
			ApiRestrictions.PopDisableApiInternal_Injected(contextApi, Object.MarshalledUnityObject.Marshal<Object>(context), globalApi);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0003BAC4 File Offset: 0x00039CC4
		internal static bool TryApiInternal(ApiRestrictions.ContextRestrictions contextApi, Object context, ApiRestrictions.GlobalRestrictions globalApi, bool allowErrorLogging)
		{
			return ApiRestrictions.TryApiInternal_Injected(contextApi, Object.MarshalledUnityObject.Marshal<Object>(context), globalApi, allowErrorLogging);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0003BADF File Offset: 0x00039CDF
		internal static void PushDisableApi(ApiRestrictions.ContextRestrictions api, Object owner)
		{
			ApiRestrictions.PushDisableApiInternal(api, owner, ApiRestrictions.GlobalRestrictions.GLOBALCOUNT);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0003BAEB File Offset: 0x00039CEB
		internal static void PushDisableApi(ApiRestrictions.GlobalRestrictions api)
		{
			ApiRestrictions.PushDisableApiInternal(ApiRestrictions.ContextRestrictions.CONTEXTCOUNT, null, api);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0003BAF7 File Offset: 0x00039CF7
		internal static void PopDisableApi(ApiRestrictions.ContextRestrictions api, Object context)
		{
			ApiRestrictions.PopDisableApiInternal(api, context, ApiRestrictions.GlobalRestrictions.GLOBALCOUNT);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0003BB03 File Offset: 0x00039D03
		internal static void PopDisableApi(ApiRestrictions.GlobalRestrictions api)
		{
			ApiRestrictions.PopDisableApiInternal(ApiRestrictions.ContextRestrictions.CONTEXTCOUNT, null, api);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0003BB10 File Offset: 0x00039D10
		internal static bool TryApi(ApiRestrictions.ContextRestrictions api, Object context, bool allowErrorLogging = true)
		{
			return ApiRestrictions.TryApiInternal(api, context, ApiRestrictions.GlobalRestrictions.GLOBALCOUNT, allowErrorLogging);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0003BB2C File Offset: 0x00039D2C
		internal static bool TryApi(ApiRestrictions.GlobalRestrictions api, bool allowErrorLogging = true)
		{
			return ApiRestrictions.TryApiInternal(ApiRestrictions.ContextRestrictions.CONTEXTCOUNT, null, api, allowErrorLogging);
		}

		// Token: 0x06001F0F RID: 7951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PushDisableApiInternal_Injected(ApiRestrictions.ContextRestrictions contextApi, IntPtr context, ApiRestrictions.GlobalRestrictions globalApi);

		// Token: 0x06001F10 RID: 7952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PopDisableApiInternal_Injected(ApiRestrictions.ContextRestrictions contextApi, IntPtr context, ApiRestrictions.GlobalRestrictions globalApi);

		// Token: 0x06001F11 RID: 7953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryApiInternal_Injected(ApiRestrictions.ContextRestrictions contextApi, IntPtr context, ApiRestrictions.GlobalRestrictions globalApi, bool allowErrorLogging);

		// Token: 0x0200024D RID: 589
		internal enum GlobalRestrictions
		{
			// Token: 0x04000922 RID: 2338
			OBJECT_DESTROYIMMEDIATE,
			// Token: 0x04000923 RID: 2339
			OBJECT_SENDMESSAGE,
			// Token: 0x04000924 RID: 2340
			OBJECT_RENDERING,
			// Token: 0x04000925 RID: 2341
			GLOBALCOUNT
		}

		// Token: 0x0200024E RID: 590
		internal enum ContextRestrictions
		{
			// Token: 0x04000927 RID: 2343
			RENDERERSCENE_ADDREMOVE,
			// Token: 0x04000928 RID: 2344
			OBJECT_ADDCOMPONENTTRANSFORM,
			// Token: 0x04000929 RID: 2345
			CONTEXTCOUNT
		}
	}
}
