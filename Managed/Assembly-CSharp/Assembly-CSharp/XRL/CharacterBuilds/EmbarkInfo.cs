using System;
using System.Collections.Generic;
using System.Linq;
using XRL.World;

namespace XRL.CharacterBuilds
{
	// Token: 0x02000591 RID: 1425
	public class EmbarkInfo
	{
		// Token: 0x06003D31 RID: 15665 RVA: 0x0012BF24 File Offset: 0x0012A124
		public T getData<T>() where T : AbstractEmbarkBuilderModuleData
		{
			return this._data.Where<AbstractEmbarkBuilderModuleData>((AbstractEmbarkBuilderModuleData d) => d.GetType() == typeof(T)).FirstOrDefault<AbstractEmbarkBuilderModuleData>() as T;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x0012BF5F File Offset: 0x0012A15F
		public List<AbstractEmbarkBuilderModule> modules
		{
			get
			{
				return this._modules;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x0012BF67 File Offset: 0x0012A167
		public IEnumerable<AbstractEmbarkBuilderModule> enabledModules
		{
			get
			{
				return this._modules.Where<AbstractEmbarkBuilderModule>((AbstractEmbarkBuilderModule m) => m.enabled);
			}
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x0012BF94 File Offset: 0x0012A194
		public T getModule<T>() where T : AbstractEmbarkBuilderModule
		{
			for (int i = 0; i < this.modules.Count; i++)
			{
				if (this.modules[i] is T)
				{
					return this.modules[i] as T;
				}
			}
			return default(T);
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x0012BFEC File Offset: 0x0012A1EC
		public void bootGame(XRLGame game)
		{
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in this.enabledModules)
			{
				abstractEmbarkBuilderModule.bootGame(game, this);
			}
			EmbarkEvent.Send(game, this, "BootGame");
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0012C044 File Offset: 0x0012A244
		public void fireBootEvent(string id, XRLGame game)
		{
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in this.enabledModules)
			{
				abstractEmbarkBuilderModule.handleBootEvent(id, game, this, null);
			}
			EmbarkEvent.Send(game, this, id);
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x0012C09C File Offset: 0x0012A29C
		public T fireBootEvent<T>(string id, XRLGame game, T element)
		{
			object obj = element;
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in this.enabledModules)
			{
				obj = abstractEmbarkBuilderModule.handleBootEvent(id, game, this, obj);
			}
			EmbarkEvent.Send(game, this, id, ref obj);
			return (T)((object)obj);
		}

		// Token: 0x04001E72 RID: 7794
		public string GameSeed;

		// Token: 0x04001E73 RID: 7795
		public List<AbstractEmbarkBuilderModuleData> _data = new List<AbstractEmbarkBuilderModuleData>();

		// Token: 0x04001E74 RID: 7796
		public List<AbstractEmbarkBuilderModule> _modules = new List<AbstractEmbarkBuilderModule>();

		// Token: 0x04001E75 RID: 7797
		public List<IGameStateSingleton> GameStateSingletons = new List<IGameStateSingleton>();
	}
}
