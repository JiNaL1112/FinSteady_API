

# ---------------------------------------------------------------------------
# Stage 1: build
# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the files restore needs first, so Docker's layer cache is reused
# on every subsequent build unless a .csproj/.sln/lock file actually changed.
COPY FinSteady_API.sln ./
COPY FinSteady_API.csproj ./
COPY packages.lock.json ./
# If/when the test project has its own lock file, copy it here too, e.g.:
# COPY FinSteady_API.Tests/FinSteady_API.Tests.csproj ./FinSteady_API.Tests/
# COPY FinSteady_API.Tests/packages.lock.json ./FinSteady_API.Tests/

# --locked-mode fails the build immediately if packages.lock.json and the
# .csproj files have drifted apart, instead of silently re-resolving to
# different (possibly vulnerable) versions inside the image.
RUN dotnet restore FinSteady_API.sln --locked-mode

# Now bring in the rest of the source and build.
COPY . .
RUN dotnet build FinSteady_API.sln -c Release --no-restore -o /app/build

# ---------------------------------------------------------------------------
# Stage 2: publish (API project only — test assemblies never leave this stage)
# ---------------------------------------------------------------------------
FROM build AS publish
RUN dotnet publish FinSteady_API.csproj -c Release --no-build -o /app/publish

# ---------------------------------------------------------------------------
# Stage 3: final runtime image — no SDK, no source, non-root user
# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create a dedicated non-root user rather than running as the image default
# (root). Least-privilege inside the container even if it's later compromised.
RUN addgroup --system --gid 1000 appgroup \
 && adduser  --system --uid 1000 --ingroup appgroup --shell /bin/false appuser

COPY --from=publish /app/publish .

# ASP.NET Core 8's runtime images listen on 8080 by default for non-root
# users (port 80 requires elevated privileges on Linux).
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

USER appuser

ENTRYPOINT ["dotnet", "FinSteady_API.dll"]