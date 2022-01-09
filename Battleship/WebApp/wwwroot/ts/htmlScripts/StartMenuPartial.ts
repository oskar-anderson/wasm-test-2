import Rendering from "../Rendering.js";

export default class StartMenuPartial {
    public static main(rendering: Rendering): void {
        document.getElementById("newGameBtn")?.addEventListener(
            "click", 
            () => rendering.renderByName('NewGame')
        );
    }
}