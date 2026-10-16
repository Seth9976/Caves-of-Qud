using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.Collections;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x0200062D RID: 1581
	[GenerateSerializationPartial]
	public class GameEventCommand : IActionCommand, IComposite
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x00154897 File Offset: 0x00152A97
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x0015489A File Offset: 0x00152A9A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Command);
			Writer.WriteOptimized(this.Type);
			Writer.WriteOptimized(this.Level);
			Writer.WriteOptimized(this.Flags);
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x001548CC File Offset: 0x00152ACC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Command = Reader.ReadOptimizedString();
			this.Type = Reader.ReadOptimizedString();
			this.Level = Reader.ReadOptimizedInt32();
			this.Flags = Reader.ReadOptimizedInt32();
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00154900 File Offset: 0x00152B00
		public static void Issue(string Command, string Type = null, int Level = 0, int Flags = 0)
		{
			ActionManager actionManager = The.ActionManager;
			GameEventCommand gameEventCommand;
			if (!GameEventCommand.Pool.TryEject(out gameEventCommand))
			{
				gameEventCommand = new GameEventCommand();
			}
			gameEventCommand.Command = Command;
			gameEventCommand.Type = Type;
			gameEventCommand.Level = Level;
			gameEventCommand.Flags = Flags;
			actionManager.EnqueueAction(gameEventCommand, 0);
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x00154949 File Offset: 0x00152B49
		public void Execute(XRLGame Game, ActionManager Manager)
		{
			GenericCommandEvent.Send(Game, this.Command, this.Type, null, null, this.Level, this.Flags);
			GameEventCommand.Pool.Enqueue(this);
		}

		// Token: 0x0400206C RID: 8300
		private static RingDeque<GameEventCommand> Pool = new RingDeque<GameEventCommand>();

		// Token: 0x0400206D RID: 8301
		public string Command;

		// Token: 0x0400206E RID: 8302
		public string Type;

		// Token: 0x0400206F RID: 8303
		public int Level;

		// Token: 0x04002070 RID: 8304
		public int Flags;
	}
}
