export default class ValidGameSettings {
    constructor(areSettingsValid, errorMessage) {
        if (areSettingsValid === undefined || errorMessage === undefined) {
            throw Error("Object has undefined fields!");
        }
        this.areSettingsValid = areSettingsValid;
        this.errorMessage = errorMessage;
    }
    static mapJsonToObject(json) {
        let obj = JSON.parse(json);
        return new ValidGameSettings(obj.AreSettingsValid, obj.ErrorMessage);
    }
}
//# sourceMappingURL=ValidGameSettings.js.map