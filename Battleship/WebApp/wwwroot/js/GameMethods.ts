import ValidGameSettings from "./model/ValidGameSettings.js";
import GameSettings from "./model/GameSettings.js";
import GameView from "./model/GameView.js";

export default class GameMethods {
    
    static async CheckGameSettingsValidity(settings: GameSettings): Promise<ValidGameSettings> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/CheckValidGameSettings";
        return await fetch(url, {
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
    }

    static async StartGame(settings: ValidGameSettings): Promise<GameView> {
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

    static async DoGame(gameData: object): Promise<GameView> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/DoGame";
        await console.log("In DoGame");
        await console.log(gameData);
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