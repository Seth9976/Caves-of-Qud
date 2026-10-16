using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200054C RID: 1356
	public abstract class RenderPipelineGlobalSettings : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x00065658 File Offset: 0x00063858
		protected virtual List<IRenderPipelineGraphicsSettings> settingsList
		{
			get
			{
				Debug.LogWarning(string.Format("To be able to use {0} in your {1} you must override {2}", "IRenderPipelineGraphicsSettings", base.GetType(), "settingsList"));
				Debug.LogWarning(string.Format("Create your own '[{0}] List<{1}> m_Settings = new();' in your {2} and override {3} returning m_Settings;", new object[]
				{
					"SerializeReference",
					"IRenderPipelineGraphicsSettings",
					base.GetType(),
					"settingsList"
				}));
				return null;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000656C2 File Offset: 0x000638C2
		private Dictionary<Type, int> settingsMap { get; } = new Dictionary<Type, int>();

		// Token: 0x060033E0 RID: 13280 RVA: 0x000656CC File Offset: 0x000638CC
		private void RecreateSettingsMap()
		{
			this.settingsMap.Clear();
			bool flag = this.settingsList == null;
			if (!flag)
			{
				for (int i = 0; i < this.settingsList.Count; i++)
				{
					IRenderPipelineGraphicsSettings renderPipelineGraphicsSettings = this.settingsList[i];
					bool flag2 = renderPipelineGraphicsSettings == null;
					if (!flag2)
					{
						this.settingsMap.Add(renderPipelineGraphicsSettings.GetType(), i);
					}
				}
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x00065740 File Offset: 0x00063940
		protected internal bool TryGet(Type type, out IRenderPipelineGraphicsSettings settings)
		{
			settings = null;
			bool flag = this.settingsList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int num;
				bool flag3 = !this.settingsMap.TryGetValue(type, out num);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					settings = this.settingsList[num];
					flag2 = settings != null;
				}
			}
			return flag2;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x00065794 File Offset: 0x00063994
		protected internal bool TryGetFirstSettingsImplementingInterface<TSettingsInterfaceType>(out TSettingsInterfaceType settings) where TSettingsInterfaceType : class, IRenderPipelineGraphicsSettings
		{
			settings = default(TSettingsInterfaceType);
			bool flag = this.settingsList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.settingsList.Count; i++)
				{
					TSettingsInterfaceType tsettingsInterfaceType = this.settingsList[i] as TSettingsInterfaceType;
					bool flag3 = tsettingsInterfaceType != null;
					if (flag3)
					{
						settings = tsettingsInterfaceType;
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x00065810 File Offset: 0x00063A10
		protected internal bool GetSettingsImplementingInterface<TSettingsInterfaceType>(out List<TSettingsInterfaceType> settings) where TSettingsInterfaceType : class, IRenderPipelineGraphicsSettings
		{
			settings = new List<TSettingsInterfaceType>();
			bool flag = this.settingsList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.settingsList.Count; i++)
				{
					TSettingsInterfaceType tsettingsInterfaceType = this.settingsList[i] as TSettingsInterfaceType;
					bool flag3 = tsettingsInterfaceType != null;
					if (flag3)
					{
						settings.Add(tsettingsInterfaceType);
					}
				}
				flag2 = settings.Count > 0;
			}
			return flag2;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x00065894 File Offset: 0x00063A94
		protected internal bool Contains(Type type)
		{
			return this.settingsList != null && this.settingsMap.ContainsKey(type);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000658BD File Offset: 0x00063ABD
		public virtual void OnAfterDeserialize()
		{
			this.RecreateSettingsMap();
		}
	}
}
