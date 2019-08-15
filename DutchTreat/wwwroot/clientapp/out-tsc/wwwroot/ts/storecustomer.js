//export
class StoreCustomer {
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 0;
    }
    showName() {
        alert(this.firstName + " " + this.lastName);
    }
    returningFunction(param) {
        return param;
    }
    set name(val) { this._name = val; }
    get name() { return this._name; }
}
//# sourceMappingURL=storecustomer.js.map