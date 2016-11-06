##



## Running DB Migration
cd Silverscreen.Core

`dotnet ef --startup-project ../Silverscreen.API migrations add Initial`

`dotnet ef --startup-project ../Silverscreen.API database update`
