# ETL TOOL

## How to use
> arguments

--path  (required) path to the file.

--entity  (required) specifies entities in the provided file e.g. customer || task

--enc (optional) specifies if the encryption algorithm of the data in the file. e.g. base64

--f (optional) specifies the format of the provided file e.g. csv

```console
foo@bar:~$ EtlTool.exe --path C:\\customer.csv --entity customer --enc base64 -f csv
```

## Tasks
1. **ETL Tool** - create a simple console application that will extract initial data from files of
the given format, transform it to MySQL format and load to MySQL database.

1. **Backend** - build a RESTful Web Service (API) that allows CRUD operations on the
records in the database and provide access for the user to system functionality.

1. **Frontend** - build a user interface that will provide users the access to the software
features.

 ## Technologies
- .NET Core 3.1 with C# to build ETL and backend.
- Entity Framework Core or Dapper should be used as ORM.
- MySQL as a database.
- Vue.js should be used as a frontend framework.
- GitHub should be used as a project repository
