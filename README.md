# SQL Mailer #

**SQL Mailer** enables SQL Server to send emails through CLR assembly.


## Getting Started ##

Before installing **SQL Mailer** as a CLR assembly, it's worth reading this article, [Send Email from SQL Server Express Using a CLR Stored Procedure](http://www.mssqltips.com/sqlservertip/1795/send-email-from-sql-server-express-using-a-clr-stored-procedure/).

Once you get confident with this, run the following script.

```sql
-- Assumes that the CLR assembly is located at 'C:\Temp\Aliencube.SqlMailer.Clr'.
CREATE ASSEMBLY [SqlMailer]
    FROM 'C:\Temp\Aliencube.SqlMailer.Clr\Aliencube.SqlMailer.Clr.dll'   
    WITH PERMISSION_SET = EXTERNAL_ACCESS   
GO

CREATE PROCEDURE [dbo].[usp_SendMail]
    @applicationName    AS NVARCHAR(256),
    @body               AS NVARCHAR(MAX)
AS 
EXTERNAL NAME [SqlMailer].[Aliencube.SqlMailer.Clr.SqlMailer].[SendMail]
GO
```

If you see an error related to `System.Net.Mail`, the following script should be run to allow it.

```sql
-- Assumes that the database to create the CLR assembly is 'MyDatabase'.
USE [master]
GO

ALTER DATABASE [MyDatabase]
    SET TRUSTWORHTY ON
    WITH ROLLBACK IMMEDIATE
GO
```

It might not be possible, unless your account has a system administrator privilege. If this is the case, the following script might help.

```sql
-- Assumes that the database owner is 'aliencube'.
EXEC sp_changedbowner 'aliencube'
```

Then create the stored procedure again. Once the stored procedure is created, try the following script for test.

```sql
-- To success sending an email.
EXEC usp_SendMail 'Application Name', 'body'

-- To fail sending an email.
EXEC usp_SendMail 'Application Name', ''
```


## Configuration ##

**SQL Mailer** is highly configurable. With `SqlMail.config`, you can change settings as many times as you can.

```xml
<configuration>
  <smtp>
    <enableSsl>false</enableSsl>
    <host>localhost</host>
    <port>25</port>
    <defaultCredentials>true</defaultCredentials>
  </smtp>

  <applications>
    <application>
      <name>Application Name</name>
      <sender>from@emailserver</sender>
      <recipients>
        <recipient>to@emailserver</recipient>
      </recipients>
      <carbonCopies>
        <carbonCopy>cc@emailserver</carbonCopy>
      </carbonCopies>
      <blindCarbonCopies>
        <blindCarbonCopy>bcc@emailserver</blindCarbonCopy>
      </blindCarbonCopies>
      <subject>Email Subject</subject>
      <isBodyHtml>true</isBodyHtml>
    </application>
  </applications>
</configuration>
```


### `smtp` Section ###

* `enableSsl`: (Optional) Value that specifies whether to enable SSL connection or not. Default value is `false`.
* `host`: (Optional) SMTP server name. Default value is `localhost`.
* `port`: (Optional) SMTP server port. Default value is `25`.
* `defaultCredentials`: (Optional) Value that specifies whether to use default credentials or not. Default value is `true`.


### `application` Section ###

* `name`: Name of the application for `SqlMailer` to identify message settings. This is called within a stored procedure.
* `sender`: Sender's email address.
* `recipients`: List of recipients' email addresses.
* `carbonCopies`: (Optional) List of carbon-copied recipients' email addresses.
* `blindCarbonCopies`: (Optional) List of blind carbon-copied recipients' email addresses.
* `subject`: Email subject.
* `isBodyHtml`: (Optional) Value that specifies whether to send emails in HTML format or not. Default value is `true`.


## Testing ##

If necessary, [Papercut](http://papercut.codeplex.com/) might be useful for testing. [Papercut](http://papercut.codeplex.com/) is a standalone application resembling a dummy SMTP/POP server so it doesn't need installation.


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work, please send us a pull request onto our `dev` branch for review.


## License ##

**SQL Mailer** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2014 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
