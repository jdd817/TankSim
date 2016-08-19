$.url = function (url) {
    var root = location.protocol + "//" + location.host;
    var path = application_path;
    var rand = "";

    if (path.charAt(path.length - 1) != "/") {
        path = path + "/";
    }

    if (typeof areaInfo != "undefined" && areaInfo != null && areaInfo.name != null) {
        path = path + areaInfo.name;
    }

    if (url.indexOf(".htm") !== -1) {
        if (url.indexOf("?") === -1)
            rand = "?rand=" + Math.random();
        else
            rand = "&rand=" + Math.random();
    }

    return root + path + url + rand;
};