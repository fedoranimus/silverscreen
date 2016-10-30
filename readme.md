##



## Running DB Migration
cd Silverscreen.Model
`dotnet ef --startup-project ../Silverscreen.Core migrations add Initial`
`dotnet ef --startup-project ../Silverscreen.Core database update`
