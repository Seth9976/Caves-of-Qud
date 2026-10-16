using System;
using System.Collections.Generic;
using RoslynCSharp.Compiler;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoslynCSharp.Example
{
	// Token: 0x02000016 RID: 22
	public class MazeCrawlerExample : MonoBehaviour
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002E78 File Offset: 0x00001078
		public void Awake()
		{
			this.runCrawlerButton.onClick.AddListener(new UnityAction(this.RunCrawler));
			this.stopCrawlerButton.onClick.AddListener(new UnityAction(this.StopCrawler));
			this.restartCrawlerButton.onClick.AddListener(new UnityAction(this.RestartCrawler));
			this.editCodeButton.onClick.AddListener(delegate
			{
				this.codeEditorWindow.SetActive(true);
			});
			this.codeEditorCloseButton.onClick.AddListener(delegate
			{
				this.codeEditorWindow.SetActive(false);
			});
			this.codeEditorLoadTemplateButton.onClick.AddListener(delegate
			{
				this.runCrawlerInput.text = this.mazeCodeTemplate.text;
			});
			this.codeEditorLoadSolutionButton.onClick.AddListener(delegate
			{
				this.runCrawlerInput.text = this.mazeCodeSolution.text;
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F4C File Offset: 0x0000114C
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("MazeCrawlerCode", true, true, null);
			try
			{
				IMetadataReferenceProvider metadataReferenceProvider = AssemblyReference.FromNameOrFile("netstandard", null);
				if (metadataReferenceProvider.TryResolveReference())
				{
					this.domain.RoslynCompilerService.ReferenceAssemblies.Add(metadataReferenceProvider);
				}
			}
			catch
			{
			}
			this.domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(typeof(MazeCrawlerExample).Assembly));
			this.domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(typeof(global::UnityEngine.Object).Assembly));
			this.domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(typeof(Stack<>).Assembly));
			this.domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(typeof(HashSet<>).Assembly));
			if (this.showCompletedCodeOnStartup)
			{
				this.runCrawlerInput.text = this.mazeCodeSolution.text;
				return;
			}
			this.runCrawlerInput.text = this.mazeCodeTemplate.text;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003084 File Offset: 0x00001284
		public void RunCrawler()
		{
			string text = this.runCrawlerInput.text;
			if (this.activeCSharpSource != text || this.activeCrawlerScript == null)
			{
				this.StopCrawler();
				try
				{
					ScriptType scriptType = this.domain.CompileAndLoadMainSource(text, ScriptSecurityMode.UseSettings, null);
					if (scriptType == null)
					{
						if (!this.domain.RoslynCompilerService.LastCompileResult.Success)
						{
							throw new Exception("Maze crawler code contained errors. Please fix and try again");
						}
						if (!this.domain.SecurityResult.IsSecurityVerified)
						{
							throw new Exception("Maze crawler code failed code security verification");
						}
						throw new Exception("Maze crawler code does not define a class. You must include one class definition of any name that inherits from 'RoslynCSharp.Example.MazeCrawler'");
					}
					else
					{
						if (!scriptType.IsSubTypeOf<MazeCrawler>())
						{
							throw new Exception("Maze crawler code must define a single type that inherits from 'RoslynCSharp.Example.MazeCrawler'");
						}
						this.activeCrawlerScript = scriptType.CreateInstance(this.mazeMouse);
						this.activeCSharpSource = text;
						this.activeCrawlerScript.Fields["breadcrumbPrefab"] = this.breadcrumbPrefab;
						this.activeCrawlerScript.Fields["moveSpeed"] = this.mouseSpeed;
						return;
					}
				}
				catch (Exception ex)
				{
					this.codeEditorWindow.SetActive(true);
					throw ex;
				}
			}
			this.activeCrawlerScript.GetInstanceAs<MazeCrawler>(false, null).Restart();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031B0 File Offset: 0x000013B0
		public void StopCrawler()
		{
			if (this.activeCrawlerScript != null)
			{
				this.activeCrawlerScript.GetInstanceAs<MazeCrawler>(false, null).Restart();
				this.activeCrawlerScript.Dispose();
				this.activeCrawlerScript = null;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000031E0 File Offset: 0x000013E0
		public void RestartCrawler()
		{
			if (this.activeCrawlerScript != null)
			{
				ScriptType scriptType = this.activeCrawlerScript.ScriptType;
				this.activeCrawlerScript.GetInstanceAs<MazeCrawler>(false, null).Restart();
				this.activeCrawlerScript.Dispose();
				this.activeCrawlerScript = scriptType.CreateInstance(this.mazeMouse);
				this.activeCrawlerScript.Fields["breadcrumbPrefab"] = this.breadcrumbPrefab;
				this.activeCrawlerScript.Fields["moveSpeed"] = this.mouseSpeed;
			}
		}

		// Token: 0x0400002B RID: 43
		private string activeCSharpSource;

		// Token: 0x0400002C RID: 44
		private ScriptProxy activeCrawlerScript;

		// Token: 0x0400002D RID: 45
		private ScriptDomain domain;

		// Token: 0x0400002E RID: 46
		public InputField runCrawlerInput;

		// Token: 0x0400002F RID: 47
		public Button runCrawlerButton;

		// Token: 0x04000030 RID: 48
		public Button stopCrawlerButton;

		// Token: 0x04000031 RID: 49
		public Button restartCrawlerButton;

		// Token: 0x04000032 RID: 50
		public Button editCodeButton;

		// Token: 0x04000033 RID: 51
		public GameObject codeEditorWindow;

		// Token: 0x04000034 RID: 52
		public Button codeEditorCloseButton;

		// Token: 0x04000035 RID: 53
		public Button codeEditorLoadTemplateButton;

		// Token: 0x04000036 RID: 54
		public Button codeEditorLoadSolutionButton;

		// Token: 0x04000037 RID: 55
		public GameObject mazeMouse;

		// Token: 0x04000038 RID: 56
		public GameObject breadcrumbPrefab;

		// Token: 0x04000039 RID: 57
		public TextAsset mazeCodeTemplate;

		// Token: 0x0400003A RID: 58
		public TextAsset mazeCodeSolution;

		// Token: 0x0400003B RID: 59
		public float mouseSpeed = 5f;

		// Token: 0x0400003C RID: 60
		public bool showCompletedCodeOnStartup;
	}
}
