using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200061A RID: 1562
	internal static class CommandManipulator
	{
		// Token: 0x06002E2F RID: 11823 RVA: 0x000A38B0 File Offset: 0x000A1AB0
		private static bool IsParentOrAncestorOf(this VisualElement ve, VisualElement child)
		{
			while (child.hierarchy.parent != null)
			{
				bool flag = child.hierarchy.parent == ve;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000A3908 File Offset: 0x000A1B08
		public static void ReplaceCommands(RenderChain renderChain, VisualElement ve, EntryProcessor processor)
		{
			bool flag = processor.firstHeadCommand == null && processor.firstTailCommand == null && ve.renderChainData.firstHeadCommand != null;
			if (flag)
			{
				CommandManipulator.ResetCommands(renderChain, ve);
			}
			else
			{
				bool flag2 = false;
				RenderChainCommand renderChainCommand = null;
				RenderChainCommand renderChainCommand2 = null;
				bool flag3 = ve.renderChainData.firstHeadCommand != null;
				if (flag3)
				{
					renderChainCommand = ve.renderChainData.firstHeadCommand.prev;
					renderChainCommand2 = ve.renderChainData.lastHeadCommand.next;
					CommandManipulator.RemoveChain(renderChain, ve.renderChainData.firstHeadCommand, ve.renderChainData.lastHeadCommand);
					flag2 = true;
				}
				bool flag4 = processor.firstHeadCommand != null;
				if (flag4)
				{
					bool flag5 = !flag2;
					if (flag5)
					{
						CommandManipulator.FindHeadCommandInsertionPoint(ve, out renderChainCommand, out renderChainCommand2);
					}
					bool flag6 = renderChainCommand != null;
					if (flag6)
					{
						processor.firstHeadCommand.prev = renderChainCommand;
						renderChainCommand.next = processor.firstHeadCommand;
					}
					bool flag7 = renderChainCommand2 != null;
					if (flag7)
					{
						processor.lastHeadCommand.next = renderChainCommand2;
						renderChainCommand2.prev = processor.lastHeadCommand;
					}
					renderChain.OnRenderCommandAdded(processor.firstHeadCommand);
				}
				ve.renderChainData.firstHeadCommand = processor.firstHeadCommand;
				ve.renderChainData.lastHeadCommand = processor.lastHeadCommand;
				bool flag8 = false;
				RenderChainCommand renderChainCommand3 = null;
				RenderChainCommand renderChainCommand4 = null;
				bool flag9 = ve.renderChainData.firstTailCommand != null;
				if (flag9)
				{
					renderChainCommand3 = ve.renderChainData.firstTailCommand.prev;
					renderChainCommand4 = ve.renderChainData.lastTailCommand.next;
					CommandManipulator.RemoveChain(renderChain, ve.renderChainData.firstTailCommand, ve.renderChainData.lastTailCommand);
					flag8 = true;
				}
				bool flag10 = processor.firstTailCommand != null;
				if (flag10)
				{
					bool flag11 = !flag8;
					if (flag11)
					{
						CommandManipulator.FindTailCommandInsertionPoint(ve, out renderChainCommand3, out renderChainCommand4);
					}
					bool flag12 = renderChainCommand3 != null;
					if (flag12)
					{
						processor.firstTailCommand.prev = renderChainCommand3;
						renderChainCommand3.next = processor.firstTailCommand;
					}
					bool flag13 = renderChainCommand4 != null;
					if (flag13)
					{
						processor.lastTailCommand.next = renderChainCommand4;
						renderChainCommand4.prev = processor.lastTailCommand;
					}
					renderChain.OnRenderCommandAdded(processor.firstTailCommand);
				}
				ve.renderChainData.firstTailCommand = processor.firstTailCommand;
				ve.renderChainData.lastTailCommand = processor.lastTailCommand;
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000A3B50 File Offset: 0x000A1D50
		private static void FindHeadCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement visualElement = ve.renderChainData.prev;
			while (visualElement != null && visualElement.renderChainData.lastHeadCommand == null)
			{
				visualElement = visualElement.renderChainData.prev;
			}
			bool flag = visualElement != null && visualElement.renderChainData.lastHeadCommand != null;
			if (flag)
			{
				bool flag2 = visualElement.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					prev = visualElement.renderChainData.lastTailOrHeadCommand;
				}
				else
				{
					bool flag3 = visualElement.IsParentOrAncestorOf(ve);
					if (flag3)
					{
						prev = visualElement.renderChainData.lastHeadCommand;
					}
					else
					{
						RenderChainCommand renderChainCommand = visualElement.renderChainData.lastTailOrHeadCommand;
						bool flag5;
						do
						{
							prev = renderChainCommand;
							renderChainCommand = renderChainCommand.next;
							bool flag4 = renderChainCommand == null || renderChainCommand.owner == ve || !renderChainCommand.isTail;
							if (flag4)
							{
								break;
							}
							flag5 = renderChainCommand.owner.IsParentOrAncestorOf(ve);
						}
						while (!flag5);
					}
				}
				next = prev.next;
			}
			else
			{
				VisualElement visualElement2 = ve.renderChainData.next;
				while (visualElement2 != null && visualElement2.renderChainData.firstHeadCommand == null)
				{
					visualElement2 = visualElement2.renderChainData.next;
				}
				next = ((visualElement2 != null) ? visualElement2.renderChainData.firstHeadCommand : null);
				prev = null;
				Debug.Assert(next == null || next.prev == null);
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000A3CCC File Offset: 0x000A1ECC
		private static void FindTailCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement visualElement = ve.renderChainData.next;
			while (visualElement != null && visualElement.renderChainData.firstHeadCommand == null)
			{
				visualElement = visualElement.renderChainData.next;
			}
			bool flag = visualElement != null && visualElement.renderChainData.firstHeadCommand != null;
			if (flag)
			{
				bool flag2 = visualElement.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					next = visualElement.renderChainData.firstHeadCommand;
					prev = next.prev;
				}
				else
				{
					bool flag3 = ve.IsParentOrAncestorOf(visualElement);
					if (flag3)
					{
						bool flag4;
						do
						{
							prev = visualElement.renderChainData.lastTailOrHeadCommand;
							RenderChainCommand next2 = prev.next;
							visualElement = ((next2 != null) ? next2.owner : null);
							flag4 = visualElement == null || !ve.IsParentOrAncestorOf(visualElement);
						}
						while (!flag4);
						next = prev.next;
					}
					else
					{
						prev = ve.renderChainData.lastHeadCommand;
						next = prev.next;
					}
				}
			}
			else
			{
				prev = ve.renderChainData.lastHeadCommand;
				next = prev.next;
			}
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000A3DF4 File Offset: 0x000A1FF4
		private static void RemoveChain(RenderChain renderChain, RenderChainCommand first, RenderChainCommand last)
		{
			Debug.Assert(first != null);
			Debug.Assert(last != null);
			renderChain.OnRenderCommandsRemoved(first, last);
			bool flag = first.prev != null;
			if (flag)
			{
				first.prev.next = last.next;
			}
			bool flag2 = last.next != null;
			if (flag2)
			{
				last.next.prev = first.prev;
			}
			RenderChainCommand renderChainCommand = first;
			RenderChainCommand renderChainCommand2;
			do
			{
				RenderChainCommand next = renderChainCommand.next;
				renderChain.FreeCommand(renderChainCommand);
				renderChainCommand2 = renderChainCommand;
				renderChainCommand = next;
			}
			while (renderChainCommand2 != last);
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000A3E84 File Offset: 0x000A2084
		public static void ResetCommands(RenderChain renderChain, VisualElement ve)
		{
			bool flag = ve.renderChainData.firstHeadCommand != null;
			if (flag)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstHeadCommand, ve.renderChainData.lastHeadCommand);
			}
			RenderChainCommand renderChainCommand = ((ve.renderChainData.firstHeadCommand != null) ? ve.renderChainData.firstHeadCommand.prev : null);
			RenderChainCommand renderChainCommand2 = ((ve.renderChainData.lastHeadCommand != null) ? ve.renderChainData.lastHeadCommand.next : null);
			Debug.Assert(renderChainCommand == null || renderChainCommand.owner != ve);
			Debug.Assert(renderChainCommand2 == null || renderChainCommand2 == ve.renderChainData.firstTailCommand || renderChainCommand2.owner != ve);
			bool flag2 = renderChainCommand != null;
			if (flag2)
			{
				renderChainCommand.next = renderChainCommand2;
			}
			bool flag3 = renderChainCommand2 != null;
			if (flag3)
			{
				renderChainCommand2.prev = renderChainCommand;
			}
			bool flag4 = ve.renderChainData.firstHeadCommand != null;
			if (flag4)
			{
				RenderChainCommand renderChainCommand3;
				RenderChainCommand next;
				for (renderChainCommand3 = ve.renderChainData.firstHeadCommand; renderChainCommand3 != ve.renderChainData.lastHeadCommand; renderChainCommand3 = next)
				{
					next = renderChainCommand3.next;
					renderChain.FreeCommand(renderChainCommand3);
				}
				renderChain.FreeCommand(renderChainCommand3);
			}
			ve.renderChainData.firstHeadCommand = (ve.renderChainData.lastHeadCommand = null);
			renderChainCommand = ((ve.renderChainData.firstTailCommand != null) ? ve.renderChainData.firstTailCommand.prev : null);
			renderChainCommand2 = ((ve.renderChainData.lastTailCommand != null) ? ve.renderChainData.lastTailCommand.next : null);
			Debug.Assert(renderChainCommand == null || renderChainCommand.owner != ve);
			Debug.Assert(renderChainCommand2 == null || renderChainCommand2.owner != ve);
			bool flag5 = renderChainCommand != null;
			if (flag5)
			{
				renderChainCommand.next = renderChainCommand2;
			}
			bool flag6 = renderChainCommand2 != null;
			if (flag6)
			{
				renderChainCommand2.prev = renderChainCommand;
			}
			bool flag7 = ve.renderChainData.firstTailCommand != null;
			if (flag7)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstTailCommand, ve.renderChainData.lastTailCommand);
				RenderChainCommand renderChainCommand4;
				RenderChainCommand next2;
				for (renderChainCommand4 = ve.renderChainData.firstTailCommand; renderChainCommand4 != ve.renderChainData.lastTailCommand; renderChainCommand4 = next2)
				{
					next2 = renderChainCommand4.next;
					renderChain.FreeCommand(renderChainCommand4);
				}
				renderChain.FreeCommand(renderChainCommand4);
			}
			ve.renderChainData.firstTailCommand = (ve.renderChainData.lastTailCommand = null);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000A4108 File Offset: 0x000A2308
		private static void InjectCommandInBetween(RenderChain renderChain, RenderChainCommand cmd, RenderChainCommand prev, RenderChainCommand next)
		{
			bool flag = prev != null;
			if (flag)
			{
				cmd.prev = prev;
				prev.next = cmd;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				cmd.next = next;
				next.prev = cmd;
			}
			VisualElement owner = cmd.owner;
			bool flag3 = !cmd.isTail;
			if (flag3)
			{
				bool flag4 = owner.renderChainData.firstHeadCommand == null || owner.renderChainData.firstHeadCommand == next;
				if (flag4)
				{
					owner.renderChainData.firstHeadCommand = cmd;
				}
				bool flag5 = owner.renderChainData.lastHeadCommand == null || owner.renderChainData.lastHeadCommand == prev;
				if (flag5)
				{
					owner.renderChainData.lastHeadCommand = cmd;
				}
			}
			else
			{
				bool flag6 = owner.renderChainData.firstTailCommand == null || owner.renderChainData.firstTailCommand == next;
				if (flag6)
				{
					owner.renderChainData.firstTailCommand = cmd;
				}
				bool flag7 = owner.renderChainData.lastTailCommand == null || owner.renderChainData.lastTailCommand == prev;
				if (flag7)
				{
					owner.renderChainData.lastTailCommand = cmd;
				}
			}
			renderChain.OnRenderCommandAdded(cmd);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000A4228 File Offset: 0x000A2428
		public static void DisableElementRendering(RenderChain renderChain, VisualElement ve, bool renderingDisabled)
		{
			bool flag = !ve.renderChainData.isInChain;
			if (!flag)
			{
				if (renderingDisabled)
				{
					bool flag2 = ve.renderChainData.firstHeadCommand == null || ve.renderChainData.firstHeadCommand.type != CommandType.BeginDisable;
					if (flag2)
					{
						RenderChainCommand renderChainCommand = renderChain.AllocCommand();
						renderChainCommand.type = CommandType.BeginDisable;
						renderChainCommand.owner = ve;
						bool flag3 = ve.renderChainData.firstHeadCommand == null;
						if (flag3)
						{
							RenderChainCommand renderChainCommand2;
							RenderChainCommand renderChainCommand3;
							CommandManipulator.FindHeadCommandInsertionPoint(ve, out renderChainCommand2, out renderChainCommand3);
							CommandManipulator.InjectCommandInBetween(renderChain, renderChainCommand, renderChainCommand2, renderChainCommand3);
						}
						else
						{
							RenderChainCommand prev = ve.renderChainData.firstHeadCommand.prev;
							RenderChainCommand firstHeadCommand = ve.renderChainData.firstHeadCommand;
							RenderChainCommand lastHeadCommand = ve.renderChainData.lastHeadCommand;
							Debug.Assert(lastHeadCommand != null);
							ve.renderChainData.firstHeadCommand = null;
							CommandManipulator.InjectCommandInBetween(renderChain, renderChainCommand, prev, firstHeadCommand);
							ve.renderChainData.lastHeadCommand = lastHeadCommand;
						}
					}
					bool flag4 = ve.renderChainData.lastTailCommand == null || ve.renderChainData.lastTailCommand.type != CommandType.EndDisable;
					if (flag4)
					{
						RenderChainCommand renderChainCommand4 = renderChain.AllocCommand();
						renderChainCommand4.type = CommandType.EndDisable;
						renderChainCommand4.isTail = true;
						renderChainCommand4.owner = ve;
						bool flag5 = ve.renderChainData.lastTailCommand == null;
						if (flag5)
						{
							RenderChainCommand renderChainCommand5;
							RenderChainCommand renderChainCommand6;
							CommandManipulator.FindTailCommandInsertionPoint(ve, out renderChainCommand5, out renderChainCommand6);
							CommandManipulator.InjectCommandInBetween(renderChain, renderChainCommand4, renderChainCommand5, renderChainCommand6);
						}
						else
						{
							RenderChainCommand lastTailCommand = ve.renderChainData.lastTailCommand;
							RenderChainCommand next = ve.renderChainData.lastTailCommand.next;
							Debug.Assert(ve.renderChainData.firstTailCommand != null);
							CommandManipulator.InjectCommandInBetween(renderChain, renderChainCommand4, lastTailCommand, next);
						}
					}
				}
				else
				{
					bool flag6 = ve.renderChainData.firstHeadCommand != null && ve.renderChainData.firstHeadCommand.type == CommandType.BeginDisable;
					if (flag6)
					{
						CommandManipulator.RemoveSingleCommand(renderChain, ve, ve.renderChainData.firstHeadCommand);
					}
					bool flag7 = ve.renderChainData.lastTailCommand != null && ve.renderChainData.lastTailCommand.type == CommandType.EndDisable;
					if (flag7)
					{
						CommandManipulator.RemoveSingleCommand(renderChain, ve, ve.renderChainData.lastTailCommand);
					}
				}
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000A4470 File Offset: 0x000A2670
		private static void RemoveSingleCommand(RenderChain renderChain, VisualElement ve, RenderChainCommand cmd)
		{
			Debug.Assert(cmd != null);
			Debug.Assert(cmd.owner == ve);
			renderChain.OnRenderCommandsRemoved(cmd, cmd);
			RenderChainCommand prev = cmd.prev;
			RenderChainCommand next = cmd.next;
			bool flag = prev != null;
			if (flag)
			{
				prev.next = next;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				next.prev = prev;
			}
			bool flag3 = ve.renderChainData.firstHeadCommand == cmd;
			if (flag3)
			{
				bool flag4 = ve.renderChainData.firstHeadCommand == ve.renderChainData.lastHeadCommand;
				if (flag4)
				{
					RenderChainCommand prev2 = cmd.prev;
					Debug.Assert(((prev2 != null) ? prev2.owner : null) != ve, "When removing the first head command, the command before this one in the queue should belong to an other parent");
					RenderChainCommand next2 = cmd.next;
					Debug.Assert(((next2 != null) ? next2.owner : null) != ve || cmd.next == ve.renderChainData.firstTailCommand);
					ve.renderChainData.firstHeadCommand = null;
					ve.renderChainData.lastHeadCommand = null;
				}
				else
				{
					Debug.Assert(cmd.next.owner == ve);
					Debug.Assert(ve.renderChainData.lastHeadCommand != null);
					ve.renderChainData.firstHeadCommand = cmd.next;
				}
			}
			else
			{
				bool flag5 = ve.renderChainData.lastHeadCommand == cmd;
				if (flag5)
				{
					Debug.Assert(cmd.prev.owner == ve);
					Debug.Assert(ve.renderChainData.firstHeadCommand != null);
					ve.renderChainData.lastHeadCommand = cmd.prev;
				}
			}
			bool flag6 = ve.renderChainData.firstTailCommand == cmd;
			if (flag6)
			{
				bool flag7 = ve.renderChainData.firstTailCommand == ve.renderChainData.lastTailCommand;
				if (flag7)
				{
					RenderChainCommand prev3 = cmd.prev;
					Debug.Assert(((prev3 != null) ? prev3.owner : null) != ve || cmd.prev == ve.renderChainData.lastHeadCommand);
					RenderChainCommand next3 = cmd.next;
					Debug.Assert(((next3 != null) ? next3.owner : null) != ve);
					ve.renderChainData.firstTailCommand = null;
					ve.renderChainData.lastTailCommand = null;
				}
				else
				{
					Debug.Assert(cmd.next.owner == ve);
					Debug.Assert(ve.renderChainData.lastTailCommand != null);
					ve.renderChainData.firstTailCommand = cmd.next;
				}
			}
			else
			{
				bool flag8 = ve.renderChainData.lastTailCommand == cmd;
				if (flag8)
				{
					Debug.Assert(cmd.prev.owner == ve);
					Debug.Assert(ve.renderChainData.firstTailCommand != null);
					ve.renderChainData.lastTailCommand = cmd.prev;
				}
			}
			renderChain.FreeCommand(cmd);
		}
	}
}
