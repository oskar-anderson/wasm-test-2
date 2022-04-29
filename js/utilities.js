window.setTitle = (title) => {
    document.title = title + " - BlazorGames";
}

window.SetFocusToElement = (element) => {
    element.focus();
};

window.PlayAudio = (elementName) => {
    document.getElementById(elementName).play();
}

window.PauseAudio = (elementName) => {
    document.getElementById(elementName).pause();
}

window.WriteCookie = (name, value, days) => {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

window.ReadCookie = (cname) => {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return 0;
}


window.JsFunctions = {
    addKeyboardEventListener: function () {
        let serializedEvent = (e) => {
            return {
                key: e.key,
                code: e.keyCode.toString(),
                location: e.location,
                repeat: e.repeat,
                ctrlKey: e.ctrlKey,
                shiftKey: e.shiftKey,
                altKey: e.altKey,
                metaKey: e.metaKey,
                type: e.type
            };
        }
        window.document.addEventListener('keydown', (e) => {
            DotNet.invokeMethodAsync('BlazorApp', 'JsKeyDown', serializedEvent(e))
        })
    },
}

console.log("utilities.js loaded");