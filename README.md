This project is meant to deminstrate a bug introduced in Newtonsoft.Json v9.0.1.

Installing Newtonsoft.Json v8.0.3 using 
```
Install-Package Newtonsoft.Json -Version 8.0.3
```
will cause the Unit Test to pass.

Installing v9.0.1
```
Uninstall-Package Newtonsoft.Json
Install-Package Newtonsoft.Json -Version 9.0.1
```
will then cause the test to fail.


It appears that serializing a dictionary in 9.0.1 causes it to ignore the custom Contract Resolver that was specified.