dotnet publish -c Release -o publish
docker build . --tag ariyana/microservices:backend-%1 
docker push ariyana/microservices:backend-%1
PAUSE