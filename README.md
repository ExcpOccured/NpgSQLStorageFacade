NpgSQLStorageFacade [![Status Zero][status-zero]][andivionian-status-classifier]
==========

NpgSQLStorageFacade is a Getting Things Done (GTD) framework that 
which contains a set of convenient tools for working with the context of transactions, commits e.t.c

**⚠ Currently the project is in the develop state, nothing is ready yet.**

Status
-------------

[![NuGet]()]() - Not deployed

Documentation
-------------

1. [Development Process][docs.1.development-process]
2. [Configuration][docs.2.configuration]

Prerequisites
-------------

NpgSQLStorageFacade is a .NET 5.0 library. To use it on your own, [.NET 5.0
SDK][dotnet.download] is required.

Build
-----

To build the library, tests and the example, run the following
command in the terminal:

```console
$ dotnet build --configuration Release
```

Configure
---------

By default, NpgSQLStorageFacade will use the `appsettings.json` file found in the same
directory as the entry point file. See more information
in [the configuration documentation][docs.2.configuration].

Test
----

To execute the automatic test suite, run the following command in the terminal:

```console
$ dotnet test --configuration Release
```


[status-zero]: https://img.shields.io/badge/status-zero-lightgrey.svg

[docs.1.development-process]: docs/1.development-process.md
[docs.2.configuration]: docs/2.configuration.md
[dotnet.download]: https://dotnet.microsoft.com/download
