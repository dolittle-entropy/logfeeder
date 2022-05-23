FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0 AS build
SHELL ["/bin/bash", "-c"]
ARG VERSION
ARG TARGETARCH

COPY LogFeeder.sln /build/
COPY Source /build/Source/

WORKDIR /build/Source/Command
RUN dotnet restore
RUN dotnet publish -c "Release" -p:Version=${VERSION} -p:RuntimeIdentifier="linux-${TARGETARCH/amd64/x64}" -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
SHELL ["/bin/bash", "-c"]

COPY --from=build /build/Source/Command/out /app

WORKDIR /app
ENTRYPOINT ["dotnet", "Command.dll"]