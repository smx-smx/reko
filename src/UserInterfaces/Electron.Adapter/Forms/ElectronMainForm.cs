using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reko.Gui.Forms;
using FormWindowState = Reko.Gui.Forms.FormWindowState;

namespace Reko.Gui.Electron.Adapter.Forms
{
	public class ElectronMainForm : IMainForm
	{
		private readonly dynamic jsMainForm;

		private MainFormInteractor interactor;
		private ElectronDecompilerShellUiService uiSvc;

		public ElectronMainForm(dynamic jsMainForm)
		{
			this.jsMainForm = jsMainForm;
		}

		public void Dispose() {
			throw new NotImplementedException();
		}

		public void Attach(
			dynamic jsShellUi,
			IServiceContainer services
		){ 
			//this.interactor = new MainFormInteractor(services);
			this.uiSvc = new ElectronDecompilerShellUiService(jsShellUi, services);
			services.AddService(typeof(IDecompilerShellUiService), this.uiSvc);
		}

		public event EventHandler Closed;
		public event EventHandler Load;
		public string TitleText { get; set; }
		public Size Size { get; set; }
		public FormWindowState WindowState { get; set; }

		public TabPage FindResultsPage {
			get { return null; }
		}

		public TabPage DiagnosticsPage {
			get { return null; }
		}

		public void LayoutMdi(DocumentWindowLayout layout) {
			jsMainForm.LayoutMdi(layout);
		}

		public void Show() {
			jsMainForm.Show(null);
		}

		public void Close() {
			jsMainForm.Close(null);
		}

		public object Invoke(Delegate action, params object[] args) {
			return jsMainForm.Invoke(new {
				action = action,
				args = args
			});
		}

		public void UpdateToolbarState() {
			jsMainForm.UpdateToolbarState(null);
		}
	}
}
