interface IShellUiService {
	ShowError(ex:Error, format:string, args:string[]):void;
	ShowOpenFileDialog(fileName:string):string;
	ShowSaveFileDialog(fileName:string):string;
	Prompt(prompt:string):boolean;
	ShowMessage(msg:string):void;
	QueryStatus(cmdId:object, status:object, text:string):boolean;
	Execute(cmdId:object):boolean;

	ActiveFrame:object;
	DocumentWindows:object[];
	ToolWindows:object[];

	CreateWindow(windowType:string, windowTitle:string, pane: object): object;
	CreateDocumentWindow(windowType:string) : object;
	FindWindow(windowType: string): object;
	FindDocumentWindow(documentType: string, docItem: object) : object;
	SetContextMenu(control:object, menuID:number):void;
	ShowModalDialog(dlg:object): number;
	WithWaitCursor(p:() => void):void;
}