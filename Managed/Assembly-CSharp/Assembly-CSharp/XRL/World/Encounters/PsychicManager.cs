using System;
using System.Collections.Generic;

namespace XRL.World.Encounters
{
	// Token: 0x02000BA4 RID: 2980
	[Obsolete]
	[Serializable]
	public class PsychicManager : IGamestatePostload
	{
		// Token: 0x060076BC RID: 30396 RVA: 0x002CB2B8 File Offset: 0x002C94B8
		public void OnGamestatePostload(XRLGame game, SerializationReader reader)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				if (i < this.PsychicFactions.Count)
				{
					this.PsychicFactions[i].dimensionalTraining = text;
				}
			}
			int num2 = reader.ReadInt32();
			for (int j = 0; j < num2; j++)
			{
				string text2 = reader.ReadString();
				if (j < this.ExtraDimensions.Count)
				{
					this.ExtraDimensions[j].Training = text2;
				}
			}
			DimensionManager dimensionManager = new DimensionManager();
			dimensionManager.PsychicFactions = this.PsychicFactions;
			dimensionManager.ExtraDimensions = this.ExtraDimensions;
			game.ObjectGameState["DimensionManager"] = dimensionManager;
			game.ObjectGameState.Remove("PsychicManager");
		}

		// Token: 0x040036A4 RID: 13988
		public List<PsychicFaction> PsychicFactions = new List<PsychicFaction>();

		// Token: 0x040036A5 RID: 13989
		public List<ExtraDimension> ExtraDimensions = new List<ExtraDimension>();
	}
}
