//export
    class StoreCustomer {

    public visits: number = 0;
    private _name: string;

    constructor(private firstName: string, private lastName: string) { }

    public showName() {
        alert(this.firstName + " " + this.lastName);
    }

    public returningFunction(param:string) : string {
        return param;
    }

    set name(val: string) { this._name = val; }
    get name() { return this._name; }

}