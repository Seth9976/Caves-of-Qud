using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F41 RID: 3905
	[Serializable]
	public class CyberneticsPropertyModifier : IPart
	{
		// Token: 0x06009BC9 RID: 39881 RVA: 0x003881DE File Offset: 0x003863DE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BCA RID: 39882 RVA: 0x003881E1 File Offset: 0x003863E1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == PooledEvent<GetJumpingBehaviorEvent>.ID || ID == PooledEvent<GetPropertyModDescription>.ID;
		}

		// Token: 0x06009BCB RID: 39883 RVA: 0x00388210 File Offset: 0x00386410
		public override bool HandleEvent(GetPropertyModDescription E)
		{
			int num;
			if (E.Actor == this.ParentObject.Implantee && this.ParseProps(this.Props).TryGetValue(E.Property, out num))
			{
				E.AddLinearBonusModifier(num, this.ParentObject.BaseDisplayName);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BCC RID: 39884 RVA: 0x00388264 File Offset: 0x00386464
		public override bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			int num;
			if (E.Actor == this.ParentObject.Implantee && E.Stats != null && this.ParseProps(this.Props).TryGetValue("JumpRangeModifier", out num))
			{
				E.Stats.AddLinearBonusModifier("Range", num, this.ParentObject.BaseDisplayName);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BCD RID: 39885 RVA: 0x003882CC File Offset: 0x003864CC
		public override bool HandleEvent(ImplantedEvent E)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.ParseProps(this.Props))
			{
				E.Implantee.ModIntProperty(keyValuePair.Key, keyValuePair.Value, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BCE RID: 39886 RVA: 0x00388340 File Offset: 0x00386540
		public override bool HandleEvent(UnimplantedEvent E)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.ParseProps(this.Props))
			{
				E.Implantee.ModIntProperty(keyValuePair.Key, -keyValuePair.Value, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BCF RID: 39887 RVA: 0x003883B8 File Offset: 0x003865B8
		public Dictionary<string, int> ParseProps(string Props)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			string[] array = Props.Split(';', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':', StringSplitOptions.None);
				dictionary.Add(array2[0], Convert.ToInt32(array2[1]));
			}
			return dictionary;
		}

		// Token: 0x0400408B RID: 16523
		public string Props;
	}
}
