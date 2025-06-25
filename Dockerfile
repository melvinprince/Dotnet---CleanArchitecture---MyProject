# Build stage -------------------------------------
    FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
    WORKDIR /src
    
    # Install Node.js for Angular build
    RUN apt-get update && \
        apt-get install -y curl && \
        curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
        apt-get install -y nodejs && \
        apt-get clean && rm -rf /var/lib/apt/lists/*
    
    # Copy everything
    COPY . .
    
    # Restore and publish
    RUN dotnet restore src/Web/Web.csproj
    RUN dotnet publish src/Web/Web.csproj -c Release -o /app/publish
    
    # Runtime stage -----------------------------------
    FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
    WORKDIR /app
    COPY --from=build /app/publish .
    ENV ASPNETCORE_URLS=http://+:8080
    ENTRYPOINT ["dotnet", "Web.dll"]
    