FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FileUploader.Service.Transaction/FileUploader.Service.Transaction.csproj", "FileUploader.Service.Transaction/"]
RUN dotnet restore "FileUploader.Service.Transaction/FileUploader.Service.Transaction.csproj"
COPY . .
WORKDIR "/src/FileUploader.Service.Transaction"
RUN dotnet build "FileUploader.Service.Transaction.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FileUploader.Service.Transaction.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FileUploader.Service.Transaction.dll"]