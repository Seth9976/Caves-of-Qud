using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F91 RID: 3985
	[Serializable]
	public class TongueOnEat : IPart
	{
		// Token: 0x06009E18 RID: 40472 RVA: 0x003939E4 File Offset: 0x00391BE4
		public override bool SameAs(IPart p)
		{
			TongueOnEat tongueOnEat = p as TongueOnEat;
			return !(tongueOnEat.Tongues != this.Tongues) && !(tongueOnEat.Range != this.Range) && !(tongueOnEat.Color != this.Color) && !(tongueOnEat.Message != this.Message) && base.SameAs(p);
		}

		// Token: 0x06009E19 RID: 40473 RVA: 0x00393A53 File Offset: 0x00391C53
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E1A RID: 40474 RVA: 0x00393A68 File Offset: 0x00391C68
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				this.Message = "A trio of tongues vegetate from =subject.t's= =bodypart:Face=!";
				gameObjectParameter.EmitMessage(this.Message, null, null, false);
				StickyTongue.HarpoonNearest(gameObjectParameter, this.Range.RollCached(), this.Color, this.Tongues.RollCached(), true, false);
				if (gameObjectParameter.IsPlayer())
				{
					E.RequestInterfaceExit();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004161 RID: 16737
		public string Tongues = "1";

		// Token: 0x04004162 RID: 16738
		public string Range = "10";

		// Token: 0x04004163 RID: 16739
		public string Color = "&M";

		// Token: 0x04004164 RID: 16740
		public string Message;
	}
}
