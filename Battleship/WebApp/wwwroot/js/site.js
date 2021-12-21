// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code. 

// This class is here because I could not figure out how to use js imports in .html files,
// because I do not understand where the .js files are located after compilation.
// I didnt want to duplicate the wwwroot folder by coping its content to the compilation directory.
// wwwroot folder seems handle imports nicely.
class Imports {
     static async importModule(moduleName) {
        console.log("in importModule");
        try {
            let {default: module} = await import(`./${moduleName}.js`);
            return module;
        } catch (error) {
            throw Error(`import failed for module ${moduleName},` + error);
        }


        switch (moduleName) {
            case "GameMethods":
                try {
                    let {default: GameMethods} = await import('./GameMethods.js');
                    return GameMethods;
                } catch (error) {
                    console.error('import failed' + error);
                }
                break;
            case "GameSettings":
                try {
                    let {default: GameSettings} = await import('./GameSettings.js');
                    return GameSettings;
                } catch (error) {
                    console.error('import failed' + error);
                }
                break;
            default:
                console.error("Unknown import moduleName:" + moduleName);
        }
        console.log("load success");
    }

    // Doing only 1 import in 1 function lets Rider IDE provide type support for the return value
    static async importGameSettings() {
        let {default: lib} = await import('./model/GameSettings.js');
        return lib;
    }
    
    static async importGameMethods() {
        let {default: lib} = await import('./GameMethods.js');
        return lib;
    }
}

