# Naobiz

![.NET Core](https://github.com/iberisoft/Naobiz/workflows/.NET%20Core/badge.svg)

E-commerce platform for Spain.

## Installation

### Prerequisites

* Windows 7+ 64 bits, Windows Server 2012 R2+
* SQL Server 2008+

The latest freeware version of SQL Server can download [here](https://go.microsoft.com/fwlink/?linkid=866658).

### Running Naobiz

Naobiz can simply run as any console application.

### Making Naobiz as Windows Service

Install Naobiz as a service under the local system account by running:
```
Naobiz install --localsystem
```

Then start the service in Windows Services Manager or by running:
```
Naobiz start
```

It can stop in Windows Services Manager or by using `stop` command when running `Naobiz`. It can uninstall by using
`uninstall` command. See more options by using `help` command.

## Configuration

The tool is configured in file `appsettings.json`.

### Database

The `DbConnection` setting should be `Server=(local)\\SQLEXPRESS;Database=Naobiz;Trusted_Connection=True;MultipleActiveResultSets=true` and
the `DependeeServiceName` setting should be `MSSQL$SQLEXPRESS`.

### Emails

The following settings are used to configure emails distributing to the users:

Setting       | Default Value               | Description
--------------|-----------------------------|------------
SupportUrl    | http://naobiz.com/#contacta | Support page reference
SiteUrl       | http://localhost:5000/      | Site reference
SiteEmail     | negocios@naobiz.com         | Email address

The SMTP server is defined in the `Smtp` setting:

Setting       | Default Value             | Description
--------------|---------------------------|------------
Host          | smtp.eu.mailgun.org       | Server host
Port          | 587                       | Server port
User          | postmaster@app.naobiz.com | Username to access the server
Password      |                           | User password

### Dashboard

The `Dashboard` setting consist of sections, each section includes the following settings:

Setting       | Description
--------------|------------
Name          | Icon title
LinkUrl       | Reference to the corresponding page
ImageSource   | Relative or absolute path to the icon image

## How to Get SSL Certificate

### Prepare Certificate Signing Request

Create a private key and CSR:
```
openssl req -new -newkey rsa:2048 -nodes -keyout private.pem -out csr.pem
```

Send `csr.pem` to an SSL provider and keep `private.pem` for the next step.

### Convert Certificate to PFX Format

Convert the p7b file provided by the SSL provider to a cer file:
```
openssl pkcs7 -print_certs -in app_naobiz_com.p7b -out app_naobiz_com.cer
```

Convert the cer file to a pfx file:
```
openssl pkcs12 -export -in app_naobiz_com.cer -inkey private.pem -out app_naobiz_com.pfx
```
