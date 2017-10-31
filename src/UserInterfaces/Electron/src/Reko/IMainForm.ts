interface IMainForm {
	Closed: Function,
	Load: Function,

	TitleText: string,
	Size: {
		Width: number,
		Height: number
	},

	WindowState: any,

	LayoutMdi(layout:any): void;
	Show(): void;
	Close(): void;
	
	Invoke(action: () => void, args: any[]): any;
	UpdateToolbarState(): void;

}