using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Cci;
using Microsoft.Cci.Pdb;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x02000009 RID: 9
	public class NativePdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000255B File Offset: 0x0000075B
		internal NativePdbReader(Disposable<Stream> file)
		{
			this.pdb_file = file;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000258B File Offset: 0x0000078B
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new NativePdbWriterProvider();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002594 File Offset: 0x00000794
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			if (!header.HasEntries)
			{
				return false;
			}
			ImageDebugHeaderEntry codeViewEntry = header.GetCodeViewEntry();
			if (codeViewEntry == null)
			{
				return false;
			}
			ImageDebugDirectory directory = codeViewEntry.Directory;
			if (directory.Type != ImageDebugType.CodeView)
			{
				return false;
			}
			if (directory.MajorVersion != 0 || directory.MinorVersion != 0)
			{
				return false;
			}
			byte[] data = codeViewEntry.Data;
			if (data.Length < 24)
			{
				return false;
			}
			if (NativePdbReader.ReadInt32(data, 0) != 1396986706)
			{
				return false;
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(data, 4, array, 0, 16);
			this.guid = new Guid(array);
			this.age = NativePdbReader.ReadInt32(data, 20);
			return this.PopulateFunctions();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000262E File Offset: 0x0000082E
		private static int ReadInt32(byte[] bytes, int start)
		{
			return (int)bytes[start] | ((int)bytes[start + 1] << 8) | ((int)bytes[start + 2] << 16) | ((int)bytes[start + 3] << 24);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002650 File Offset: 0x00000850
		private bool PopulateFunctions()
		{
			using (this.pdb_file)
			{
				Dictionary<uint, PdbTokenLine> dictionary;
				string text;
				int num;
				Guid guid;
				PdbFunction[] array = PdbFile.LoadFunctions(this.pdb_file.value, out dictionary, out text, out num, out guid);
				if (this.guid != guid)
				{
					return false;
				}
				PdbFunction[] array2 = array;
				foreach (PdbFunction pdbFunction in array2)
				{
					this.functions.Add(pdbFunction.token, pdbFunction);
				}
			}
			return true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000026EC File Offset: 0x000008EC
		public MethodDebugInformation Read(MethodDefinition method)
		{
			MetadataToken metadataToken = method.MetadataToken;
			PdbFunction pdbFunction;
			if (!this.functions.TryGetValue(metadataToken.ToUInt32(), ref pdbFunction))
			{
				return null;
			}
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			this.ReadSequencePoints(pdbFunction, methodDebugInformation);
			MethodDebugInformation methodDebugInformation2 = methodDebugInformation;
			ScopeDebugInformation scopeDebugInformation2;
			if (pdbFunction.scopes.IsNullOrEmpty<PdbScope>())
			{
				ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation();
				scopeDebugInformation.Start = new InstructionOffset(0);
				scopeDebugInformation2 = scopeDebugInformation;
				scopeDebugInformation.End = new InstructionOffset((int)pdbFunction.length);
			}
			else
			{
				scopeDebugInformation2 = this.ReadScopeAndLocals(pdbFunction.scopes[0], methodDebugInformation);
			}
			methodDebugInformation2.scope = scopeDebugInformation2;
			if (pdbFunction.tokenOfMethodWhoseUsingInfoAppliesToThisMethod != method.MetadataToken.ToUInt32() && pdbFunction.tokenOfMethodWhoseUsingInfoAppliesToThisMethod != 0U)
			{
				methodDebugInformation.scope.import = this.GetImport(pdbFunction.tokenOfMethodWhoseUsingInfoAppliesToThisMethod, method.Module);
			}
			if (pdbFunction.scopes.Length > 1)
			{
				for (int i = 1; i < pdbFunction.scopes.Length; i++)
				{
					ScopeDebugInformation scopeDebugInformation3 = this.ReadScopeAndLocals(pdbFunction.scopes[i], methodDebugInformation);
					if (!NativePdbReader.AddScope(methodDebugInformation.scope.Scopes, scopeDebugInformation3))
					{
						methodDebugInformation.scope.Scopes.Add(scopeDebugInformation3);
					}
				}
			}
			if (pdbFunction.iteratorScopes != null)
			{
				foreach (ILocalScope localScope in pdbFunction.iteratorScopes)
				{
					methodDebugInformation.CustomDebugInformations.Add(new StateMachineScopeDebugInformation((int)localScope.Offset, (int)(localScope.Offset + localScope.Length + 1U)));
				}
			}
			if (pdbFunction.synchronizationInformation != null)
			{
				AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation = new AsyncMethodBodyDebugInformation((int)pdbFunction.synchronizationInformation.GeneratedCatchHandlerOffset);
				PdbSynchronizationPoint[] synchronizationPoints = pdbFunction.synchronizationInformation.synchronizationPoints;
				foreach (PdbSynchronizationPoint pdbSynchronizationPoint in synchronizationPoints)
				{
					asyncMethodBodyDebugInformation.Yields.Add(new InstructionOffset((int)pdbSynchronizationPoint.SynchronizeOffset));
					asyncMethodBodyDebugInformation.Resumes.Add(new InstructionOffset((int)pdbSynchronizationPoint.ContinuationOffset));
				}
				methodDebugInformation.CustomDebugInformations.Add(asyncMethodBodyDebugInformation);
				asyncMethodBodyDebugInformation.MoveNextMethod = method;
				methodDebugInformation.StateMachineKickOffMethod = (MethodDefinition)method.Module.LookupToken((int)pdbFunction.synchronizationInformation.kickoffMethodToken);
			}
			return methodDebugInformation;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002924 File Offset: 0x00000B24
		private Collection<ScopeDebugInformation> ReadScopeAndLocals(PdbScope[] scopes, MethodDebugInformation info)
		{
			Collection<ScopeDebugInformation> collection = new Collection<ScopeDebugInformation>(scopes.Length);
			foreach (PdbScope pdbScope in scopes)
			{
				if (pdbScope != null)
				{
					collection.Add(this.ReadScopeAndLocals(pdbScope, info));
				}
			}
			return collection;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002960 File Offset: 0x00000B60
		private ScopeDebugInformation ReadScopeAndLocals(PdbScope scope, MethodDebugInformation info)
		{
			ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation();
			scopeDebugInformation.Start = new InstructionOffset((int)scope.offset);
			scopeDebugInformation.End = new InstructionOffset((int)(scope.offset + scope.length));
			if (!scope.slots.IsNullOrEmpty<PdbSlot>())
			{
				scopeDebugInformation.variables = new Collection<VariableDebugInformation>(scope.slots.Length);
				PdbSlot[] slots = scope.slots;
				foreach (PdbSlot pdbSlot in slots)
				{
					if (pdbSlot.flags != 1)
					{
						VariableDebugInformation variableDebugInformation = new VariableDebugInformation((int)pdbSlot.slot, pdbSlot.name);
						if (pdbSlot.flags == 4)
						{
							variableDebugInformation.IsDebuggerHidden = true;
						}
						scopeDebugInformation.variables.Add(variableDebugInformation);
					}
				}
			}
			if (!scope.constants.IsNullOrEmpty<PdbConstant>())
			{
				scopeDebugInformation.constants = new Collection<ConstantDebugInformation>(scope.constants.Length);
				PdbConstant[] constants = scope.constants;
				foreach (PdbConstant pdbConstant in constants)
				{
					TypeReference typeReference = info.Method.Module.Read<PdbConstant, TypeReference>(pdbConstant, (PdbConstant c, MetadataReader r) => r.ReadConstantSignature(new MetadataToken(c.token)));
					object obj = pdbConstant.value;
					if (typeReference != null && !typeReference.IsValueType && obj is int && (int)obj == 0)
					{
						obj = null;
					}
					scopeDebugInformation.constants.Add(new ConstantDebugInformation(pdbConstant.name, typeReference, obj));
				}
			}
			if (!scope.usedNamespaces.IsNullOrEmpty<string>())
			{
				ImportDebugInformation import;
				if (this.imports.TryGetValue(scope, ref import))
				{
					scopeDebugInformation.import = import;
				}
				else
				{
					import = NativePdbReader.GetImport(scope, info.Method.Module);
					this.imports.Add(scope, import);
					scopeDebugInformation.import = import;
				}
			}
			scopeDebugInformation.scopes = this.ReadScopeAndLocals(scope.scopes, info);
			return scopeDebugInformation;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002B44 File Offset: 0x00000D44
		private static bool AddScope(Collection<ScopeDebugInformation> scopes, ScopeDebugInformation scope)
		{
			foreach (ScopeDebugInformation scopeDebugInformation in scopes)
			{
				if (scopeDebugInformation.HasScopes && NativePdbReader.AddScope(scopeDebugInformation.Scopes, scope))
				{
					return true;
				}
				if (scope.Start.Offset >= scopeDebugInformation.Start.Offset && scope.End.Offset <= scopeDebugInformation.End.Offset)
				{
					scopeDebugInformation.Scopes.Add(scope);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private ImportDebugInformation GetImport(uint token, ModuleDefinition module)
		{
			PdbFunction pdbFunction;
			if (!this.functions.TryGetValue(token, ref pdbFunction))
			{
				return null;
			}
			if (pdbFunction.scopes.Length != 1)
			{
				return null;
			}
			PdbScope pdbScope = pdbFunction.scopes[0];
			ImportDebugInformation import;
			if (this.imports.TryGetValue(pdbScope, ref import))
			{
				return import;
			}
			import = NativePdbReader.GetImport(pdbScope, module);
			this.imports.Add(pdbScope, import);
			return import;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002C54 File Offset: 0x00000E54
		private static ImportDebugInformation GetImport(PdbScope scope, ModuleDefinition module)
		{
			if (scope.usedNamespaces.IsNullOrEmpty<string>())
			{
				return null;
			}
			ImportDebugInformation importDebugInformation = new ImportDebugInformation();
			string[] usedNamespaces = scope.usedNamespaces;
			foreach (string text in usedNamespaces)
			{
				if (!string.IsNullOrEmpty(text))
				{
					ImportTarget importTarget = null;
					string text2 = text.Substring(1);
					char c = text.get_Chars(0);
					if (c != 'A')
					{
						if (c != 'T')
						{
							if (c == 'U')
							{
								importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
								{
									@namespace = text2
								};
							}
						}
						else
						{
							TypeReference type = module.GetType(text2, true);
							if (type != null)
							{
								importTarget = new ImportTarget(ImportTargetKind.ImportType)
								{
									type = type
								};
							}
						}
					}
					else
					{
						int num = text.IndexOf(' ');
						string text3 = text.Substring(1, num - 1);
						string text4 = text.Substring(num + 2);
						c = text.get_Chars(num + 1);
						if (c != 'T')
						{
							if (c == 'U')
							{
								importTarget = new ImportTarget(ImportTargetKind.DefineNamespaceAlias)
								{
									alias = text3,
									@namespace = text4
								};
							}
						}
						else
						{
							TypeReference type2 = module.GetType(text4, true);
							if (type2 != null)
							{
								importTarget = new ImportTarget(ImportTargetKind.DefineTypeAlias)
								{
									alias = text3,
									type = type2
								};
							}
						}
					}
					if (importTarget != null)
					{
						importDebugInformation.Targets.Add(importTarget);
					}
				}
			}
			return importDebugInformation;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002DA0 File Offset: 0x00000FA0
		private void ReadSequencePoints(PdbFunction function, MethodDebugInformation info)
		{
			if (function.lines != null)
			{
				info.sequence_points = new Collection<SequencePoint>();
				PdbLines[] lines = function.lines;
				foreach (PdbLines pdbLines in lines)
				{
					this.ReadLines(pdbLines, info);
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private void ReadLines(PdbLines lines, MethodDebugInformation info)
		{
			Document document = this.GetDocument(lines.file);
			PdbLine[] lines2 = lines.lines;
			for (int i = 0; i < lines2.Length; i++)
			{
				NativePdbReader.ReadLine(lines2[i], document, info);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002E24 File Offset: 0x00001024
		private static void ReadLine(PdbLine line, Document document, MethodDebugInformation info)
		{
			SequencePoint sequencePoint = new SequencePoint((int)line.offset, document);
			sequencePoint.StartLine = (int)line.lineBegin;
			sequencePoint.StartColumn = (int)line.colBegin;
			sequencePoint.EndLine = (int)line.lineEnd;
			sequencePoint.EndColumn = (int)line.colEnd;
			info.sequence_points.Add(sequencePoint);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002E7C File Offset: 0x0000107C
		private Document GetDocument(PdbSource source)
		{
			string name = source.name;
			Document document;
			if (this.documents.TryGetValue(name, ref document))
			{
				return document;
			}
			document = new Document(name)
			{
				Language = source.language.ToLanguage(),
				LanguageVendor = source.vendor.ToVendor(),
				Type = source.doctype.ToType()
			};
			this.documents.Add(name, document);
			return document;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002EEC File Offset: 0x000010EC
		public void Dispose()
		{
			this.pdb_file.Dispose();
		}

		// Token: 0x0400000E RID: 14
		private int age;

		// Token: 0x0400000F RID: 15
		private Guid guid;

		// Token: 0x04000010 RID: 16
		private readonly Disposable<Stream> pdb_file;

		// Token: 0x04000011 RID: 17
		private readonly Dictionary<string, Document> documents = new Dictionary<string, Document>();

		// Token: 0x04000012 RID: 18
		private readonly Dictionary<uint, PdbFunction> functions = new Dictionary<uint, PdbFunction>();

		// Token: 0x04000013 RID: 19
		private readonly Dictionary<PdbScope, ImportDebugInformation> imports = new Dictionary<PdbScope, ImportDebugInformation>();
	}
}
