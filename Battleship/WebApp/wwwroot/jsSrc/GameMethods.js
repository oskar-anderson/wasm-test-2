var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import ValidGameSettings from "./model/ValidGameSettings.js";
import GameView from "./model/GameView.js";
export default class GameMethods {
    static CheckGameSettingsValidity(settings) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = window.location.protocol + "//" + window.location.host + "/api/Game/CheckValidGameSettings";
            return yield fetch(url, {
                method: 'POST',
                body: JSON.stringify(settings),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((response) => response.json()).
                then((data) => {
                return ValidGameSettings.mapJsonToObject(data);
            }).catch((err) => {
                throw Error(err);
            });
        });
    }
    static StartGame(settings) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = window.location.protocol + "//" + window.location.host + "/api/Game/StartGame";
            return yield fetch(url, {
                method: 'POST',
                body: JSON.stringify(settings),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((response) => response.json()).
                then((data) => {
                return GameView.mapJsonToObject(data);
            }).catch((err) => {
                throw Error(err);
            });
        });
    }
    static DoGame(gameData) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = window.location.protocol + "//" + window.location.host + "/api/Game/DoGame";
            yield console.log("In DoGame");
            yield console.log(gameData);
            return yield fetch(url, {
                method: 'POST',
                body: JSON.stringify(gameData),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((response) => response.json()).
                then((data) => {
                return GameView.mapJsonToObject(data);
            }).catch((err) => {
                throw Error(err);
            });
        });
    }
}
//# sourceMappingURL=GameMethods.js.map