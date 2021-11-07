export default class GameMethods {

    constructor() {
        
    }
    
    static async CheckGameSettingsValidity(settings) {
        let url = "https://" + window.location.host + "/api/Game/CheckValidGameSettings";
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(settings),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then((response) => response.json()).
            then((data) => {
                console.log(data);
                return data;
            }).catch((err) => {
                console.error(err);
            });
    }

    static async StartGame(settings) {
        let url = "https://" + window.location.host + "/api/Game/StartGame";
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(settings),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then((response) => response.json()).
            then((data) => {
                console.log(data);
                data = JSON.parse(data);
                console.log(data);
                return data;
            }).catch((err) => {
                console.error(err);
            });
    }

    static async DoGame(gameData) {
        let url = "https://" + window.location.host + "/api/Game/DoGame";
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
                console.log(data);
                return data;
            }).catch((err) => {
                console.error(err);
            });
    }
}