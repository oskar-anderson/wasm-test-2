import AreGameSettingsValid from "./model/AreGameSettingsValid.js";
import GameSettings from "./model/GameSettings.js";
import GameView from "./model/GameView.js";

export default class ServerRestApiGameMethods {
    
    public static async CheckGameSettingsValidity(settings: GameSettings): Promise<AreGameSettingsValid> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/CheckValidGameSettings";
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(settings),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then((response) => response.json()).
            then((data) => {
                return AreGameSettingsValid.mapJsonToObject(data);
            }).catch((err) => {
                throw Error(err);
            });
    }

    public static async StartGame(settings: GameSettings): Promise<GameView> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/StartGame";
        return await fetch(url, {
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
    }

    public static async DoGame(gameData: object): Promise<GameView> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/DoGame";
        return await fetch(url, {
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
    }
}