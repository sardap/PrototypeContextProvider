function httpGetAsync(theUrl, callback)
{
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function() { 
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", theUrl, true); // true for asynchronous 
    xmlHttp.send(null);
}

function httpGotten(responseText)
{
    if(responseText == 1)
    {
        alert("Sucess you get access to this resouce");
    }
    else
    {
        alert("You are denied");
    }
}

httpGetAsync("", httpGotten);