import {dialog} from 'electron';
import Helpers from './Helpers';

export default class ShellUiService implements IShellUiService, IJsonClassProvider {
	ShowError(ex: Error, format: string, args: string[]): void {
		throw new Error("Method not implemented.");
	}
	ShowOpenFileDialog(fileName: string): string {
		throw new Error("Method not implemented.");
	}
	ShowSaveFileDialog(fileName: string): string {
		throw new Error("Method not implemented.");
	}
	Prompt(prompt: string): boolean {
		throw new Error("Method not implemented.");
	}
	ShowMessage(msg: string): void {
		dialog.showMessageBox({
			message: msg
		});
	}
	QueryStatus(cmdId: object, status: object, text: string): boolean {
		throw new Error("Method not implemented.");
	}
	Execute(cmdId: object): boolean {
		throw new Error("Method not implemented.");
	}
	ActiveFrame: object;
	DocumentWindows: object[];
	ToolWindows: object[];
	CreateWindow(windowType: string, windowTitle: string, pane: object): object {
		throw new Error("Method not implemented.");
	}
	CreateDocumentWindow(windowType: string): object {
		throw new Error("Method not implemented.");
	}
	FindWindow(windowType: string): object {
		throw new Error("Method not implemented.");
	}
	FindDocumentWindow(documentType: string, docItem: object): object {
		throw new Error("Method not implemented.");
	}
	SetContextMenu(control: object, menuID: number): void {
		throw new Error("Method not implemented.");
	}
	ShowModalDialog(dlg: object): number {
		throw new Error("Method not implemented.");
	}
	WithWaitCursor(p: () => void): void {
		throw new Error("Method not implemented.");
	}

	public GetClassAsJSON(): object {
		return Helpers.GetClassAsJSON(this);
	}
}