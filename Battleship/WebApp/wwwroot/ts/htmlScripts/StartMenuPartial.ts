import Rendering from "../Rendering.js";

export default class StartMenuPartial {
    public static main(): void {
        document.getElementById("newGameBtn")?.addEventListener(
            "click", 
            () => Rendering.renderByName('NewGame')
        );
    }
}