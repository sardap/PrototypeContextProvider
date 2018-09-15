http = require('http');
http.createServer(function (request, response) {
    request.addListener('end', function () {
        var buf = '<div style=\"color:green;\">' + '2' + ' is prime.  </div>';
        for (var i = 2; i < 500; i++) 
        {
            for (var j = 2; j < i; j++) 
            {
                if (i % j == 0) 
                {
                    var prime = 0;
                    var tmp = '<div style=\"color:red;\">' + i + 
                                ' is not prime.  It is divisible by ' + j + '</div>';
                    buf += tmp;
                    break;
                }
                else
                    var prime = 1;
            }
            if (prime == 1) 
            {
                var tmp = '<div style=\"color:green;\">' + i + ' is prime.  </div>';
                buf += tmp;
            }
        }
        response.write('<html><body><p>' + buf + '</p></body></html>');
        response.end();
    });
}).listen(8000, 'localhost');
console.log('system waiting at http://localhost:8000');
