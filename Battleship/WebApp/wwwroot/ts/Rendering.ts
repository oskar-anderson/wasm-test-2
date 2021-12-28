import GameViewController from "./GameViewController.js";

export default class Rendering {
    
    public static render(template: string, data = {}, targetElementId: string) {
        // console.log(template);
    
        // decode html entities
        console.log(template);
        // template = new DOMParser().parseFromString(template, "text/html").documentElement.textContent ?? "";
        console.log(template);
    
        // replace marked variables with values
        let rendered = "";
        try {
            // @ts-ignore
            rendered = raz.render(template, data);
        } catch (e) {
            throw Error(`Raz rendering module is not available or failed to render! Error: ${e}`);
        }
        
        // let rendered = template;
        // console.log(rendered);
        // console.log("before parsePartialHtml");
    
        let frag = Rendering.parsePartialHtml(rendered);
        Rendering.fixParsedScriptsToExecute(frag);
        // console.log("after fixParsedScriptsToExecute(frag);");
        let target = document.getElementById(targetElementId);
        if (target === null) { throw Error(`Target element with id ${targetElementId}`); }
        target.innerHTML = "";
        target.appendChild(frag);
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
                break;
            case "StartMenu":
                Rendering.render(startMenuPartial, {}, targetElementId);
                break;
            default:
                console.error("Invalid render name");
    
        }
    }
    
    /**
     *
     * @@param { string } html
     */
    public static parsePartialHtml(html: string) {
        let doc = new DOMParser().parseFromString(html, "text/html");
        let frag = document.createDocumentFragment();
    
        if (doc.childNodes.length !== 0) {
            frag.appendChild(doc.childNodes[0]);
        } else {
            console.error('unexpected! doc.childNodes.length is ' + doc.childNodes.length);
        }
        return frag;
    }


    /**
     *
     * @@param { DocumentFragment } frag
     */
    public static fixParsedScriptsToExecute(frag: DocumentFragment) {
        let scripts = frag.querySelectorAll('script');
    
        for (let i = 0; i < scripts.length; i++) {
            let script = scripts[i];
            let fixedScript = document.createElement('script');
            fixedScript.type = script.type;
            fixedScript.innerHTML = script.innerHTML;
    
            script.parentNode?.replaceChild(fixedScript, script);
        }
    }
}