# .NET-Microservices

# Create new Web API project:

dotnet new webapi -n PlatformService
dotnet new webapi -n CommandsService

dotnet build
dotnet run

# Add packages to PlatformService:

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package RabbitMQ.Client
dotnet add package Grpc.AspNetCore

# Add packages to CommandsService:

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Grpc.Tools
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf

# Add migrations:

dotnet ef migrations add initialmigration

# Docker commands:

docker build -t konstantinkirchev/platformservice .
docker run -p 8080:80 -d konstantinkirchev/platformservice - run a new instance of the docker container
docker ps - shows running containers
docker stop <container ID> - stops running container
docker start <container ID> - starts existing container
docker push <your docker hub ID>/platformservice - uploads the image

# Kubernetes commands:

kubectl version --short
kubectl apply -f platforms-depl.yaml
kubectl get deployments
kubectl get pods
kubectl delete deployment platforms-depl
kubectl describe pod platforms-depl
kubectl get services
kubectl rollout restart deployment platforms-depl
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.5.1/deploy/static/provider/cloud/deploy.yaml
kubectl get namespace
kubectl get pods --namespace=ingress-nginx
kubectl get storageclass
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!







