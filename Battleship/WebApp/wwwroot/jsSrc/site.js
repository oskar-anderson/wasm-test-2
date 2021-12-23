"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class Imports {
    static importModule(moduleName) {
        return __awaiter(this, void 0, void 0, function* () {
            console.log("in importModule");
            try {
                let { default: module } = yield import(`./${moduleName}.js`);
                return module;
            }
            catch (error) {
                throw Error(`import failed for module ${moduleName},` + error);
            }
            switch (moduleName) {
                case "GameMethods":
                    try {
                        let { default: GameMethods } = yield import('./GameMethods.js');
                        return GameMethods;
                    }
                    catch (error) {
                        console.error('import failed' + error);
                    }
                    break;
                case "GameSettings":
                    try {
                        let { default: GameSettings } = yield import('./GameSettings.js');
                        return GameSettings;
                    }
                    catch (error) {
                        console.error('import failed' + error);
                    }
                    break;
                default:
                    console.error("Unknown import moduleName:" + moduleName);
            }
            console.log("load success");
        });
    }
    static importGameSettings() {
        return __awaiter(this, void 0, void 0, function* () {
            let { default: lib } = yield import('../jsSrc/model/GameSettings.js');
            return lib;
        });
    }
    static importGameMethods() {
        return __awaiter(this, void 0, void 0, function* () {
            console.log("in importGameMethods");
            let { default: lib } = yield import('../jsSrc/GameMethods.js');
            return lib;
        });
    }
}
//# sourceMappingURL=site.js.map