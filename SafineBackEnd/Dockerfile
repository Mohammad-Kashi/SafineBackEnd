FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY /publish .
EXPOSE 5135
ENTRYPOINT [ "dotnet", "SafineBackEnd.dll" ]