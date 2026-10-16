using System;
using System.Reflection;
using XRL.Collections;
using XRL.Messages;
using XRL.UI;
using XRL.World.Conversations;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200096B RID: 2411
	[Serializable]
	public class GamePlayer
	{
		// Token: 0x06006399 RID: 25497 RVA: 0x001E8B90 File Offset: 0x001E6D90
		internal void EnqueuePlayerCell()
		{
			GameObject body = this._Body;
			Cell cell;
			if (body == null)
			{
				cell = null;
			}
			else
			{
				Physics physics = body.Physics;
				cell = ((physics != null) ? physics._CurrentCell : null);
			}
			Cell cell2 = cell;
			if (cell2 == null || (this.PlayerCells.Count != 0 && this.PlayerCells.Last == cell2))
			{
				return;
			}
			if (this.PlayerCells.Count >= 8)
			{
				this.PlayerCells.Dequeue();
			}
			this.PlayerCells.Enqueue(cell2);
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x001E8C04 File Offset: 0x001E6E04
		internal void CheckPlayerLocations()
		{
			int i = 0;
			int count = this.PlayerCells.Count;
			while (i < count)
			{
				Cell cell = this.PlayerCells[i];
				int j = 0;
				int count2 = cell.Objects.Count;
				while (j < count2)
				{
					GameObject gameObject = cell.Objects[j];
					if (gameObject.Physics == null || gameObject.Physics._CurrentCell != cell)
					{
						cell.LogInvalidPhysics(gameObject);
					}
					j++;
				}
				i++;
			}
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x001E8C7E File Offset: 0x001E6E7E
		public void Save(SerializationWriter Writer)
		{
			Writer.WriteFields<GamePlayer>(this, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			Choice.Hashes.Write(Writer);
		}

		// Token: 0x0600639C RID: 25500 RVA: 0x001E8C99 File Offset: 0x001E6E99
		public static GamePlayer Load(SerializationReader Reader)
		{
			GamePlayer gamePlayer = Reader.ReadInstanceFields<GamePlayer>(BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			gamePlayer._Body = Reader.GetPlayer();
			Choice.Hashes.Read(Reader);
			return gamePlayer;
		}

		/// <summary>Set the player's controlled game object.</summary>
		/// <param name="Body">A new <see cref="T:XRL.World.GameObject" /> for the player to control.</param>
		/// <param name="Transient">Whether this is a temporary assignment that should not be visible to the player.</param>
		// Token: 0x0600639D RID: 25501 RVA: 0x001E8CC0 File Offset: 0x001E6EC0
		public void SetBody(GameObject Body, bool Transient = false)
		{
			if (Body == this._Body)
			{
				return;
			}
			GameObject body = this._Body;
			if (Body != null && !Transient)
			{
				Cell currentCell = Body.CurrentCell;
				if (currentCell != null)
				{
					this._Body = Body;
					The.ZoneManager.SetActiveZone(currentCell.ParentZone);
					The.ActionManager.AddActiveObject(this._Body);
					The.ZoneManager.ProcessGoToPartyLeader();
				}
				Body.Render.RenderLayer = 100;
				Body.Brain.Goals.Clear();
				AbilityManager.UpdateFavorites();
			}
			else
			{
				this._Body = Body;
			}
			AfterPlayerBodyChangeEvent.Send(Body, body);
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x001E8D52 File Offset: 0x001E6F52
		// (set) Token: 0x0600639F RID: 25503 RVA: 0x001E8D5A File Offset: 0x001E6F5A
		public GameObject Body
		{
			get
			{
				return this._Body;
			}
			set
			{
				this.SetBody(value, false);
			}
		}

		// Token: 0x04002ED6 RID: 11990
		[NonSerialized]
		public GameObject _Body;

		// Token: 0x04002ED7 RID: 11991
		[NonSerialized]
		internal RingDeque<Cell> PlayerCells = new RingDeque<Cell>(8);

		// Token: 0x04002ED8 RID: 11992
		public MessageQueue Messages = new MessageQueue();
	}
}
