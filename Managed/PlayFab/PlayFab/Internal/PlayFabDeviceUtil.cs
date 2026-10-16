using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	// Token: 0x020001A2 RID: 418
	public static class PlayFabDeviceUtil
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x000137FC File Offset: 0x000119FC
		private static void DoAttributeInstall(PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			if (!PlayFabDeviceUtil._needsAttribution || settings.DisableAdvertising)
			{
				return;
			}
			AttributeInstallRequest attributeInstallRequest = new AttributeInstallRequest();
			string advertisingIdType = settings.AdvertisingIdType;
			if (!(advertisingIdType == "Adid"))
			{
				if (advertisingIdType == "Idfa")
				{
					attributeInstallRequest.Idfa = settings.AdvertisingIdValue;
				}
			}
			else
			{
				attributeInstallRequest.Adid = settings.AdvertisingIdValue;
			}
			PlayFabClientInstanceAPI playFabClientInstanceAPI = instanceApi as PlayFabClientInstanceAPI;
			if (playFabClientInstanceAPI != null)
			{
				playFabClientInstanceAPI.AttributeInstall(attributeInstallRequest, new Action<AttributeInstallResult>(PlayFabDeviceUtil.OnAttributeInstall), null, settings, null);
				return;
			}
			PlayFabClientAPI.AttributeInstall(attributeInstallRequest, new Action<AttributeInstallResult>(PlayFabDeviceUtil.OnAttributeInstall), null, settings, null);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00013892 File Offset: 0x00011A92
		private static void OnAttributeInstall(AttributeInstallResult result)
		{
			PlayFabApiSettings playFabApiSettings = (PlayFabApiSettings)result.CustomData;
			playFabApiSettings.AdvertisingIdType += "_Successful";
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000138B4 File Offset: 0x00011AB4
		private static void SendDeviceInfoToPlayFab(PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			if (settings.DisableDeviceInfo || !PlayFabDeviceUtil._gatherDeviceInfo)
			{
				return;
			}
			ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "");
			DeviceInfoRequest deviceInfoRequest = new DeviceInfoRequest
			{
				Info = plugin.DeserializeObject<Dictionary<string, object>>(plugin.SerializeObject(new PlayFabDataGatherer()))
			};
			PlayFabClientInstanceAPI playFabClientInstanceAPI = instanceApi as PlayFabClientInstanceAPI;
			if (playFabClientInstanceAPI != null)
			{
				playFabClientInstanceAPI.ReportDeviceInfo(deviceInfoRequest, null, new Action<PlayFabError>(PlayFabDeviceUtil.OnGatherFail), settings, null);
				return;
			}
			PlayFabClientAPI.ReportDeviceInfo(deviceInfoRequest, null, new Action<PlayFabError>(PlayFabDeviceUtil.OnGatherFail), settings, null);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013930 File Offset: 0x00011B30
		private static void OnGatherFail(PlayFabError error)
		{
			Debug.Log("OnGatherFail: " + error.GenerateErrorReport());
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013948 File Offset: 0x00011B48
		public static void OnPlayFabLogin(PlayFabResultCommon result, PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			LoginResult loginResult = result as LoginResult;
			RegisterPlayFabUserResult registerPlayFabUserResult = result as RegisterPlayFabUserResult;
			if (loginResult == null && registerPlayFabUserResult == null)
			{
				return;
			}
			UserSettings userSettings = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			if (loginResult != null)
			{
				userSettings = loginResult.SettingsForUser;
				text = loginResult.PlayFabId;
				if (loginResult.EntityToken != null)
				{
					text2 = loginResult.EntityToken.Entity.Id;
					text3 = loginResult.EntityToken.Entity.Type;
				}
			}
			else if (registerPlayFabUserResult != null)
			{
				userSettings = registerPlayFabUserResult.SettingsForUser;
				text = registerPlayFabUserResult.PlayFabId;
				if (registerPlayFabUserResult.EntityToken != null)
				{
					text2 = registerPlayFabUserResult.EntityToken.Entity.Id;
					text3 = registerPlayFabUserResult.EntityToken.Entity.Type;
				}
			}
			PlayFabDeviceUtil._OnPlayFabLogin(userSettings, text, text2, text3, settings, instanceApi);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013A00 File Offset: 0x00011C00
		private static void _OnPlayFabLogin(UserSettings settingsForUser, string playFabId, string entityId, string entityType, PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			PlayFabDeviceUtil._needsAttribution = (PlayFabDeviceUtil._gatherDeviceInfo = (PlayFabDeviceUtil._gatherScreenTime = false));
			if (settingsForUser != null)
			{
				PlayFabDeviceUtil._needsAttribution = settingsForUser.NeedsAttribution;
				PlayFabDeviceUtil._gatherDeviceInfo = settingsForUser.GatherDeviceInfo;
				PlayFabDeviceUtil._gatherScreenTime = settingsForUser.GatherFocusInfo;
			}
			if (settings.AdvertisingIdType != null && settings.AdvertisingIdValue != null)
			{
				PlayFabDeviceUtil.DoAttributeInstall(settings, instanceApi);
			}
			else
			{
				PlayFabDeviceUtil.GetAdvertIdFromUnity(settings, instanceApi);
			}
			PlayFabDeviceUtil.SendDeviceInfoToPlayFab(settings, instanceApi);
			if (!string.IsNullOrEmpty(entityId) && !string.IsNullOrEmpty(entityType) && PlayFabDeviceUtil._gatherScreenTime)
			{
				PlayFabHttp.InitializeScreenTimeTracker(entityId, entityType, playFabId);
				return;
			}
			settings.DisableFocusTimeCollection = true;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013A9A File Offset: 0x00011C9A
		private static void GetAdvertIdFromUnity(PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
		}

		// Token: 0x0400090F RID: 2319
		private static bool _needsAttribution;

		// Token: 0x04000910 RID: 2320
		private static bool _gatherDeviceInfo;

		// Token: 0x04000911 RID: 2321
		private static bool _gatherScreenTime;
	}
}
