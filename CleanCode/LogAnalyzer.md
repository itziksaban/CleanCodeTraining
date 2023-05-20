A company has to build an application that will analyze apache logs and will generate the following reports:

#### Countries distribution:  
Us: 37.3%  
Italy: 11.2%  
Germany: 31%  


#### Browsers distribution:  
Chrome: 57%  
Edge: 20%  
Firefox: 12%  


Suggest a design (including class diagrams) that will allow that company to:

* Support different input sources (system file, azure blob, amazon s3, etc)
* Add new types of reports easily
* Support different output formats (currently csv, but can be json, html and others)

#### Example of apache logs
94.23.71.20 - - [21/Jan/2013:01:23:23 -0600] "GET / HTTP/1.0" 200 9983 "-" "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.3 Safari/533.19.4" 232 10198 - 129236  

94.23.71.20 - - [21/Jan/2013:01:53:10 -0600] "GET / HTTP/1.0" 200 9983 "-" "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_8; en-US) AppleWebKit/533.21.1 (KHTML, like Gecko) Version/5.0.5 Safari/533.21.1" 240 10198 - 127803
