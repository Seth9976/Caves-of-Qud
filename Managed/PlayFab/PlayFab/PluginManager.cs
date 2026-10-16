using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.Json;

namespace PlayFab
{
	// Token: 0x0200002C RID: 44
	public class PluginManager
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00010367 File Offset: 0x0000E567
		private PluginManager()
		{
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001037F File Offset: 0x0000E57F
		public static T GetPlugin<T>(PluginContract contract, string instanceName = "") where T : IPlayFabPlugin
		{
			return (T)((object)PluginManager.Instance.GetPluginInternal(contract, instanceName));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010392 File Offset: 0x0000E592
		public static void SetPlugin(IPlayFabPlugin plugin, PluginContract contract, string instanceName = "")
		{
			PluginManager.Instance.SetPluginInternal(plugin, contract, instanceName);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000103A4 File Offset: 0x0000E5A4
		private IPlayFabPlugin GetPluginInternal(PluginContract contract, string instanceName)
		{
			PluginContractKey pluginContractKey = new PluginContractKey
			{
				_pluginContract = contract,
				_pluginName = instanceName
			};
			IPlayFabPlugin playFabPlugin;
			if (!this.plugins.TryGetValue(pluginContractKey, out playFabPlugin))
			{
				if (contract != PluginContract.PlayFab_Serializer)
				{
					if (contract != PluginContract.PlayFab_Transport)
					{
						throw new ArgumentException("This contract is not supported", "contract");
					}
					playFabPlugin = this.CreatePlayFabTransportPlugin();
				}
				else
				{
					playFabPlugin = this.CreatePlugin<SimpleJsonInstance>();
				}
				this.plugins[pluginContractKey] = playFabPlugin;
			}
			return playFabPlugin;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010414 File Offset: 0x0000E614
		private void SetPluginInternal(IPlayFabPlugin plugin, PluginContract contract, string instanceName)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin", "Plugin instance cannot be null");
			}
			PluginContractKey pluginContractKey = new PluginContractKey
			{
				_pluginContract = contract,
				_pluginName = instanceName
			};
			this.plugins[pluginContractKey] = plugin;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001045B File Offset: 0x0000E65B
		private IPlayFabPlugin CreatePlugin<T>() where T : IPlayFabPlugin, new()
		{
			return (IPlayFabPlugin)Activator.CreateInstance(typeof(T));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010474 File Offset: 0x0000E674
		private ITransportPlugin CreatePlayFabTransportPlugin()
		{
			ITransportPlugin transportPlugin = null;
			if (PlayFabSettings.RequestType == WebRequestType.HttpWebRequest)
			{
				transportPlugin = new PlayFabWebRequest();
			}
			if (transportPlugin == null)
			{
				transportPlugin = new PlayFabUnityHttp();
			}
			return transportPlugin;
		}

		// Token: 0x040002C9 RID: 713
		private Dictionary<PluginContractKey, IPlayFabPlugin> plugins = new Dictionary<PluginContractKey, IPlayFabPlugin>(new PluginContractKeyComparator());

		// Token: 0x040002CA RID: 714
		private static readonly PluginManager Instance = new PluginManager();
	}
}
