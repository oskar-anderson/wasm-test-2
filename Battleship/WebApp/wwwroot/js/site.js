// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code. 

async function importModule(moduleName) {
    console.log("in importModule");
    switch (moduleName) {
        case "GameMethods":
            try {
                let {default: GameMethods} = await import('./GameMethods.js');
                return GameMethods;
            } catch (error) {
                console.error('import failed' + error);
            }
            break;
        default:
            console.error("Unknown import moduleName:" + moduleName);
    }
    console.log("load success");
}
