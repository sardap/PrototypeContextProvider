var interval = null;
var url_string = window.location.href;
var url = new URL(url_string);
var authTokken = url.searchParams.get("auth");
var ready = false;

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

function Check(responseText)
{
    var result = false;

    if(responseText == "1")
    {
        result = true;
    }
    else if(responseText == "0")
    {
        result = false;
    }

    var elem = document.getElementById('datares');
    if(!result)
    {
        elem.style.display = 'none';
    }
    else
    {
        elem.style.display = "";
    }

    ready = true;
}

function SetUpResponse(responseText)
{
    if(interval == null)
    {
        interval = parseInt(responseText);
        
        window.setInterval( function(){
            if(ready)
            {
                var url = "https://localhost:44320/api/values/CheckCon/" + authTokken; 
                httpGetAsync(url, Check)
                ready = false;
            }
          },
        interval)  
    }

    ready = true;
}


function SetUp()
{
    var url = "https://localhost:44320/api/values/CheckCon/GetInterval/" + authTokken; 
    httpGetAsync(url, SetUpResponse);
}

SetUp();