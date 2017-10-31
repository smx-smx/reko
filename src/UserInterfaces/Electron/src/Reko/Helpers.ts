export default class Helpers {
	public static GetClassAsJSON(handle:object): object {
		return Object.getOwnPropertyNames(Object.getPrototypeOf(handle)).reduce((a:any, b:string) => {
			a[b] = (<any>handle)[b];
			return a;
		}, {});
	}
}