using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World
{
	/// <summary>
	/// Tracks ongoing cooldowns for commands, even should the source ability be lost and regained.
	/// </summary>
	// Token: 0x0200084C RID: 2124
	[GenerateSerializationPartial]
	public class CommandCooldown : ITokenized, IComposite
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06005436 RID: 21558 RVA: 0x0019176E File Offset: 0x0018F96E
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x00191771 File Offset: 0x0018F971
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Command);
			Writer.WriteOptimized(this.Segments);
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0019178B File Offset: 0x0018F98B
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Command = Reader.ReadOptimizedString();
			this.Segments = Reader.ReadOptimizedInt32();
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x001917A5 File Offset: 0x0018F9A5
		// (set) Token: 0x0600543A RID: 21562 RVA: 0x001917AD File Offset: 0x0018F9AD
		public int Token { get; set; }

		// Token: 0x0600543B RID: 21563 RVA: 0x001917B6 File Offset: 0x0018F9B6
		public CommandCooldown()
		{
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x001917BE File Offset: 0x0018F9BE
		public CommandCooldown(CommandCooldown Source)
		{
			this.Command = Source.Command;
			this.Segments = Source.Segments;
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x001917DE File Offset: 0x0018F9DE
		public CommandCooldown(string Command, int Segments = 0)
		{
			this.Command = Command;
			this.Segments = Segments;
		}

		// Token: 0x04002A53 RID: 10835
		public string Command;

		// Token: 0x04002A54 RID: 10836
		public int Segments;
	}
}
