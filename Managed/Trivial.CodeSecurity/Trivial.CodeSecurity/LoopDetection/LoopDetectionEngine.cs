using System;
using System.IO;
using Trivial.Mono.Cecil;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.CodeSecurity.LoopDetection
{
	// Token: 0x02000010 RID: 16
	public class LoopDetectionEngine : IDisposable
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003A4C File Offset: 0x00001C4C
		public LoopDetectionEngine(string assemblyDefinitionFile)
		{
			if (assemblyDefinitionFile == null)
			{
				throw new ArgumentNullException("assemblyDefinitionFile");
			}
			using (Stream stream = File.OpenRead(assemblyDefinitionFile))
			{
				this.InitializeFromStream(stream, null);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003A98 File Offset: 0x00001C98
		public LoopDetectionEngine(byte[] assemblyImage)
		{
			if (assemblyImage == null)
			{
				throw new ArgumentNullException("assemblyImage");
			}
			Stream stream = new MemoryStream(assemblyImage);
			this.InitializeFromStream(stream, null);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public LoopDetectionEngine(Stream assemblyDefinitionStream)
		{
			if (assemblyDefinitionStream == null)
			{
				throw new ArgumentNullException("assemblyDefinitionStream");
			}
			this.InitializeFromStream(assemblyDefinitionStream, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003AE6 File Offset: 0x00001CE6
		public void Dispose()
		{
			if (this.definition != null)
			{
				this.definition.Dispose();
				this.definition = null;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B04 File Offset: 0x00001D04
		public bool LoopDetectAndPatchAssembly()
		{
			LoopDetectionInstructionPatcher loopDetectionInstructionPatcher = new LoopDetectionInstructionPatcher(this.definition.MainModule);
			LoopDetectionHashGenerator loopDetectionHashGenerator = new LoopDetectionHashGenerator();
			bool flag = false;
			foreach (TypeDefinition typeDefinition in this.definition.MainModule.Types)
			{
				foreach (MethodDefinition methodDefinition in typeDefinition.Methods)
				{
					Collection<Instruction> instructions = methodDefinition.Body.Instructions;
					if (instructions != null && instructions.Count > 0 && loopDetectionInstructionPatcher.DetectPotentialLoopInstructions(instructions) && loopDetectionInstructionPatcher.PatchPotentialLoopInstructions(instructions, loopDetectionHashGenerator))
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private void InitializeFromStream(Stream loadStream, Stream symbolsStream = null)
		{
			this.definition = AssemblyDefinition.ReadAssembly(loadStream, new ReaderParameters
			{
				ReadSymbols = false
			});
			if (this.definition == null)
			{
				throw new Exception("Failed to read assembly definition for loop evaluation");
			}
		}

		// Token: 0x04000038 RID: 56
		private byte[] definitionBytes;

		// Token: 0x04000039 RID: 57
		private AssemblyDefinition definition;
	}
}
