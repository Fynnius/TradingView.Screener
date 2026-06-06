# Release Checklist

Use this checklist when publishing a new NuGet release.

## Prepare

1. Update package versions in:
   - `src/TradingView.Screener/TradingView.Screener.csproj`
   - `src/TradingView.Screener.Cli/TradingView.Screener.Cli.csproj`
2. Update `CHANGELOG.md` with the release date and notable changes.
3. Confirm README examples still match the public API and CLI behavior.

## Verify

Run the full test suite:

```bash
dotnet test TradingView.Screener.sln --configuration Release
```

Create local packages:

```bash
mkdir -p nupkg
dotnet pack src/TradingView.Screener/TradingView.Screener.csproj --configuration Release --output nupkg
dotnet pack src/TradingView.Screener.Cli/TradingView.Screener.Cli.csproj --configuration Release --output nupkg
```

Inspect the generated `.nupkg` files before publishing.

## Publish

1. Commit the version and changelog changes.
2. Create and push a tag using the package version:

```bash
git tag v1.0.1
git push origin v1.0.1
```

3. Confirm the `Publish to NuGet` workflow completed successfully.
4. Confirm both packages are visible on NuGet:
   - `TradingView.Screener`
   - `TradingView.Screener.Cli`
