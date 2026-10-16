using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;
using XRL.World.Anatomy;

namespace XRL.World.Units
{
	// Token: 0x020009AA RID: 2474
	[Serializable]
	public class GameObjectCyberneticsUnit : GameObjectUnit
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x00226410 File Offset: 0x00224610
		public GameObjectBlueprint Entry
		{
			get
			{
				GameObjectBlueprint gameObjectBlueprint;
				if ((gameObjectBlueprint = this._Entry) == null)
				{
					gameObjectBlueprint = (this._Entry = (GameObjectFactory.Factory.Blueprints.TryGetValue(this.Blueprint, out this._Entry) ? this._Entry : null));
				}
				return gameObjectBlueprint;
			}
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x00226456 File Offset: 0x00224656
		public override void Apply(GameObject Object)
		{
			this.Implant(Object);
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x00226460 File Offset: 0x00224660
		public GameObject Implant(GameObject Object)
		{
			BodyPart bodyPart = null;
			if (!this.Slot.IsNullOrEmpty())
			{
				bodyPart = Object.Body.GetPartByName(this.Slot);
			}
			if (bodyPart == null)
			{
				GameObjectBlueprint entry = this.Entry;
				string[] array = ((entry != null) ? entry.GetPartParameter<string>("CyberneticsBaseItem", "Slots", null).Split(',', StringSplitOptions.None) : null);
				if (!array.IsNullOrEmpty<string>())
				{
					GameObjectCyberneticsUnit.<>c__DisplayClass8_0 CS$<>8__locals1 = new GameObjectCyberneticsUnit.<>c__DisplayClass8_0();
					List<BodyPart> list = new List<BodyPart>();
					GameObjectCyberneticsUnit.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
					string tag = this.Entry.GetTag("CyberneticsPreferredSlots", "");
					CS$<>8__locals2.preferred = ((tag != null) ? tag.Split(',', StringSplitOptions.None) : null);
					BodyPart body = Object.Body.GetBody();
					Algorithms.RandomShuffleInPlace<string>(array);
					if (!CS$<>8__locals1.preferred.IsNullOrEmpty<string>())
					{
						Algorithms.StableSortInPlace<string>(array, (string a, string b) => Array.IndexOf<string>(CS$<>8__locals1.preferred, b).CompareTo(Array.IndexOf<string>(CS$<>8__locals1.preferred, a)));
					}
					foreach (string text in array)
					{
						body.GetPart(text, list);
						list.RemoveAll((BodyPart x) => x.Cybernetics != null);
						bodyPart = list.GetRandomElement<BodyPart>(null);
						if (bodyPart != null)
						{
							break;
						}
					}
				}
			}
			if (bodyPart == null)
			{
				MetricsManager.LogError(string.Format("Unable to implant {0} with {1}.", Object, this.Blueprint));
				return null;
			}
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(this.Entry, 0, 0, null, null, null, null, null);
			if (!this.Removable)
			{
				gameObject.SetIntProperty("CyberneticsNoRemove", 1, false);
			}
			if (Object.IsPlayer())
			{
				gameObject.MakeUnderstood(false);
			}
			bodyPart.Implant(gameObject, false, false);
			GameObjectBlueprint entry2 = this.Entry;
			if (entry2 != null)
			{
				entry2.GetPartParameter<int>("CyberneticsBaseItem", "Cost", 0);
			}
			return gameObject;
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x00226610 File Offset: 0x00224810
		public override void Remove(GameObject Object)
		{
			foreach (BodyPart bodyPart in Object.Body.LoopParts())
			{
				GameObject cybernetics = bodyPart.Cybernetics;
				if (((cybernetics != null) ? cybernetics.Blueprint : null) == this.Blueprint)
				{
					bodyPart.Unimplant(false);
				}
			}
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x00226684 File Offset: 0x00224884
		public override void Reset()
		{
			base.Reset();
			this.Blueprint = null;
			this.Slot = null;
			this._Entry = null;
			this.Removable = true;
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x002266A8 File Offset: 0x002248A8
		public override bool CanInscribe()
		{
			return true;
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x002266AC File Offset: 0x002248AC
		public override string GetDescription(bool Inscription = false)
		{
			if (!this.LicenseStat.IsNullOrEmpty())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(Inscription ? "" : "Cybernetic implant installed");
				GameObjectBlueprint entry = this.Entry;
				if (entry != null)
				{
					entry.GetPartParameter<int>("CyberneticsBaseItem", "Cost", 0);
				}
				return Event.FinalizeString(stringBuilder);
			}
			if (!Inscription)
			{
				return "Cybernetic implant installed";
			}
			return "";
		}

		// Token: 0x040030E4 RID: 12516
		public string Blueprint;

		// Token: 0x040030E5 RID: 12517
		public string Slot;

		// Token: 0x040030E6 RID: 12518
		public string LicenseStat;

		// Token: 0x040030E7 RID: 12519
		public bool Removable = true;

		// Token: 0x040030E8 RID: 12520
		[NonSerialized]
		private GameObjectBlueprint _Entry;
	}
}
