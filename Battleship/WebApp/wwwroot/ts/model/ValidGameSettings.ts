export default class ValidGameSettings {
    areSettingsValid: boolean;
    errorMessage: string;

    constructor(
        areSettingsValid: boolean | undefined, 
        errorMessage: string | undefined) {
        if (areSettingsValid === undefined || errorMessage === undefined) {
            throw Error("Object has undefined fields!");
        }
        this.areSettingsValid = areSettingsValid;
        this.errorMessage = errorMessage;
    }
    
    public static mapJsonToObject(json: string): ValidGameSettings {
        let obj = JSON.parse(json);
        return new ValidGameSettings(obj.AreSettingsValid, obj.ErrorMessage);
    }
}