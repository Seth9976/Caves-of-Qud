using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World;

namespace ConsoleLib.Console
{
	// Token: 0x02001543 RID: 5443
	[GenerateSerializationPartial]
	[Serializable]
	public class SnapshotRenderable : Renderable
	{
		// Token: 0x0600D43A RID: 54330 RVA: 0x004ED62D File Offset: 0x004EB82D
		public SnapshotRenderable()
		{
		}

		// Token: 0x0600D43B RID: 54331 RVA: 0x004ED638 File Offset: 0x004EB838
		public SnapshotRenderable(IRenderable Source)
		{
			base.Copy(Source);
			this.Tile = Source.getTile();
			this.RenderString = Source.getRenderString();
			this.ColorString = Source.getColorString();
			this.TileColor = Source.getTileColor();
			this.DetailColor = Source.getDetailColor();
			this.HFlip = Source.getHFlip();
			this.VFlip = Source.getVFlip();
		}

		// Token: 0x0600D43C RID: 54332 RVA: 0x004ED6A6 File Offset: 0x004EB8A6
		public override bool getHFlip()
		{
			return this.HFlip;
		}

		// Token: 0x0600D43D RID: 54333 RVA: 0x004ED6AE File Offset: 0x004EB8AE
		public override bool getVFlip()
		{
			return this.VFlip;
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600D43E RID: 54334 RVA: 0x004ED6B6 File Offset: 0x004EB8B6
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600D43F RID: 54335 RVA: 0x004ED6BC File Offset: 0x004EB8BC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.HFlip);
			Writer.Write(this.VFlip);
			Writer.WriteOptimized(this.Tile);
			Writer.WriteOptimized(this.RenderString);
			Writer.WriteOptimized(this.ColorString);
			Writer.WriteOptimized(this.TileColor);
			Writer.Write(this.DetailColor);
		}

		// Token: 0x0600D440 RID: 54336 RVA: 0x004ED720 File Offset: 0x004EB920
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.HFlip = Reader.ReadBoolean();
			this.VFlip = Reader.ReadBoolean();
			this.Tile = Reader.ReadOptimizedString();
			this.RenderString = Reader.ReadOptimizedString();
			this.ColorString = Reader.ReadOptimizedString();
			this.TileColor = Reader.ReadOptimizedString();
			this.DetailColor = Reader.ReadChar();
		}

		// Token: 0x0400551A RID: 21786
		public bool HFlip;

		// Token: 0x0400551B RID: 21787
		public bool VFlip;
	}
}
