# Shell Assist

## Publish Command

### Windows
`dotnet publish -o ./dist/win --runtime win10-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`

### OSX
`dotnet publish -o ./dist/osx --runtime osx-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`