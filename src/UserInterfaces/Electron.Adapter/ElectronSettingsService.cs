using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Reko.Gui.Electron.Adapter
{
	public class ElectronSettingsService : ISettingsService
	{
		private ServiceContainer services;

		public ElectronSettingsService(ServiceContainer services)
		{
			this.services = services;
		}

		public object Get(string settingName, object defaultValue) {
			throw new System.NotImplementedException();
		}

		public string[] GetList(string settingName) {
			throw new System.NotImplementedException();
		}

		public void SetList(string name, IEnumerable<string> values) {
			throw new System.NotImplementedException();
		}

		public void Set(string name, object value) {
			throw new System.NotImplementedException();
		}

		public void Delete(string name) {
			throw new System.NotImplementedException();
		}

		public void Load() {
			throw new System.NotImplementedException();
		}

		public void Save() {
			throw new System.NotImplementedException();
		}
	}
}