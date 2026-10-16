using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004E4 RID: 1252
	[Serializable]
	public abstract class IPlayerSystem : IGameSystem
	{
		// Token: 0x06003610 RID: 13840 RVA: 0x001046C4 File Offset: 0x001028C4
		public override void ApplyRegistrar(XRLGame Game)
		{
			EventRegistrar eventRegistrar = EventRegistrar.Get(Game, this);
			eventRegistrar.Register(PooledEvent<AfterPlayerBodyChangeEvent>.ID, 0, false);
			this.Register(Game, eventRegistrar);
			GameObject body = Game.Player._Body;
			if (body != null)
			{
				eventRegistrar.Source = body;
				this.RegisterPlayer(body, eventRegistrar);
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x0010470C File Offset: 0x0010290C
		public override void ApplyUnregistrar(XRLGame Game)
		{
			EventUnregistrar eventUnregistrar = EventUnregistrar.Get(Game, this);
			eventUnregistrar.Register(PooledEvent<AfterPlayerBodyChangeEvent>.ID, 0, false);
			this.Register(Game, eventUnregistrar);
			GameObject body = Game.Player._Body;
			if (body != null)
			{
				eventUnregistrar.Source = body;
				this.RegisterPlayer(body, eventUnregistrar);
			}
		}

		/// <summary>Register to events from the player <see cref="T:XRL.World.GameObject" />.</summary>
		/// <param name="Player">The non-null player <see cref="T:XRL.World.GameObject" />.</param>
		/// <param name="Registrar">An event registrar with this <see cref="T:XRL.IGameSystem" /> and the player <see cref="T:XRL.World.GameObject" /> provisioned as defaults.</param>
		// Token: 0x06003612 RID: 13842 RVA: 0x00104754 File Offset: 0x00102954
		public virtual void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x00104758 File Offset: 0x00102958
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			if (E.OldBody != null)
			{
				this.RegisterPlayer(E.OldBody, EventUnregistrar.Get(E.OldBody, this));
			}
			if (E.NewBody != null)
			{
				this.RegisterPlayer(E.NewBody, EventRegistrar.Get(E.NewBody, this));
			}
			return base.HandleEvent(E);
		}
	}
}
