import GameViewController from "./GameViewController.js";
import StartMenuPartial from "./htmlScripts/StartMenuPartial.js";
import NewGamePartial from "./htmlScripts/NewGamePartial.js";

export default class Rendering {
    
    public static render(template: string, data = {}, targetElementId: string) {
        // replace marked variables with values
        let rendered = "";
        try {
            // @ts-ignore
            rendered = raz.render(template, data);
        } catch (e) {
            throw Error(`Raz rendering module is not available or failed to render! Error: ${e}`);
        }
        let elTarget = document.getElementById(targetElementId);
        if (elTarget === null) throw Error("No Element found to attach content!");
        elTarget.innerHTML = rendered;
    }

    public static async renderByName(name: string, model = {}, targetElementId = "mainBody") {
        let gameView = await fetch('html/GameView.html').then(x => x.text());
        let newGamePartial = await fetch('html/NewGamePartial.html').then(x => x.text());
        let startMenuPartial = await fetch('html/StartMenuPartial.html').then(x => x.text());
        switch (name) {
            case "GameView":
                console.log("in index.cshtml renderByName GameView");
                console.log(model);
                Rendering.render(gameView, model, targetElementId);
                break;
            case "GameViewController":
                console.log("in index.cshtml renderByName GameViewController");
                console.log(model);
                await GameViewController.RunGame(model);
                break;
            case "NewGame":
                Rendering.render(newGamePartial, {}, targetElementId);
                NewGamePartial.main();
                break;
            case "StartMenu":
                Rendering.render(startMenuPartial, {}, targetElementId);
                StartMenuPartial.main();
                break;
            default:
                throw Error("Invalid render name");
    
        }
    }
}