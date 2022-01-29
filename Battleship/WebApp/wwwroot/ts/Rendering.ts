import GameViewController from "./GameViewController.js";
import StartMenuPartial from "./htmlScripts/StartMenuPartial.js";
import NewGamePartial from "./htmlScripts/NewGamePartial.js";
import GameData from "./model/GameData.js";
import GameViewPartial from "./htmlScripts/GameViewPartial.js";

export default class Rendering {
    
    public GameView = "Error";
    public NewGamePartial = "Error";
    public StartMenuPartial = "Error";
    
    constructor() {
        // do not forget to call init   
    }

    public async init(): Promise<Rendering> {
        this.GameView = await fetch('html/GameView.html').then(x => x.text());
        this.NewGamePartial = await fetch('html/NewGamePartial.html').then(x => x.text());
        this.StartMenuPartial = await fetch('html/StartMenuPartial.html').then(x => x.text());
        return this;
    }
    
    public render(template: string, data = {}, targetElementId: string) {
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

    public renderByName(name: string, model = {}, targetElementId = "mainBody") {
        switch (name) {
            case "GameView":
                this.render(this.GameView, model, targetElementId);
                // @ts-ignore
                GameViewPartial.draw(model.Board, 320,320);
                break;
            case "GameViewController":
                (new GameViewController(<GameData> model, this)).runGame();
                break;
            case "NewGame":
                this.render(this.NewGamePartial, {}, targetElementId);
                NewGamePartial.main(this);
                break;
            case "StartMenu":
                this.render(this.StartMenuPartial, {}, targetElementId);
                StartMenuPartial.main(this);
                break;
            default:
                throw Error("Invalid render name");
    
        }
    }
}