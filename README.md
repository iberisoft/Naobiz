# Naobiz

![.NET Core](https://github.com/iberisoft/Naobiz/workflows/.NET%20Core/badge.svg)

E-commerce platform for Spain.

## Configuration

The tool is configured in file `appsettings.json`.

### Database

The `DbConnection` setting should be `Server=(local)\\SQLEXPRESS;Database=Naobiz;Trusted_Connection=True;MultipleActiveResultSets=true`.

### Emails

The following settings are used to configure emails distributing to the users:

Setting       | Default Value          | Description
--------------|------------------------|------------
SiteUrl       | http://localhost:5000/ | Site reference
SiteEmail     | negocios@naobiz.com    | Email address

The SMTP server is defined in the `Smtp` setting:

Setting       | Default Value          | Description
--------------|------------------------|------------
Host          | c25702.sgvps.net       | Server host
Port          | 587                    | Server port
User          | negocios@naobiz.com    | Username to access the server
Password      | NaveNegocios19         | User password

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
