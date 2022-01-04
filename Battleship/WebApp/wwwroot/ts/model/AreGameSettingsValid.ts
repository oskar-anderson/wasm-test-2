export default class AreGameSettingsValid {
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
    
    public static mapJsonToObject(json: string): AreGameSettingsValid {
        let obj = JSON.parse(json);
        return new AreGameSettingsValid(obj.AreSettingsValid, obj.ErrorMessage);
    }
}