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

var url = "https://localhost:44320/api/values/7080546380500131593/D4C11AD0ACA0D1E2D350C5C04B905389F368BAEE22A0F44D8F147FAAB5AA457265185ABF7280804C8A164CC66C3925E6752CFCE84488F316941CF5C438EA8540EA16C1E57E54EFBB168C93B5B6507B723A83FE9E970DF44E23EF98D2960B754B61BD9C189AE18C9DF0508F3304F67DB65D034D1AB3AF34C21C2DA67C44481CBF";
httpGetAsync(url, httpGotten);