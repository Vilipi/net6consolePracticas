FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

ENV env_saludo Hola
WORKDIR /App
COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /Application
COPY --from=build-env /App/out .
#ENTRYPOINT ["/bin/bash"]
ENTRYPOINT ["dotnet", "aa1.dll"]


