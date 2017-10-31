using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Gui.Electron.Adapter
{
	public class ElectronDecompilerShellUiService : IDecompilerShellUiService {
		private readonly dynamic jsShellUi;
		private IServiceProvider services;

		public ElectronDecompilerShellUiService(
			dynamic jsShellUi,
			IServiceProvider sp
		) {
			this.jsShellUi = jsShellUi;
			this.services = sp;
		}

		public void ShowError(Exception ex, string format, params object[] args) {
			throw new NotImplementedException();
		}

		public string ShowOpenFileDialog(string fileName) {
			throw new NotImplementedException();
		}

		public string ShowSaveFileDialog(string fileName) {
			throw new NotImplementedException();
		}

		public bool Prompt(string prompt) {
			throw new NotImplementedException();
		}

		public void ShowMessage(string msg) {
			jsShellUi.ShowMessage(msg);
		}

		public bool QueryStatus(CommandID cmdId, CommandStatus status, CommandText text) {
			throw new NotImplementedException();
		}

		public bool Execute(CommandID cmdId) {
			throw new NotImplementedException();
		}

		public IWindowFrame ActiveFrame { get; }
		public IEnumerable<IWindowFrame> DocumentWindows { get; }
		public IEnumerable<IWindowFrame> ToolWindows { get; }
		public IWindowFrame CreateWindow(string windowType, string windowTitle, IWindowPane pane) {
			throw new NotImplementedException();
		}

		public IWindowFrame CreateDocumentWindow(string documentType, object docItem, string documentTitle, IWindowPane pane) {
			throw new NotImplementedException();
		}

		public IWindowFrame FindWindow(string windowType) {
			throw new NotImplementedException();
		}

		public IWindowFrame FindDocumentWindow(string documentType, object docItem) {
			throw new NotImplementedException();
		}

		public void SetContextMenu(object control, int menuID) {
			throw new NotImplementedException();
		}

		public DialogResult ShowModalDialog(IDialog dlg) {
			throw new NotImplementedException();
		}

		public void WithWaitCursor(Action p) {
			throw new NotImplementedException();
		}
	}
}
