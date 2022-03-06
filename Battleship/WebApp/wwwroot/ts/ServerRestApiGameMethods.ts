import AreGameSettingsValid from "./model/AreGameSettingsValid.js";
import GameSettings from "./model/GameSettings.js";
import GameView from "./model/GameView.js";
import GameData from "./model/GameData.js";
import GameView_v1 from "./model/GameView_v1.js";
import GameView_v2 from "./model/GameView_v2.js";

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

    public static async DoGame(gameData: GameData): Promise<GameView> {
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

    public static async DoGame_v1(gameData: GameData): Promise<GameView_v1> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/DoGame_v1";
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(gameData),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then((response) => response.json()).
        then((data) => {
            return GameView_v1.mapJsonToObject(data);
        }).catch((err) => {
            throw Error(err);
        });
    }

    public static async DoGame_v2(gameData: GameData): Promise<GameView_v2> {
        let url = window.location.protocol + "//" + window.location.host + "/api/Game/DoGame_v2";
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(gameData),
            headers: {
                'Content-Type': 'application/json',
            }
        }).then((response) => response.json()).
        then((data) => {
            return GameView_v2.mapJsonToObject(data);
        }).catch((err) => {
            throw Error(err);
        });
    }
}