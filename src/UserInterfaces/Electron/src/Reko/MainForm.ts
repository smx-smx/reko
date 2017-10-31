import Helpers from './Helpers';

export default class MainForm implements IMainForm, IJsonClassProvider {
	Closed: Function;
	Load: Function;
	TitleText: string;
	Size: { Width: number; Height: number; };
	WindowState: any;
	LayoutMdi(layout: any): void {
		throw new Error("Method not implemented.");
	}
	Show(): void {
		throw new Error("Method not implemented.");
	}
	Close(): void {
		throw new Error("Method not implemented.");
	}
	Invoke(action: () => void, args: any[]) {
		throw new Error("Method not implemented.");
	}
	UpdateToolbarState(): void {
		throw new Error("Method not implemented.");
	}

	public GetClassAsJSON(): object {
		return Helpers.GetClassAsJSON(this);
	}
}